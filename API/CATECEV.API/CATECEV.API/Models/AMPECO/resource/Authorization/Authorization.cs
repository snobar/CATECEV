namespace CATECEV.API.Models.AMPECO.resource.Authorization
{
    public class Authorization
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Method { get; set; }
        public string Status { get; set; }
        public string RejectionReason { get; set; }
        public string Source { get; set; }
        public string IdTagUid { get; set; }
        public PlatformData Roaming { get; set; }
        public int? ChargePointId { get; set; }
        public int? EvseId { get; set; }
        public List<int> SessionIds { get; set; } = new List<int>();
        public DateTime LastUpdatedAt { get; set; }
    }

    public class PlatformData
    {
        public int PlatformId { get; set; }
        public int PlatformRoleId { get; set; }
        public string PlatformRole { get; set; }
        public string Reference { get; set; }
        public string Client { get; set; }
    }
}
