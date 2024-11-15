using System.Data;
using AutoMapper;
using ClassManagement.Api.Common.Exceptions;
using ClassManagement.Api.Data.EF;
using ClassManagement.Api.Data.Entities;
using ClassManagement.Api.DTO.AppRole;
using ClassManagement.Api.DTO.Page;
using ClassManagement.Api.DTO.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Utilities.Common;
using Utilities.Interfaces;
using Utilities.Messages;
using static Utilities.Enums.EnumTypes;

namespace ClassManagement.Api.Services.AppRole
{
    class RoleService(RoleManager<Role> roleManager, AppDbContext appDbContext, IMapper mapper, UserManager<User> userManager) : IRoleService, ISortItem<Role>
    {
        private readonly RoleManager<Role> _roleManager = roleManager;

        private readonly UserManager<User> _userManager = userManager;

        private readonly AppDbContext _appDbContext = appDbContext;

        private readonly IMapper _mapper = mapper;

        public async Task<int> CreateAsync(CreateRoleRequest request)
        {
            var roleEntity = await _roleManager.Roles.AsNoTracking().FirstOrDefaultAsync(x => x.Name.Equals(request.Name.ToUpper()));

            if (roleEntity is not null) throw new BadRequestException(string.Format(ErrorMessages.INVALID, "Name"));

            var createRoleEntity = _mapper.Map<Role>(request);

            var result = await _roleManager.CreateAsync(createRoleEntity);

            if (!result.Succeeded) throw new BadRequestException(string.Format(ErrorMessages.HANDLING_FAILURE, "Create"));

            return createRoleEntity.Id;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var roleEntity = await _roleManager.FindByIdAsync(id.ToString())

                        ?? throw new KeyNotFoundException(string.Format(ErrorMessages.NOT_FOUND, "Id"));

            var usersInRole = await _userManager.GetUsersInRoleAsync(roleEntity.Name);

            if (usersInRole.Any()) throw new BadRequestException(string.Format(ErrorMessages.WAS_IN, "Some user", $"{roleEntity.Name} role"));

            if (roleEntity.Name.Equals(RoleConstants.ADMIN_NAME)) throw new BadRequestException(string.Format(ErrorMessages.FORBIDDEN, "Id"));

            using var transaction = await _appDbContext.Database.BeginTransactionAsync();

            try
            {
                var init = _appDbContext.Database.CreateExecutionStrategy();

                await init.ExecuteAsync(async () =>
                {
                    var result = await _roleManager.DeleteAsync(roleEntity);

                    if (!result.Succeeded) throw new BadRequestException();

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

        public async Task<PageResult<RoleResponse>> GetAsync(CommonPageRequest request)
        {
            var query = _roleManager.Roles.AsQueryable();

            if (!string.IsNullOrEmpty(request.Keyword))

                query = query.Where(x => x.Name.Contains(request.Keyword) || x.Description.Contains(request.Keyword));

            var roleEntities = await query.ToListAsync();

            roleEntities = DoSort(roleEntities, request.SortOrder);

            var totalRow = roleEntities.Count;

            var pageList = roleEntities.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).Select(entity => entity);

            var pageResult = new PageResult<RoleResponse>()
            {
                TotalRecords = totalRow,

                PageSize = request.PageSize,

                PageIndex = request.PageIndex,

                Items = _mapper.Map<IEnumerable<Role>, List<RoleResponse>>(pageList)
            };

            return pageResult;
        }

        public async Task<RoleResponse> GetByIdAsync(int id)
        {
            var entity = await _roleManager.FindByIdAsync(id.ToString());

            return entity is null

                ? throw new KeyNotFoundException(string.Format(ErrorMessages.NOT_FOUND, "Id"))

                : _mapper.Map<RoleResponse>(entity);
        }

        public async Task<bool> UpdateAsync(int id, UpdateRoleRequest request)
        {
            var entity = await _roleManager.FindByIdAsync(id.ToString())

                        ?? throw new KeyNotFoundException(string.Format(string.Format(ErrorMessages.NOT_FOUND, "Id")));

            _mapper.Map(request, entity);

            using var transaction = await _appDbContext.Database.BeginTransactionAsync();

            try
            {
                var init = _appDbContext.Database.CreateExecutionStrategy();

                await init.ExecuteAsync(async () =>
                {
                    var result = await _roleManager.UpdateAsync(entity);

                    if (!result.Succeeded) throw new BadRequestException();

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

        public async Task<UserRolesResponse> GetRolesByUserIdAsync(int id)
        {
            var entity = await _appDbContext.Users.FirstOrDefaultAsync(x => x.Id.Equals(id) && x.Admin == null)

                        ?? throw new KeyNotFoundException(string.Format(ErrorMessages.NOT_FOUND, $"Id {id}"));

            var roles = await _userManager.GetRolesAsync(entity);

            return new UserRolesResponse
            {
                Id = entity.Id,

                Roles = roles
            };
        }

        public async Task<List<RoleResponse>> GetAsync()
        {
            var roleEntities = await _appDbContext.Roles.ToListAsync();

            List<RoleResponse> list = new();

            foreach (var entity in roleEntities)
            {
                if (entity.Name.Equals(RoleConstants.ADMIN_NAME)) continue;

                list.Add(_mapper.Map<Role, RoleResponse>(entity));
            }

            return list;
        }

        public List<Role> DoSort(List<Role> entities, SortOrder sortOrder)
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
