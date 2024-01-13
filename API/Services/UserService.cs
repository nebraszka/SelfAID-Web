using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using SelfAID.API.Data;
using SelfAID.CommonLib.Dtos.User;
using SelfAID.CommonLib.Models;
using SelfAID.CommonLib.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace SelfAID.API.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IConfiguration _config;

        public UserService(IMapper mapper, DataContext context, IConfiguration config)
        {
            _mapper = mapper;
            _context = context;
            _config = config;
        }
        public async Task<ServiceResponse<string>> Login(UserDto userDto)
        {
            var serviceResponse = new ServiceResponse<string>();
            if (userDto == null || string.IsNullOrEmpty(userDto.Username) || string.IsNullOrEmpty(userDto.Password))
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Nie podano danych logowania";
                return serviceResponse;
            }
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == userDto.Username);
                if (user == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Użytkownik nie istnieje";
                    return serviceResponse;
                }

                if (!BCrypt.Net.BCrypt.Verify(userDto.Password, user.PasswordHash))
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Nieprawidłowe hasło";
                    return serviceResponse;
                }

                serviceResponse.Data = CreateToken(user);
                serviceResponse.Message = "Użytkownik został zalogowany";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Błąd przy logowaniu: " + ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<User>> Register(UserDto userDto)
        {
            var serviceResponse = new ServiceResponse<User>();
            if (userDto == null || string.IsNullOrEmpty(userDto.Username) || string.IsNullOrEmpty(userDto.Password))
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Nie podano danych rejestracji";
                return serviceResponse;
            }
            try
            {
                if (await _context.Users.AnyAsync(u => u.Username == userDto.Username))
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Użytkownik już istnieje";
                    return serviceResponse;
                }

                string passwordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
                await _context.Users.AddAsync(new User { Username = userDto.Username, PasswordHash = passwordHash });
                await _context.SaveChangesAsync();

                serviceResponse.Message = "Użytkownik został zarejestrowany";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Błąd przy rejestracji: " + ex.Message;
            }
            return serviceResponse;
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}