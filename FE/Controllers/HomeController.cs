using CATECEV.CORE.Extensions;
using CATECEV.Data.Context;
using CATECEV.FE.Models;
using CATECEV.Models.Entity;
using CATECEV.FE.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.DotNet.Scaffolding.Shared;
using CATECEV.FE.Extensions;
using CATECEV.Models.Entity.AMPECO.Resources.AmbPartner;
using Newtonsoft.Json;

namespace CATECEV.FE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDBContext _appContext;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IDataProtector _protector;

        public HomeController(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<HomeController> logger, AppDBContext appContext, IDataProtectionProvider provider)
        {
            _logger = logger;
            _appContext = appContext;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _protector = provider.CreateProtector("PartnerIdProtector");

        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> RefreshBalance(string id)
        {
            int partnerId = ShortEncryptor.Decrypt(id);

            await GetPartnerExpense(partnerId, false);

            var partner = _appContext.Partner.FirstOrDefault(p => p.Id == partnerId);
            if (partner == null)
            {
                return Json(new { success = false });
            }

            // Optional: update balance logic here
            var newBalance = partner.BalanceAmount.ToString("F2");

            return Json(new
            {
                success = true,
                newBalance = newBalance
            });
        }

        public async Task<IActionResult> ViewInfo(string id)
        {
            int partnerId;
            try
            {
                partnerId = ShortEncryptor.Decrypt(id);
            }
            catch
            {
                return BadRequest("Invalid or tampered ID");
            }
            var expenses = await GetPartnerExpense(partnerId, true);

            var selectedPartner = _appContext.Partner.FirstOrDefault(p => p.Id == partnerId);

            var payments = _appContext.PartnerPayment
                       .Include(p => p.Partner)
                       .Where(x => x.PartnerId == partnerId)
                       .OrderByDescending(x => x.CreatedOn)
                       .Select(p => new PartnerPaymentViewModel
                       {
                           Id = p.Id,
                           PartnerId = ShortEncryptor.Encrypt(p.PartnerId),
                           PartnerName = p.Partner.Name,
                           PaymentAmount = p.PaymentAmount,
                           PaymentDate = p.PaymentDate
                       }).ToList();

            var model = new PartnerPaymentPageViewModel
            {
                NewPayment = new PartnerPaymentViewModel
                {
                    PartnerId = ShortEncryptor.Encrypt(partnerId),
                    PaymentDate = DateTime.Today
                },
                Partners = _appContext.Partner.Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Name
                }).ToList(),
                SelectedPartner = new SelectedPartnerViewModel
                {
                    Id = selectedPartner.Id,
                    Name = selectedPartner.Name,
                    RegNo = selectedPartner.RegNo,
                    BalanceAmount = selectedPartner.BalanceAmount,
                    LastCalculationBalanceDate = selectedPartner.LastCalculationBalanceDate,
                    Email = selectedPartner.Email,
                    Mobile = selectedPartner.Phone
                },
                PartnerExpenses = expenses,
                PartnerPayments = payments
            };

            return View(model);
        }


        private async Task<List<AMPECOPartnerExpenseViewModel>> GetPartnerExpense(int partnerId, bool showAllExpenses)
        {
            string baseUrl = _configuration["ServiceEndpoints:AmpecoApi"];
            string url = $"{baseUrl}/AmbecoPartnerExpenseData?partnerId={partnerId}&showAllExpenses={showAllExpenses}";

            var client = _httpClientFactory.CreateClient();

            try
            {
                var response = await client.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    ViewBag.ApiError = $"API call failed: {response.StatusCode}";
                    return new List<AMPECOPartnerExpenseViewModel>();
                }

                var json = await response.Content.ReadAsStringAsync();
                var expenses = JsonConvert.DeserializeObject<List<AMPECOPartnerExpenseViewModel>>(json);
                return expenses ?? new List<AMPECOPartnerExpenseViewModel>();
            }
            catch (Exception ex)
            {
                ViewBag.ApiError = $"Exception: {ex.Message}";
                return new List<AMPECOPartnerExpenseViewModel>();
            }
        }

    }
}