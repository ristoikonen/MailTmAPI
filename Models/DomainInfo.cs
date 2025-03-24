using System.Text.Json;
using System.Text.Json.Serialization;

namespace MailTmAPI.Models
{
    public class DomainInfo
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("domain")]
        public string? Domain { get; set; }

        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; }

        //[JsonProperty("isPrivate")]
        //public string? IsPrivate { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updatedAt")]
        public DateTime UpdatedAt { get; set; }

        [JsonPropertyName("@type")]
        public string? typeat { get; set; }

        [JsonPropertyName("@id")]
        public string? idat { get; set; }

        //[JsonProperty("@context")]
        //public string? context { get; set; }
    }
}
