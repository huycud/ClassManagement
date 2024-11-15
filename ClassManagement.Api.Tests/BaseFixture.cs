using ClassManagement.Api.Data.EF;
using ClassManagement.Api.Tests.TestData;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Utilities.Common;
using static Utilities.Enums.EnumTypes;

namespace ClassManagement.Api.Tests
{
    public class BaseFixture : WebApplicationFactory<Program>
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly string _dbName = "TestDb";
        private readonly int _pageIndex = 1;
        private readonly int _pageSize = 5;
        private readonly SortOrder _sortOrder = SortOrder.AscendingId;
        private readonly bool _isDeleted = false;
        private readonly bool _isDisabled = false;

        private readonly string _subjectApi = "api/subjects";
        private readonly string _roleApi = "api/roles";
        private readonly string _departmentApi = "api/departments";
        private readonly string _semesterApi = "api/semesters";
        private readonly string _notifyApi = "api/notifies";
        private readonly string _authenticationApi = "api/authentications";
        private readonly string _clientApi = "api/clients";
        private readonly string _classApi = "api/classes";

        private readonly string _subjectId = "HDH";
        private readonly string _subjectKeyword = "cấu trúc";

        private readonly int _roleId = 2;
        private readonly int _adminRoleId = 3;
        private readonly string _roleKeyword = "teacher";

        private readonly string _departmentId = "KTMT";
        private readonly string _departmentKeyword = "KTMT";

        private readonly string _semesterId = "HK120232024";
        private readonly string _semesterKeyword = "HK220232024";

        private readonly string _notifyId = "test-tao-thong-bao-lan-1";
        private readonly string _notifyKeyword = "thông báo";
        private readonly int _userId = 100001;
        private readonly string _type = NotifyType.System.ToString();

        private readonly int _clientId = 100001;
        private readonly int _clientIdForUpdatePassword = 100012;
        private readonly int _adminId = 100022;
        private readonly int _studentId = 100009;
        private readonly int _teacherId = 100004;
        private readonly int _clientIdWithImagePathNotFound = 100005;
        // private readonly string _username = "user8";
        private readonly string _roleName = RoleConstants.STUDENT_NAME;
        private readonly string _classId = "HDH001";
        private readonly string _getClientsByRoleKeyword = "user10";
        private readonly string _getClientsByClassKeyword = "user5";
        private readonly string _updateImageUrl = "update-avatar";
        private readonly string _updatePasswordUrl = "update-password";

        private readonly int _teacherIdInClass = 100002;
        private readonly string _practiceClassId = "HDH001.1";
        private readonly string _fullClassId = "SS003";
        private readonly string _invalidAmountClassId = "SS001";
        private readonly string _getByClientUrl = "get-by-client";
        private readonly string _getStudentsNotInClassUrl = "get-students-not-exist-in-class";
        private readonly string _classKeyword = "HDH001.1";
        private readonly string _addStudentsToClassUrl = "add-student-to-class";

        public IConfiguration Configuration { get; }

