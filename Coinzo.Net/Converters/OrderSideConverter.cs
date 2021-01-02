using Coinzo.Net.Enums;
using CryptoExchange.Net.Converters;
using System.Collections.Generic;

namespace Coinzo.Net.Converters
{
    internal class OrderSideConverter : BaseConverter<CoinzoOrderSide>
    {
        public OrderSideConverter() : this(true) { }
        public OrderSideConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<CoinzoOrderSide, string>> Mapping => new List<KeyValuePair<CoinzoOrderSide, string>>
        {
            new KeyValuePair<CoinzoOrderSide, string>(CoinzoOrderSide.Buy, "BUY"),
            new KeyValuePair<CoinzoOrderSide, string>(CoinzoOrderSide.Sell, "SELL"),
        };
    }
}