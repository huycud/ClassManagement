using ClassManagement.Api.DTO.Department;

namespace ClassManagement.Api.Tests.TestData
{
    internal static class TestDepartmentData
    {
        public static CreateDepartmentRequest CreateDepartmentRequest()
        {
            return new CreateDepartmentRequest
            {
                Id = "MMTT",
                Name = "Mạng máy tính và truyền thông"
            };
        }

        public static CreateDepartmentRequest CreateDepartmentWithIdEmptyRequest()
        {
            return new CreateDepartmentRequest
            {
                Id = "",
                Name = "Mạng máy tính và truyền thông"
            };
        }

        public static CreateDepartmentRequest CreateDepartmentWithIdDuplicateRequest()
        {
            return new CreateDepartmentRequest
            {
                Id = "HTTT",
                Name = "hệ thống thông tin"
            };
        }

        public static CreateDepartmentRequest CreateDepartmentWithIdGreaterThanMaximumLengthRequest()
        {
            return new CreateDepartmentRequest
            {
                Id = "HTTTHTTTHTTTHTTTHTTTH",
                Name = "hệ thống thông tin"
            };
        }

        public static CreateDepartmentRequest CreateDepartmentWithIdContainSpecialCharacterRequest()
        {
            return new CreateDepartmentRequest
            {
                Id = "HTTT@",
                Name = "hệ thống thông tin"
            };
        }

        public static CreateDepartmentRequest CreateDepartmentWithNameEmptyRequest()
        {
            return new CreateDepartmentRequest
            {
                Id = "MMTT",
                Name = ""
            };
        }

        public static CreateDepartmentRequest CreateDepartmentWithNameGreaterThanMaximumLengthRequest()
        {
            string name = "hệ thống thông tin";

            for (int i = 0; i < 8; i++)
            {
                name += name;
            }

            return new CreateDepartmentRequest
            {
                Id = "HTTT",
                Name = name
            };
        }

        public static UpdateDepartmentRequest UpdateDepartmentRequest()
        {
            return new UpdateDepartmentRequest
            {
                Name = "Kỹ thuật máy tính"
            };
        }

        public static UpdateDepartmentRequest UpdateDepartmentWithNameEmptyRequest()
        {
            return new UpdateDepartmentRequest
            {
                Name = ""
            };
        }

        public static UpdateDepartmentRequest UpdateDepartmentWithNameGreaterThanMaximumLengthRequest()
        {
            string name = "hệ thống thông tin";

            for (int i = 0; i < 8; i++)
            {
                name += name;
            }

            return new UpdateDepartmentRequest
            {
                Name = name
            };
        }
    }
}
