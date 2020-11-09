using Gaspra.SlackApi.Models.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gaspra.SlackApi.Models.Events
{
    public class EventType
    {
        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public MetadataTypes MetadataType { get; set; }
    }
}
