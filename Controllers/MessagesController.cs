using MailTmAPI.Http;
using MailTmAPI.Models;
using MailTmAPI.Properties;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Security.Cryptography;
using System.Text;
using System.Net.Mail;
using Microsoft.Win32.SafeHandles;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics.Metrics;
using Microsoft.Extensions.Hosting;

namespace MailTmAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly ILogger<MessagesController> _logger;

        public MessagesController(ILogger<MessagesController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "Messages")]
        public async Task<IEnumerable<MessageInfo>> GetMessagesAsync()
        {
            //GenerateHashFromFirstLetters(@"risto@gmx.com");

            HttpGenericClient<MessageInfo> client = new HttpGenericClient<MessageInfo>();
            var jsondoc = await client.GetAsync(Endpoints.ApiRoot + Endpoints.Messages,null, @"eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzUxMiJ9.eyJpYXQiOjE3NDIyNzQ3NjYsInJvbGVzIjpbIlJPTEVfVVNFUiJdLCJhZGRyZXNzIjoicmlzdG9zQGluZGlnb2Jvb2suY29tIiwiaWQiOiI2N2Q5MDBhNzA3OWM0MjkwNjkwNTNjMDkiLCJtZXJjdXJlIjp7InN1YnNjcmliZSI6WyIvYWNjb3VudHMvNjdkOTAwYTcwNzljNDI5MDY5MDUzYzA5Il19fQ.BJ3SvScfZcfdrvgGRxqv9ozm9-BKHJyeGr2IXb5swOFiMixFFGSfetzr2Rhno4PGm2sKB8xPJqEN21yuRtiyNw");
            if (jsondoc is not null)
            {
                var root = jsondoc.RootElement;
                JsonNode document = JsonNode.Parse(root.ToString() ?? "")!;
                
                var totalitems = document?["hydra:totalItems"]?.DeepClone();
                //if (totalitems is not null && totalitems.GetValue<int>() == 0)
                //    return Enumerable.Empty<MessageInfo>();

                var member = document?["hydra:member"]?.DeepClone();
                if (member is not null && member is JsonArray)
                {
                    var messagelist = JsonSerializer.Deserialize<List<MessageInfo>>(member);
                    if (messagelist is not null && messagelist.Count > 0)
                    {
                        var sh = GenerateHashFromFirstLetters(messagelist[0].From?.Address);
                        Console.WriteLine($"{messagelist[0].From?.Address} => {sh}");
                    }
                    return messagelist ?? Enumerable.Empty<MessageInfo>();
                }
            }

            return Enumerable.Empty<MessageInfo>();
        }
        

        private string GenerateHashFromFirstLetters(string? hashMe)
        {
            if (hashMe is null)
                return "";


            MailAddress address = new MailAddress(hashMe);
            string host = address.Host;
            host = host.ToUpperInvariant();

            string firstfourhoist = host.GetHashCode().ToString().Substring(4);

            int addhash = address.GetHashCode();

            string firstfour = addhash.ToString().Substring(4);

            return firstfour;

        }



    }
}
