using Coinzo.Net.Converters;
using Coinzo.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Coinzo.Net.RestObjects
{
    public class CoinzoWithdrawal
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

        [JsonProperty("status"), JsonConverter(typeof(WithdrawStatusConverter))]
        public CoinzoWithdrawStatus Status { get; set; }

        [JsonProperty("created_at"), JsonConverter(typeof(TimestampSecondsConverter))]
        public DateTime CreatedAt { get; set; }
    }
}
