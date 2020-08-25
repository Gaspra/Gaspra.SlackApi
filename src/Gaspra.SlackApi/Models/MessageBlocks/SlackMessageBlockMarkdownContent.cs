using Newtonsoft.Json;

namespace Gaspra.SlackApi.Models.MessageBlocks
{
    public class SlackMessageBlockMarkdownContent
    {
        [JsonProperty("type")]
        protected string Type { get; } = "mrkdwn";

        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
