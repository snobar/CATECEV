using CATECEV.API.Helper.IService;
using CATECEV.API.Models.Zuper.Teams;
using CATECEV.CORE.Extensions;
using CATECEV.CORE.Framework;
using System;

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

        public async Task<int> GetTimesheet()
        {
            Dictionary<string, string> headersMap = new Dictionary<string, string> { { "x-api-key", _token }, { "accept", "application/json" } };

            DateTime currentDate = DateTime.Now;
            DateTime FirstDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 9, 0, 0);

            DateTime desiredDate = DateTime.Now.AddDays(14);
            DateTime SecondDate = new DateTime(desiredDate.Year, desiredDate.Month, desiredDate.Day, 18, 0, 0);

            // Set up API URL
            string fromDateTime = FirstDate.ToString("yyyy-MM-dd HH:mm:ss");
            string toDateTime = SecondDate.ToString("yyyy-MM-dd HH:mm:ss");
            string userUid = Utility.GetAppsettingsValue("ZuperConfiguration", "UsersIds");
            string jobDuration = Utility.GetAppsettingsValue("ZuperConfiguration", "JobDuration");
            string user_type = Utility.GetAppsettingsValue("ZuperConfiguration", "UserType");
            string timesheetAPIURL = $"{_zuperBaseUrl}/timesheets?date={fromDateTime}&to_date={toDateTime}";

            var timesheetData = await _httpClientService.GetAsync<int>("https://app.zuperpro.com/api/timesheets", _token, headersMap);

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://app.zuperpro.com/api/timesheets?page=1&limit=10&sort=ASC&sort_by=checked_time&date=2025-03-01&to_date=2025-03-24"),
                Headers =
    {
        { "accept", "application/json" },
        { "x-api-key", "a6d316cb51e39dbf84366251b8fcedad" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                Console.WriteLine(body);
            }

            return 0;
        }
    }
}
