using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Tuwan.Proto
{
    public class Giftinfo
    {
        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("title")]
        public string title { get; set; }

        [JsonProperty("num")]
        public int num { get; set; }

        [JsonProperty("pic")]
        public string pic { get; set; }

        [JsonProperty("gif")]
        public string gif { get; set; }

        [JsonProperty("svga")]
        public string svga { get; set; }

        [JsonProperty("showtype")]
        public int showtype { get; set; }

        [JsonProperty("giftime")]
        public int giftime { get; set; }

        [JsonProperty("tips_type")]
        public int tips_type { get; set; }

        [JsonProperty("uids")]
        public string uids { get; set; }

        [JsonProperty("is_show")]
        public int is_show { get; set; }

        [JsonProperty("diamond")]
        public int diamond { get; set; }

        [JsonProperty("video_pc_url")]
        public string video_pc_url { get; set; }

        [JsonProperty("video_app_url")]
        public string video_app_url { get; set; }

        [JsonProperty("video_sound")]
        public int video_sound { get; set; }

        [JsonProperty("video_type")]
        public int video_type { get; set; }

        [JsonProperty("pag_app_url")]
        public string pag_app_url { get; set; }

        [JsonProperty("pag_pc_url")]
        public string pag_pc_url { get; set; }

        [JsonProperty("isFullScreenGift")]
        public int isFullScreenGift { get; set; }

        [JsonProperty("vap_pc_url")]
        public string vap_pc_url { get; set; }

        [JsonProperty("vap_pc_config")]
        public string vap_pc_config { get; set; }

        [JsonProperty("vap_app_url")]
        public string vap_app_url { get; set; }

        [JsonProperty("vap_app_md5")]
        public string vap_app_md5 { get; set; }

        [JsonProperty("attr")]
        public List<object> attr { get; set; }
    }

    public class SendGiftResponse
    {
        [JsonProperty("error")]
        public int error { get; set; }

        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("tradenoArr")]
        public TradenoArr tradenoArr { get; set; }

        [JsonProperty("error_msg")]
        public string error_msg { get; set; }

        [JsonProperty("giftinfo")]
        public Giftinfo giftinfo { get; set; }
    }

    public class TradenoArr
    {
        [JsonProperty("3844140")]
        public string _3844140 { get; set; }
    }


}
