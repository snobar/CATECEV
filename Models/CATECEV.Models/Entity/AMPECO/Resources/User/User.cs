using CATECEV.Models.Entity.AMPECO.Resources.Authorization;
using CATECEV.Models.Entity.AMPECO.Resources.ChargePoint;
using CATECEV.Models.Entity.AMPECO.Resources.Session;
using CATECEV.Models.Entity.Base;

namespace CATECEV.Models.Entity.AMPECO.Resources.User
{
    public class User : BaseEntity
    {
        public int AMPECOId { get; set; }
        public string Email { get; set; }
        public bool RequirePasswordReset { get; set; }
        public DateTime? EmailVerified { get; set; }
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
        public string UserGroups { get; set; }
        public string SubscriptionId { get; set; }
        public string ExternalId { get; set; }
        public DateTime? LastUpdatedAt { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public List<int> TermsAndPoliciesIdsWithConsent { get; set; }

        public int OptionsId { get; set; }
        public UserOptions Options { get; set; }

        public ICollection<ChargingSessionEntity> ChargingSessions { get; set; }
        public ICollection<ChargePointEntity> ChargePoint { get; set; }
    }
}
