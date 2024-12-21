using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using hijazi.Data;
using hijazi.Extensions;
using hijazi.Models;
using hijazi.Models.Entity;
using hijazi.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace hijazi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDBContext _appContext;
        public HomeController(ILogger<HomeController> logger, AppDBContext appContext)
        {
            _logger = logger;
            _appContext = appContext;
        }

        #region Select
        public async Task<IActionResult> Index()
        {
            var companiesData = (await _appContext.Company
                .Include(x => x.Vehicles)
                    .ThenInclude(x => x.VehicleUsers)
                .ToListAsync()).OrderByDescending(x => x.Id);

            return View(companiesData);
        }

        public async Task<IActionResult> Manage(int? CompanyId = null)
        {
            var vehicleTypes = await _appContext.VehicleType.ToListAsync();
            var countries = await _appContext.Country.ToListAsync();
            var manageModel = new CompanyWizardViewModel
            {
                Id = 0,
                VehicleTypes = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(vehicleTypes, "Id", "EnglishName"),
                Countries = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(countries, "Id", "EnglishName"),
            };


            if (CompanyId.IsNotNullOrEmpty())
            {
                manageModel.Id = CompanyId.Value;

                var companyData = await GetCompany(CompanyId.Value);
                var citiesData = await GetCityByCountyId(companyData.CountryId.Value);


                manageModel.CompanyName = companyData.CompanyName;
                manageModel.CompanyRegistrationNumber = companyData.CompanyRegistrationNumber;
                manageModel.CountryId = companyData.CountryId;
                manageModel.CountryName = companyData.Country.IsNotNullOrEmpty() && companyData.Country.EnglishName.IsNotNullOrEmpty() ? companyData.Country.EnglishName : "";
                manageModel.CityId = companyData.CityId;
                manageModel.CityName = companyData.City.IsNotNullOrEmpty() && companyData.City.EnglishName.IsNotNullOrEmpty() ? companyData.City.EnglishName : "";
                manageModel.Email = companyData.Email;
                manageModel.Mobile = companyData.Mobile;
                manageModel.Cities = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(citiesData, "Id", "EnglishName");
                manageModel.TopUpAmount = companyData.TopUpAmount;



                if (companyData.Vehicles.IsNotNullOrEmpty())
                {
                    foreach (var vehicle in companyData.Vehicles)
                    {
                        var type = vehicleTypes.FirstOrDefault(x=>x.Id == vehicle.TypeId);
                        manageModel.Vehicles.Add(
                            new VehicleViewModel
                            {
                                Id = vehicle.Id,
                                CompanyId = vehicle.CompanyId,
                                MACAddress = vehicle.MACAddress,
                                PlateNumber = vehicle.PlateNumber,
                                TypeId = vehicle.TypeId,
                                Type = type.IsNotNullOrEmpty() && type.EnglishName.IsNotNullOrEmpty() ? type.EnglishName : "",
                                VINNumber = vehicle.VINNumber,
                                IsActive = vehicle.IsActive,
                            });

                        if (vehicle.VehicleUsers.IsNotNullOrEmpty())
                        {
                            foreach (var user in vehicle.VehicleUsers)
                            {
                                manageModel.VehicleUsers.Add(
                                new VehicleUserViewModel
                                {
                                    Id = user.Id,
                                    ArabicName = user.ArabicName,
                                    EnglishName = user.EnglishName,
                                    Mobile = user.Mobile,
                                    VehicleId = user.VehicleId,
                                    IsActive = user.IsActive
                                });
                            }

                        }
                    }
                }

            }

            return View("Manage/Manage", manageModel);
        }

        public async Task<IActionResult> AddUserToVehicle(int VehicleId,int CompanyId)
        {
            if (VehicleId > 0 && CompanyId > 0)
            {
                var companyData = await GetCompany(CompanyId);

                if (companyData.IsNotNullOrEmpty() && companyData.Vehicles.IsNotNullOrEmpty())
                {
                    var vehicleData = companyData.Vehicles.FirstOrDefault(x=>x.Id == VehicleId);

                    if (vehicleData.IsNotNullOrEmpty() && vehicleData.Id == VehicleId)
                    {
                        var vehicleTypes = await _appContext.VehicleType.ToListAsync();
                        
                        var vehicleViewModel = new VehicleViewModel
                        {
                            Id = VehicleId,
                            CompanyId = CompanyId,
                            Company = companyData.CompanyName,
                            MACAddress = vehicleData.MACAddress,
                            PlateNumber = vehicleData.PlateNumber,
                            VINNumber = vehicleData.VINNumber,
                            TypeId = vehicleData.TypeId,
                            Users = vehicleData.VehicleUsers,
                            IsActive = vehicleData.IsActive,
                            VehicleTypes = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(vehicleTypes, "Id", "EnglishName")
                        };
                        return View("Manage/AddUser", vehicleViewModel);
                    }
                }
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region Save
        [HttpPost]
        public async Task<IActionResult> SubmitCompany(Company SaveModel)
        {
            if (SaveModel.IsNotNullOrEmpty())
            {
                if (SaveModel.Id > 0)
                {
                    var companyData = await GetCompany(SaveModel.Id);

                    if (companyData.IsNotNullOrEmpty() && companyData.Id == SaveModel.Id)
                    {
                        //companyData.CompanyName = SaveModel.CompanyName;
                        //companyData.CompanyRegistrationNumber = SaveModel.CompanyRegistrationNumber;
                        //companyData.CountryId = SaveModel.CountryId;
                        //companyData.CityId = SaveModel.CityId;
                        //companyData.Mobile = SaveModel.Mobile;
                        //companyData.Email = SaveModel.Email;
                        companyData.TopUpAmount = SaveModel.TopUpAmount;
                        _appContext.Company.Update(companyData);
                    }
                }
                else
                {
                    await _appContext.Company.AddAsync(SaveModel);
                }

                await _appContext.SaveChangesAsync();
                return RedirectToAction("Manage", new { CompanyId = SaveModel.Id });
            }
            return RedirectToAction("Manage");
        }

        [HttpPost]
        public async Task<IActionResult> SaveVehicle(Vehicle SaveModel)
        {
            if (SaveModel.IsNotNullOrEmpty())
            {
                if (SaveModel.Id > 0)
                {
                    var vehicleData = await GetVehicle(SaveModel.Id);

                    vehicleData.MACAddress = SaveModel.MACAddress;
                    vehicleData.TypeId = SaveModel.TypeId;
                    vehicleData.VINNumber = SaveModel.VINNumber;
                    vehicleData.PlateNumber = SaveModel.PlateNumber;

                    _appContext.Vehicle.Update(vehicleData);
                    await _appContext.SaveChangesAsync();

                    return RedirectToAction("AddUserToVehicle", new { VehicleId = SaveModel.Id, SaveModel.CompanyId });
                }
                else
                {
                    await _appContext.Vehicle.AddAsync(SaveModel);
                    await _appContext.SaveChangesAsync();
                }

                await _appContext.SaveChangesAsync();
                return RedirectToAction("Manage", new { CompanyId = SaveModel.CompanyId });
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> SaveUser(VehicleUser SaveModel)
        {
            if (SaveModel.IsNotNullOrEmpty())
            {
                await _appContext.VehicleUser.AddAsync(new VehicleUser
                {
                    Id = 0,
                    VehicleId = SaveModel.VehicleId,
                    ArabicName = SaveModel.ArabicName,
                    EnglishName = SaveModel.EnglishName,
                    Mobile = SaveModel.Mobile
                });
                await _appContext.SaveChangesAsync();

                return RedirectToAction("AddUserToVehicle", new { SaveModel.VehicleId, SaveModel.Vehicle.CompanyId });
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region Delete
        public async Task<IActionResult> AciveInactiveCompany(int CompanyId, bool isActive)
        {
            if (CompanyId > 0)
            {
                var companyData = await GetCompany(CompanyId);

                if (companyData.IsNotNullOrEmpty() && companyData.Id == CompanyId)
                {
                    companyData.IsActive = isActive;

                    _appContext.Company.Update(companyData);
                    await _appContext.SaveChangesAsync();
                }
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AciveInactiveVehicle(int VehicleId, int CompanyId, bool isActive)
        {
            if (VehicleId > 0 && CompanyId > 0)
            {
                var vehicleData = await GetVehicle(VehicleId);

                if (vehicleData.IsNotNullOrEmpty() && vehicleData.Id == VehicleId)
                {
                    vehicleData.IsActive = isActive;

                    _appContext.Vehicle.Update(vehicleData);
                    await _appContext.SaveChangesAsync();

                    return RedirectToAction("Manage", new { CompanyId });
                }
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AciveInactiveUser(int VehicleId, int CompanyId, int userId,bool isActive)
        {
            if (VehicleId > 0 && CompanyId > 0 && userId > 0)
            {
                var userData = await GetUser(userId);

                if (userData.IsNotNullOrEmpty() && userData.Id == userId)
                {
                    userData.IsActive = isActive;

                    _appContext.VehicleUser.Update(userData);
                    await _appContext.SaveChangesAsync();

                    return RedirectToAction("AddUserToVehicle", new { VehicleId, CompanyId });
                }
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region Private
        private async Task<Company> GetCompany(int Id)
        {
            return (await _appContext.Company
                    .Where(x => x.Id == Id)
                        .Include(x => x.Country)
                         .Include(x => x.City)
                        .Include(x => x.Vehicles)
                            .ThenInclude(x => x.VehicleUsers)
                .ToListAsync()).FirstOrDefault();
        }

        private async Task<List<City>> GetCityByCountyId(int CountyId)
        {
            return (await _appContext.City
                    .Where(x => x.CountryId == CountyId).ToListAsync());
        }

        private async Task<Vehicle> GetVehicle(int Id)
        {
            return (await _appContext.Vehicle
                    .Where(x => x.Id == Id)
                .ToListAsync()).FirstOrDefault();
        }

        private async Task<VehicleUser> GetUser(int Id)
        {
            return (await _appContext.VehicleUser
                    .Where(x => x.Id == Id)
                .ToListAsync()).FirstOrDefault();
        }
        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
