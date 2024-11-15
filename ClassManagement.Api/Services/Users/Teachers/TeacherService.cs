using AutoMapper;
using ClassManagement.Api.Common.Exceptions;
using ClassManagement.Api.Data.EF;
using ClassManagement.Api.Data.Entities;
using ClassManagement.Api.DTO.Homeworks;
using ClassManagement.Api.Services.Email;
using ClassManagement.Api.Services.Storage;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Utilities.Interfaces;
using Utilities.Messages;
using static Utilities.Enums.EnumTypes;

namespace ClassManagement.Api.Services.Users.Teachers
{
    class TeacherService(UserManager<User> userManager, AppDbContext appDbContext, IStorageService storageService, IMapper mapper, IEmailService emailService,

                    LinkGenerator linkGenerator, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)

                    : UserService(appDbContext, storageService, mapper, userManager, emailService, configuration), ITeacherService, ISortItem<Client>
    {
        private readonly LinkGenerator _linkGenerator = linkGenerator;

        protected readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public async Task<bool> AddHomeworkToClassAsync(int id, CreateHomeworkRequest request, CancellationToken cancellationToken)
        {
            var entity = await _appDbContext.Classes.FirstOrDefaultAsync(x => x.Id.Equals(request.ClassId, StringComparison.InvariantCultureIgnoreCase) 
            
                                                                            && x.UserId.Equals(id), cancellationToken)

                        ?? throw new BadRequestException(string.Format(ErrorMessages.NOT_FOUND, "Id"));

            using var transaction = await _appDbContext.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                var data = _mapper.Map<Homework>(request);

                data.FilePath = await _storageService.SaveFileAsync(request.File, entity.Name, FileType.Compression, string.Empty, cancellationToken);

                entity.Homeworks.Add(data);

                var init = _appDbContext.Database.CreateExecutionStrategy();

                await init.ExecuteAsync(async () =>
                {
                    _appDbContext.Classes.Update(entity);

                    await _appDbContext.SaveChangesAsync();

                    await transaction.CommitAsync();
                });

                return true;
            }

            catch (Exception e)
            {
                await transaction.RollbackAsync(cancellationToken);

                return false;
            }
        }

        //public async Task<PageResult<StudentsInClassResponse>> GetStudentsByClassIdAsync(int id, StudentPageRequest request)
        //{
        //    var query = from u in _appDbContext.Users

        //                where u.Id == id

        //                join cl in _appDbContext.Classes on u.Id equals cl.UserId

        //                join c in _appDbContext.Clients on u.Id equals c.UserId

        //                join sc in _appDbContext.StudentClasses on new { UserId = u.Id, ClassId = cl.Id } equals new { sc.UserId, sc.ClassId }

        //                join d in _appDbContext.Departments on c.DepartmentId equals d.Id

        //                join img in _appDbContext.Images on u.Id equals img.UserId

        //                select new { c, img.ImagePath, d.Name };

        //    if (!string.IsNullOrEmpty(request.Keyword))
        //    {
        //        query = query.Where(x => x.c.Firstname.ToUpper().Contains(request.Keyword.ToUpper())

        //                            || x.c.Lastname.ToUpper().Contains(request.Keyword.ToUpper())

        //                            || x.c.Id.ToString().Contains(request.Keyword)

        //                            || x.Name.ToUpper().Contains(request.Keyword.ToUpper()));
        //    }

        //    var list = new List<Client>();

        //    foreach (var item in query)
        //    {
        //        var entity = new Client
        //        {
        //            UserId = item.c.UserId,

        //            Firstname = item.c.Firstname,

        //            Lastname = item.c.Lastname,

        //            Address = item.c.Address,

        //            DateOfBirth = item.c.DateOfBirth,

        //            Gender = item.c.Gender,

        //            DepartmentId = item.c.DepartmentId
        //        };

        //        list.Add(entity);
        //    }
        //    _mapper.Map(DoSort(list, request.SortProperty, request.SortOrder), query);

        //    int total = await query.CountAsync();

        //    var pageList = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize)

        //        .Select(x => new StudentsInClassResponse
        //        {
        //            UserId = x.c.UserId,

        //            Firstname = x.c.Firstname,

        //            Lastname = x.c.Lastname,

        //            Address = x.c.Address,

        //            DateOfBirth = x.c.DateOfBirth,

        //            Gender = x.c.Gender,

        //            DepartmentName = x.Name,

        //            ImagePath = x.ImagePath

        //        }).ToListAsync();

        //    var pageResult = new PageResult<StudentsInClassResponse>()
        //    {
        //        TotalRecords = total,

        //        PageSize = request.PageSize,

        //        PageIndex = request.PageIndex,

        //        Items = pageList
        //    };

        //    return pageResult;
        //}

        public Task<bool> UpdateExerciseToClassAsync(int id, UpdateHomeworkRequest request)
        {
            throw new NotImplementedException();
        }

        public List<Client> DoSort(List<Client> entities, SortOrder sortOrder)
        {
            entities = sortOrder switch
            {
                SortOrder.AscendingName => [.. entities.OrderBy(x => x.Firstname)],

                SortOrder.DescendingName => [.. entities.OrderByDescending(x => x.Firstname)],

                SortOrder.DescendingId => [.. entities.OrderByDescending(x => x.Id)],

                _ => [.. entities.OrderBy(x => x.Id)],
            };

            return entities;
        }
    }
}
