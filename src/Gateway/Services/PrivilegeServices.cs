using Gateway.IServices;
using ModelDTO.Bonus;
using ModelDTO.Response;
using ModelDTO.Ticket;

namespace Gateway.Services
{
    public class PrivilegeServices : IPrivilegeServices
    {
        private readonly HttpClient _httpClient = new()
        {
            BaseAddress = new Uri("http://privilegeservice:8080")
            // BaseAddress = new Uri("http://localhost:8060")
            // BaseAddress = new Uri("http://host.docker.internal:8060")
            //BaseAddress = new Uri("http://localhost:5006")
        };

        public async Task<PrivilegeDTO> GetByUsername(string username)
        {
            using HttpRequestMessage req = new(HttpMethod.Get, $"api/v1/privilege/{username}");
            using var res = await _httpClient.SendAsync(req);
            var responses = await res.Content.ReadAsStringAsync();
            var response = await res.Content.ReadFromJsonAsync<PrivilegeDTO>();
            return response;
        }
        public async Task UpdatePrivilege(int id, PrivilegeDTO privilege)
        {
            using HttpRequestMessage req = new(HttpMethod.Patch, $"api/v1/privilege/{id}");
            req.Content = JsonContent.Create(privilege);
            await _httpClient.SendAsync(req);
        }
    }
}
