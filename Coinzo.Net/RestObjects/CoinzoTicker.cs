using Newtonsoft.Json;

namespace Coinzo.Net.RestObjects
{
    public class CoinzoTicker
    {
        [JsonProperty("pair")]
        public string Symbol { get; set; }
        
        [JsonProperty("low")]
        public decimal Low { get; set; }
        
        [JsonProperty("high")]
        public decimal High { get; set; }
        
        [JsonProperty("bid")]
        public decimal Bid { get; set; }
        
        [JsonProperty("ask")]
        public decimal Ask { get; set; }

        [JsonProperty("last")]
        public decimal Last { get; set; }
        
        [JsonProperty("volume")]
        public decimal Volume { get; set; }
        
        [JsonProperty("daily_change")]
        public decimal DailyChange { get; set; }
        
        [JsonProperty("daily_change_percentage")]
        public decimal DailyChangePercentage { get; set; }
        
    }
}