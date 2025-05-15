using CATECEV.Models.Entity.AMPECO.Resources.AmbPartner;
using CATECEV.Models.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CATECEV.FE.Models.ViewModels
{
    public class PartnerPaymentPageViewModel
    {
        public PartnerPaymentViewModel NewPayment { get; set; }
        public List<SelectListItem> Partners { get; set; }
        public List<PartnerPaymentViewModel> Payments { get; set; }

        public Partner SelectedPartner { get; set; } // Add this
    }

}
