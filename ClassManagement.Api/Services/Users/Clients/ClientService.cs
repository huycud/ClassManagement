using System.Text;
using AutoMapper;
using ClassManagement.Api.Common.EmailReader;
using ClassManagement.Api.Common.Exceptions;
using ClassManagement.Api.Data.EF;
using ClassManagement.Api.Data.Entities;
using ClassManagement.Api.DTO.Authentication;
using ClassManagement.Api.DTO.Page;
using ClassManagement.Api.DTO.Sender;
using ClassManagement.Api.DTO.Users;
using ClassManagement.Api.DTO.Users.Clients;
using ClassManagement.Api.Services.Email;
using ClassManagement.Api.Services.Storage;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Utilities.Common;
using Utilities.Interfaces;
using Utilities.Messages;
using static Utilities.Enums.EnumTypes;

namespace ClassManagement.Api.Services.Users.Clients
{
    class ClientService(UserManager<User> userManager, AppDbContext appDbContext, IStorageService storageService, IMapper mapper, IEmailService emailService,

            IEmailTemplateReader emailTemplateReader, LinkGenerator linkGenerator, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)

            : UserService(appDbContext, storageService, mapper, userManager, emailService, configuration), IClientService, ISortItem<User>
    {
        private readonly LinkGenerator _linkGenerator = linkGenerator;

        private readonly IEmailTemplateReader _emailTemplateReader = emailTemplateReader;

        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public async Task<int> CreateClientAsync(CreateClientRequest request, CancellationToken cancellationToken)
        {
            var userEntity = await _userManager.Users.AsNoTracking().FirstOrDefaultAsync(x => x.UserName.Equals(request.UserName, StringComparison.InvariantCultureIgnoreCase), cancellationToken);

            if (userEntity is not null) throw new BadRequestException(string.Format(ErrorMessages.DUPLICATE_VALIDATOR, "UserName"));

            var isExistEmail = await _userManager.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email.Equals(request.Email, StringComparison.InvariantCultureIgnoreCase), cancellationToken);

            if (isExistEmail is not null) throw new BadRequestException(string.Format(ErrorMessages.DUPLICATE_VALIDATOR, "Email"));

            var departmentEntity = await _appDbContext.Departments.FirstOrDefaultAsync(x => x.Id.Equals(request.DepartmentId.ToUpper()), cancellationToken)

                                ?? throw new KeyNotFoundException(string.Format(ErrorMessages.NOT_FOUND, "Department"));

            var roles = _appDbContext.Roles.Where(x => !x.Name.Equals(RoleConstants.ADMIN_NAME)).Select(x => new { x.Name });

            if (!roles.Any(x => x.Name.Equals(request.RoleName.ToUpper()))) throw new BadRequestException(string.Format(ErrorMessages.INVALID, "Role name"));

            var createClientEntity = _mapper.Map<User>(request);

            createClientEntity.Client = _mapper.Map<Client>(request);

            var result = await _userManager.CreateAsync(createClientEntity, request.Password);

            if (result.Succeeded)
            {
                var contentTemplate = await _emailTemplateReader.GetTemplateAsync(SystemConstants.CONFIRMEMAIL_TEMPLATE_NAME);

                var confirmToken = await _userManager.GenerateEmailConfirmationTokenAsync(createClientEntity);

                var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(confirmToken));

                var confirmEmailRequest = new ConfirmEmailRequest
                {
                    Email = createClientEntity.Email,

                    ConfirmEmailToken = encodedToken
                };

                var url = _linkGenerator.GetUriByAction(

                    action: "ConfirmEmail",

                    controller: "Clients",

                    values: confirmEmailRequest,

                    scheme: _httpContextAccessor.HttpContext.Request.Scheme,

                    host: _httpContextAccessor.HttpContext.Request.Host);

                var body = string.Format(contentTemplate, createClientEntity.UserName, url);

                var emailRequest = new EmailRequest
                {
                    To = createClientEntity.Email,

                    Subject = "Confirm Email For Register",

                    Content = body
                };

                await _emailService.SendMailAsync(emailRequest, cancellationToken);

                var isSuccess = await _userManager.AddToRoleAsync(createClientEntity, request.RoleName);

                if (!isSuccess.Succeeded) throw new BadRequestException(string.Format(ErrorMessages.HANDLING_FAILURE, "Add role"));

                return createClientEntity.Id;
            }

