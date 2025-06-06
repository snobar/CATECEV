﻿using CATECEV.Models.Entity.Base;

namespace CATECEV.Models.Entity
{
    public class City : BaseEntity
    {
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }
        public int CountryId { get; set; }

        public ICollection<Company> Companies { get; set; }
        public Country Country { get; set; }
    }
}
