namespace CATECEV.FE.Models.ViewModels
{
    public class AMPECOPartnerExpenseViewModel
    {
        public int Id { get; set; }
        public int PartnerId { get; set; }
        public string PartnerName { get; set; }
        public DateTime Date { get; set; }
        public string Origin { get; set; }
        public TotalAmount TotalAmount { get; set; }
        public string CurrencyCode { get; set; }
        public int? SettlementReportId { get; set; }
        public int ChargingSessionId { get; set; }
        public DateTime StartedAt { get; set; }
        public string Duration { get; set; }
        public string ChargePointName { get; set; }
        public string Authorization { get; set; }
        public decimal OperatorDiscountPercentage { get; set; }
        public decimal OperatorDiscountAmount { get; set; }
    }

    public class TotalAmount
    {
        public decimal WithoutTax { get; set; }
    }
}
