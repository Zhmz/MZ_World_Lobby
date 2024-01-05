
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Tuwan.Proto
{
    public class UserInfoResponse
    {
        [JsonProperty("error")]
        public int error { get; set; }

        [JsonProperty("data")]
        public List<UserInfoResponsedData> data { get; set; }

        [JsonProperty("mobile")]
        public int mobile { get; set; }

    }
}

