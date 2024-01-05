using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tuwan.Proto
{
    public class MoneyResponse
    {
        [JsonProperty("diamond")]
        public int diamond { get; set; }

        [JsonProperty("diamond_str")]
        public string diamond_str { get; set; }

        [JsonProperty("isteacher")]
        public int isteacher { get; set; }

        [JsonProperty("money")]
        public double money { get; set; }

        [JsonProperty("money_str")]
        public string money_str { get; set; }

        [JsonProperty("number")]
        public int number { get; set; }

        [JsonProperty("number_show")]
        public int number_show { get; set; }

        [JsonProperty("number_str")]
        public string number_str { get; set; }


    }
}
