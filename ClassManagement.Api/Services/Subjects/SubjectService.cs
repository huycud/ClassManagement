using AutoMapper;
using ClassManagement.Api.Common.Exceptions;
using ClassManagement.Api.Data.EF;
using ClassManagement.Api.Data.Entities;
using ClassManagement.Api.DTO.Page;
using ClassManagement.Api.DTO.Subject;
using Microsoft.EntityFrameworkCore;
using Utilities.Handlers;
using Utilities.Interfaces;
using Utilities.Messages;
using static Utilities.Enums.EnumTypes;

namespace ClassManagement.Api.Services.Subjects
{
    class SubjectService(AppDbContext appDbContext, IMapper mapper) : ISubjectService, ISortItem<Subject>
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        private readonly IMapper _mapper = mapper;

        public async Task<string> CreateAsync(CreateSubjectRequest request)
        {
            var subjectEntity = await _appDbContext.Subjects.AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(request.Id.ToUpper()));

            if (subjectEntity is not null) throw new BadRequestException(string.Format(ErrorMessages.DUPLICATE_VALIDATOR, "Id"));

            var departmentEntity = await _appDbContext.Departments.AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(request.DepartmentId))

                                ?? throw new KeyNotFoundException(string.Format(ErrorMessages.NOT_FOUND, "Department"));

            var createSubjectEntity = _mapper.Map<Subject>(request);

            await _appDbContext.Subjects.AddAsync(createSubjectEntity);

            await _appDbContext.SaveChangesAsync();

            return createSubjectEntity.Id;
        }

        public async Task<PageResult<SubjectResponse>> GetAsync(SubjectDepartmentPageRequest request)
        {
            var query = _appDbContext.Subjects.AsQueryable();

            if (!string.IsNullOrEmpty(request.DepartmentId))

                query = query.Where(x => x.DepartmentId.Equals(request.DepartmentId.ToUpper()));

            if (!string.IsNullOrEmpty(request.Keyword))

                query = query.Where(x => x.Id.Contains(request.Keyword.ToUpper())

                                            || Normalize.NormalizeString(x.Name).Contains(Normalize.NormalizeString(request.Keyword))

                                            || Normalize.NormalizeString(x.Department.Name).Contains(Normalize.NormalizeString(request.Keyword)));

            var subjectEntities = await query.ToListAsync();

            subjectEntities = DoSort(subjectEntities, request.SortOrder);

            var total = subjectEntities.Count;

            var result = new List<SubjectResponse>();

            for (int i = 0; i < total; i++)
            {
                var item = _mapper.Map<SubjectResponse>(subjectEntities[i]);

                item.ClassesId = item.ClassesId ?? new List<string>();

                if (subjectEntities[i].Classes is not null)
                {
                    for (int j = 0; j < subjectEntities[i].Classes.Count; j++)
                    {
                        var classId = subjectEntities[i].Classes[j].Id;

                        item.ClassesId.Add(classId);
                    }
                }

                result.Add(item);
            }

            var pageList = result.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).Select(item => item);

            var pageResult = new PageResult<SubjectResponse>
            {
                TotalRecords = total,

                PageSize = request.PageSize,

                PageIndex = request.PageIndex,

                Items = _mapper.Map<IEnumerable<SubjectResponse>, List<SubjectResponse>>(pageList)
            };

            return pageResult;
        }

        public async Task<List<SubjectResponse>> GetAsync()
        {
            var subjectEntities = await _appDbContext.Subjects.ToListAsync();

            return _mapper.Map<List<Subject>, List<SubjectResponse>>(subjectEntities);
        }

        public async Task<SubjectResponse> GetByIdAsync(string id)
        {
            var entity = await _appDbContext.Subjects.FirstOrDefaultAsync(x => x.Id.Equals(id.ToUpper()))

                        ?? throw new KeyNotFoundException(string.Format(ErrorMessages.NOT_FOUND, "Id"));

            var result = _mapper.Map<SubjectResponse>(entity);

            result.ClassesId = result.ClassesId ?? [];

            if (entity.Classes is not null)
            {
                foreach (var item in entity.Classes) result.ClassesId.Add(item.Id);
            }

            return result;
        }

        public async Task<bool> UpdateAsync(string id, UpdateSubjectRequest request)
        {
            var entity = await _appDbContext.Subjects.FirstOrDefaultAsync(x => x.Id.Equals(id.ToUpper()))

                        ?? throw new KeyNotFoundException(string.Format(ErrorMessages.NOT_FOUND, "Id"));

            if (entity.Classes?.Count != 0 && request.IsPracticed != entity.IsPracticed)

                throw new BadRequestException(string.Format(ErrorMessages.INVALID_PRACTICE, "Practice"));

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

        public List<Subject> DoSort(List<Subject> entities, SortOrder sortOrder)
        {
            entities = sortOrder switch
            {
                SortOrder.AscendingName => [.. entities.OrderBy(x => x.Name)],

                SortOrder.DescendingName => [.. entities.OrderByDescending(x => x.Name)],

                SortOrder.DescendingId => [.. entities.OrderByDescending(x => x.Id)],

                _ => [.. entities.OrderBy(x => x.Id)]
            };

            return entities;
        }
    }
}
