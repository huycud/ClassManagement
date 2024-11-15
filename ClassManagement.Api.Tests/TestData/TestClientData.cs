using ClassManagement.Api.DTO.Users;
using ClassManagement.Api.DTO.Users.Clients;
using ClassManagement.Api.DTO.Users.Manager;
using Microsoft.AspNetCore.Http;
using Utilities.Common;
using static Utilities.Enums.EnumTypes;

namespace ClassManagement.Api.Tests.TestData
{
    internal static class TestClientData
    {
        public static string[] filesName = ["potato.jpg", "onion.jpg", "orange.jpg", "tomato.jpg", "watermelon.jpg", "test.txt", "exceedsize.jpg"];
        //public const string INVALIDTYPEFILENAME = "test.txt";
        //public const string INVALIDSIZEFILENAME = "exceedsize.jpg";

        public static CreateAdminRequest CreateUserRequest()
        {
            return new CreateAdminRequest
            {
                Fullname = "user",
                UserName = "user",
                Email = "user@gmail.com",
                Password = "123456",
                ConfirmPassword = "123456"
            };
        }

        public static CreateClientRequest CreateClientRequest()
        {
            return new CreateClientRequest
            {
                Firstname = "client",
                Lastname = "nguyen thi",
                Password = "123456",
                ConfirmPassword = "123456",
                Address = "VN",
                DateOfBirth = DateTime.UtcNow.AddYears(-20),
                Email = "client1@gmail.com",
                UserName = "client1",
                DepartmentId = "CNPM",
                Gender = Gender.Male,
                RoleName = RoleConstants.STUDENT_NAME
            };
        }

        public static CreateClientRequest CreateClientWithUsernameEmptyRequest()
        {
            return new CreateClientRequest
            {
                Firstname = "client",
                Lastname = "nguyen thi",
                Password = "123456",
                ConfirmPassword = "123456",
                Address = "VN",
                DateOfBirth = DateTime.UtcNow.AddYears(-20),
                Email = "client1@gmail.com",
                UserName = "",
                DepartmentId = "CNPM",
                Gender = Gender.Male,
                RoleName = RoleConstants.STUDENT_NAME
            };
        }

        public static CreateClientRequest CreateClientWithUsernameLessThanMinimumLengthRequest()
        {
            return new CreateClientRequest
            {
                Firstname = "clien1",
                Lastname = "nguyen thi",
                Password = "123456",
                ConfirmPassword = "123456",
                Address = "VN",
                DateOfBirth = DateTime.UtcNow.AddYears(-20),
                Email = "client1@gmail.com",
                UserName = "clie",
                DepartmentId = "CNPM",
                Gender = Gender.Male,
                RoleName = RoleConstants.STUDENT_NAME
            };
        }

        public static CreateClientRequest CreateClientWithUsernameGreaterThanMaximumLengthRequest()
        {
            return new CreateClientRequest
            {
                Firstname = "client",
                Lastname = "nguyen thi",
                Password = "123456",
                ConfirmPassword = "123456",
                Address = "VN",
                DateOfBirth = DateTime.UtcNow.AddYears(-20),
                Email = "client1@gmail.com",
                UserName = new string('h', 21),
                DepartmentId = "CNPM",
                Gender = Gender.Male,
                RoleName = RoleConstants.STUDENT_NAME
            };
        }

        public static CreateClientRequest CreateClientWithUsernameContainSpecialCharacterRequest()
        {
            return new CreateClientRequest
            {
                Firstname = "client",
                Lastname = "nguyen thi",
                Password = "123456",
                ConfirmPassword = "123456",
                Address = "VN",
                DateOfBirth = DateTime.UtcNow.AddYears(-20),
                Email = "client1@gmail.com",
                UserName = "client@",
                DepartmentId = "CNPM",
                Gender = Gender.Male,
                RoleName = RoleConstants.STUDENT_NAME
            };
        }

