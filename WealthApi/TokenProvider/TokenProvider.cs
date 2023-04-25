using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WealthApi.Contracts;

namespace WealthApi.Facades
{
    public interface ITokenProvider
    {
        string GenerateConfirmationToken(UserRegisterDTO dto);
        bool ValidateToken(string stringifiedToken);
        (string, string, string) GetCredentialsClaimsValues(string token);
    }

    public class TokenProvider : ITokenProvider
    {
        private readonly IConfiguration _config;
        private readonly IPasswordHasher<UserRegisterDTO> _passwordHasher;

        public TokenProvider(IConfiguration config, IPasswordHasher<UserRegisterDTO> passwordHasher)
        {
            _config = config;
            _passwordHasher = passwordHasher;
        }

        public string GenerateConfirmationToken(UserRegisterDTO dto)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            Claim[] claims = new[]
            {
                new Claim(ClaimTypes.Name, dto.Username),
                new Claim(ClaimTypes.Email, dto.Email),
                new Claim(type: "EncryptedPassword", value: _passwordHasher.HashPassword(dto, dto.Password))
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool ValidateToken(string stringifiedToken)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(stringifiedToken);
            if (jwtToken.ValidTo < DateTime.Now)
            {
                return false;
            }

            return true;
        }

        public (string, string, string) GetCredentialsClaimsValues(string token)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);

            var username = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
            var email = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
            var encryptedPassword = jwtToken.Claims.FirstOrDefault(c => c.Type == "EncryptedPassword").Value;

            return (username, email, encryptedPassword);
        }
    }
}
