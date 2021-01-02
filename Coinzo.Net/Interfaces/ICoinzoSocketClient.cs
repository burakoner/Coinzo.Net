using Coinzo.Net.Enums;
using Coinzo.Net.SocketObjects;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using System;
using System.Threading.Tasks;

namespace Coinzo.Net.Interfaces
{
    public interface ICoinzoSocketClient
    {
        CallResult<UpdateSubscription> SubscribeToCandles(string pair, CoinzoPeriod period, Action<CoinzoSocketCandle> onData);
        Task<CallResult<UpdateSubscription>> SubscribeToCandlesAsync(string pair, CoinzoPeriod period, Action<CoinzoSocketCandle> onData);
        CallResult<UpdateSubscription> SubscribeToOrderBook(string pair, Action<CoinzoSocketOrderBookTotal> onTotalData, Action<CoinzoSocketOrderBookEntry> onEntryData);
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookAsync(string pair, Action<CoinzoSocketOrderBookTotal> onTotalData, Action<CoinzoSocketOrderBookEntry> onEntryData);
        CallResult<UpdateSubscription> SubscribeToTicker(string pair, Action<CoinzoSocketTicker> onData);
        Task<CallResult<UpdateSubscription>> SubscribeToTickerAsync(string pair, Action<CoinzoSocketTicker> onData);
        CallResult<UpdateSubscription> SubscribeToTrades(string pair, Action<CoinzoSocketTrade> onData);
        Task<CallResult<UpdateSubscription>> SubscribeToTradesAsync(string pair, Action<CoinzoSocketTrade> onData);
    }
}
