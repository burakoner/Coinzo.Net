using Coinzo.Net.Enums;
using CryptoExchange.Net.Converters;
using System.Collections.Generic;

namespace Coinzo.Net.Converters
{
    public class WithdrawStatusConverter : BaseConverter<CoinzoWithdrawStatus>
    {
        public WithdrawStatusConverter() : this(true) { }
        public WithdrawStatusConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<CoinzoWithdrawStatus, string>> Mapping => new List<KeyValuePair<CoinzoWithdrawStatus, string>>
        {
            new KeyValuePair<CoinzoWithdrawStatus, string>(CoinzoWithdrawStatus.SentConfirmationEmail, "1"),
            new KeyValuePair<CoinzoWithdrawStatus, string>(CoinzoWithdrawStatus.Pending, "2"),
            new KeyValuePair<CoinzoWithdrawStatus, string>(CoinzoWithdrawStatus.InProcess, "3"),
            new KeyValuePair<CoinzoWithdrawStatus, string>(CoinzoWithdrawStatus.Completed, "4"),
            new KeyValuePair<CoinzoWithdrawStatus, string>(CoinzoWithdrawStatus.Cancelled, "5"),
        };
    }
}