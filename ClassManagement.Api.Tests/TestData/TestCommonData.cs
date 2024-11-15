using System.Collections;
using System.Net.Http.Headers;
using System.Text;
using ClassManagement.Api.Data.EF;
using ClassManagement.Api.Data.Entities;
using ClassManagement.Api.DTO.Users.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Utilities.Common;
using Utilities.Handlers;
using static Utilities.Enums.EnumTypes;

namespace ClassManagement.Api.Tests.TestData
{
    internal static class TestCommonData
    {
        public const string ADMIN_ROLE = RoleConstants.ADMIN_NAME;
        public const string TEACHER_ROLE = RoleConstants.TEACHER_NAME;
        public const string STUDENT_ROLE = RoleConstants.STUDENT_NAME;
        public static List<Subject> SubjectSeeding()
        {
            return new List<Subject>()
            {
                new() {
                    Id = "CTDL",
                    Name = "Cấu trúc dữ liệu và giải thuật",
                    Credit = 4,
                    Status = Status.Opening.ToString(),
                    DepartmentId ="CNPM",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    IsPracticed = true
                },
                new() {
                    Id = "CTRR",
                    Name = "Cấu trúc rời rạc",
                    Credit = 3,
                    Status = Status.Opening.ToString(),
                    DepartmentId ="TOANTIN",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    IsPracticed = false
                },
                new() {
                    Id = "CNXH",
                    Name = "Chủ nghĩa xã hội",
                    Credit = 2,
                    Status = Status.Opening.ToString(),
                    DepartmentId ="CHINHTRI",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    IsPracticed = false
                },
                new() {
                    Id = "HDH",
                    Name = "Operating System",
                    Credit = 4,
                    Status = Status.Opening.ToString(),
                    DepartmentId ="KTMT",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    IsPracticed = true
                },
                new() {
                    Id = "DSTT",
                    Name = "Đại số tuyến tính",
                    Credit = 3,
                    Status = Status.Closed.ToString(),
                    DepartmentId ="TOANTIN",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    IsPracticed = false
                }
            };
        }

