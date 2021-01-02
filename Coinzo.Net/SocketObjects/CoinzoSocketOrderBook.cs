using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Coinzo.Net.SocketObjects
{
    [JsonConverter(typeof(ArrayConverter))]
    public class CoinzoSocketOrderBookTotal
    {
        public string Pair { get; set; }

        [ArrayProperty(0)]
        public decimal Value { get; set; }

        [ArrayProperty(1)]
        public decimal Amount { get; set; }
    }

    [JsonConverter(typeof(ArrayConverter))]
    public class CoinzoSocketOrderBookEntry
    {
        public string Pair { get; set; }

        [ArrayProperty(0)]
        public long OrderId { get; set; }
        
        [ArrayProperty(1)]
        public int Count { get; set; }

        [ArrayProperty(2)]
        public decimal Amount { get; set; }
    }
}