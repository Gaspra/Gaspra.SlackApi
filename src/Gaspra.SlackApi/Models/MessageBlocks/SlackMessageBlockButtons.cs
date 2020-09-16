using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gaspra.SlackApi.Models.MessageBlocks
{
    public class SlackMessageBlockButtons : ISlackMessageBlock
    {
        [JsonProperty("type")]
        public string Type { get; } = "actions";

        [JsonProperty("elements")]
        public IList<SlackMessageBlockElement> Elements { get; set; }
    }
}
