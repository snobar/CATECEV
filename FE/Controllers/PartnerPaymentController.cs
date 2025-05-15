using CATECEV.Data.Context;
using CATECEV.FE.Models.ViewModels;
using CATECEV.Models.Entity;
using CATECEV.Models.Entity.AMPECO.Resources.AmbPartner;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;

namespace CATECEV.FE.Controllers
{
    public class PartnerPaymentController : Controller
    {
        private readonly ILogger<PartnerPaymentController> _logger;
        private readonly AppDBContext _appContext;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public PartnerPaymentController(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<PartnerPaymentController> logger, AppDBContext appContext)
        {
            _logger = logger;
            _appContext = appContext;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }



        public async Task<IActionResult> Index()
        {

            var payments = _appContext.PartnerPayment
                .Include(p => p.Partner)
                .Select(p => new PartnerPaymentViewModel
                {
                    Id = p.Id,
                    PartnerId = p.PartnerId,
                    PartnerName = p.Partner.Name,
                    PaymentAmount = p.PaymentAmount,
                    PaymentDate = p.PaymentDate
                }).ToList();

            var model = new PartnerPaymentPageViewModel
            {
                NewPayment = new PartnerPaymentViewModel
                {
                    PaymentDate = DateTime.Today
                },
                Payments = payments,
                Partners = _appContext.Partner.Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Name
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(PartnerPaymentPageViewModel model)
        {

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { success = false, errors });
            }

            var payment = new PartnerPayment
            {
                PartnerId = model.NewPayment.PartnerId,
                PaymentAmount = model.NewPayment.PaymentAmount,
                PaymentDate = model.NewPayment.PaymentDate
            };

            _appContext.PartnerPayment.Add(payment);

            var partnerdata = _appContext.Partner.FirstOrDefault(x => x.Id == model.NewPayment.PartnerId);
            partnerdata.BalanceAmount += model.NewPayment.PaymentAmount;
            _appContext.Partner.Update(partnerdata);

            _appContext.SaveChanges();

            string baseUrl = _configuration["ServiceEndpoints:AmpecoApi"];
            string url = $"{baseUrl}/AmbecoPartnerExpenseData?partnerId={model.NewPayment.PartnerId}";

            var client = _httpClientFactory.CreateClient();

            try
            {
                var response = await client.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    // Optional: log or throw error
                    ViewBag.ApiError = $"API call failed: {response.StatusCode}";
                }
                else
                {
                    // Optionally read result here if API returns data
                    // var result = await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                ViewBag.ApiError = $"Exception: {ex.Message}";
            }

            var partnerName = _appContext.Partner.Find(model.NewPayment.PartnerId)?.Name;

            return Json(new
            {
                success = true,
                row = new
                {
                    partnerName,
                    model.NewPayment.PaymentAmount,
                    date = model.NewPayment.PaymentDate.ToShortDateString()
                }
            });
        }

    }

}
