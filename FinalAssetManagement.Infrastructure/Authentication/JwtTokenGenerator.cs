using FinalAssetManagement.Application.Common.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FinalAssetManagement.Infrastructure.Authentication
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtSettings _settings;
        // Constructor: receives JwtSettings from Dependency Injection
        public JwtTokenGenerator(IOptions<JwtSettings> options)
        {
            _settings = options.Value;
        }

        //---------------------------------------------------------------------------------------

        public string GenerateToken(int userId, string userName)
        {
            // 1) Creating Claims (the user information stored inside the token)
            var claims = new List<Claim>()
            {
                // Standard JWT claim: User ID as Subject
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),

                // ASP.NET Core claim for accessing UserId inside controllers
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),

                // Username: makes User.Identity.Name return this value
                new Claim(ClaimTypes.Name, userName),

                // Unique username (standard JWT claim)
                new Claim(JwtRegisteredClaimNames.UniqueName, userName),

                // Unique token identifier (helps with token tracking)
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // 2) Creating the security key from SecretKey
            var securityKey = new SymmetricSecurityKey
                                  (Encoding.UTF8.GetBytes(_settings.SecretKey));

            // 3) Creating signing credentials (HmacSha256 algorithm)
            var signingCredentials = new SigningCredentials
                                        (securityKey, SecurityAlgorithms.HmacSha256);

            // 4) Creating the actual JWT token: Issuer, Audience, Claims, Expiration, Signing Credentials
            var token = new JwtSecurityToken(
                            issuer:_settings.Issuer,
                            audience:_settings.Audience,
                            claims: claims,
                            expires: DateTime.UtcNow.AddMinutes(_settings.ExpiryInMinutes),
                            signingCredentials: signingCredentials);

            // 5) Converting the JWT object to a string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //---------------------------------------------------------------------------------------
    }
}
