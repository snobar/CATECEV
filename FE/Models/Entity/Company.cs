using CATECEV.FE.Models.Entity.Base;

namespace CATECEV.FE.Models.Entity
{
    public class Company : BaseEntity
    {
        public string CompanyName { get; set; }
        public string CompanyRegistrationNumber { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public int? CountryId { get; set; }
        public int? CityId { get; set; }
        public int TopUpAmount { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }
        public Country Country { get; set; }
        public City City { get; set; }
    }
}
