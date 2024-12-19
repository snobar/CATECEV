using hijazi.Models.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace hijazi.Models.ViewModels
{
    public class CompanyWizardViewModel
    {
        public int Id { get; set; }
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }

        public SelectList VehicleTypes { get; set; }
        public List<VehicleViewModel> Vehicles { get; set; } = new List<VehicleViewModel>();

        public List<VehicleUserViewModel> VehicleUsers { get; set; } = new List<VehicleUserViewModel>();
    }

    public class VehicleViewModel
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Company { get; set; }
        public string PlateNumber { get; set; }
        public int TypeId { get; set; }
        public string Type { get; set; }
        public string VINNumber { get; set; }
        public string MACAddress { get; set; }
        public bool IsActive { get; set; }

        public SelectList VehicleTypes { get; set; }
        public IEnumerable<VehicleUser> Users { get; set; }
    }

    public class VehicleUserViewModel
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }
        public string Mobile { get; set; }
        public bool IsActive { get; set; }
    }
}
