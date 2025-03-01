using CATECEV.Models.Entity.Base;

namespace CATECEV.Models.Entity.Shared
{
    public class Lookups : BaseEntity
    {
        public string Description { get; set; }
        public int LookupCategoryId { get; set; }
        public int OrderId { get; set; }

        public LookupCategory LookupCategory { get; set; }
    }
}
