using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Coinzo.Net.SocketObjects
{
    [JsonConverter(typeof(ArrayConverter))]
    public class CoinzoSocketTrade
    {
        public string Pair { get; set; }

        [ArrayProperty(0)]
        public long TradeId { get; set; }
        
        [ArrayProperty(1)]
        public long OrderId { get; set; }
        
        [ArrayProperty(2)]
        public decimal Amount { get; set; }
    }
}
