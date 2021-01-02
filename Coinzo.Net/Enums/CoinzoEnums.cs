namespace Coinzo.Net.Enums
{
    public enum CoinzoOrderSide
    {
        Buy,
        Sell,
    }

    public enum CoinzoOrderType
    {
        Limit,
        Market,
        Stop,
        StopLimit,
    }
    
    public enum CoinzoWithdrawStatus
    {
        SentConfirmationEmail,
        Pending,
        InProcess,
        Completed,
        Cancelled,
    }

    public enum CoinzoPeriod
    {
        OneMinute,
        FiveMinutes,
        FifteenMinutes,
        ThirtyMinutes,
        OneHour,
        ThreeHours,
        SixHours,
        TwelveHours,
        OneDay,
        OneWeek,
    }

}
