using AutoMapper;
using CATECEV.API.Helper.IService;
using CATECEV.API.Models.Reports.Sessions;
using CATECEV.CORE.Extensions;
using CATECEV.CORE.Logger;
using CATECEV.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CATECEV.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly AppDBContext _appContext;
        private readonly IMapper _mapper;
        private readonly IAMPECOResource<Models.AMPECO.resource.Location.Location> _locationResource;

        private static Dictionary<int, Models.AMPECO.resource.Location.Location> locationKeyValuePairs = new Dictionary<int, Models.AMPECO.resource.Location.Location>();

        public ReportsController(AppDBContext appContext, IMapper mapper, IAMPECOResource<Models.AMPECO.resource.Location.Location> locationResource)
        {
            _appContext = appContext;
            _mapper = mapper;
            _locationResource = locationResource;
        }

        [HttpGet("SessionsRawData")]
        public async Task<IActionResult> SessionsRawData()
        {
            try
            {
                var result = await (from a in _appContext.ChargingSession
                                    join u in _appContext.User on a.AMPECOUserId equals u.AMPECOId
                                    join e in _appContext.Evse on a.AMPECOEvseId equals e.AMPECOId
                                    join cp in _appContext.ChargePoint on a.AMPECOChargePointId equals cp.AMPECOId
                                    join au in _appContext.Authorization on a.AMPECOAuthorizationId equals au.AMPECOId
                                    where a.Status == "finished" && a.StoppedAt != null && a.StartedAt != null
                                    orderby a.AMPECOId descending
                                    select new ChargingSessionViewModel
                                    {
                                        ID = a.AMPECOId,
                                        User = (u.FirstName ?? "") + " " + (u.LastName ?? ""),
                                        UserID = a.AMPECOUserId,
                                        Email = u.Email,
                                        UserGroups = u.UserGroups,
                                        StartDate = a.StartedAt.HasValue ? a.StartedAt.Value.Date : (DateTime?)null,
                                        StartTime = a.StartedAt.HasValue ? a.StartedAt.Value.TimeOfDay : (TimeSpan?)null,
                                        EndDate = a.StoppedAt.HasValue ? a.StoppedAt.Value.Date : (DateTime?)null,
                                        EndTime = a.StoppedAt.HasValue ? a.StoppedAt.Value.TimeOfDay : (TimeSpan?)null,
                                        DurationInSeconds = a.StartedAt.HasValue && a.StoppedAt.HasValue
                                            ? (long)(a.StoppedAt.Value - a.StartedAt.Value).TotalSeconds
                                            : (long?)null,
                                        EnergySupplied = a.Energy,
                                        SessionStatus = a.Status,
                                        PaymentStatus = a.PaymentStatus,
                                        TotalAmount = a.Amount,
                                        TotalAmountWithoutTax = a.TotalAmountWithoutTax,
                                        Currency = a.Currency,
                                        EVSEIdentifier = a.AMPECOEvseId,
                                        EVCSNumber = a.EvsePhysicalReference ?? string.Empty,
                                        StopReason = a.Reason ?? string.Empty,
                                        EVSEType = e.CurrentType,
                                        ChargePointName = cp.Name ?? string.Empty,
                                        LocationId = cp.LocationId,
                                        AuthorizationMethod = au.Method,
                                        IDTag = a.IdTag,
                                        IDTagLabel = a.IdTagLabel,
                                        Roaming = au.Roaming,
                                        RoamingCPO = e.RoamingStatus ?? string.Empty
                                    }).ToListAsync();

                if (result.IsNotNullOrEmpty())
                {
                    foreach (var item in result)
                    {
                        if (locationKeyValuePairs.TryGetValue(item.LocationId, out var location))
                        {
                            item.Location = location.Address.Where(x=>x.Locale == "en").FirstOrDefault()?.Translation;
                            item.City = location.City;
                            item.PostCode = location.PostCode;
                            item.Country = location.Country;
                        }
                        else
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
                    }
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                FileLogger.WriteLog($"Message: {ex.Message}\n InnerException: {ex.InnerException}");
                return StatusCode(500, false);
            }
        }
    }
}
