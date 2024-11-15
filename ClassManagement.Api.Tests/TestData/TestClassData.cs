using ClassManagement.Api.DTO.Class;
using static Utilities.Enums.EnumTypes;

namespace ClassManagement.Api.Tests.TestData
{
    internal static class TestClassData
    {
        public static CreateClassRequest CreateClassRequest()
        {
            return new CreateClassRequest
            {
                Id = "ctrr001",
                Name = "cấu trúc rời rạc 001",
                ClassSize = 100,
                EndedAt = DateTime.UtcNow.AddMonths(3),
                SemesterId = "HK220232024",
                StartedAt = DateTime.UtcNow,
                Type = ClassType.Theory,
                UserId = 100015,
                SubjectId = "CTRR",
                DayOfWeek = DayOfWeek.Monday,
                ClassPeriods = [ClassPeriod.First, ClassPeriod.Second, ClassPeriod.Third]
            };
        }

        public static CreateClassRequest CreateClassWithIdEmptyRequest()
        {
            return new CreateClassRequest
            {
                Id = "",
                Name = "name",
                ClassSize = 100,
                EndedAt = DateTime.UtcNow.AddMonths(3),
                SemesterId = "HK220232024",
                StartedAt = DateTime.UtcNow,
                Type = ClassType.Theory,
                UserId = 100004,
                SubjectId = "CTRR",
                DayOfWeek = DayOfWeek.Tuesday,
                ClassPeriods = [ClassPeriod.First, ClassPeriod.Second, ClassPeriod.Third]
            };
        }

        public static CreateClassRequest CreateClassWithIdDuplicateRequest()
        {
            return new CreateClassRequest
            {
                Id = "ctdlduplicate",
                Name = "name",
                ClassSize = 100,
                EndedAt = DateTime.UtcNow.AddMonths(3),
                SemesterId = "HK220232024",
                StartedAt = DateTime.UtcNow,
                Type = ClassType.Theory,
                UserId = 100004,
                SubjectId = "CTRR",
                DayOfWeek = DayOfWeek.Wednesday,
                ClassPeriods = [ClassPeriod.First, ClassPeriod.Second, ClassPeriod.Third]
            };
        }

        public static CreateClassRequest CreateClassWithIdGreaterThanMaximumLengthRequest()
        {
            return new CreateClassRequest
            {
                Id = new string('h', 21),
                Name = "name",
                ClassSize = 100,
                EndedAt = DateTime.UtcNow.AddMonths(3),
                SemesterId = "HK220232024",
                StartedAt = DateTime.UtcNow,
                Type = ClassType.Theory,
                UserId = 100004,
                SubjectId = "CTRR",
                DayOfWeek = DayOfWeek.Monday,
                ClassPeriods = [ClassPeriod.First, ClassPeriod.Second, ClassPeriod.Third]
            };
        }

        public static CreateClassRequest CreateClassWithIdContainSpecialCharacterRequest()
        {
            return new CreateClassRequest
            {
                Id = "ctrr001@@",
                Name = "name",
                ClassSize = 100,
                EndedAt = DateTime.UtcNow.AddMonths(3),
                SemesterId = "HK220232024",
                StartedAt = DateTime.UtcNow,
                Type = ClassType.Theory,
                UserId = 100004,
                SubjectId = "CTRR",
                DayOfWeek = DayOfWeek.Monday,
                ClassPeriods = [ClassPeriod.First, ClassPeriod.Second, ClassPeriod.Third]
            };
        }

        public static CreateClassRequest CreateClassWithNameEmptyRequest()
        {
            return new CreateClassRequest
            {
                Id = "ctrrnameempty",
                Name = "",
                ClassSize = 100,
                EndedAt = DateTime.UtcNow.AddMonths(3),
                SemesterId = "HK220232024",
                StartedAt = DateTime.UtcNow,
                Type = ClassType.Theory,
                UserId = 100004,
                SubjectId = "CTRR",
                DayOfWeek = DayOfWeek.Monday,
                ClassPeriods = [ClassPeriod.First, ClassPeriod.Second, ClassPeriod.Third]
            };
        }

