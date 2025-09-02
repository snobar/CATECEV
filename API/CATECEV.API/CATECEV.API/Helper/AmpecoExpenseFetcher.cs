using CATECEV.API.Models.AMPECO.resource.PartnerExpenses;
using CATECEV.CORE.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CATECEV.API.Helper
{
    public static class AmpecoExpenseFetcher
    {
        /// <summary>
        /// Fetches all partner expenses for the current month by querying one day at a time.
        /// Each day window is [dayStart, nextDay), at UTC midnight boundaries.
        /// </summary>
        public static async Task<List<AMPECOPartnerExpense>> FetchCurrentMonthDailyAsync(
            ApiService apiService,
            int ampecoPartnerId,
            string token,
            DateTime? nowUtcOverride = null,
            Uri baseAddress = null,
            int pageSize = 100,
            bool includeFutureDaysThisMonth = false) // false = stop at "tomorrow"; true = iterate to month end
        {
            if (apiService == null) throw new ArgumentNullException(nameof(apiService));
            if (pageSize <= 0) pageSize = 100;

            var baseUrl = (baseAddress ?? new Uri("https://shabikuae.eu.charge.ampeco.tech"))
                          .ToString().TrimEnd('/');

            var nowUtc = nowUtcOverride ?? DateTime.UtcNow;

            // Month bounds at UTC midnight
            var monthStartUtc = new DateTime(nowUtc.Year, nowUtc.Month, 1, 0, 0, 0, DateTimeKind.Utc);

            // End bound:
            // - If includeFutureDaysThisMonth=false: stop at "tomorrow" (so we include full "today")
            // - If true: go to first day of next month
            var tomorrowUtc = new DateTime(nowUtc.Year, nowUtc.Month, nowUtc.Day, 0, 0, 0, DateTimeKind.Utc).AddDays(1);
            var monthEndExclusiveUtc = includeFutureDaysThisMonth
                ? monthStartUtc.AddMonths(1)
                : tomorrowUtc;

            // Clamp end not to precede start (shouldn’t happen, but safe)
            if (monthEndExclusiveUtc <= monthStartUtc)
                monthEndExclusiveUtc = monthStartUtc.AddDays(1);

            var all = new List<AMPECOPartnerExpense>();

            // Iterate one day at a time
            for (var dayStart = monthStartUtc; dayStart < monthEndExclusiveUtc; dayStart = dayStart.AddDays(1))
            {
                var dayEnd = dayStart.AddDays(1);
                if (dayEnd > monthEndExclusiveUtc) dayEnd = monthEndExclusiveUtc;

                // Build daily URL
                var url =
                    $"{baseUrl}/public-api/resources/partner-expenses/v1.1" +
                    $"?filter[partnerId]={ampecoPartnerId}" +
                    $"&filter[dateAfter]={dayStart:yyyy-MM-dd}" +
                    $"&filter[dateBefore]={dayEnd:yyyy-MM-dd}" +
                    $"&page[size]={pageSize}";

                // Fetch all pages for the day
                List<AMPECOPartnerExpense> daily = null;
                try
                {
                    daily = await apiService.GetAllPaginatedDataAsync<AMPECOPartnerExpense>(url, token);
                }
                catch (Exception ex)
                {
                    // Optional: log and continue to next day
                    Console.Error.WriteLine($"Failed fetching {dayStart:yyyy-MM-dd}: {ex.Message}");
                }

                if (daily is { Count: > 0 })
                    all.AddRange(daily);
            }

            return all;
        }
    }

}
