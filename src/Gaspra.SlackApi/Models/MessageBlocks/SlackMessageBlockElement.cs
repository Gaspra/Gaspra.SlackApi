using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gaspra.SlackApi.Models.MessageBlocks
{
    public class SlackMessageBlockElement
    {
        [JsonProperty("type")]
        protected string Type { get; } = "button";

        [JsonProperty("text")]
        public SlackMessageBlockTextContent Text { get; set; }

        [JsonProperty("value")]
        protected string Value { get; set; }
    }
}
