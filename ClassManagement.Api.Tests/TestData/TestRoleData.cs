using ClassManagement.Api.DTO.AppRole;

namespace ClassManagement.Api.Tests.TestData
{
    internal static class TestRoleData
    {
        public static CreateRoleRequest CreateRoleRequest()
        {
            return new CreateRoleRequest
            {
                Name = "Test1",
                Description = "Highest role of website, Admin will site management and tasks."
            };
        }

        public static CreateRoleRequest CreateRoleWithNameDuplicateRequest()
        {
            return new CreateRoleRequest
            {
                Name = "admin",
                Description = "Highest role of website, Admin will site management and tasks."
            };
        }

        public static CreateRoleRequest CreateRoleWithNameEmptyRequest()
        {
            return new CreateRoleRequest
            {
                Name = "",
                Description = "Highest role of website, Admin will site management and tasks."
            };
        }

        public static CreateRoleRequest CreateRoleWithNameLessThanMinimumLengthRequest()
        {
            return new CreateRoleRequest
            {
                Name = "T",
                Description = "Highest role of website, Admin will site management and tasks."
            };
        }

        public static CreateRoleRequest CreateRoleWithNameGreaterThanMaximumLengthRequest()
        {
            string name = "superadminsuperadmin";

            for (int i = 0; i < 8; i++) name += name;

            return new CreateRoleRequest
            {
                Name = name,
                Description = "Highest role of website, Admin will site management and tasks."
            };
        }

        public static CreateRoleRequest CreateRoleWithNameContainSpecialCharacterRequest()
        {
            return new CreateRoleRequest
            {
                Name = "Test1@",
                Description = "Highest role of website, Admin will site management and tasks."
            };
        }

        public static UpdateRoleRequest UpdateRoleRequest()
        {
            return new UpdateRoleRequest
            {
                Name = "Test2",
                Description = "This is a test role."
            };
        }

        public static UpdateRoleRequest UpdateRoleWithNameDuplicateRequest()
        {
            return new UpdateRoleRequest
            {
                Name = "admin",
                Description = "This is a test role."
            };
        }

        public static UpdateRoleRequest UpdateRoleWithNameEmptyRequest()
        {
            return new UpdateRoleRequest
            {
                Name = "",
                Description = "This is a test role."
            };
        }

        public static UpdateRoleRequest UpdateRoleWithNameLessThanMinimumLengthRequest()
        {
            return new UpdateRoleRequest
            {
                Name = "T",
                Description = "Highest role of website, Admin will site management and tasks."
            };
        }

        public static UpdateRoleRequest UpdateRoleWithNameGreaterThanMaximumLengthRequest()
        {
            string name = "superadminsuperadmin";

            for (int i = 0; i < 8; i++) name += name;

            return new UpdateRoleRequest
            {
                Name = name,
                Description = "Highest role of website, Admin will site management and tasks."
            };
        }

        public static UpdateRoleRequest UpdateRoleWithNameContainSpecialCharacterRequest()
        {
            return new UpdateRoleRequest
            {
                Name = "Test1@",
                Description = "Highest role of website, Admin will site management and tasks."
            };
        }
    }
}
