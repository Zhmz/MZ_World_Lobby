using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Tuwan.Proto
{
    public class SMSLoginRequest
    {
        [JsonProperty("t")]
        public long tel { get; set; }
        [JsonProperty("c")]
        public int code { get; set; }

    }
}
