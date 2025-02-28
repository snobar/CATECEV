using CATECEV.FE.Models.Entity.Base;

namespace CATECEV.FE.Models.Entity
{
    public class Vehicle : BaseEntity
    {
        public int CompanyId { get; set; }
        public string PlateNumber { get; set; }
        public int TypeId { get; set; }
        public string VINNumber { get; set; }
        public string MACAddress { get; set; }

        public Company Company { get; set; }
        public VehicleType VehicleType { get; set; }
        public ICollection<VehicleUser> VehicleUsers { get; set; }
    }
}
