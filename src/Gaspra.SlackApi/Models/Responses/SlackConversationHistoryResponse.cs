using Newtonsoft.Json;
using System.Collections.Generic;

namespace Gaspra.SlackApi.Models.Responses
{
    public class SlackConversationHistoryResponse
    {
        [JsonProperty("ok")]
        public bool Ok { get; set; }
        [JsonProperty("messages")]
        public IEnumerable<SlackMessage> Messages { get; set; }
    }
}
