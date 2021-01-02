using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Coinzo.Net.SocketObjects
{
    [JsonConverter(typeof(ArrayConverter))]
    public class CoinzoSocketTicker
    {
        public string Pair { get; set; }

        [ArrayProperty(0)]
        public decimal Change { get; set; }

        [ArrayProperty(1)]
        public decimal ChangePercent { get; set; }

        [ArrayProperty(2)]
        public decimal Close { get; set; }

        [ArrayProperty(3)]
        public decimal Volume { get; set; }

        [ArrayProperty(4)]
        public decimal High { get; set; }

        [ArrayProperty(5)]
        public decimal Low { get; set; }

        public decimal Open { get; set; }
    }
}
