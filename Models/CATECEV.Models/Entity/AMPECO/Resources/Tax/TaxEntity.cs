using CATECEV.Models.Entity.AMPECO.Resources.Session;
using CATECEV.Models.Entity.Base;

namespace CATECEV.Models.Entity.AMPECO.Resources.Tax
{
    public class TaxEntity : BaseEntity
    {
        public int AMPECOId { get; set; }
        public string Name { get; set; }
        public int Percentage { get; set; }
        public int TaxIdentificationNumberId { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public ICollection<TaxDisplayNameEntity> DisplayName { get; set; }

        public ICollection<ChargingSessionEntity> ChargingSession { get; set; }
    }
}
