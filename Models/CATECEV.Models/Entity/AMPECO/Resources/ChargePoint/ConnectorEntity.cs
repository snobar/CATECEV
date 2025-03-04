using CATECEV.Models.Entity.AMPECO.Resources.Session;
using CATECEV.Models.Entity.Base;

namespace CATECEV.Models.Entity.AMPECO.Resources.ChargePoint
{
    public class ConnectorEntity : BaseEntity
    {
        public int AMPECOId { get; set; }
        public int EvseId { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public string Format { get; set; }

        public EvseEntity Evse { get; set; }
        public ICollection<ChargingSessionEntity> ChargingSessions { get; set; }
    }
}
