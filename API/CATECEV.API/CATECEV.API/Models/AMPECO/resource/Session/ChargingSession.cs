namespace CATECEV.API.Models.AMPECO.resource.Session
{
    public class ChargingSession
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ChargePointId { get; set; }
        public int EvseId { get; set; }
        public int? ConnectorId { get; set; }
        public string Status { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime StoppedAt { get; set; }
        public int Energy { get; set; }
        public double PowerKw { get; set; }
        public decimal Amount { get; set; }
        public TotalAmount TotalAmount { get; set; }
        public TaxDetails Tax { get; set; }
        public string Currency { get; set; }
        public int NonBillableEnergy { get; set; }
        public string PaymentType { get; set; }
        public string PaymentMethodId { get; set; }
        public int? TerminalId { get; set; }
        public string PaymentStatus { get; set; }
        public int AuthorizationId { get; set; }
        public string IdTag { get; set; }
        public string IdTagLabel { get; set; }
        public int? ExtendingSessionId { get; set; }
        public bool ReimbursementEligibility { get; set; }
        public int TariffSnapshotId { get; set; }
        public decimal? ElectricityCost { get; set; }
        public string ExternalSessionId { get; set; }
        public string EvsePhysicalReference { get; set; }
        public DateTime PaymentStatusUpdatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public int ReceiptId { get; set; }
        public EnergyConsumption EnergyConsumption { get; set; }
        public string BillingStatus { get; set; }
    }

    public class TotalAmount
    {
        public decimal WithTax { get; set; }
        public decimal WithoutTax { get; set; }
    }

    public class TaxDetails
    {
        public int TaxId { get; set; }
        public decimal TaxPercentage { get; set; }
    }

    public class EnergyConsumption
    {
        public int Total { get; set; }
        public int Grid { get; set; }
    }

}
