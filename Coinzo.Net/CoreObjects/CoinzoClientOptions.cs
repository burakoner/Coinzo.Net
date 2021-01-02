using CryptoExchange.Net.Objects;

namespace Coinzo.Net.CoreObjects
{
    public class CoinzoClientOptions: RestClientOptions
    {
        public CoinzoClientOptions():base("https://api.coinzo.com")
        {
        }

        public CoinzoClientOptions Copy()
        {
            return Copy<CoinzoClientOptions>();
        }
    }
}
