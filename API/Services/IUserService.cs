using SelfAID.CommonLib.Dtos.User;
using SelfAID.CommonLib.Models;
using SelfAID.CommonLib.Models.User;

namespace SelfAID.API.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResponse<User>> Register(UserDto userDto);
        Task<ServiceResponse<string>> Login(UserDto userDto);   
    }
}