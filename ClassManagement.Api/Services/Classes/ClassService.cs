using AutoMapper;
using ClassManagement.Api.Common.Exceptions;
using ClassManagement.Api.Data.EF;
using ClassManagement.Api.Data.Entities;
using ClassManagement.Api.DTO.Class;
using ClassManagement.Api.DTO.Common;
using ClassManagement.Api.DTO.Page;
using ClassManagement.Api.DTO.Users.Clients;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Utilities.Common;
using Utilities.Handlers;
using Utilities.Interfaces;
using Utilities.Messages;
using static Utilities.Enums.EnumTypes;

namespace ClassManagement.Api.Services.Classes
{
    class ClassService(AppDbContext appDbContext, IMapper mapper, UserManager<User> userManager) : IClassService, ISortItem<Class>
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        private readonly UserManager<User> _userManager = userManager;

        private readonly IMapper _mapper = mapper;

        public async Task<bool> AddStudentToClassAsync(string id, List<int> request)
        {
            if (request.Count == 0) throw new BadRequestException(string.Format(ErrorMessages.INVALID, "List"));

            var classEntity = await _appDbContext.Classes.Include(x => x.StudentClasses).FirstOrDefaultAsync(x => x.Id.Equals(id.ToUpper()))

                            ?? throw new KeyNotFoundException(string.Format(ErrorMessages.NOT_FOUND, $"Class {id}"));

            if (classEntity.Amount == classEntity.ClassSize) throw new BadRequestException(string.Format(ErrorMessages.WAS_FULL, $"Class {classEntity.Id}"));

            if (classEntity.Amount + request.Count > classEntity.ClassSize)

                throw new BadRequestException(string.Format(ErrorMessages.AMOUNT_INVALID, "Amount"));

            foreach (var studentId in request)
            {
                var entity = await _appDbContext.Users.FirstOrDefaultAsync(x => x.Id.Equals(studentId))

                            ?? throw new KeyNotFoundException(string.Format(ErrorMessages.NOT_FOUND, $"Student {studentId}"));

                if (classEntity.UserId.Equals(studentId)) throw new BadRequestException(string.Format(ErrorMessages.INVALID, $"Student {studentId}"));

                if (classEntity.StudentClasses.Any(s => s.UserId == studentId))

                    throw new BadRequestException(string.Format(ErrorMessages.WAS_IN, $"Student {studentId}", $"class {classEntity.Id}"));

                if (classEntity.Type == ClassType.Practice.ToString())
                {
                    if (entity.StudentClasses.Count == 0 || !entity.StudentClasses.Any(x => x.Class.Type == ClassType.Theory.ToString()

                                                         && x.Class.SubjectId.Equals(classEntity.SubjectId, StringComparison.InvariantCultureIgnoreCase)))
                    {
                        throw new KeyNotFoundException(string.Format(ErrorMessages.NOT_FOUND, "TheoryClass"));
                    }
                }

                classEntity.StudentClasses.Add(new StudentClass { ClassId = classEntity.Id, UserId = studentId });

                classEntity.Amount++;
            }

            //classEntity.Amount += request.Count;

            using var transaction = await _appDbContext.Database.BeginTransactionAsync();

            try
            {
                var init = _appDbContext.Database.CreateExecutionStrategy();

                await init.ExecuteAsync(async () =>
                {
                    _appDbContext.Classes.Update(classEntity);

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

        public async Task<string> CreateAsync(CreateClassRequest request)
        {
            var classEntity = await _appDbContext.Classes.AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(request.Id));

            if (classEntity is not null) throw new BadRequestException(string.Format(ErrorMessages.DUPLICATE_VALIDATOR, "Id"));

            var subjectEntity = await _appDbContext.Subjects.AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(request.SubjectId)

                                                                                                  && x.Status.Equals(Status.Opening.ToString()))

                                ?? throw new KeyNotFoundException(string.Format(ErrorMessages.NOT_FOUND, "Subject"));

            var userEntity = await _appDbContext.Users.FirstOrDefaultAsync(x => x.Id.Equals(request.UserId))

                                ?? throw new KeyNotFoundException(string.Format(ErrorMessages.NOT_FOUND, "Teacher"));

            if (request.Type == ClassType.Practice && !subjectEntity.IsPracticed)

                throw new BadRequestException(string.Format(ErrorMessages.INVALID, "ClassType"));

            var semesterEntity = await _appDbContext.Semesters.AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(request.SemesterId))

                                ?? throw new KeyNotFoundException(string.Format(ErrorMessages.NOT_FOUND, "Semester"));

            var isValidTeacher = await CheckRoleAsync(userEntity, RoleConstants.TEACHER_NAME);

            if (!isValidTeacher) throw new BadRequestException(string.Format(ErrorMessages.INVALID, "Teacher"));

            if (!userEntity.Client.DepartmentId.Equals(subjectEntity.DepartmentId, StringComparison.InvariantCultureIgnoreCase))

                throw new BadRequestException(string.Format(ErrorMessages.NOT_RELATED, "Teacher", "Subject"));

            var createClassEntity = _mapper.Map<CreateClassRequest, Class>(request);

            if (createClassEntity.Type == ClassType.Practice.ToString())
            {
                createClassEntity.Credit = 1;

                createClassEntity.ClassSize = 50;
            }

            else createClassEntity.Credit = subjectEntity.Credit - 1;

            _appDbContext.Classes.Add(createClassEntity);

            await _appDbContext.SaveChangesAsync();

            return createClassEntity.Id;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _appDbContext.Classes.FirstOrDefaultAsync(x => x.Id.Equals(id.ToUpper()))

                        ?? throw new KeyNotFoundException(string.Format(ErrorMessages.NOT_FOUND, "Id"));

            using var transaction = await _appDbContext.Database.BeginTransactionAsync();

            try
            {
                var init = _appDbContext.Database.CreateExecutionStrategy();

                await init.ExecuteAsync(async () =>
                {
                    _appDbContext.Classes.Remove(entity);

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

        public async Task<PageResult<ClassResponse>> GetAsync(ClassPageRequest request)
        {
            var query = _appDbContext.Classes.AsQueryable();

            if (!string.IsNullOrEmpty(request.SubjectId)) query = query.Where(x => x.SubjectId.Equals(request.SubjectId.ToUpper()));

            if (!string.IsNullOrEmpty(request.Keyword))

                query = query.Where(x => x.Id.Contains(request.Keyword) || x.Name.Contains(request.Keyword, StringComparison.InvariantCultureIgnoreCase));

            var classEntities = await query.ToListAsync();

            classEntities = DoSort(classEntities, request.SortOrder);

            var total = classEntities.Count;

            var result = new List<ClassResponse>();

            foreach (var entity in classEntities)
            {
                var item = _mapper.Map<ClassResponse>(entity);

                item.TeacherItem = new TeacherItem
                {
                    Id = entity.UserId,

                    Fullname = $"{StringConverter.Convert(entity.User.Client.Lastname)} {StringConverter.Convert(entity.User.Client.Firstname)}"
                };

                item.HomeworksId = entity.Homeworks?.Select(x => x.Id).ToList();

                result.Add(item);
            }

            var pageList = result.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).Select(item => item);

            var pageResult = new PageResult<ClassResponse>
            {
                TotalRecords = total,

                PageSize = request.PageSize,

                PageIndex = request.PageIndex,

                Items = _mapper.Map<IEnumerable<ClassResponse>, List<ClassResponse>>(pageList)
            };

            return pageResult;
        }

        public async Task<ClassResponse> GetByIdAsync(string id)
        {
            var entity = await _appDbContext.Classes.FirstOrDefaultAsync(x => x.Id.Equals(id.ToUpper()))

                        ?? throw new KeyNotFoundException(string.Format(ErrorMessages.NOT_FOUND, "Id"));

            var result = _mapper.Map<ClassResponse>(entity);

            result.TeacherItem = new TeacherItem
            {
                Id = entity.UserId,

                Fullname = $"{entity.User.Client.Firstname} {entity.User.Client.Lastname}"
            };

            return result;
        }

        public async Task<bool> UpdateAsync(string id, UpdateClassRequest request)
        {
            var classEntity = await _appDbContext.Classes.FirstOrDefaultAsync(x => x.Id.Equals(id.ToUpper()))

                        ?? throw new KeyNotFoundException(string.Format(ErrorMessages.NOT_FOUND, "Id"));

            var userEntity = await _appDbContext.Users.FirstOrDefaultAsync(x => x.Id.Equals(request.UserId))

                        ?? throw new KeyNotFoundException(string.Format(ErrorMessages.NOT_FOUND, "Teacher"));

            var isValidTeacher = await CheckRoleAsync(userEntity, RoleConstants.TEACHER_NAME);

            if (!isValidTeacher) throw new BadRequestException(string.Format(ErrorMessages.INVALID, "Teacher"));

            if (!userEntity.Client.DepartmentId.Equals(classEntity.Subject.DepartmentId, StringComparison.InvariantCultureIgnoreCase))

                throw new BadRequestException(string.Format(ErrorMessages.NOT_RELATED, "Teacher", "Subject"));

            using var transaction = await _appDbContext.Database.BeginTransactionAsync();

            _mapper.Map(request, classEntity);

            try
            {
                var init = _appDbContext.Database.CreateExecutionStrategy();

                await init.ExecuteAsync(async () =>
                {
                    _appDbContext.Classes.Update(classEntity);

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

        public async Task<PageResult<ClientResponse>> GetStudentsNotExistInClassAsync(string id, UserPageRequest request)
        {
            var classEntity = await _appDbContext.Classes.FirstOrDefaultAsync(x => x.Id.Equals(id.ToUpper()))

                            ?? throw new KeyNotFoundException(string.Format(ErrorMessages.NOT_FOUND, "Class"));

            var studentQuery = _appDbContext.Users.Where(x => x.Admin == null);

            if (request.IsDisabled) studentQuery = studentQuery.Where(x => x.IsDisabled == request.IsDisabled);

            if (!string.IsNullOrEmpty(request.Keyword))

                studentQuery = studentQuery.Where(x => x.UserName.Contains(request.Keyword, StringComparison.InvariantCultureIgnoreCase)

                                                    || x.Id.ToString().Contains(request.Keyword));

            var studentEntities = await studentQuery.ToListAsync();

            studentEntities = [.. studentEntities.OrderBy(x => x.Id)];

            var studentInClassQuery = _appDbContext.StudentClasses.Where(x => x.ClassId.Equals(classEntity.Id));

            var result = studentEntities.Where(entity => !studentInClassQuery.Any(item => entity.Id == item.UserId))

                                        .Select(entity => _mapper.Map<User, ClientResponse>(entity)).ToList();

            var total = result.Count;

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

        public async Task<PageResult<ClassResponse>> GetByClientIdAsync(ClassesClientPageRequest request)
        {
            var clientEntity = await _appDbContext.Users.FirstOrDefaultAsync(x => x.Id.Equals(request.ClientId) && x.Admin == null)

                            ?? throw new KeyNotFoundException(string.Format(ErrorMessages.NOT_FOUND, "Client"));

            var roles = await _userManager.GetRolesAsync(clientEntity);

            var query = _appDbContext.Classes.AsQueryable();

            foreach (var role in roles)
            {
                if (role.Equals(RoleConstants.TEACHER_NAME))
                {
                    query = query.Where(x => x.UserId == request.ClientId);

                    break;
                }
                else
                {
                    query = query.Where(x => x.StudentClasses.Any(client => client.UserId == request.ClientId));

                    break;
                }
            }

            if (!string.IsNullOrEmpty(request.Keyword))

                query = query.Where(x => x.Id.Contains(request.Keyword) || x.Name.Contains(request.Keyword, StringComparison.InvariantCultureIgnoreCase));

            var classEntities = await query.ToListAsync();

            classEntities = DoSort(classEntities, request.SortOrder);

            var total = classEntities.Count;

            var result = new List<ClassResponse>();

            foreach (var entity in classEntities)
            {
                var item = _mapper.Map<ClassResponse>(entity);

                item.TeacherItem = new TeacherItem
                {
                    Id = entity.UserId,

                    Fullname = $"{StringConverter.Convert(entity.User.Client.Lastname)} {StringConverter.Convert(entity.User.Client.Firstname)}"
                };

                item.HomeworksId = entity.Homeworks?.Select(x => x.Id).ToList();

                result.Add(item);
            }

            var pageList = result.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).Select(item => item);

            var pageResult = new PageResult<ClassResponse>
            {
                TotalRecords = total,

                PageSize = request.PageSize,

                PageIndex = request.PageIndex,

                Items = _mapper.Map<IEnumerable<ClassResponse>, List<ClassResponse>>(pageList)
            };

            return pageResult;
        }

        public List<Class> DoSort(List<Class> entities, SortOrder sortOrder)
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

        private async Task<bool> CheckRoleAsync(User user, string roleName)
        {
            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                if (role.Equals(roleName.ToUpper())) return true;
            }

            return false;
        }
    }
}
