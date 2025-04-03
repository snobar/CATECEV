namespace CATECEV.API.Models.Zuper
{
    public class ZuperResponse<T>
    {
        public string status { get; set; }
        public string type { get; set; }
        public int total_pages { get; set; }
        public int current_page { get; set; }
        public int total_records { get; set; }
        public T data { get; set; }
    }
}
