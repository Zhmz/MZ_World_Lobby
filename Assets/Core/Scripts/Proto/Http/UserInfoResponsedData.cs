using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Tuwan.Proto
{
    public class UserInfoResponsedData
    {
        [JsonProperty("uid")]
        public int uid { get; set; }

        [JsonProperty("car")]
        public string car { get; set; }

        [JsonProperty("medal")]
        public string medal { get; set; }

        [JsonProperty("guard")]
        public List<object> guard { get; set; }

        [JsonProperty("pendant")]
        public string pendant { get; set; }

        [JsonProperty("nickname")]
        public string nickname { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("age")]
        public int age { get; set; }

        [JsonProperty("sex")]
        public int sex { get; set; }

        [JsonProperty("vip")]
        public int vip { get; set; }

        [JsonProperty("vipicon")]
        public string vipicon { get; set; }

        [JsonProperty("online")]
        public int online { get; set; }

        [JsonProperty("avatar")]
        public string avatar { get; set; }

        [JsonProperty("city")]
        public string city { get; set; }

        [JsonProperty("teacher")]
        public int teacher { get; set; }

        [JsonProperty("sid")]
        public string sid { get; set; }

        [JsonProperty("dtid")]
        public List<object> dtid { get; set; }

        [JsonProperty("services")]
        public List<object> services { get; set; }

        [JsonProperty("ordernum")]
        public int ordernum { get; set; }

        [JsonProperty("hat")]
        public string hat { get; set; }

        [JsonProperty("vipuid")]
        public int vipuid { get; set; }

        [JsonProperty("viplevel")]
        public int viplevel { get; set; }

        [JsonProperty("color")]
        public string color { get; set; }

        [JsonProperty("bubble")]
        public int bubble { get; set; }

        [JsonProperty("self_cid")]
        public int self_cid { get; set; }

        [JsonProperty("skin")]
        public string skin { get; set; }

        [JsonProperty("skin_bg")]
        public string skin_bg { get; set; }

        [JsonProperty("skin_bg_app")]
        public string skin_bg_app { get; set; }

        [JsonProperty("skin_bg_app_width")]
        public string skin_bg_app_width { get; set; }

        [JsonProperty("skin_bg_app_height")]
        public string skin_bg_app_height { get; set; }

        [JsonProperty("remark_name")]
        public string remark_name { get; set; }
    }
}

