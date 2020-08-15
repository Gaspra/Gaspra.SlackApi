using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gaspra.SlackApi.Models.Responses
{
    public class SlackUsersResponse
    {
        [JsonProperty("ok")]
        public bool Ok { get; set; }
        [JsonProperty("members")]
        public IEnumerable<SlackUser> Users { get; set; }
        [JsonProperty("response_metadata")]
        public NextCursor NextCursor { get; set; }
    }
}
