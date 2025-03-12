using AutoMapper;
using CATECEV.API.EntityHelper.IService;
using CATECEV.API.Models.Reports.Sessions;
using CATECEV.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CATECEV.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IUser _entityUserService;
        private readonly ITax _entityTaxService;
        private readonly IEvse _entityEvseService;
        private readonly IConnector _entityConnectorService;
        private readonly IChargePoint _entityChargePointService;
        private readonly AppDBContext _appContext;
        private readonly IMapper _mapper;


        public ReportsController(IUser entityUserService, ITax entityTaxService, IEvse entityEvseService, IConnector entityConnectorService, IChargePoint entityChargePointService, AppDBContext appContext, IMapper mapper)
        {
            _entityUserService = entityUserService;
            _entityTaxService = entityTaxService;
            _entityEvseService = entityEvseService;
            _entityConnectorService = entityConnectorService;
            _entityChargePointService = entityChargePointService;
            _appContext = appContext;
            _mapper = mapper;
        }

        [HttpGet("SessionsRawDetailedData")]
        public async Task<IActionResult> SessionsRawDetailedData()
        {
            var result = await (from a in _appContext.ChargingSession
                                join e in _appContext.Evse on a.AMPECOEvseId equals e.AMPECOId
                                join cp in _appContext.ChargePoint on a.AMPECOChargePointId equals cp.AMPECOId
                                orderby a.StartedAt
                                select new ChargingSessionViewModel
                                {
                                    ID = a.AMPECOId,
                                    EVCSNumber = a.EvsePhysicalReference ?? string.Empty,
                                    ChargePointName = cp.Name ?? string.Empty,
                                    DateOfChargingEvent = a.StartedAt.HasValue ? a.StartedAt.Value.Date : (DateTime?)null,
                                    StartTimeOfCharging = a.StartedAt.HasValue ? a.StartedAt.Value.TimeOfDay : (TimeSpan?)null,
                                    StoppedDateOfChargingEvent = a.StoppedAt.HasValue ? a.StoppedAt.Value.Date : (DateTime?)null,
                                    EndTimeOfCharging = a.StoppedAt.HasValue ? a.StoppedAt.Value.TimeOfDay : (TimeSpan?)null,
                                    TotalChargingDurationIncludingIdling = (a.StartedAt.HasValue && a.StoppedAt.HasValue)
                                ? EF.Functions.DateDiffMinute(a.StartedAt.Value, a.StoppedAt.Value)
                                : (int?)null,
                                    TotalEnergySuppliedByEVCSExcludingIdling = a.Energy,
                                    ChargingServicesRetailPrice = a.Amount,
                                    StopReason = a.Reason ?? string.Empty,
                                    RoamingCPO = e.RoamingStatus ?? string.Empty
                                })
                        .ToListAsync();

            return Ok(result);
        }

        [HttpGet("SessionsRawRegularData")]
        public async Task<IActionResult> SessionsRawRegularData()
        {
            return Ok();
        }
    }
}
