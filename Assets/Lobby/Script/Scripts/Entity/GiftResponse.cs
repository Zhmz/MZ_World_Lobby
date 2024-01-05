using Newtonsoft.Json;
using System.Collections.Generic;
namespace Tuwan.Lobby.Entity
{

    public class GiftResponse
    {
        [JsonProperty("data")]
        public List<GiftItemResponse> data { get; set; }

        [JsonProperty("error")]
        public int error { get; set; }

        [JsonProperty("error_msg")]
        public string error_msg { get; set; }

        [JsonProperty("navid")]
        public int navid { get; set; }

        [JsonProperty("tabs")]
        public List<Tab> tabs { get; set; }
    }

}