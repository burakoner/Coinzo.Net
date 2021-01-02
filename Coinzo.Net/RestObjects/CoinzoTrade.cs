using Coinzo.Net.Converters;
using Coinzo.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Coinzo.Net.RestObjects
{
    public class CoinzoTrade
    {
        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("side"), JsonConverter(typeof(OrderSideConverter))]
        public CoinzoOrderSide Side { get; set; }

        [JsonProperty("created_at"), JsonConverter(typeof(TimestampSecondsConverter))]
        public DateTime CreatedAt { get; set; }
    }
}
