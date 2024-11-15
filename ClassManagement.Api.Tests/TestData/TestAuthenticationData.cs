using ClassManagement.Api.DTO.Authentication;

namespace ClassManagement.Api.Tests.TestData
{
    internal static class TestAuthenticationData
    {
        public static LoginRequest LoginRequest()
        {
            return new LoginRequest
            {
                UserName = "user1",
                Password = "123456"
            };
        }

        public static LoginRequest LoginWithUsernameNotFoundRequest()
        {
            return new LoginRequest
            {
                UserName = "admin99",
                Password = "123456"
            };
        }

        public static LoginRequest LoginWithPasswordInvalidRequest()
        {
            return new LoginRequest
            {
                UserName = "user3",
                Password = "1234567"
            };
        }

        public static LoginRequest LoginWithPasswordInvalidExceedTheAllowedAmountRequest()
        {
            return new LoginRequest
            {
                UserName = "user5",
                Password = "1234567"
            };
        }

        public static LoginRequest LoginWithUsernameDisabledRequest()
        {
            return new LoginRequest
            {
                UserName = "user17",
                Password = "123456"
            };
        }

    }
}
