using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gaspra.SlackApi.Models.MessageBlocks
{
    public class SlackMessageBlockTextContent
    {
        [JsonProperty("type")]
        public string Type { get; } = "plain_text";

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("emoji")]
        public bool Emoji { get; set; }
    }
}
