using HealthcareManagementSystem.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace HealthcareManagementSystem.Authorization
{
    public interface IJwtUtils
    {
        public string GenerateJwtToken(UsersModel users);
        public int? ValidateJwtToken(string jwtToken);
        // RefreshToken GenerateRefreshToken();
    }
    public class JwtUtils:IJwtUtils
    {
        public IOptions<MyConfig> _config;
        public JwtUtils(IOptions<MyConfig> config) 
        { 
            _config = config; 
        }
        public string GenerateJwtToken(UsersModel users)
        {
            var jwtsecret = _config.Value.JwtSecret;
            // generate token that is valid for 10 minutes
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtsecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", users.UserId.ToString()) }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                //Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public int? ValidateJwtToken(string token)
        {
            var jwtsecret = _config.Value.JwtSecret;

            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtsecret);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var UserId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

                // return account id from JWT token if validation successful
                return UserId;
            }
            catch
            {
                // return null if validation fails
                return null;
            }
        }
    }
}
