using ClassManagement.Api.DTO.Semester;

namespace ClassManagement.Api.Tests.TestData
{
    internal static class TestSemesterData
    {
        public static CreateSemesterRequest CreateSemesterRequest()
        {
            return new CreateSemesterRequest
            {
                Id = "HK220242025",
                Name = "học kỳ 2 năm học 2024-2025"
            };
        }

        public static CreateSemesterRequest CreateSemesterWithIdEmptyRequest()
        {
            return new CreateSemesterRequest
            {
                Id = "",
                Name = "học kỳ 2 năm học 2024-2025"
            };
        }

        public static CreateSemesterRequest CreateSemesterWithIdDuplicateRequest()
        {
            return new CreateSemesterRequest
            {
                Id = "HK120232024",
                Name = "học kỳ 2 năm học 2024-2025"
            };
        }

        public static CreateSemesterRequest CreateSemesterWithIdGreaterThanMaximumLengthRequest()
        {
            return new CreateSemesterRequest
            {
                Id = new string('h', 21),
                Name = "học kỳ 2 năm học 2024-2025"
            };
        }

        public static CreateSemesterRequest CreateSemesterWithIdContainSpecialCharacterRequest()
        {
            return new CreateSemesterRequest
            {
                Id = "HK1-20232024",
                Name = "học kỳ 2 năm học 2024-2025"
            };
        }

        public static CreateSemesterRequest CreateSemesterWithNameEmptyRequest()
        {
            return new CreateSemesterRequest
            {
                Id = "HK220242025",
                Name = ""
            };
        }

        public static CreateSemesterRequest CreateSemesterWithNameGreaterThanMaximumLenghtRequest()
        {
            return new CreateSemesterRequest
            {
                Id = "HK220242025",
                Name = new string('h', 256)
            };
        }

        public static UpdateSemesterRequest UpdateSemesterRequest()
        {
            return new UpdateSemesterRequest
            {
                Name = "học kì 1 2023-2024"
            };
        }

        public static UpdateSemesterRequest UpdateSemesterWithNameEmptyRequest()
        {
            return new UpdateSemesterRequest
            {
                Name = ""
            };
        }

        public static UpdateSemesterRequest UpdateSemesterWithNameGreaterThanMaximumLengthRequest()
        {
            return new UpdateSemesterRequest
            {
                Name = new string('h', 256)
            };
        }
    }
}