        public static CreateClassRequest CreateClassWithNameGreaterThanMaximumLengthRequest()
        {
            return new CreateClassRequest
            {
                Id = "ctrrnamegreaterthan",
                Name = new string('h', 256),
                ClassSize = 100,
                EndedAt = DateTime.UtcNow.AddMonths(3),
                SemesterId = "HK220232024",
                StartedAt = DateTime.UtcNow,
                Type = ClassType.Theory,
                UserId = 100004,
                SubjectId = "CTRR",
                DayOfWeek = DayOfWeek.Monday,
                ClassPeriods = [ClassPeriod.First, ClassPeriod.Second, ClassPeriod.Third]
            };
        }

        public static CreateClassRequest CreateClassWithClassSizeEmptyRequest()
        {
            return new CreateClassRequest
            {
                Id = "ctrrclasssizeempty",
                Name = "name",
                EndedAt = DateTime.UtcNow.AddMonths(3),
                SemesterId = "HK220232024",
                StartedAt = DateTime.UtcNow,
                Type = ClassType.Theory,
                UserId = 100004,
                SubjectId = "CTRR",
                DayOfWeek = DayOfWeek.Monday,
                ClassPeriods = [ClassPeriod.First, ClassPeriod.Second, ClassPeriod.Third]
            };
        }

        public static CreateClassRequest CreateClassWithClassSizeGreaterThanMaximumValueRequest()
        {
            return new CreateClassRequest
            {
                Id = "ctrrclasssizegreater",
                Name = "name",
                EndedAt = DateTime.UtcNow.AddMonths(3),
                SemesterId = "HK220232024",
                StartedAt = DateTime.UtcNow,
                Type = ClassType.Theory,
                UserId = 100004,
                SubjectId = "CTRR",
                ClassSize = 151,
                DayOfWeek = DayOfWeek.Monday,
                ClassPeriods = [ClassPeriod.First, ClassPeriod.Second, ClassPeriod.Third]
            };
        }

        public static CreateClassRequest CreateClassWithClassSizeLessThanMinimumValueRequest()
        {
            return new CreateClassRequest
            {
                Id = "ctrrclasssizeless",
                Name = "name",
                EndedAt = DateTime.UtcNow.AddMonths(3),
                SemesterId = "HK220232024",
                StartedAt = DateTime.UtcNow,
                Type = ClassType.Theory,
                UserId = 100004,
                SubjectId = "CTRR",
                ClassSize = 1,
                DayOfWeek = DayOfWeek.Monday,
                ClassPeriods = [ClassPeriod.First, ClassPeriod.Second, ClassPeriod.Third]
            };
        }

        public static CreateClassRequest CreateClassWithUserIdEmptyRequest()
        {
            return new CreateClassRequest
            {
                Id = "ctrruserempty",
                Name = "name",
                ClassSize = 100,
                EndedAt = DateTime.UtcNow.AddMonths(3),
                SemesterId = "HK220232024",
                StartedAt = DateTime.UtcNow,
                Type = ClassType.Theory,
                SubjectId = "CTRR",
                DayOfWeek = DayOfWeek.Monday,
                ClassPeriods = [ClassPeriod.First, ClassPeriod.Second, ClassPeriod.Third]
            };
        }

        public static CreateClassRequest CreateClassWithUserIdNotFoundRequest()
        {
            return new CreateClassRequest
            {
                Id = "ctrrusernotfound",
                Name = "name",
                UserId = 100030,
                ClassSize = 100,
                EndedAt = DateTime.UtcNow.AddMonths(3),
                SemesterId = "HK220232024",
                StartedAt = DateTime.UtcNow,
                Type = ClassType.Theory,
                SubjectId = "CTRR",
                DayOfWeek = DayOfWeek.Monday,
                ClassPeriods = [ClassPeriod.First, ClassPeriod.Second, ClassPeriod.Third]
            };
        }

