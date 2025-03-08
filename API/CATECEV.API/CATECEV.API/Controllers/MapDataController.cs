using AutoMapper;
using CATECEV.API.Helper.IService;
using CATECEV.API.EntityHelper.IService;
using CATECEV.CORE.Extensions;
using CATECEV.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CATECEV.Models.Entity.AMPECO.Resources.Tax;
using CATECEV.Models.Entity.AMPECO.Resources.ChargePoint;

namespace CATECEV.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MapDataController : ControllerBase
    {
        private readonly IAMPECOUser _user;
        private readonly IAMPECOChargePoints _aMPECOChargePoints;
        private readonly IAMPECOSessions _aMPECOSessions;
        private readonly IAMPECOTaxes _aMPECOTaxes;
        private readonly IUser _entityUserService;
        private readonly ITax _entityTaxService;
        private readonly IEvse _entityEvseService;
        private readonly IConnector _entityConnectorService;
        private readonly IChargePoint _entityChargePointService;
        private readonly AppDBContext _appContext;
        private readonly IMapper _mapper;
        public MapDataController(IAMPECOUser user, AppDBContext appContext, IMapper mapper, IUser entityUserService, IAMPECOChargePoints aMPECOChargePoints, IAMPECOSessions aMPECOSessions, IAMPECOTaxes aMPECOTaxes, ITax entityTaxService, IEvse entityEvseService, IConnector entityConnectorService, IChargePoint entityChargePointService)
        {
            _user = user;
            _appContext = appContext;
            _mapper = mapper;
            _entityUserService = entityUserService;
            _aMPECOChargePoints = aMPECOChargePoints;
            _aMPECOSessions = aMPECOSessions;
            _aMPECOTaxes = aMPECOTaxes;
            _entityTaxService = entityTaxService;
            _entityEvseService = entityEvseService;
            _entityConnectorService = entityConnectorService;
            _entityChargePointService = entityChargePointService;
        }

        [HttpGet("GetData")]
        public async Task<IActionResult> GetData()
        {
            var pageNumber = 1;

            /*
            #region User
            var userData = await _user.GetUsers(pageNumber);

            if (userData.IsNotNullOrEmpty() && userData.Data.IsNotNullOrEmpty() && userData.TotalRecords > 0)
            {
                var userEntity = new List<CATECEV.Models.Entity.AMPECO.Resources.User.User>();
                var mappedUserData = _mapper.Map<IEnumerable<Models.AMPECO.resource.users.User>, IEnumerable<CATECEV.Models.Entity.AMPECO.Resources.User.User>>(userData.Data);
                userEntity.AddRange(mappedUserData.ToList());

                while (userData.TotalPages > pageNumber)
                {
                    var nextUserData = await _user.GetUsers(++pageNumber);
                    if (nextUserData.IsNotNullOrEmpty() && nextUserData.Data.IsNotNullOrEmpty())
                    {
                        var mappedNextUserData = _mapper.Map<IEnumerable<Models.AMPECO.resource.users.User>, IEnumerable<CATECEV.Models.Entity.AMPECO.Resources.User.User>>(nextUserData.Data);
                        userEntity.AddRange(mappedNextUserData.ToList());
                    }
                }

                if (userEntity.IsNotNullOrEmpty())
                {
                    foreach (var item in userEntity.ToList())
                    {
                        var checkUser = await _appContext.User.AnyAsync(x => x.AMPECOId == item.AMPECOId);
                        if (checkUser)
                        {
                            userEntity.Remove(item);
                        }
                    }

                    if (userEntity.IsNotNullOrEmpty())
                    {
                        await _appContext.User.AddRangeAsync(userEntity);
                        await _appContext.SaveChangesAsync();
                    }
                }
            }
            #endregion

            #region ChargePoints
            pageNumber = 1;

            var chargePointsData = await _aMPECOChargePoints.GetChargePoints(pageNumber);
            if (chargePointsData.IsNotNullOrEmpty() && chargePointsData.Data.IsNotNullOrEmpty() && chargePointsData.TotalRecords > 0)
            {
                var chargePointEntity = new List<CATECEV.Models.Entity.AMPECO.Resources.ChargePoint.ChargePointEntity>();
                var mappedChargePointData = _mapper.Map<IEnumerable<Models.AMPECO.resource.ChargePoint.ChargePoint>, IEnumerable<CATECEV.Models.Entity.AMPECO.Resources.ChargePoint.ChargePointEntity>>(chargePointsData.Data);
                chargePointEntity.AddRange(mappedChargePointData.ToList());

                while (chargePointsData.TotalPages > pageNumber)
                {
                    var nextChargePointsData = await _aMPECOChargePoints.GetChargePoints(++pageNumber);
                    if (nextChargePointsData.IsNotNullOrEmpty() && nextChargePointsData.Data.IsNotNullOrEmpty())
                    {
                        var mappedChargePointsData = _mapper.Map<IEnumerable<Models.AMPECO.resource.ChargePoint.ChargePoint>, IEnumerable<CATECEV.Models.Entity.AMPECO.Resources.ChargePoint.ChargePointEntity>>(nextChargePointsData.Data);
                        chargePointEntity.AddRange(mappedChargePointsData.ToList());
                    }
                }

                if (chargePointEntity.IsNotNullOrEmpty())
                {
                    var entityOwnerUserDataForIds = await _entityUserService.GetUsersByAMPECOIds(chargePointEntity.Where(x => x.OwnerUserId.HasValue).Select(x => x.OwnerUserId.Value));
                    var entityOwnerPartnerContractDataForIds = await _entityUserService.GetUsersByAMPECOIds(chargePointEntity.Where(x => x.OwnerPartnerContractId.HasValue).Select(x => x.OwnerPartnerContractId.Value));
                    var entityOwnerPartnerDataForIds = await _entityUserService.GetUsersByAMPECOIds(chargePointEntity.Where(x => x.OwnerPartnerId.HasValue).Select(x => x.OwnerPartnerId.Value));
                    var entityUserDataDictionary = entityOwnerUserDataForIds.ToDictionary(x => x.AMPECOId, x => x.Id);
                    var entityOwnerPartnerContractDataDictionary = entityOwnerPartnerContractDataForIds.ToDictionary(x => x.AMPECOId, x => x.Id);
                    var entityOwnerPartnerDataDictionary = entityOwnerPartnerDataForIds.ToDictionary(x => x.AMPECOId, x => x.Id);

                    foreach (var item in chargePointEntity.ToList())
                    {
                        var checkChargePoint = await _appContext.ChargePoint.AnyAsync(x => x.AMPECOId == item.AMPECOId);
                        if (checkChargePoint)
                        {
                            chargePointEntity.Remove(item);
                        }
                        else
                        {
                            if (item.OwnerUserId.HasValue && entityUserDataDictionary.TryGetValue(item.OwnerUserId.Value, out var entityUserId))
                            {
                                item.OwnerUserId = entityUserId;
                            }

                            if (item.OwnerPartnerContractId.HasValue && entityOwnerPartnerContractDataDictionary.TryGetValue(item.OwnerPartnerContractId.Value, out var entityOwnerPartnerContractId))
                            {
                                item.OwnerPartnerContractId = entityOwnerPartnerContractId;
                            }

                            if (item.OwnerPartnerId.HasValue && entityOwnerPartnerDataDictionary.TryGetValue(item.OwnerPartnerId.Value, out var entityOwnerPartnerId))
                            {
                                item.OwnerPartnerId = entityOwnerPartnerId;
                            }
                        }
                    }

                    if (chargePointEntity.IsNotNullOrEmpty())
                    {
                        await _appContext.ChargePoint.AddRangeAsync(chargePointEntity);
                        await _appContext.SaveChangesAsync();
                    }
                }
            }
            #endregion

            #region Tax
            pageNumber = 1;

            var taxesData = await _aMPECOTaxes.GetTaxes(pageNumber);
            if (taxesData.IsNotNullOrEmpty() && taxesData.Data.IsNotNullOrEmpty() && taxesData.TotalRecords > 0)
            {
                var taxesEntity = new List<CATECEV.Models.Entity.AMPECO.Resources.Tax.TaxEntity>();
                var mappedTaxData = _mapper.Map<IEnumerable<Models.AMPECO.resource.Tax.Tax>, IEnumerable<CATECEV.Models.Entity.AMPECO.Resources.Tax.TaxEntity>>(taxesData.Data);
                taxesEntity.AddRange(mappedTaxData.ToList());

                while (taxesData.TotalPages > pageNumber)
                {
                    var nextTaxesData = await _aMPECOTaxes.GetTaxes(++pageNumber);
                    if (nextTaxesData.IsNotNullOrEmpty() && nextTaxesData.Data.IsNotNullOrEmpty())
                    {
                        var mappedNextTaxData = _mapper.Map<IEnumerable<Models.AMPECO.resource.Tax.Tax>, IEnumerable<CATECEV.Models.Entity.AMPECO.Resources.Tax.TaxEntity>>(nextTaxesData.Data);
                        taxesEntity.AddRange(mappedNextTaxData.ToList());
                    }
                }

                if (taxesEntity.IsNotNullOrEmpty())
                {
                    await _appContext.Tax.AddRangeAsync(taxesEntity);
                    await _appContext.SaveChangesAsync();
                }
            }
            #endregion
            */

            #region ChargingSession
            pageNumber = 1;

            var chargingSessionData = await _aMPECOSessions.GetChargingSession(pageNumber);
            if (chargingSessionData.IsNotNullOrEmpty() && chargingSessionData.Data.IsNotNullOrEmpty() && chargingSessionData.TotalRecords > 0)
            {
                var chargingSessionEntity = new List<CATECEV.Models.Entity.AMPECO.Resources.Session.ChargingSessionEntity>();
                var mappedChargingSessionData = _mapper.Map<IEnumerable<Models.AMPECO.resource.Session.ChargingSession>, IEnumerable<CATECEV.Models.Entity.AMPECO.Resources.Session.ChargingSessionEntity>>(chargingSessionData.Data);
                chargingSessionEntity.AddRange(mappedChargingSessionData.ToList());
                
                while (chargingSessionData.TotalPages > pageNumber)
                {
                    var nextChargingSessionData = await _aMPECOSessions.GetChargingSession(++pageNumber);
                    if (nextChargingSessionData.IsNotNullOrEmpty() && nextChargingSessionData.Data.IsNotNullOrEmpty())
                    {
                        var mappedNextChargingSessionData = _mapper.Map<IEnumerable<Models.AMPECO.resource.Session.ChargingSession>, IEnumerable<CATECEV.Models.Entity.AMPECO.Resources.Session.ChargingSessionEntity>>(nextChargingSessionData.Data);
                        chargingSessionEntity.AddRange(mappedNextChargingSessionData.ToList());
                    }
                }

                if (chargingSessionEntity.IsNotNullOrEmpty())
                {
                    var entityUserDataForIds = await _entityUserService.GetUsersByAMPECOIds(chargingSessionEntity.Select(x => x.UserId));
                    var entityUserDataDictionary = entityUserDataForIds.ToDictionary(x => x.AMPECOId, x => x.Id);

                    var entityChargePointDataForIds = await _entityChargePointService.GetChargePointByAMPECOIds(chargingSessionEntity.Select(x => x.ChargePointId));
                    var entityChargePointDataDictionary = entityChargePointDataForIds.ToDictionary(x => x.AMPECOId, x => x.Id);
                    
                    var entityEvseDataForIds = await _entityEvseService.GetEvseByAMPECOIds(chargingSessionEntity.Select(x => x.ChargePointId));
                    var entityEvseDataDictionary = entityEvseDataForIds.ToDictionary(x => x.AMPECOId, x => x.Id);
                     
                    var entityTaxDataForIds = await _entityTaxService.GetTaxesByAMPECOIds(chargingSessionEntity.Select(x => x.TaxId));
                    var entityTaxDataDictionary = entityTaxDataForIds.ToDictionary(x => x.AMPECOId, x => x.Id);

                    var entityConnectorDataForIds = await _entityConnectorService.GetConnectorByAMPECOIds(chargingSessionEntity.Where(x => x.ConnectorId.HasValue).Select(x => x.ConnectorId.Value));
                    var entityConnectorDataDictionary = entityConnectorDataForIds.ToDictionary(x => x.AMPECOId, x => x.Id);


                    foreach (var item in chargingSessionEntity.ToList())
                    {
                        var checkChargingSession = await _appContext.ChargingSession.AnyAsync(x => x.AMPECOId == item.AMPECOId);
                        if (checkChargingSession)
                        {
                            chargingSessionEntity.Remove(item);
                        }
                        else
                        {
                            if (entityUserDataDictionary.TryGetValue(item.UserId, out var entityUserId))
                            {
                                item.UserId = entityUserId;
                            }

                            if (entityUserDataDictionary.TryGetValue(item.ChargePointId, out var entityChargePointId))
                            {
                                item.ChargePointId = entityChargePointId;
                            }

                            if (entityEvseDataDictionary.TryGetValue(item.EvseId, out var entityEvseId))
                            {
                                item.EvseId = entityEvseId;
                            }

                            if (item.ConnectorId.HasValue && entityConnectorDataDictionary.TryGetValue(item.ConnectorId.Value, out var entityConnectorId))
                            {
                                item.ConnectorId = entityConnectorId;
                            }

                            if (entityTaxDataDictionary.TryGetValue(item.TaxId, out var entityTaxId))
                            {
                                item.TaxId = entityTaxId;
                            }
                        }
                    }
                }
            }
            #endregion

            return Ok(true);
        }
    }
}
