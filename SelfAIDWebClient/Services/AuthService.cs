using SelfAID.CommonLib.Dtos.User;
using SelfAID.CommonLib.Models;
using SelfAID.CommonLib.Models.User;

using Newtonsoft.Json;
using System.Text;

namespace SelfAID.WebClient.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private string urlPostfix = "Auth";

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<ServiceResponse<string>?> LoginUser(UserDto user)
        {
            try
            {
                var itemJson = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"{urlPostfix}/login", itemJson);
                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ServiceResponse<string>>(responseBody);
                }
                return null;
            }
            catch (Exception ex)
            {
                return new ServiceResponse<string>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ServiceResponse<User>?> RegisterUser(UserDto user)
        {
            try
            {
                var itemJson = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"{urlPostfix}/register", itemJson);
                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ServiceResponse<User>>(responseBody);
                }
                return null;
            }
            catch (Exception ex)
            {
                return new ServiceResponse<User>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}