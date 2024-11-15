using AutoMapper;
using ClassManagement.Api.Common.Exceptions;
using ClassManagement.Api.Data.EF;
using ClassManagement.Api.Data.Entities;
using ClassManagement.Api.DTO.Homeworks;
using ClassManagement.Api.DTO.Page;
using ClassManagement.Api.Services.Storage;
using Microsoft.EntityFrameworkCore;
using Utilities.Handlers;
using Utilities.Interfaces;
using Utilities.Messages;
using static Utilities.Enums.EnumTypes;

namespace ClassManagement.Api.Services.Homeworks
{
    internal class HomeworkService : IHomeworkService, ISortItem<Homework>
    {
        private readonly IStorageService _storageService;

        private readonly AppDbContext _appDbContext;

        private readonly IMapper _mapper;

        public HomeworkService(AppDbContext appDbContext, IMapper mapper, IStorageService storageService)
        {
            _appDbContext = appDbContext;

            _mapper = mapper;

            _storageService = storageService;
        }

        public async Task<int> AddHomeworkToClassAsync(int id, CreateHomeworkRequest request, CancellationToken cancellationToken)
        {
            var entity = await _appDbContext.Classes.FirstOrDefaultAsync(x => x.Id.ToUpper().Equals(request.ClassId.ToUpper()) && x.UserId.Equals(id), cancellationToken);

            if (entity is null) throw new BadRequestException(string.Format(ErrorMessages.NOT_FOUND, "Id"));

            using var transaction = await _appDbContext.Database.BeginTransactionAsync();

            try
            {
                var data = _mapper.Map<Homework>(request);

                string fileName = StringConverter.Convert(entity.Name.ToLower());

                string saveFileResult = await _storageService.SaveFileAsync(request.File, fileName, FileType.Compression, string.Empty, cancellationToken);

                data.FilePath = !string.IsNullOrEmpty(saveFileResult) ? saveFileResult : throw new BadRequestException(string.Format(ErrorMessages.IMAGE_INVALID, "File"));

                entity.Homeworks.Add(data);

                var init = _appDbContext.Database.CreateExecutionStrategy();

                await init.ExecuteAsync(async () =>
                {
                    _appDbContext.Classes.Update(entity);

                    await _appDbContext.SaveChangesAsync();

                    await transaction.CommitAsync();
                });

                return data.Id;
            }

            catch (Exception e)
            {
                await transaction.RollbackAsync(cancellationToken);

                throw e;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _appDbContext.Exercises.FindAsync(id);

            if (entity is null) throw new KeyNotFoundException(string.Format(ErrorMessages.NOT_FOUND, "Id"));

            using var transaction = await _appDbContext.Database.BeginTransactionAsync();

            try
            {
                var init = _appDbContext.Database.CreateExecutionStrategy();

                await init.ExecuteAsync(async () =>
                {
                    _appDbContext.Exercises.Remove(entity);

                    await _appDbContext.SaveChangesAsync();

                    await transaction.CommitAsync();
                });

                return true;
            }

            catch (Exception e)
            {
                await transaction.RollbackAsync();

                throw e;
            }
        }

        public List<Homework> DoSort(List<Homework> entities, SortOrder sortOrder)
        {
            entities = sortOrder switch
            {
                SortOrder.AscendingName => [.. entities.OrderBy(x => x.Title)],

                SortOrder.DescendingName => [.. entities.OrderByDescending(x => x.Title)],

                SortOrder.DescendingId => [.. entities.OrderByDescending(x => x.Id)],

                _ => [.. entities.OrderBy(x => x.Id)]
            };

            return entities;
        }

        public async Task<PageResult<HomeworkResponse>> GetAsync(UserPageRequest request)
        {
            //var query = from e in _appDbContext.Exercises
            //            join c in _appDbContext.Classes on e.ClassId equals c.Id
            //            select new { e, c.Name };
            //if (!string.IsNullOrEmpty(request.Keyword))
            //{
            //    query = query.Where(x => x.Name.ToUpper().Contains(request.Keyword)
            //                        || x.e.ClassId.ToUpper().Contains(request.Keyword)
            //                        || x.e.Status.ToUpper().Contains(request.Keyword)
            //                        || x.e.Title.ToUpper().Contains(request.Keyword));
            //}
            //var list = new List<Data.Entities.Homework>();
            //foreach (var item in query)
            //{
            //    var entity = new Data.Entities.Homework
            //    {
            //        Id = item.e.Id,
            //        Title = item.e.Title,
            //        Description = item.e.Description,
            //        CreatedAt = item.e.CreatedAt,
            //        Status = item.e.Status,
            //        StartedAt = item.e.StartedAt,
            //        EndedAt = item.e.EndedAt,
            //        FilePath = item.e.FilePath,
            //        ClassId = item.e.ClassId
            //    };
            //    list.Add(entity);
            //}
            //_mapper.Map(DoSort(list, request.SortProperty, request.SortOrder), query);
            //var total = await query.CountAsync();
            //var pageList = await query.Skip((request.PageIndex - 1) * request.PageSize)
            //                    .Take(request.PageSize)
            //                    .Select(x => new ExerciseResponse
            //                    {
            //                        Id = x.e.Id,
            //                        Title = x.e.Title,
            //                        Description = x.e.Description,
            //                        CreatedAt = x.e.CreatedAt,
            //                        Status = x.e.Status,
            //                        StartedAt = x.e.StartedAt,
            //                        EndedAt = x.e.EndedAt,
            //                        FilePath = x.e.FilePath,
            //                        ClassName = x.Name
            //                    })
            //                    .ToListAsync();
            //var pageResult = new PageResult<ExerciseResponse>()
            //{
            //    TotalRecords = total,
            //    PageSize = request.PageSize,
            //    PageIndex = request.PageIndex,
            //    Items = pageList
            //};
            //return pageResult;
            throw new BadRequestException();
        }

        public async Task<HomeworkResponse> GetByIdAsync(int id)
        {
            var entity = await _appDbContext.Homeworks.Include(x => x.Class).FirstOrDefaultAsync(x => x.Id.Equals(id));

            if (entity is null) throw new KeyNotFoundException(string.Format(ErrorMessages.NOT_FOUND, "Id"));

            var result = _mapper.Map<HomeworkResponse>(entity);

            return result;
        }

        public async Task<List<HomeworkResponse>> GetHomeworksByClassId(string classId)
        {
            var entities = await _appDbContext.Homeworks.Where(x => x.ClassId.ToUpper().Equals(classId.ToUpper())).ToListAsync();

            if (entities.Count == 0) return default;

            return _mapper.Map<List<Homework>, List<HomeworkResponse>>(entities);
        }

        public async Task<bool> UpdateAsync(int id, UpdateHomeworkRequest request)
        {
            var entity = await _appDbContext.Exercises.FindAsync(id);

            if (entity is null) throw new KeyNotFoundException(string.Format(ErrorMessages.NOT_FOUND, "Id"));

            var fileSizeLimit = 10 * 1024 * 1024;

            if (request.File?.Length > fileSizeLimit) throw new BadRequestException(string.Format(ErrorMessages.OVER_MAXIMUM_SIZE, "File"));

            using var transaction = await _appDbContext.Database.BeginTransactionAsync();

            try
            {
                _mapper.Map(request, entity);

                var init = _appDbContext.Database.CreateExecutionStrategy();

                await init.ExecuteAsync(async () =>
                {
                    _appDbContext.Exercises.Update(entity);

                    await _appDbContext.SaveChangesAsync();

                    await transaction.CommitAsync();
                });

                return true;
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();

                throw e;
            }
        }
    }
}
