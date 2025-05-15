using CATECEV.Models.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CATECEV.Models.Entity.AMPECO.Resources.AmbPartner
{
    public class PartnerOptions : BaseEntity
    {
        public bool CreateUsers { get; set; }
        public bool AddUserBalance { get; set; }
        public bool SupplierOnReceipts { get; set; }
        public bool AllowToControlTariffs { get; set; }
        public bool AllowToControlTariffGroups { get; set; }
    }
}
