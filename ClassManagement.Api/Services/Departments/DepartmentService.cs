using AutoMapper;
using ClassManagement.Api.Common.Exceptions;
using ClassManagement.Api.Data.EF;
using ClassManagement.Api.Data.Entities;
using ClassManagement.Api.DTO.Department;
using ClassManagement.Api.DTO.Page;
using Microsoft.EntityFrameworkCore;
using Utilities.Interfaces;
using Utilities.Messages;
using static Utilities.Enums.EnumTypes;

namespace ClassManagement.Api.Services.Departments
{
    class DepartmentService(AppDbContext appDbContext, IMapper mapper) : IDepartmentService, ISortItem<Department>
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        private readonly IMapper _mapper = mapper;

        public async Task<string> CreateAsync(CreateDepartmentRequest request)
        {
            var departmentEntity = await _appDbContext.Departments.AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(request.Id.ToUpper()));

            if (departmentEntity is not null) throw new BadRequestException(string.Format(ErrorMessages.DUPLICATE_VALIDATOR, "Id"));

            var createDepartmentEntity = _mapper.Map<CreateDepartmentRequest, Department>(request);

            await _appDbContext.Departments.AddAsync(createDepartmentEntity);

            await _appDbContext.SaveChangesAsync();

            return createDepartmentEntity.Id;
        }

        //option
        //public async Task<bool> DeleteAsync(string id)
        //{
        //    var entity = await _appDbContext.Departments.FindAsync(id);
        //    if (entity is null)
        //    {
        //        throw new KeyNotFoundException(string.Format(ErrorMessages.NOT_FOUND, "Id"));
        //    }
        //    using var transaction = await _appDbContext.Database.BeginTransactionAsync();
        //    try
        //    {
        //        var init = _appDbContext.Database.CreateExecutionStrategy();
        //        await init.ExecuteAsync(async () =>
        //        {
        //            _appDbContext.Departments.Remove(entity);
        //            await _appDbContext.SaveChangesAsync();
        //            await transaction.CommitAsync();
        //        });
        //        return true;
        //    }
        //    catch (Exception e)
        //    {
        //        await transaction.RollbackAsync();
        //        return false;
        //    }
        //}

        public async Task<PageResult<DepartmentResponse>> GetAsync(CommonPageRequest request)
        {
            var query = _appDbContext.Departments.AsQueryable();

            if (!string.IsNullOrEmpty(request.Keyword))

                query = query.Where(x => x.Name.Contains(request.Keyword.ToUpper()) || x.Id.Contains(request.Keyword.ToUpper()));

            var departmentEntities = await query.ToListAsync();

            departmentEntities = DoSort(departmentEntities, request.SortOrder);

            var total = departmentEntities.Count;

            List<DepartmentResponse> result = [];

            foreach (var entity in departmentEntities)
            {
                var item = _mapper.Map<DepartmentResponse>(entity);

                result.Add(item);
            }
            var pageList = result.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).Select(item => item);

            var pageResult = new PageResult<DepartmentResponse>()
            {
                TotalRecords = total,

                PageSize = request.PageSize,

                PageIndex = request.PageIndex,

                Items = _mapper.Map<IEnumerable<DepartmentResponse>, List<DepartmentResponse>>(pageList)
            };

            return pageResult;
        }

        public async Task<DepartmentResponse> GetByIdAsync(string id)
        {
            var entity = await _appDbContext.Departments.FirstOrDefaultAsync(x => x.Id.Equals(id.ToUpper()));

            return entity is null

                ? throw new KeyNotFoundException(string.Format(ErrorMessages.NOT_FOUND, "Id"))

                : _mapper.Map<DepartmentResponse>(entity);
        }

        public async Task<List<DepartmentResponse>> GetAsync()
        {
            var departmentEntities = await _appDbContext.Departments.ToListAsync();

            return _mapper.Map<List<Department>, List<DepartmentResponse>>(departmentEntities);
        }

        public async Task<bool> UpdateAsync(string id, UpdateDepartmentRequest request)
        {
            var entity = await _appDbContext.Departments.FirstOrDefaultAsync(x => x.Id.Equals(id.ToUpper()))

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

        public List<Department> DoSort(List<Department> entities, SortOrder sortOrder)
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
