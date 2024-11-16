using AutoMapper;
using ClassManagement.Api.Common.Exceptions;
using ClassManagement.Api.Data.EF;
using ClassManagement.Api.Data.Entities;
using ClassManagement.Api.DTO.Notifies;
using ClassManagement.Api.DTO.Page;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Utilities.Common;
using Utilities.Interfaces;
using Utilities.Messages;
using static Utilities.Enums.EnumTypes;

namespace ClassManagement.Api.Services.Notifies
{
    class NotifyService(AppDbContext appDbContext, IMapper mapper, UserManager<User> userManager) : INotifyService, ISortItem<Notify>
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        private readonly UserManager<User> _userManager = userManager;

        private readonly IMapper _mapper = mapper;

        public async Task<string> CreateAsync(CreateNotifyRequest request)
        {
            var id = Mapper.Utilities.ConvertIdString(request.Title);

            var notifyEntity = await _appDbContext.Notifies.AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(id));

            if (notifyEntity is not null) throw new BadRequestException(string.Format(ErrorMessages.DUPLICATE_VALIDATOR, "Title"));

            var userEntity = await _appDbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(request.UserId))

                            ?? throw new KeyNotFoundException(string.Format(ErrorMessages.NOT_FOUND, "User"));

            var isValidUserRole = await _userManager.IsInRoleAsync(userEntity, RoleConstants.ADMIN_NAME) || await _userManager.IsInRoleAsync(userEntity, RoleConstants.TEACHER_NAME);

            if (!isValidUserRole) throw new BadRequestException(string.Format(ErrorMessages.FORBIDDEN, "UserRole"));

            var createNotifyEntity = _mapper.Map<Notify>(request);

            await _appDbContext.Notifies.AddAsync(createNotifyEntity);

            await _appDbContext.SaveChangesAsync();

            return createNotifyEntity.Id;
        }

        public async Task<PageResult<NotifyResponse>> GetAsync(NotifyPageRequest request)
        {
            var query = _appDbContext.Notifies.Where(x => x.IsDeleted == request.IsDeleted);

            if (!string.IsNullOrEmpty(request.UserId.ToString()))

                query = query.Where(x => x.UserId.Equals(request.UserId));

            if (!string.IsNullOrEmpty(request.Type.ToString()))

                query = query.Where(x => x.Type.Equals(request.Type.ToString()));

            if (!string.IsNullOrEmpty(request.Keyword))

                query = query.Where(x => x.Title.ToString().Contains(request.Keyword.ToUpper()) || x.Id.Contains(request.Keyword.ToLower()));

            var notifyEntities = await query.ToListAsync();

            notifyEntities = DoSort(notifyEntities, request.SortOrder);

            var result = notifyEntities.Select(item => _mapper.Map<NotifyResponse>(item)).ToList();

            var total = result.Count;

            var pageList = result.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).Select(item => item);

            var pageResult = new PageResult<NotifyResponse>()
            {
                TotalRecords = total,

                PageSize = request.PageSize,

                PageIndex = request.PageIndex,

                Items = _mapper.Map<IEnumerable<NotifyResponse>, List<NotifyResponse>>(pageList)
            };

            return pageResult;
        }

        public async Task<NotifyResponse> GetByIdAsync(string id)
        {
            var entity = await _appDbContext.Notifies.FirstOrDefaultAsync(x => x.Id.Equals(id.ToLower()));

            return entity is null

                ? throw new KeyNotFoundException(string.Format(ErrorMessages.NOT_FOUND, "Id"))

                : _mapper.Map<NotifyResponse>(entity);
        }

        public async Task<bool> UpdateAsync(string id, UpdateNotifyRequest request)
        {
            var entity = await _appDbContext.Notifies.FirstOrDefaultAsync(x => x.Id.Equals(id.ToLower()))

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

        public List<Notify> DoSort(List<Notify> entities, SortOrder sortOrder)
        {
            entities = sortOrder switch
            {
                SortOrder.DescendingName => [.. entities.OrderByDescending(x => x.Title)],

                _ => [.. entities.OrderBy(x => x.Title)],
            };

            return entities;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _appDbContext.Notifies.FirstOrDefaultAsync(x => x.Id.Equals(id, StringComparison.InvariantCultureIgnoreCase))

                        ?? throw new KeyNotFoundException(string.Format(ErrorMessages.NOT_FOUND, "Id"));

            using var transaction = await _appDbContext.Database.BeginTransactionAsync();

            try
            {
                var init = _appDbContext.Database.CreateExecutionStrategy();

                await init.ExecuteAsync(async () =>
                {
                    _appDbContext.Notifies.Remove(entity);

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

        public async Task<bool> ChangeStatusAsync(string id, ChangeNotifyStatusRequest request)
        {
            var entity = await _appDbContext.Notifies.FirstOrDefaultAsync(x => x.Id.Equals(id, StringComparison.InvariantCultureIgnoreCase) && x.UserId == request.UserId)

                        ?? throw new KeyNotFoundException(string.Format(ErrorMessages.NOT_FOUND, "Id"));

            entity.IsDeleted = request.IsDeleted;

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
    }
}
