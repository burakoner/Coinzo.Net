using CryptoExchange.Net.Objects;

namespace Coinzo.Net.CoreObjects
{
    public class CoinzoSocketClientOptions : SocketClientOptions
    {
        public CoinzoSocketClientOptions(): base("wss://www.coinzo.com/ws")
        {
            SocketSubscriptionsCombineTarget = 100;
        }

        public CoinzoSocketClientOptions Copy()
        {
            var copy = Copy<CoinzoSocketClientOptions>();
            return copy;
        }
    }
}
