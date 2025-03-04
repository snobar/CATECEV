namespace CATECEV.API.Models.AMPECO.resource.Tax
{
    public class Tax
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Percentage { get; set; }
        public int TaxIdentificationNumberId { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public List<DisplayName> DisplayName { get; set; }
    }

    public class DisplayName
    {
        public string Locale { get; set; }
        public string Translation { get; set; }
    }
}
