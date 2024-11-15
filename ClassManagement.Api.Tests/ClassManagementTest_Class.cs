using System.Net;
using ClassManagement.Api.DTO.Class;
using ClassManagement.Api.DTO.Page;
using ClassManagement.Api.DTO.Users.Clients;
using ClassManagement.Api.Tests.TestData;
using FluentAssertions;
using Newtonsoft.Json;

namespace ClassManagement.Api.Tests
{
    public class ClassManagementTest_Class : ClassManagementTest_Base
    {
        private readonly string _classUrl;

        public ClassManagementTest_Class(BaseFixture fixture) : base(fixture)
        {
            _classUrl = $"{_fixture.BaseUrl}/{_fixture.ClassApi}";
        }

        [Fact]
        public async Task Get_Classes_By_SubjectId_And_Successfully()
        {
            var getClassesBySubjectUrl = string.Format(ClassManagementApiDef.GetClassesBySubject, _fixture.SubjectId,

                                        string.Empty, _fixture.PageIndex, _fixture.PageSize, _fixture.SortOrder);

            var response = await _httpClient.GetAsync($"{_classUrl}?{getClassesBySubjectUrl}");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<PageResult<ClassResponse>>(result, TestCommonData.ConvertDateTime());

            data.Items.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Get_Classes_By_SubjectId_With_SubjectId_Null_And_Successfully()
        {
            var getClassesBySubjectUrl = string.Format(ClassManagementApiDef.GetClassesBySubject, string.Empty, string.Empty,

                                        _fixture.PageIndex, _fixture.PageSize, _fixture.SortOrder);

            var response = await _httpClient.GetAsync($"{_classUrl}?{getClassesBySubjectUrl}");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<PageResult<ClassResponse>>(result, TestCommonData.ConvertDateTime());

            data.Items.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Get_Classes_By_SubjectId_With_SubjectId_Not_Found_And_Successfully()
        {
            var getClassesBySubjectUrl = string.Format(ClassManagementApiDef.GetClassesBySubject, _fixture.SubjectIdNotFound, string.Empty,

                                        _fixture.PageIndex, _fixture.PageSize, _fixture.SortOrder);

            var response = await _httpClient.GetAsync($"{_classUrl}?{getClassesBySubjectUrl}");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<PageResult<ClassResponse>>(result, TestCommonData.ConvertDateTime());

            data.Items.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task Get_Classes_With_Not_Admin_Role_And_Fail()
        {
            await CheckStudentAuthorizeAsync();

            var getClassesBySubjectUrl = string.Format(ClassManagementApiDef.GetClassesBySubject, _fixture.SubjectId, string.Empty,

                                        _fixture.PageIndex, _fixture.PageSize, _fixture.SortOrder);

            var response = await _httpClient.GetAsync($"{_classUrl}?{getClassesBySubjectUrl}");

            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task Get_Classes_With_Keyword_Not_Null_And_Successfully()
        {
            var getClassesBySubjectUrl = string.Format(ClassManagementApiDef.GetClassesBySubject, _fixture.SubjectId, _fixture.ClassKeyword,

                                        _fixture.PageIndex, _fixture.PageSize, _fixture.SortOrder);

            var response = await _httpClient.GetAsync($"{_classUrl}?{getClassesBySubjectUrl}");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<PageResult<ClassResponse>>(result, TestCommonData.ConvertDateTime());

            data.Items.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Get_Classes_With_Keyword_Not_Found_And_Successfully()
        {
            var getClassesBySubjectUrl = string.Format(ClassManagementApiDef.GetClassesBySubject, _fixture.SubjectId, _fixture.ClassKeywordNotFound,

                                        _fixture.PageIndex, _fixture.PageSize, _fixture.SortOrder);

            var response = await _httpClient.GetAsync($"{_classUrl}?{getClassesBySubjectUrl}");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<PageResult<ClassResponse>>(result, TestCommonData.ConvertDateTime());

            data.Items.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task Get_Class_By_Id_Successfully()
        {
            var response = await _httpClient.GetAsync($"{_classUrl}/{_fixture.ClassId}");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            result.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Get_Class_By_Id_With_Id_Not_Found_And_Fail()
        {
            var response = await _httpClient.GetAsync($"{_classUrl}/{_fixture.ClassIdNotFound}");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Get_Class_By_Id_With_Not_Admin_Role_Or_Teacher_Role_And_Fail()
        {
            await CheckStudentAuthorizeAsync();

            var response = await _httpClient.GetAsync($"{_classUrl}/{_fixture.ClassId}");

            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task Get_Classes_By_StudentId_Successfully()
        {
            var getClassesByClientUrl = string.Format(ClassManagementApiDef.GetClassesByClient, _fixture.StudentId, string.Empty,

                                        _fixture.PageIndex, _fixture.PageSize, _fixture.SortOrder);

            var response = await _httpClient.GetAsync($"{_classUrl}/{getClassesByClientUrl}");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<PageResult<ClassResponse>>(result, TestCommonData.ConvertDateTime());

            data.Items.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Get_Classes_By_StudentId_With_StudentId_Not_Found_And_Fail()
        {
            var getClassesByClientUrl = string.Format(ClassManagementApiDef.GetClassesByClient, _fixture.StudentIdNotFound, string.Empty,

                                        _fixture.PageIndex, _fixture.PageSize, _fixture.SortOrder);

            var response = await _httpClient.GetAsync($"{_classUrl}/{getClassesByClientUrl}");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Get_Classes_By_TeacherId_Successfully()
        {
            var getClassesByClientUrl = string.Format(ClassManagementApiDef.GetClassesByClient, _fixture.TeacherIdInClass, string.Empty,

                                        _fixture.PageIndex, _fixture.PageSize, _fixture.SortOrder);

            var response = await _httpClient.GetAsync($"{_classUrl}/{getClassesByClientUrl}");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<PageResult<ClassResponse>>(result, TestCommonData.ConvertDateTime());

            data.Items.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Get_Classes_By_TeacherId_With_TeacherId_Not_Found_And_Fail()
        {
            var getClassesByClientUrl = string.Format(ClassManagementApiDef.GetClassesByClient, _fixture.TeacherIdNotFound, string.Empty,

                                        _fixture.PageIndex, _fixture.PageSize, _fixture.SortOrder);

            var response = await _httpClient.GetAsync($"{_classUrl}/{getClassesByClientUrl}");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Get_Classes_By_ClientId_With_ClientId_Null_And_Fail()
        {
            var getClassesByClientUrl = string.Format(ClassManagementApiDef.GetClassesByClient, string.Empty, string.Empty,

                                        _fixture.PageIndex, _fixture.PageSize, _fixture.SortOrder);

            var response = await _httpClient.GetAsync($"{_classUrl}/{getClassesByClientUrl}");

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Get_Classes_By_TeacherId_With_Keyword_Not_Found_And_Fail()
        {
            var getClassesByClientUrl = string.Format(ClassManagementApiDef.GetClassesByClient, _fixture.TeacherId, _fixture.ClassKeywordNotFound,

                                        _fixture.PageIndex, _fixture.PageSize, _fixture.SortOrder);

            var response = await _httpClient.GetAsync($"{_classUrl}/{getClassesByClientUrl}");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<PageResult<ClassResponse>>(result, TestCommonData.ConvertDateTime());

            data.Items.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task Get_Classes_By_TeacherId_With_Keyword_Not_Null_And_Fail()
        {
            var getClassesByClientUrl = string.Format(ClassManagementApiDef.GetClassesByClient, _fixture.TeacherId, _fixture.ClassKeyword,

                                        _fixture.PageIndex, _fixture.PageSize, _fixture.SortOrder);

            var response = await _httpClient.GetAsync($"{_classUrl}/{getClassesByClientUrl}");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<PageResult<ClassResponse>>(result, TestCommonData.ConvertDateTime());

            data.Items.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Get_Classes_By_StudentId_With_Keyword_Not_Found_And_Fail()
        {
            var getClassesByClientUrl = string.Format(ClassManagementApiDef.GetClassesByClient, _fixture.StudentId, _fixture.ClassKeywordNotFound,

                                        _fixture.PageIndex, _fixture.PageSize, _fixture.SortOrder);

            var response = await _httpClient.GetAsync($"{_classUrl}/{getClassesByClientUrl}");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<PageResult<ClassResponse>>(result, TestCommonData.ConvertDateTime());

            data.Items.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task Get_Classes_By_StudentId_With_Keyword_Not_Null_And_Fail()
        {
            var getClassesByClientUrl = string.Format(ClassManagementApiDef.GetClassesByClient, _fixture.StudentId, _fixture.ClassKeyword,

                                        _fixture.PageIndex, _fixture.PageSize, _fixture.SortOrder);

            var response = await _httpClient.GetAsync($"{_classUrl}/{getClassesByClientUrl}");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<PageResult<ClassResponse>>(result, TestCommonData.ConvertDateTime());

            data.Items.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Get_Classes_By_ClientId_With_Unauthorized_And_Fail()
        {
            CheckUnauthorized();

            var getClassesByClientUrl = string.Format(ClassManagementApiDef.GetClassesByClient, _fixture.StudentId, string.Empty,

                                        _fixture.PageIndex, _fixture.PageSize, _fixture.SortOrder);

            var response = await _httpClient.GetAsync($"{_classUrl}/{getClassesByClientUrl}");

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Get_Students_Not_Exist_In_Class_And_Successfully()
        {
            var getStudentsNotExistInClassUrl = string.Format(ClassManagementApiDef.GetStudentsNotExistInClass, _fixture.ClassId, string.Empty,

                                                _fixture.PageIndex, _fixture.PageSize, _fixture.SortOrder, _fixture.IsDeleted);

            var response = await _httpClient.GetAsync($"{_classUrl}/{getStudentsNotExistInClassUrl}");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<PageResult<ClientResponse>>(result, TestCommonData.ConvertDateTime());

            data.Items.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Get_Students_Not_Exist_In_Class_With_Not_Admin_Role_Or_Teacher_Role_And_Fail()
        {
            await CheckStudentAuthorizeAsync();

            var getStudentsNotExistInClassUrl = string.Format(ClassManagementApiDef.GetStudentsNotExistInClass, _fixture.ClassId, string.Empty,

                                                _fixture.PageIndex, _fixture.PageSize, _fixture.SortOrder, _fixture.IsDeleted);

            var response = await _httpClient.GetAsync($"{_classUrl}/{getStudentsNotExistInClassUrl}");

            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task Get_Students_Not_Exist_In_Class_With_ClassId_Not_Found_And_Fail()
        {
            var getStudentsNotExistInClassUrl = string.Format(ClassManagementApiDef.GetStudentsNotExistInClass, _fixture.ClassIdNotFound, string.Empty,

                                                _fixture.PageIndex, _fixture.PageSize, _fixture.SortOrder, _fixture.IsDeleted);

            var response = await _httpClient.GetAsync($"{_classUrl}/{getStudentsNotExistInClassUrl}");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Get_Students_Not_Exist_In_Class_With_Keyword_Not_Found_And_Successfully()
        {
            var getStudentsNotExistInClassUrl = string.Format(ClassManagementApiDef.GetStudentsNotExistInClass, _fixture.ClassId, _fixture.ClassKeywordNotFound,

                                                _fixture.PageIndex, _fixture.PageSize, _fixture.SortOrder, _fixture.IsDeleted);

            var response = await _httpClient.GetAsync($"{_classUrl}/{getStudentsNotExistInClassUrl}");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<PageResult<ClientResponse>>(result, TestCommonData.ConvertDateTime());

            data.Items.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task Get_Students_Not_Exist_In_Class_With_Keyword_Not_Null_And_Successfully()
        {
            var getStudentsNotExistInClassUrl = string.Format(ClassManagementApiDef.GetStudentsNotExistInClass, _fixture.ClassId, _fixture.StudentId,

                                                _fixture.PageIndex, _fixture.PageSize, _fixture.SortOrder, _fixture.IsDeleted);

            var response = await _httpClient.GetAsync($"{_classUrl}/{getStudentsNotExistInClassUrl}");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<PageResult<ClientResponse>>(result, TestCommonData.ConvertDateTime());

            data.Items.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Create_Class_Successfully()
        {
            var data = TestClassData.CreateClassRequest();

            var response = await _httpClient.PostAsync(_classUrl, data.GetRequestMultipartFormContent());

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            result.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Create_Class_With_Not_Admin_Role_And_Fail()
        {
            await CheckTeacherAuthorizeAsync();

            var data = TestClassData.CreateClassRequest();

            var response = await _httpClient.PostAsync(_classUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task Create_Class_With_Id_Empty_And_Fail()
        {
            var data = TestClassData.CreateClassWithIdEmptyRequest();

            var x = data.GetRequestMultipartFormContent();

            var response = await _httpClient.PostAsync(_classUrl, x);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Class_With_Id_Duplicate_And_Fail()
        {
            var data = TestClassData.CreateClassWithIdDuplicateRequest();

            var response = await _httpClient.PostAsync(_classUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Class_With_Id_Greater_Than_Maximum_Length_And_Fail()
        {
            var data = TestClassData.CreateClassWithIdGreaterThanMaximumLengthRequest();

            var response = await _httpClient.PostAsync(_classUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Class_With_Id_Contain_Special_Character_And_Fail()
        {
            var data = TestClassData.CreateClassWithIdContainSpecialCharacterRequest();

            var response = await _httpClient.PostAsync(_classUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Class_With_Name_Empty_And_Fail()
        {
            var data = TestClassData.CreateClassWithNameEmptyRequest();

            var response = await _httpClient.PostAsync(_classUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Class_With_Name_Greater_Than_Maximum_Length_And_Fail()
        {
            var data = TestClassData.CreateClassWithNameGreaterThanMaximumLengthRequest();

            var response = await _httpClient.PostAsync(_classUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Class_With_ClassSize_Empty_And_Fail()
        {
            var data = TestClassData.CreateClassWithClassSizeEmptyRequest();

            var response = await _httpClient.PostAsync(_classUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Class_With_ClassSize_Greater_Than_Maximum_Value_And_Fail()
        {
            var data = TestClassData.CreateClassWithClassSizeGreaterThanMaximumValueRequest();

            var response = await _httpClient.PostAsync(_classUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Class_With_ClassSize_Less_Than_Minimum_Value_And_Fail()
        {
            var data = TestClassData.CreateClassWithClassSizeLessThanMinimumValueRequest();

            var response = await _httpClient.PostAsync(_classUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Class_With_UserId_Empty_And_Fail()
        {
            var data = TestClassData.CreateClassWithUserIdEmptyRequest();

            var response = await _httpClient.PostAsync(_classUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Class_With_UserId_Not_Found_And_Fail()
        {
            var data = TestClassData.CreateClassWithUserIdNotFoundRequest();

            var x = data.GetRequestMultipartFormContent();

            var response = await _httpClient.PostAsync(_classUrl, x);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Create_Class_With_UserId_Not_TeacherId_And_Fail()
        {
            var data = TestClassData.CreateClassWithUserIdNotTeacherIdRequest();

            var response = await _httpClient.PostAsync(_classUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Class_With_User_And_Subject_Not_In_The_Same_Department_And_Fail()
        {
            var data = TestClassData.CreateClassWithUserAndSubjectNotInTheSameDepartmentRequest();

            var response = await _httpClient.PostAsync(_classUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Class_With_SubjectId_Empty_And_Fail()
        {
            var data = TestClassData.CreateClassWithSubjectIdEmptyRequest();

            var response = await _httpClient.PostAsync(_classUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Class_With_SubjectId_Not_Found_And_Fail()
        {
            var data = TestClassData.CreateClassWithSubjectIdNotFoundRequest();

            var response = await _httpClient.PostAsync(_classUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Create_Class_With_Subject_Closed_And_Fail()
        {
            var data = TestClassData.CreateClassWithSubjectClosedRequest();

            var response = await _httpClient.PostAsync(_classUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Create_Class_With_SubjectId_Greater_Than_Maximum_Length_And_Fail()
        {
            var data = TestClassData.CreateClassWithSubjectIdGreaterThanMaximumLengthRequest();

            var response = await _httpClient.PostAsync(_classUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Class_With_SubjectId_Contain_Special_Character_And_Fail()
        {
            var data = TestClassData.CreateClassWithSubjectIdContainSpecialCharacterRequest();

            var response = await _httpClient.PostAsync(_classUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Class_With_SemesterId_Empty_And_Fail()
        {
            var data = TestClassData.CreateClassWithSemesterIdEmptyRequest();

            var response = await _httpClient.PostAsync(_classUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Class_With_SemesterId_Not_Found_And_Fail()
        {
            var data = TestClassData.CreateClassWithSemesterIdNotFoundRequest();

            var response = await _httpClient.PostAsync(_classUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Create_Class_With_SemesterId_Greater_Than_Maximum_Length_And_Fail()
        {
            var data = TestClassData.CreateClassWithSemesterIdGreaterThanMaximumLengthRequest();

            var response = await _httpClient.PostAsync(_classUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Class_With_SemesterId_Contain_Special_Character_And_Fail()
        {
            var data = TestClassData.CreateClassWithSemesterIdContainSpecialCharacterRequest();

            var response = await _httpClient.PostAsync(_classUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Class_With_ClassType_Invalid_And_Fail()
        {
            var data = TestClassData.CreateClassWithTypeInvalidRequest();

            var response = await _httpClient.PostAsync(_classUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Class_With_ClassType_And_SubjectType_Not_Match_And_Fail()
        {
            var data = TestClassData.CreateClassWithClassTypeAndSubjectTypeNotMatchRequest();

            var response = await _httpClient.PostAsync(_classUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Class_With_StartAt_Empty_And_Fail()
        {
            var data = TestClassData.CreateClassWithStartedAtEmptyRequest();

            var response = await _httpClient.PostAsync(_classUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Class_With_EndAt_Empty_And_Fail()
        {
            var data = TestClassData.CreateClassWithEndedAtEmptyRequest();

            var response = await _httpClient.PostAsync(_classUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Class_With_StartedAt_Greater_Than_EndedAt_And_Fail()
        {
            var data = TestClassData.CreateClassWithStartedAtGreaterThanEndedAtRequest();

            var response = await _httpClient.PostAsync(_classUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Class_With_DayOfWeek_Invalid_And_Fail()
        {
            var data = TestClassData.CreateClassWithDayOfWeekInvalidRequest();

            var response = await _httpClient.PostAsync(_classUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Class_With_ClassPeriods_Invalid_And_Fail()
        {
            var data = TestClassData.CreateClassWithClassPeriodsInvalidRequest();

            var response = await _httpClient.PostAsync(_classUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_Class_Successfully()
        {
            var data = TestClassData.UpdateClassRequest();

            var response = await _httpClient.PutAsync($"{_classUrl}/{_fixture.ClassId}", data.GetRequestMultipartFormContent());

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Update_Class_With_ClassId_Not_Found_And_Fail()
        {
            var data = TestClassData.UpdateClassRequest();

            var response = await _httpClient.PutAsync($"{_classUrl}/{_fixture.ClassIdNotFound}", data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Update_Class_With_Not_Admin_Role_And_Fail()
        {
            await CheckStudentAuthorizeAsync();

            var data = TestClassData.UpdateClassRequest();

            var response = await _httpClient.PutAsync($"{_classUrl}/{_fixture.ClassId}", data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task Update_Class_With_Name_Empty_And_Fail()
        {
            var data = TestClassData.UpdateClassWithNameEmptyRequest();

            var response = await _httpClient.PutAsync($"{_classUrl}/{_fixture.ClassId}", data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_Class_With_Name_Greater_Than_Maximum_Length_And_Fail()
        {
            var data = TestClassData.UpdateClassWithNameGreaterThanMaximumLengthRequest();

            var response = await _httpClient.PutAsync($"{_classUrl}/{_fixture.ClassId}", data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_Class_With_ClassSize_Empty_And_Fail()
        {
            var data = TestClassData.UpdateClassWithClassSizeEmptyRequest();

            var response = await _httpClient.PutAsync($"{_classUrl}/{_fixture.ClassId}", data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_Class_With_ClassSize_Less_Than_Minimum_Value_And_Fail()
        {
            var data = TestClassData.UpdateClassWithClassSizeLessThanMinimumValueRequest();

            var response = await _httpClient.PutAsync($"{_classUrl}/{_fixture.ClassId}", data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_Class_With_ClassSize_Greater_Than_Maximum_Value_And_Fail()
        {
            var data = TestClassData.UpdateClassWithClassSizeGreaterThanMaximumValueRequest();

            var response = await _httpClient.PutAsync($"{_classUrl}/{_fixture.ClassId}", data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_Class_With_UserId_Empty_And_Fail()
        {
            var data = TestClassData.UpdateClassWithUserIdEmptyRequest();

            var response = await _httpClient.PutAsync($"{_classUrl}/{_fixture.ClassId}", data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_Class_With_UserId_Not_Found_And_Fail()
        {
            var data = TestClassData.UpdateClassWithUserIdNotFoundRequest();

            var response = await _httpClient.PutAsync($"{_classUrl}/{_fixture.ClassId}", data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Update_Class_With_StartAt_Empty_And_Fail()
        {
            var data = TestClassData.UpdateClassWithStartedAtEmptyRequest();

            var response = await _httpClient.PutAsync($"{_classUrl}/{_fixture.ClassId}", data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_Class_With_EndAt_Empty_And_Fail()
        {
            var data = TestClassData.UpdateClassWithEndedAtEmptyRequest();

            var response = await _httpClient.PutAsync($"{_classUrl}/{_fixture.ClassId}", data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_Class_With_UserId_Not_TeacherId_And_Fail()
        {
            var data = TestClassData.UpdateClassWithUserIdNotTeacherIdRequest();

            var response = await _httpClient.PutAsync($"{_classUrl}/{_fixture.ClassId}", data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_Class_With_User_And_Subject_Not_In_The_Same_Department_And_Fail()
        {
            var data = TestClassData.UpdateClassWithUserAndSubjectNotInTheSameDepartmentRequest();

            var response = await _httpClient.PutAsync($"{_classUrl}/{_fixture.ClassId}", data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_Class_With_StartedAt_Greater_Than_EndedAt_And_Fail()
        {
            var data = TestClassData.UpdateClassWithStartedAtGreaterThanEndedAtRequest();

            var response = await _httpClient.PutAsync($"{_classUrl}/{_fixture.ClassId}", data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Add_Students_To_Class_Successfully()
        {
            var data = TestClassData.AddStudentsToClassRequest();

            var response = await _httpClient.PutAsync($"{_classUrl}/{_fixture.GetAddStudentsToClassUrl}/{_fixture.ClassId}", data.GetRequestContent());

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Add_Students_To_Class_With_Not_Admin_Role_Or_Teacher_Role_And_Fail()
        {
            await CheckStudentAuthorizeAsync();

            var data = TestClassData.AddStudentsToClassRequest();

            var response = await _httpClient.PutAsync($"{_classUrl}/{_fixture.GetAddStudentsToClassUrl}/{_fixture.ClassId}", data.GetRequestContent());

            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task Add_Students_To_Class_With_List_Null_And_Fail()
        {
            var response = await _httpClient.PutAsync($"{_classUrl}/{_fixture.GetAddStudentsToClassUrl}/{_fixture.ClassId}", new MultipartFormDataContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Add_Students_To_Class_With_ClassId_Not_Found_And_Fail()
        {
            var data = TestClassData.AddStudentsToClassRequest();

            var response = await _httpClient.PutAsync($"{_classUrl}/{_fixture.GetAddStudentsToClassUrl}/{_fixture.ClassIdNotFound}", data.GetRequestContent());

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Add_Students_To_Class_With_Class_Full_And_Fail()
        {
            var data = TestClassData.AddStudentsToClassRequest();

            var response = await _httpClient.PutAsync($"{_classUrl}/{_fixture.GetAddStudentsToClassUrl}/{_fixture.FullClassId}", data.GetRequestContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Add_Students_To_Class_With_Invalid_Amount_Class_And_Fail()
        {
            var data = TestClassData.AddStudentsToClassWithInvalidAmountClassRequest();

            var response = await _httpClient.PutAsync($"{_classUrl}/{_fixture.GetAddStudentsToClassUrl}/{_fixture.InvalidAmoutClassId}", data.GetRequestContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Add_Students_To_Class_With_List_Length_Invalid_And_Fail()
        {
            var data = TestClassData.AddStudentsToClassWithListInvalidRequest();

            var response = await _httpClient.PutAsync($"{_classUrl}/{_fixture.GetAddStudentsToClassUrl}/{_fixture.ClassId}", data.GetRequestContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Add_Students_To_Class_With_Student_Not_Found_And_Fail()
        {
            var data = TestClassData.AddStudentsToClassWithStudentIdNotFoundRequest();

            var response = await _httpClient.PutAsync($"{_classUrl}/{_fixture.GetAddStudentsToClassUrl}/{_fixture.ClassId}", data.GetRequestContent());

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Add_Students_To_Class_With_StudentId_As_TeacherId_And_Fail()
        {
            var data = TestClassData.AddStudentsToClassWithStudentIdInvalidRequest();

            var response = await _httpClient.PutAsync($"{_classUrl}/{_fixture.GetAddStudentsToClassUrl}/{_fixture.ClassId}", data.GetRequestContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Add_Students_To_Class_With_Existing_StudentId_And_Fail()
        {
            var data = TestClassData.AddStudentsToClassWithStudentIdWasInClassRequest();

            var response = await _httpClient.PutAsync($"{_classUrl}/{_fixture.GetAddStudentsToClassUrl}/{_fixture.ClassId}", data.GetRequestContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Add_Students_To_Class_With_Practical_Class_But_Not_Theory_Class_And_Fail()
        {
            var data = TestClassData.AddStudentsToClassWithNotFoundTheoryClassRequest();

            var response = await _httpClient.PutAsync($"{_classUrl}/{_fixture.GetAddStudentsToClassUrl}/{_fixture.PracticeClassId}", data.GetRequestContent());

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
