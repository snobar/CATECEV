using CATECEV.API.Helper.IService;
using CATECEV.API.Models;
using CATECEV.CORE.Extensions;
using CATECEV.CORE.Framework;

namespace CATECEV.API.Helper.Service
{
    public class AMPECOChargePoints : IAMPECOChargePoints
    {
        private string _userApi;
        private string _token;

        private readonly IHttpClientService _httpClientService;

        public AMPECOChargePoints(IHttpClientService httpClientService)
        {
            _userApi = $"{Utility.GetAppsettingsValue("AmpecoBaseUrl")}{Utility.GetAppsettingsValue("Resources", "ChargePoints")}";
            _token = Utility.GetAppsettingsValue("AccessToken");
            _httpClientService = httpClientService;
        }

        public async Task<AMPECOResponseModel<IEnumerable<Models.AMPECO.resource.ChargePoint.ChargePoint>>> GetChargePoints(int pageNumber, int pageSize = 100)
        {

            var apiUrl = $"{_userApi}?page={pageNumber}&per_page={pageSize}";
            var chargePointData = await _httpClientService.GetAsync<IEnumerable<Models.AMPECO.resource.ChargePoint.ChargePoint>>(apiUrl, _token);
            if (chargePointData.IsNotNullOrEmpty() && chargePointData.Data.IsNotNullOrEmpty() && chargePointData.IsSuccess && chargePointData.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return new AMPECOResponseModel<IEnumerable<Models.AMPECO.resource.ChargePoint.ChargePoint>>
                {
                    Data = chargePointData.Data,
                    TotalPages = chargePointData.TotalPages,
                    TotalRecords = chargePointData.TotalRecords,
                    CurrentPage = chargePointData.CurrentPage,
                };
            }

            return new AMPECOResponseModel<IEnumerable<Models.AMPECO.resource.ChargePoint.ChargePoint>>();
        }
    }
}
