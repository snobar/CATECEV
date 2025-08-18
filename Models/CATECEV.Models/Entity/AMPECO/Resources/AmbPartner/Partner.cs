using CATECEV.Models.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CATECEV.Models.Entity.AMPECO.Resources.AmbPartner
{
    public class Partner : BaseEntity
    {
        public int AMPECOId { get; set; }
        public string Name { get; set; }
        public string RegNo { get; set; }
        public string VatNo { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }
        public string Address { get; set; }
        public string ContactPerson { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string FaultNotificationsEmail { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public decimal MonthlyPlatformFee { get; set; }
        public decimal BalanceAmount { get; set; }
        public decimal? InitialBalanceAmount { get; set; }

        public DateTime LastCalculationBalanceDate { get; set; }
        public PartnerOptions Options { get; set; }
        public CorporateBilling CorporateBilling { get; set; }
        public string ExternalId { get; set; }

        public ICollection<PartnerPayment> PartnerPayments { get; set; }
    }
}
