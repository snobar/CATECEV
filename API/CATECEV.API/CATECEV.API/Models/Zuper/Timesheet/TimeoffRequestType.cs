namespace CATECEV.API.Models.Zuper.Timesheet
{
    public class TimeoffRequestType
    {
        public string timeoff_request_type_uid { get; set; }
        public string name { get; set; }
        public int no_of_days_per_year { get; set; }
        public string type { get; set; }
        public object display_order { get; set; }
        public DateTime created_at { get; set; }
        public ZuperUser created_by_user { get; set; }
    }
}
