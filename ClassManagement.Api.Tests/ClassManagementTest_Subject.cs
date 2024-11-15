using System.Net;
using ClassManagement.Api.DTO.Page;
using ClassManagement.Api.DTO.Subject;
using ClassManagement.Api.Tests.TestData;
using FluentAssertions;
using Newtonsoft.Json;

namespace ClassManagement.Api.Tests
{
    public class ClassManagementTest_Subject : ClassManagementTest_Base
    {
        private readonly string _subjectUrl;

        public ClassManagementTest_Subject(BaseFixture fixture) : base(fixture)
        {
            _subjectUrl = $"{_fixture.BaseUrl}/{_fixture.SubjectApi}";
        }

        [Fact]
        public async Task Get_Subjects_With_DepartmentId_And_Successfully()
        {
            var getSubjectUrl = string.Format(ClassManagementApiDef.GetSubjects, _fixture.DepartmentId, string.Empty, _fixture.PageIndex, _fixture.PageSize, _fixture.SortOrder);

            var response = await _httpClient.GetAsync($"{_subjectUrl}?{getSubjectUrl}");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<PageResult<SubjectResponse>>(result, TestCommonData.ConvertDateTime());

            data?.Items.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Get_Subjects_With_DepartmentId_Not_Found_And_Successfully()
        {
            var getSubjectUrl = string.Format(ClassManagementApiDef.GetSubjects, _fixture.DepartmentIdNotFound, string.Empty, _fixture.PageIndex, _fixture.PageSize, _fixture.SortOrder);

            var response = await _httpClient.GetAsync($"{_subjectUrl}?{getSubjectUrl}");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<PageResult<SubjectResponse>>(result, TestCommonData.ConvertDateTime());

            data?.Items.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task Get_Subjects_With_Keyword_Not_Null_And_Successfully()
        {
            var getSubjectUrl = string.Format(ClassManagementApiDef.GetSubjects, string.Empty, _fixture.SubjectKeyword, _fixture.PageIndex, _fixture.PageSize, _fixture.SortOrder);

            var response = await _httpClient.GetAsync($"{_subjectUrl}?{getSubjectUrl}");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<PageResult<SubjectResponse>>(result, TestCommonData.ConvertDateTime());

            data?.Items.Should().NotBeNullOrEmpty();

            data?.Items.Count.Should().Be(2);
        }

        [Fact]
        public async Task Get_Subjects_With_Keyword_Not_Found_And_Successfully()
        {
            var getSubjectUrl = string.Format(ClassManagementApiDef.GetSubjects, string.Empty, _fixture.SubjectKeywordNotFound, _fixture.PageIndex, _fixture.PageSize, _fixture.SortOrder);

            var response = await _httpClient.GetAsync($"{_subjectUrl}?{getSubjectUrl}"); ;

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<PageResult<SubjectResponse>>(result, TestCommonData.ConvertDateTime());

            data?.Items.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task Get_Subject_By_Id_Successfully()
        {
            var response = await _httpClient.GetAsync($"{_subjectUrl}/{_fixture.SubjectId}");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<PageResult<SubjectResponse>>(result, TestCommonData.ConvertDateTime());

            data?.Items.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task Get_Subject_By_Id_Not_Found_And_Fail()
        {
            var response = await _httpClient.GetAsync($"{_subjectUrl}/{_fixture.SubjectIdNotFound}");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Create_Subject_With_Not_Admin_Role_And_Fail()
        {
            await CheckStudentAuthorizeAsync();

            var data = TestSubjectData.CreateSubjectRequest();

            var response = await _httpClient.PostAsync(_subjectUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task Create_Subject_Successfully()
        {
            var data = TestSubjectData.CreateSubjectRequest();

            var response = await _httpClient.PostAsync(_subjectUrl, data.GetRequestMultipartFormContent());

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            result.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Create_Subject_With_Id_Duplicate_And_Fail()
        {
            var data = TestSubjectData.CreateSubjectWithIdDuplicateRequest();

            var response = await _httpClient.PostAsync(_subjectUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Subject_With_Id_Empty_And_Fail()
        {
            var data = TestSubjectData.CreateSubjectWithIdEmptyRequest();

            var response = await _httpClient.PostAsync(_subjectUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Subject_With_Name_Empty_And_Fail()
        {
            var data = TestSubjectData.CreateSubjectWithNameEmptyRequest();

            var response = await _httpClient.PostAsync(_subjectUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Subject_With_Credit_Less_Than_Minium_Value_And_Fail()
        {
            var data = TestSubjectData.CreateSubjectWithCreditLessThanMiniumValueRequest();

            var response = await _httpClient.PostAsync(_subjectUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Subject_With_Credit_Greater_Than_Maximum_Value_And_Fail()
        {
            var data = TestSubjectData.CreateSubjectWithCreditGreaterThanMaximumValueRequest();

            var response = await _httpClient.PostAsync(_subjectUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Subject_With_Status_Invalid_And_Fail()
        {
            var data = TestSubjectData.CreateSubjectWithStatusInvalidRequest();

            var response = await _httpClient.PostAsync(_subjectUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Subject_With_Department_Id_Not_Found_And_Fail()
        {
            var data = TestSubjectData.CreateSubjectWithDepartmentIdNotFoundRequest();

            var response = await _httpClient.PostAsync(_subjectUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Create_Subject_With_Id_Greater_Than_Maximum_Length_And_Fail()
        {
            var data = TestSubjectData.CreateSubjectWithIdGreaterThanMaximumLengthRequest();

            var response = await _httpClient.PostAsync(_subjectUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Subject_With_Department_Id_Greater_Than_Maximum_Length_And_Fail()
        {
            var data = TestSubjectData.CreateSubjectWithDepartmentIdGreaterThanMaximumLengthRequest();

            var response = await _httpClient.PostAsync(_subjectUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Subject_With_Id_Contain_Special_Character_And_Fail()
        {
            var data = TestSubjectData.CreateSubjectWithIdContainSpecialCharacterRequest();

            var response = await _httpClient.PostAsync(_subjectUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Subject_With_Department_Id_Contain_Special_Character_And_Fail()
        {
            var data = TestSubjectData.CreateSubjectWithDepartmentIdContainSpecialCharacterRequest();

            var response = await _httpClient.PostAsync(_subjectUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_Subject_With_Not_Admin_Role_And_Fail()
        {
            await CheckStudentAuthorizeAsync();

            var data = TestSubjectData.UpdateSubjectRequest();

            var response = await _httpClient.PutAsync($"{_subjectUrl}/{_fixture.SubjectId}", data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task Update_Subject_Successfully()
        {
            var data = TestSubjectData.UpdateSubjectRequest();

            var response = await _httpClient.PutAsync($"{_subjectUrl}/{_fixture.SubjectId}", data.GetRequestMultipartFormContent());

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Update_Subject_With_Subject_Id_Not_Found_And_Fail()
        {
            var data = TestSubjectData.UpdateSubjectRequest();

            var response = await _httpClient.PutAsync($"{_subjectUrl}/{_fixture.SubjectIdNotFound}", data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Update_Subject_With_Name_Empty_And_Fail()
        {
            var data = TestSubjectData.UpdateSubjectWithNameEmptyRequest();

            var response = await _httpClient.PutAsync($"{_subjectUrl}/{_fixture.SubjectId}", data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_Subject_With_Credit_Less_Than_Minimum_Value_And_Fail()
        {
            var data = TestSubjectData.UpdateSubjectWithCreditLessThanMinimumValueRequest();

            var response = await _httpClient.PutAsync($"{_subjectUrl}/{_fixture.SubjectId}", data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_Subject_With_Credit_Greater_Than_Maximum_Value_And_Fail()
        {
            var data = TestSubjectData.UpdateSubjectWithCreditGreaterThanMaximumValueRequest();

            var response = await _httpClient.PutAsync($"{_subjectUrl}/{_fixture.SubjectId}", data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_Subject_With_Status_Invalid_And_Fail()
        {
            var data = TestSubjectData.UpdateSubjectWithStatusInvalidRequest();

            var response = await _httpClient.PutAsync($"{_subjectUrl}/{_fixture.SubjectId}", data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_Subject_With_Type_Not_Same_Type_Class_Teaching()
        {
            var data = TestSubjectData.UpdateSubjectWithTypeNotSameTypeClassTeachingRequest();

            var response = await _httpClient.PutAsync($"{_subjectUrl}/{_fixture.SubjectId}", data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

    }
}
