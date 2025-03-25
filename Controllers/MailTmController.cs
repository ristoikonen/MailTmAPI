using Microsoft.AspNetCore.Mvc;
using MailTmAPI.Models;
using MailTmAPI.Properties;
using MailTmAPI.Http;
using System.Runtime.Serialization.Json;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Drawing;

namespace MailTmAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MailTmController : ControllerBase
    {

        private readonly ILogger<MailTmController> _logger;

        public MailTmController(ILogger<MailTmController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "TestDomains")]
        public async Task<IEnumerable<DomainInfo>> GetDomainsAsync()
        {

            HttpGenericClient<DomainInfo> client = new HttpGenericClient<DomainInfo>();
            var jsondoc = await client.GetAsync(Endpoints.ApiRoot + Endpoints.Domains);
            if (jsondoc is not null)
            {
                var root = jsondoc.RootElement;
                JsonNode document = JsonNode.Parse(root.ToString() ?? "")!;
                var domains = document?["hydra:member"]?.DeepClone();

                if (domains is not null && domains is JsonArray)
                {
                    var domainslist = JsonSerializer.Deserialize<List<DomainInfo>>(domains);
                    return domainslist ?? Enumerable.Empty<DomainInfo>();
                }
            }
            
            return Enumerable.Empty<DomainInfo>();

            //return Enumerable.Range(1, 5).Select(index => new DomainInfo
            //{
            //    CreatedAt = DateTime.Now,
            //    Id = index.ToString(),
            //    IsActive = true,
            //    Domain = "dnm"

            //})
            //.ToArray();
        }

        [HttpPost(Name = "PostAccount")]
        public async Task<AccountInfo> PostAccountAsync()
        {
            var c = Enum.GetValues<KnownColor>()[new Random().Next(20, 100)];
            string emailaddress = c.ToString();

            // 1. Fetch our domain names.
            //TOSO use API! List<DomainInfo> domaininfolist = await RunIt.RunAPIGetDomainsAsync();

            //var firstdomain = domaininfolist?.Count > 0 ? domaininfolist[0].Domain : "NoDomain";
            //emailaddress += @"@" + domaininfolist?[0].Domain;
            emailaddress = "aidriss2s@indigobook.com";
            Dictionary<string, string> emailparams = new Dictionary<string, string> { { "address", emailaddress }, { "password", "HeHe" } };

            AccountInfo[] domainnames = Array.Empty<AccountInfo>();
            //HttpCustomClient customclient = new HttpCustomClient();

            try
            {
                //var accountinfoc = await customclient.PostAsync(Endpoints.ApiRoot + Endpoints.Accounts, emailparams);
                //return accountinfo;


                HttpGenericClient<AccountInfo> client = new HttpGenericClient<AccountInfo>();
                //AccountInfo[] domainnames = Array.Empty<AccountInfo>();


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
