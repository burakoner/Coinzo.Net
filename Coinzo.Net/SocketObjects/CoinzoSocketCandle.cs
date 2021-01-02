using Coinzo.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Coinzo.Net.SocketObjects
{
    [JsonConverter(typeof(ArrayConverter))]
    public class CoinzoSocketCandle
    {
        public string Pair { get; set; }
        public CoinzoPeriod Period { get; set; }

        [ArrayProperty(0)]
        [JsonConverter(typeof(TimestampSecondsConverter))]
        public DateTime OpenTime { get; set; }

        [ArrayProperty(1)]
        public decimal Open { get; set; }
        
        [ArrayProperty(2)]
        public decimal High { get; set; }
        
        [ArrayProperty(3)]
        public decimal Low { get; set; }
        
        [ArrayProperty(4)]
        public decimal Close { get; set; }
        
        [ArrayProperty(5)]
        public decimal Volume { get; set; }
    }
}
