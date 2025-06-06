﻿namespace CATECEV.API.Models
{
    public class ResponseModel<T>
    {
        public T Data { get; set; }
        public int CurrentPage { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPages { get; set; }
    }

    public class PaginatedResponse<T>
    {
        public List<T> Data { get; set; }
        public Links Links { get; set; }
        public Meta Meta { get; set; }
    }

    public class Links
    {
        public string First { get; set; }
        public string Last { get; set; }
        public string Prev { get; set; }
        public string Next { get; set; }
    }

    public class Meta
    {
        public string Path { get; set; }
        public int PerPage { get; set; }
        public string NextCursor { get; set; }
        public string PrevCursor { get; set; }
    }


}
