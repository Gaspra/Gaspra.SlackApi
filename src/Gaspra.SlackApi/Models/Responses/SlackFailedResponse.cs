using Gaspra.SlackApi.Models.Enums;
using Newtonsoft.Json;

namespace Gaspra.SlackApi.Models.Responses
{
    public class SlackFailedResponse
    {
        [JsonProperty("ok")]
        public bool Ok { get; set; }
        [JsonProperty("error")]
        public ErrorTypes Error { get; set; }
    }
}
