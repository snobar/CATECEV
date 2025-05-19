using System.ComponentModel.DataAnnotations;

namespace CATECEV.FE.Models.ViewModels
{
    public class PartnerPaymentViewModel
    {
        public int Id { get; set; }
        public string PartnerId { get; set; }

        [Required]
        public decimal PaymentAmount { get; set; }


        [Required]
        [DataType(DataType.Date)]
        public DateTime PaymentDate { get; set; }

        public string PartnerName { get; set; }
    }
}