        public static CreateClassRequest CreateClassWithUserIdNotTeacherIdRequest()
        {
            return new CreateClassRequest
            {
                Id = "ctrrnotteacher",
                Name = "name",
                UserId = 100005,
                ClassSize = 100,
                EndedAt = DateTime.UtcNow.AddMonths(3),
                SemesterId = "HK220232024",
                StartedAt = DateTime.UtcNow,
                Type = ClassType.Theory,
                SubjectId = "CTRR",
                DayOfWeek = DayOfWeek.Monday,
                ClassPeriods = [ClassPeriod.First, ClassPeriod.Second, ClassPeriod.Third]
            };
        }

        public static CreateClassRequest CreateClassWithSubjectIdEmptyRequest()
        {
            return new CreateClassRequest
            {
                Id = "ctrrsubjectempty",
                Name = "name",
                ClassSize = 100,
                EndedAt = DateTime.UtcNow.AddMonths(3),
                SemesterId = "HK220232024",
                StartedAt = DateTime.UtcNow,
                Type = ClassType.Theory,
                UserId = 100004,
                DayOfWeek = DayOfWeek.Monday,
                ClassPeriods = [ClassPeriod.First, ClassPeriod.Second, ClassPeriod.Third]
            };
        }

        public static CreateClassRequest CreateClassWithSubjectIdNotFoundRequest()
        {
            return new CreateClassRequest
            {
                Id = "ctrrsubjectnotfound",
                Name = "name",
                ClassSize = 100,
                EndedAt = DateTime.UtcNow.AddMonths(3),
                SemesterId = "HK220232024",
                StartedAt = DateTime.UtcNow,
                Type = ClassType.Theory,
                UserId = 100004,
                SubjectId = "MMT",
                DayOfWeek = DayOfWeek.Monday,
                ClassPeriods = [ClassPeriod.First, ClassPeriod.Second, ClassPeriod.Third]
            };
        }

        public static CreateClassRequest CreateClassWithSubjectClosedRequest()
        {
            return new CreateClassRequest
            {
                Id = "dsttsubjectclosed",
                Name = "name",
                ClassSize = 100,
                EndedAt = DateTime.UtcNow.AddMonths(3),
                SemesterId = "HK220232024",
                StartedAt = DateTime.UtcNow,
                Type = ClassType.Theory,
                UserId = 100004,
                SubjectId = "DSTT",
                DayOfWeek = DayOfWeek.Monday,
                ClassPeriods = [ClassPeriod.First, ClassPeriod.Second, ClassPeriod.Third]
            };
        }

        public static CreateClassRequest CreateClassWithSubjectIdGreaterThanMaximumLengthRequest()
        {
            return new CreateClassRequest
            {
                Id = "ctrrsubjectgreater",
                Name = "name",
                ClassSize = 100,
                EndedAt = DateTime.UtcNow.AddMonths(3),
                SemesterId = "HK220232024",
                StartedAt = DateTime.UtcNow,
                Type = ClassType.Theory,
                UserId = 100004,
                SubjectId = new string('h', 21),
                DayOfWeek = DayOfWeek.Monday,
                ClassPeriods = [ClassPeriod.First, ClassPeriod.Second, ClassPeriod.Third]
            };
        }

        public static CreateClassRequest CreateClassWithSubjectIdContainSpecialCharacterRequest()
        {
            return new CreateClassRequest
            {
                Id = "ctrrsubjectcontain",
                Name = "name",
                ClassSize = 100,
                EndedAt = DateTime.UtcNow.AddMonths(3),
                SemesterId = "HK220232024",
                StartedAt = DateTime.UtcNow,
                Type = ClassType.Theory,
                UserId = 100004,
                SubjectId = "@@test",
                DayOfWeek = DayOfWeek.Monday,
                ClassPeriods = [ClassPeriod.First, ClassPeriod.Second, ClassPeriod.Third]
            };
        }

        public static CreateClassRequest CreateClassWithSemesterIdEmptyRequest()
        {
            return new CreateClassRequest
            {
                Id = "ctrrsemesterempty",
                Name = "name",
                ClassSize = 100,
                EndedAt = DateTime.UtcNow.AddMonths(3),
                StartedAt = DateTime.UtcNow,
                Type = ClassType.Theory,
                UserId = 100004,
                SubjectId = "CTRR",
                DayOfWeek = DayOfWeek.Monday,
                ClassPeriods = [ClassPeriod.First, ClassPeriod.Second, ClassPeriod.Third]
            };
        }

