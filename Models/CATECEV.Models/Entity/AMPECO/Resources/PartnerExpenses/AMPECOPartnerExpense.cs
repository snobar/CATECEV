using CATECEV.Models.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CATECEV.Models.Entity.AMPECO.Resources.PartnerExpenses
{
    public class AMPECOPartnerExpense : BaseEntity
    {
        public int AMPECOId { get; set; }
        public int PartnerId { get; set; }
        public string PartnerName { get; set; }
        public DateTime Date { get; set; }
        public string Origin { get; set; }
        public TotalAmount TotalAmount { get; set; }
        public string CurrencyCode { get; set; }
        public int SettlementReportId { get; set; }
        public int ChargingSessionId { get; set; }
        public DateTime StartedAt { get; set; }
        public string Duration { get; set; }
        public string ChargePointName { get; set; }
        public string Authorization { get; set; }
        public decimal OperatorDiscountPercentage { get; set; }
        public decimal OperatorDiscountAmount { get; set; }
    }
    public class PartnerExpensesLinks
    {
        public string First { get; set; }
        public string Last { get; set; }
        public string Prev { get; set; }
        public string Next { get; set; }
    }
}
