using ClassManagement.Api.DTO.Page;
using ClassManagement.Api.DTO.Semester;
using ClassManagement.Api.Tests.TestData;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net;

namespace ClassManagement.Api.Tests
{
    public class ClassManagementTest_Semester : ClassManagementTest_Base
    {
        private readonly string _semesterUrl;

        public ClassManagementTest_Semester(BaseFixture fixture) : base(fixture)
        {
            _semesterUrl = $"{_fixture.BaseUrl}/{_fixture.SemesterApi}";
        }

        [Fact]
        public async Task Get_Semesters_Successfully()
        {
            var getSemesterUrl = string.Format(ClassManagementApiDef.GetPageUrl, string.Empty, _fixture.PageIndex, _fixture.PageSize, _fixture.SortOrder);

            var response = await _httpClient.GetAsync($"{_semesterUrl}?{getSemesterUrl}");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<PageResult<SemesterResponse>>(result, TestCommonData.ConvertDateTime());

            data.Items.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Get_Semesters_With_Not_Admin_Role_And_Fail()
        {
            await CheckStudentAuthorizeAsync();

            var getSemesterUrl = string.Format(ClassManagementApiDef.GetPageUrl, string.Empty, _fixture.PageIndex, _fixture.PageSize, _fixture.SortOrder);

            var response = await _httpClient.GetAsync($"{_semesterUrl}?{getSemesterUrl}");

            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task Get_Semesters_With_Keyword_Not_Null_And_Successfully()
        {
            var getSemesterUrl = string.Format(ClassManagementApiDef.GetPageUrl, _fixture.SemesterKeyword, _fixture.PageIndex, _fixture.PageSize, _fixture.SortOrder);

            var response = await _httpClient.GetAsync($"{_semesterUrl}?{getSemesterUrl}");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<PageResult<SemesterResponse>>(result, TestCommonData.ConvertDateTime());

            data.Items.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Get_Semesters_With_Keyword_Not_Found_And_Successfully()
        {
            var getSemesterUrl = string.Format(ClassManagementApiDef.GetPageUrl, _fixture.SemesterKeywordNotFound, _fixture.PageIndex, _fixture.PageSize, _fixture.SortOrder);

            var response = await _httpClient.GetAsync($"{_semesterUrl}?{getSemesterUrl}");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<PageResult<SemesterResponse>>(result, TestCommonData.ConvertDateTime());

            data.Items.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task Get_Semester_By_Id_Successfully()
        {
            var response = await _httpClient.GetAsync($"{_semesterUrl}/{_fixture.SemesterId}");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            result.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Get_Semester_With_Id_Not_Found_And_Fail()
        {
            var response = await _httpClient.GetAsync($"{_semesterUrl}/{_fixture.SemesterIdNotFound}");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Get_Semester_By_Id_With_Not_Admin_Role_And_Fail()
        {
            await CheckStudentAuthorizeAsync();

            var response = await _httpClient.GetAsync($"{_semesterUrl}/{_fixture.SemesterId}");

            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task Create_Semesters_Successfully()
        {
            var data = TestSemesterData.CreateSemesterRequest();

            var response = await _httpClient.PostAsync(_semesterUrl, data.GetRequestContent());

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            result.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Create_Semester_With_Not_Admin_Role_And_Fail()
        {
            await CheckStudentAuthorizeAsync();

            var data = TestSemesterData.CreateSemesterRequest();

            var response = await _httpClient.PostAsync(_semesterUrl, data.GetRequestContent());

            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task Create_Semester_With_Id_Empty_And_Fail()
        {
            var data = TestSemesterData.CreateSemesterWithIdEmptyRequest();

            var response = await _httpClient.PostAsync(_semesterUrl, data.GetRequestContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Semester_With_Id_Duplicate_And_Fail()
        {
            var data = TestSemesterData.CreateSemesterWithIdDuplicateRequest();

            var response = await _httpClient.PostAsync(_semesterUrl, data.GetRequestContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Semester_With_Id_Greater_Than_Maximum_Length_And_Fail()
        {
            var data = TestSemesterData.CreateSemesterWithIdGreaterThanMaximumLengthRequest();

            var response = await _httpClient.PostAsync(_semesterUrl, data.GetRequestContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Semester_With_Id_Contain_Special_Character_And_Fail()
        {
            var data = TestSemesterData.CreateSemesterWithIdContainSpecialCharacterRequest();

            var response = await _httpClient.PostAsync(_semesterUrl, data.GetRequestContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Semester_With_Name_Empty_And_Fail()
        {
            var data = TestSemesterData.CreateSemesterWithNameEmptyRequest();

            var response = await _httpClient.PostAsync(_semesterUrl, data.GetRequestContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Semester_With_Name_Greater_Than_Maximum_Length_And_Fail()
        {
            var data = TestSemesterData.CreateSemesterWithNameGreaterThanMaximumLenghtRequest();

            var response = await _httpClient.PostAsync(_semesterUrl, data.GetRequestContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_Semesters_Successfully()
        {
            var data = TestSemesterData.UpdateSemesterRequest();

            var response = await _httpClient.PutAsync($"{_semesterUrl}/{_fixture.SemesterId}", data.GetRequestContent());

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Update_Semester_With_Id_Not_Found_And_Fail()
        {
            var data = TestSemesterData.UpdateSemesterRequest();

            var response = await _httpClient.PutAsync($"{_semesterUrl}/{_fixture.SemesterIdNotFound}", data.GetRequestContent());

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Update_Semester_With_Not_Admin_Role_And_Fail()
        {
            await CheckStudentAuthorizeAsync();

            var data = TestSemesterData.UpdateSemesterRequest();

            var response = await _httpClient.PutAsync($"{_semesterUrl}/{_fixture.SemesterIdNotFound}", data.GetRequestContent());

            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task Update_Semester_With_Name_Empty_And_Fail()
        {
            var data = TestSemesterData.UpdateSemesterWithNameEmptyRequest();

            var response = await _httpClient.PutAsync($"{_semesterUrl}/{_fixture.SemesterIdNotFound}", data.GetRequestContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_Semester_With_Name_Greater_Than_Maximum_Length_And_Fail()
        {
            var data = TestSemesterData.UpdateSemesterWithNameGreaterThanMaximumLengthRequest();

            var response = await _httpClient.PutAsync($"{_semesterUrl}/{_fixture.SemesterIdNotFound}", data.GetRequestContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

    }
}
