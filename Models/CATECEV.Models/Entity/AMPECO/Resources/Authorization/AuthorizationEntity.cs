using CATECEV.Models.Entity.AMPECO.Resources.ChargePoint;
using CATECEV.Models.Entity.Base;

namespace CATECEV.Models.Entity.AMPECO.Resources.Authorization
{
    public class AuthorizationEntity : BaseEntity
    {
        public int AMPECOId { get; set; }
        public int AMPECOUserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Method { get; set; }
        public string Status { get; set; }
        public string? RejectionReason { get; set; }
        public string Source { get; set; }
        public string IdTagUid { get; set; }
        public bool? Roaming { get; set; }
        public int? AMPECOChargePointId { get; set; }
        public int? AMPECOEvseId { get; set; }
        public DateTime LastUpdatedAt { get; set; }
    }
}