        public static CreateClientRequest CreateClientWithUsernameDuplicateRequest()
        {
            return new CreateClientRequest
            {
                Firstname = "client",
                Lastname = "nguyen thi",
                Password = "123456",
                ConfirmPassword = "123456",
                Address = "VN",
                DateOfBirth = DateTime.UtcNow.AddYears(-20),
                Email = "client1@gmail.com",
                UserName = "student1",
                DepartmentId = "CNPM",
                Gender = Gender.Male,
                RoleName = RoleConstants.STUDENT_NAME
            };
        }

        public static CreateClientRequest CreateClientWithPasswordEmptyRequest()
        {
            return new CreateClientRequest
            {
                Firstname = "client",
                Lastname = "nguyen thi",
                Password = "",
                ConfirmPassword = "123456",
                Address = "VN",
                DateOfBirth = DateTime.UtcNow.AddYears(-20),
                Email = "client1@gmail.com",
                UserName = "client",
                DepartmentId = "CNPM",
                Gender = Gender.Male,
                RoleName = RoleConstants.STUDENT_NAME
            };
        }

        public static CreateClientRequest CreateClientWithPasswordLessThanMinimumLengthRequest()
        {
            return new CreateClientRequest
            {
                Firstname = "client",
                Lastname = "nguyen thi",
                Password = "12345",
                ConfirmPassword = "12345",
                Address = "VN",
                DateOfBirth = DateTime.UtcNow.AddYears(-20),
                Email = "client1@gmail.com",
                UserName = "client",
                DepartmentId = "CNPM",
                Gender = Gender.Male,
                RoleName = RoleConstants.STUDENT_NAME
            };
        }

        public static CreateClientRequest CreateClientWithPasswordGreaterThanMaximumLengthRequest()
        {
            return new CreateClientRequest
            {
                Firstname = "client",
                Lastname = "nguyen thi",
                Password = "123451234512345123456",
                ConfirmPassword = "123451234512345123456",
                Address = "VN",
                DateOfBirth = DateTime.UtcNow.AddYears(-20),
                Email = "client1@gmail.com",
                UserName = "client",
                DepartmentId = "CNPM",
                Gender = Gender.Male,
                RoleName = RoleConstants.STUDENT_NAME
            };
        }

        public static CreateClientRequest CreateClientWithPasswordNotMatchRequest()
        {
            return new CreateClientRequest
            {
                Firstname = "client",
                Lastname = "nguyen thi",
                Password = "123456",
                ConfirmPassword = "12354",
                Address = "VN",
                DateOfBirth = DateTime.UtcNow.AddYears(-20),
                Email = "client1@gmail.com",
                UserName = "client",
                DepartmentId = "CNPM",
                Gender = Gender.Male,
                RoleName = RoleConstants.STUDENT_NAME
            };
        }

        public static CreateClientRequest CreateClientWithFirstnameEmptyRequest()
        {
            return new CreateClientRequest
            {
                Firstname = "",
                Lastname = "nguyen thi",
                Password = "123456",
                ConfirmPassword = "12345",
                Address = "VN",
                DateOfBirth = DateTime.UtcNow.AddYears(-20),
                Email = "client1@gmail.com",
                UserName = "client",
                DepartmentId = "CNPM",
                Gender = Gender.Male,
                RoleName = RoleConstants.STUDENT_NAME
            };
        }

        public static CreateClientRequest CreateClientWithFirstnameLessThanMinimumLengthRequest()
        {
            return new CreateClientRequest
            {
                Firstname = "n",
                Lastname = "nguyen thi",
                Password = "123456",
                ConfirmPassword = "12345",
                Address = "VN",
                DateOfBirth = DateTime.UtcNow.AddYears(-20),
                Email = "client1@gmail.com",
                UserName = "client",
                DepartmentId = "CNPM",
                Gender = Gender.Male,
                RoleName = RoleConstants.STUDENT_NAME
            };
        }

        public static CreateClientRequest CreateClientWithFirstnameGreaterThanMaximumLengthRequest()
        {
            return new CreateClientRequest
            {
                Firstname = new string('h', 256),
                Lastname = "nguyen thi",
                Password = "123456",
                ConfirmPassword = "12345",
                Address = "VN",
                DateOfBirth = DateTime.UtcNow.AddYears(-20),
                Email = "client1@gmail.com",
                UserName = "client",
                DepartmentId = "CNPM",
                Gender = Gender.Male,
                RoleName = RoleConstants.STUDENT_NAME
            };
        }

