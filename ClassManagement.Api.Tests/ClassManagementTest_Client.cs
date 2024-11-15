using System.Net;
using ClassManagement.Api.DTO.Page;
using ClassManagement.Api.DTO.Users.Clients;
using ClassManagement.Api.Tests.TestData;
using FluentAssertions;
using Newtonsoft.Json;

namespace ClassManagement.Api.Tests
{
    public class ClassManagementTest_Client : ClassManagementTest_Base
    {
        private readonly string _clientUrl;

        public ClassManagementTest_Client(BaseFixture fixture) : base(fixture)
        {
            _clientUrl = $"{_fixture.BaseUrl}/{_fixture.ClientApi}";
        }

        [Fact]
        public async Task Get_Clients_By_Role_With_Not_Admin_Role_And_Fail()
        {
            await CheckStudentAuthorizeAsync();

            var getClientsByRole = string.Format(ClassManagementApiDef.GetClientsByRole, _fixture.Rolename, string.Empty,

                                _fixture.PageIndex, _fixture.PageSize, _fixture.SortOrder, _fixture.IsDisabled);

            var response = await _httpClient.GetAsync($"{_clientUrl}/{getClientsByRole}");

            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task Get_Clients_By_Role_And_Successfully()
        {
            var getClientsByRole = string.Format(ClassManagementApiDef.GetClientsByRole, _fixture.Rolename, string.Empty,

                                _fixture.PageIndex, _fixture.PageSize, _fixture.SortOrder, _fixture.IsDisabled);

            var response = await _httpClient.GetAsync($"{_clientUrl}/{getClientsByRole}");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<PageResult<ClientResponse>>(result, TestCommonData.ConvertDateTime());

            data.Items.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Get_Clients_By_Role_With_Rolename_Null_And_Successfully()
        {
            var getClientsByRole = string.Format(ClassManagementApiDef.GetClientsByRole, string.Empty, string.Empty,

                                _fixture.PageIndex, _fixture.PageSize, _fixture.SortOrder, _fixture.IsDisabled);

            var response = await _httpClient.GetAsync($"{_clientUrl}/{getClientsByRole}");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<PageResult<ClientResponse>>(result, TestCommonData.ConvertDateTime());

            data.Items.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Get_Clients_By_Role_With_Rolename_Not_Found_And_Successfully()
        {
            var getClientsByRole = string.Format(ClassManagementApiDef.GetClientsByRole, _fixture.RolenameNotFound, string.Empty,

                                _fixture.PageIndex, _fixture.PageSize, _fixture.SortOrder, _fixture.IsDisabled);

            var response = await _httpClient.GetAsync($"{_clientUrl}/{getClientsByRole}");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<PageResult<ClientResponse>>(result);

            data.Items.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task Get_Clients_By_Role_With_Keyword_Not_Null_And_Successfully()
        {
            var getClientsByRole = string.Format(ClassManagementApiDef.GetClientsByRole, _fixture.Rolename, _fixture.GetClientsByRoleWithKeyword,

                                _fixture.PageIndex, _fixture.PageSize, _fixture.SortOrder, _fixture.IsDisabled);

            var response = await _httpClient.GetAsync($"{_clientUrl}/{getClientsByRole}");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<PageResult<ClientResponse>>(result, TestCommonData.ConvertDateTime());

            data.Items.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Get_Clients_By_Role_With_Keyword_Not_Found_And_Successfully()
        {
            var getClientsByRole = string.Format(ClassManagementApiDef.GetClientsByRole, _fixture.Rolename, _fixture.GetClientsByRoleWithKeywordNotFound,

                                _fixture.PageIndex, _fixture.PageSize, _fixture.SortOrder, _fixture.IsDisabled);

            var response = await _httpClient.GetAsync($"{_clientUrl}/{getClientsByRole}");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<PageResult<ClientResponse>>(result);

            data.Items.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task Get_Clients_By_Role_With_Client_Disabled_And_Successfully()
        {
            var getClientsByRole = string.Format(ClassManagementApiDef.GetClientsByRole, _fixture.Rolename, string.Empty, _fixture.PageIndex,

                                _fixture.PageSize, _fixture.SortOrder, true);

            var response = await _httpClient.GetAsync($"{_clientUrl}/{getClientsByRole}");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<PageResult<ClientResponse>>(result, TestCommonData.ConvertDateTime());

            data.Items.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Get_Clients_By_ClassId_With_Not_Teacher_Role_Or_Not_Admin_Role_And_Fail()
        {
            await CheckStudentAuthorizeAsync();

            var getClientsByClassUrl = string.Format(ClassManagementApiDef.GetClientsByClass, _fixture.ClassId, string.Empty,

                                    _fixture.PageIndex, _fixture.PageSize, _fixture.SortOrder);

            var response = await _httpClient.GetAsync($"{_clientUrl}/{getClientsByClassUrl}");

            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task Get_Clients_By_ClassId_With_ClassId_Not_Found_And_Successfully()
        {
            var getClientsByClassUrl = string.Format(ClassManagementApiDef.GetClientsByClass, _fixture.ClassIdNotFound, string.Empty,

                                    _fixture.PageIndex, _fixture.PageSize, _fixture.SortOrder);

            var response = await _httpClient.GetAsync($"{_clientUrl}/{getClientsByClassUrl}");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Get_Clients_By_ClassId_With_Admin_Role_And_Successfully()
        {
            var getClientsByClassUrl = string.Format(ClassManagementApiDef.GetClientsByClass, _fixture.ClassId, string.Empty,

                                    _fixture.PageIndex, _fixture.PageSize, _fixture.SortOrder);

            var response = await _httpClient.GetAsync($"{_clientUrl}/{getClientsByClassUrl}");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<PageResult<ClientResponse>>(result, TestCommonData.ConvertDateTime());

            data.Items.Should().NotBeNullOrEmpty();
        }

        //[Fact]
        //public async Task Get()
        //{
        //    var response = await _httpClient.GetAsync("https://localhost:7209/api/clients/get-clients?pageIndex=1&pageSize=5");

        //    response.EnsureSuccessStatusCode();

        //    var result = await response.Content.ReadAsStringAsync();

        //    var data = JsonConvert.DeserializeObject<PageResult<ClientResponse>>(result, _fixture.ConvertDateTime());

        //    data.Items.Should().NotBeNullOrEmpty();
        //}

        [Fact]
        public async Task Get_Clients_By_ClassId_With_ClassId_Null_And_Successfully()
        {
            var getClientsByClassUrl = string.Format(ClassManagementApiDef.GetClientsByClass, string.Empty, string.Empty,

                                    _fixture.PageIndex, _fixture.PageSize, _fixture.SortOrder);

            var response = await _httpClient.GetAsync($"{_clientUrl}/{getClientsByClassUrl}");

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Get_Clients_By_ClassId_With_Keyword_Not_Null_And_Successfully()
        {
            var getClientsByClassUrl = string.Format(ClassManagementApiDef.GetClientsByClass, _fixture.ClassId, _fixture.GetClientsByClassWithKeyword,

                                    _fixture.PageIndex, _fixture.PageSize, _fixture.SortOrder);

            var response = await _httpClient.GetAsync($"{_clientUrl}/{getClientsByClassUrl}");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<PageResult<ClientResponse>>(result, TestCommonData.ConvertDateTime());

            data.Items.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Get_Clients_By_ClassId_With_Keyword_Not_Found_And_Successfully()
        {
            var getClientsByClassUrl = string.Format(ClassManagementApiDef.GetClientsByClass, _fixture.ClassId, _fixture.GetClientsByClassWithKeywordNotFound,

                                    _fixture.PageIndex, _fixture.PageSize, _fixture.SortOrder);

            var response = await _httpClient.GetAsync($"{_clientUrl}/{getClientsByClassUrl}");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<PageResult<ClientResponse>>(result);

            data.Items.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task Get_Client_By_Id_Successfully()
        {
            var response = await _httpClient.GetAsync($"{_clientUrl}/{_fixture.ClientId}");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            result.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Get_Client_By_Id_Not_Found_And_Fail()
        {
            var response = await _httpClient.GetAsync($"{_clientUrl}/{_fixture.ClientIdNotFound}");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Create_Client_Successfully()
        {
            var data = TestClientData.CreateClientRequest();

            var response = await _httpClient.PostAsync(_clientUrl, data.GetRequestMultipartFormContent());

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            result.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Create_Client_With_Not_Admin_Role_And_Fail()
        {
            await CheckStudentAuthorizeAsync();

            var data = TestClientData.CreateClientRequest();

            var response = await _httpClient.PostAsync(_clientUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task Create_Client_With_Username_Empty_And_Fail()
        {
            var data = TestClientData.CreateClientWithUsernameEmptyRequest();

            var response = await _httpClient.PostAsync(_clientUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Client_With_Username_Less_Than_Minimum_Length_And_Fail()
        {
            var data = TestClientData.CreateClientWithUsernameLessThanMinimumLengthRequest();

            var response = await _httpClient.PostAsync(_clientUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Client_With_Username_Greater_Than_Maximum_Length_And_Fail()
        {
            var data = TestClientData.CreateClientWithUsernameGreaterThanMaximumLengthRequest();

            var response = await _httpClient.PostAsync(_clientUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Client_With_Username_Contain_Special_Character_And_Fail()
        {
            var data = TestClientData.CreateClientWithUsernameContainSpecialCharacterRequest();

            var response = await _httpClient.PostAsync(_clientUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Client_With_Username_Duplicate_And_Fail()
        {
            var data = TestClientData.CreateClientWithUsernameDuplicateRequest();

            var response = await _httpClient.PostAsync(_clientUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Client_With_Password_Empty_And_Fail()
        {
            var data = TestClientData.CreateClientWithPasswordEmptyRequest();

            var response = await _httpClient.PostAsync(_clientUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Client_With_Password_Less_Than_Minimum_Length_And_Fail()
        {
            var data = TestClientData.CreateClientWithPasswordLessThanMinimumLengthRequest();

            var response = await _httpClient.PostAsync(_clientUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Client_With_Password_Greater_Than_Maximum_Length_And_Fail()
        {
            var data = TestClientData.CreateClientWithPasswordGreaterThanMaximumLengthRequest();

            var response = await _httpClient.PostAsync(_clientUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Client_With_Confirm_Password_Not_Match_And_Fail()
        {
            var data = TestClientData.CreateClientWithPasswordNotMatchRequest();

            var response = await _httpClient.PostAsync(_clientUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Client_With_Firstname_Empty_And_Fail()
        {
            var data = TestClientData.CreateClientWithFirstnameEmptyRequest();

            var response = await _httpClient.PostAsync(_clientUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Client_With_Firstname_Less_Than_Minimum_Length_And_Fail()
        {
            var data = TestClientData.CreateClientWithFirstnameLessThanMinimumLengthRequest();

            var response = await _httpClient.PostAsync(_clientUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Client_With_Firstname_Greater_Than_Maximum_Length_And_Fail()
        {
            var data = TestClientData.CreateClientWithFirstnameGreaterThanMaximumLengthRequest();

            var response = await _httpClient.PostAsync(_clientUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Client_With_Firstname_Contain_Special_Character_And_Fail()
        {
            var data = TestClientData.CreateClientWithFirstnameContainSpecialCharacterRequest();

            var response = await _httpClient.PostAsync(_clientUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Client_With_Lastname_Empty_And_Fail()
        {
            var data = TestClientData.CreateClientWithLastnameEmptyRequest();

            var response = await _httpClient.PostAsync(_clientUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Client_With_Lastname_Less_Than_Minimum_Length_And_Fail()
        {
            var data = TestClientData.CreateClientWithLastnameLessThanMinimumLengthRequest();

            var response = await _httpClient.PostAsync(_clientUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Client_With_Lastname_Greater_Than_Maximum_Length_And_Fail()
        {
            var data = TestClientData.CreateClientWithLastnameGreaterThanMaximumLengthRequest();

            var response = await _httpClient.PostAsync(_clientUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Client_With_Lastname_Contain_Special_Character_And_Fail()
        {
            var data = TestClientData.CreateClientWithLastnameContainSpecialCharacterRequest();

            var response = await _httpClient.PostAsync(_clientUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Client_With_Email_Empty_And_Fail()
        {
            var data = TestClientData.CreateClientWithEmailEmptyRequest();

            var response = await _httpClient.PostAsync(_clientUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Client_With_Email_Invalid_And_Fail()
        {
            var data = TestClientData.CreateClientWithEmailInvalidRequest();

            var response = await _httpClient.PostAsync(_clientUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Client_With_Email_Duplicate_And_Fail()
        {
            var data = TestClientData.CreateClientWithEmailDuplicateRequest();

            var response = await _httpClient.PostAsync(_clientUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Client_With_Andress_Empty_And_Fail()
        {
            var data = TestClientData.CreateClientWithAddressEmptyRequest();

            var response = await _httpClient.PostAsync(_clientUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Client_With_Andress_Greater_Than_Maximum_Length_And_Fail()
        {
            var data = TestClientData.CreateClientWithAddressGreaterThanMaximumLengthRequest();

            var response = await _httpClient.PostAsync(_clientUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Client_With_Gender_Invalid_And_Fail()
        {
            var data = TestClientData.CreateClientWithGenderInvalidRequest();

            var response = await _httpClient.PostAsync(_clientUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Client_With_Date_Of_Birth_Empty_And_Fail()
        {
            var data = TestClientData.CreateClientWithDateOfBirthEmptyRequest();

            var response = await _httpClient.PostAsync(_clientUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Client_With_Date_Of_Birth_Less_Than_Minimum_Date_And_Fail()
        {
            var data = TestClientData.CreateClientWithDateOfBirthLessThanMinimumDateRequest();

            var response = await _httpClient.PostAsync(_clientUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Client_With_Date_Of_Birth_Greater_Than_Maximum_Date_And_Fail()
        {
            var data = TestClientData.CreateClientWithDateOfBirthGreaterThanMaximumDateRequest();

            var response = await _httpClient.PostAsync(_clientUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Client_With_Department_Id_Empty_And_Fail()
        {
            var data = TestClientData.CreateClientWithDepartmentIdEmptyRequest();

            var response = await _httpClient.PostAsync(_clientUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Client_With_Department_Id_Greater_Than_Maximum_Length_And_Fail()
        {
            var data = TestClientData.CreateClientWithDepartmentIdGreaterThanMaximumLengthRequest();

            var response = await _httpClient.PostAsync(_clientUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Client_With_Departmen_Id_Contain_Special_Character_And_Fail()
        {
            var data = TestClientData.CreateClientWithDepartmentIdContainSpecialCharacterRequest();

            var response = await _httpClient.PostAsync(_clientUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Client_With_Department_Id_Not_Found_And_Fail()
        {
            var data = TestClientData.CreateClientWithDepartmentIdNotFoundRequest();

            var response = await _httpClient.PostAsync(_clientUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Client_With_Rolenamae_Not_Found_And_Fail()
        {
            var data = TestClientData.CreateClientWithRolenameNotFoundRequest();

            var response = await _httpClient.PostAsync(_clientUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Client_With_Rolename_Empty_And_Fail()
        {
            var data = TestClientData.CreateClientWithRolenameEmptyRequest();

            var response = await _httpClient.PostAsync(_clientUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Client_With_Rolename_Less_Than_Minimum_Length_And_Fail()
        {
            var data = TestClientData.CreateClientWithRolenameLassThanMinimumLengthRequest();

            var response = await _httpClient.PostAsync(_clientUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Client_With_Rolename_Greater_Than_Maximum_Length_And_Fail()
        {
            var data = TestClientData.CreateClientWithRolenameGreaterThanMaximumLengthRequest();

            var response = await _httpClient.PostAsync(_clientUrl, data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_Client_Successfully()
        {
            var data = TestClientData.UpdateClientRequest();

            var response = await _httpClient.PutAsync($"{_clientUrl}/{_fixture.ClientId}", data.GetRequestMultipartFormContent());

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Update_Client_With_Id_Not_Found_And_Fail()
        {
            var data = TestClientData.UpdateClientRequest();

            var response = await _httpClient.PutAsync($"{_clientUrl}/{_fixture.ClientIdNotFound}", data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Update_Clien_With_Firstname_Empty_And_Fail()
        {
            var data = TestClientData.UpdateClientWithFirstnameEmptyRequest();

            var response = await _httpClient.PutAsync($"{_clientUrl}/{_fixture.ClientId}", data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_Client_With_Firstname_Less_Than_Minimum_Length_And_Fail()
        {
            var data = TestClientData.UpdateClientWithFirstnameLessThanMinimumLengthRequest();

            var response = await _httpClient.PutAsync($"{_clientUrl}/{_fixture.ClientId}", data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_Client_With_Firstname_Greater_Than_Maximum_Length_And_Fail()
        {
            var data = TestClientData.UpdateClientWithFirstnameGreaterThanMaximumLengthRequest();

            var response = await _httpClient.PutAsync($"{_clientUrl}/{_fixture.ClientId}", data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_Client_With_Firstname_Contain_Special_Character_And_Fail()
        {
            var data = TestClientData.UpdateClientWithFirstnameContainSpecialCharacterRequest();

            var response = await _httpClient.PutAsync($"{_clientUrl}/{_fixture.ClientId}", data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_Client_With_Lastname_Empty_And_Fail()
        {
            var data = TestClientData.UpdateClientWithLastnameEmptyRequest();

            var response = await _httpClient.PutAsync($"{_clientUrl}/{_fixture.ClientId}", data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_Client_With_Lastname_Less_Than_Minimum_Length_And_Fail()
        {
            var data = TestClientData.UpdateClientWithLastnameLessThanMinimumLengthRequest();

            var response = await _httpClient.PutAsync($"{_clientUrl}/{_fixture.ClientId}", data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_Client_With_Lastname_Greater_Than_Maximum_Length_And_Fail()
        {
            var data = TestClientData.UpdateClientWithLastnameGreaterThanMaximumLengthRequest();

            var response = await _httpClient.PutAsync($"{_clientUrl}/{_fixture.ClientId}", data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_Client_With_Lastname_Contain_Special_Character_And_Fail()
        {
            var data = TestClientData.UpdateClientWithLastnameContainSpecialCharacterRequest();

            var response = await _httpClient.PutAsync($"{_clientUrl}/{_fixture.ClientId}", data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_Client_With_Date_Of_Birth_Empty_And_Fail()
        {
            var data = TestClientData.UpdateClientWithDateOfBirthEmptyRequest();

            var response = await _httpClient.PutAsync($"{_clientUrl}/{_fixture.ClientId}", data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_Client_With_Date_Of_Birth_Less_Than_Minimum_Date_And_Fail()
        {
            var data = TestClientData.UpdateClientWithDateOfBirthLessThanMinimumDateRequest();

            var response = await _httpClient.PutAsync($"{_clientUrl}/{_fixture.ClientId}", data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_Client_With_Date_Of_Birth_Greater_Than_Maximum_Date_And_Fail()
        {
            var data = TestClientData.UpdateClientWithDateOfBirthGreaterThanMaximumDateRequest();

            var response = await _httpClient.PutAsync($"{_clientUrl}/{_fixture.ClientId}", data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_Client_With_Andress_Empty_And_Fail()
        {
            var data = TestClientData.UpdateClientWithAddressEmptyRequest();

            var response = await _httpClient.PutAsync($"{_clientUrl}/{_fixture.ClientId}", data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_Client_With_Andress_Greater_Than_Maximum_Length_And_Fail()
        {
            var data = TestClientData.CreateClientWithAddressGreaterThanMaximumLengthRequest();

            var response = await _httpClient.PutAsync($"{_clientUrl}/{_fixture.ClientId}", data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_Password_Successfully()
        {
            var data = TestClientData.UpdatePasswordRequest();

            var response = await _httpClient.PutAsync($"{_clientUrl}/{_fixture.UpdatePasswordUrl}/{_fixture.ClientIdForUpdatePassword}", data.GetRequestContent());

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Update_Password_With_Id_Not_Found_And_Fail()
        {
            var data = TestClientData.UpdatePasswordRequest();

            var response = await _httpClient.PutAsync($"{_clientUrl}/{_fixture.UpdatePasswordUrl}/{_fixture.ClientIdNotFound}", data.GetRequestContent());

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Update_Password_With_Current_Password_Empty_And_Fail()
        {
            var data = TestClientData.UpdatePasswordWithCurrentPasswordEmptyRequest();

            var response = await _httpClient.PutAsync($"{_clientUrl}/{_fixture.UpdatePasswordUrl}/{_fixture.ClientId}", data.GetRequestContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_Password_With_Current_Password_Incorrect_And_Fail()
        {
            var data = TestClientData.UpdatePasswordWithCurrentPasswordIncorrectRequest();

            var response = await _httpClient.PutAsync($"{_clientUrl}/{_fixture.UpdatePasswordUrl}/{_fixture.ClientId}", data.GetRequestContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_Password_With_New_Password_Empty_And_Fail()
        {
            var data = TestClientData.UpdatePasswordWithNewPasswordEmptyRequest();

            var response = await _httpClient.PutAsync($"{_clientUrl}/{_fixture.UpdatePasswordUrl}/{_fixture.ClientId}", data.GetRequestContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_Password_With_New_Password_Less_Than_Minimum_Length_And_Fail()
        {
            var data = TestClientData.UpdatePasswordWithNewPasswordLessThanMinimumLengthRequest();

            var response = await _httpClient.PutAsync($"{_clientUrl}/{_fixture.UpdatePasswordUrl}/{_fixture.ClientId}", data.GetRequestContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_Password_With_New_Password_Greater_Than_Maximum_Length_And_Fail()
        {
            var data = TestClientData.UpdatePasswordWithNewPasswordGreaterThanMaximumLengthRequest();

            var response = await _httpClient.PutAsync($"{_clientUrl}/{_fixture.UpdatePasswordUrl}/{_fixture.ClientId}", data.GetRequestContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_Password_With_New_Password_Same_Current_Password_And_Fail()
        {
            var data = TestClientData.UpdatePasswordWithNewPasswordSameCurrentPasswordRequest();

            var response = await _httpClient.PutAsync($"{_clientUrl}/{_fixture.UpdatePasswordUrl}/{_fixture.ClientId}", data.GetRequestContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_Password_With_Confirm_Password_Not_Match_And_Fail()
        {
            var data = TestClientData.UpdatePasswordWithConfirmPasswordNotMatchRequest();

            var response = await _httpClient.PutAsync($"{_clientUrl}/{_fixture.UpdatePasswordUrl}/{_fixture.ClientId}", data.GetRequestContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_Avatar_With_Admin_And_Successfully()
        {
            await CheckAdminAuthorizeAsync();

            var data = TestClientData.UpdateAdminImageRequest();

            var response = await _httpClient.PutAsync($"api/admins/{_fixture.UpdateImageUrl}/{_fixture.AdminId}", data.GetRequestMultipartFormContent());

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Update_Avatar_With_Teacher_And_Successfully()
        {
            await CheckTeacherAuthorizeAsync();

            var data = TestClientData.UpdateTeacherImageRequest();

            var response = await _httpClient.PutAsync($"{_clientUrl}/{_fixture.UpdateImageUrl}/{_fixture.TeacherId}", data.GetRequestMultipartFormContent());

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Update_Avatar_With_Student_And_Successfully()
        {
            await CheckStudentAuthorizeAsync();

            var data = TestClientData.UpdateStudentImageRequest();

            var response = await _httpClient.PutAsync($"{_clientUrl}/{_fixture.UpdateImageUrl}/{_fixture.StudentId}", data.GetRequestMultipartFormContent());

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Update_Avatar_With_ClientId_Not_Found_And_Fail()
        {
            var data = TestClientData.UpdateImageWithClientIdNotFoundRequest();

            var response = await _httpClient.PutAsync($"{_clientUrl}/{_fixture.UpdateImageUrl}/{_fixture.ClientIdNotFound}", data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Update_Avatar_With_Image_Null_And_Fail()
        {
            var data = TestClientData.UpdateImageWithImageNullRequest();

            var response = await _httpClient.PutAsync($"{_clientUrl}/{_fixture.UpdateImageUrl}/{_fixture.ClientId}", data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_Avatar_With_Image_Size_Exceed_The_Allowed_Size_And_Fail()
        {
            var data = TestClientData.UpdateImageWithImageExceedTheAllowedSizeRequest();

            var response = await _httpClient.PutAsync($"{_clientUrl}/{_fixture.UpdateImageUrl}/{_fixture.ClientId}", data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_Avatar_With_Current_Image_Path_Not_Found_And_Fail()
        {
            var data = TestClientData.UpdateImageWithCurrentImagePathNotFoundRequest();

            var response = await _httpClient.PutAsync($"{_clientUrl}/{_fixture.UpdateImageUrl}/{_fixture.ClientIdWithImagePathNotFound}", data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_Avatar_With_Invalid_Image_Format_And_Fail()
        {
            var data = TestClientData.UpdateImageWithInvalidImageFormatRequest();

            var response = await _httpClient.PutAsync($"{_clientUrl}/{_fixture.UpdateImageUrl}/{_fixture.ClientId}", data.GetRequestMultipartFormContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
