using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tuwan.Proto
{
    public class SocketResponse
    {
        [JsonProperty("typeid")]
        public int TypeId { get; set; }
        [JsonProperty("error_msg")]
        public string error_msg { get; set; }
        [JsonProperty("error")]
        public int error { get; set; }
        [JsonProperty("state")]
        public int state { get; set; }

        [JsonProperty("type")]
        public int type { get; set; }
        [JsonProperty("cid")]
        public int cid { get; set; }
        [JsonProperty("uid")]
        public string uid { get; set; }

        [JsonProperty("wheatUid")]
        public object wheatUid { get; set; }
        [JsonProperty("invite")]
        public int invite { get; set; }
        [JsonProperty("chatTypeArr")]
        public List<int> chatTypeArr { get; set; }
        [JsonProperty("data")]
        public object data { get; set; }


    }
}
