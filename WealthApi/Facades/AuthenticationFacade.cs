using Microsoft.AspNetCore.Identity;
using WealthApi.Contracts;
using WealthApi.Database;
using WealthApi.Database.Models;
using WealthApi.EmailSender;

namespace WealthApi.Facades
{
    public interface IAuthenticationFacade
    {
        Task AttemptRegistration(UserRegisterDTO dto);
        Task ConfirmRegistration(string token);
        string GetAuthorizationToken(UserLoginDTO dto);
    }

    public class AuthenticationFacade : IAuthenticationFacade
    {
        private readonly ITokenProvider _tokenProvider;
        private readonly IEmailSender _emailSender;
        private readonly DataContext _dataContext;
        private readonly IPasswordHasher<UserLoginDTO> _passwordHasher;

        public AuthenticationFacade(ITokenProvider tokenProvider, IEmailSender emailSender, DataContext context,
            IPasswordHasher<UserLoginDTO> passwordHasher)
        {
            _tokenProvider = tokenProvider;
            _emailSender = emailSender;
            _dataContext = context;
            _passwordHasher = passwordHasher;
        }

        public string GetAuthorizationToken(UserLoginDTO dto)
        {
                var user = _dataContext.Users.FirstOrDefault(u => u.Username == dto.Username);
                if(_passwordHasher.VerifyHashedPassword(dto, user.EncryptedPassword, dto.Password) == PasswordVerificationResult.Failed)
                {
                    throw new Exception("Provided credentials are invalid");
                }

            return _tokenProvider.GenerateLoginToken(dto.Username);
        }

        public async Task AttemptRegistration(UserRegisterDTO dto)
        {
            string token = _tokenProvider.GenerateConfirmationToken(dto);
            
            if (!string.IsNullOrEmpty(token))
            {
                await Task.Run(() => _emailSender.SendRegisterConfirmationEmail(dto.Email, token));
            }    
        }

        public async Task ConfirmRegistration(string token)
        {
            (string username, string email, string encryptedPassword) = _tokenProvider.GetCredentialsClaimsValues(token);

            User newUser = new User()
            {
                Username = username,
                Email = email,
                EncryptedPassword = encryptedPassword,
                CreatedAt = DateTime.UtcNow
            };

            await _dataContext.Users.AddAsync(newUser);
            _dataContext.SaveChanges();
        }
    }
}
