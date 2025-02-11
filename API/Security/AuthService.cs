namespace API.Security
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Security.Cryptography;
    using System.Text;
    using Data.Context;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using Shared.Models.Models;

    namespace YourNamespace.Services
    {
        public class AuthService
        {
            private readonly MicrowaveContext _context;
            private readonly IConfiguration _config;

            public AuthService(MicrowaveContext context, IConfiguration config)
            {
                _context = context;
                _config = config;
            }

            // Gerar hash da senha usando SHA1 (256 bits)
            public static string HashPassword(string password)
            {
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                    return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                }
            }

            // Criar token JWT
            public string GenerateJwtToken(User user)
            {
                var key = Encoding.UTF8.GetBytes(_config["JwtSettings:Secret"]);
                var claims = new[]
                {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddHours(2),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                    Issuer = _config["JwtSettings:Issuer"],
                    Audience = _config["JwtSettings:Audience"]
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }

            // Registrar usuário
            public async Task<string> Register(string username, string password)
            {
                if (_context.Users.Any(u => u.Username == username))
                    return ""; 

                var hashedPassword = HashPassword(password);
                var user = new User { Username = username, PasswordHash = hashedPassword };
                var token = GenerateJwtToken(user);
                _context.Users.Add(user);
                _context.SaveChanges();
                return token;
            }

            // Autenticar usuário
            public async Task<string> Authenticate(string username, string password)
            {
                var hashedPassword = HashPassword(password);
                var user = _context.Users.FirstOrDefault(u => u.Username == username && u.PasswordHash == hashedPassword);

                return user != null ? GenerateJwtToken(user) : null;
            }
        }
    }

}
