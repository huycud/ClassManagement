using ClassManagement.Api.DTO.Page;
using ClassManagement.Api.DTO.Users.Clients;

namespace ClassManagement.Api.Services.Users.Clients
{
    public interface IClientService : IProfileService
    {
        Task<PageResult<ClientResponse>> GetClientsByRoleNameAsync(ClientsRolePageRequest request);
        Task<List<ClientResponse>> GetClientsByRoleNameAsync(string roleName);
        Task<PageResult<ClientResponse>> GetClientsByClassIdAsync(ClientsClassPageRequest request);
        Task<ClientResponse> GetByIdAsync(int id);
        Task<int> CreateClientAsync(CreateClientRequest request, CancellationToken cancellationToken);
        Task<bool> UpdateClientAsync(int id, UpdateClientRequest request);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);       
    }
}
