using System.Text.Json.Serialization;

namespace MailTmAPI.Models
{
    public class AccountInfo
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("address")]
        public string? Address { get; set; }

        [JsonPropertyName("quota")]
        public int Quota { get; set; }

        [JsonPropertyName("used")]
        public int Used { get; set; }

        [JsonPropertyName("isDisabled")]
        public bool IsDisabled { get; set; }

        [JsonPropertyName("isDeleted")]
        public bool IsDeleted { get; set; }


        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updatedAt")]
        public DateTime UpdatedAt { get; set; }

        [JsonPropertyName("retentionAt")]
        public DateTime RetentionAt { get; set; }

        [JsonPropertyName("@context")]
        public string? contextat { get; set; }

        [JsonPropertyName("@type")]
        public string? typeat { get; set; }

        [JsonPropertyName("@id")]
        public string? idat { get; set; }
    }

}
