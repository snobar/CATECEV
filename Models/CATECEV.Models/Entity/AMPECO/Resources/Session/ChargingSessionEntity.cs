using CATECEV.Models.Entity.AMPECO.Resources.ChargePoint;
using CATECEV.Models.Entity.AMPECO.Resources.Tax;
using CATECEV.Models.Entity.Base;

namespace CATECEV.Models.Entity.AMPECO.Resources.Session
{
    public class ChargingSessionEntity : BaseEntity
    {
        public int AMPECOId { get; set; }

        #region User
        public int? UserId { get; set; }
        public int AMPECOUserId { get; set; }
        public User.User User { get; set; }
        #endregion

        #region ChargePoint
        public int? ChargePointId { get; set; }
        public int AMPECOChargePointId { get; set; }
        public ChargePointEntity ChargePoint { get; set; }
        #endregion

        #region Evse
        public int? EvseId { get; set; }
        public int AMPECOEvseId { get; set; }
        public EvseEntity Evse { get; set; }
        #endregion

        #region Connector
        public int? ConnectorId { get; set; }
        public int? AMPECOConnectorId { get; set; }
        public ConnectorEntity Connector { get; set; }
        #endregion

        #region TaxDetails
        public int? TaxId { get; set; }
        public int AMPECOTaxId { get; set; }
        public decimal TaxPercentage { get; set; }

        public TaxEntity Tax { get; set; }
        #endregion

        public string Status { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? StoppedAt { get; set; }
        public int Energy { get; set; }
        public double PowerKw { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public int NonBillableEnergy { get; set; }
        public string PaymentType { get; set; }
        public string PaymentMethodId { get; set; }
        public int? TerminalId { get; set; }
        public string PaymentStatus { get; set; }
        public int? AuthorizationId { get; set; }
        public string IdTag { get; set; }
        public string IdTagLabel { get; set; }
        public int? ExtendingSessionId { get; set; }
        public bool ReimbursementEligibility { get; set; }
        public int? TariffSnapshotId { get; set; }
        public decimal? ElectricityCost { get; set; }
        public string ExternalSessionId { get; set; }
        public string EvsePhysicalReference { get; set; }
        public DateTime? PaymentStatusUpdatedAt { get; set; }
        public DateTime? LastUpdatedAt { get; set; }
        public int? ReceiptId { get; set; }
        public string BillingStatus { get; set; }
        public string Reason { get; set; }

        #region TotalAmount
        public decimal TotalAmountWithTax { get; set; }
        public decimal TotalAmountWithoutTax { get; set; }
        #endregion

        #region EnergyConsumption
        public int TotalEnergyConsumption { get; set; }
        public int EnergyConsumptionGrid { get; set; }
        #endregion
    }
}
