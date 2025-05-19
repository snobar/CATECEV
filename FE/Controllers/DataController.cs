using CATECEV.Data.Context;
using CATECEV.FE.Extensions;
using CATECEV.FE.Models.ViewModels;
using CATECEV.Models.Entity;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using MimeKit;
using Humanizer;

namespace CATECEV.FE.Controllers
{
    public class DataController : Controller
    {
        private readonly ILogger<DataController> _logger;
        private readonly AppDBContext _appContext;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IDataProtector _protector;
        private readonly SmtpSettings _smtpSettings;

        public DataController(IOptions<SmtpSettings> smtpOptions, IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<DataController> logger, AppDBContext appContext, IDataProtectionProvider provider)
        {
            _logger = logger;
            _appContext = appContext;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _protector = provider.CreateProtector("PartnerIdProtector");
            _smtpSettings = smtpOptions.Value;
        }

        [HttpGet]
        public IActionResult PrintSmtpConfig()
        {
            var configSummary = new
            {
                Host = _smtpSettings.Host,
                Port = _smtpSettings.Port,
                From = _smtpSettings.From,
                UserName = _smtpSettings.UserName,
                UseSSL = _smtpSettings.UseSSL,
                Alias = _smtpSettings.Alias
            };

            return Ok(configSummary);
        }

        public IActionResult Index()
        {
            var partners = _appContext.Partner.ToList();

            var viewModel = partners.Select(p => new PartnerListItem
            {
                Name = p.Name,
                RegNo = p.RegNo,
                BalanceAmount = p.BalanceAmount,
                LastCalculationBalanceDate = p.LastCalculationBalanceDate,
                EncryptedId = ShortEncryptor.Encrypt(p.Id)
            }).ToList();

            return View(viewModel);
        }

        public async Task<IActionResult> Manage(string id)
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

            await GetPartnerExpense(partnerId);

            var payments = _appContext.PartnerPayment
                .Include(p => p.Partner)
                .Where(x => x.PartnerId == partnerId)
                .Select(p => new PartnerPaymentViewModel
                {
                    Id = p.Id,
                    PartnerId = ShortEncryptor.Encrypt(p.PartnerId),
                    PartnerName = p.Partner.Name,
                    PaymentAmount = p.PaymentAmount,
                    PaymentDate = p.PaymentDate
                }).ToList();

            // Fetch the selected partner
            var selectedPartner = _appContext.Partner.FirstOrDefault(p => p.Id == partnerId);

