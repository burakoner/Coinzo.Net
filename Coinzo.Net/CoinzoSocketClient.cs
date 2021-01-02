using Coinzo.Net.Converters;
using Coinzo.Net.CoreObjects;
using Coinzo.Net.Enums;
using Coinzo.Net.Interfaces;
using Coinzo.Net.SocketObjects;
using CryptoExchange.Net;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coinzo.Net
{
    /// <summary>
    /// Client for the Coinzo Websocket API
    /// </summary>
    public partial class CoinzoSocketClient : SocketClient, ISocketClient, ICoinzoSocketClient
    {
        private SortedDictionary<string, string> SubscriptionChannels = new SortedDictionary<string, string>();

        #region Client Options
        private static CoinzoSocketClientOptions defaultOptions = new CoinzoSocketClientOptions();
        private static CoinzoSocketClientOptions DefaultOptions => defaultOptions.Copy();
        #endregion

        #region Constructor/Destructor
        /// <summary>
        /// Create a new instance of CoinzoSocketClient with default options
        /// </summary>
        public CoinzoSocketClient() : this(DefaultOptions)
        {
        }

        /// <summary>
        /// Create a new instance of CoinzoSocketClient using provided options
        /// </summary>
        /// <param name="options">The options to use for this client</param>
        public CoinzoSocketClient(CoinzoSocketClientOptions options) : base("Coinzo", options, options.ApiCredentials == null ? null : new CoinzoAuthenticationProvider(options.ApiCredentials))
        {
        }
        #endregion

        #region Common Methods
        /// <summary>
        /// Set the default options to be used when creating new socket clients
        /// </summary>
        /// <param name="options">The options to use for new clients</param>
        public static void SetDefaultOptions(CoinzoSocketClientOptions options)
        {
            defaultOptions = options;
        }
        #endregion

        #region Web Socket Feeds
        public CallResult<UpdateSubscription> SubscribeToTrades(string pair, Action<CoinzoSocketTrade> onData) => SubscribeToTradesAsync(pair, onData).Result;
        public async Task<CallResult<UpdateSubscription>> SubscribeToTradesAsync(string pair, Action<CoinzoSocketTrade> onData)
        {
            var id = NextSubscriptionId();
            var key = $"{string.Format("{0:0000000000}", id)}-trades-{pair}";
            SubscriptionChannels.Add(key, string.Empty);
            var internalHandler = new Action<IEnumerable<object>>(data =>
            {
                if (SubscriptionChannels.ContainsKey(key))
                {
                    if (data.Count() > 0 && (string)data.ElementAt(0) == SubscriptionChannels[key])
                    {
                        for (var i = 1; i < data.Count(); i++)
                        {
                            var trade = JsonConvert.DeserializeObject<CoinzoSocketTrade>(data.ElementAt(i).ToString());
                            trade.Pair = pair;
                            onData(trade);
                        }
                    }
                }
            });

            var request = new CoinzoSocketRequest { Event = "subscribe", Channel = "trades", Pair = pair, SubscriptionKey = key };
            return await Subscribe(request, null, false, internalHandler).ConfigureAwait(false);
        }

        public CallResult<UpdateSubscription> SubscribeToTicker(string pair, Action<CoinzoSocketTicker> onData) => SubscribeToTickerAsync(pair, onData).Result;
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerAsync(string pair, Action<CoinzoSocketTicker> onData)
        {
            var id = NextSubscriptionId();
            var key = $"{string.Format("{0:0000000000}", id)}-ticker-{pair}";
            SubscriptionChannels.Add(key, string.Empty);
            var internalHandler = new Action<IEnumerable<object>>(data =>
            {
                if (SubscriptionChannels.ContainsKey(key))
                {
                    if (data.Count() > 0 && (string)data.ElementAt(0) == SubscriptionChannels[key])
                    {
                        for (var i = 1; i < data.Count(); i++)
                        {
                            var ticker = JsonConvert.DeserializeObject<CoinzoSocketTicker>(data.ElementAt(i).ToString());
                            ticker.Pair = pair;
                            ticker.Open = ticker.Close - ticker.Change;
                            onData(ticker);
                        }
                    }
                }
            });

            var request = new CoinzoSocketRequest { Event = "subscribe", Channel = "ticker", Pair = pair, SubscriptionKey = key };
            return await Subscribe(request, null, false, internalHandler).ConfigureAwait(false);
        }

        public CallResult<UpdateSubscription> SubscribeToCandles(string pair, CoinzoPeriod period, Action<CoinzoSocketCandle> onData) => SubscribeToCandlesAsync(pair, period, onData).Result;
        public async Task<CallResult<UpdateSubscription>> SubscribeToCandlesAsync(string pair, CoinzoPeriod period, Action<CoinzoSocketCandle> onData)
        {
            var id = NextSubscriptionId();
            var key = $"{string.Format("{0:0000000000}", id)}-candles-{pair}";
            SubscriptionChannels.Add(key, string.Empty);
            var internalHandler = new Action<IEnumerable<object>>(data =>
            {
                if (SubscriptionChannels.ContainsKey(key))
                {
                    if (data.Count() > 0 && (string)data.ElementAt(0) == SubscriptionChannels[key])
                    {
                        for (var i = 1; i < data.Count(); i++)
                        {
                            var candles = JsonConvert.DeserializeObject<IEnumerable<CoinzoSocketCandle>>(data.ElementAt(i).ToString());
                            foreach (var candle in candles)
                            {
                                candle.Pair = pair;
                                candle.Period = period;
                                onData(candle);
                            }
                        }
                    }
                }
            });

            var period_s = JsonConvert.SerializeObject(period, new PeriodConverter(false));
            var request = new CoinzoSocketRequest { Event = "subscribe", Channel = "candles", Pair = pair, Key = period_s, SubscriptionKey = key };
            return await Subscribe(request, null, false, internalHandler).ConfigureAwait(false);
        }

        public CallResult<UpdateSubscription> SubscribeToOrderBook(string pair, Action<CoinzoSocketOrderBookTotal> onTotalData, Action<CoinzoSocketOrderBookEntry> onEntryData) => SubscribeToOrderBookAsync(pair, onTotalData, onEntryData).Result;
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookAsync(string pair, Action<CoinzoSocketOrderBookTotal> onTotalData, Action<CoinzoSocketOrderBookEntry> onEntryData)
        {
            var id = NextSubscriptionId();
            var key = $"{string.Format("{0:0000000000}", id)}-book-{pair}";
            SubscriptionChannels.Add(key, string.Empty);
            var internalHandler = new Action<IEnumerable<object>>(data =>
            {
                if (SubscriptionChannels.ContainsKey(key))
                {
                    if (data.Count() > 0 && (string)data.ElementAt(0) == SubscriptionChannels[key])
                    {
                        for (var i = 1; i < data.Count(); i++)
                        {
                            if (i == 1)
                            {
                                var total = JsonConvert.DeserializeObject<CoinzoSocketOrderBookTotal>(data.ElementAt(i).ToString());
                                total.Pair = pair;
                                onTotalData(total);
                            }
                            else
                            {
                                var entry = JsonConvert.DeserializeObject<CoinzoSocketOrderBookEntry>(data.ElementAt(i).ToString());
                                entry.Pair = pair;
                                onEntryData(entry);
                            }
                        }
                    }
                }
            });

            var request = new CoinzoSocketRequest { Event = "subscribe", Channel = "book", Pair = pair, Precision = 0, SubscriptionKey = key };
            return await Subscribe(request, null, false, internalHandler).ConfigureAwait(false);
        }
        #endregion

        #region Core Methods
        protected override bool HandleQueryResponse<T>(SocketConnection s, object request, JToken data, out CallResult<T> callResult)
        {
            callResult = new CallResult<T>(default, null);
            return false;
        }

        protected override bool HandleSubscriptionResponse(SocketConnection s, SocketSubscription subscription, object request, JToken message, out CallResult<object> callResult)
        {
            callResult = null;

            // Check for Success
            if (request is CoinzoSocketRequest socRequest)
            {
                if (socRequest.Event == "subscribe")
                {
                    if (message["chanId"] != null && message["channel"] != null && message["event"] != null && message["pair"] != null)
                    {
                        var channelId = (string)message["chanId"];
                        var channel = (string)message["channel"];
                        var result = (string)message["event"];
                        var pair = (string)message["pair"];
                        if (channel == socRequest.Channel && pair == socRequest.Pair && result == "subscribed")
                        {
                            if (SubscriptionChannels.ContainsKey(socRequest.SubscriptionKey))
                            {
                                SubscriptionChannels[socRequest.SubscriptionKey] = channelId;
                                callResult = new CallResult<object>(true, null);
                                return true;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        protected override bool MessageMatchesHandler(JToken data, object request)
        {
            if (request is CoinzoSocketRequest socRequest)
            {
                if (socRequest.Event == "subscribe")
                {
                    if (SubscriptionChannels.ContainsKey(socRequest.SubscriptionKey))
                    {
                        var channelId = SubscriptionChannels[socRequest.SubscriptionKey];
                        var msg = data.ToString().Replace("\r", "").Replace("\n", "").Replace(" ", "");

                        if (msg.StartsWith($"[\"{channelId}\","))
                            return true;
                    }
                }
            }

            return false;
        }

        protected override bool MessageMatchesHandler(JToken message, string identifier)
        {
            return true;
        }

        protected override SocketConnection GetWebsocket(string address, bool authenticated)
        {
            address = address.TrimEnd('/');
            var socketResult = sockets.Where(s =>
                s.Value.Socket.Url.TrimEnd('/') == address.TrimEnd('/') &&
                (s.Value.Authenticated == authenticated || !authenticated) &&
                s.Value.Connected).OrderBy(s => s.Value.HandlerCount).FirstOrDefault();
            var result = socketResult.Equals(default(KeyValuePair<int, SocketConnection>)) ? null : socketResult.Value;
            if (result != null)
            {
                if (result.HandlerCount < SocketCombineTarget || (sockets.Count >= MaxSocketConnections && sockets.All(s => s.Value.HandlerCount >= SocketCombineTarget)))
                {
                    // Use existing socket if it has less than target connections OR it has the least connections and we can't make new
                    return result;
                }
            }

            // Create new socket
            var socket = CreateSocket(address);
            var socketWrapper = new SocketConnection(this, socket);
            foreach (var kvp in genericHandlers)
            {
                var handler = SocketSubscription.CreateForIdentifier(kvp.Key, false, kvp.Value);
                socketWrapper.AddHandler(handler);
            }

            return socketWrapper;
        }

        protected override async Task<bool> Unsubscribe(SocketConnection connection, SocketSubscription s)
        {
            if (s == null || s.Request == null)
                return false;

            var channelId = ((CoinzoSocketRequest)s.Request).SubscriptionKey;
            var request = new CoinzoSocketRequest { Event = "unsubscribe", ChannelId = channelId };
            await connection.SendAndWait(request, ResponseTimeout, data =>
            {
                if (data.Type != JTokenType.Object)
                    return false;

                if (data["event"] != null && (string)data["event"] == "unsubscribed"
                && data["chanId"] == null && (string)data["chanId"] == channelId
                && data["status"] == null && (string)data["status"] == "OK")
                {
                    return true;
                }

                return false;
            });

            return false;
        }

        protected override Task<CallResult<bool>> AuthenticateSocket(SocketConnection s)
        {
            throw new NotImplementedException();
        }

        private int iterator = 0;
        private int NextSubscriptionId()
        {
            return ++iterator;
        }

        #endregion

    }
}
