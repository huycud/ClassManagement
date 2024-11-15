using ClassManagement.Api.Data.Configurations;
using ClassManagement.Api.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ClassManagement.Api.Data.EF
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<User, Role, int>(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AdminConfiguration());

            builder.ApplyConfiguration(new ClassConfiguration());

            builder.ApplyConfiguration(new ClientConfiguration());

            builder.ApplyConfiguration(new DepartmentConfiguration());

            builder.ApplyConfiguration(new EvalutionConfiguration());

            builder.ApplyConfiguration(new ExerciseConfiguration());

            builder.ApplyConfiguration(new HomeworkConfiguration());

            builder.ApplyConfiguration(new ImageConfiguration());

            builder.ApplyConfiguration(new NotifyConfiguration());

            builder.ApplyConfiguration(new RoleConfiguration());

            builder.ApplyConfiguration(new ScoreConfiguration());

            builder.ApplyConfiguration(new StudentClassConfiguration());

            builder.ApplyConfiguration(new SemesterConfiguration());

            builder.ApplyConfiguration(new SubjectConfiguration());

            builder.ApplyConfiguration(new UserConfiguration());

            builder.ApplyConfiguration(new PasswordResetTokenConfiguration());

            base.OnModelCreating(builder);
        }

        public DbSet<Admin> Admins { get; set; }

        public DbSet<Class> Classes { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Evalution> Evalutions { get; set; }

        public DbSet<Exercise> Exercises { get; set; }

        public DbSet<Homework> Homeworks { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<Notify> Notifies { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Score> Scores { get; set; }

        public DbSet<StudentClass> StudentClasses { get; set; }

        public DbSet<Semester> Semesters { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<PasswordResetToken> PasswordResetTokens { get; set; }
    }
}
