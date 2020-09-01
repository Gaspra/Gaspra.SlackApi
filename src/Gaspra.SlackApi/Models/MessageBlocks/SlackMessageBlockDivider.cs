using Newtonsoft.Json;

namespace Gaspra.SlackApi.Models.MessageBlocks
{
    public class SlackMessageBlockDivider : ISlackMessageBlock
    {
        [JsonProperty("type")]
        public string Type { get; } = "divider";
    }
}
