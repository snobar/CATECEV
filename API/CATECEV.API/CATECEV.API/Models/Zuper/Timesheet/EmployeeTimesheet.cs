namespace CATECEV.API.Models.Zuper.Timesheet
{
    public class EmployeeTimesheet
    {
        public string employee_timesheet_uid { get; set; }
        public string type_of_check { get; set; }
        public double? latitude { get; set; }
        public double? longitude { get; set; }
        public object auth_pic { get; set; }
        public object remarks { get; set; }
        public DateTime? checked_time { get; set; }
        public DateTime? created_at { get; set; }
        public ZuperUser users { get; set; }
        public ZuperUser created_user { get; set; }
        public object timesheet_location { get; set; }
    }

    public class ZuperTimesheetData
    {
        public int total_pages { get; set; }
        public int current_page { get; set; }
        public int total_records { get; set; }
        public List<EmployeeTimesheet> timesheets { get; set; }
    }
}
