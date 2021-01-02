using Newtonsoft.Json;
using System.Collections.Generic;

namespace Coinzo.Net.RestObjects
{
    public class CoinzoOrderBook
    {
        [JsonProperty("asks")]
        public IEnumerable<CoinzoOrderBookEntry> Asks { get; set; }

        [JsonProperty("bids")]
        public IEnumerable<CoinzoOrderBookEntry> Bids { get; set; }

        [JsonProperty("total")]
        public CoinzoOrderBookTotal Total { get; set; }
    }

    public class CoinzoOrderBookEntry
    {
        [JsonProperty("price")]
        public decimal Price { get; set; }
        
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
        
        [JsonProperty("count")]
        public int Count { get; set; }
    }

    public class CoinzoOrderBookTotal
    {
        [JsonProperty("ask")]
        public decimal Ask { get; set; }
        
        [JsonProperty("bid")]
        public decimal Bid { get; set; }
    }
}
