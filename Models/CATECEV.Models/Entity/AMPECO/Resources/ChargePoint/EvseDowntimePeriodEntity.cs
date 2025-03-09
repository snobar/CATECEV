using CATECEV.Models.Entity.Base;

namespace CATECEV.Models.Entity.AMPECO.Resources.ChargePoint
{
    public class EvseDowntimePeriodEntity : BaseEntity
    {
        public int AMPECOId { get; set; }

        #region Evse
        public int? EvseId { get; set; }
        public int AMPECOEvseId { get; set; }
        public EvseEntity Evse { get; set; }
        #endregion

        public int ChargePointId { get; set; }
        public int LocationId { get; set; }
        public string EntryMode { get; set; }
        public string Type { get; set; }

        #region Statuses
        public string SystemStatus { get; set; }
        public string NetworkStatus { get; set; }
        public string HardwareStatus { get; set; }
        #endregion

        public DateTime StartedAt { get; set; }
        public DateTime? EndedAt { get; set; }
    }
}
