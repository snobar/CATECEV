using CATECEV.Models.Entity.AMPECO.Resources.Session;
using CATECEV.Models.Entity.Base;

namespace CATECEV.Models.Entity.AMPECO.Resources.ChargePoint
{
    public class EvseEntity : BaseEntity
    {
        public int AMPECOId { get; set; }
        public string ExternalId { get; set; }
        public string NetworkId { get; set; }
        public string PhysicalReference { get; set; }
        public string CurrentType { get; set; }
        public int MaxPower { get; set; }
        public string MaxVoltage { get; set; }
        public int MaxAmperage { get; set; }
        public string Status { get; set; }
        public string HardwareStatus { get; set; }
        public int? MidMeterCertificationEndYear { get; set; }
        public int TariffGroupId { get; set; }
        public string ChargingProfile { get; set; }
        public bool AllowsReservation { get; set; }

        #region Roaming
        public string RoamingPhysicalReference { get; set; }
        public List<string> TariffIds { get; set; }
        public List<string> Capabilities { get; set; }
        public string RoamingStatus { get; set; }
        #endregion

        #region ChargePoint
        public int? ChargePointId { get; set; }
        public int AMPECOChargePointId { get; set; }
        public ChargePointEntity ChargePoint { get; set; }
        #endregion

        public ICollection<ConnectorEntity> Connectors { get; set; }
        public ICollection<ChargingSessionEntity> ChargingSessions { get; set; }
    }
}
