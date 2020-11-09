using Gaspra.SlackApi.Models.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gaspra.SlackApi.Models.Events
{
    public class EventCallbackMetadata
    {
        [JsonProperty("token")]
        public string Token { get; set; }
        [JsonProperty("team_id")]
        public string TeamId { get; set; }
        [JsonProperty("api_app_id")]
        public string ApiAppId { get; set; }
        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public MetadataTypes MetadataType { get; set; }
        [JsonProperty("event_id")]
        public string EventId { get; set; }
        [JsonProperty("event_time")]
        public string EventTime { get; set; }
        [JsonProperty("event")]
        public EventCallbackType EventCallbackType { get; set; }
        [JsonProperty("authed_users")]
        public IEnumerable<string> AuthedUsers { get; set; }
    }
}
