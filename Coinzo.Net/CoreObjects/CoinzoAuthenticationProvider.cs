using CryptoExchange.Net.Authentication;
using System;

namespace Coinzo.Net.CoreObjects
{
    public class CoinzoAuthenticationProvider : AuthenticationProvider
    {
        public CoinzoAuthenticationProvider(ApiCredentials credentials) : base(credentials)
        {
            if (credentials.Secret == null)
                throw new ArgumentException("No valid API credentials provided. Key/Secret needed.");
        }

    }
}