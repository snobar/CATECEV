using CATECEV.FE.Models.Entity.Base;

namespace CATECEV.FE.Models.Entity
{
    public class VehicleUser : BaseEntity
    {
        public int VehicleId { get; set; }
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }
        public string Mobile { get; set; }

        public Vehicle Vehicle { get; set; }
    }
}
