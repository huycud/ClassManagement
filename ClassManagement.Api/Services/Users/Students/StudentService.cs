using AutoMapper;
using ClassManagement.Api.Data.EF;
using ClassManagement.Api.Data.Entities;
using ClassManagement.Api.DTO.Page;
using ClassManagement.Api.DTO.Users;
using ClassManagement.Api.DTO.Users.Clients;
using ClassManagement.Api.Services.Email;
using ClassManagement.Api.Services.Storage;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Utilities.Interfaces;
using Utilities.Messages;
using static Utilities.Enums.EnumTypes;

namespace ClassManagement.Api.Services.Users.Students
{
    class StudentService(UserManager<User> userManager, AppDbContext appDbContext, IMapper mapper, IStorageService storageService, IEmailService emailService,

                    LinkGenerator linkGenerator, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)

                : UserService(appDbContext, storageService, mapper, userManager, emailService, configuration), IStudentService, ISortItem<Client>
    {
        private readonly LinkGenerator _linkGenerator = linkGenerator;

        protected readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public async Task<PageResult<StudentsInClassResponse>> GetStudentsByClassIdAsync(int id, StudentPageRequest request)
        {
            var query = await _appDbContext.Users.FirstOrDefaultAsync(x => x.Id == id)

                        ?? throw new KeyNotFoundException(string.Format(ErrorMessages.NOT_FOUND, "Id"));

            var classEntity = query.Classes.FirstOrDefault(x => x.Id.Equals(request.Keyword, StringComparison.InvariantCultureIgnoreCase))

                            ?? throw new KeyNotFoundException(string.Format(ErrorMessages.NOT_FOUND, "Class"));

            List<ClientResponse> students = [];

            foreach (var item in classEntity.StudentClasses)
            {
                var entity = await _appDbContext.Users.FirstOrDefaultAsync(x => x.Client.UserId == item.UserId);

                var studentResponse = _mapper.Map<User, ClientResponse>(entity);

                students.Add(studentResponse);
            }
            //var query = from u in _appDbContext.Users

            //            where u.Id == id

            //            join cl in _appDbContext.Classes on u.Id equals cl.UserId

            //            join c in _appDbContext.Clients on u.Id equals c.UserId//

            //            join sc in _appDbContext.StudentClasses on new { UserId = u.Id, ClassId = cl.Id } equals new { sc.UserId, sc.ClassId }

            //            join d in _appDbContext.Departments on c.DepartmentId equals d.Id

            //            join img in _appDbContext.Images on u.Id equals img.UserId

            //            select new { c, img.ImagePath, d.Name };

            //if (!string.IsNullOrEmpty(request.Keyword))
            //{
            //    query = query.Where(x => x.c.Firstname.ToUpper().Contains(request.Keyword.ToUpper())

            //                        || x.c.Lastname.ToUpper().Contains(request.Keyword.ToUpper())

            //                        || x.c.Id.ToString().Contains(request.Keyword)

            //                        || x.Name.ToUpper().Contains(request.Keyword.ToUpper()));
            //}

            //var list = new List<Client>();

            //foreach (var item in query)
            //{
            //    var entity = new Client
            //    {
            //        UserId = item.c.UserId,

            //        Firstname = item.c.Firstname,

            //        Lastname = item.c.Lastname,

            //        Address = item.c.Address,

            //        DateOfBirth = item.c.DateOfBirth,

            //        Gender = item.c.Gender,

            //        DepartmentId = item.c.DepartmentId
            //    };

            //    list.Add(entity);
            //}

            //_mapper.Map(DoSort(list, request.SortProperty, request.SortOrder), query);

            int total = students.Count;

            var pageList = students.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize)

                .Select(x => new StudentsInClassResponse
                {
                    UserId = x.Id,

                    Firstname = x.Firstname,

                    Lastname = x.Lastname,

                    Address = x.Address,

                    DateOfBirth = x.DateOfBirth,

                    Gender = x.Gender,

                    DepartmentName = x.DepartmentName,

                    ImagePath = x.ImagePath

                }).ToList();

            var pageResult = new PageResult<StudentsInClassResponse>()
            {
                TotalRecords = total,

                PageSize = request.PageSize,

                PageIndex = request.PageIndex,

                Items = pageList
            };

            return pageResult;
        }

        //public async Task<bool> UpdateAsync(int id, UpdateClientInfoRequest request)
        //{
        //    return await UpdateUserAsync(id, request);
        //}

        public async Task<bool> UpdateImageAsync(int id, UpdateImageRequest request, CancellationToken cancellationToken)
        {
            return await UpdateAvatarAsync(id, request, cancellationToken);
        }

        public async Task<bool> UpdatePasswordAsync(int id, UpdatePasswordRequest request)
        {
            return await ChangePasswordAsync(id, request);
        }

        public List<Client> DoSort(List<Client> entities, SortOrder sortOrder)
        {
            entities = sortOrder switch
            {
                SortOrder.AscendingName => [.. entities.OrderBy(x => x.Firstname)],

                SortOrder.DescendingName => [.. entities.OrderByDescending(x => x.Firstname)],

                SortOrder.DescendingId => [.. entities.OrderByDescending(x => x.Id)],

                _ => [.. entities.OrderBy(x => x.Id)]
            };

            return entities;
        }
    }
}
