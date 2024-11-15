using System.Net;
using ClassManagement.Api.DTO.Department;
using ClassManagement.Api.DTO.Page;
using ClassManagement.Api.Tests.TestData;
using FluentAssertions;
using Newtonsoft.Json;

namespace ClassManagement.Api.Tests
{
    public class ClassManagementTest_Department : ClassManagementTest_Base
    {
        private readonly string _departmentUrl;

        public ClassManagementTest_Department(BaseFixture fixture) : base(fixture)
        {
            _departmentUrl = $"{_fixture.BaseUrl}/{_fixture.DepartmentApi}";
        }

        [Fact]
        public async Task Get_Departmets_With_Keyword_Null_And_Successfully()
        {
            var getDepartmentUrl = string.Format(ClassManagementApiDef.GetPageUrl, string.Empty, _fixture.PageIndex, _fixture.PageSize, _fixture.SortOrder);

            var response = await _httpClient.GetAsync($"{_departmentUrl}?{getDepartmentUrl}");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<PageResult<DepartmentResponse>>(result, TestCommonData.ConvertDateTime());

            data?.Items.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Get_Departments_With_Keyword_Not_Null_And_Successfully()
        {
            var getDepartmentUrl = string.Format(ClassManagementApiDef.GetPageUrl, _fixture.DepartmentKeyword, _fixture.PageIndex, _fixture.PageSize, _fixture.SortOrder);

            var response = await _httpClient.GetAsync($"{_departmentUrl}?{getDepartmentUrl}");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<PageResult<DepartmentResponse>>(result, TestCommonData.ConvertDateTime());

            data?.Items.Should().NotBeNullOrEmpty();

            data?.Items.Count.Should().Be(1);
        }

        [Fact]
        public async Task Get_Departments_With_Keyword_Not_Found_And_Successfully()
        {
            var getDepartmentUrl = string.Format(ClassManagementApiDef.GetPageUrl, _fixture.DepartmentKeywordNotFound, _fixture.PageIndex, _fixture.PageSize, _fixture.SortOrder);

            var response = await _httpClient.GetAsync($"{_departmentUrl}?{getDepartmentUrl}");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<PageResult<DepartmentResponse>>(result);

            data.Items.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task Get_Department_With_Not_Admin_Role_And_Fail()
        {
            await CheckStudentAuthorizeAsync();

            var getDepartmentUrl = string.Format(ClassManagementApiDef.GetPageUrl, string.Empty, _fixture.PageIndex, _fixture.PageSize, _fixture.SortOrder);

            var response = await _httpClient.GetAsync($"{_departmentUrl}?{getDepartmentUrl}");

            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task Get_Department_By_Id_Successfully()
        {
            var response = await _httpClient.GetAsync($"{_departmentUrl}/{_fixture.DepartmentId}");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            result.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Get_Department_By_Id_Not_Found_And_Fail()
        {
            var response = await _httpClient.GetAsync($"{_departmentUrl}/{_fixture.DepartmentIdNotFound}");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Get_Department_By_Id_With_Not_Admin_Role_And_Fail()
        {
            await CheckStudentAuthorizeAsync();

            var response = await _httpClient.GetAsync($"{_departmentUrl}/{_fixture.DepartmentId}");

            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task Create_Department_Successfully()
        {
            var data = TestDepartmentData.CreateDepartmentRequest();

            var response = await _httpClient.PostAsync(_departmentUrl, data.GetRequestContent());

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            result.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Create_Department_With_Id_Duplicate_And_Fail()
        {
            var data = TestDepartmentData.CreateDepartmentWithIdDuplicateRequest();

            var response = await _httpClient.PostAsync(_departmentUrl, data.GetRequestContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Department_With_Id_Empty_And_Fail()
        {
            var data = TestDepartmentData.CreateDepartmentWithIdEmptyRequest();

            var response = await _httpClient.PostAsync(_departmentUrl, data.GetRequestContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Department_With_Id_Greater_Than_Maximum_Length_And_Fail()
        {
            var data = TestDepartmentData.CreateDepartmentWithIdGreaterThanMaximumLengthRequest();

            var response = await _httpClient.PostAsync(_departmentUrl, data.GetRequestContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Department_With_Id_Contain_Special_Character_And_Fail()
        {
            var data = TestDepartmentData.CreateDepartmentWithIdContainSpecialCharacterRequest();

            var response = await _httpClient.PostAsync(_departmentUrl, data.GetRequestContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Department_With_Name_Empty_And_Fail()
        {
            var data = TestDepartmentData.CreateDepartmentWithNameEmptyRequest();

            var response = await _httpClient.PostAsync(_departmentUrl, data.GetRequestContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Department_With_Name_Greater_Than_Maximum_Length_And_Fail()
        {
            var data = TestDepartmentData.CreateDepartmentWithNameGreaterThanMaximumLengthRequest();

            var response = await _httpClient.PostAsync(_departmentUrl, data.GetRequestContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Department_With_Not_Admin_Role_And_Fail()
        {
            await CheckStudentAuthorizeAsync();

            var data = TestDepartmentData.CreateDepartmentRequest();

            var response = await _httpClient.PostAsync(_departmentUrl, data.GetRequestContent());

            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task Update_Department_Successfully()
        {
            var data = TestDepartmentData.UpdateDepartmentRequest();

            var response = await _httpClient.PutAsync($"{_departmentUrl}/{_fixture.DepartmentId}", data.GetRequestContent());

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Update_Department_With_Id_Not_Found_And_Fail()
        {
            var data = TestDepartmentData.UpdateDepartmentRequest();

            var response = await _httpClient.PutAsync($"{_departmentUrl}/{_fixture.DepartmentIdNotFound}", data.GetRequestContent());

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Update_Department_With_Name_Empty_And_Fail()
        {
            var data = TestDepartmentData.UpdateDepartmentWithNameEmptyRequest();

            var response = await _httpClient.PutAsync($"{_departmentUrl}/{_fixture.DepartmentId}", data.GetRequestContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_Department_With_Name_Greater_Than_Maximum_Length_And_Fail()
        {
            var data = TestDepartmentData.UpdateDepartmentWithNameGreaterThanMaximumLengthRequest();

            var response = await _httpClient.PostAsync(_departmentUrl, data.GetRequestContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_Department_With_Not_Admin_Role_And_Fail()
        {
            await CheckStudentAuthorizeAsync();

            var data = TestDepartmentData.UpdateDepartmentRequest();

            var response = await _httpClient.PutAsync($"{_departmentUrl}/{_fixture.DepartmentId}", data.GetRequestContent());

            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

    }
}
