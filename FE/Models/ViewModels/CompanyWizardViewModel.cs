using CATECEV.Models.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CATECEV.FE.Models.ViewModels
{
    public class CompanyWizardViewModel
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string CompanyRegistrationNumber { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public int? CountryId { get; set; }
        public string CountryName { get; set; }
        public int? CityId { get; set; }
        public string CityName { get; set; }
        public int TopUpAmount { get; set; }


        public SelectList VehicleTypes { get; set; }
        public SelectList Cities { get; set; }
        public SelectList Countries { get; set; }
        public List<VehicleViewModel> Vehicles { get; set; } = new List<VehicleViewModel>();

        public List<VehicleUserViewModel> VehicleUsers { get; set; } = new List<VehicleUserViewModel>();
    }

    public class VehicleViewModel
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Company { get; set; }

        [Required]
        public string PlateNumber { get; set; }
        public int TypeId { get; set; }
        public string Type { get; set; }

        [Required]
        public string VINNumber { get; set; }

        [Required]
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
