using MailTmAPI.Http;
using MailTmAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Nodes;
using System.Text.Json;
using MailTmAPI.Properties;
using System.Net.Mail;
using System.Drawing;
using System.Text;

namespace MailTmAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {

        private readonly ILogger<AccountsController> _logger;

        public AccountsController(ILogger<AccountsController> logger)
        {
            _logger = logger;
        }

        [HttpPost(Name = "Accounts")]
        public async Task<AccountInfo> GetAccountsAsync()
        {
            var c = Enum.GetValues<KnownColor>()[new Random().Next(20, 100)];
            string emailaddress = c.ToString();

            // 1. Fetch our domain names.
            //TOSO use API! List<DomainInfo> domaininfolist = await RunIt.RunAPIGetDomainsAsync();

            //var firstdomain = domaininfolist?.Count > 0 ? domaininfolist[0].Domain : "NoDomain";
            //emailaddress += @"@" + domaininfolist?[0].Domain;

            emailaddress = "id@indigobook.com";
            Dictionary<string, string> emailparams = new Dictionary<string, string> { { "address", emailaddress }, { "password", "HeHe" } };

            HttpGenericClient<AccountInfo> client = new HttpGenericClient<AccountInfo>();
            try
            {
                var accountinfo = await client.PostAsync(Endpoints.ApiRoot + Endpoints.Accounts, emailparams);
                return accountinfo ?? new AccountInfo();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }

            return new AccountInfo();

        }

    }
}
