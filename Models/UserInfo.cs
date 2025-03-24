using System.Text.Json.Serialization;

namespace MailTmAPI.Models
{
    public class UserInfo
    {
        [JsonPropertyName("address")]
        public string? Address { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }
}
