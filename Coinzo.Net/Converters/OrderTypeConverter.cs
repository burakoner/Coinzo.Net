using Coinzo.Net.Enums;
using CryptoExchange.Net.Converters;
using System.Collections.Generic;

namespace Coinzo.Net.Converters
{
    public class OrderTypeConverter : BaseConverter<CoinzoOrderType>
    {
        public OrderTypeConverter() : this(true) { }
        public OrderTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<CoinzoOrderType, string>> Mapping => new List<KeyValuePair<CoinzoOrderType, string>>
        {
            new KeyValuePair<CoinzoOrderType, string>(CoinzoOrderType.Limit, "LIMIT"),
            new KeyValuePair<CoinzoOrderType, string>(CoinzoOrderType.Market, "MARKET"),
            new KeyValuePair<CoinzoOrderType, string>(CoinzoOrderType.Stop, "STOP"),
            new KeyValuePair<CoinzoOrderType, string>(CoinzoOrderType.StopLimit, "STOP LIMIT"),
        };
    }
}