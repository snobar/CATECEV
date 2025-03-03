﻿namespace CATECEV.API.Models
{
    public class AMPECOResponseModel<T>
    {
        public T Data { get; set; }
        public int CurrentPage { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPages { get; set; }
    }
}
