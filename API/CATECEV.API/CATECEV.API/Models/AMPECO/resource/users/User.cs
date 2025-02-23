using Microsoft.AspNetCore.Identity;

namespace CATECEV.API.Models.AMPECO.resource.users
{
    public class User
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public bool RequirePasswordReset { get; set; }
        public DateTime EmailVerified { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string Address { get; set; }
        public string VehicleNo { get; set; }
        public string PersonalId { get; set; }
        public bool ReceiveNewsAndPromotions { get; set; }
        public string Locale { get; set; }
        public decimal Balance { get; set; }
        public string Status { get; set; }
        public List<int> UserGroupIds { get; set; }
        public string SubscriptionId { get; set; }
        public string ExternalId { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public UserOptions Options { get; set; }
        public List<int> TermsAndPoliciesIdsWithConsent { get; set; }
    }
}
