using System.Security.Claims;
using System.Text;
using CoreDotNetToken.Repositories;
using CoreDotNetToken.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace CoreDotNetToken.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        // Ensure the correct type for SecurityAlgorithms
        public static class SecurityAlgorithms
        {
            public const string HmacSha256Signature = "HS256";
        }

        public async Task<bool> RegisterUserAsync(string username, string password)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            if (user != null)
                return false;

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            await _userRepository.CreateAsync(new User { Username = username, Password = passwordHash });

            return true;
        }

        public string ValidateUserAndGenerateToken(string username, string password)
        {
            var user = _userRepository.GetByUsernameAsync(username).Result;
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
                return null;

            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, username) }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }

 
}

