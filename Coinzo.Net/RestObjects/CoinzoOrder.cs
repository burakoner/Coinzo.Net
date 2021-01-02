using Coinzo.Net.Converters;
using Coinzo.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Coinzo.Net.RestObjects
{
    public class CoinzoOrder
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        
        [JsonProperty("pair")]
        public string Pair { get; set; }

        [JsonProperty("side"), JsonConverter(typeof(OrderSideConverter))]
        public CoinzoOrderSide Side { get; set; }

        [JsonProperty("type"), JsonConverter(typeof(OrderTypeConverter))]
        public CoinzoOrderType Type { get; set; }

        [JsonProperty("limit_price")]
        public decimal? LimitPrice { get; set; }
        
        [JsonProperty("stop_price")]
        public decimal? StopPrice { get; set; }
        
        [JsonProperty("original_amount")]
        public decimal OriginalAmount { get; set; }
        
        [JsonProperty("executed_amount")]
        public decimal ExecutedAmount { get; set; }
        
        [JsonProperty("remaining_amount")]
        public decimal RemainingAmount { get; set; }
        
        [JsonProperty("active")]
        public bool Active { get; set; }
        
        [JsonProperty("cancelled")]
        public bool Cancelled { get; set; }

        [JsonProperty("updated_at"), JsonConverter(typeof(TimestampSecondsConverter))]
        public DateTime UpdatedAt { get; set; }
    }
}
