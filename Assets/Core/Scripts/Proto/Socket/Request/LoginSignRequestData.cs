using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tuwan.Proto
{
    public class LoginSignRequestData
    {
        [JsonProperty("uid")]
        public int uid { get; set; }

        [JsonProperty("nickname")]
        public string nickname { get; set; }

        [JsonProperty("avatar")]
        public string avatar { get; set; }

        [JsonProperty("playvip")]
        public int playvip { get; set; }

        [JsonProperty("vipicon")]
        public string vipicon { get; set; }

        [JsonProperty("svga_pc")]
        public string svga_pc { get; set; }

        [JsonProperty("svga_app")]
        public string svga_app { get; set; }

        [JsonProperty("show_type")]
        public int show_type { get; set; }

        [JsonProperty("attr")]
        public List<object> attr { get; set; }

        [JsonProperty("medal")]
        public string medal { get; set; }

        [JsonProperty("color")]
        public string color { get; set; }

        [JsonProperty("teacher")]
        public int teacher { get; set; }

        [JsonProperty("is_show")]
        public int is_show { get; set; }

        [JsonProperty("services")]
        public List<int> services { get; set; }

        [JsonProperty("face")]
        public int face { get; set; }

        [JsonProperty("share_nickname")]
        public string share_nickname { get; set; }

        [JsonProperty("share_desc")]
        public string share_desc { get; set; }

        [JsonProperty("share_color")]
        public string share_color { get; set; }

        [JsonProperty("name_color")]
        public string name_color { get; set; }

        [JsonProperty("vap_pc_url")]
        public string vap_pc_url { get; set; }

        [JsonProperty("vap_pc_config")]
        public string vap_pc_config { get; set; }

        [JsonProperty("vap_app_url")]
        public string vap_app_url { get; set; }

        [JsonProperty("vap_app_md5")]
        public string vap_app_md5 { get; set; }

        [JsonProperty("is_full")]
        public int is_full { get; set; }
    }



}
