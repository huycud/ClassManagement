using AutoMapper;
using ClassManagement.Api.Common.Exceptions;
using ClassManagement.Api.Data.EF;
using ClassManagement.Api.Data.Entities;
using ClassManagement.Api.DTO.AppRole;
using ClassManagement.Api.DTO.Authentication;
using ClassManagement.Api.DTO.Common;
using ClassManagement.Api.DTO.Users;
using ClassManagement.Api.DTO.Users.Manager;
using ClassManagement.Api.Services.Email;
using ClassManagement.Api.Services.Storage;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Utilities.Common;
using Utilities.Messages;

namespace ClassManagement.Api.Services.Users.Manager
{
    class AdminService(AppDbContext appDbContext, IMapper mapper, ILogger<AdminService> logger, IStorageService storageService, UserManager<User> userManager,

                        IEmailService emailService, LinkGenerator linkGenerator, IHttpContextAccessor httpContextAccessor,

                        IConfiguration configuration)

                : UserService(appDbContext, storageService, mapper, userManager, emailService, configuration), IAdminService
    {
        private readonly ILogger<AdminService> _logger = logger;

        private readonly LinkGenerator _linkGenerator = linkGenerator;

        protected readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public async Task<bool> AddRoleAsync(int id, RoleRequest request)
        {
            var entity = await _appDbContext.Users.FirstOrDefaultAsync(x => x.Id.Equals(id))

                        ?? throw new KeyNotFoundException(string.Format(ErrorMessages.NOT_FOUND, "Id"));

            var result = await _userManager.AddToRoleAsync(entity, request.RoleName);

            if (result.Succeeded) return true;

            return false;
        }

        public async Task<int> CreateAsync(CreateAdminRequest request)
        {
            var adminEntity = await _userManager.Users.AsNoTracking().FirstOrDefaultAsync(x => x.UserName.Equals(request.UserName.ToUpper()));

            if (adminEntity is not null) throw new BadRequestException(string.Format(ErrorMessages.DUPLICATE_VALIDATOR, "UserName"));

            var createAdminEntity = _mapper.Map<User>(request);

            createAdminEntity.Admin = _mapper.Map<Admin>(request);

            var result = await _userManager.CreateAsync(createAdminEntity, request.Password);

            if (result.Succeeded)
            {
                var isSuccess = await _userManager.AddToRoleAsync(createAdminEntity, RoleConstants.ADMIN_NAME);

                if (!isSuccess.Succeeded) throw new BadRequestException(string.Format(ErrorMessages.HANDLING_FAILURE, "Add role"));

                return createAdminEntity.Id;
            }

            throw new BadRequestException(string.Format(ErrorMessages.HANDLING_FAILURE, "Create"));
        }

        public async Task<bool> UpdateAsync(int id, UpdateAdminRequest request)
        {
            var entity = await _appDbContext.Users.FirstOrDefaultAsync(x => x.Id.Equals(id))

                        ?? throw new KeyNotFoundException(string.Format(ErrorMessages.NOT_FOUND, "Id"));

            _mapper.Map(request, entity);

            _mapper.Map(request, entity.Admin);

            using var transaction = await _appDbContext.Database.BeginTransactionAsync();

            try
            {
                var init = _appDbContext.Database.CreateExecutionStrategy();

                await init.ExecuteAsync(async () =>
                {
                    var result = await _userManager.UpdateAsync(entity);

                    if (!result.Succeeded) throw new BadRequestException(string.Format(ErrorMessages.HANDLING_FAILURE, "Update"));

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

        public async Task<bool> UpdateImageAsync(int id, UpdateImageRequest request, CancellationToken cancellationToken)
        {
            return await UpdateAvatarAsync(id, request, cancellationToken);
        }

        public async Task<bool> UpdatePasswordAsync(int id, UpdatePasswordRequest request)
        {
            return await ChangePasswordAsync(id, request);
        }

        public async Task<bool> DisableAccountAsync(int id, DisableAccountRequest request)
        {
            if (await _appDbContext.Admins.FindAsync(id.ToString()) is not null)

                throw new BadRequestException(string.Format(ErrorMessages.FORBIDDEN, "Id"));

            var entity = await _appDbContext.Users.FirstOrDefaultAsync(x => x.Id.Equals(id))

                        ?? throw new KeyNotFoundException(string.Format(ErrorMessages.NOT_FOUND, "Id"));

            entity.IsDisabled = request.IsDisabled;

            using var transaction = await _appDbContext.Database.BeginTransactionAsync();

            try
            {
                var init = _appDbContext.Database.CreateExecutionStrategy();

                await init.ExecuteAsync(async () =>
                {
                    _appDbContext.Users.Update(entity);

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

        public async Task<bool> RemoveRoleAsync(int id, RoleRequest request)
        {
            var entity = await _appDbContext.Users.FirstOrDefaultAsync(x => x.Id.Equals(id))

                        ?? throw new KeyNotFoundException(string.Format(ErrorMessages.NOT_FOUND, "Id"));

            var result = await _userManager.RemoveFromRoleAsync(entity, request.RoleName);

            if (result.Succeeded) return true;

            return false;
        }

        public async Task<AdminResponse> GetByIdAsync(int id)
        {
            var entity = await _appDbContext.Users.FirstOrDefaultAsync(x => x.Id.Equals(id) && x.Admin != null);

            return entity is null

                ? throw new KeyNotFoundException(string.Format(ErrorMessages.NOT_FOUND, "Id"))

                : _mapper.Map<AdminResponse>(entity);
        }

        public new async Task<bool> ForgotPasswordAsync(ForgotPasswordRequest request, CancellationToken cancellationToken)
        {
            return await base.ForgotPasswordAsync(request, cancellationToken);
        }

        public new async Task<bool> ResetPasswordAsync(ResetPasswordRequest request)
        {
            return await base.ResetPasswordAsync(request);
        }
    }
}
