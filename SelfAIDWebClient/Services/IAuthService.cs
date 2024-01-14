using SelfAID.CommonLib.Dtos.User;
using SelfAID.CommonLib.Models;
using SelfAID.CommonLib.Models.User;

namespace SelfAID.WebClient.Services
{
    public interface IAuthService
    {
        Task<ServiceResponse<User>?> RegisterUser(UserDto user);
        Task<ServiceResponse<string>?> LoginUser(UserDto user);
    }
}