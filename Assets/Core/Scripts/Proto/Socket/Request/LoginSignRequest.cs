using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tuwan.Proto
{
    public class LoginSignRequest
    {
        [JsonProperty("room")]
        public int room { get; set; }
        [JsonProperty("uid")]
        public int uid { get; set; }
        [JsonProperty("time")]
        public int time { get; set; }
        [JsonProperty("token")]
        public string token { get; set; }
        [JsonProperty("ver")]
        public int ver { get; set; }
        [JsonProperty("data_statistics")]
        public LoginPlatform data_statistics { get; set; }

    }

}
