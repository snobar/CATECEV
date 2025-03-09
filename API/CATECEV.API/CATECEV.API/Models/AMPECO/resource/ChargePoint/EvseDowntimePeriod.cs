namespace CATECEV.API.Models.AMPECO.resource.ChargePoint
{
    public class EvseDowntimePeriod
    {
        public int EvseId { get; set; }
        public int Id { get; set; }
        public int ChargePointId { get; set; }
        public int LocationId { get; set; }
        public string EntryMode { get; set; }
        public string Type { get; set; }
        public EvseDowntimePeriodStatuses Statuses { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? EndedAt { get; set; }
    }

    public class EvseDowntimePeriodStatuses
    {
        public string System { get; set; }
        public string Network { get; set; }
        public string Hardware { get; set; }
    }
}
