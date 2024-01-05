using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tuwan.Proto
{
    public class LoginRoomInfoResponse
    {
        [JsonProperty("error")]
        public int error { get; set; }
        [JsonProperty("error_msg")]
        public string error_msg { get; set; }
        [JsonProperty("data")]
        public LoginRoomInfoData data { get; set; }

    }
}
