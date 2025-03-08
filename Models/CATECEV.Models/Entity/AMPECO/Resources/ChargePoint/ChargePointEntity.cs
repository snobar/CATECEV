using CATECEV.Models.Entity.AMPECO.Resources.Session;
using CATECEV.Models.Entity.Base;

namespace CATECEV.Models.Entity.AMPECO.Resources.ChargePoint
{
    public class ChargePointEntity : BaseEntity
    {
        public int AMPECOId { get; set; }
        public string Name { get; set; }
        public string? NetworkId { get; set; }
        public string NetworkProtocol { get; set; }
        public string NetworkPassword { get; set; }
        public string NetworkIp { get; set; }
        public int? NetworkPort { get; set; }
        public string Status { get; set; }
        public int LocationId { get; set; }
        public string Pin { get; set; }
        public string NetworkStatus { get; set; }
        public string HardwareStatus { get; set; }
        public bool PlugAndCharge { get; set; }
        //public DateTime? LastBootNotification { get; set; }
        public string AccessType { get; set; }
        public string ChargingProfile { get; set; }
        public int CurrentSecurityProfile { get; set; }
        public int? HardwareEnabledSecurityProfile { get; set; }
        public int? DesiredSecurityProfile { get; set; }
        public string DesiredSecurityProfileStatus { get; set; }
        public int? RoamingOperatorId { get; set; }
        public int? OwnerPartnerId { get; set; }
        public int? OwnerPartnerContractId { get; set; }
        public bool PartnerCorporateBillingAsDefault { get; set; }
        public List<string> Capabilities { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? FirstConnection { get; set; }
        public int? OwnerUserId { get; set; }
        public User.User OwnerUser { get; set; }
        public bool ManagedByOperator { get; set; }
        public bool AutoFaultRecovery { get; set; }
        public List<string> Tags { get; set; }
        public bool DisplayTariffsAndCosts { get; set; }
        public string ExternalId { get; set; }

        public bool SubscriptionRequired { get; set; }
        public List<string> SubscriptionPlanIds { get; set; }

        public ICollection<EvseEntity> Evses { get; set; }
        public ICollection<ChargingSessionEntity> ChargingSessions { get; set; }
    }

}
