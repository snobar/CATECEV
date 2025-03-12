using CATECEV.API.Helper.IService;
using CATECEV.API.Models;
using CATECEV.CORE.Extensions;
using CATECEV.CORE.Framework;

namespace CATECEV.API.Helper.Service
{
    public class AMPECOResource<T> : IAMPECOResource<T> where T : class
    {
        private string _userApi;
        private string _token;

        private readonly IHttpClientService _httpClientService;

        public AMPECOResource(IHttpClientService httpClientService)
        {
            if (typeof(T) == typeof(Models.AMPECO.resource.users.UserGroup))
            {
                _userApi = $"{Utility.GetAppsettingsValue("AmpecoBaseUrl")}{Utility.GetAppsettingsValue("Resources", "UserGroup")}";
            }
            else if (typeof(T) == typeof(Models.AMPECO.resource.ChargePoint.ChargePoint))
            {
                _userApi = $"{Utility.GetAppsettingsValue("AmpecoBaseUrl")}{Utility.GetAppsettingsValue("Resources", "ChargePoints")}";
            }
            else if (typeof(T) == typeof(Models.AMPECO.resource.ChargePoint.Evse))
            {
                _userApi = $"{Utility.GetAppsettingsValue("AmpecoBaseUrl")}{Utility.GetAppsettingsValue("Resources", "Evses")}";
            }
            else if (typeof(T) == typeof(Models.AMPECO.resource.Session.ChargingSession))
            {
                _userApi = $"{Utility.GetAppsettingsValue("AmpecoBaseUrl")}{Utility.GetAppsettingsValue("Resources", "Sessions")}";
            }
            else if (typeof(T) == typeof(Models.AMPECO.resource.Tax.Tax))
            {
                _userApi = $"{Utility.GetAppsettingsValue("AmpecoBaseUrl")}{Utility.GetAppsettingsValue("Resources", "Taxes")}";
            }
            else if (typeof(T) == typeof(Models.AMPECO.resource.users.User))
            {
                _userApi = $"{Utility.GetAppsettingsValue("AmpecoBaseUrl")}{Utility.GetAppsettingsValue("Resources", "User")}";
            }

            _token = Utility.GetAppsettingsValue("AccessToken");
            _httpClientService = httpClientService;
        }

        public async Task<AMPECOResponseModel<IEnumerable<T>>> GetResourceData(int pageNumber, int pageSize = 100)
        {

            var apiUrl = $"{_userApi}?page={pageNumber}&per_page={pageSize}";
            var chargePointData = await _httpClientService.GetAsync<IEnumerable<T>>(apiUrl, _token);

            if (chargePointData.IsNotNullOrEmpty() && chargePointData.Data.IsNotNullOrEmpty() && chargePointData.IsSuccess && chargePointData.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return new AMPECOResponseModel<IEnumerable<T>>
                {
                    Data = chargePointData.Data,
                    TotalPages = chargePointData.TotalPages,
                    TotalRecords = chargePointData.TotalRecords,
                    CurrentPage = chargePointData.CurrentPage,
                };
            }

            return new AMPECOResponseModel<IEnumerable<T>>();
        }
    }
}
