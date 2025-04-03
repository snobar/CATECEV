using CATECEV.API.Helper.IService;
using CATECEV.API.Models.Zuper;
using CATECEV.API.Models.Zuper.Teams;
using CATECEV.API.Models.Zuper.Timesheet;
using CATECEV.CORE.Extensions;
using CATECEV.CORE.Framework;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CATECEV.API.Helper.Service
{
    public class Zuper : IZuper
    {
        private string _zuperBaseUrl;
        private string _token;

        private readonly IHttpClientService _httpClientService;

        public Zuper(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;

            _zuperBaseUrl = $"{Utility.GetAppsettingsValue("ZuperConfiguration", "ZuperAPIURL")}";

            _token = Utility.GetAppsettingsValue("ZuperConfiguration", "Token");
        }

        public async Task<Models.ResponseModel<IEnumerable<ZuperTeam>>> GetTeams()
        {
            Dictionary<string, string> headersMap = new Dictionary<string, string> { { "x-api-key", _token }, { "accept", "application/json" } };
            var teamsData = await _httpClientService.GetAsync<IEnumerable<ZuperTeam>>("https://app.zuperpro.com/api/teams/summary", _token, apiHeader: headersMap);

            if (teamsData.IsNotNullOrEmpty() && teamsData.StatusCode == System.Net.HttpStatusCode.OK && teamsData.Data.IsNotNullOrEmpty())
            {
                return new Models.ResponseModel<IEnumerable<ZuperTeam>>
                {
                    Data = teamsData.Data,
                    TotalRecords = teamsData.TotalRecords,
                };
            }

            return new Models.ResponseModel<IEnumerable<ZuperTeam>>();
        }

        public async Task<ZuperResponse<ZuperTimesheetData>> GetTimesheet(int count,string date,string to_date)
        {
            Dictionary<string, string> headersMap = new Dictionary<string, string> { { "x-api-key", _token }, { "accept", "application/json" } };

            string timesheetAPIURL = $"{_zuperBaseUrl}/timesheets?count={count}&date={date}&page=1&to_date={to_date}";

            var timesheetData = await _httpClientService.GetAsync2<ZuperResponse<ZuperTimesheetData>>(timesheetAPIURL, _token, headersMap);

            if (timesheetData.IsNotNullOrEmpty() && timesheetData.status == "success" && timesheetData.data.IsNotNullOrEmpty())
            {
                while (timesheetData.data.total_records > count)
                {
                    count = count * 2;
                    timesheetAPIURL = $"{_zuperBaseUrl}/timesheets?count={count}&date={date}&page=1&to_date={to_date}";

                    timesheetData = await _httpClientService.GetAsync2<ZuperResponse<ZuperTimesheetData>>(timesheetAPIURL, _token, headersMap);

                    if (timesheetData.IsNotNullOrEmpty() && timesheetData.status == "success" && timesheetData.data.IsNotNullOrEmpty() && timesheetData.data.total_records <= count)
                    {
                        return timesheetData;
                    }
                }
            }

            return timesheetData;
        }

        public async Task<ZuperResponse<List<TimeoffRequestType>>> GetTimeoffRequestType()
        {
            Dictionary<string, string> headersMap = new Dictionary<string, string> { { "x-api-key", _token }, { "accept", "application/json" } };

            string timeoffRequestTypeAPIURL = $"{_zuperBaseUrl}/timesheet/request/timeoff_type";

            var timesheetData = await _httpClientService.GetAsync2<ZuperResponse<List<TimeoffRequestType>>>(timeoffRequestTypeAPIURL, _token, headersMap);

            if (timesheetData.IsNotNullOrEmpty() && timesheetData.data.IsNotNullOrEmpty())
            {
                timesheetData.total_records = timesheetData.data.Count();
            }

            return timesheetData;
        }

        public async Task<ZuperResponse<List<ZuperUser>>> GetZuperUsers()
        {
            Dictionary<string, string> headersMap = new Dictionary<string, string> { { "x-api-key", _token }, { "accept", "application/json" } };

            string timeoffRequestTypeAPIURL = $"{_zuperBaseUrl}/user/all";

            var usersData = await _httpClientService.GetAsync2<ZuperResponse<List<ZuperUser>>>(timeoffRequestTypeAPIURL, _token, headersMap);

            return usersData;
        }
    }
}
