using CATECEV.Models.Entity.AMPECO.Resources.AmbPartner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CATECEV.Models.Entity
{
    public class PartnerMonthlyCalculationTransaction
    {
        public int Id { get; set; }
        public int PartnerId { get; set; }
        /// YYYYMM (e.g., 202508)
        public int MonthValue { get; set; }
        /// Monthly total WITHOUT tax
        public decimal TotalAmount { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public Partner Partner { get; set; }
    }
}
