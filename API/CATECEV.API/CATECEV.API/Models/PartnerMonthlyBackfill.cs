using CATECEV.API.Helper;
using CATECEV.API.Models.AMPECO.resource.PartnerExpenses;
using CATECEV.CORE.Wrapper;
using CATECEV.Data.Context;
using CATECEV.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace CATECEV.API.Models
{
    // a quick service/endpoint to prefill all months up to last month. It calls AMPECO month-by-month
    public class PartnerMonthlyBackfill
    {
        private readonly AppDBContext _db;
        private readonly ApiService _api;
        private readonly string _token;

        public PartnerMonthlyBackfill(AppDBContext db, ApiService api, string token)
        { _db = db; _api = api; _token = token; }

        public async Task RunAsync(int partnerId, DateTime startFromUtc)
        {
            var partner = await _db.Partner.FirstOrDefaultAsync(p => p.Id == partnerId);
            if (partner == null || partner.AMPECOId == null) return;

            var nowUtc = DateTime.UtcNow;
            var cursor = new DateTime(startFromUtc.Year, startFromUtc.Month, 1, 0, 0, 0, DateTimeKind.Utc);
            var currentMonthStart = new DateTime(nowUtc.Year, nowUtc.Month, 1, 0, 0, 0, DateTimeKind.Utc);

            while (cursor < currentMonthStart) // only closed months
            {
                var yyyymm = MonthHelpers.ToYYYYMM(cursor);
                bool exists = await _db.PartnerMonthlyCalculationTransactions
                    .AnyAsync(x => x.PartnerId == partnerId && x.MonthValue == yyyymm);
                if (!exists)
                {
                    var (fromUtc, toUtcExcl) = MonthHelpers.Bounds(yyyymm);
                    string url =
                        $"https://shabikuae.eu.charge.ampeco.tech/public-api/resources/partner-expenses/v1.1" +
                        $"?filter[partnerId]={partner.AMPECOId}&filter[dateAfter]={fromUtc:O}&filter[dateBefore]={toUtcExcl:O}";

                    var monthItems = await _api.GetAllPaginatedDataAsync<AMPECOPartnerExpense>(url, _token);
                    var totalWithoutTax = monthItems.Sum(r => r.TotalAmount?.WithoutTax ?? 0m);

                    _db.PartnerMonthlyCalculationTransactions.Add(new PartnerMonthlyCalculationTransaction
                    {
                        PartnerId = partnerId,
                        MonthValue = yyyymm,
                        TotalAmount = totalWithoutTax,
                        IsActive = true
                    });
                    await _db.SaveChangesAsync();
                }
                cursor = cursor.AddMonths(1);
            }
        }
    }

}
