using Coinzo.Net.CoreObjects;
using Coinzo.Net.Enums;
using CryptoExchange.Net.Logging;
using System;

namespace Coinzo.Net.Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            /* Rest Api Client */
            var api = new CoinzoClient();
            api.SetApiCredentials("XXXXXXXX-API-KEY-XXXXXXXX", "XXXXXXXX-API-SECRET-XXXXXXXX");

            /* Public Endpoints */
            var tickers = api.GetTickers("BTC-TRY");
            var orderbook = api.GetOrderBook("BTC-TRY");
            var trades = api.GetTrades("BTC-TRY");

            /* Private Endpoints */
            var usage = api.GetUsage();
            var balances = api.GetBalances();
            var place_order = api.PlaceOrder("CNZ-TRY", 100, CoinzoOrderSide.Sell, CoinzoOrderType.Limit, 0.40m);
            var order_status = api.GetOrderStatus(359420493762035717);
            var cancel_order = api.CancelOrder(359420493762035717);
            var cancel_all_orders = api.CancelAllOrders();
            var orders = api.GetOrders();
            var fills = api.GetFills();
            var address = api.GetDepositAddress("BTC");
            var deposits = api.GetDepositHistory();
            var withdraw = api.Withdraw("ETH", "0x65b02db9b67b73f5f1e983ae10796f91ded57b64", 1.15m);
            var withdrawals = api.GetWithdrawHistory();

            /* Web Socket Api Client */
            var ws = new CoinzoSocketClient();
            var ws01 = ws.SubscribeToTrades("BTC-TRY", (data) =>
             {
                 if (data != null)
                 {
                     Console.WriteLine($"Trade Update >> {data.Pair} >> TID:{data.TradeId} OID:{data.OrderId} A:{data.Amount}");
                 }
             });

            var ws02 = ws.SubscribeToTicker("BTC-TRY", (data) =>
             {
                 if (data != null)
                 {
                     Console.WriteLine($"Ticker Update >> {data.Pair} >> C:{data.Change} CP:{data.ChangePercent} O:{data.Open} H:{data.High} L:{data.Low} C:{data.Close} V:{data.Volume}");
                 }
             });

            var ws03 = ws.SubscribeToCandles("BTC-TRY", CoinzoPeriod.OneHour, (data) =>
            {
                if (data != null)
                {
                    Console.WriteLine($"Candle Update >> {data.Pair} >> C:{data.Period} O:{data.Open} H:{data.High} L:{data.Low} C:{data.Close} V:{data.Volume}");
                }
            });

            var ws04 = ws.SubscribeToOrderBook("BTC-TRY", (data) =>
            {
                if (data != null)
                {
                    Console.WriteLine($"Book Total Update >> {data.Pair} >> A:{data.Amount} V:{data.Value}");
                }
            }, (data) =>
            {
                if (data != null)
                {
                    Console.WriteLine($"Book Entry Update >> {data.Pair} >> A:{data.Amount} C:{data.Count} OID:{data.OrderId}");
                }
            });

            Console.ReadLine();
        }
    }
}
