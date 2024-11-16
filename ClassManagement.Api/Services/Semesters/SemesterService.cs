using AutoMapper;
using ClassManagement.Api.Common.Exceptions;
using ClassManagement.Api.Data.EF;
using ClassManagement.Api.Data.Entities;
using ClassManagement.Api.DTO.Page;
using ClassManagement.Api.DTO.Semester;
using Microsoft.EntityFrameworkCore;
using Utilities.Interfaces;
using Utilities.Messages;
using static Utilities.Enums.EnumTypes;

namespace ClassManagement.Api.Services.Semesters
{
    class SemesterService(AppDbContext appDbContext, IMapper mapper) : ISemesterService, ISortItem<Semester>
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        private readonly IMapper _mapper = mapper;

        public async Task<string> CreateAsync(CreateSemesterRequest request)
        {
            var semesterEntity = await _appDbContext.Semesters.AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(request.Id.ToUpper()));

            if (semesterEntity is not null) throw new BadRequestException(string.Format(ErrorMessages.DUPLICATE_VALIDATOR, "Id"));

            var createSemesterEntity = _mapper.Map<CreateSemesterRequest, Semester>(request);

            await _appDbContext.Semesters.AddAsync(createSemesterEntity);

            await _appDbContext.SaveChangesAsync();

            return createSemesterEntity.Id;
        }

        public async Task<PageResult<SemesterResponse>> GetAsync(CommonPageRequest request)
        {
            var query = _appDbContext.Semesters.AsQueryable();

            if (!string.IsNullOrEmpty(request.Keyword))

                query = query.Where(x => x.Name.Contains(request.Keyword.ToUpper()) || x.Id.Contains(request.Keyword.ToUpper()));

            var semesterEntities = await query.ToListAsync();

            semesterEntities = DoSort(semesterEntities, request.SortOrder);

            var result = semesterEntities.Select(item => _mapper.Map<SemesterResponse>(item)).ToList();

            var total = result.Count;

            var pageList = result.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).Select(item => item);

            var pageResult = new PageResult<SemesterResponse>()
            {
                TotalRecords = total,

                PageSize = request.PageSize,

                PageIndex = request.PageIndex,

                Items = _mapper.Map<IEnumerable<SemesterResponse>, List<SemesterResponse>>(pageList)
            };

            return pageResult;
        }

        public async Task<List<SemesterResponse>> GetAsync()
        {
            var semesterEntities = await _appDbContext.Semesters.ToListAsync();

            return _mapper.Map<List<Semester>, List<SemesterResponse>>(semesterEntities);
        }

        public async Task<SemesterResponse> GetByIdAsync(string id)
        {
            var entity = await _appDbContext.Semesters.FirstOrDefaultAsync(x => x.Id.Equals(id.ToUpper()));

            return entity is null

                ? throw new KeyNotFoundException(string.Format(ErrorMessages.NOT_FOUND, "Id"))

                : _mapper.Map<SemesterResponse>(entity);
        }

        public async Task<bool> UpdateAsync(string id, UpdateSemesterRequest request)
        {
            var entity = await _appDbContext.Semesters.FirstOrDefaultAsync(x => x.Id.Equals(id.ToUpper()))

                        ?? throw new KeyNotFoundException(string.Format(ErrorMessages.NOT_FOUND, "Id"));

            _mapper.Map(request, entity);

            using var transaction = await _appDbContext.Database.BeginTransactionAsync();

            try
            {
                var init = _appDbContext.Database.CreateExecutionStrategy();

                await init.ExecuteAsync(async () =>
                {
                    await _appDbContext.SaveChangesAsync();

                    await transaction.CommitAsync();
                });

                return true;
            }

            catch (Exception e)
            {
                await transaction.RollbackAsync();

                return false;
            }
        }

        public List<Semester> DoSort(List<Semester> entities, SortOrder sortOrder)
        {
            entities = sortOrder switch
            {
                SortOrder.AscendingName => [.. entities.OrderBy(x => x.Name)],

                SortOrder.DescendingName => [.. entities.OrderByDescending(x => x.Name)],

                SortOrder.DescendingId => [.. entities.OrderByDescending(x => x.Id)],

                _ => [.. entities.OrderBy(x => x.Id)],
            };

            return entities;
        }
    }
}
