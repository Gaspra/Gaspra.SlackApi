using Newtonsoft.Json;
using System.Collections.Generic;

namespace Gaspra.SlackApi.Models.Responses
{
    public class ChannelsResponse
    {
        [JsonProperty("ok")]
        public bool Ok { get; set; }
        [JsonProperty("channels")]
        public IEnumerable<SlackChannel> Channels { get; set; }
    }
}
