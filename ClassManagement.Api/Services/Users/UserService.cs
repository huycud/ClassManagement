using System.Text;
using AutoMapper;
using ClassManagement.Api.Common.Exceptions;
using ClassManagement.Api.Data.EF;
using ClassManagement.Api.Data.Entities;
using ClassManagement.Api.DTO.Authentication;
using ClassManagement.Api.DTO.Sender;
using ClassManagement.Api.DTO.Users;
using ClassManagement.Api.Services.Email;
using ClassManagement.Api.Services.Storage;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Utilities.Common;
using Utilities.Handlers;
using Utilities.Messages;
using static Utilities.Enums.EnumTypes;

// Các phương thức mà cà Admin, Teacher, Student đều sử dụng như:
// Thay đổi mật khẩu
// Thay đổi avatar

namespace ClassManagement.Api.Services.Users
{
    class UserService(AppDbContext appDbContext, IStorageService storageService, IMapper mapper, UserManager<User> userManager, IEmailService emailService,

                    IConfiguration configuration)
    {
        protected readonly AppDbContext _appDbContext = appDbContext;

        protected readonly IStorageService _storageService = storageService;

        protected readonly IMapper _mapper = mapper;

        protected readonly UserManager<User> _userManager = userManager;

        protected readonly IEmailService _emailService = emailService;

        public readonly IConfiguration _configuration = configuration;

        protected async Task<bool> UpdateAvatarAsync(int id, UpdateImageRequest request, CancellationToken cancellationToken)
        {
            var entity = await _appDbContext.Users.FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken)

                        ?? throw new KeyNotFoundException(string.Format(ErrorMessages.NOT_FOUND, "Id"));

            using var transaction = await _appDbContext.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                var init = _appDbContext.Database.CreateExecutionStrategy();

                await init.ExecuteAsync(async () =>
                {
                    string oldPath = entity.Images is null ? "" : entity.Images.ImagePath;

                    string imageFirstname = StringConverter.Convert(entity.UserName.ToLower());

                    var saveFileResult = await _storageService.SaveFileAsync(request.Image, imageFirstname, FileType.Image, oldPath, cancellationToken);

                    if (string.IsNullOrEmpty(saveFileResult)) throw new BadRequestException(ErrorMessages.IMAGE_INVALID);

                    if (entity.Images is null)
                    {
                        entity.Images = new Image
                        {
                            ImagePath = saveFileResult,

                            ImageName = _storageService.GetOriginalFileName(request.Image),

                            Caption = $"{StringConverter.Convert(entity.UserName.ToLower())} Image",

                            CreatedAt = DateTime.UtcNow,

                            FileSize = request.Image.Length,
                        };
                    }

                    else
                    {
                        entity.Images.ImagePath = saveFileResult;

                        entity.Images.ImageName = _storageService.GetOriginalFileName(request.Image);

                        entity.Images.FileSize = request.Image.Length;
                    }

                    var result = await _userManager.UpdateAsync(entity);

                    if (!result.Succeeded) throw new BadRequestException(string.Format(ErrorMessages.HANDLING_FAILURE, "Update image"));

                    await transaction.CommitAsync();
                });

                return true;
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync(cancellationToken);

                throw e;
            }
        }

        protected async Task<bool> ChangePasswordAsync(int id, UpdatePasswordRequest request)
        {
            var entity = await _appDbContext.Users.FirstOrDefaultAsync(x => x.Id.Equals(id))

                        ?? throw new KeyNotFoundException(string.Format(ErrorMessages.NOT_FOUND, "Id"));

            if (entity.IsDisabled) return false;

            if (!await _userManager.CheckPasswordAsync(entity, request.CurrentPassword))

                throw new BadRequestException(string.Format(ErrorMessages.INVALID, "CurrentPassword"));

            if (request.CurrentPassword.Equals(request.NewPassword))

                throw new BadRequestException(string.Format(ErrorMessages.HAS_BEEN_USED, "NewPassword"));

            using var transaction = await _appDbContext.Database.BeginTransactionAsync();

            try
            {
                var init = _appDbContext.Database.CreateExecutionStrategy();

                await init.ExecuteAsync(async () =>
                {
                    var result = await _userManager.ChangePasswordAsync(entity, request.CurrentPassword, request.NewPassword);

                    if (!result.Succeeded) throw new BadRequestException(string.Format(ErrorMessages.HANDLING_FAILURE, "Update password"));

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

        protected async Task<bool> ForgotPasswordAsync(ForgotPasswordRequest request, CancellationToken cancellationToken)
        {
            var entity = await _userManager.FindByEmailAsync(request.Email)

                        ?? throw new BadRequestException(string.Format(ErrorMessages.NOT_FOUND, "Email"));

            var forgotPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(entity);

            var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(forgotPasswordToken));

            var resetPasswordUrl = string.Format(SystemConstants.RESET_PASSWORD_URL, _configuration["BaseAddress"], request.Email, encodedToken);

            var body = string.Format(SystemConstants.RESET_PASSWORD_HTML_BODY, resetPasswordUrl);

            var model = new EmailRequest
            {
                To = entity.Email,

                Subject = "Reset Password",

                Content = body
            };

            await _emailService.SendMailAsync(model, cancellationToken);

            return true;
        }

        protected async Task<bool> ResetPasswordAsync(ResetPasswordRequest request)
        {
            var entity = await _userManager.FindByEmailAsync(request.Email)

                        ?? throw new BadRequestException(string.Format(ErrorMessages.NOT_FOUND, "Email"));

            var decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));

            var result = _userManager.ResetPasswordAsync(entity, decodedToken, request.NewPassword);

            if (!result.IsCompletedSuccessfully) return false;

            return true;
        }

        //protected async Task<bool> UpdateClientAsync(int id, UpdateClientRequest request)
        //{
        //    var entity = await _appDbContext.Users.FirstOrDefaultAsync(x => x.Id.Equals(id));

        //    if (entity is null) throw new KeyNotFoundException(string.Format(ErrorMessages.NOT_FOUND, "Id"));

        //    _mapper.Map(request, entity);

        //    _mapper.Map(request, entity.Client);

        //    using var transaction = await _appDbContext.Database.BeginTransactionAsync();

        //    try
        //    {
        //        var init = _appDbContext.Database.CreateExecutionStrategy();

        //        await init.ExecuteAsync(async () =>
        //        {
        //            var result = await _userManager.UpdateAsync(entity);

        //            if (!result.Succeeded) throw new BadRequestException(string.Format(Messages.FAIL, "Update"));

        //            await transaction.CommitAsync();
        //        });

        //        return true;
        //    }

        //    catch (Exception e)
        //    {
        //        await transaction.RollbackAsync();

        //        throw e;
        //    }
        //}
    }
}
