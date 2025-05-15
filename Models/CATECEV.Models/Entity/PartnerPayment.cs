using CATECEV.Models.Entity.AMPECO.Resources.AmbPartner;
using CATECEV.Models.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CATECEV.Models.Entity
{
    public class PartnerPayment : BaseEntity
    {
        public int PartnerId { get; set; }

        public decimal PaymentAmount { get; set; }

        public DateTime PaymentDate{ get; set; }

        public Partner Partner { get; set; }
    }
}
