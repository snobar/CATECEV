using AutoMapper;
using CATECEV.API.Helper.IService;
using CATECEV.API.Models.AMPECO.resource.Partner;
using CATECEV.API.Models.AMPECO.resource.PartnerExpenses;
using CATECEV.API.Models.Reports.Sessions;
using CATECEV.CORE.Extensions;
using CATECEV.CORE.Framework;
using CATECEV.CORE.Logger;
using CATECEV.CORE.model;
using CATECEV.CORE.Wrapper;
using CATECEV.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Collections.Concurrent;

namespace CATECEV.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly AppDBContext _appContext;
        private readonly IMapper _mapper;
        private readonly IAMPECOResource<Models.AMPECO.resource.Location.Location> _locationResource;
        private readonly IAMPECOResource<Models.AMPECO.resource.Partner.AMPECOPartner> _partnerResource;
        private readonly IAMPECOResource<Models.AMPECO.resource.PartnerExpenses.AMPECOPartnerExpense> _partnerExpenseResource;
        private string _token;

        private readonly IAMPECOResource<Models.AMPECO.resource.Session.ChargingSession> _aMPECOSessions;
        private readonly IAMPECOResource<Models.AMPECO.resource.users.User> _user;
        private readonly IAMPECOResource<Models.AMPECO.resource.ChargePoint.ChargePoint> _aMPECOChargePoints;
        private readonly IAMPECOResource<Models.AMPECO.resource.ChargePoint.Evse> _aMPECOEvses;
        private readonly IAMPECOResource<Models.AMPECO.resource.Authorization.Authorization> _aMPECOAuthorization;
        private readonly IAMPECOResource<Models.AMPECO.resource.users.UserGroup> _userGroupResource;

        private static Dictionary<int, Models.AMPECO.resource.Location.Location> locationKeyValuePairs = new Dictionary<int, Models.AMPECO.resource.Location.Location>();
        private static Dictionary<int, Models.AMPECO.resource.Partner.AMPECOPartner> partnerKeyValuePairs = new Dictionary<int, Models.AMPECO.resource.Partner.AMPECOPartner>();

        public ReportsController(AppDBContext appContext, IMapper mapper, IAMPECOResource<Models.AMPECO.resource.Location.Location> locationResource, IAMPECOResource<Models.AMPECO.resource.Partner.AMPECOPartner> partnerResource, IAMPECOResource<Models.AMPECO.resource.PartnerExpenses.AMPECOPartnerExpense> partnerExpenseResource, IAMPECOResource<Models.AMPECO.resource.Session.ChargingSession> aMPECOSessions, IAMPECOResource<Models.AMPECO.resource.users.User> user, IAMPECOResource<Models.AMPECO.resource.ChargePoint.ChargePoint> aMPECOChargePoints, IAMPECOResource<Models.AMPECO.resource.ChargePoint.Evse> aMPECOEvses, IAMPECOResource<Models.AMPECO.resource.Authorization.Authorization> aMPECOAuthorization, IAMPECOResource<Models.AMPECO.resource.users.UserGroup> userGroupResource)
        {
            _appContext = appContext;
            _mapper = mapper;
            _locationResource = locationResource;
            _partnerResource = partnerResource;
            _partnerExpenseResource = partnerExpenseResource;

            _aMPECOSessions = aMPECOSessions;
            _user = user;
            _aMPECOChargePoints = aMPECOChargePoints;
            _aMPECOEvses = aMPECOEvses;
            _aMPECOAuthorization = aMPECOAuthorization;
            _userGroupResource = userGroupResource;
            _token = Utility.GetAppsettingsValue("AMPECOConfiguration", "AccessToken");

        }

        [HttpGet("SessionsRawData")]
        public async Task<IActionResult> SessionsRawData(int partnerId, string fromDate, string toDate)
        {
            try
            {
                string[] validFormats = new[]
{
                    "yyyy-MM-dd"
                };


                var tempFromData = DateTime.Now;
                var tempToDate = DateTime.Now;

                if (DateTime.TryParseExact(fromDate, validFormats, null, System.Globalization.DateTimeStyles.None, out var _fromDate))
                {
                    tempFromData = _fromDate;
                    //_fromDate = _fromDate.AddHours(Utility.GetAppsettingsValue("AMPECOConfiguration", "TimeDiffAdd").ToAnyType<int>());
                    fromDate = _fromDate.ToString("yyyy-MM-dd HH:mm:ss.fff");
                }

                if (DateTime.TryParseExact(toDate, validFormats, null, System.Globalization.DateTimeStyles.None, out var _toDate))
                {
                    tempToDate = _toDate;
                    _toDate = _toDate.Date.AddDays(1).AddMilliseconds(-1);
                    //_toDate = _toDate.AddHours(Utility.GetAppsettingsValue("AMPECOConfiguration", "TimeDiffAdd").ToAnyType<int>());
                    toDate = _toDate.ToString("yyyy-MM-dd HH:mm:ss.fff");
                }

                //var tempFromData = fromDate;
                //var tempToDate = toDate;

                //if (DateTime.TryParse(tempFromData, out var _tempFromDate))
                //{
                //    _tempFromDate = _tempFromDate.Date.AddDays(-1);
                //    tempFromData = _fromDate.ToString("yyyy-MM-dd HH:mm:ss.fff");
                //}

                //if (DateTime.TryParse(tempToDate, out var _tempToDate))
                //{
                //    _tempToDate = _tempToDate.Date.AddDays(2).AddMilliseconds(-1);
                //    tempToDate = _tempToDate.ToString("yyyy-MM-dd HH:mm:ss.fff");
                //}

                var startTime = DateTime.Now;

                var sessions = (await _aMPECOSessions.GetFullResourcesData(_fromDate: fromDate, _toDate: toDate)).DistinctBy(x => x.Id).ToList();

                //sessions.Where(x => x.StartedAt.HasValue && x.StoppedAt.HasValue).ToList().ForEach(x =>
                //{
                //    x.StartedAt = x.StartedAt.Value.AddHours(Utility.GetAppsettingsValue("AMPECOConfiguration", "TimeDiffAdd").ToAnyType<int>());
                //    x.StoppedAt = x.StoppedAt.Value.AddHours(Utility.GetAppsettingsValue("AMPECOConfiguration", "TimeDiffAdd").ToAnyType<int>());
                //});

                foreach (var x in sessions)
                {
                    if (x.StartedAt.HasValue)
                    {
                        x.StartedAt = x.StartedAt.Value.AddHours(Utility.GetAppsettingsValue("AMPECOConfiguration", "TimeDiffAdd").ToAnyType<int>());
                    }

                    if (x.StoppedAt.HasValue)
                    {
                        x.StoppedAt = x.StoppedAt.Value.AddHours(Utility.GetAppsettingsValue("AMPECOConfiguration", "TimeDiffAdd").ToAnyType<int>());
                    }
                }

                var users = (await _user.GetFullResourcesData()).DistinctBy(x => x.Id).ToList();
                var chargePoints = (await _aMPECOChargePoints.GetFullResourcesData()).DistinctBy(x => x.Id).ToList();
                var evses = (await _aMPECOEvses.GetFullResourcesData()).DistinctBy(x => x.Id).ToList();
                var authorization = (await _aMPECOAuthorization.GetFullResourcesData(_fromDate: fromDate, _toDate: toDate)).DistinctBy(x => x.Id).ToList();
                var userGroups = (await _userGroupResource.GetFullResourcesData()).DistinctBy(x => x.Id).ToList();

                var usersDictionary = users.ToDictionary(x => x.Id);
                var chargePointsDictionary = chargePoints.ToDictionary(x => x.Id);
                var evsesDictionary = evses.ToDictionary(x => x.Id);
                var authorizationDictionary = authorization.ToDictionary(x => x.Id);
                var entityGroupDataDictionary = userGroups.ToDictionary(x => x.Id);

                var currentPartnerCharePointIds = chargePoints.Where(x => x.OwnerPartnerId == partnerId).Select(x => x.Id);

                var test = sessions.FirstOrDefault(x => x.Id == "92015");

                sessions = sessions.Where(x =>
                    currentPartnerCharePointIds.Contains(x.ChargePointId)
                    && (x.PaymentStatus == "partially" || x.PaymentStatus == "paid")
                    && x.StartedAt.Value >= _fromDate && x.StartedAt.Value <= _toDate
                    && x.Status == "finished").ToList();

                var test2 = sessions.FirstOrDefault(x => x.Id == "92015");

                /*               var ids = new List<string>
                {
                    "92811", "92810", "92809", "92808", "92807", "92806", "92805", "92804",
                    "92803", "92802", "92801", "92800", "92799", "92854", "92852", "92851",
                    "92850", "92848", "92847", "92846", "92844", "92843", "92842", "92841",
                    "92840", "92839", "92837", "92836", "92834", "92833", "92832", "92831",
                    "92830", "92829", "92827", "92826", "92825", "92824", "92823", "92822",
                    "92821", "92820", "92818", "92817", "92815", "92813", "92812", "92015",
                    "88206", "85914"
                };*/

                var SessionsRawData = sessions
                    .Select(a => new ChargingSessionViewModel
                    {
                        ID = a.Id,
                        //User = (u.FirstName ?? "") + " " + (u.LastName ?? ""),
                        UserID = a.UserId,
                        //Email = u.Email,
                        //UserGroups = string.Join(",",
                        //                   u.UserGroupIds
                        //                       .Select(id => entityGroupDataDictionary.TryGetValue(id, out var userGroup) ? userGroup.Name : null)
                        //                       .Where(name => name != null)
                        //               ),
                        StartDate = a.StartedAt.HasValue ? a.StartedAt.Value.Date : null,
                        StartTime = a.StartedAt.HasValue ? a.StartedAt.Value.TimeOfDay : null,
                        EndDate = a.StoppedAt.HasValue ? a.StoppedAt.Value.Date : null,
                        EndTime = a.StoppedAt.HasValue ? a.StoppedAt.Value.TimeOfDay : null,
                        DurationInSeconds = a.StartedAt.HasValue && a.StoppedAt.HasValue
                                            ? (long)(a.StoppedAt.Value - a.StartedAt.Value).TotalSeconds
                                            : (long?)null,
                        EnergySupplied = a.Energy,
                        SessionStatus = a.Status,
                        PaymentStatus = a.PaymentStatus,
                        TotalAmount = a.Amount,
                        TotalAmountWithoutTax = a.TotalAmount.WithTax,
                        Currency = a.Currency,
                        EVSEIdentifier = a.EvseId,
                        EVCSNumber = a.EvsePhysicalReference ?? string.Empty,
                        StopReason = a.Reason ?? string.Empty,
                        //EVSEType = e.CurrentType,
                        //ChargePointName = cp.Name ?? string.Empty,
                        //LocationId = cp.LocationId,
                        //AuthorizationMethod = au.Method,
                        IDTag = a.IdTag,
                        IDTagLabel = a.IdTagLabel,
                        ChargePointId = a.ChargePointId,
                        //Roaming = au.Roaming,
                        //RoamingCPO = e.Roaming is not null ? e.Roaming.Status : string.Empty,
                    }).ToList();


                foreach (var item in SessionsRawData)
                {
                    if (item.ID == "92015")
                    {

                    }
                    if (usersDictionary.TryGetValue(item.UserID.ToString(), out var userData))
                    {
                        item.User = (userData.FirstName ?? "") + " " + (userData.LastName ?? "");
                        item.Email = userData.Email;
                        item.UserGroups = string.Join(",",
                                           userData.UserGroupIds
                                               .Select(id => entityGroupDataDictionary.TryGetValue(id, out var userGroup) ? userGroup.Name : null)
                                               .Where(name => name != null)
                                       );
                    }

                    if (evsesDictionary.TryGetValue(item.EVSEIdentifier, out var evseData))
                    {
                        item.EVSEType = evseData.CurrentType;
                        item.RoamingCPO = evseData.Roaming is not null ? evseData.Roaming.Status : string.Empty;
                    }

                    if (chargePointsDictionary.TryGetValue(item.ChargePointId, out var chargePointData))
                    {
                        item.ChargePointName = chargePointData.Name;
                        item.LocationId = chargePointData.LocationId;
                        item.OwnerPartnerId = chargePointData.OwnerPartnerId;
                    }

                    if (authorizationDictionary.TryGetValue(item.AuthorizationId, out var authorizationData))
                    {
                        item.AuthorizationMethod = authorizationData.Method;
                    }

                    if (locationKeyValuePairs.TryGetValue(item.LocationId, out var location))
                    {
                        item.Location = location.Address.Where(x => x.Locale == "en").FirstOrDefault()?.Translation;
                        item.City = location.City;
                        item.PostCode = location.PostCode;
                        item.Country = location.Country;
                    }
                    else if (item.LocationId > 0)
                    {
                        var locationData = await _locationResource.GetResourceData(item.LocationId);

                        if (locationData.IsNotNullOrEmpty() && locationData.Data.IsNotNullOrEmpty() && locationData.Data.Id > 0
                            && locationKeyValuePairs.TryAdd(locationData.Data.Id, locationData.Data))
                        {
                            item.Location = locationData.Data.Address.Where(x => x.Locale == "en").FirstOrDefault()?.Translation;
                            item.City = locationData.Data.City;
                            item.PostCode = locationData.Data.PostCode;
                            item.Country = locationData.Data.Country;
                        }
                    }

                    if (item.OwnerPartnerId.HasValue && partnerKeyValuePairs.TryGetValue(item.OwnerPartnerId.Value, out var partner))
                    {
                        item.PartnerName = partner.BusinessName;
                    }
                    else if (item.OwnerPartnerId.HasValue && item.OwnerPartnerId.Value > 0)
                    {
                        var partnerData = await _partnerResource.GetResourceData(item.OwnerPartnerId.Value);

                        if (partnerData.IsNotNullOrEmpty() && partnerData.Data.IsNotNullOrEmpty() && partnerData.Data.Id > 0
                            && partnerKeyValuePairs.TryAdd(partnerData.Data.Id, partnerData.Data))
                        {
                            item.PartnerName = partnerData.Data.BusinessName;
                        }
                    }
                }

                var test4 = SessionsRawData.FirstOrDefault(x => x.ID == "92015");

                #region MyRegion
                //var result = from a in sessions
                //             join u in users on a.UserId.ToString() equals u.Id
                //             join e in evses on a.EvseId equals e.Id
                //             join cp in chargePoints on a.ChargePointId equals cp.Id
                //             join au in authorization on a.AuthorizationId equals au.Id
                //             where (a.PaymentStatus == "partially" || a.PaymentStatus == "paid")
                //             && a.Status == "finished"
                //             && cp.OwnerPartnerId == 16
                //             && (a.StartedAt.HasValue && a.StartedAt.Value.Date >= new DateTime(2025, 2, 1) && a.StartedAt.Value.Date <= new DateTime(2025, 2, 28))
                //             orderby a.Id descending
                //             select new ChargingSessionViewModel
                //             {
                //                 ID = a.Id,
                //                 User = (u.FirstName ?? "") + " " + (u.LastName ?? ""),
                //                 UserID = a.UserId,
                //                 Email = u.Email,
                //                 UserGroups = string.Join(",",
                //                       u.UserGroupIds
                //                           .Select(id => entityGroupDataDictionary.TryGetValue(id, out var userGroup) ? userGroup.Name : null)
                //                           .Where(name => name != null)
                //                   ),
                //                 StartDate = a.StartedAt.HasValue ? a.StartedAt.Value.Date : null,
                //                 StartTime = a.StartedAt.HasValue ? a.StartedAt.Value.TimeOfDay : null,
                //                 EndDate = a.StoppedAt.HasValue ? a.StoppedAt.Value.Date : null,
                //                 EndTime = a.StoppedAt.HasValue ? a.StoppedAt.Value.TimeOfDay : null,
                //                 DurationInSeconds = a.StartedAt.HasValue && a.StoppedAt.HasValue
                //                     ? (long)(a.StoppedAt.Value - a.StartedAt.Value).TotalSeconds
                //                     : (long?)null,
                //                 EnergySupplied = a.Energy,
                //                 SessionStatus = a.Status,
                //                 PaymentStatus = a.PaymentStatus,
                //                 TotalAmount = a.Amount,
                //                 TotalAmountWithoutTax = a.TotalAmount.WithTax,
                //                 Currency = a.Currency,
                //                 EVSEIdentifier = a.EvseId,
                //                 EVCSNumber = a.EvsePhysicalReference ?? string.Empty,
                //                 StopReason = a.Reason ?? string.Empty,
                //                 EVSEType = e.CurrentType,
                //                 ChargePointName = cp.Name ?? string.Empty,
                //                 LocationId = cp.LocationId,
                //                 AuthorizationMethod = au.Method,
                //                 IDTag = a.IdTag,
                //                 IDTagLabel = a.IdTagLabel,
                //                 Roaming = au.Roaming,
                //                 RoamingCPO = e.Roaming is not null ? e.Roaming.Status : string.Empty,
                //             };
                //var result = await (from a in _appContext.ChargingSession
                //                    join u in _appContext.User on a.AMPECOUserId equals u.AMPECOId
                //                    join e in _appContext.Evse on a.AMPECOEvseId equals e.AMPECOId
                //                    join cp in _appContext.ChargePoint on a.AMPECOChargePointId equals cp.AMPECOId
                //                    join au in _appContext.Authorization on a.AMPECOAuthorizationId equals au.AMPECOId
                //                    where a.Status == "finished" && a.StoppedAt != null && a.StartedAt != null
                //                    orderby a.AMPECOId descending
                //                    select new ChargingSessionViewModel
                //                    {
                //                        ID = a.AMPECOId,
                //                        User = (u.FirstName ?? "") + " " + (u.LastName ?? ""),
                //                        UserID = a.AMPECOUserId,
                //                        Email = u.Email,
                //                        UserGroups = u.UserGroups,
                //                        StartDate = a.StartedAt.HasValue ? a.StartedAt.Value.Date : (DateTime?)null,
                //                        StartTime = a.StartedAt.HasValue ? a.StartedAt.Value.TimeOfDay : (TimeSpan?)null,
                //                        EndDate = a.StoppedAt.HasValue ? a.StoppedAt.Value.Date : (DateTime?)null,
                //                        EndTime = a.StoppedAt.HasValue ? a.StoppedAt.Value.TimeOfDay : (TimeSpan?)null,
                //                        DurationInSeconds = a.StartedAt.HasValue && a.StoppedAt.HasValue
                //                            ? (long)(a.StoppedAt.Value - a.StartedAt.Value).TotalSeconds
                //                            : (long?)null,
                //                        EnergySupplied = a.Energy,
                //                        SessionStatus = a.Status,
                //                        PaymentStatus = a.PaymentStatus,
                //                        TotalAmount = a.Amount,
                //                        TotalAmountWithoutTax = a.TotalAmountWithoutTax,
                //                        Currency = a.Currency,
                //                        EVSEIdentifier = a.AMPECOEvseId,
                //                        EVCSNumber = a.EvsePhysicalReference ?? string.Empty,
                //                        StopReason = a.Reason ?? string.Empty,
                //                        EVSEType = e.CurrentType,
                //                        ChargePointName = cp.Name ?? string.Empty,
                //                        LocationId = cp.LocationId,
                //                        AuthorizationMethod = au.Method,
                //                        IDTag = a.IdTag,
                //                        IDTagLabel = a.IdTagLabel,
                //                        Roaming = au.Roaming,
                //                        RoamingCPO = e.RoamingStatus ?? string.Empty,
                //                    }).ToListAsync();
                #endregion

                var dateDiff = (DateTime.Now - startTime).TotalSeconds;
                return Ok(new { Total = SessionsRawData.Count(), dateDiff, SessionsRawData });
            }
            catch (Exception ex)
            {
                FileLogger.WriteLog($"SessionsRawData \nMessage: {ex.Message}\n InnerException: {ex.InnerException}");
                return StatusCode(500, false);
            }
        }


        [HttpGet("AmbecoPartnerData")]
        public async Task<IActionResult> AmbecoPartnerData(string fromDate, string toDate)
        {
            try
            {
                string[] validFormats = new[]
{
                    "yyyy-MM-dd"
                };


                var tempFromData = DateTime.Now;
                var tempToDate = DateTime.Now;

                if (DateTime.TryParseExact(fromDate, validFormats, null, System.Globalization.DateTimeStyles.None, out var _fromDate))
                {
                    tempFromData = _fromDate;
                    //_fromDate = _fromDate.AddHours(Utility.GetAppsettingsValue("AMPECOConfiguration", "TimeDiffAdd").ToAnyType<int>());
                    fromDate = _fromDate.ToString("yyyy-MM-dd HH:mm:ss.fff");
                }

                if (DateTime.TryParseExact(toDate, validFormats, null, System.Globalization.DateTimeStyles.None, out var _toDate))
                {
                    tempToDate = _toDate;
                    _toDate = _toDate.Date.AddDays(1).AddMilliseconds(-1);
                    //_toDate = _toDate.AddHours(Utility.GetAppsettingsValue("AMPECOConfiguration", "TimeDiffAdd").ToAnyType<int>());
                    toDate = _toDate.ToString("yyyy-MM-dd HH:mm:ss.fff");
                }

                var startTime = DateTime.Now;


                var httpClient = new HttpClient();
                var apiService = new ApiService(httpClient);

                string initialUrl = "https://shabikuae.eu.charge.ampeco.tech/public-api/resources/partners/v2.0";

                List<AMPECOPartner> allReportpartners = await apiService.GetAllPaginatedDataAsync<AMPECOPartner>(initialUrl, _token);
                var mappedData = _mapper.Map<List<AMPECOPartner>, List<CATECEV.Models.Entity.AMPECO.Resources.AmbPartner.Partner>>(allReportpartners);

                if (mappedData.IsNotNullOrEmpty())
                {
                    var allPartnerGroupEntity = await _appContext.Partner.Where(x => x.IsActive).ToListAsync();
                    var allPartnerEntityDictionary = allPartnerGroupEntity.ToDictionary(x => x.AMPECOId);

                    var filterdPartnerEntity = mappedData.Where(x => !allPartnerEntityDictionary.ContainsKey(x.AMPECOId));

                    if (filterdPartnerEntity.IsNotNullOrEmpty())
                    {
                        await _appContext.Partner.AddRangeAsync(filterdPartnerEntity.DistinctBy(x => x.AMPECOId));
                        await _appContext.SaveChangesAsync();
                    }
                }
                var dateDiff = (DateTime.Now - startTime).TotalSeconds;
                return Ok();
            }
            catch (Exception ex)
            {
                FileLogger.WriteLog($"SessionsRawData \nMessage: {ex.Message}\n InnerException: {ex.InnerException}");
                return StatusCode(500, false);
            }
        }

        [HttpGet("AmbecoPartnerExpenseData")]
        public async Task<IActionResult> AmbecoPartnerExpenseData(int partnerId)
        {
            try
            {
                var httpClient = new HttpClient();
                var apiService = new ApiService(httpClient);

                var selectedPartner = _appContext.Partner.FirstOrDefault(x => x.Id == partnerId);
                if (selectedPartner != null && selectedPartner.LastCalculationBalanceDate == DateTime.MinValue)
                {
                    selectedPartner.LastCalculationBalanceDate = DateTime.Now;
                }
                string initialUrl = $"https://shabikuae.eu.charge.ampeco.tech/public-api/resources/partner-expenses/v1.1?filter[partnerId]={selectedPartner.AMPECOId}&filter[dateBefore]={DateTime.Now}&filter[dateAfter]={selectedPartner.LastCalculationBalanceDate}";

                List<AMPECOPartnerExpense> allReports = await apiService.GetAllPaginatedDataAsync<AMPECOPartnerExpense>(initialUrl, _token);

                decimal totalWithoutTax = allReports.Sum(r => r.TotalAmount?.WithoutTax ?? 0);

                selectedPartner.LastCalculationBalanceDate = DateTime.Now;
                selectedPartner.BalanceAmount -= totalWithoutTax;
                _appContext.Partner.Update(selectedPartner);
                await _appContext.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                FileLogger.WriteLog($"SessionsRawData \nMessage: {ex.Message}\n InnerException: {ex.InnerException}");
                return StatusCode(500, false);
            }
        }

        private async Task<IEnumerable<T>> GetResourcesDataEntity<T, TAMPECO, TService>(TService service) where TService : IAMPECOResource<TAMPECO>
        {
            var pageNumber = 1;

            var resourceData = await service.GetResourceDataList(pageNumber);
            if (resourceData is null || !resourceData.Data.IsNotNullOrEmpty() || resourceData.TotalRecords <= 0)
                return new List<T>();

            var totalPages = resourceData.TotalPages;
            var batchSize = 30;
            var resourceEntity = new ConcurrentBag<T>();


            var dateTime = DateTime.Now;

            for (var i = 1; i <= totalPages; i += batchSize)
            {
                var tasks = Enumerable.Range(i, Math.Min(batchSize, totalPages - i + 1))
                    .Select(async page =>
                    {
                        var pageData = await service.GetResourceDataList(page);
                        if (pageData != null && pageData.Data.IsNotNullOrEmpty())
                        {
                            var mappedData = _mapper.Map<IEnumerable<TAMPECO>, IEnumerable<T>>(pageData.Data);
                            foreach (var item in mappedData)
                            {
                                resourceEntity.Add(item);
                            }
                        }
                    });

                await Task.WhenAll(tasks);
            }

            return resourceEntity;
        }
    }
}
