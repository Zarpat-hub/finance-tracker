using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WealthApi.Contracts;
using WealthApi.Database;
using WealthApi.Database.Models;
using WealthApi.Middleware;

namespace WealthApi.Facades
{
    public interface IUserFacade
    {
        Task<User> GetCurrentUser();
        Task ChangePassword(ChangePasswordDTO dto);
        Task EditProfile(EditProfileDTO dto);
        Task<string> ChangeUserImg(IFormFile formFile);
       
    }


    public class UserFacade : IUserFacade
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DataContext _context;
        private readonly IPasswordHasher<ChangePasswordDTO> _changePasswordHasher;

        public UserFacade(IHttpContextAccessor httpContextAccessor, DataContext context, IPasswordHasher<ChangePasswordDTO> changePasswordHasher)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _changePasswordHasher = changePasswordHasher;
        }

        private ClaimsPrincipal User => _httpContextAccessor.HttpContext?.User;


        public async Task<User> GetCurrentUser()
        {
            string userEmail = GetUserByName();
            var user = await _context.Users.FirstOrDefaultAsync(e => e.Username == userEmail);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
            return user;
        }


        public async Task ChangePassword(ChangePasswordDTO dto)
        {
            User user = await GetCurrentUser();


            if (_changePasswordHasher.VerifyHashedPassword(dto, user.EncryptedPassword, dto.OldPassword) == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Old Password is wrong");
            }

            if (dto.NewPassword != dto.RetypedNewPassword)
            {
                throw new BadRequestException("Passwords are not the same");
            }

            user.EncryptedPassword = _changePasswordHasher.HashPassword(dto, dto.NewPassword);
            await _context.SaveChangesAsync();
        }

        private string GetUserByName()
        {
            var result = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                result = User.FindFirstValue(ClaimTypes.Name);
            }
            return result;
        }

        public async Task EditProfile(EditProfileDTO dto)
        {
            User user = await GetCurrentUser();
            user.Email = dto.Email;
            user.FirstName = dto.Firstname;
            user.LastName = dto.Lastname;

            await _context.SaveChangesAsync();
        }
        public async Task<string> ChangeUserImg(IFormFile formFile)
        {
            User user = await GetCurrentUser();
  
            string baseUrl = "https://localhost:7083";
            string relativePath = $"UserFiles/{user.Username}-{formFile.FileName}";
            string fileUrl = baseUrl.TrimEnd('/') + "/" + relativePath;
            user.UserImg = fileUrl;

            //TODO: delete old user profile img

            if (formFile != null && formFile.Length > 0)
            {
                var rootPath = Directory.GetCurrentDirectory();
                var fullPath = $"{rootPath}/UserFiles/{user.Username}-{formFile.FileName}";
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    formFile.CopyTo(stream);
                } 
            }

            await _context.SaveChangesAsync();
            return fileUrl;
        }
    }
}
