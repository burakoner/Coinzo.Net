using Newtonsoft.Json;

namespace Coinzo.Net.RestObjects
{
    public class CoinzoDepositAddress
    {
        [JsonProperty("asset")]
        public string Asset { get; set; }
        
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("tag")]
        public string Tag { get; set; } = "";
    }
}