        public static CreateClassRequest CreateClassWithSemesterIdNotFoundRequest()
        {
            return new CreateClassRequest
            {
                Id = "ctrrsemesternotf",
                Name = "name",
                ClassSize = 100,
                SemesterId = "HK2202320242",
                EndedAt = DateTime.UtcNow.AddMonths(3),
                StartedAt = DateTime.UtcNow,
                Type = ClassType.Theory,
                UserId = 100004,
                SubjectId = "CTRR",
                DayOfWeek = DayOfWeek.Monday,
                ClassPeriods = [ClassPeriod.First, ClassPeriod.Second, ClassPeriod.Third]
            };
        }

        public static CreateClassRequest CreateClassWithSemesterIdGreaterThanMaximumLengthRequest()
        {
            return new CreateClassRequest
            {
                Id = "ctrrsemestergreater",
                Name = "name",
                ClassSize = 100,
                SemesterId = new string('h', 21),
                EndedAt = DateTime.UtcNow.AddMonths(3),
                StartedAt = DateTime.UtcNow,
                Type = ClassType.Theory,
                UserId = 100004,
                SubjectId = "CTRR",
                DayOfWeek = DayOfWeek.Monday,
                ClassPeriods = [ClassPeriod.First, ClassPeriod.Second, ClassPeriod.Third]
            };
        }

        public static CreateClassRequest CreateClassWithSemesterIdContainSpecialCharacterRequest()
        {
            return new CreateClassRequest
            {
                Id = "ctrrsemestercontain",
                Name = "name",
                ClassSize = 100,
                SemesterId = "HK120232024@",
                EndedAt = DateTime.UtcNow.AddMonths(3),
                StartedAt = DateTime.UtcNow,
                Type = ClassType.Theory,
                UserId = 100004,
                SubjectId = "CTRR",
                DayOfWeek = DayOfWeek.Monday,
                ClassPeriods = [ClassPeriod.First, ClassPeriod.Second, ClassPeriod.Third]
            };
        }

        public static CreateClassRequest CreateClassWithTypeInvalidRequest()
        {
            return new CreateClassRequest
            {
                Id = "ctrrtypeinvalid",
                Name = "name",
                ClassSize = 100,
                SemesterId = "HK220232024",
                EndedAt = DateTime.UtcNow.AddMonths(3),
                StartedAt = DateTime.UtcNow,
                Type = (ClassType)100,
                UserId = 100004,
                SubjectId = "CTRR",
                DayOfWeek = DayOfWeek.Monday,
                ClassPeriods = [ClassPeriod.First, ClassPeriod.Second, ClassPeriod.Third]
            };
        }

        public static CreateClassRequest CreateClassWithClassTypeAndSubjectTypeNotMatchRequest()
        {
            return new CreateClassRequest
            {
                Id = "ctrrtypesubject",
                Name = "name",
                ClassSize = 100,
                SemesterId = "HK220232024",
                EndedAt = DateTime.UtcNow.AddMonths(3),
                StartedAt = DateTime.UtcNow,
                Type = ClassType.Practice,
                UserId = 100004,
                SubjectId = "CTRR",
                DayOfWeek = DayOfWeek.Monday,
                ClassPeriods = [ClassPeriod.First, ClassPeriod.Second, ClassPeriod.Third]
            };
        }

        public static CreateClassRequest CreateClassWithDayOfWeekInvalidRequest()
        {
            return new CreateClassRequest
            {
                Id = "ctrrdow",
                Name = "name",
                ClassSize = 100,
                SemesterId = "HK220232024",
                EndedAt = DateTime.UtcNow.AddMonths(3),
                StartedAt = DateTime.UtcNow,
                Type = ClassType.Theory,
                UserId = 100004,
                SubjectId = "CTRR",
                DayOfWeek = (DayOfWeek)100,
                ClassPeriods = [ClassPeriod.First, ClassPeriod.Second, ClassPeriod.Third]
            };
        }

