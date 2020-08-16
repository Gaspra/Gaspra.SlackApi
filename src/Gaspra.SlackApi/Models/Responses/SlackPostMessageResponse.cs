using Newtonsoft.Json;

namespace Gaspra.SlackApi.Models.Responses
{
    public class SlackPostMessageResponse
    {
        [JsonProperty("ok")]
        public bool Ok { get; set; }
        [JsonProperty("message")]
        public SlackMessage Message { get; set; }
    }
}
