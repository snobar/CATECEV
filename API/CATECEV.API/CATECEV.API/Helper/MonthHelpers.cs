namespace CATECEV.API.Helper
{
    static class MonthHelpers
    {
        public static int ToYYYYMM(DateTime dt) => dt.Year * 100 + dt.Month;

        public static (DateTime fromUtc, DateTime toUtcExcl) Bounds(int yyyymm)
        {
            var y = yyyymm / 100; var m = yyyymm % 100;
            var from = new DateTime(y, m, 1, 0, 0, 0, DateTimeKind.Utc);
            return (from, from.AddMonths(1));
        }

        public static (DateTime fromUtc, DateTime toUtcExcl) CurrentMonthBoundsUtc(DateTime nowUtc)
        {
            var start = new DateTime(nowUtc.Year, nowUtc.Month, 1, 0, 0, 0, DateTimeKind.Utc);
            return (start, start.AddMonths(1));
        }
    }
}