        public static CreateClassRequest CreateClassWithClassPeriodsInvalidRequest()
        {
            return new CreateClassRequest
            {
                Id = "ctrrperiodin",
                Name = "name",
                ClassSize = 100,
                EndedAt = DateTime.UtcNow.AddMonths(3),
                SemesterId = "HK220232024",
                StartedAt = DateTime.UtcNow,
                Type = ClassType.Theory,
                UserId = 100004,
                SubjectId = "CTRR",
                DayOfWeek = DayOfWeek.Monday,
                ClassPeriods = []
            };
        }

        public static CreateClassRequest CreateClassWithStartedAtEmptyRequest()
        {
            return new CreateClassRequest
            {
                Id = "ctrrstartedempty",
                Name = "name",
                ClassSize = 100,
                EndedAt = DateTime.UtcNow.AddMonths(3),
                SemesterId = "HK220232024",
                Type = ClassType.Theory,
                UserId = 100004,
                SubjectId = "CTRR",
                DayOfWeek = DayOfWeek.Monday,
                ClassPeriods = [ClassPeriod.First, ClassPeriod.Second, ClassPeriod.Third]
            };
        }

        public static CreateClassRequest CreateClassWithEndedAtEmptyRequest()
        {
            return new CreateClassRequest
            {
                Id = "ctrrendedempty",
                Name = "name",
                ClassSize = 100,
                SemesterId = "HK220232024",
                StartedAt = DateTime.UtcNow,
                Type = ClassType.Theory,
                UserId = 100004,
                SubjectId = "CTRR",
                DayOfWeek = DayOfWeek.Monday,
                ClassPeriods = [ClassPeriod.First, ClassPeriod.Second, ClassPeriod.Third]
            };
        }

        public static CreateClassRequest CreateClassWithStartedAtGreaterThanEndedAtRequest()
        {
            return new CreateClassRequest
            {
                Id = "ctrrstartedgreater",
                Name = "name",
                ClassSize = 100,
                EndedAt = DateTime.UtcNow.AddMonths(3),
                SemesterId = "HK220232024",
                StartedAt = DateTime.UtcNow.AddMonths(4),
                Type = ClassType.Theory,
                UserId = 100004,
                SubjectId = "CTRR",
                DayOfWeek = DayOfWeek.Monday,
                ClassPeriods = [ClassPeriod.First, ClassPeriod.Second, ClassPeriod.Third]
            };
        }

        public static CreateClassRequest CreateClassWithUserAndSubjectNotInTheSameDepartmentRequest()
        {
            return new CreateClassRequest
            {
                Id = "ctrrusersubject",
                Name = "name",
                ClassSize = 100,
                EndedAt = DateTime.Now,
                SemesterId = "HK220232024",
                StartedAt = DateTime.Now,
                Type = ClassType.Theory,
                UserId = 100001,
                SubjectId = "CTRR",
                DayOfWeek = DayOfWeek.Monday,
                ClassPeriods = [ClassPeriod.First, ClassPeriod.Second, ClassPeriod.Third]
            };
        }

        public static UpdateClassRequest UpdateClassRequest()
        {
            return new UpdateClassRequest
            {
                Name = "Chính trị Mac-Lenin",
                ClassSize = 100,
                EndedAt = DateTime.Now.AddMonths(3),
                StartedAt = DateTime.Now,
                UserId = 100004
            };
        }

        public static UpdateClassRequest UpdateClassWithNameEmptyRequest()
        {
            return new UpdateClassRequest
            {
                ClassSize = 100,
                EndedAt = DateTime.Now.AddMonths(3),
                StartedAt = DateTime.Now,
                UserId = 100004
            };
        }

        public static UpdateClassRequest UpdateClassWithNameGreaterThanMaximumLengthRequest()
        {
            return new UpdateClassRequest
            {
                Name = new string('h', 256),
                ClassSize = 100,
                EndedAt = DateTime.Now.AddMonths(3),
                StartedAt = DateTime.Now,
                UserId = 100004
            };
        }

