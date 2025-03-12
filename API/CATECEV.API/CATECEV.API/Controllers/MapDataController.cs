using AutoMapper;
using CATECEV.API.Helper.IService;
using CATECEV.API.EntityHelper.IService;
using CATECEV.CORE.Extensions;
using CATECEV.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CATECEV.Models.Entity.AMPECO.Resources.Session;
using Microsoft.Extensions.Logging;
using CATECEV.CORE.Logger;
using System.Collections.Generic;
using CATECEV.Models.Entity.AMPECO.Resources.ChargePoint;
using System.Linq;

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
        private readonly IAMPECOEvses _aMPECOEvses;
        private readonly IAMPECOResource<Models.AMPECO.resource.users.UserGroup> _userGroupResource;
        private readonly IUser _entityUserService;
        private readonly ITax _entityTaxService;
        private readonly IEvse _entityEvseService;
        private readonly IConnector _entityConnectorService;
        private readonly IChargePoint _entityChargePointService;
        private readonly AppDBContext _appContext;
        private readonly IMapper _mapper;
        public MapDataController(IAMPECOUser user, AppDBContext appContext, IMapper mapper, IUser entityUserService, IAMPECOChargePoints aMPECOChargePoints, IAMPECOSessions aMPECOSessions, IAMPECOTaxes aMPECOTaxes, ITax entityTaxService, IEvse entityEvseService, IConnector entityConnectorService, IChargePoint entityChargePointService, IAMPECOEvses aMPECOEvses, IAMPECOResource<Models.AMPECO.resource.users.UserGroup> userGroupResource)
        {
            _user = user;
            _appContext = appContext;
            _mapper = mapper;
            _entityUserService = entityUserService;
            _aMPECOChargePoints = aMPECOChargePoints;
            _aMPECOSessions = aMPECOSessions;
            _aMPECOTaxes = aMPECOTaxes;
            _aMPECOEvses = aMPECOEvses;
            _entityTaxService = entityTaxService;
            _entityEvseService = entityEvseService;
            _entityConnectorService = entityConnectorService;
            _entityChargePointService = entityChargePointService;
            _userGroupResource = userGroupResource;
        }

        [HttpGet("SessionTrigger")]
        public async Task<IActionResult> SessionTrigger(string bodyString)
        {
            try
            {
                FileLogger.WriteLog($"SessionTrigger LOG {DateTime.Now} {bodyString}");

                return Ok(true);
            }
            catch (Exception ex)
            {
                FileLogger.WriteLog($"Message: {ex.Message}\n InnerException: {ex.InnerException}");
                return StatusCode(500, false);
            }
        }


        [HttpGet("GetData")]
        public async Task<IActionResult> GetData()
        {
            var pageNumber = 1;

            try
            {
                #region UserGroup
                var userGroupData = await _userGroupResource.GetResourceData(pageNumber);

                if (userGroupData.IsNotNullOrEmpty() && userGroupData.Data.IsNotNullOrEmpty() && userGroupData.TotalRecords > 0)
                {
                    var userGroupEntity = new List<CATECEV.Models.Entity.AMPECO.Resources.User.UserGroup>();
                    var mappedUserGroupData = _mapper.Map<IEnumerable<Models.AMPECO.resource.users.UserGroup>, IEnumerable<CATECEV.Models.Entity.AMPECO.Resources.User.UserGroup>>(userGroupData.Data);
                    userGroupEntity.AddRange(mappedUserGroupData.ToList());

                    while (userGroupData.TotalPages > pageNumber)
                    {
                        var nextUserGroupData = await _userGroupResource.GetResourceData(++pageNumber);
                        if (nextUserGroupData.IsNotNullOrEmpty() && nextUserGroupData.Data.IsNotNullOrEmpty())
                        {
                            var mappedGroupNextUserData = _mapper.Map<IEnumerable<Models.AMPECO.resource.users.UserGroup>, IEnumerable<CATECEV.Models.Entity.AMPECO.Resources.User.UserGroup>>(nextUserGroupData.Data);
                            userGroupEntity.AddRange(mappedGroupNextUserData.ToList());
                        }
                    }

                    if (userGroupEntity.IsNotNullOrEmpty())
                    {
                        foreach (var item in userGroupEntity.ToList())
                        {
                            var checkGroup = await _appContext.UserGroup.AnyAsync(x => x.AMPECOId == item.AMPECOId);
                            if (checkGroup)
                            {
                                userGroupEntity.Remove(item);
                            }
                        }

                        if (userGroupEntity.IsNotNullOrEmpty())
                        {
                            await _appContext.UserGroup.AddRangeAsync(userGroupEntity.DistinctBy(x => x.AMPECOId));
                            await _appContext.SaveChangesAsync();
                        }
                    }
                }
                #endregion

                #region User
                pageNumber = 1;

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
                        var groupIds = userEntity.Where(x => x.UserGroupIds.IsNotNullOrEmpty()).SelectMany(x => x.UserGroupIds);

                        var entityGroupData = await _appContext.UserGroup.Where(u => groupIds.Contains(u.AMPECOId)).ToListAsync();
                        var entityGroupDataDictionary = entityGroupData.ToDictionary(x => x.AMPECOId);

                        foreach (var item in userEntity.ToList())
                        {
                            var checkUser = await _appContext.User.AnyAsync(x => x.AMPECOId == item.AMPECOId);
                            if (checkUser)
                            {
                                userEntity.Remove(item);
                            }
                            else if (item.UserGroupIds.IsNotNullOrEmpty())
                            {
                                item.UserGroups = string.Join(",",
                                    item.UserGroupIds
                                        .Select(id => entityGroupDataDictionary.TryGetValue(id, out var userGroup) ? userGroup.Name : null)
                                        .Where(name => name != null)
                                );
                            }
                        }

                        if (userEntity.IsNotNullOrEmpty())
                        {
                            await _appContext.User.AddRangeAsync(userEntity.DistinctBy(x => x.AMPECOId));
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
                            await _appContext.ChargePoint.AddRangeAsync(chargePointEntity.DistinctBy(x => x.AMPECOId));
                            await _appContext.SaveChangesAsync();
                        }
                    }
                }
                #endregion

                #region Evse
                pageNumber = 1;

                var evseData = await _aMPECOEvses.GetEvse(pageNumber);
                if (evseData.IsNotNullOrEmpty() && evseData.Data.IsNotNullOrEmpty() && evseData.TotalRecords > 0)
                {
                    var evseEntity = new List<CATECEV.Models.Entity.AMPECO.Resources.ChargePoint.EvseEntity>();
                    var mappedEvseData = _mapper.Map<IEnumerable<Models.AMPECO.resource.ChargePoint.Evse>, IEnumerable<CATECEV.Models.Entity.AMPECO.Resources.ChargePoint.EvseEntity>>(evseData.Data);
                    evseEntity.AddRange(mappedEvseData.ToList());

                    while (evseData.TotalPages > pageNumber)
                    {
                        var nextEvseData = await _aMPECOEvses.GetEvse(++pageNumber);
                        if (nextEvseData.IsNotNullOrEmpty() && nextEvseData.Data.IsNotNullOrEmpty())
                        {
                            var mappedNextEvseData = _mapper.Map<IEnumerable<Models.AMPECO.resource.ChargePoint.Evse>, IEnumerable<CATECEV.Models.Entity.AMPECO.Resources.ChargePoint.EvseEntity>>(nextEvseData.Data);
                            evseEntity.AddRange(mappedNextEvseData.ToList());
                        }
                    }

                    if (evseEntity.IsNotNullOrEmpty())
                    {
                        var entityChargePointDataForIds = await _entityChargePointService.GetChargePointByAMPECOIds(evseEntity.Select(x => x.AMPECOChargePointId));
                        var entityChargePointDataDictionary = entityChargePointDataForIds.ToDictionary(x => x.AMPECOId);

                        foreach (var item in evseEntity.ToList())
                        {
                            var checkEvsePoint = await _appContext.Evse.AnyAsync(x => x.AMPECOId == item.AMPECOId);
                            if (checkEvsePoint)
                            {
                                evseEntity.Remove(item);
                            }
                            else
                            {
                                if (entityChargePointDataDictionary.TryGetValue(item.AMPECOChargePointId, out var entityChargePoint))
                                {
                                    item.ChargePointId = entityChargePoint.Id;
                                }
                            }
                        }

                        if (evseEntity.IsNotNullOrEmpty())
                        {
                            await _appContext.Evse.AddRangeAsync(evseEntity.DistinctBy(x => x.AMPECOId));
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
                        foreach (var item in taxesEntity.ToList())
                        {
                            var checkTax = await _appContext.Tax.AnyAsync(x => x.AMPECOId == item.AMPECOId);
                            if (checkTax)
                            {
                                taxesEntity.Remove(item);
                            }
                        }

                        if (taxesEntity.IsNotNullOrEmpty())
                        {
                            await _appContext.Tax.AddRangeAsync(taxesEntity.DistinctBy(x => x.AMPECOId));
                            await _appContext.SaveChangesAsync();
                        }
                    }
                }
                #endregion

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
                        var entityUserDataForIds = await _entityUserService.GetUsersByAMPECOIds(chargingSessionEntity.Select(x => x.AMPECOUserId));
                        var entityUserDataDictionary = entityUserDataForIds.ToDictionary(x => x.AMPECOId);

                        var entityChargePointDataForIds = await _entityChargePointService.GetChargePointByAMPECOIds(chargingSessionEntity.Select(x => x.AMPECOChargePointId));
                        var entityChargePointDataDictionary = entityChargePointDataForIds.ToDictionary(x => x.AMPECOId);

                        var entityEvseDataForIds = await _entityEvseService.GetEvseByAMPECOIds(chargingSessionEntity.Select(x => x.AMPECOChargePointId));
                        var entityEvseDataDictionary = entityEvseDataForIds.ToDictionary(x => x.AMPECOId);

                        var entityTaxDataForIds = await _entityTaxService.GetTaxesByAMPECOIds(chargingSessionEntity.Select(x => x.AMPECOTaxId));
                        var entityTaxDataDictionary = entityTaxDataForIds.ToDictionary(x => x.AMPECOId);

                        var entityConnectorDataForIds = await _entityConnectorService.GetConnectorByAMPECOIds(chargingSessionEntity.Where(x => x.AMPECOConnectorId.HasValue).Select(x => x.AMPECOConnectorId.Value));
                        var entityConnectorDataDictionary = entityConnectorDataForIds.ToDictionary(x => x.AMPECOId);

                        var allChargingSession = await _appContext.ChargingSession.Where(x => x.IsActive).ToListAsync();
                        var allChargingSessionDictionary = allChargingSession.ToDictionary(x => x.AMPECOId);
                        foreach (var item in chargingSessionEntity.ToList())
                        {
                            if (allChargingSessionDictionary.TryGetValue(item.AMPECOId, out var tempDataNoUse))
                            {
                                chargingSessionEntity.Remove(item);
                            }
                            else
                            {
                                if (entityUserDataDictionary.TryGetValue(item.AMPECOUserId, out var entityUser))
                                {
                                    item.UserId = entityUser.Id;
                                }

                                if (entityChargePointDataDictionary.TryGetValue(item.AMPECOChargePointId, out var entityChargePoint))
                                {
                                    item.ChargePointId = entityChargePoint.Id;
                                }

                                if (entityEvseDataDictionary.TryGetValue(item.AMPECOEvseId, out var entityEvse))
                                {
                                    item.EvseId = entityEvse.Id;
                                }

                                if (item.AMPECOConnectorId.HasValue && entityConnectorDataDictionary.TryGetValue(item.AMPECOConnectorId.Value, out var entityConnector))
                                {
                                    item.ConnectorId = entityConnector.Id;
                                }

                                if (entityTaxDataDictionary.TryGetValue(item.AMPECOTaxId, out var entityTax))
                                {
                                    item.TaxId = entityTax.Id;
                                }
                            }
                        }

                        if (chargingSessionEntity.IsNotNullOrEmpty())
                        {
                            await _appContext.ChargingSession.AddRangeAsync(chargingSessionEntity.DistinctBy(x => x.AMPECOId));
                            await _appContext.SaveChangesAsync();
                        }
                    }
                }
                #endregion

                return Ok(true);
            }
            catch (Exception ex)
            {
                return Ok($"Message: {ex.Message}\n InnerException: {ex.InnerException}");
            }
        }
    }
}
