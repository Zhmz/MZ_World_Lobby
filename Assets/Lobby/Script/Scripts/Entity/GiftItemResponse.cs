using Newtonsoft.Json;
namespace Tuwan.Lobby.Entity
{

    public class GiftItemResponse
    {
        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("title")]
        public string title { get; set; }

        [JsonProperty("pic")]
        public string pic { get; set; }

        [JsonProperty("gifpic")]
        public string gifpic { get; set; }

        [JsonProperty("intro")]
        public string intro { get; set; }

        [JsonProperty("price")]
        public int price { get; set; }

        [JsonProperty("charm_score")]
        public int charm_score { get; set; }

        [JsonProperty("diamond")]
        public int diamond { get; set; }

        [JsonProperty("uuid")]
        public int uuid { get; set; }

        [JsonProperty("tuid")]
        public int tuid { get; set; }

        [JsonProperty("create_time")]
        public int create_time { get; set; }

        [JsonProperty("app_gif")]
        public string app_gif { get; set; }

        [JsonProperty("show_type")]
        public int show_type { get; set; }

        [JsonProperty("giftime")]
        public int giftime { get; set; }

        [JsonProperty("box_num")]
        public int box_num { get; set; }

        [JsonProperty("diamond_box")]
        public int diamond_box { get; set; }

        [JsonProperty("type")]
        public int type { get; set; }

        [JsonProperty("video_pc_url")]
        public string video_pc_url { get; set; }

        [JsonProperty("video_app_url")]
        public string video_app_url { get; set; }

        [JsonProperty("video_sound")]
        public int video_sound { get; set; }

        [JsonProperty("video_type")]
        public int video_type { get; set; }

        [JsonProperty("tips_type")]
        public int tips_type { get; set; }

        [JsonProperty("question_gift")]
        public int question_gift { get; set; }

        [JsonProperty("worth")]
        public int worth { get; set; }
    }

}