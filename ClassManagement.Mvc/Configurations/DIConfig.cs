using ClassManagement.Mvc.Integrations.Authenticate;
using ClassManagement.Mvc.Integrations.Class;
using ClassManagement.Mvc.Integrations.Department;
using ClassManagement.Mvc.Integrations.Homework;
using ClassManagement.Mvc.Integrations.Notify;
using ClassManagement.Mvc.Integrations.Role;
using ClassManagement.Mvc.Integrations.Semester;
using ClassManagement.Mvc.Integrations.Subject;
using ClassManagement.Mvc.Integrations.Users.Client;
using ClassManagement.Mvc.Integrations.Users.Manager;
using ClassManagement.Mvc.Integrations.Users.Teacher;

namespace ClassManagement.Mvc.Configurations
{
    internal static class DIConfig
    {
        public static void ConfigureDI(IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IAuthHttpClientService, AuthHttpClientService>();

            services.AddScoped<ITeacherHttpClientService, TeacherHttpClientService>();

            services.AddScoped<IAdminHttpClientService, AdminHttpClientService>();

            services.AddScoped<IClientHttpService, ClientHttpService>();

            services.AddScoped<IDepartmentHttpClientService, DepartmentHttpClientService>();

            services.AddScoped<ISubjectHttpClientService, SubjectHttpClientService>();

            services.AddScoped<ISemestercHttpClientService, SemesterHttpClientService>();

            services.AddScoped<IClassHttpClientService, ClassHttpClientService>();

            services.AddScoped<IRoleHttpClientService, RoleHttpClientService>();

            services.AddScoped<IHomeworkHttpClientService, HomeworkHttpClientService>();

            services.AddScoped<INotifyHttpClientService, NotifyHttpClientService>();
        }
    }
}
