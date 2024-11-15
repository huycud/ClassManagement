using ClassManagement.Api.DTO.Notifies;
using ClassManagement.Api.DTO.Page;
using ClassManagement.Api.Tests.TestData;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net;

namespace ClassManagement.Api.Tests
{
    public class ClassManagementTest_Notify : ClassManagementTest_Base
    {
        private readonly string _notifyUrl;

        public ClassManagementTest_Notify(BaseFixture fixture) : base(fixture)
        {
            _notifyUrl = $"{_fixture.BaseUrl}/{_fixture.NotifyApi}";
        }

        [Fact]
        public async Task Get_Notifies_With_UserId_Successfully()
        {
            var getNotifyUrl = string.Format(ClassManagementApiDef.GetNotifies, _fixture.UserId, string.Empty, string.Empty,

                            _fixture.PageIndex, _fixture.PageSize, _fixture.SortOrder, _fixture.IsDeleted);

            var response = await _httpClient.GetAsync($"{_notifyUrl}?{getNotifyUrl}");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<PageResult<NotifyResponse>>(result, TestCommonData.ConvertDateTime());

            data.Items.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Get_Notifies_With_UserId_Not_Found_And_Successfully()
        {
            var getNotifyUrl = string.Format(ClassManagementApiDef.GetNotifies, _fixture.UserIdNotFound, string.Empty, string.Empty,

                            _fixture.PageIndex, _fixture.PageSize, _fixture.SortOrder, _fixture.IsDeleted);

            var response = await _httpClient.GetAsync($"{_notifyUrl}?{getNotifyUrl}");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<PageResult<NotifyResponse>>(result, TestCommonData.ConvertDateTime());

            data.Items.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task Get_Notifies_With_UserId_Null_And_Successfully()
        {
            var getNotifyUrl = string.Format(ClassManagementApiDef.GetNotifies, string.Empty, string.Empty, string.Empty,

                            _fixture.PageIndex, _fixture.PageSize, _fixture.SortOrder, _fixture.IsDeleted);

            var response = await _httpClient.GetAsync($"{_notifyUrl}?{getNotifyUrl}");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<PageResult<NotifyResponse>>(result, TestCommonData.ConvertDateTime());

            data.Items.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Get_Notifies_With_Type_And_Successfully()
        {
            var getNotifyUrl = string.Format(ClassManagementApiDef.GetNotifies, string.Empty, _fixture.NotificationType, string.Empty,

                            _fixture.PageIndex, _fixture.PageSize, _fixture.SortOrder, _fixture.IsDeleted);

            var response = await _httpClient.GetAsync($"{_notifyUrl}?{getNotifyUrl}");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<PageResult<NotifyResponse>>(result, TestCommonData.ConvertDateTime());

            data.Items.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Get_Notifies_With_Deleted_Status_And_Successfully()
        {
            var getNotifyUrl = string.Format(ClassManagementApiDef.GetNotifies, string.Empty, string.Empty, string.Empty,

                            _fixture.PageIndex, _fixture.PageSize, _fixture.SortOrder, true);

            var response = await _httpClient.GetAsync($"{_notifyUrl}?{getNotifyUrl}");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<PageResult<NotifyResponse>>(result, TestCommonData.ConvertDateTime());

            data.Items.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Get_Notifies_With_Unauthorized_And_Fail()
        {
            CheckUnauthorized();

            var getNotifyUrl = string.Format(ClassManagementApiDef.GetNotifies, string.Empty, string.Empty, string.Empty,

                            _fixture.PageIndex, _fixture.PageSize, _fixture.SortOrder, _fixture.IsDeleted);

            var response = await _httpClient.GetAsync($"{_notifyUrl}?{getNotifyUrl}");

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Get_Notify_By_Id_Successfully()
        {
            var response = await _httpClient.GetAsync($"{_notifyUrl}/{_fixture.NotifyId}");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            result.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Get_Notify_By_It_Not_Found_And_Fail()
        {
            var response = await _httpClient.GetAsync($"{_notifyUrl}/{_fixture.NotifyIdNotFound}");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Create_Notify_Successfully()
        {
            var data = TestNotifyData.CreateNotifyRequest();

            var response = await _httpClient.PostAsync(_notifyUrl, data.GetRequestMultipartFormContent());

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            result.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Create_Notify_With_Not_Admin_Or_Teacher_Role_And_Fail()
        {
            await CheckStudentAuthorizeAsync();

            var data = TestNotifyData.CreateNotifyRequest();

            var response = await _httpClient.PostAsync(_notifyUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task Create_New_With_User_Id_Empty_And_Fail()
        {
            var data = TestNotifyData.CreateNotifyWithUserIdEmptyRequest();

            var response = await _httpClient.PostAsync(_notifyUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Notify_With_User_Id_Not_Teacher_Or_Not_Admin_And_Fail()
        {
            var data = TestNotifyData.CreateNotifyWithUserIdNotTeacherOrNotAdminRequest();

            var response = await _httpClient.PostAsync(_notifyUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Notify_With_User_Id_Not_Found_And_Fail()
        {
            var data = TestNotifyData.CreateNotifyWithUserIdNotFoundRequest();

            var response = await _httpClient.PostAsync(_notifyUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Create_New_With_User_Id_Less_Than_Zero_And_Fail()
        {
            var data = TestNotifyData.CreateNotifyWithUserIdLessThanZeroRequest();

            var response = await _httpClient.PostAsync(_notifyUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_New_With_Title_Empty_And_Fail()
        {
            var data = TestNotifyData.CreateNotifyWithTitleEmptyRequest();

            var response = await _httpClient.PostAsync(_notifyUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Notify_With_Title_Duplicate_And_Fail()
        {
            var data = TestNotifyData.CreateNotifyWithTitleDuplicateRequest();

            var response = await _httpClient.PostAsync(_notifyUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_New_With_Title_Greater_Than_Maximum_Length_And_Fail()
        {
            var data = TestNotifyData.CreateNotifyWithTitleGreaterThanMaximumLengthRequest();

            var response = await _httpClient.PostAsync(_notifyUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_New_With_Content_Empty_And_Fail()
        {
            var data = TestNotifyData.CreateNotifyWithContentEmptyRequest();

            var response = await _httpClient.PostAsync(_notifyUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_New_With_Type_Invalid_And_Fail()
        {
            var data = TestNotifyData.CreateNotifyWithTypeInvalidRequest();

            var response = await _httpClient.PostAsync(_notifyUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_Notify_Successfully()
        {
            var data = TestNotifyData.UpdateNotifyRequest();

            var response = await _httpClient.PutAsync($"{_notifyUrl}/{_fixture.NotifyId}", data.GetRequestMultipartFormContent());

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Update_Notify_With_Id_Not_Found_And_Fail()
        {
            var data = TestNotifyData.UpdateNotifyRequest();

            var response = await _httpClient.PutAsync($"{_notifyUrl}/{_fixture.NotifyIdNotFound}", data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Update_New_With_Title_Empty_And_Fail()
        {
            var data = TestNotifyData.UpdateNotifyWithTitleEmptyRequest();

            var response = await _httpClient.PutAsync($"{_notifyUrl}/{_fixture.NotifyId}", data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_New_With_Title_Greater_Than_Maximum_Length_And_Fail()
        {
            var data = TestNotifyData.UpdateNotifyWithTitleGreaterThanMaximumLengthRequest();

            var response = await _httpClient.PutAsync($"{_notifyUrl}/{_fixture.NotifyId}", data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_New_With_Content_Empty_And_Fail()
        {
            var data = TestNotifyData.UpdateNotifyWithContentEmptyRequest();

            var response = await _httpClient.PutAsync($"{_notifyUrl}/{_fixture.NotifyId}", data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_New_With_Type_Invalid_And_Fail()
        {
            var data = TestNotifyData.UpdateNotifyWithTypeInvalidRequest();

            var response = await _httpClient.PutAsync($"{_notifyUrl}/{_fixture.NotifyId}", data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
