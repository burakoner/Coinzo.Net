using Coinzo.Net.Converters;
using Coinzo.Net.CoreObjects;
using Coinzo.Net.Enums;
using Coinzo.Net.Interfaces;
using Coinzo.Net.RestObjects;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Coinzo.Net
{
    public class CoinzoClient : RestClient, IRestClient, ICoinzoClient
    {
        #region Fields
        protected static CoinzoClientOptions defaultOptions = new CoinzoClientOptions();
        protected static CoinzoClientOptions DefaultOptions => defaultOptions.Copy();

        // V1 Endpoints
        protected const string Endpoints_Public_Ticker = "ticker";
        protected const string Endpoints_Public_OrderBook = "order-book";
        protected const string Endpoints_Public_Trades = "trades";

        protected const string Endpoints_Private_Usage = "usage";
        protected const string Endpoints_Private_Balances = "balances";
        protected const string Endpoints_Private_PlaceOrder = "order/new";
        protected const string Endpoints_Private_OrderStatus = "order";
        protected const string Endpoints_Private_CancelOrder = "order";
        protected const string Endpoints_Private_CancelAllOrders = "orders";
        protected const string Endpoints_Private_ListOrders = "orders";
        protected const string Endpoints_Private_Fills = "fills";
        protected const string Endpoints_Private_DepositAddress = "deposit/address";
        protected const string Endpoints_Private_DepositList = "deposit/list";
        protected const string Endpoints_Private_Withdraw = "withdraw";
        protected const string Endpoints_Private_WithdrawList = "withdraw/list";
        #endregion

        #region Constructor / Destructor
        /// <summary>
        /// Create a new instance of CoinzoClient using the default options
        /// </summary>
        public CoinzoClient() : this(DefaultOptions)
        {
            requestBodyFormat = RequestBodyFormat.FormData;
        }

        /// <summary>
        /// Create a new instance of the CoinzoClient with the provided options
        /// </summary>
        public CoinzoClient(CoinzoClientOptions options) : base("Coinzo", options, options.ApiCredentials == null ? null : new CoinzoAuthenticationProvider(options.ApiCredentials))
        {
            requestBodyFormat = RequestBodyFormat.FormData;
            arraySerialization = ArrayParametersSerialization.MultipleValues;
        }
        #endregion

        #region Core Methods
        /// <summary>
        /// Sets the default options to use for new clients
        /// </summary>
        /// <param name="options">The options to use for new clients</param>
        public static void SetDefaultOptions(CoinzoClientOptions options)
        {
            defaultOptions = options;
        }

        /// <summary>
        /// Set the API key and secret
        /// </summary>
        /// <param name="apiKey">The api key</param>
        /// <param name="apiSecret">The api secret</param>
        public virtual void SetApiCredentials(string apiKey, string apiSecret)
        {
            SetAuthenticationProvider(new CoinzoAuthenticationProvider(new ApiCredentials(apiKey, apiSecret)));
        }
        #endregion

        #region Api Methods

        /// <summary>
        /// Snapshot information about the last trade (tick), 24h low, high price and volume.
        /// </summary>
        /// <param name="pair">A valid pair</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<CoinzoTicker> GetTickers(string pair, CancellationToken ct = default) => GetTickersAsync(pair, ct).Result;
        /// <summary>
        /// Snapshot information about the last trade (tick), 24h low, high price and volume.
        /// </summary>
        /// <param name="pair">A valid pair</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<CoinzoTicker>> GetTickersAsync(string pair, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "pair", pair },
            };
            return await SendRequest<CoinzoTicker>(GetUrl(Endpoints_Public_Ticker), method: HttpMethod.Get, cancellationToken: ct, checkResult: false, signed: false, parameters: parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Get a list of open orders for a pair
        /// </summary>
        /// <param name="pair">A valid pair</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<CoinzoOrderBook> GetOrderBook(string pair, CancellationToken ct = default) => GetOrderBookAsync(pair, ct).Result;
        /// <summary>
        /// Get a list of open orders for a pair
        /// </summary>
        /// <param name="pair">A valid pair</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<CoinzoOrderBook>> GetOrderBookAsync(string pair, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "pair", pair },
            };
            return await SendRequest<CoinzoOrderBook>(GetUrl(Endpoints_Public_OrderBook), method: HttpMethod.Get, cancellationToken: ct, checkResult: false, signed: false, parameters: parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// List the latest trades for a pair
        /// </summary>
        /// <param name="pair">A valid pair</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<CoinzoTrade>> GetTrades(string pair, CancellationToken ct = default) => GetTradesAsync(pair, ct).Result;
        /// <summary>
        /// List the latest trades for a pair
        /// </summary>
        /// <param name="pair">A valid pair</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<CoinzoTrade>>> GetTradesAsync(string pair, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "pair", pair },
            };
            return await SendRequest<IEnumerable<CoinzoTrade>>(GetUrl(Endpoints_Public_Trades), method: HttpMethod.Get, cancellationToken: ct, checkResult: false, signed: false, parameters: parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Get account fee, liquidity usage and volume info
        /// </summary>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<CoinzoUsage> GetUsage(CancellationToken ct = default) => GetUsageAsync(ct).Result;
        /// <summary>
        /// Get account fee, liquidity usage and volume info
        /// </summary>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<CoinzoUsage>> GetUsageAsync(CancellationToken ct = default)
        {
            return await SendRequest<CoinzoUsage>(GetUrl(Endpoints_Private_Usage), method: HttpMethod.Get, cancellationToken: ct, checkResult: false, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get account balances
        /// </summary>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<CoinzoBalance>> GetBalances(CancellationToken ct = default) => GetBalancesAsync(ct).Result;
        /// <summary>
        /// Get account balances
        /// </summary>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<CoinzoBalance>>> GetBalancesAsync(CancellationToken ct = default)
        {
            return await SendRequest<IEnumerable<CoinzoBalance>>(GetUrl(Endpoints_Private_Balances), method: HttpMethod.Get, cancellationToken: ct, checkResult: false, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Submit a new order
        /// </summary>
        /// <param name="pair">A valid pair</param>
        /// <param name="amount">Amount of coin to buy or sell</param>
        /// <param name="side">BUY or SELL</param>
        /// <param name="type">Type of order (LIMIT, MARKET, STOP OR STOP LIMIT)</param>
        /// <param name="limitPrice">Price of per coin that you buy or sell</param>
        /// <param name="stopPrice">[Optinal] Stop price for stop and stop limit orders</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<CoinzoOrderId> PlaceOrder(string pair, decimal amount, CoinzoOrderSide side, CoinzoOrderType type, decimal? limitPrice = null, decimal? stopPrice = null, CancellationToken ct = default) => PlaceOrderAsync(pair, amount, side, type, limitPrice, stopPrice, ct).Result;
        /// <summary>
        /// Submit a new order
        /// </summary>
        /// <param name="pair">A valid pair</param>
        /// <param name="amount">Amount of coin to buy or sell</param>
        /// <param name="side">BUY or SELL</param>
        /// <param name="type">Type of order (LIMIT, MARKET, STOP OR STOP LIMIT)</param>
        /// <param name="limitPrice">Price of per coin that you buy or sell</param>
        /// <param name="stopPrice">[Optinal] Stop price for stop and stop limit orders</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<CoinzoOrderId>> PlaceOrderAsync(string pair, decimal amount, CoinzoOrderSide side, CoinzoOrderType type, decimal? limitPrice = null, decimal? stopPrice = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "pair", pair },
                { "side", JsonConvert.SerializeObject(side, new OrderSideConverter(false)) },
                { "type", JsonConvert.SerializeObject(type, new OrderTypeConverter(false)) },
                { "amount", amount.ToString(CultureInfo.InvariantCulture) },
            };
            parameters.AddOptionalParameter("limitPrice", limitPrice?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("stopPrice", stopPrice?.ToString(CultureInfo.InvariantCulture));

            return await SendRequest<CoinzoOrderId>(GetUrl(Endpoints_Private_PlaceOrder), method: HttpMethod.Post, cancellationToken: ct, parameters: parameters, checkResult: false, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get status of order
        /// </summary>
        /// <param name="id">The ID of order to get status</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<CoinzoOrder> GetOrderStatus(long id, CancellationToken ct = default) => GetOrderStatusAsync(id, ct).Result;
        /// <summary>
        /// Get status of order
        /// </summary>
        /// <param name="id">The ID of order to get status</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<CoinzoOrder>> GetOrderStatusAsync(long id, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "id", id },
            };
            return await SendRequest<CoinzoOrder>(GetUrl(Endpoints_Private_OrderStatus), method: HttpMethod.Get, cancellationToken: ct, parameters: parameters, checkResult: false, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Cancel a previously placed order
        /// </summary>
        /// <param name="id">The ID of the order to delete</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<CoinzoOrderId> CancelOrder(long id, CancellationToken ct = default) => CancelOrderAsync(id, ct).Result;
        /// <summary>
        /// Cancel a previously placed order
        /// </summary>
        /// <param name="id">The ID of the order to delete</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<CoinzoOrderId>> CancelOrderAsync(long id, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "id", id },
            };
            return await SendRequest<CoinzoOrderId>(GetUrl(Endpoints_Private_CancelOrder), method: HttpMethod.Delete, cancellationToken: ct, parameters: parameters, checkResult: false, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Cancel all open orders
        /// </summary>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<CoinzoOrderId> CancelAllOrders(CancellationToken ct = default) => CancelAllOrdersAsync(ct).Result;
        /// <summary>
        /// Cancel all open orders
        /// </summary>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<CoinzoOrderId>> CancelAllOrdersAsync(CancellationToken ct = default)
        {
            return await SendRequest<CoinzoOrderId>(GetUrl(Endpoints_Private_CancelAllOrders), method: HttpMethod.Delete, cancellationToken: ct, checkResult: false, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// List your current open orders. Only open or un-settled orders are returned. As soon as an order is no longer open and settled, it will no longer appear in the default request.
        /// </summary>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<CoinzoOrder>> GetOrders(CancellationToken ct = default) => GetOrdersAsync(ct).Result;
        /// <summary>
        /// List your current open orders. Only open or un-settled orders are returned. As soon as an order is no longer open and settled, it will no longer appear in the default request.
        /// </summary>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<CoinzoOrder>>> GetOrdersAsync(CancellationToken ct = default)
        {
            return await SendRequest<IEnumerable<CoinzoOrder>>(GetUrl(Endpoints_Private_ListOrders), method: HttpMethod.Get, cancellationToken: ct, checkResult: false, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get a list of recent fills.
        /// </summary>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<CoinzoFill>> GetFills(CancellationToken ct = default) => GetFillsAsync(ct).Result;
        /// <summary>
        /// Get a list of recent fills.
        /// </summary>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<CoinzoFill>>> GetFillsAsync(CancellationToken ct = default)
        {
            return await SendRequest<IEnumerable<CoinzoFill>>(GetUrl(Endpoints_Private_Fills), method: HttpMethod.Get, cancellationToken: ct, checkResult: false, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Show deposit address for asset
        /// </summary>
        /// <param name="asset">A valid asset</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<CoinzoDepositAddress> GetDepositAddress(string asset, CancellationToken ct = default) => GetDepositAddressAsync(asset, ct).Result;
        /// <summary>
        /// Show deposit address for asset
        /// </summary>
        /// <param name="asset">A valid asset</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<CoinzoDepositAddress>> GetDepositAddressAsync(string asset, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "asset", asset },
            };
            return await SendRequest<CoinzoDepositAddress>(GetUrl(Endpoints_Private_DepositAddress), method: HttpMethod.Get, cancellationToken: ct, checkResult: false, signed: true, parameters: parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// List your deposit history
        /// </summary>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<CoinzoDeposit>> GetDepositHistory(CancellationToken ct = default) => GetDepositHistoryAsync(ct).Result;
        /// <summary>
        /// List your deposit history
        /// </summary>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<CoinzoDeposit>>> GetDepositHistoryAsync(CancellationToken ct = default)
        {
            return await SendRequest<IEnumerable<CoinzoDeposit>>(GetUrl(Endpoints_Private_DepositList), method: HttpMethod.Get, cancellationToken: ct, checkResult: false, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Withdraws funds to a crypto address.
        /// </summary>
        /// <param name="asset">A valid asset</param>
        /// <param name="address">A crypto address of the recipient</param>
        /// <param name="amount">The amount to withdraw</param>
        /// <param name="tag">[Optional] Destination tag for XRP withdraws</param>
        /// <param name="memo">[Optional] Memo for EOS withdraws</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<CoinzoWithdraw> Withdraw(string asset, string address, decimal amount, string tag = null, string memo = null, CancellationToken ct = default) => WithdrawAsync(asset, address, amount, tag, memo, ct).Result;
        /// <summary>
        /// Withdraws funds to a crypto address.
        /// </summary>
        /// <param name="asset">A valid asset</param>
        /// <param name="address">A crypto address of the recipient</param>
        /// <param name="amount">The amount to withdraw</param>
        /// <param name="tag">[Optional] Destination tag for XRP withdraws</param>
        /// <param name="memo">[Optional] Memo for EOS withdraws</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<CoinzoWithdraw>> WithdrawAsync(string asset, string address, decimal amount, string tag = null, string memo = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "asset", asset },
                { "address", address },
                { "amount", amount },
            };
            parameters.AddOptionalParameter("tag", tag);
            parameters.AddOptionalParameter("memo", memo);

            return await SendRequest<CoinzoWithdraw>(GetUrl(Endpoints_Private_Withdraw), method: HttpMethod.Post, cancellationToken: ct, parameters: parameters, checkResult: false, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// List your withdraw history
        /// </summary>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<CoinzoWithdrawal>> GetWithdrawHistory(CancellationToken ct = default) => GetWithdrawHistoryAsync(ct).Result;
        /// <summary>
        /// List your withdraw history
        /// </summary>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<CoinzoWithdrawal>>> GetWithdrawHistoryAsync(CancellationToken ct = default)
        {
            return await SendRequest<IEnumerable<CoinzoWithdrawal>>(GetUrl(Endpoints_Private_WithdrawList), method: HttpMethod.Get, cancellationToken: ct, checkResult: false, signed: true).ConfigureAwait(false);
        }
        #endregion

        #region Protected Methods
        protected override Error ParseErrorResponse(JToken error)
        {
            return this.CoinzoParseErrorResponse(error);
        }
        protected virtual Error CoinzoParseErrorResponse(JToken error)
        {
            if (error["error"] == null)
                return new ServerError(error.ToString());

            var err = "";
            if (error["error"] is JArray && error["error"][0] != null) err = (string)error["error"][0];
            else err = error["error"].ToString();

            return new ServerError(err);
        }

        protected virtual Uri GetUrl(string endpoint)
        {
            return new Uri($"{BaseAddress.TrimEnd('/')}/{endpoint}");
        }
        #endregion

    }
}
