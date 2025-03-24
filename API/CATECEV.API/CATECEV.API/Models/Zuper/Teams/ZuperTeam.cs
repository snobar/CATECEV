using System.Text.Json.Serialization;

namespace CATECEV.API.Models.Zuper.Teams
{
    public class ZuperTeam
    {
        [JsonPropertyName("team_uid")]
        public string TeamUid { get; set; }

        [JsonPropertyName("team_name")]
        public string TeamName { get; set; }

        [JsonPropertyName("team_color")]
        public string TeamColor { get; set; }

        [JsonPropertyName("team_description")]
        public string TeamDescription { get; set; }

        [JsonPropertyName("team_timezone")]
        public string TeamTimezone { get; set; }

        [JsonPropertyName("user_count")]
        public int UserCount { get; set; }

        [JsonPropertyName("is_active")]
        public bool IsActive { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonPropertyName("created_by")]
        public CreatedBy CreatedBy { get; set; }
    }
}
