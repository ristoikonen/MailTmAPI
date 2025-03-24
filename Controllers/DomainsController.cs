using MailTmAPI.Http;
using MailTmAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Nodes;
using System.Text.Json;

namespace MailTmAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DomainsController : ControllerBase
    {
        private readonly ILogger<DomainsController> _logger;

        public DomainsController(ILogger<DomainsController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "Domains")]
        public async Task<IEnumerable<DomainInfo>> GetDomainsAsync()
        {

            HttpGenericClient<DomainInfo> client = new HttpGenericClient<DomainInfo>();
            var jsondoc = await client.GetAsync();
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
    }
}
