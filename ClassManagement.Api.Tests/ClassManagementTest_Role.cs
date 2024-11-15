using System.Net;
using ClassManagement.Api.DTO.AppRole;
using ClassManagement.Api.DTO.Page;
using ClassManagement.Api.Tests.TestData;
using FluentAssertions;
using Newtonsoft.Json;

namespace ClassManagement.Api.Tests
{
    public class ClassManagementTest_Role : ClassManagementTest_Base
    {
        private readonly string _roleUrl;

        public ClassManagementTest_Role(BaseFixture fixture) : base(fixture)
        {
            _roleUrl = $"{_fixture.BaseUrl}/{_fixture.RoleApi}";
        }


        [Fact]
        public async Task Get_Roles_With_Keyword_Null_And_Successfully()
        {
            var getRoleUrl = string.Format(ClassManagementApiDef.GetPageUrl, string.Empty, _fixture.PageIndex, _fixture.PageSize, _fixture.SortOrder);

            var response = await _httpClient.GetAsync($"{_roleUrl}?{getRoleUrl}");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<PageResult<RoleResponse>>(result);

            data.Items.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Get_Roles_With_Keyword_Not_Null_And_Successfully()
        {
            var getRoleUrl = string.Format(ClassManagementApiDef.GetPageUrl, _fixture.RoleKeyword, _fixture.PageIndex, _fixture.PageSize, _fixture.SortOrder);

            var response = await _httpClient.GetAsync($"{_roleUrl}?{getRoleUrl}");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<PageResult<RoleResponse>>(result);

            data.Items.Should().NotBeNullOrEmpty();

            data.Items.Count.Should().Be(1);
        }

        [Fact]
        public async Task Get_Roles_With_Keyword_Not_Found_And_Successfully()
        {
            var getRoleUrl = string.Format(ClassManagementApiDef.GetPageUrl, _fixture.RoleKeywordNotFound, _fixture.PageIndex, _fixture.PageSize, _fixture.SortOrder);

            var response = await _httpClient.GetAsync($"{_roleUrl}?{getRoleUrl}");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<PageResult<RoleResponse>>(result);

            data?.Items.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task Get_Role_With_Not_Admin_Role_And_Fail()
        {
            await CheckStudentAuthorizeAsync();

            var getRoleUrl = string.Format(ClassManagementApiDef.GetPageUrl, string.Empty, _fixture.PageIndex, _fixture.PageSize, _fixture.SortOrder);

            var response = await _httpClient.GetAsync($"{_roleUrl}?{getRoleUrl}");

            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task Get_Role_By_Id_Successfully()
        {
            var response = await _httpClient.GetAsync($"{_roleUrl}/{_fixture.RoleId}");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            result.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Get_Role_By_Id_With_Id_Not_Found_And_Fail()
        {
            var response = await _httpClient.GetAsync($"{_roleUrl}/{_fixture.RoleIdNotFound}");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Get_Role_By_Id_With_Not_Admin_Role_And_Fail()
        {
            await CheckStudentAuthorizeAsync();

            var response = await _httpClient.GetAsync($"{_roleUrl}/{_fixture.RoleId}");

            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task Create_Role_Successfully()
        {
            var data = TestRoleData.CreateRoleRequest();

            var response = await _httpClient.PostAsync(_roleUrl, data.GetRequestContent());

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            result.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Create_Role_With_Name_Duplicate_And_Fail()
        {
            var data = TestRoleData.CreateRoleWithNameDuplicateRequest();

            var response = await _httpClient.PostAsync(_roleUrl, data.GetRequestContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Role_With_Name_Empty_And_Fail()
        {
            var data = TestRoleData.CreateRoleWithNameEmptyRequest();

            var response = await _httpClient.PostAsync(_roleUrl, data.GetRequestContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Role_With_Name_Less_Than_Minimum_Length_And_Fail()
        {
            var data = TestRoleData.CreateRoleWithNameLessThanMinimumLengthRequest();

            var response = await _httpClient.PostAsync(_roleUrl, data.GetRequestContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Role_With_Name_Greater_Than_Maximum_Length_And_Fail()
        {
            var data = TestRoleData.CreateRoleWithNameGreaterThanMaximumLengthRequest();

            var response = await _httpClient.PostAsync(_roleUrl, data.GetRequestContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Role_With_Name_Contain_Special_Character_And_Fail()
        {
            var data = TestRoleData.CreateRoleWithNameContainSpecialCharacterRequest();

            var response = await _httpClient.PostAsync(_roleUrl, data.GetRequestContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Role_With_Not_Admin_Role_And_Fail()
        {
            await CheckStudentAuthorizeAsync();

            var data = TestRoleData.CreateRoleRequest();

            var response = await _httpClient.PostAsync(_roleUrl, data.GetRequestContent());

            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task Update_Role_Successfully()
        {
            var data = TestRoleData.UpdateRoleRequest();

            var response = await _httpClient.PutAsync($"{_roleUrl}/{_fixture.RoleId}", data.GetRequestContent());

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Update_Role_With_Id_Not_Found_And_Fail()
        {
            var data = TestRoleData.UpdateRoleRequest();

            var response = await _httpClient.PutAsync($"{_roleUrl}/{_fixture.RoleIdNotFound}", data.GetRequestContent());

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Update_Role_With_Name_Duplicate_And_Fail()
        {
            var data = TestRoleData.UpdateRoleWithNameDuplicateRequest();

            var response = await _httpClient.PutAsync($"{_roleUrl}/{_fixture.RoleId}", data.GetRequestContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_Role_With_Name_Empty_And_Fail()
        {
            var data = TestRoleData.UpdateRoleWithNameEmptyRequest();

            var response = await _httpClient.PutAsync($"{_roleUrl}/{_fixture.RoleId}", data.GetRequestContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_Role_With_Name_Less_Than_Minimum_Length_And_Fail()
        {
            var data = TestRoleData.UpdateRoleWithNameLessThanMinimumLengthRequest();

            var response = await _httpClient.PutAsync($"{_roleUrl}/{_fixture.RoleId}", data.GetRequestContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_Role_With_Name_Greater_Than_Maximum_Length_And_Fail()
        {
            var data = TestRoleData.UpdateRoleWithNameGreaterThanMaximumLengthRequest();

            var response = await _httpClient.PutAsync($"{_roleUrl}/{_fixture.RoleId}", data.GetRequestContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_Role_With_Name_Contain_Special_Character_And_Fail()
        {
            var data = TestRoleData.UpdateRoleWithNameContainSpecialCharacterRequest();

            var response = await _httpClient.PutAsync($"{_roleUrl}/{_fixture.RoleId}", data.GetRequestContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_Role_With_Not_Admin_Role_And_Fail()
        {
            await CheckStudentAuthorizeAsync();

            var data = TestRoleData.UpdateRoleRequest();

            var response = await _httpClient.PutAsync($"{_roleUrl}/{_fixture.RoleId}", data.GetRequestContent());

            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task Delete_Role_Successfully()
        {
            var response = await _httpClient.DeleteAsync($"{_roleUrl}/{_fixture.RoleIdToDelete}");

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Delete_Role_With_Id_Not_Found_And_Fail()
        {
            var response = await _httpClient.DeleteAsync($"{_roleUrl}/{_fixture.RoleIdNotFound}");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Delete_Role_With_Admin_Id_And_Fail()
        {
            var response = await _httpClient.DeleteAsync($"{_roleUrl}/{_fixture.AdminRoleId}");

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Delete_Role_With_Not_Admin_Role_And_Fail()
        {
            await CheckStudentAuthorizeAsync();

            var response = await _httpClient.DeleteAsync($"{_roleUrl}/{_fixture.RoleIdToDelete}");

            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task Delete_Role_With_Role_That_Many_User_Own_And_Fail()
        {
            var response = await _httpClient.DeleteAsync($"{_roleUrl}/{_fixture.RoleIdToDeleteFail}");

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

    }
}