            var model = new PartnerPaymentPageViewModel
            {
                NewPayment = new PartnerPaymentViewModel
                {
                    PartnerId = ShortEncryptor.Encrypt(partnerId),
                    PaymentDate = DateTime.Today
                },
                Payments = payments,
                Partners = _appContext.Partner.Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Name
                }).ToList(),
                SelectedPartner = selectedPartner // ← here’s the important addition
            };

            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> Manage(PartnerPaymentPageViewModel model)
        {

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { success = false, errors });
            }
            int partnerId = ShortEncryptor.Decrypt(model.NewPayment.PartnerId);
            var payment = new PartnerPayment
            {
                PartnerId = partnerId,
                PaymentAmount = model.NewPayment.PaymentAmount,
                PaymentDate = model.NewPayment.PaymentDate
            };

            _appContext.PartnerPayment.Add(payment);

            var partnerdata = _appContext.Partner.FirstOrDefault(x => x.Id == partnerId);
            partnerdata.BalanceAmount += model.NewPayment.PaymentAmount;
            _appContext.Partner.Update(partnerdata);

            _appContext.SaveChanges();
            await GetPartnerExpense(partnerId);

            var partnerName = _appContext.Partner.Find(partnerId)?.Name;

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

        [HttpGet]
        public async Task<IActionResult> RefreshBalance(string id)
        {
            int partnerId = ShortEncryptor.Decrypt(id);

            await GetPartnerExpense(partnerId);

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
            await GetPartnerExpense(partnerId);

            var selectedPartner = _appContext.Partner.FirstOrDefault(p => p.Id == partnerId);

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
                SelectedPartner = selectedPartner // ← here’s the important addition
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult SendBalanceEmail(string id)
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
            var partner = _appContext.Partner.FirstOrDefault(p => p.Id == partnerId);
            if (partner == null || string.IsNullOrWhiteSpace(partner.Email))
                return Json(new { success = false, message = "Partner or email not found" });

            try
            {
                var subject = "Balance Information from CATEC";
                var body = $@"
<html>
<head>
  <style>
    body {{
      font-family: Arial, sans-serif;
      font-size: 14px;
      color: #333;
    }}
    .container {{
      padding: 20px;
      border: 1px solid #ddd;
      border-radius: 6px;
      background-color: #f9f9f9;
    }}
    .header {{
      font-size: 18px;
      font-weight: bold;
      color: #005b96;
      margin-bottom: 10px;
    }}
    .highlight {{
      background-color: yellow;
      padding: 3px 6px;
      border-radius: 4px;
      font-weight: bold;
    }}
    .footer {{
      margin-top: 20px;
      font-size: 12px;
      color: #888;
    }}
  </style>
</head>
<body>
  <div class='container'>
    <div class='header'>Balance Notification</div>
    <p>Dear <strong>{partner.Name}</strong>,</p>

    <p>Your current balance information is as follows:</p>

    <p>Balance Amount: <span class='highlight'>{partner.BalanceAmount}</span></p>
    <p>Last Calculation Date: {partner.LastCalculationBalanceDate:yyyy-MM-dd HH:mm}</p>

    <p>Thank you for using <strong>CATEC</strong>.</p>

    <!-- ❗ Disclaimer Section -->
    <p style='color: #888; font-size: 12px; margin-top: 20px;'>
        Disclaimer: This email is not considered an official communication. The information provided — including any figures or balances — is for informational purposes only and may not reflect the final or accurate data. Please do not rely on it for any formal or official use.
    </p>

    <div class='footer'>
      CATEC System • <a href='https://www.catec.ae'>www.catec.ae</a>
    </div>
  </div>
</body>
</html>";




                SendEmail(partner.Email, subject, body);

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                // Optionally log the exception
                return Json(new { success = false, message = ex.Message });
            }
        }
        private async Task GetPartnerExpense(int partnerId)
        {

            string baseUrl = _configuration["ServiceEndpoints:AmpecoApi"];
            string url = $"{baseUrl}/AmbecoPartnerExpenseData?partnerId={partnerId}";

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
        }
        private void SendEmail(string toEmail, string subject, string body)
        {
            SmtpClient client = new SmtpClient();
            client.Connect(_smtpSettings.Host, Convert.ToInt32(_smtpSettings.Port), Convert.ToBoolean(_smtpSettings.UseSSL));

            if (!string.IsNullOrEmpty(_smtpSettings.UserName))
                client.Authenticate(_smtpSettings.UserName, _smtpSettings.Password);

            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = body;

            MimeMessage message = new MimeMessage();
            message.Subject = subject;
            message.Body = bodyBuilder.ToMessageBody();


            message.To.Add(MailboxAddress.Parse(toEmail));
            message.Cc.Add(MailboxAddress.Parse("RAbuHayah@catec.ae"));
            message.Cc.Add(MailboxAddress.Parse("SIrshidat@catec.ae"));

            MailboxAddress from = new MailboxAddress(_smtpSettings.Alias, _smtpSettings.From);
            message.From.Add(from);

            client.Send(message);
            client.Disconnect(true);
            client.Dispose();


            //using var smtpClient = new SmtpClient(_smtpSettings.Host)
            //{
            //    Port = int.Parse(_smtpSettings.Port),
            //    Credentials = new NetworkCredential(_smtpSettings.UserName, _smtpSettings.Password),
            //    EnableSsl = true // Office365 requires SSL/TLS even if "UseSSL" is false in config
            //};

            //var mailMessage = new MailMessage
            //{
            //    From = new MailAddress(_smtpSettings.From, _smtpSettings.Alias),
            //    Subject = subject,
            //    Body = body,
            //    IsBodyHtml = true
            //};
            //mailMessage.To.Add(toEmail);

            //smtpClient.Send(mailMessage);
        }

    }
}