            throw new BadRequestException(string.Format(ErrorMessages.HANDLING_FAILURE, "Create"));
        }

        public async Task<bool> UpdateImageAsync(int id, UpdateImageRequest request, CancellationToken cancellationToken)
        {
            return await UpdateAvatarAsync(id, request, cancellationToken);
        }

        public async Task<bool> UpdatePasswordAsync(int id, UpdatePasswordRequest request)
        {
            return await ChangePasswordAsync(id, request);
        }

        public async Task<bool> UpdateClientAsync(int id, UpdateClientRequest request)
        {
            var entity = await _appDbContext.Users.FirstOrDefaultAsync(x => x.Id.Equals(id))

                        ?? throw new KeyNotFoundException(string.Format(ErrorMessages.NOT_FOUND, "Id"));

            _mapper.Map(request, entity);

            _mapper.Map(request, entity.Client);

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

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var entity = await _appDbContext.Users.FirstOrDefaultAsync(x => x.Id.Equals(id) && x.Admin == null, cancellationToken)

                        ?? throw new KeyNotFoundException(string.Format(ErrorMessages.NOT_FOUND, "Id"));

            using var transaction = await _appDbContext.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                var init = _appDbContext.Database.CreateExecutionStrategy();

                await init.ExecuteAsync(async () =>
                {
                    var result = await _userManager.DeleteAsync(entity);

                    if (!result.Succeeded) throw new BadRequestException(string.Format(ErrorMessages.HANDLING_FAILURE, "Delete"));

                    if (entity.Images is not null) await _storageService.DeleteFilePathAsync(entity.Images.ImagePath, cancellationToken);

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

        public async Task<PageResult<ClientResponse>> GetClientsByRoleNameAsync(ClientsRolePageRequest request)
        {
            var query = _appDbContext.Users.AsQueryable();

            if (string.IsNullOrEmpty(request.RoleName)) query = query.Where(x => x.Admin == null);

            else
            {
                if (request.RoleName.ToUpper().Equals(RoleConstants.ADMIN_NAME)) throw new BadRequestException(string.Format(ErrorMessages.INVALID, "Role name"));

                var clientsInRoleEntities = await _userManager.GetUsersInRoleAsync(request.RoleName.ToUpper());

                query = query.Where(x => clientsInRoleEntities.Select(c => c.Id).Contains(x.Id));
            }

            if (request.IsDisabled) query = query.Where(x => x.IsDisabled == request.IsDisabled);

            if (!string.IsNullOrEmpty(request.Keyword))

                query = query.Where(x => x.UserName.Contains(request.Keyword, StringComparison.InvariantCultureIgnoreCase) || x.Id.ToString().Contains(request.Keyword));

            var clientEntities = await query.ToListAsync();

            clientEntities = DoSort(clientEntities, request.SortOrder);

            var total = clientEntities.Count;

            var result = new List<ClientResponse>();

            foreach (var entity in clientEntities)
            {
                var iem = _mapper.Map<User, ClientResponse>(entity);

                result.Add(iem);
            }

            var pageList = result.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).Select(item => item);

            var pageResult = new PageResult<ClientResponse>()
            {
                TotalRecords = total,

                PageSize = request.PageSize,

                PageIndex = request.PageIndex,

                Items = _mapper.Map<IEnumerable<ClientResponse>, List<ClientResponse>>(pageList)
            };

            return pageResult;
        }

        //public async Task<ClientResponse> GetByKeyAsync(string key)
        //{
        //    var entity = await _appDbContext.Users.FirstOrDefaultAsync(x => x.Id.ToString().Equals(key)

        //                                                                || x.UserName.ToUpper().Equals(key.ToUpper()))

        //                ?? throw new KeyNotFoundException(string.Format(ErrorMessages.NOT_FOUND, "Key"));

        //    if (entity.Admin is not null) throw new KeyNotFoundException(string.Format(ErrorMessages.FORBIDDEN, "Id"));

        //    return _mapper.Map<ClientResponse>(entity);
        //}

        public async Task<List<ClientResponse>> GetClientsByRoleNameAsync(string roleName)
        {
            var clientEntities = await _userManager.GetUsersInRoleAsync(roleName.ToUpper());

            var result = new List<ClientResponse>();

            foreach (var entity in clientEntities)
            {
                var item = _mapper.Map<User, ClientResponse>(entity);

                result.Add(item);
            }

            return result;
        }

        public async Task<PageResult<ClientResponse>> GetClientsByClassIdAsync(ClientsClassPageRequest request)
        {
            var classEntity = await _appDbContext.Classes.AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(request.ClassId, StringComparison.InvariantCultureIgnoreCase))

                                ?? throw new KeyNotFoundException(string.Format(ErrorMessages.NOT_FOUND, $"Class {request.ClassId}"));

            var query = _appDbContext.Users.Where(x => x.Admin == null && x.StudentClasses.Count != 0

                                                    && x.StudentClasses.Any(x => x.ClassId.Equals(request.ClassId, StringComparison.InvariantCultureIgnoreCase)));

            if (!string.IsNullOrEmpty(request.Keyword))

                query = query.Where(x => x.UserName.Contains(request.Keyword, StringComparison.InvariantCultureIgnoreCase)

                                      || x.Id.ToString().Contains(request.Keyword));

            var clientEntities = await query.ToListAsync();

            clientEntities = DoSort(clientEntities, request.SortOrder);

            var total = clientEntities.Count;

            var result = new List<ClientResponse>();

            foreach (var entity in clientEntities)
            {
                var iem = _mapper.Map<User, ClientResponse>(entity);

                result.Add(iem);
            }

            var pageList = result.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).Select(item => item);

            var pageResult = new PageResult<ClientResponse>()
            {
                TotalRecords = total,

                PageSize = request.PageSize,

                PageIndex = request.PageIndex,

                Items = _mapper.Map<IEnumerable<ClientResponse>, List<ClientResponse>>(pageList)
            };

            return pageResult;
        }

        public async Task<ClientResponse> GetByIdAsync(int id)
        {
            var entity = await _appDbContext.Users.FirstOrDefaultAsync(x => x.Id.Equals(id) && x.Admin == null)

                      ?? throw new KeyNotFoundException(string.Format(ErrorMessages.NOT_FOUND, "Id"));

            return _mapper.Map<ClientResponse>(entity);
        }

        public List<User> DoSort(List<User> entities, SortOrder sortOrder)
        {
            entities = sortOrder switch
            {
                SortOrder.AscendingName => [.. entities.OrderBy(x => x.UserName)],

                SortOrder.DescendingName => [.. entities.OrderByDescending(x => x.UserName)],

                SortOrder.DescendingId => [.. entities.OrderByDescending(x => x.Id)],

                _ => [.. entities.OrderBy(x => x.Id)]
            };

            return entities;
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
