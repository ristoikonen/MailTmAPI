using System.Text.Json.Serialization;

namespace MailTmAPI.Models
{
    public class MessageInfo
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("accountId")]
        public string? AccountId { get; set; }

        [JsonPropertyName("msgid")]
        public string? MessageId { get; set; }

        [JsonPropertyName("from")]
        public UserInfo? From { get; set; }

        [JsonPropertyName("to")]
        public UserInfo[]? To { get; set; }

        [JsonPropertyName("subject")]
        public string? Subject { get; set; }

        [JsonPropertyName("intro")]
        public string? Intro { get; set; }

        [JsonPropertyName("seen")]
        public bool Seen { get; set; }

        [JsonPropertyName("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonPropertyName("hasAttachments")]
        public bool HasAttachments { get; set; }

        [JsonPropertyName("size")]
        public int Size { get; set; }

        [JsonPropertyName("downloadUrl")]
        public string? DownloadUrl { get; set; }

        [JsonPropertyName("sourceUrl")]
        public string? SourceUrl { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updatedAt")]
        public DateTime UpdatedAt { get; set; }
    }
}
