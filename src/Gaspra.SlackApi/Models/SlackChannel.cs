using Newtonsoft.Json;

namespace Gaspra.SlackApi.Models
{
    public class SlackChannel
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
