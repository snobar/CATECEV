using CATECEV.API.Helper.IService;
using CATECEV.API.Models;
using CATECEV.CORE.Extensions;
using CATECEV.CORE.Framework;

namespace CATECEV.API.Helper.Service
{
    public class AMPECOTaxes : IAMPECOTaxes
    {
        private string _userApi;
        private string _token;

        private readonly IHttpClientService _httpClientService;

        public AMPECOTaxes(IHttpClientService httpClientService)
        {
            _userApi = $"{Utility.GetAppsettingsValue("AmpecoBaseUrl")}{Utility.GetAppsettingsValue("Resources", "Taxes")}";
            _token = Utility.GetAppsettingsValue("AccessToken");
            _httpClientService = httpClientService;
        }


        public async Task<AMPECOResponseModel<IEnumerable<Models.AMPECO.resource.Tax.Tax>>> GetTaxes(int pageNumber, int pageSize = 100)
        {

            var apiUrl = $"{_userApi}?page={pageNumber}&per_page={pageSize}";
            var taxData = await _httpClientService.GetAsync<IEnumerable<Models.AMPECO.resource.Tax.Tax>>(apiUrl, _token);
            if (taxData.IsNotNullOrEmpty() && taxData.Data.IsNotNullOrEmpty() && taxData.IsSuccess && taxData.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return new AMPECOResponseModel<IEnumerable<Models.AMPECO.resource.Tax.Tax>>
                {
                    Data = taxData.Data,
                    TotalPages = taxData.TotalPages,
                    TotalRecords = taxData.TotalRecords,
                    CurrentPage = taxData.CurrentPage,
                };
            }

            return new AMPECOResponseModel<IEnumerable<Models.AMPECO.resource.Tax.Tax>>();
        }
    }
}
