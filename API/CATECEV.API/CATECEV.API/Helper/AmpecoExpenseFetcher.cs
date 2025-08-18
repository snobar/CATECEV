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
        public static async Task<List<AMPECOPartnerExpense>> FetchCurrentMonthTwoWindowAsync(
            ApiService apiService,
            int ampecoPartnerId,
            string token,
            DateTime? nowUtcOverride = null,
            Uri baseAddress = null)
        {
            if (apiService == null) throw new ArgumentNullException(nameof(apiService));

            var baseUrl = (baseAddress ?? new Uri("https://shabikuae.eu.charge.ampeco.tech"))
                          .ToString().TrimEnd('/');

            var nowUtc = nowUtcOverride ?? DateTime.UtcNow;

            // Month bounds at UTC midnight
            var monthStartUtc = new DateTime(nowUtc.Year, nowUtc.Month, 1, 0, 0, 0, DateTimeKind.Utc);
            var todayStartUtc = new DateTime(nowUtc.Year, nowUtc.Month, nowUtc.Day, 0, 0, 0, DateTimeKind.Utc);
            var tomorrowUtc = todayStartUtc.AddDays(1);

            // Clamp recent-from so it never goes before the 1st of this month
            var recentFromUtc = todayStartUtc.AddDays(-2);
            if (recentFromUtc < monthStartUtc) recentFromUtc = monthStartUtc;

            // Early-month window: [monthStart, recentFrom) — may be empty in first 2 days
            var earlyStartUtc = monthStartUtc;
            var earlyEndUtc = recentFromUtc;      // exclusive

            // Recent window: [recentFrom, tomorrow) — includes full "today"
            var recentEndUtc = tomorrowUtc;        // exclusive

            // Build URLs (ApiService normalizes to Z and follows pagination)
            string earlyUrl =
                $"{baseUrl}/public-api/resources/partner-expenses/v1.1" +
                $"?filter[partnerId]={ampecoPartnerId}" +
                $"&filter[dateAfter]={earlyStartUtc:yyyy-MM-dd}" +
                $"&filter[dateBefore]={earlyEndUtc:yyyy-MM-dd}" +
                $"&page[size]=100";

            string recentUrl =
                $"{baseUrl}/public-api/resources/partner-expenses/v1.1" +
                $"?filter[partnerId]={ampecoPartnerId}" +
                $"&filter[dateAfter]={recentFromUtc:yyyy-MM-dd}" +
                $"&filter[dateBefore]={recentEndUtc:yyyy-MM-dd}" +
                $"&page[size]=100";

            var result = new List<AMPECOPartnerExpense>();

            // Only call the early window if it’s non-empty
            if (earlyEndUtc > earlyStartUtc)
            {
                var early = await apiService.GetAllPaginatedDataAsync<AMPECOPartnerExpense>(earlyUrl, token);
                result.AddRange(early);
            }

            var recent = await apiService.GetAllPaginatedDataAsync<AMPECOPartnerExpense>(recentUrl, token);
            result.AddRange(recent);

            return result;
        }
    }
}
