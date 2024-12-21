using Gateway.IServices;
using ModelDTO.Bonus;

namespace Gateway.Services
{
    public class PrivilegeHistoryServices : IPrivilegeHistoryServices
    {
        private readonly HttpClient _httpClient = new()
        {
            BaseAddress = new Uri("http://privilegeservice:8080")
            //  BaseAddress = new Uri("http://localhost:8060")
            // BaseAddress = new Uri("http://host.docker.internal:8060")
            //BaseAddress = new Uri("http://localhost:5006")
        };
        public async Task<List<PrivilegeHistoryDTO>> GetAll(int page, int size)
        {
            using HttpRequestMessage req = new(HttpMethod.Get, $"api/v1/privilegeHistory?page={page}&size={size}");
            using var res = await _httpClient.SendAsync(req);
            var responses = await res.Content.ReadAsStringAsync();
            var response = await res.Content.ReadFromJsonAsync<List<PrivilegeHistoryDTO>>();
            return response;
        }
        public async Task AddNew(PrivilegeHistoryDTO history)
        {
            using HttpRequestMessage req = new(HttpMethod.Post, $"api/v1/privilegeHistory");
            req.Content = JsonContent.Create( history );
            var res = await _httpClient.SendAsync(req);
            //res.EnsureSuccessStatusCode();
        }
    }
}
