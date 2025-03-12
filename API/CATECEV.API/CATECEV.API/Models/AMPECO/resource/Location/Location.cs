namespace CATECEV.API.Models.AMPECO.resource.Location
{
    public class Location
    {
        public int Id { get; set; }
        public List<LocalizedText> Name { get; set; } = new();
        public string Status { get; set; }
        public List<LocalizedText> Description { get; set; } = new();
        public List<LocalizedText> ShortDescription { get; set; } = new();
        public List<LocalizedText> AdditionalDescription { get; set; } = new();
        public Geoposition Geoposition { get; set; }
        public List<LocalizedText> Address { get; set; } = new();
        public string City { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string PostCode { get; set; }
        public WorkingHours WorkingHours { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public List<string> Tags { get; set; } = new();
        public int RoamingOperatorId { get; set; }
    }

    public class LocalizedText
    {
        public string Locale { get; set; }
        public string Translation { get; set; }
    }

    public class Geoposition
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class WorkingHours
    {
        public bool IsAlwaysOpen { get; set; }
    }

}
