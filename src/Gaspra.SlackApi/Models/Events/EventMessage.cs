using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gaspra.SlackApi.Models.Events
{
    public class EventMessage
    {
        [JsonProperty("channel")]
        public string ChannelId { get; set; }
        [JsonProperty("user")]
        public string UserId { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("ts")]
        public string ThreadId { get; set; }
    }
}
