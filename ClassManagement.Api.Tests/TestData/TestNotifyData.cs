using ClassManagement.Api.DTO.Notifies;
using static Utilities.Enums.EnumTypes;

namespace ClassManagement.Api.Tests.TestData
{
    internal static class TestNotifyData
    {
        public static CreateNotifyRequest CreateNotifyRequest()
        {
            return new CreateNotifyRequest
            {
                Title = "title",
                Content = "content",
                Type = NotifyType.System,
                UserId = 100001
            };
        }

        public static CreateNotifyRequest CreateNotifyWithUserIdEmptyRequest()
        {
            return new CreateNotifyRequest
            {
                Title = "title 14",
                Content = "content",
                Type = NotifyType.System,
            };
        }

        public static CreateNotifyRequest CreateNotifyWithUserIdNotFoundRequest()
        {
            return new CreateNotifyRequest
            {
                Title = "title 12",
                Content = "content",
                Type = NotifyType.System,
                UserId = 111000
            };
        }

        public static CreateNotifyRequest CreateNotifyWithUserIdNotTeacherOrNotAdminRequest()
        {
            return new CreateNotifyRequest
            {
                Title = "title 124",
                Content = "content",
                Type = NotifyType.System,
                UserId = 100005
            };
        }

        public static CreateNotifyRequest CreateNotifyWithUserIdLessThanZeroRequest()
        {
            return new CreateNotifyRequest
            {
                Title = "title 12312",
                Content = "content",
                Type = NotifyType.System,
                UserId = -1
            };
        }

        public static CreateNotifyRequest CreateNotifyWithTitleEmptyRequest()
        {
            return new CreateNotifyRequest
            {
                Title = "",
                Content = "content",
                Type = NotifyType.System,
                UserId = 100001
            };
        }

        public static CreateNotifyRequest CreateNotifyWithTitleDuplicateRequest()
        {
            return new CreateNotifyRequest
            {
                Title = "test tạo thông báo lần 1",
                Content = "content",
                Type = NotifyType.System,
                UserId = 100001
            };
        }

        public static CreateNotifyRequest CreateNotifyWithContentEmptyRequest()
        {
            return new CreateNotifyRequest
            {
                Title = "test tạo thông báo lần 4",
                Content = "",
                Type = NotifyType.System,
                UserId = 100001
            };
        }

        public static CreateNotifyRequest CreateNotifyWithTypeInvalidRequest()
        {
            return new CreateNotifyRequest
            {
                Title = "test tạo thông báo lần 4",
                Content = "content",
                Type = (NotifyType)6,
                UserId = 100001
            };
        }

        public static CreateNotifyRequest CreateNotifyWithTitleGreaterThanMaximumLengthRequest()
        {
            return new CreateNotifyRequest
            {
                Title = new string('h', 256),
                Content = "content",
                Type = NotifyType.System,
                UserId = 100001
            };
        }

        public static UpdateNotifyRequest UpdateNotifyRequest()
        {
            return new UpdateNotifyRequest
            {
                Title = "test tạo thông báo lần 4",
                Content = "content",
                Type = NotifyType.Class
            };
        }

        public static UpdateNotifyRequest UpdateNotifyWithTitleEmptyRequest()
        {
            return new UpdateNotifyRequest
            {
                Title = "",
                Content = "content",
                Type = NotifyType.Class
            };
        }

        public static UpdateNotifyRequest UpdateNotifyWithTitleGreaterThanMaximumLengthRequest()
        {
            return new UpdateNotifyRequest
            {
                Title = new string('h', 256),
                Content = "content",
                Type = NotifyType.System,
            };
        }

        public static UpdateNotifyRequest UpdateNotifyWithContentEmptyRequest()
        {
            return new UpdateNotifyRequest
            {
                Title = "test tạo thông báo lần 4",
                Content = "",
                Type = NotifyType.System,
            };
        }

        public static UpdateNotifyRequest UpdateNotifyWithTypeInvalidRequest()
        {
            return new UpdateNotifyRequest
            {
                Title = "test tạo thông báo lần 4",
                Content = "content",
                Type = (NotifyType)6,
            };
        }
    }
}
