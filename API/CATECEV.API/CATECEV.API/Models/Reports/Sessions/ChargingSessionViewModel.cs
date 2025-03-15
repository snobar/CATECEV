
namespace CATECEV.API.Models.Reports.Sessions
{
    public class ChargingSessionViewModel
    {
        public int ID { get; set; }
        public string User { get; set; }
        public int UserID { get; set; }
        public string Email { get; set; }
        public string UserGroups { get; set; }
        public DateTime? StartDate { get; set; }
        public TimeSpan? StartTime { get; set; }
        public DateTime? EndDate { get; set; }
        public TimeSpan? EndTime { get; set; }
        public long? DurationInSeconds { get; set; }
        public int EnergySupplied { get; set; }
        public string SessionStatus { get; set; }
        public string PaymentStatus { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalAmountWithoutTax { get; set; }
        public string Currency { get; set; }
        public int EVSEIdentifier { get; set; }
        public string EVCSNumber { get; set; }
        public string StopReason { get; set; }
        public string EVSEType { get; set; }
        public string ChargePointName { get; set; }
        public int LocationId { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string AuthorizationMethod { get; set; }
        public string IDTag { get; set; }
        public string IDTagLabel { get; set; }
        public bool? Roaming { get; set; }
        public string RoamingCPO { get; set; }
        public string Location { get; set; }
        public string Country { get; set; }
    }
}
