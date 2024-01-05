using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Tuwan.Proto
{
    public class Touserinfo
    {
        [JsonProperty("lastGiftNum")]
        public int lastGiftNum { get; set; }

        [JsonProperty("newGiftNum")]
        public int newGiftNum { get; set; }

        [JsonProperty("lastGiftTime")]
        public string lastGiftTime { get; set; }

        [JsonProperty("userid")]
        public string userid { get; set; }

        [JsonProperty("username")]
        public string username { get; set; }

        [JsonProperty("color")]
        public string color { get; set; }
    }
    public class ShowGiftResponse
    {
        [JsonProperty("count")]
        public int count { get; set; }

        [JsonProperty("currentGiftTime")]
        public string currentGiftTime { get; set; }

        [JsonProperty("from")]
        public int from { get; set; }

        [JsonProperty("giftid")]
        public int giftid { get; set; }

        [JsonProperty("giftname")]
        public string giftname { get; set; }

        [JsonProperty("giftpic")]
        public string giftpic { get; set; }

        [JsonProperty("pathpic")]
        public string pathpic { get; set; }

        [JsonProperty("gifsrc")]
        public string gifsrc { get; set; }

        [JsonProperty("pathsvga")]
        public string pathsvga { get; set; }

        [JsonProperty("platforms")]
        public int platforms { get; set; }

        [JsonProperty("showtype")]
        public int showtype { get; set; }

        [JsonProperty("touserinfo")]
        public List<Touserinfo> touserinfo { get; set; }

        [JsonProperty("tipstype")]
        public int tipstype { get; set; }

        [JsonProperty("gifttype")]
        public int gifttype { get; set; }

        [JsonProperty("giftime")]
        public int giftime { get; set; }

        [JsonProperty("lovetypeid")]
        public int lovetypeid { get; set; }

        [JsonProperty("guardtype")]
        public int guardtype { get; set; }

        [JsonProperty("boxname")]
        public string boxname { get; set; }

        [JsonProperty("boxnum")]
        public int boxnum { get; set; }

        [JsonProperty("type")]
        public int type { get; set; }

        [JsonProperty("price")]
        public int price { get; set; }

        [JsonProperty("video_pc_url")]
        public string video_pc_url { get; set; }

        [JsonProperty("video_app_url")]
        public string video_app_url { get; set; }

        [JsonProperty("video_sound")]
        public int video_sound { get; set; }

        [JsonProperty("video_type")]
        public int video_type { get; set; }

        [JsonProperty("isshow")]
        public int isshow { get; set; }

        [JsonProperty("version")]
        public int version { get; set; }

        [JsonProperty("userinfo")]
        public UserInfoResponsedData userinfo { get; set; }

        [JsonProperty("delay_time")]
        public int delay_time { get; set; }

        [JsonProperty("pag_app_url")]
        public string pag_app_url { get; set; }

        [JsonProperty("pag_pc_url")]
        public string pag_pc_url { get; set; }

        [JsonProperty("isFullScreenGift")]
        public int isFullScreenGift { get; set; }

        [JsonProperty("wheat_gif")]
        public string wheat_gif { get; set; }

        [JsonProperty("wheat_frames")]
        public int wheat_frames { get; set; }
    }





}