using hijazi.Models.Entity.Base;

namespace hijazi.Models.Entity
{
    public class VehicleType : BaseEntity
    {
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
