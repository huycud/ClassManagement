using ClassManagement.Api.Tests.TestData;
using FluentAssertions;
using System.Net;

namespace ClassManagement.Api.Tests
{
    public class ClassManagementTest_Authentication : ClassManagementTest_Base
    {
        private readonly string _authenticationUrl;

        public ClassManagementTest_Authentication(BaseFixture fixture) : base(fixture)
        {
            _authenticationUrl = $"{_fixture.BaseUrl}/{_fixture.AuthenticationApi}";

            CheckUnauthorized();
        }

        [Fact]
        public async Task Login_Successfully()
        {
            var loginUrl = string.Format(ClassManagementApiDef.GetLoginUrl, _authenticationUrl);

            var data = TestAuthenticationData.LoginRequest();

            var response = await _httpClient.PostAsync(loginUrl, data.GetRequestContent());

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Login_With_Username_Not_Found_And_Fail()
        {
            var loginUrl = string.Format(ClassManagementApiDef.GetLoginUrl, _authenticationUrl);

            var data = TestAuthenticationData.LoginWithUsernameNotFoundRequest();

            var response = await _httpClient.PostAsync(loginUrl, data.GetRequestContent());

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Login_With_Password_Invalid_And_Fail()
        {
            var loginUrl = string.Format(ClassManagementApiDef.GetLoginUrl, _authenticationUrl);

            var data = TestAuthenticationData.LoginWithPasswordInvalidRequest();

            var response = await _httpClient.PostAsync(loginUrl, data.GetRequestContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Login_With_Password_Invalid_Exceed_The_Allowed_Amount_And_Fail()
        {
            var loginUrl = string.Format(ClassManagementApiDef.GetLoginUrl, _authenticationUrl);

            var data = TestAuthenticationData.LoginWithPasswordInvalidExceedTheAllowedAmountRequest();

            int i = 0;

            HttpResponseMessage? response;
            do
            {
                response = await _httpClient.PostAsync(loginUrl, data.GetRequestContent());

                i++;

            } while (i < 4);

            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task Login_With_Username_Disabled_And_Fail()
        {
            var loginUrl = string.Format(ClassManagementApiDef.GetLoginUrl, _authenticationUrl);

            var data = TestAuthenticationData.LoginWithUsernameDisabledRequest();

            var response = await _httpClient.PostAsync(loginUrl, data.GetRequestContent());

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
