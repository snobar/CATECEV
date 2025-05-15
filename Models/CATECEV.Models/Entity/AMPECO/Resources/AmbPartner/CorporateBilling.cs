using CATECEV.Models.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CATECEV.Models.Entity.AMPECO.Resources.AmbPartner
{
    public class CorporateBilling : BaseEntity
    {
        public bool Enabled { get; set; }
        public decimal? MonthlyLimit { get; set; }
        public decimal Discount { get; set; }
    }
}
