using ClassManagement.Api.Mapper.AppRole;
using ClassManagement.Api.Mapper.Classes;
using ClassManagement.Api.Mapper.Departments;
using ClassManagement.Api.Mapper.Homeworks;
using ClassManagement.Api.Mapper.Semesters;
using ClassManagement.Api.Mapper.Subjects;
using ClassManagement.Api.Mapper.Users.Clients;
using ClassManagement.Api.Mapper.Users.Manager;

namespace ClassManagement.Api.Configurations
{
    static class AutoMapperConfig
    {
        public static void ConfigureAutoMapper(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AdminMapping));

            services.AddAutoMapper(typeof(ClientMapping));

            services.AddAutoMapper(typeof(DepartmentMapping));

            services.AddAutoMapper(typeof(RoleMapping));

            services.AddAutoMapper(typeof(ClassMapping));

            services.AddAutoMapper(typeof(SemesterMapping));

            services.AddAutoMapper(typeof(HomeworkMapping));

            services.AddAutoMapper(typeof(SubjectMapping));
        }
    }
}