        public static CreateClientRequest CreateClientWithFirstnameContainSpecialCharacterRequest()
        {
            return new CreateClientRequest
            {
                Firstname = "huy123@@",
                Lastname = "nguyen thi",
                Password = "123456",
                ConfirmPassword = "12345",
                Address = "VN",
                DateOfBirth = DateTime.UtcNow.AddYears(-20),
                Email = "client1@gmail.com",
                UserName = "client",
                DepartmentId = "CNPM",
                Gender = Gender.Male,
                RoleName = RoleConstants.STUDENT_NAME
            };
        }

        //
        public static CreateClientRequest CreateClientWithLastnameEmptyRequest()
        {
            return new CreateClientRequest
            {
                Firstname = "huy",
                Lastname = "",
                Password = "123456",
                ConfirmPassword = "12345",
                Address = "VN",
                DateOfBirth = DateTime.UtcNow.AddYears(-20),
                Email = "client1@gmail.com",
                UserName = "client",
                DepartmentId = "CNPM",
                Gender = Gender.Male,
                RoleName = RoleConstants.STUDENT_NAME
            };
        }

        public static CreateClientRequest CreateClientWithLastnameLessThanMinimumLengthRequest()
        {
            return new CreateClientRequest
            {
                Firstname = "huy",
                Lastname = "n",
                Password = "123456",
                ConfirmPassword = "12345",
                Address = "VN",
                DateOfBirth = DateTime.UtcNow.AddYears(-20),
                Email = "client1@gmail.com",
                UserName = "client",
                DepartmentId = "CNPM",
                Gender = Gender.Male,
                RoleName = RoleConstants.STUDENT_NAME
            };
        }

        public static CreateClientRequest CreateClientWithLastnameGreaterThanMaximumLengthRequest()
        {
            return new CreateClientRequest
            {
                Firstname = "huy",
                Lastname = new string('h', 256),
                Password = "123456",
                ConfirmPassword = "12345",
                Address = "VN",
                DateOfBirth = DateTime.UtcNow.AddYears(-20),
                Email = "client1@gmail.com",
                UserName = "client",
                DepartmentId = "CNPM",
                Gender = Gender.Male,
                RoleName = RoleConstants.STUDENT_NAME
            };
        }

        public static CreateClientRequest CreateClientWithLastnameContainSpecialCharacterRequest()
        {
            return new CreateClientRequest
            {
                Firstname = "huy",
                Lastname = "nguyen thi#",
                Password = "123456",
                ConfirmPassword = "12345",
                Address = "VN",
                DateOfBirth = DateTime.UtcNow.AddYears(-20),
                Email = "client1@gmail.com",
                UserName = "client",
                DepartmentId = "CNPM",
                Gender = Gender.Male,
                RoleName = RoleConstants.STUDENT_NAME
            };
        }

        public static CreateClientRequest CreateClientWithEmailEmptyRequest()
        {
            return new CreateClientRequest
            {
                Firstname = "huy",
                Lastname = "nguyen thi",
                Password = "123456",
                ConfirmPassword = "12345",
                Address = "VN",
                DateOfBirth = DateTime.UtcNow.AddYears(-20),
                Email = "",
                UserName = "client",
                DepartmentId = "CNPM",
                Gender = Gender.Male,
                RoleName = RoleConstants.STUDENT_NAME
            };
        }

        public static CreateClientRequest CreateClientWithEmailInvalidRequest()
        {
            return new CreateClientRequest
            {
                Firstname = "huy",
                Lastname = "nguyen thi",
                Password = "123456",
                ConfirmPassword = "12345",
                Address = "VN",
                DateOfBirth = DateTime.UtcNow.AddYears(-20),
                Email = "client@gmail.com.vn.gg",
                UserName = "client",
                DepartmentId = "CNPM",
                Gender = Gender.Male,
                RoleName = RoleConstants.STUDENT_NAME
            };
        }

