using Newtonsoft.Json;

namespace Gaspra.SlackApi.Models
{
    public class NextCursor
    {
        [JsonProperty("next_cursor")]
        public string Cursor { get; set; }
    }
}
