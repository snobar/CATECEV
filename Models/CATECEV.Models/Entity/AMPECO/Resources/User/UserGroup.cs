using CATECEV.Models.Entity.Base;

namespace CATECEV.Models.Entity.AMPECO.Resources.User
{
    public class UserGroup : BaseEntity
    {
        public int AMPECOId { get; set; }
        public string Name { get; set; }
        public int? PartnerId { get; set; }
        public string Description { get; set; }
        public DateTime LastUpdatedAt { get; set; }
    }
}