        public static CreateClientRequest CreateClientWithEmailDuplicateRequest()
        {
            return new CreateClientRequest
            {
                Firstname = "huy",
                Lastname = "nguyen thi",
                Password = "123456",
                ConfirmPassword = "12345",
                Address = "VN",
                DateOfBirth = DateTime.UtcNow.AddYears(-20),
                Email = "student2@gmail.com",
                UserName = "client",
                DepartmentId = "CNPM",
                Gender = Gender.Male,
                RoleName = RoleConstants.STUDENT_NAME
            };
        }

        public static CreateClientRequest CreateClientWithAddressEmptyRequest()
        {
            return new CreateClientRequest
            {
                Firstname = "huy",
                Lastname = "nguyen thi",
                Password = "123456",
                ConfirmPassword = "12345",
                Address = "",
                DateOfBirth = DateTime.UtcNow.AddYears(-20),
                Email = "client@gmail.com",
                UserName = "client",
                DepartmentId = "CNPM",
                Gender = Gender.Male,
                RoleName = RoleConstants.STUDENT_NAME
            };
        }

        public static CreateClientRequest CreateClientWithAddressGreaterThanMaximumLengthRequest()
        {
            return new CreateClientRequest
            {
                Firstname = "huy",
                Lastname = "nguyen thi",
                Password = "123456",
                ConfirmPassword = "12345",
                Address = new string('a', 256),
                DateOfBirth = DateTime.UtcNow.AddYears(-20),
                Email = "client@gmail.com",
                UserName = "client",
                DepartmentId = "CNPM",
                Gender = Gender.Male,
                RoleName = RoleConstants.STUDENT_NAME
            };
        }

        public static CreateClientRequest CreateClientWithGenderInvalidRequest()
        {
            return new CreateClientRequest
            {
                Firstname = "huy",
                Lastname = "nguyen thi",
                Password = "123456",
                ConfirmPassword = "12345",
                Address = "VN",
                DateOfBirth = DateTime.UtcNow.AddYears(-20),
                Email = "client@gmail.com",
                UserName = "client",
                DepartmentId = "CNPM",
                Gender = (Gender)5,
                RoleName = RoleConstants.STUDENT_NAME
            };
        }

        public static CreateClientRequest CreateClientWithDateOfBirthEmptyRequest()
        {
            return new CreateClientRequest
            {
                Firstname = "huy",
                Lastname = "nguyen thi",
                Password = "123456",
                ConfirmPassword = "12345",
                Address = "VN",
                DateOfBirth = DateTime.MinValue,
                Email = "client@gmail.com",
                UserName = "client",
                DepartmentId = "CNPM",
                Gender = Gender.Male,
                RoleName = RoleConstants.STUDENT_NAME
            };
        }

        public static CreateClientRequest CreateClientWithDateOfBirthLessThanMinimumDateRequest()
        {
            return new CreateClientRequest
            {
                Firstname = "huy",
                Lastname = "nguyen thi",
                Password = "123456",
                ConfirmPassword = "12345",
                Address = "VN",
                DateOfBirth = DateTime.UtcNow.AddMinutes(5),
                Email = "client@gmail.com",
                UserName = "client",
                DepartmentId = "CNPM",
                Gender = Gender.Male,
                RoleName = RoleConstants.STUDENT_NAME
            };
        }

        public static CreateClientRequest CreateClientWithDateOfBirthGreaterThanMaximumDateRequest()
        {
            return new CreateClientRequest
            {
                Firstname = "huy",
                Lastname = "nguyen thi",
                Password = "123456",
                ConfirmPassword = "12345",
                Address = "VN",
                DateOfBirth = DateTime.UtcNow.AddYears(-201),
                Email = "client@gmail.com",
                UserName = "client",
                DepartmentId = "CNPM",
                Gender = Gender.Male,
                RoleName = RoleConstants.STUDENT_NAME
            };
        }

