using CATECEV.Models.Entity.Base;

namespace CATECEV.Models.Entity.Shared
{
    public class LookupCategory : BaseEntity
    {
        public string Description { get; set; }

        public ICollection<Lookups> Lookups { get; set; }
    }
}
