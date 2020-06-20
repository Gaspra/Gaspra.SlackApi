using Newtonsoft.Json;

namespace Gaspra.SlackApi.Models
{
    public class SlackMessage
    {
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("ts")]
        public string ThreadId { get; set; }
    }
}
