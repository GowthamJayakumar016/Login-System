using CreditCardAPI.DTOs;
using CreditCardAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CreditCardAPI
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _repo;
        private readonly IConfiguration _config;

        public AuthService(IUserRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        public async Task<string> Register(RegisterDto dto)
        { 
            
            var existingUser = await _repo.GetUserByUsername(dto.Username);
            if (existingUser != null)
                throw new Exception("Username already exists");

    
            var existingEmail = await _repo.GetUserByEmail(dto.Email);
            if (existingEmail != null)
                throw new Exception("Email already registered");

            var user = new Users
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = "User"
            };

            await _repo.CreateUser(user);
            await _repo.Save();

            return "User registered successfully";
        }

        public async Task<string> Login(LoginDto dto)
        {
           

            var user = await _repo.GetUserByUsername(dto.Username);

            if (user == null)
                throw new Exception("User not found");

            bool valid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);

            if (!valid)
                throw new Exception("Invalid password");

            return GenerateJwt(user);
        }

        private string GenerateJwt(Users user)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"])
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("userId", user.UserId.ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}