        public static UpdateClassRequest UpdateClassWithClassSizeEmptyRequest()
        {
            return new UpdateClassRequest
            {
                Name = "Hệ điều hành",
                EndedAt = DateTime.Now.AddMonths(3),
                StartedAt = DateTime.Now,
                UserId = 100004
            };
        }

        public static UpdateClassRequest UpdateClassWithClassSizeLessThanMinimumValueRequest()
        {
            return new UpdateClassRequest
            {
                Name = "Hệ điều hành",
                ClassSize = 29,
                EndedAt = DateTime.Now.AddMonths(3),
                StartedAt = DateTime.Now,
                UserId = 100004
            };
        }

        public static UpdateClassRequest UpdateClassWithClassSizeGreaterThanMaximumValueRequest()
        {
            return new UpdateClassRequest
            {
                Name = "Hệ điều hành",
                ClassSize = 151,
                EndedAt = DateTime.Now.AddMonths(3),
                StartedAt = DateTime.Now,
                UserId = 100004
            };
        }

        public static UpdateClassRequest UpdateClassWithUserIdEmptyRequest()
        {
            return new UpdateClassRequest
            {
                Name = "Hệ điều hành",
                ClassSize = 100,
                EndedAt = DateTime.Now.AddMonths(3),
                StartedAt = DateTime.Now
            };
        }

        public static UpdateClassRequest UpdateClassWithUserIdNotFoundRequest()
        {
            return new UpdateClassRequest
            {
                Name = "Hệ điều hành",
                ClassSize = 100,
                EndedAt = DateTime.Now.AddMonths(3),
                StartedAt = DateTime.Now,
                UserId = 100055
            };
        }

        public static UpdateClassRequest UpdateClassWithUserAndSubjectNotInTheSameDepartmentRequest()
        {
            return new UpdateClassRequest
            {
                Name = "Hệ điều hành",
                ClassSize = 100,
                EndedAt = DateTime.Now.AddMonths(3),
                StartedAt = DateTime.Now,
                UserId = 100005
            };
        }

        public static UpdateClassRequest UpdateClassWithUserIdNotTeacherIdRequest()
        {
            return new UpdateClassRequest
            {
                Name = "Hệ điều hành",
                ClassSize = 100,
                EndedAt = DateTime.Now.AddMonths(3),
                StartedAt = DateTime.Now,
                UserId = 100005
            };
        }

        public static UpdateClassRequest UpdateClassWithStartedAtGreaterThanEndedAtRequest()
        {
            return new UpdateClassRequest
            {
                Name = "Hệ điều hành",
                ClassSize = 100,
                EndedAt = DateTime.Now,
                StartedAt = DateTime.Now.AddMonths(3),
                UserId = 100004
            };
        }

        public static UpdateClassRequest UpdateClassWithStartedAtEmptyRequest()
        {
            return new UpdateClassRequest
            {
                Name = "Hệ điều hành",
                ClassSize = 100,
                EndedAt = DateTime.Now,
                UserId = 100004
            };
        }

        public static UpdateClassRequest UpdateClassWithEndedAtEmptyRequest()
        {
            return new UpdateClassRequest
            {
                Name = "Hệ điều hành",
                ClassSize = 100,
                StartedAt = DateTime.Now,
                UserId = 100004
            };
        }

        public static List<int> AddStudentsToClassRequest()
        {
            return new List<int> { 100010, 100011 };
        }

        public static List<int> AddStudentsToClassWithInvalidAmountClassRequest()
        {
            return new List<int> { 100012, 100013, 100014 };
        }

        public static List<int> AddStudentsToClassWithStudentIdNotFoundRequest()
        {
            return new List<int> { 100012, 100055 };
        }

        public static List<int> AddStudentsToClassWithStudentIdWasInClassRequest()
        {
            return new List<int> { 100012, 100013, 100007 };
        }

        public static List<int> AddStudentsToClassWithNotFoundTheoryClassRequest()
        {
            return new List<int> { 100014 };
        }

        public static List<int> AddStudentsToClassWithStudentIdInvalidRequest()
        {
            return new List<int> { 100005, 100006, 100004 };
        }

        public static List<int> AddStudentsToClassWithListInvalidRequest()
        {
            return new List<int>();
        }
    }
}
