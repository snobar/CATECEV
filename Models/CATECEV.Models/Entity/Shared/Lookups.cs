using CATECEV.Models.Entity.Base;

namespace CATECEV.Models.Entity.Shared
{
    public class Lookups : BaseEntity
    {
        public string ArabicDescription { get; set; }
        public string EnglishDescription { get; set; }
        public int LookupCategoryId { get; set; }
        public int OrderId { get; set; }

        public LookupCategory LookupCategory { get; set; }
    }
}