        public BaseFixture()
        {
            Configuration = new ConfigurationBuilder()
                            .SetBasePath(Path.GetFullPath(@"..\..\..\..\ClassManagement.Api"))
                            .AddJsonFile("appsettings.Staging.json", optional: false)
                            .Build();

            _httpClient = new WebApplicationFactory<Program>()
               .WithWebHostBuilder(builder =>
               {
                   builder.ConfigureServices(services =>
                   {
                       var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));
                       if (descriptor != null)
                       {
                           services.Remove(descriptor);
                       }
                       services.AddDbContext<AppDbContext>(options =>
                       {
                           options.UseInMemoryDatabase(_dbName)
                                    .UseLazyLoadingProxies()
                                    .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning));
                       });
                       var sp = services.BuildServiceProvider();
                       using var scope = sp.CreateScope();
                       var scopedServices = scope.ServiceProvider;
                       var db = scopedServices.GetRequiredService<AppDbContext>();
                       var logger = scopedServices.GetRequiredService<ILogger<WebApplicationFactory<Program>>>();
                       db.Database.EnsureCreated();
                       try
                       {
                           TestCommonData.Init(db);
                       }
                       catch (Exception ex)
                       {
                           logger.LogError(ex, "An error occurred seeding the database with test messages. "
                                            + "Error: {Message}", ex.Message);
                       }
                   });
                   builder.UseEnvironment("Staging");
               }).CreateClient();

            _baseUrl = Configuration.GetSection("BaseAddress").Value;
        }

        public HttpClient GetHttpClient() => _httpClient;

        public string BaseUrl => _baseUrl;

        public int PageIndex => _pageIndex;

        public int PageSize => _pageSize;

        public SortOrder SortOrder => _sortOrder;

        public bool IsDeleted => _isDeleted;

        public bool IsDisabled => _isDisabled;

        //
        public string SubjectApi => _subjectApi;

        public string SubjectId => _subjectId;

        public string SubjectIdNotFound => _subjectId + "not found";

        public string SubjectKeyword => _subjectKeyword;

        public string SubjectKeywordNotFound => _subjectKeyword + "not found";

        //
        public string RoleApi => _roleApi;

        public int RoleId => _roleId - 1;

        public int RoleIdNotFound => _roleId + 5;

        public int AdminRoleId => _adminRoleId;

        public int RoleIdToDelete => _roleId + 2;

        public int RoleIdToDeleteFail => _roleId + 1;

        public string RoleKeyword => _roleKeyword;

        public string RoleKeywordNotFound => _roleKeyword + "not found";

        //
        public string DepartmentApi => _departmentApi;

        public string DepartmentId => _departmentId;

        public string DepartmentIdNotFound => _departmentId + "not found";

        public string DepartmentKeyword => _departmentKeyword;

        public string DepartmentKeywordNotFound => _departmentKeyword + "not found";

        //
        public string SemesterApi => _semesterApi;

        public string SemesterId => _semesterId;

        public string SemesterIdNotFound => _semesterId + "not found";

        public string SemesterKeyword => _semesterKeyword;

        public string SemesterKeywordNotFound => _semesterKeyword + "not found";

        //
        public string NotifyApi => _notifyApi;

        public string NotifyId => _notifyId;

        public string NotifyIdNotFound => _notifyId + "notfound";

        public int UserId => _userId;

        public string NotificationType => _type;

        public int UserIdNotFound => _userId + 10;

        public string NotifyKeyword => _notifyKeyword;

        public string NotifyKeywordNotFound => _notifyKeyword + "not found";

        //
        public string AuthenticationApi => _authenticationApi;

        //
        public string ClientApi => _clientApi;

        public string Rolename => _roleName;

        public string RolenameNotFound => _roleName + "notfound";

        public string ClassId => _classId;

        public string ClassIdNotFound => _classId + "notfound";

        public int ClientId => _clientId;

        public int AdminId => _adminId;

        public int StudentId => _studentId;

        public int TeacherId => _teacherId;

        public int ClientIdWithImagePathNotFound => _clientIdWithImagePathNotFound;

        public int ClientIdNotFound => _clientId + 100;

        //public string Username => _username;

        //public string UsernameNotFound => _username + "notfound";

        public string GetClientsByRoleWithKeyword => _getClientsByRoleKeyword;

        public string GetClientsByRoleWithKeywordNotFound => _getClientsByRoleKeyword + "not found";

        public string GetClientsByClassWithKeyword => _getClientsByClassKeyword;

        public string GetClientsByClassWithKeywordNotFound => _getClientsByClassKeyword + "not found";

        public string UpdateImageUrl => _updateImageUrl;

        public string UpdatePasswordUrl => _updatePasswordUrl;

        //
        public string ClassApi => _classApi;

        public string ClassKeyword => _classKeyword;

        public string ClassKeywordNotFound => _classKeyword + "notfound";

        public int TeacherIdNotFound => _teacherId + 100;

        public int StudentIdNotFound => _studentId + 100;

        public string PracticeClassId => _practiceClassId;

        public string FullClassId => _fullClassId;

        public string InvalidAmoutClassId => _invalidAmountClassId;

        public string GetAddStudentsToClassUrl => _addStudentsToClassUrl;

        public int TeacherIdInClass => _teacherIdInClass;

        public int ClientIdForUpdatePassword => _clientIdForUpdatePassword;
    }
}