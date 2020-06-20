using Newtonsoft.Json;
using System.Collections.Generic;

namespace Gaspra.SlackApi.Models.Responses
{
    public class SlackChannelsResponse
    {
        [JsonProperty("ok")]
        public bool Ok { get; set; }
        [JsonProperty("channels")]
        public IEnumerable<SlackChannel> Channels { get; set; }
    }
}