        public static CreateClientRequest CreateClientWithDepartmentIdEmptyRequest()
        {
            return new CreateClientRequest
            {
                Firstname = "client1",
                Lastname = "nguyen thi",
                Password = "123456",
                ConfirmPassword = "123456",
                Address = "VN",
                DateOfBirth = DateTime.UtcNow.AddYears(-20),
                Email = "client1@gmail.com",
                UserName = "client1",
                DepartmentId = "",
                Gender = Gender.Male,
                RoleName = RoleConstants.STUDENT_NAME
            };
        }

        public static CreateClientRequest CreateClientWithDepartmentIdGreaterThanMaximumLengthRequest()
        {
            return new CreateClientRequest
            {
                Firstname = "client1",
                Lastname = "nguyen thi",
                Password = "123456",
                ConfirmPassword = "123456",
                Address = "VN",
                DateOfBirth = DateTime.UtcNow.AddYears(-20),
                Email = "client1@gmail.com",
                UserName = "client1",
                DepartmentId = new string('a', 21),
                Gender = Gender.Male,
                RoleName = RoleConstants.STUDENT_NAME
            };
        }

        public static CreateClientRequest CreateClientWithDepartmentIdContainSpecialCharacterRequest()
        {
            return new CreateClientRequest
            {
                Firstname = "client1",
                Lastname = "nguyen thi",
                Password = "123456",
                ConfirmPassword = "123456",
                Address = "VN",
                DateOfBirth = DateTime.UtcNow.AddYears(-20),
                Email = "client1@gmail.com",
                UserName = "client1",
                DepartmentId = "CNPM@",
                Gender = Gender.Male,
                RoleName = RoleConstants.STUDENT_NAME
            };
        }

        public static CreateClientRequest CreateClientWithDepartmentIdNotFoundRequest()
        {
            return new CreateClientRequest
            {
                Firstname = "client1",
                Lastname = "nguyen thi",
                Password = "123456",
                ConfirmPassword = "123456",
                Address = "VN",
                DateOfBirth = DateTime.UtcNow.AddYears(-20),
                Email = "client1@gmail.com",
                UserName = "client1",
                DepartmentId = "CNPM1",
                Gender = Gender.Male,
                RoleName = RoleConstants.STUDENT_NAME
            };
        }

        public static CreateClientRequest CreateClientWithRolenameNotFoundRequest()
        {
            return new CreateClientRequest
            {
                Firstname = "client1",
                Lastname = "nguyen thi",
                Password = "123456",
                ConfirmPassword = "123456",
                Address = "VN",
                DateOfBirth = DateTime.UtcNow.AddYears(-20),
                Email = "client1@gmail.com",
                UserName = "client1",
                DepartmentId = "CNPM",
                Gender = Gender.Male,
                RoleName = "member"
            };
        }

        public static CreateClientRequest CreateClientWithRolenameEmptyRequest()
        {
            return new CreateClientRequest
            {
                Firstname = "client1",
                Lastname = "nguyen thi",
                Password = "123456",
                ConfirmPassword = "123456",
                Address = "VN",
                DateOfBirth = DateTime.UtcNow.AddYears(-20),
                Email = "client1@gmail.com",
                UserName = "client1",
                DepartmentId = "CNPM",
                Gender = Gender.Male,
                RoleName = ""
            };
        }

        public static CreateClientRequest CreateClientWithRolenameGreaterThanMaximumLengthRequest()
        {
            return new CreateClientRequest
            {
                Firstname = "client1",
                Lastname = "nguyen thi",
                Password = "123456",
                ConfirmPassword = "123456",
                Address = "VN",
                DateOfBirth = DateTime.UtcNow.AddYears(-20),
                Email = "client1@gmail.com",
                UserName = "client1",
                DepartmentId = "CNPM",
                Gender = Gender.Male,
                RoleName = new string('a', 256)
            };
        }

        public static CreateClientRequest CreateClientWithRolenameLassThanMinimumLengthRequest()
        {
            return new CreateClientRequest
            {
                Firstname = "client1",
                Lastname = "nguyen thi",
                Password = "123456",
                ConfirmPassword = "123456",
                Address = "VN",
                DateOfBirth = DateTime.UtcNow.AddYears(-20),
                Email = "client1@gmail.com",
                UserName = "client1",
                DepartmentId = new string('a', 21),
                Gender = Gender.Male,
                RoleName = "1"
            };
        }

