using Coinzo.Net.Enums;
using Coinzo.Net.RestObjects;
using CryptoExchange.Net.Objects;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Coinzo.Net.Interfaces
{
    public interface ICoinzoClient
    {
        void SetApiCredentials(string apiKey, string apiSecret);
        WebCallResult<CoinzoOrderId> CancelAllOrders(CancellationToken ct = default);
        Task<WebCallResult<CoinzoOrderId>> CancelAllOrdersAsync(CancellationToken ct = default);
        WebCallResult<CoinzoOrderId> CancelOrder(long id, CancellationToken ct = default);
        Task<WebCallResult<CoinzoOrderId>> CancelOrderAsync(long id, CancellationToken ct = default);
        WebCallResult<IEnumerable<CoinzoBalance>> GetBalances(CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<CoinzoBalance>>> GetBalancesAsync(CancellationToken ct = default);
        WebCallResult<CoinzoDepositAddress> GetDepositAddress(string asset, CancellationToken ct = default);
        Task<WebCallResult<CoinzoDepositAddress>> GetDepositAddressAsync(string asset, CancellationToken ct = default);
        WebCallResult<IEnumerable<CoinzoDeposit>> GetDepositHistory(CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<CoinzoDeposit>>> GetDepositHistoryAsync(CancellationToken ct = default);
        WebCallResult<IEnumerable<CoinzoFill>> GetFills(CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<CoinzoFill>>> GetFillsAsync(CancellationToken ct = default);
        WebCallResult<CoinzoOrderBook> GetOrderBook(string symbol, CancellationToken ct = default);
        Task<WebCallResult<CoinzoOrderBook>> GetOrderBookAsync(string symbol, CancellationToken ct = default);
        WebCallResult<IEnumerable<CoinzoOrder>> GetOrders(CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<CoinzoOrder>>> GetOrdersAsync(CancellationToken ct = default);
        WebCallResult<CoinzoOrder> GetOrderStatus(long id, CancellationToken ct = default);
        Task<WebCallResult<CoinzoOrder>> GetOrderStatusAsync(long id, CancellationToken ct = default);
        WebCallResult<CoinzoTicker> GetTickers(string symbol, CancellationToken ct = default);
        Task<WebCallResult<CoinzoTicker>> GetTickersAsync(string symbol, CancellationToken ct = default);
        WebCallResult<IEnumerable<CoinzoTrade>> GetTrades(string symbol, CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<CoinzoTrade>>> GetTradesAsync(string symbol, CancellationToken ct = default);
        WebCallResult<CoinzoUsage> GetUsage(CancellationToken ct = default);
        Task<WebCallResult<CoinzoUsage>> GetUsageAsync(CancellationToken ct = default);
        WebCallResult<IEnumerable<CoinzoWithdrawal>> GetWithdrawHistory(CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<CoinzoWithdrawal>>> GetWithdrawHistoryAsync(CancellationToken ct = default);
        WebCallResult<CoinzoOrderId> PlaceOrder(string pair, decimal amount, CoinzoOrderSide side, CoinzoOrderType type, decimal? limitPrice = null, decimal? stopPrice = null, CancellationToken ct = default);
        Task<WebCallResult<CoinzoOrderId>> PlaceOrderAsync(string pair, decimal amount, CoinzoOrderSide side, CoinzoOrderType type, decimal? limitPrice = null, decimal? stopPrice = null, CancellationToken ct = default);
        WebCallResult<CoinzoWithdraw> Withdraw(string asset, string address, decimal amount, string tag = null, string memo = null, CancellationToken ct = default);
        Task<WebCallResult<CoinzoWithdraw>> WithdrawAsync(string asset, string address, decimal amount, string tag = null, string memo = null, CancellationToken ct = default);
    }
}
