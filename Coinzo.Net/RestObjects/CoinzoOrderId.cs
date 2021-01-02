using Newtonsoft.Json;

namespace Coinzo.Net.RestObjects
{
    public class CoinzoOrderId
    {
        [JsonProperty("id")]
        public long Id { get; set; }
    }
}