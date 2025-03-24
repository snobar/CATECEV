using CATECEV.API.Helper.IService;
using CATECEV.API.Models;
using CATECEV.CORE.Extensions;
using CATECEV.CORE.Framework;

namespace CATECEV.API.Helper.Service
{
    public class AMPECOResource<T> : IAMPECOResource<T> where T : class
    {
        private string _ampecoBaseUrl;
        private string _token;

        private readonly IHttpClientService _httpClientService;

        public AMPECOResource(IHttpClientService httpClientService)
        {
            if (typeof(T) == typeof(Models.AMPECO.resource.users.UserGroup))
            {
                _ampecoBaseUrl = $"{Utility.GetAppsettingsValue("AMPECOConfiguration", "AmpecoBaseUrl")}{Utility.GetAppsettingsValue("Resources", "UserGroup")}";
            }
            else if (typeof(T) == typeof(Models.AMPECO.resource.ChargePoint.ChargePoint))
            {
                _ampecoBaseUrl = $"{Utility.GetAppsettingsValue("AMPECOConfiguration", "AmpecoBaseUrl")}{Utility.GetAppsettingsValue("Resources", "ChargePoints")}";
            }
            else if (typeof(T) == typeof(Models.AMPECO.resource.ChargePoint.Evse))
            {
                _ampecoBaseUrl = $"{Utility.GetAppsettingsValue("AMPECOConfiguration", "AmpecoBaseUrl")}{Utility.GetAppsettingsValue("Resources", "Evses")}";
            }
            else if (typeof(T) == typeof(Models.AMPECO.resource.Session.ChargingSession))
            {
                _ampecoBaseUrl = $"{Utility.GetAppsettingsValue("AMPECOConfiguration", "AmpecoBaseUrl")}{Utility.GetAppsettingsValue("Resources", "Sessions")}";
            }
            else if (typeof(T) == typeof(Models.AMPECO.resource.Tax.Tax))
            {
                _ampecoBaseUrl = $"{Utility.GetAppsettingsValue("AMPECOConfiguration", "AmpecoBaseUrl")}{Utility.GetAppsettingsValue("Resources", "Taxes")}";
            }
            else if (typeof(T) == typeof(Models.AMPECO.resource.users.User))
            {
                _ampecoBaseUrl = $"{Utility.GetAppsettingsValue("AMPECOConfiguration", "AmpecoBaseUrl")}{Utility.GetAppsettingsValue("Resources", "User")}";
            }
            else if (typeof(T) == typeof(Models.AMPECO.resource.Authorization.Authorization))
            {
                _ampecoBaseUrl = $"{Utility.GetAppsettingsValue("AMPECOConfiguration", "AmpecoBaseUrl")}{Utility.GetAppsettingsValue("Resources", "Authorizations")}";
            }
            else if (typeof(T) == typeof(Models.AMPECO.resource.Location.Location))
            {
                _ampecoBaseUrl = $"{Utility.GetAppsettingsValue("AMPECOConfiguration", "AmpecoBaseUrl")}{Utility.GetAppsettingsValue("Resources", "Location")}";
            }

            _token = Utility.GetAppsettingsValue("AMPECOConfiguration", "AccessToken");
            _httpClientService = httpClientService;
        }

        public async Task<Models.ResponseModel<IEnumerable<T>>> GetResourceDataList(int pageNumber, int pageSize = 100)
        {

            var apiUrl = $"{_ampecoBaseUrl}?page={pageNumber}&per_page={pageSize}";
            var chargePointData = await _httpClientService.GetAsync<IEnumerable<T>>(apiUrl, _token);

            if (chargePointData.IsNotNullOrEmpty() && chargePointData.Data.IsNotNullOrEmpty() && chargePointData.IsSuccess && chargePointData.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return new Models.ResponseModel<IEnumerable<T>>
                {
                    Data = chargePointData.Data,
                    TotalPages = chargePointData.TotalPages,
                    TotalRecords = chargePointData.TotalRecords,
                    CurrentPage = chargePointData.CurrentPage,
                };
            }

            return new Models.ResponseModel<IEnumerable<T>>();
        }

        public async Task<Models.ResponseModel<T>> GetResourceData(int AMPECOId)
        {

            var apiUrl = $"{_ampecoBaseUrl}/{AMPECOId}";
            var chargePointData = await _httpClientService.GetAsync<T>(apiUrl, _token);

            if (chargePointData.IsNotNullOrEmpty() && chargePointData.Data.IsNotNullOrEmpty() && chargePointData.IsSuccess && chargePointData.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return new Models.ResponseModel<T>
                {
                    Data = chargePointData.Data,
                    TotalPages = chargePointData.TotalPages,
                    TotalRecords = chargePointData.TotalRecords,
                    CurrentPage = chargePointData.CurrentPage,
                };
            }

            return new Models.ResponseModel<T>();
        }
    }
}