        public static List<Department> DepartmentSeeding()
        {
            return new List<Department>()
            {
                new() {
                    Id = "CNPM",
                    Name = "CÔNG NGHỆ PHẦN MỀM",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new() {
                    Id = "KTMT",
                    Name = "KĨ THUẬT MÁY TÍNH",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new() {
                    Id = "HTTT",
                    Name = "HỆ THỐNG THÔNG TIN",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new() {
                    Id = "TOANTIN",
                    Name = "TOÁN TIN",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new() {
                    Id = "CHINHTRI",
                    Name = "CHÍNH TRỊ",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };
        }

        public static List<Class> ClassSeeding()
        {
            return new List<Class>()
            {
                new()
                {
                    Id = "HDH001",
                    Name = "Lý Thuyết Hệ điều hành",
                    UserId = 100004,
                    SubjectId = "HDH",
                    ClassSize = 100,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    StartedAt = DateTime.UtcNow,
                    EndedAt = DateTime.UtcNow.AddMonths(3),
                    Amount = 11,
                    SemesterId = "HK120232024",
                    Type = "Theory",
                    Credit = 3,
                    DayOfWeek = DayOfWeek.Wednesday.ToString(),
                    ClassPeriods = $"{ClassPeriod.First},{ClassPeriod.Second},{ClassPeriod.Third}"
                },
                new()
                {
                    Id = "HDH001.1",
                    Name = "Thực hành Hệ điều hành",
                    UserId = 100004,
                    SubjectId = "HDH",
                    ClassSize = 50,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    StartedAt = DateTime.UtcNow,
                    EndedAt = DateTime.UtcNow.AddMonths(3),
                    Amount = 20,
                    SemesterId = "HK120232024",
                    Type = "Practice",
                    Credit = 1,
                    DayOfWeek = DayOfWeek.Thursday.ToString(),
                    ClassPeriods = $"{ClassPeriod.Sixth},{ClassPeriod.Seventh},{ClassPeriod.Eighth},{ClassPeriod.Ninth},{ClassPeriod.Tenth}"
                },
                new()
                {
                    Id = "CTDL001",
                    Name = "Cấu trúc dữ liệu và giải thuật",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    UserId = 100001,
                    SubjectId = "CTDL",
                    ClassSize = 100,
                    Amount = 50,
                    StartedAt = DateTime.UtcNow,
                    EndedAt = DateTime.UtcNow.AddMonths(3),
                    Credit = 3,
                    Type = "Theory",
                    SemesterId ="HK220232024",
                    DayOfWeek = DayOfWeek.Tuesday.ToString(),
                    ClassPeriods = $"{ClassPeriod.First},{ClassPeriod.Second},{ClassPeriod.Third}"
                },
                new()
                {
                    Id = "CTDL001.1",
                    Name = "Thực hành cấu trúc dữ liệu và giải thuật",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    UserId = 100002,
                    SubjectId = "CTDL",
                    ClassSize = 50,
                    Amount = 40,
                    StartedAt = DateTime.UtcNow,
                    EndedAt = DateTime.UtcNow.AddMonths(3),
                    Credit = 1,
                    Type = "Practice",
                    SemesterId = "HK220232024",
                    DayOfWeek= DayOfWeek.Tuesday.ToString(),
                    ClassPeriods = $"{ClassPeriod.Sixth},{ClassPeriod.Seventh},{ClassPeriod.Eighth},{ClassPeriod.Ninth},{ClassPeriod.Tenth}"
                },
                new()
                {
                    Id = "SS001",
                    Name = "Chủ nghĩa Mac-Lenin",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    UserId = 100016,
                    SubjectId = "CHINHTRI",
                    ClassSize = 50,
                    Amount = 48,
                    StartedAt = DateTime.UtcNow,
                    EndedAt = DateTime.UtcNow.AddMonths(3),
                    Credit = 5,
                    Type = "Theory",
                    SemesterId = "HK120242025",
                    DayOfWeek= DayOfWeek.Tuesday.ToString(),
                    ClassPeriods = $"{ClassPeriod.Sixth},{ClassPeriod.Seventh},{ClassPeriod.Eighth},{ClassPeriod.Ninth},{ClassPeriod.Tenth}"
                },
                new()
                {
                    Id = "SS003",
                    Name = "Tư tưởng Hồ Chí Minh",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    UserId = 100016,
                    SubjectId = "CHINHTRI",
                    ClassSize = 50,
                    Amount = 50,
                    StartedAt = DateTime.UtcNow,
                    EndedAt = DateTime.UtcNow.AddMonths(3),
                    Credit = 2,
                    Type = "Theory",
                    SemesterId = "HK120242025",
                    DayOfWeek= DayOfWeek.Monday.ToString(),
                    ClassPeriods = $"{ClassPeriod.First},{ClassPeriod.Second}"
                },
            };
        }

        public static List<User> ClientSeeding()
        {
            var list = new List<User>();

            for (int i = 1; i < 15; i++)
            {
                list.Add(new User
                {
                    Id = 100000 + i,
                    UserName = $"user{i}",
                    Email = $"user{i}@gmail.com",
                    NormalizedUserName = $"user{i}".ToUpper(),
                    NormalizedEmail = $"user{i}@gmail.com".ToUpper(),
                    PasswordHash = Password.HashPassword("123456"),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    LockoutEnabled = true,
                    LockoutEnd = null,
                    EmailConfirmed = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    IsDisabled = false,
                    Client = new Client
                    {
                        UserId = 100000 + i,
                        DepartmentId = i % 2 == 0 ? "KTMT" : "CNPM",
                        Gender = Gender.Male.ToString(),
                        Firstname = $"user{i}",
                        Lastname = "nguyen van",
                        Address = "VN",
                        DateOfBirth = DateTime.UtcNow.AddYears(-20)
                    }
                });
            }

            list.Add(new User
            {
                Id = 100017,
                UserName = "user17",
                Email = $"user17@gmail.com",
                NormalizedUserName = "user17".ToUpper(),
                NormalizedEmail = $"user17@gmail.com".ToUpper(),
                PasswordHash = Password.HashPassword("123456"),
                SecurityStamp = Guid.NewGuid().ToString(),
                LockoutEnabled = true,
                LockoutEnd = null,
                EmailConfirmed = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDisabled = true,
                Client = new Client
                {
                    UserId = 100017,
                    DepartmentId = "TOANTIN",
                    Gender = Gender.Male.ToString(),
                    Firstname = $"user17",
                    Lastname = "nguyen van",
                    Address = "VN",
                    DateOfBirth = DateTime.UtcNow.AddYears(-20)
                }
            });

            list.Add(new User
            {
                Id = 100015,
                UserName = "user15",
                Email = $"user15@gmail.com",
                NormalizedUserName = "user15".ToUpper(),
                NormalizedEmail = $"user15@gmail.com".ToUpper(),
                PasswordHash = Password.HashPassword("123456"),
                SecurityStamp = Guid.NewGuid().ToString(),
                LockoutEnabled = true,
                LockoutEnd = null,
                EmailConfirmed = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDisabled = false,
                Client = new Client
                {
                    UserId = 100015,
                    DepartmentId = "TOANTIN",
                    Gender = Gender.Male.ToString(),
                    Firstname = $"user15",
                    Lastname = "nguyen van",
                    Address = "VN",
                    DateOfBirth = DateTime.UtcNow.AddYears(-20)
                }
            });

            list.Add(new User
            {
                Id = 100016,
                UserName = "user16",
                Email = $"user16@gmail.com",
                NormalizedUserName = "user16".ToUpper(),
                NormalizedEmail = $"user16@gmail.com".ToUpper(),
                PasswordHash = Password.HashPassword("123456"),
                SecurityStamp = Guid.NewGuid().ToString(),
                LockoutEnabled = true,
                LockoutEnd = null,
                EmailConfirmed = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDisabled = false,
                Client = new Client
                {
                    UserId = 100016,
                    DepartmentId = "CHINHTRI",
                    Gender = Gender.Male.ToString(),
                    Firstname = $"user16",
                    Lastname = "nguyen van",
                    Address = "VN",
                    DateOfBirth = DateTime.UtcNow.AddYears(-20)
                }
            });

            list.Add(new User
            {
                Id = 100022,
                UserName = "admin22",
                Email = "admin22@gmail.com",
                NormalizedUserName = "admin22".ToUpper(),
                NormalizedEmail = "admin22@gmail.com".ToUpper(),
                PasswordHash = Password.HashPassword("123456"),
                SecurityStamp = Guid.NewGuid().ToString(),
                LockoutEnabled = true,
                LockoutEnd = null,
                EmailConfirmed = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDisabled = false,
                Admin = new Admin
                {
                    UserId = 100022,
                    Fullname = "admin 22"
                }
            });

            return list;
        }

        public static List<Image> ImageSeeding()
        {
            return new List<Image>
            {
                new()
                {
                    Id = 1,
                    UserId = 100005,
                    ImageName = "watermelon.jpg",
                    Caption = "Student3 Image",
                    FileSize = 21111,
                    CreatedAt = DateTime.UtcNow,
                    ImagePath = $"/UploadFileTesting/Images/Student/Student5-{Guid.NewGuid()}.jpg"
                }
            };
        }

        public static List<Notify> NotifySeeding()
        {
            return new List<Notify>
            {
                new()
                {
                    Id = "test-tao-thong-bao-lan-1",
                    Title="test tạo thông báo lần 1",
                    Content = "đây là thông báo test",
                    IsDeleted =false,
                    Type = "ClassRoom",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    UserId = 100001
                },
                new()
                {
                    Id = "test-tao-thong-bao-lan-2",
                    Title="test tạo thông báo lần 2",
                    Content = "đây là thông báo test",
                    IsDeleted =false,
                    Type = "System",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    UserId = 100002
                },
                new()
                {
                    Id = "test-tao-thong-bao-lan-3",
                    Title="test tạo thông báo lần 3",
                    Content = "đây là thông báo test",
                    IsDeleted =true,
                    Type = "Class",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    UserId = 100003
                }
            };
        }

        public static List<Role> RoleSeeding()
        {
            return new List<Role>
            {
                new() {
                    Id = 1,
                    Description = "This is teacher role.",
                    Name = RoleConstants.TEACHER_NAME,
                    NormalizedName = RoleConstants.TEACHER_NAME,
                },
                new()
                {
                    Id = 2,
                    Description = "This is student role.",
                    Name = RoleConstants.STUDENT_NAME,
                    NormalizedName = RoleConstants.STUDENT_NAME
                },
                new()
                {
                    Id = 3,
                    Description = "This is admin role.",
                    Name = RoleConstants.ADMIN_NAME,
                    NormalizedName = RoleConstants.ADMIN_NAME
                },
                new()
                {
                    Id = 4,
                    Description = "This is super member role.",
                    Name = "SUPERMEMBER",
                    NormalizedName = "SUPERMEMBER"
                },
                new()
                {
                    Id = 5,
                    Description = "This is member role.",
                    Name = "MEMBER",
                    NormalizedName = "MEMBER"
                }
            };
        }

        public static List<IdentityUserRole<int>> UserRolesSeeding()
        {
            return new List<IdentityUserRole<int>>
            {
                new()
                {
                    UserId = 100001,
                    RoleId = 1
                },
                new()
                {
                    UserId = 100002,
                    RoleId = 1
                },
                new()
                {
                    UserId = 100003,
                    RoleId = 3
                },
                new()
                {
                    UserId = 100004,
                    RoleId = 1
                },
                new()
                {
                    UserId = 100015,
                    RoleId = 1
                },
                new()
                {
                    UserId = 100016,
                    RoleId = 1
                },
                new()
                {
                    UserId = 100005,
                    RoleId = 2
                },
                new()
                {
                    UserId = 100006,
                    RoleId = 2
                },
                new()
                {
                    UserId = 100007,
                    RoleId = 2
                },
                new()
                {
                    UserId = 100008,
                    RoleId = 2
                },
                new()
                {
                    UserId = 100009,
                    RoleId = 2
                },
                new()
                {
                    UserId = 100010,
                    RoleId = 2
                },
                new()
                {
                    UserId = 100011,
                    RoleId = 2
                },
                new()
                {
                    UserId = 100012,
                    RoleId = 2
                },
                new()
                {
                    UserId = 100013,
                    RoleId = 2
                },
                new()
                {
                    UserId = 100014,
                    RoleId = 2
                },
                new()
                {
                    UserId = 100017,
                    RoleId = 2
                },
                new()
                {
                    UserId = 100022,
                    RoleId = 3
                },
            };
        }

        public static List<Semester> SemesterSeeding()
        {
            return new List<Semester>
            {
                new()
                {
                    Id = "HK120232024",
                    Name = "Học kỳ 1 2023-2024",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                },
                new()
                {
                    Id = "HK220232024",
                    Name = "Học kỳ 2 2023-2024",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                },
                new()
                {
                    Id = "HK320232024",
                    Name = "Học kỳ 3 2023-2024",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                },
                new()
                {
                    Id = "HK120242025",
                    Name = "Học kỳ 1 2024-2025",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                }
            };
        }

        public static List<StudentClass> StudentClassesSeeding()
        {
            return new List<StudentClass>
            {
                new()
                {
                    UserId = 100005,
                    ClassId = "CTDL001"
                },
                new()
                {
                    UserId = 100006,
                    ClassId = "CTDL001"
                },
                new()
                {
                    UserId = 100007,
                    ClassId = "CTDL001"
                },
                new()
                {
                    UserId = 100008,
                    ClassId = "CTDL001"
                },
                new()
                {
                    UserId = 100006,
                    ClassId = "CTDL001.1"
                },
                new()
                {
                    UserId = 100007,
                    ClassId = "CTDL001.1"
                },
                new()
                {
                    UserId = 100008,
                    ClassId = "CTDL001.1"
                },
                new()
                {
                    UserId = 100009,
                    ClassId = "CTDL001"
                },
                new()
                {
                    UserId = 100005,
                    ClassId = "HDH001"
                },
                new()
                {
                    UserId = 100006,
                    ClassId = "HDH001"
                },
                new()
                {
                    UserId = 100007,
                    ClassId = "HDH001"
                },
                new()
                {
                    UserId = 100005,
                    ClassId = "HDH001.1"
                },
                new()
                {
                    UserId = 100006,
                    ClassId = "HDH001.1"
                },
                new()
                {
                    UserId = 100007,
                    ClassId = "HDH001.1"
                },
                new()
                {
                    UserId = 100009,
                    ClassId = "HDH001.1"
                }
            };
        }

        public static List<string> HighestRolesRequest()
        {
            return new List<string>
            {
                RoleConstants.ADMIN_NAME,

                RoleConstants.TEACHER_NAME
            };
        }

        public static List<string> AdminRolesRequest()
        {
            return new List<string>
            {
                RoleConstants.ADMIN_NAME
            };
        }

        public static List<string> TeacherRolesRequest()
        {
            return new List<string>
            {
               RoleConstants.TEACHER_NAME
            };
        }

        public static List<string> StudentRolesRequest()
        {
            return new List<string>
            {
               RoleConstants.STUDENT_NAME
            };
        }

        public static CreateAdminRequest CreateUserRequest()
        {
            return new CreateAdminRequest()
            {
                Fullname = "user",
                UserName = "user",
                Email = "user@gmail.com",
                Password = "123456",
                ConfirmPassword = "123456"
            };
        }

        public static IsoDateTimeConverter ConvertDateTime()
        {
            var format = "HH:mm:ss dd/MM/yyyy";

            var dateTimeConverter = new IsoDateTimeConverter { DateTimeFormat = format };

            return dateTimeConverter;
        }

        public static HttpContent GetRequestContent(this object obj)
        {
            var json = JsonConvert.SerializeObject(obj);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            return content;
        }

        public static HttpContent GetRequestMultipartFormContent(this object obj)
        {
            var multipartContent = new MultipartFormDataContent();

            var properties = obj.GetType().GetProperties();

            foreach (var prop in properties)
            {
                var value = prop.GetValue(obj);

                if (value != null)
                {
                    if (prop.PropertyType == typeof(IFormFile))
                    {
                        var file = (IFormFile)value;
                        byte[] data;

                        using (var br = new BinaryReader(file.OpenReadStream()))
                        {
                            data = br.ReadBytes((int)file.OpenReadStream().Length);
                        }

                        ByteArrayContent bytes = new ByteArrayContent(data);

                        bytes.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);

                        multipartContent.Add(bytes, "Image", file.FileName);
                    }

                    else if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                    {
                        IList list = (IList)prop.GetValue(obj);

                        foreach (var item in list) multipartContent.Add(new StringContent(item.ToString()), prop.Name);
                    }

                    else multipartContent.Add(new StringContent(value.ToString()), prop.Name);
                }
            }
            return multipartContent;
        }

        public static void Init(AppDbContext db)
        {
            db.Subjects.AddRange(SubjectSeeding());
            db.Departments.AddRange(DepartmentSeeding());
            db.Classes.AddRange(ClassSeeding());
            db.Roles.AddRange(RoleSeeding());
            db.Users.AddRange(ClientSeeding());
            db.Semesters.AddRange(SemesterSeeding());
            db.UserRoles.AddRange(UserRolesSeeding());
            db.StudentClasses.AddRange(StudentClassesSeeding());
            db.Notifies.AddRange(NotifySeeding());
            db.Images.AddRange(ImageSeeding());
            db.SaveChanges();
        }

        public static void Reinit(AppDbContext db)
        {
            db.Subjects.RemoveRange(db.Subjects);
            db.Departments.RemoveRange(db.Departments);
            db.Classes.RemoveRange(db.Classes);
            db.Roles.RemoveRange(db.Roles);
            db.Users.RemoveRange(db.Users);
            db.UserRoles.RemoveRange(db.UserRoles);
            db.StudentClasses.RemoveRange(db.StudentClasses);
            db.Semesters.RemoveRange(db.Semesters);
            db.Notifies.RemoveRange(db.Notifies);
            db.Images.RemoveRange(db.Images);
            Init(db);
        }
    }
}
