namespace CATECEV.API.Models.AMPECO.resource.users
{
    public class UserGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? PartnerId { get; set; }
        public string Description { get; set; }
        public DateTime LastUpdatedAt { get; set; }
    }
}
