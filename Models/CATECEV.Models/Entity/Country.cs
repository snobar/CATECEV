using CATECEV.Models.Entity.Base;

namespace CATECEV.Models.Entity
{
    public class Country : BaseEntity
    {
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }

        public ICollection<Company> Companies { get; set; }
        public ICollection<City> Cities { get; set; }

    }
}
