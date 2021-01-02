using Newtonsoft.Json;

namespace Coinzo.Net.CoreObjects
{
    internal class CoinzoSocketRequest
    {
        [JsonProperty("event")]
        public string Event { get; set; }
        
        [JsonProperty("channel", NullValueHandling = NullValueHandling.Ignore)]
        public string Channel { get; set; }
        
        [JsonProperty("pair", NullValueHandling = NullValueHandling.Ignore)]
        public string Pair { get; set; }
        
        [JsonProperty("key", NullValueHandling = NullValueHandling.Ignore)]
        public string Key { get; set; }
        
        [JsonProperty("chanId", NullValueHandling = NullValueHandling.Ignore)]
        public string ChannelId { get; set; }
        
        [JsonProperty("prec", NullValueHandling = NullValueHandling.Ignore)]
        public int? Precision { get; set; }

        [JsonIgnore]
        public string SubscriptionKey { get; set; }
    }
}