        public static UpdateClientRequest UpdateClientRequest()
        {
            return new UpdateClientRequest
            {
                Firstname = "huy",
                Lastname = "huy",
                DateOfBirth = DateTime.UtcNow.AddYears(-18),
                Address = "USA"
            };
        }

        public static UpdateClientRequest UpdateClientWithFirstnameEmptyRequest()
        {
            return new UpdateClientRequest
            {
                Firstname = "",
                Lastname = "huy",
                DateOfBirth = DateTime.UtcNow.AddYears(-18),
                Address = "USA"
            };
        }

        public static UpdateClientRequest UpdateClientWithFirstnameLessThanMinimumLengthRequest()
        {
            return new UpdateClientRequest
            {
                Firstname = "a",
                Lastname = "huy",
                DateOfBirth = DateTime.UtcNow.AddYears(-18),
                Address = "USA"
            };
        }

        public static UpdateClientRequest UpdateClientWithFirstnameGreaterThanMaximumLengthRequest()
        {
            return new UpdateClientRequest
            {
                Firstname = new string('a', 21),
                Lastname = "huy",
                DateOfBirth = DateTime.UtcNow.AddYears(-18),
                Address = "USA"
            };
        }

        public static UpdateClientRequest UpdateClientWithFirstnameContainSpecialCharacterRequest()
        {
            return new UpdateClientRequest
            {
                Firstname = "huy@",
                Lastname = "huy",
                DateOfBirth = DateTime.UtcNow.AddYears(-18),
                Address = "USA"
            };
        }

        public static UpdateClientRequest UpdateClientWithLastnameEmptyRequest()
        {
            return new UpdateClientRequest
            {
                Firstname = "huy",
                Lastname = "",
                DateOfBirth = DateTime.UtcNow.AddYears(-18),
                Address = "USA"
            };
        }

        public static UpdateClientRequest UpdateClientWithLastnameLessThanMinimumLengthRequest()
        {
            return new UpdateClientRequest
            {
                Firstname = "huy",
                Lastname = "h",
                DateOfBirth = DateTime.UtcNow.AddYears(-18),
                Address = "USA"
            };
        }

        public static UpdateClientRequest UpdateClientWithLastnameGreaterThanMaximumLengthRequest()
        {
            return new UpdateClientRequest
            {
                Firstname = "huy",
                Lastname = new string('a', 21),
                DateOfBirth = DateTime.UtcNow.AddYears(-18),
                Address = "USA"
            };
        }

        public static UpdateClientRequest UpdateClientWithLastnameContainSpecialCharacterRequest()
        {
            return new UpdateClientRequest
            {
                Firstname = "huy",
                Lastname = "huy@",
                DateOfBirth = DateTime.UtcNow.AddYears(-18),
                Address = "USA"
            };
        }

        public static UpdateClientRequest UpdateClientWithDateOfBirthEmptyRequest()
        {
            return new UpdateClientRequest
            {
                Firstname = "huy",
                Lastname = "huy",
                DateOfBirth = DateTime.MinValue,
                Address = "USA"
            };
        }

        public static UpdateClientRequest UpdateClientWithDateOfBirthLessThanMinimumDateRequest()
        {
            return new UpdateClientRequest
            {
                Firstname = "huy",
                Lastname = "huy",
                DateOfBirth = DateTime.UtcNow.AddMinutes(5),
                Address = "USA"
            };
        }

        public static UpdateClientRequest UpdateClientWithDateOfBirthGreaterThanMaximumDateRequest()
        {
            return new UpdateClientRequest
            {
                Firstname = "huy",
                Lastname = "huy",
                DateOfBirth = DateTime.UtcNow.AddYears(-201),
                Address = "USA"
            };
        }

        public static UpdateClientRequest UpdateClientWithAddressEmptyRequest()
        {
            return new UpdateClientRequest
            {
                Firstname = "huy",
                Lastname = "huy",
                DateOfBirth = DateTime.UtcNow.AddYears(-18),
                Address = ""
            };
        }

