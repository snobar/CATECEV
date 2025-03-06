using CATECEV.API.Helper.IService;
using CATECEV.API.Models;
using CATECEV.CORE.Extensions;
using CATECEV.CORE.Framework;

namespace CATECEV.API.Helper.Service
{
    public class AMPECOSessions : IAMPECOSessions
    {
        private string _userApi;
        private string _token;

        private readonly IHttpClientService _httpClientService;

        public AMPECOSessions(IHttpClientService httpClientService)
        {
            _userApi = $"{Utility.GetAppsettingsValue("AmpecoBaseUrl")}{Utility.GetAppsettingsValue("Resources", "Sessions")}";
            _token = Utility.GetAppsettingsValue("AccessToken");
            _httpClientService = httpClientService;
        }

        public async Task<AMPECOResponseModel<IEnumerable<Models.AMPECO.resource.Session.ChargingSession>>> GetChargePoints(int pageNumber, int pageSize = 100)
        {

            var apiUrl = $"{_userApi}?page={pageNumber}&per_page={pageSize}";
            var chargingSessionData = await _httpClientService.GetAsync<IEnumerable<Models.AMPECO.resource.Session.ChargingSession>>(apiUrl, _token);
            if (chargingSessionData.IsNotNullOrEmpty() && chargingSessionData.Data.IsNotNullOrEmpty() && chargingSessionData.IsSuccess && chargingSessionData.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return new AMPECOResponseModel<IEnumerable<Models.AMPECO.resource.Session.ChargingSession>>
                {
                    Data = chargingSessionData.Data,
                    TotalPages = chargingSessionData.TotalPages,
                    TotalRecords = chargingSessionData.TotalRecords,
                    CurrentPage = chargingSessionData.CurrentPage,
                };
            }

            return new AMPECOResponseModel<IEnumerable<Models.AMPECO.resource.Session.ChargingSession>>();
        }
    }
}
