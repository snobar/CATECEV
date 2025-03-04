using CATECEV.Models.Entity.Base;

namespace CATECEV.Models.Entity.AMPECO.Resources.Tax
{
    public class TaxDisplayNameEntity : BaseEntity
    {
        public string Locale { get; set; }
        public string Translation { get; set; }

        public int TaxId { get; set; }
        public TaxEntity Tax { get; set; }
    }
}
