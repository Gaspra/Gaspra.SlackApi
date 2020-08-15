using Newtonsoft.Json;

namespace Gaspra.SlackApi.Models
{
    public class SlackUser
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("profile")]
        public SlackProfile Profile { get; set; }

    }
}
