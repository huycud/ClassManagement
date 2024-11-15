using ClassManagement.Api.DTO.Subject;
using static Utilities.Enums.EnumTypes;

namespace ClassManagement.Api.Tests.TestData
{
    internal static class TestSubjectData
    {
        public static CreateSubjectRequest CreateSubjectRequest()
        {
            return new CreateSubjectRequest
            {
                Id = "CSDL",
                Name = "Cơ sở dữ liệu",
                Credit = 4,
                Status = Status.Opening,
                DepartmentId = "HTTT",
                IsPracticed = true
            };
        }

        public static CreateSubjectRequest CreateSubjectWithIdEmptyRequest()
        {
            return new CreateSubjectRequest
            {
                Id = "",
                Name = "Cơ sở dữ liệu",
                Credit = 4,
                Status = Status.Opening,
                DepartmentId = "HTTT",
                IsPracticed = true
            };
        }

        public static CreateSubjectRequest CreateSubjectWithIdDuplicateRequest()
        {
            return new CreateSubjectRequest
            {
                Id = "CTRR",
                Name = "Cấu trúc rời rạc",
                Credit = 3,
                Status = Status.Opening,
                DepartmentId = "TOANTIN",
                IsPracticed = false
            };
        }

        public static CreateSubjectRequest CreateSubjectWithNameEmptyRequest()
        {
            return new CreateSubjectRequest
            {
                Id = "CSDL",
                Name = "",
                Credit = 4,
                Status = Status.Opening,
                DepartmentId = "HTTT",
                IsPracticed = true
            };
        }

        public static CreateSubjectRequest CreateSubjectWithStatusInvalidRequest()
        {
            return new CreateSubjectRequest
            {
                Id = "DSTT",
                Name = "Đại số tuyến tính",
                Credit = 3,
                Status = (Status)5,
                DepartmentId = "TOANTIN",
                IsPracticed = false
            };
        }

        public static CreateSubjectRequest CreateSubjectWithDepartmentIdNotFoundRequest()
        {
            return new CreateSubjectRequest
            {
                Id = "DSTT",
                Name = "Đại số tuyến tính",
                Credit = 3,
                Status = Status.Opening,
                DepartmentId = "MANG",
                IsPracticed = false
            };
        }

        public static CreateSubjectRequest CreateSubjectWithCreditLessThanMiniumValueRequest()
        {
            return new CreateSubjectRequest
            {
                Id = "DSTT",
                Name = "Đại số tuyến tính",
                Credit = -1,
                Status = Status.Opening,
                DepartmentId = "TOANTIN",
                IsPracticed = false
            };
        }

        public static CreateSubjectRequest CreateSubjectWithCreditGreaterThanMaximumValueRequest()
        {
            return new CreateSubjectRequest
            {
                Id = "DSTT",
                Name = "Đại số tuyến tính",
                Credit = 11,
                Status = Status.Opening,
                DepartmentId = "TOANTIN",
                IsPracticed = false
            };
        }

        public static CreateSubjectRequest CreateSubjectWithIdGreaterThanMaximumLengthRequest()
        {
            return new CreateSubjectRequest
            {
                Id = "DSTTDSTTDSTTDSTTDSTTDSTT",
                Name = "Đại số tuyến tính",
                Credit = 4,
                Status = Status.Opening,
                DepartmentId = "TOANTIN",
                IsPracticed = false
            };
        }

        public static CreateSubjectRequest CreateSubjectWithDepartmentIdGreaterThanMaximumLengthRequest()
        {
            return new CreateSubjectRequest
            {
                Id = "DSTT",
                Name = "Đại số tuyến tính",
                Credit = 4,
                Status = Status.Opening,
                DepartmentId = "TOANTINTOANTINTOANTIN",
                IsPracticed = false
            };
        }

        public static CreateSubjectRequest CreateSubjectWithIdContainSpecialCharacterRequest()
        {
            return new CreateSubjectRequest
            {
                Id = "DSTT@@",
                Name = "Đại số tuyến tính",
                Credit = 4,
                Status = Status.Opening,
                DepartmentId = "TOANTINTOANTINTOANTIN",
                IsPracticed = false
            };
        }

        public static CreateSubjectRequest CreateSubjectWithDepartmentIdContainSpecialCharacterRequest()
        {
            return new CreateSubjectRequest
            {
                Id = "DSTT",
                Name = "Đại số tuyến tính",
                Credit = 4,
                Status = Status.Opening,
                DepartmentId = "TOANTIN@@",
                IsPracticed = false
            };
        }

        public static UpdateSubjectRequest UpdateSubjectRequest()
        {
            return new UpdateSubjectRequest
            {
                Name = "Operating System",
                Credit = 4,
                Status = Status.Opening,
                IsPracticed = true
            };
        }

        public static UpdateSubjectRequest UpdateSubjectWithNameEmptyRequest()
        {
            return new UpdateSubjectRequest
            {
                Name = "",
                Credit = 3,
                Status = Status.Opening,
                IsPracticed = true
            };
        }

        public static UpdateSubjectRequest UpdateSubjectWithCreditLessThanMinimumValueRequest()
        {
            return new UpdateSubjectRequest
            {
                Name = "Hệ điều hành",
                Credit = -1,
                Status = Status.Opening,
                IsPracticed = true
            };
        }

        public static UpdateSubjectRequest UpdateSubjectWithCreditGreaterThanMaximumValueRequest()
        {
            return new UpdateSubjectRequest
            {
                Name = "Hệ điều hành",
                Credit = 11,
                Status = Status.Opening,
                IsPracticed = true
            };
        }

        public static UpdateSubjectRequest UpdateSubjectWithStatusInvalidRequest()
        {
            return new UpdateSubjectRequest
            {
                Name = "Hệ điều hành",
                Credit = 2,
                Status = (Status)5,
                IsPracticed = true
            };
        }

        public static UpdateSubjectRequest UpdateSubjectWithTypeNotSameTypeClassTeachingRequest()
        {
            return new UpdateSubjectRequest
            {
                Name = "Hệ điều hành",
                Credit = 2,
                Status = (Status)5,
                IsPracticed = false
            };
        }
    }
}
