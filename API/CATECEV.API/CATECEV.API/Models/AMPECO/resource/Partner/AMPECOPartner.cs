namespace CATECEV.API.Models.AMPECO.resource.Partner
{
    public class AMPECOPartner
    {
        public int Id { get; set; }
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
        public PartnerOptions Options { get; set; }
        public CorporateBilling CorporateBilling { get; set; }
        public string ExternalId { get; set; }
    }

    public class PartnerOptions
    {
        public bool CreateUsers { get; set; }
        public bool AddUserBalance { get; set; }
        public bool SupplierOnReceipts { get; set; }
        public bool AllowToControlTariffs { get; set; }
        public bool AllowToControlTariffGroups { get; set; }
    }

    public class CorporateBilling
    {
        public bool Enabled { get; set; }
        public decimal? MonthlyLimit { get; set; }
        public decimal Discount { get; set; }
    }

}