        public static UpdatePasswordRequest UpdatePasswordRequest()
        {
            return new UpdatePasswordRequest
            {
                CurrentPassword = "123456",
                NewPassword = "1234567",
                ConfirmPassword = "1234567"
            };
        }

        public static UpdatePasswordRequest UpdatePasswordWithCurrentPasswordEmptyRequest()
        {
            return new UpdatePasswordRequest
            {
                CurrentPassword = "",
                NewPassword = "1234567",
                ConfirmPassword = "1234567"
            };
        }

        public static UpdatePasswordRequest UpdatePasswordWithCurrentPasswordIncorrectRequest()
        {
            return new UpdatePasswordRequest
            {
                CurrentPassword = "12345678",
                NewPassword = "1234567",
                ConfirmPassword = "1234567"
            };
        }

        public static UpdatePasswordRequest UpdatePasswordWithNewPasswordEmptyRequest()
        {
            return new UpdatePasswordRequest
            {
                CurrentPassword = "123456",
                NewPassword = "",
                ConfirmPassword = ""
            };
        }

        public static UpdatePasswordRequest UpdatePasswordWithNewPasswordLessThanMinimumLengthRequest()
        {
            return new UpdatePasswordRequest
            {
                CurrentPassword = "123456",
                NewPassword = "12345",
                ConfirmPassword = "12345"
            };
        }

        public static UpdatePasswordRequest UpdatePasswordWithNewPasswordGreaterThanMaximumLengthRequest()
        {
            return new UpdatePasswordRequest
            {
                CurrentPassword = "123456",
                NewPassword = new string('1', 21),
                ConfirmPassword = "12345"
            };
        }

        public static UpdatePasswordRequest UpdatePasswordWithNewPasswordSameCurrentPasswordRequest()
        {
            return new UpdatePasswordRequest
            {
                CurrentPassword = "123456",
                NewPassword = "123456",
                ConfirmPassword = "123456"
            };
        }

        public static UpdatePasswordRequest UpdatePasswordWithConfirmPasswordNotMatchRequest()
        {
            return new UpdatePasswordRequest
            {
                CurrentPassword = "123456",
                NewPassword = "1234567",
                ConfirmPassword = "123456"
            };
        }

        public static UpdateImageRequest UpdateAdminImageRequest()
        {
            return new UpdateImageRequest { Image = HandleFile(filesName[0]) };
        }

        public static UpdateImageRequest UpdateTeacherImageRequest()
        {
            return new UpdateImageRequest { Image = HandleFile(filesName[1]) };
        }

        public static UpdateImageRequest UpdateStudentImageRequest()
        {
            return new UpdateImageRequest { Image = HandleFile(filesName[2]) };
        }

        public static UpdateImageRequest UpdateImageWithCurrentImagePathNotFoundRequest()
        {
            return new UpdateImageRequest { Image = HandleFile(filesName[3]) };
        }

        public static UpdateImageRequest UpdateImageWithClientIdNotFoundRequest()
        {
            return new UpdateImageRequest { Image = HandleFile(filesName[4]) };
        }

        public static UpdateImageRequest UpdateImageWithImageNullRequest()
        {
            return new UpdateImageRequest { Image = null };
        }

        public static UpdateImageRequest UpdateImageWithImageExceedTheAllowedSizeRequest()
        {
            return new UpdateImageRequest { Image = HandleFile(filesName[6]) };
        }

        public static UpdateImageRequest UpdateImageWithInvalidImageFormatRequest()
        {
            return new UpdateImageRequest { Image = HandleFile(filesName[5]) };
        }

        private static IFormFile HandleFile(string fileName)
        {
            string dir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

            string parentDir = Directory.GetParent(dir).FullName;

            string filePath = $"{parentDir}\\Images\\{fileName}";

            var stream = new FileStream(filePath, FileMode.Open);

            var formFile = new FormFile(stream, 0, stream.Length, null, fileName)
            {
                Headers = new HeaderDictionary(),

                ContentType = "image/jpeg"
            };

            return formFile;
        }
    }
}
