namespace CATECEV.API.Models.Reports.Sessions
{
    public class ChargingSessionViewModel
    {
        public int ID { get; set; }
        public string EVCSNumber { get; set; }
        public string ChargePointName { get; set; }
        public DateTime? DateOfChargingEvent { get; set; }
        public TimeSpan? StartTimeOfCharging { get; set; }
        public DateTime? StoppedDateOfChargingEvent { get; set; }
        public TimeSpan? EndTimeOfCharging { get; set; }
        public int? TotalChargingDurationIncludingIdling { get; set; }
        public decimal TotalEnergySuppliedByEVCSExcludingIdling { get; set; }
        public decimal ChargingServicesRetailPrice { get; set; }
        public string StopReason { get; set; }
        public string RoamingCPO { get; set; }
    }
}
