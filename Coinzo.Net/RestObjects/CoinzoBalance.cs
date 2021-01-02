using Newtonsoft.Json;

namespace Coinzo.Net.RestObjects
{
    public class CoinzoBalance
    {
        [JsonProperty("asset")]
        public string Asset { get; set; }
        
        [JsonProperty("total")]
        public decimal Total { get; set; }
        
        [JsonProperty("available")]
        public decimal Available { get; set; }
    }
}