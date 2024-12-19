using hijazi.Models.Entity.Base;

namespace hijazi.Models.Entity
{
    public class Company : BaseEntity
    {
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
