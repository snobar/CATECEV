namespace CATECEV.API.Models.AMPECO.resource.Partner
{
    public class AMPECOPartner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BusinessName { get; set; }
        public string RegNo { get; set; }
        public string VatNo { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public Options Options { get; set; }
        public CorporateBilling CorporateBilling { get; set; }
        public ContactDetails ContactDetails { get; set; }
        public Notifications Notifications { get; set; }
        public string ExternalId { get; set; }
        public decimal MonthlyPlatformFee { get; set; }
    }

    public class Options
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

    public class ContactDetails
    {
        public Contact Administrative { get; set; }
        public Contact Technical { get; set; }
        public Contact Billing { get; set; }
    }

    public class Contact
    {
        public string ContactPerson { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }

    public class Notifications
    {
        public TechnicalNotifications Technical { get; set; }
        public BillingNotifications Billing { get; set; }
    }

    public class TechnicalNotifications
    {
        public bool ChargePointFaults { get; set; }
    }

    public class BillingNotifications
    {
        public bool SettlementReports { get; set; }
    }
}
