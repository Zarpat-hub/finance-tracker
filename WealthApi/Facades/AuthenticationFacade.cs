using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WealthApi.Contracts;
using WealthApi.Database;
using WealthApi.Database.Models;
using WealthApi.EmailSender;
using WealthApi.Middleware;

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
        private readonly IPasswordHasher<UserRegisterDTO> _registerPasswordHasher;

        public AuthenticationFacade(ITokenProvider tokenProvider, IEmailSender emailSender, DataContext context,
            IPasswordHasher<UserLoginDTO> passwordHasher, IPasswordHasher<UserRegisterDTO> registerPasswordHasher)
        {
            _tokenProvider = tokenProvider;
            _emailSender = emailSender;
            _dataContext = context;
            _passwordHasher = passwordHasher;
            _registerPasswordHasher = registerPasswordHasher;
        }

        public string GetAuthorizationToken(UserLoginDTO dto)
        {
            var user = _dataContext.Users.FirstOrDefault(u => u.Username == dto.Username);

            if(user is null) 
            {
                throw new NotFoundException("User not found");
            }

            if(_passwordHasher.VerifyHashedPassword(dto, user.EncryptedPassword, dto.Password) == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Wrong username or password");
            }

            return _tokenProvider.GenerateLoginToken(dto.Username);
        }

        public async Task AttemptRegistration(UserRegisterDTO dto)
        {
            string token = _tokenProvider.CreateRandomToken();
           
            User newUser = new User()
            {
                Username = dto.Username,
                Email = dto.Email,
                EncryptedPassword = _registerPasswordHasher.HashPassword(dto, dto.Password),
                RegistrationVerificationToken = token
            };

            await _dataContext.Users.AddAsync(newUser);
            _dataContext.SaveChanges();


            if (!string.IsNullOrEmpty(token))
            {
                await Task.Run(() => _emailSender.SendRegisterConfirmationEmail(dto.Email, token));
            }    
        }

        public async Task ConfirmRegistration(string token)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.RegistrationVerificationToken == token);

            if (user is null) 
            {
                throw new NotFoundException("User not found");
            }


            user.CreatedAt = DateTime.UtcNow;
            user.RegistrationVerificationToken = null;
            await _dataContext.SaveChangesAsync();
        }
    }
}
