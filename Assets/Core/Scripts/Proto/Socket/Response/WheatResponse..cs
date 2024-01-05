using Newtonsoft.Json;
namespace Tuwan.Proto
{

    public class WheatResponse
    {
        [JsonProperty("uid")]
        public int uid { get; set; }

        [JsonProperty("nickname")]
        public string nickname { get; set; }

        [JsonProperty("teamname")]
        public string teamname { get; set; }

        [JsonProperty("avatar")]
        public string avatar { get; set; }

        [JsonProperty("color")]
        public string color { get; set; }

        [JsonProperty("charm")]
        public int charm { get; set; }

        [JsonProperty("tag")]
        public string tag { get; set; }

        [JsonProperty("heads")]
        public string heads { get; set; }

        [JsonProperty("sex")]
        public int sex { get; set; }

        [JsonProperty("isnew")]
        public int isnew { get; set; }

        [JsonProperty("voices")]
        public string voices { get; set; }

        [JsonProperty("position")]
        public int position { get; set; }

        [JsonProperty("online")]
        public int online { get; set; }

        [JsonProperty("owner")]
        public int owner { get; set; }
    }

}