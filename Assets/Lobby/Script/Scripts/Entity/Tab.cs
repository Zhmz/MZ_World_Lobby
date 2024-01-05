using Newtonsoft.Json;
namespace Tuwan.Lobby.Entity
{

    public class Tab
    {
        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("title")]
        public string title { get; set; }

        [JsonProperty("num")]
        public int num { get; set; }

        [JsonProperty("is_show")]
        public int is_show { get; set; }
    }

}