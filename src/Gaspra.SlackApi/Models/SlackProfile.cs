using Newtonsoft.Json;

namespace Gaspra.SlackApi.Models
{
    public class SlackProfile
    {
        [JsonProperty("status_text")]
        public string StatusMessage { get; set; }
        [JsonProperty("status_emoji")]
        public string StatusEmoji { get; set; }
        [JsonProperty("real_name")]
        public string RealName { get; set; }
        [JsonProperty("display_name")]
        public string DisplayName { get; set; }
        [JsonProperty("email")]
        public string EmailAddress { get; set; }
    }
}
