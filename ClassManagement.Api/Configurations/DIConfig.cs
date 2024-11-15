using ClassManagement.Api.Common.EmailReader;
using ClassManagement.Api.Common.Exceptions;
using ClassManagement.Api.Data.Entities;
using ClassManagement.Api.Services.AppRole;
using ClassManagement.Api.Services.Authentication;
using ClassManagement.Api.Services.Classes;
using ClassManagement.Api.Services.Departments;
using ClassManagement.Api.Services.Email;
using ClassManagement.Api.Services.Homeworks;
using ClassManagement.Api.Services.Notifies;
using ClassManagement.Api.Services.Semesters;
using ClassManagement.Api.Services.Storage;
using ClassManagement.Api.Services.Subjects;
using ClassManagement.Api.Services.Users.Clients;
using ClassManagement.Api.Services.Users.Manager;
using ClassManagement.Api.Services.Users.Students;
using ClassManagement.Api.Services.Users.Teachers;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Utilities.Common;
using static Utilities.Common.SystemConstants;


namespace ClassManagement.Api.Configurations
{
    static class DIConfig
    {
        public static void ConfigureDI(IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IAdminService, AdminService>();

            services.AddScoped<IClientService, ClientService>();

            services.AddScoped<ITeacherService, TeacherService>();

            services.AddScoped<IStudentService, StudentService>();

            services.AddScoped<IRoleService, RoleService>();

            services.AddScoped<IDepartmentService, DepartmentService>();

            services.AddScoped<IClassService, ClassService>();

            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddScoped<ISubjectService, SubjectService>();

            services.AddScoped<ISemesterService, SemesterService>();

            services.AddScoped<IHomeworkService, HomeworkService>();

            services.AddScoped<INotifyService, NotifyService>();

            services.AddScoped<IEmailService, EmailService>();

            services.AddScoped<IEmailTemplateReader, EmailTemplateReader>();

            services.AddScoped<UserManager<User>>();

            services.AddScoped<SignInManager<User>>();

            services.AddScoped<RoleManager<Role>>();

            services.AddScoped<IStorageService>(provider =>
            {
                var webHostEnvironment = provider.GetRequiredService<IWebHostEnvironment>();

                var configEnv = provider.GetRequiredService<IConfiguration>();

                var currentUser = provider.GetRequiredService<IHttpContextAccessor>().HttpContext.User;

                if (currentUser.IsInRole(RoleConstants.ADMIN_NAME))
                {
                    return new StorageService(webHostEnvironment, ADMIN_FOLDER, configEnv);
                }
                else if (currentUser.IsInRole(RoleConstants.TEACHER_NAME))
                {
                    return new StorageService(webHostEnvironment, TEACHER_FOLDER, configEnv);
                }
                return new StorageService(webHostEnvironment, STUDENT_FOLDER, configEnv);
            });

            services.AddSingleton<IValidatorInterceptor, LoggingValidationInterceptor>();
        }
    }
}
