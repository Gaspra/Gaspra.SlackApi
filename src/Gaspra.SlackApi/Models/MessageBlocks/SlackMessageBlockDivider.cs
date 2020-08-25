using Newtonsoft.Json;

namespace Gaspra.SlackApi.Models.MessageBlocks
{
    public class SlackMessageBlockDivider : ISlackMessageBlock
    {
        [JsonProperty("type")]
        protected string Type { get; } = "divider";
    }
}
