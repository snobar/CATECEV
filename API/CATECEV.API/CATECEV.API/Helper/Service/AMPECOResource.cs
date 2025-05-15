using CATECEV.API.Helper.IService;
using CATECEV.API.Models;
using CATECEV.CORE.Extensions;
using CATECEV.CORE.Framework;
using System.Collections.Concurrent;

namespace CATECEV.API.Helper.Service
{
    public class AMPECOResource<T> : IAMPECOResource<T> where T : class
    {
        private string _ampecoBaseUrl;
        private string _token;

        private readonly IHttpClientService _httpClientService;

        public AMPECOResource(IHttpClientService httpClientService)
        {
            var sss = typeof(T).Name;

            if (typeof(T) == typeof(Models.AMPECO.resource.users.UserGroup))
            {
                _ampecoBaseUrl = $"{Utility.GetAppsettingsValue("AMPECOConfiguration", "AmpecoBaseUrl")}{Utility.GetAppsettingsValue("Resources", "UserGroup")}";
            }
            else if (typeof(T) == typeof(Models.AMPECO.resource.ChargePoint.ChargePoint))
            {
                _ampecoBaseUrl = $"{Utility.GetAppsettingsValue("AMPECOConfiguration", "AmpecoBaseUrl")}{Utility.GetAppsettingsValue("Resources", "ChargePoint")}";
            }
            else if (typeof(T) == typeof(Models.AMPECO.resource.ChargePoint.Evse))
            {
                _ampecoBaseUrl = $"{Utility.GetAppsettingsValue("AMPECOConfiguration", "AmpecoBaseUrl")}{Utility.GetAppsettingsValue("Resources", "Evse")}";
            }
            else if (typeof(T) == typeof(Models.AMPECO.resource.Session.ChargingSession))
            {
                _ampecoBaseUrl = $"{Utility.GetAppsettingsValue("AMPECOConfiguration", "AmpecoBaseUrl")}{Utility.GetAppsettingsValue("Resources", "ChargingSession")}";
            }
            else if (typeof(T) == typeof(Models.AMPECO.resource.Tax.Tax))
            {
                _ampecoBaseUrl = $"{Utility.GetAppsettingsValue("AMPECOConfiguration", "AmpecoBaseUrl")}{Utility.GetAppsettingsValue("Resources", "Tax")}";
            }
            else if (typeof(T) == typeof(Models.AMPECO.resource.users.User))
            {
                _ampecoBaseUrl = $"{Utility.GetAppsettingsValue("AMPECOConfiguration", "AmpecoBaseUrl")}{Utility.GetAppsettingsValue("Resources", "User")}";
            }
            else if (typeof(T) == typeof(Models.AMPECO.resource.Authorization.Authorization))
            {
                _ampecoBaseUrl = $"{Utility.GetAppsettingsValue("AMPECOConfiguration", "AmpecoBaseUrl")}{Utility.GetAppsettingsValue("Resources", "Authorization")}";
            }
            else if (typeof(T) == typeof(Models.AMPECO.resource.Location.Location))
            {
                _ampecoBaseUrl = $"{Utility.GetAppsettingsValue("AMPECOConfiguration", "AmpecoBaseUrl")}{Utility.GetAppsettingsValue("Resources", "Location")}";
            }
            else if (typeof(T) == typeof(Models.AMPECO.resource.Partner.AMPECOPartner))
            {
                _ampecoBaseUrl = $"{Utility.GetAppsettingsValue("AMPECOConfiguration", "AmpecoBaseUrl")}{Utility.GetAppsettingsValue("Resources", "AMPECOPartner")}";
            }
            else if (typeof(T) == typeof(Models.AMPECO.resource.PartnerExpenses.PartnerExpensesResponse))
            {
                _ampecoBaseUrl = $"{Utility.GetAppsettingsValue("AMPECOConfiguration", "AmpecoBaseUrl")}{Utility.GetAppsettingsValue("Resources", "AMPECOPartnerExpense")}";
            }

            _token = Utility.GetAppsettingsValue("AMPECOConfiguration", "AccessToken");
            _httpClientService = httpClientService;
        }

        public async Task<Models.ResponseModel<IEnumerable<T>>> GetResourceDataList(int pageNumber, int pageSize = 100, string _fromDate = "", string _toDate = "")
        {

            var apiUrl = $"{_ampecoBaseUrl}?page={pageNumber}&per_page={pageSize}";

            if (typeof(T) == typeof(Models.AMPECO.resource.Session.ChargingSession) && _fromDate.IsNotNullOrEmpty() && _toDate.IsNotNullOrEmpty())
            {
                apiUrl += $"&filter[startedAfter]={Uri.EscapeDataString(_fromDate)}&filter[startedBefore]={Uri.EscapeDataString(_toDate)}";
            }
            else if (typeof(T) == typeof(Models.AMPECO.resource.Authorization.Authorization) && _fromDate.IsNotNullOrEmpty() && _toDate.IsNotNullOrEmpty())
            {
                apiUrl += $"&filter[lastUpdatedAfter]={Uri.EscapeDataString(_fromDate)}&filter[lastUpdatedBefore]={Uri.EscapeDataString(_toDate)}";
            }

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

        public async Task<IEnumerable<T>> GetFullResourcesData(string fromDate = "", string toDate = "")
        {
            var pageNumber = 1;

            var resourceData = await GetResourceDataList(pageNumber, _fromDate: fromDate, _toDate: toDate);
            if (resourceData is null || !resourceData.Data.IsNotNullOrEmpty() || resourceData.TotalRecords <= 0)
                return new List<T>();

            var totalPages = resourceData.TotalPages;
            var batchSize = 20;
            var resourceDataList = new ConcurrentBag<T>();

            var dateTime = DateTime.Now;

            for (var i = 1; i <= totalPages; i += batchSize)
            {
                var tasks = Enumerable.Range(i, Math.Min(batchSize, totalPages - i + 1))
                    .Select(async page =>
                    {
                        var pageData = await GetResourceDataList(page, _fromDate: fromDate, _toDate: toDate);
                        if (pageData != null && pageData.Data.IsNotNullOrEmpty())
                        {
                            foreach (var item in pageData.Data)
                            {
                                resourceDataList.Add(item);
                            }
                        }
                    });

                await Task.WhenAll(tasks);
            }

            return resourceDataList;
        }
    }
}
