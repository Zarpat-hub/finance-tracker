using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WealthApi.Core;
using WealthApi.Database;
using WealthApi.Database.Models;

namespace WealthApi.Facades
{
    public interface IAccountConfigFacade
    {
        Task SaveConfig(AccountConfig config);
        Task<AccountConfig> GetConfig();
    }

    public class AccountConfigFacade : IAccountConfigFacade
    {
        private readonly IUserFacade _userService;
        private readonly DataContext _context;

        public AccountConfigFacade(IUserFacade userService, DataContext context)
        {
            _userService = userService;
            _context = context;
        }

        public async Task<AccountConfig> GetConfig()
        {
            AccountConfiguration accountConfigurationRaw = await GetRawDbConfiguration();

            if (accountConfigurationRaw != null)
            {
                AccountConfig accountConfigDeserialized = JsonConvert.DeserializeObject<AccountConfig>(accountConfigurationRaw.ConfigurationJson);
                return accountConfigDeserialized;
            }

            return null;
        }

        public async Task SaveConfig(AccountConfig config)
        {
            config.Goals = await GetGoals();
            string configJson = JsonConvert.SerializeObject(config);

            AccountConfiguration rawConfiguration = await GetRawDbConfiguration();

            if (rawConfiguration != null)
            {
                rawConfiguration.ConfigurationJson = configJson;
                _context.AccountsConfigurations.Update(rawConfiguration);
            }
            else
            {
                _context.AccountsConfigurations.Add(new AccountConfiguration(_userService.GetCurrentUser().Result.Username, configJson));
            }

            await _context.SaveChangesAsync();
        }

        private async Task<List<Goal>> GetGoals()
        {
            AccountConfig currentConfig = await GetConfig();

            if (currentConfig == null)
            {
                return new List<Goal>();
            }

            return currentConfig.Goals;
        }

        private async Task<AccountConfiguration> GetRawDbConfiguration()
        {
            string username = _userService.GetCurrentUser().Result.Username;
            AccountConfiguration accountConfigurationRaw = await _context.AccountsConfigurations.FirstOrDefaultAsync(c => c.Username == username);

            return accountConfigurationRaw;
        }
    }
}
