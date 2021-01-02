using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Coinzo.Net.RestObjects
{
    public class CoinzoUsage
    {
        [JsonProperty("fee")]
        public CoinzoUsageFee Fee { get; set; }

        [JsonProperty("usage")]
        public CoinzoUsageLiquidity Liquidity { get; set; }
        
        [JsonProperty("volume")]
        public CoinzoUsageVolume Volume { get; set; }
    }

    public class CoinzoUsageFee
    {
        [JsonProperty("use_cnz")]
        public bool UseCNZ { get; set; }
        
        [JsonProperty("maker")]
        public decimal Maker { get; set; }
        
        [JsonProperty("taker")]
        public decimal Taker { get; set; }
    }
    
    public class CoinzoUsageLiquidity
    {
        [JsonProperty("daily_liquidity")]
        public decimal DailyLiquidity { get; set; }
        
        [JsonProperty("monthly_liquidity")]
        public decimal MonthlyLiquidity { get; set; }
        
        [JsonProperty("daily_liquidity_limit")]
        public decimal DailyLiquidityLimit { get; set; }
        
        [JsonProperty("monthly_liquidity_limit")]
        public decimal MonthlyLiquidityLimit { get; set; }
    }

    public class CoinzoUsageVolume
    {
        [JsonProperty("total")]
        public decimal Total { get; set; }
        
        [JsonProperty("updated_at"), JsonConverter(typeof(TimestampSecondsConverter))]
        public DateTime UpdatedAt { get; set; }
    }
}