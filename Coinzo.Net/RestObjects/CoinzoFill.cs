using Coinzo.Net.Converters;
using Coinzo.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Coinzo.Net.RestObjects
{
    public class CoinzoFill
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        
        [JsonProperty("order_id")]
        public long OrderId { get; set; }
        
        [JsonProperty("coin")]
        public string Coin { get; set; }
        
        [JsonProperty("fiat")]
        public string Fiat { get; set; }

        [JsonProperty("side"), JsonConverter(typeof(OrderSideConverter))]
        public CoinzoOrderSide Side { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }
        
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
        
        [JsonProperty("taker")]
        public bool Taker { get; set; }
        
        [JsonProperty("fee")]
        public decimal Fee { get; set; }

        [JsonProperty("created_at"), JsonConverter(typeof(TimestampSecondsConverter))]
        public DateTime CreatedAt { get; set; }
    }
}
