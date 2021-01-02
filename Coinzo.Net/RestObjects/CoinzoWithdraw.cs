using Newtonsoft.Json;

namespace Coinzo.Net.RestObjects
{
    public class CoinzoWithdraw
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        
        [JsonProperty("asset")]
        public string Asset { get; set; }
        
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
    }
}