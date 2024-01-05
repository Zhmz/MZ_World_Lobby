using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tuwan.Proto
{
    public class LoginPlatform
    {
        [JsonProperty("platform")]
        public int platform { get; set; }

    }
}
