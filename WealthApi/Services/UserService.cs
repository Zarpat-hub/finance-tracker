using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WealthApi.Database;
using WealthApi.Database.Models;
using WealthApi.Middleware;

namespace WealthApi.Services
{
    public interface IUserService 
    {
        Task<User> GetCurrentUser();
    }


    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DataContext _context;

        public UserService(IHttpContextAccessor httpContextAccessor, DataContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
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

        private string GetUserByName()
        {
            var result = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                result = User.FindFirstValue(ClaimTypes.Name);
            }
            return result;
        }
    }
}
