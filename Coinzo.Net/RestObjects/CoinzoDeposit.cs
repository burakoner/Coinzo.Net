using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Coinzo.Net.RestObjects
{
    public class CoinzoDeposit
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        
        [JsonProperty("tx_id")]
        public string TxId { get; set; }
        
        [JsonProperty("asset")]
        public string Asset { get; set; }
        
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("confirmations")]
        public int Confirmations { get; set; }
        
        [JsonProperty("completed")]
        public bool Completed { get; set; }

        [JsonProperty("created_at"), JsonConverter(typeof(TimestampSecondsConverter))]
        public DateTime CreatedAt { get; set; }
    }
}
