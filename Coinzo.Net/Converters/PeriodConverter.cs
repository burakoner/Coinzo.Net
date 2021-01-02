using Coinzo.Net.Enums;
using CryptoExchange.Net.Converters;
using System.Collections.Generic;

namespace Coinzo.Net.Converters
{
    internal class PeriodConverter : BaseConverter<CoinzoPeriod>
    {
        public PeriodConverter() : this(true) { }
        public PeriodConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<CoinzoPeriod, string>> Mapping => new List<KeyValuePair<CoinzoPeriod, string>>
        {
            new KeyValuePair<CoinzoPeriod, string>(CoinzoPeriod.OneMinute, "1m"),
            new KeyValuePair<CoinzoPeriod, string>(CoinzoPeriod.FiveMinutes, "5m"),
            new KeyValuePair<CoinzoPeriod, string>(CoinzoPeriod.FifteenMinutes, "15m"),
            new KeyValuePair<CoinzoPeriod, string>(CoinzoPeriod.ThirtyMinutes, "30m"),
            new KeyValuePair<CoinzoPeriod, string>(CoinzoPeriod.OneHour, "1h"),
            new KeyValuePair<CoinzoPeriod, string>(CoinzoPeriod.ThreeHours, "3h"),
            new KeyValuePair<CoinzoPeriod, string>(CoinzoPeriod.SixHours, "6h"),
            new KeyValuePair<CoinzoPeriod, string>(CoinzoPeriod.TwelveHours, "12h"),
            new KeyValuePair<CoinzoPeriod, string>(CoinzoPeriod.OneDay, "1d"),
            new KeyValuePair<CoinzoPeriod, string>(CoinzoPeriod.OneWeek, "1w"),
        };
    }
}