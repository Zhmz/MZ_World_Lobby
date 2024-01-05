using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tuwan.Proto
{
    public class LoginResponse
    {
        [JsonProperty("code")]
        public int code { get; set; }
        [JsonProperty("msg")]
        public string msg { get; set; }
        [JsonProperty("data")]
        public object data { get; set; }

    }
}
