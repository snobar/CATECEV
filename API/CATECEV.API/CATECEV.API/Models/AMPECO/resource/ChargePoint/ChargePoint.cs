namespace CATECEV.API.Models.AMPECO.resource.ChargePoint
{
    public class ChargePoint
    {
        public int Id { get; set; }
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
        public List<Evse> Evses { get; set; }
        public LastBootNotification LastBootNotification { get; set; }
        public string AccessType { get; set; }
        public object ChargingProfile { get; set; }
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
        public bool ManagedByOperator { get; set; }
        public bool AutoFaultRecovery { get; set; }
        public List<string> Tags { get; set; }
        public bool DisplayTariffsAndCosts { get; set; }
        public string ExternalId { get; set; }
        public Subscription Subscription { get; set; }
    }

    public class Evse
    {
        public int Id { get; set; }
        public string ExternalId { get; set; }
        public string? NetworkId { get; set; }
        public string PhysicalReference { get; set; }
        public string CurrentType { get; set; }
        public int? MaxPower { get; set; }
        public string MaxVoltage { get; set; }
        public int? MaxAmperage { get; set; }
        public string Status { get; set; }
        public string HardwareStatus { get; set; }
        public List<Connector> Connectors { get; set; }
        public int? MidMeterCertificationEndYear { get; set; }
        public int? TariffGroupId { get; set; }
        public object ChargingProfile { get; set; }
        public bool AllowsReservation { get; set; }
        public Roaming Roaming { get; set; }
    }

    public class Connector
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public string Format { get; set; }
    }

    public class Roaming
    {
        public string EvseId { get; set; }
        public string PhysicalReference { get; set; }
        public List<string> TariffIds { get; set; }
        public List<string> Capabilities { get; set; }
        public string Status { get; set; }
    }

    public class Subscription
    {
        public bool SubscriptionRequired { get; set; }
        public List<string> SubscriptionPlanIds { get; set; }
    }

    public class LastBootNotification
    {
        public string Reason { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Vendor { get; set; } = string.Empty;
        public string ChargeBoxSerialNumber { get; set; } = string.Empty;
        public string ChargePointSerialNumber { get; set; } = string.Empty;
        public string FirmwareVersion { get; set; } = string.Empty;
        public string Iccid { get; set; } = string.Empty;
        public string Imsi { get; set; } = string.Empty;
        public string MeterSerialNumber { get; set; } = string.Empty;
        public string MeterType { get; set; } = string.Empty;
    }

}
