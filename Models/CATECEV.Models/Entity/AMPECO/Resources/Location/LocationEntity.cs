using CATECEV.Models.Entity.Base;

namespace CATECEV.Models.Entity.AMPECO.Resources.Location
{
    public class LocationEntity : BaseEntity
    {
        public int AMPECOId { get; set; }
        public string Status { get; set; }
        public ICollection<LocationNameLocalization> Name { get; set; }
        public ICollection<LocationDescriptionLocalization> Description { get; set; }
        public ICollection<LocationShortDescriptionLocalization> ShortDescription { get; set; }
        public ICollection<LocationAdditionalDescriptionLocalization> AdditionalDescription { get; set; }
        public ICollection<LocationAddressLocalization> Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string PostCode { get; set; }
        public bool IsAlwaysOpen { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public List<string> Tags { get; set; } = new();
        public int RoamingOperatorId { get; set; }
    }

    public class LocationNameLocalization : BaseEntity
    {
        public string Locale { get; set; }
        public string Translation { get; set; }

        public int LocationId { get; set; }
        public LocationEntity Location { get; set; }
    }

    public class LocationDescriptionLocalization : BaseEntity
    {
        public string Locale { get; set; }
        public string Translation { get; set; }

        public int LocationId { get; set; }
        public LocationEntity Location { get; set; }
    }

    public class LocationShortDescriptionLocalization : BaseEntity
    {
        public string Locale { get; set; }
        public string Translation { get; set; }

        public int LocationId { get; set; }
        public LocationEntity Location { get; set; }
    }

    public class LocationAdditionalDescriptionLocalization : BaseEntity
    {
        public string Locale { get; set; }
        public string Translation { get; set; }

        public int LocationId { get; set; }
        public LocationEntity Location { get; set; }
    }

    public class LocationAddressLocalization : BaseEntity
    {
        public string Locale { get; set; }
        public string Translation { get; set; }

        public int LocationId { get; set; }
        public LocationEntity Location { get; set; }
    }
}
