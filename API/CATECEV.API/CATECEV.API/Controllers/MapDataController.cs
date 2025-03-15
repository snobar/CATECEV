using AutoMapper;
using CATECEV.API.EntityHelper.IService;
using CATECEV.API.Helper.IService;
using CATECEV.CORE.Extensions;
using CATECEV.CORE.Logger;
using CATECEV.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;

namespace CATECEV.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MapDataController : ControllerBase
    {
        private readonly IAMPECOResource<Models.AMPECO.resource.users.UserGroup> _userGroupResource;
        private readonly IAMPECOResource<Models.AMPECO.resource.Session.ChargingSession> _aMPECOSessions;
        private readonly IAMPECOResource<Models.AMPECO.resource.users.User> _user;
        private readonly IAMPECOResource<Models.AMPECO.resource.ChargePoint.ChargePoint> _aMPECOChargePoints;
        private readonly IAMPECOResource<Models.AMPECO.resource.ChargePoint.Evse> _aMPECOEvses;
        private readonly IAMPECOResource<Models.AMPECO.resource.Tax.Tax> _aMPECOTaxes;
        private readonly IAMPECOResource<Models.AMPECO.resource.Authorization.Authorization> _aMPECOAuthorization;
        private readonly IUser _entityUserService;
        private readonly ITax _entityTaxService;
        private readonly IEvse _entityEvseService;
        private readonly IConnector _entityConnectorService;
        private readonly IChargePoint _entityChargePointService;
        private readonly AppDBContext _appContext;
        private readonly IMapper _mapper;
        public MapDataController(IAMPECOResource<Models.AMPECO.resource.users.User> user, AppDBContext appContext, IMapper mapper, IUser entityUserService, IAMPECOResource<Models.AMPECO.resource.ChargePoint.ChargePoint> aMPECOChargePoints, IAMPECOResource<Models.AMPECO.resource.Session.ChargingSession> aMPECOSessions, IAMPECOResource<Models.AMPECO.resource.Tax.Tax> aMPECOTaxes, ITax entityTaxService, IEvse entityEvseService, IConnector entityConnectorService, IChargePoint entityChargePointService, IAMPECOResource<Models.AMPECO.resource.ChargePoint.Evse> aMPECOEvses, IAMPECOResource<Models.AMPECO.resource.users.UserGroup> userGroupResource, IAMPECOResource<Models.AMPECO.resource.Authorization.Authorization> aMPECOAuthorization)
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
            _aMPECOAuthorization = aMPECOAuthorization;
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
            try
            {
                var startTime = DateTime.Now;

                #region UserGroup
                var userGroupEntity = (await GetResourcesDataEntity<CATECEV.Models.Entity.AMPECO.Resources.User.UserGroup, Models.AMPECO.resource.users.UserGroup, IAMPECOResource<Models.AMPECO.resource.users.UserGroup>>(_userGroupResource)).ToList();

                if (userGroupEntity.IsNotNullOrEmpty())
                {
                    var allUserGroupEntity = await _appContext.UserGroup.Where(x => x.IsActive).ToListAsync();
                    var allUserGroupEntityDictionary = allUserGroupEntity.ToDictionary(x => x.AMPECOId);

                    var filterdUserGroupEntity = userGroupEntity.Where(x => !allUserGroupEntityDictionary.ContainsKey(x.AMPECOId));

                    if (filterdUserGroupEntity.IsNotNullOrEmpty())
                    {
                        await _appContext.UserGroup.AddRangeAsync(filterdUserGroupEntity.DistinctBy(x => x.AMPECOId));
                        await _appContext.SaveChangesAsync();
                    }
                }
                #endregion

                #region User
                var userEntity = (await GetResourcesDataEntity<CATECEV.Models.Entity.AMPECO.Resources.User.User, Models.AMPECO.resource.users.User, IAMPECOResource<Models.AMPECO.resource.users.User>>(_user)).ToList();

                if (userEntity.IsNotNullOrEmpty())
                {
                    var groupIds = userEntity.Where(x => x.UserGroupIds.IsNotNullOrEmpty()).SelectMany(x => x.UserGroupIds);

                    var entityGroupData = await _appContext.UserGroup.Where(u => groupIds.Contains(u.AMPECOId)).ToListAsync();
                    var entityGroupDataDictionary = entityGroupData.ToDictionary(x => x.AMPECOId);

                    var allUserEntity = await _appContext.User.Where(x => x.IsActive).ToListAsync();
                    var allUserEntityDictionary = allUserEntity.ToDictionary(x => x.AMPECOId);

                    var filterdUserEntity = userEntity
                        .Where(x => !allUserEntityDictionary.ContainsKey(x.AMPECOId))
                        .Select(x =>
                        {
                            x.UserGroups = string.Join(",",
                                x.UserGroupIds
                                    .Select(id => entityGroupDataDictionary.TryGetValue(id, out var userGroup) ? userGroup.Name : null)
                                    .Where(name => name != null)
                            );
                            return x;
                        });


                    if (filterdUserEntity.IsNotNullOrEmpty())
                    {
                        await _appContext.User.AddRangeAsync(filterdUserEntity.DistinctBy(x => x.AMPECOId));
                        await _appContext.SaveChangesAsync();
                    }
                }
                #endregion

                #region ChargePoints
                var chargePointEntity = (await GetResourcesDataEntity<CATECEV.Models.Entity.AMPECO.Resources.ChargePoint.ChargePointEntity, Models.AMPECO.resource.ChargePoint.ChargePoint, IAMPECOResource<Models.AMPECO.resource.ChargePoint.ChargePoint>>(_aMPECOChargePoints)).ToList();

                if (chargePointEntity.IsNotNullOrEmpty())
                {
                    var entityOwnerUserDataForIds = await _entityUserService.GetUsersByAMPECOIds(chargePointEntity.Where(x => x.OwnerUserId.HasValue).Select(x => x.OwnerUserId.Value));
                    var entityOwnerPartnerContractDataForIds = await _entityUserService.GetUsersByAMPECOIds(chargePointEntity.Where(x => x.OwnerPartnerContractId.HasValue).Select(x => x.OwnerPartnerContractId.Value));
                    var entityOwnerPartnerDataForIds = await _entityUserService.GetUsersByAMPECOIds(chargePointEntity.Where(x => x.OwnerPartnerId.HasValue).Select(x => x.OwnerPartnerId.Value));
                    var entityUserDataDictionary = entityOwnerUserDataForIds.ToDictionary(x => x.AMPECOId, x => x.Id);
                    var entityOwnerPartnerContractDataDictionary = entityOwnerPartnerContractDataForIds.ToDictionary(x => x.AMPECOId, x => x.Id);
                    var entityOwnerPartnerDataDictionary = entityOwnerPartnerDataForIds.ToDictionary(x => x.AMPECOId, x => x.Id);

                    var allChargePointEntity = await _appContext.ChargePoint.Where(x => x.IsActive).ToListAsync();
                    var allChargePointEntityDictionary = allChargePointEntity.ToDictionary(x => x.AMPECOId);

                    var filterdChargePointEntity = chargePointEntity
                        .Where(x => !allChargePointEntityDictionary.ContainsKey(x.AMPECOId))
                        .Select(item =>
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

                            return item;
                        });

                    if (filterdChargePointEntity.IsNotNullOrEmpty())
                    {
                        await _appContext.ChargePoint.AddRangeAsync(filterdChargePointEntity.DistinctBy(x => x.AMPECOId));
                        await _appContext.SaveChangesAsync();
                    }
                }
                #endregion

                #region Evse
                var evseEntity = (await GetResourcesDataEntity<CATECEV.Models.Entity.AMPECO.Resources.ChargePoint.EvseEntity, Models.AMPECO.resource.ChargePoint.Evse, IAMPECOResource<Models.AMPECO.resource.ChargePoint.Evse>>(_aMPECOEvses)).ToList();

                if (evseEntity.IsNotNullOrEmpty())
                {
                    var entityChargePointDataForIds = await _entityChargePointService.GetChargePointByAMPECOIds(evseEntity.Select(x => x.AMPECOChargePointId));
                    var entityChargePointDataDictionary = entityChargePointDataForIds.ToDictionary(x => x.AMPECOId);

                    var allEvseEntity = await _appContext.Evse.Where(x => x.IsActive).ToListAsync();
                    var allEvseEntityDictionary = allEvseEntity.ToDictionary(x => x.AMPECOId);

                    var filterdEvseEntity = evseEntity
                        .Where(x => !allEvseEntityDictionary.ContainsKey(x.AMPECOId))
                        .Select(item =>
                        {
                            if (entityChargePointDataDictionary.TryGetValue(item.AMPECOChargePointId, out var entityChargePoint))
                            {
                                item.ChargePointId = entityChargePoint.Id;
                            }

                            return item;
                        });

                    if (filterdEvseEntity.IsNotNullOrEmpty())
                    {
                        await _appContext.Evse.AddRangeAsync(filterdEvseEntity.DistinctBy(x => x.AMPECOId));
                        await _appContext.SaveChangesAsync();
                    }
                }
                #endregion

                #region Tax
                var taxesEntity = (await GetResourcesDataEntity<CATECEV.Models.Entity.AMPECO.Resources.Tax.TaxEntity, Models.AMPECO.resource.Tax.Tax, IAMPECOResource<Models.AMPECO.resource.Tax.Tax>>(_aMPECOTaxes)).ToList();

                if (taxesEntity.IsNotNullOrEmpty())
                {
                    var allTaxEntity = await _appContext.Tax.Where(x => x.IsActive).ToListAsync();
                    var allTaxEntityDictionary = allTaxEntity.ToDictionary(x => x.AMPECOId);

                    var filterdTaxEntity = taxesEntity
                        .Where(x => !allTaxEntityDictionary.ContainsKey(x.AMPECOId));

                    if (filterdTaxEntity.IsNotNullOrEmpty())
                    {
                        await _appContext.Tax.AddRangeAsync(filterdTaxEntity.DistinctBy(x => x.AMPECOId));
                        await _appContext.SaveChangesAsync();
                    }
                }
                #endregion

                #region Authorization
                var authorizationEntity = (await GetResourcesDataEntity<CATECEV.Models.Entity.AMPECO.Resources.Authorization.AuthorizationEntity, Models.AMPECO.resource.Authorization.Authorization, IAMPECOResource<Models.AMPECO.resource.Authorization.Authorization>>(_aMPECOAuthorization)).ToList();
                if (authorizationEntity.IsNotNullOrEmpty())
                {
                    var allAuthorization = await _appContext.Authorization.Where(x => x.IsActive).ToListAsync();
                    var allAuthorizationDictionary = allAuthorization.ToDictionary(x => x.AMPECOId);

                    var filterdAuthorizationEntity = authorizationEntity
                        .Where(x => !allAuthorizationDictionary.ContainsKey(x.AMPECOId));

                    if (filterdAuthorizationEntity.IsNotNullOrEmpty())
                    {
                        await _appContext.Authorization.AddRangeAsync(filterdAuthorizationEntity.DistinctBy(x => x.AMPECOId));
                        await _appContext.SaveChangesAsync();
                    }
                }
                #endregion

                #region ChargingSession

                var ChargingSessionEntity = await GetResourcesDataEntity<CATECEV.Models.Entity.AMPECO.Resources.Session.ChargingSessionEntity, Models.AMPECO.resource.Session.ChargingSession, IAMPECOResource<Models.AMPECO.resource.Session.ChargingSession>>(_aMPECOSessions);
                if (ChargingSessionEntity.IsNotNullOrEmpty())
                {
                    var entityUserDataTask = await _entityUserService.GetUsersByAMPECOIds(ChargingSessionEntity.Select(x => x.AMPECOUserId));
                    var entityChargePointDataTask = await _entityChargePointService.GetChargePointByAMPECOIds(ChargingSessionEntity.Select(x => x.AMPECOChargePointId));
                    var entityEvseDataTask = await _entityEvseService.GetEvseByAMPECOIds(ChargingSessionEntity.Select(x => x.AMPECOEvseId));
                    var entityTaxDataTask = await _entityTaxService.GetTaxesByAMPECOIds(ChargingSessionEntity.Select(x => x.AMPECOTaxId));
                    var entityConnectorDataTask = await _entityConnectorService.GetConnectorByAMPECOIds(ChargingSessionEntity
                        .Where(x => x.AMPECOConnectorId.HasValue)
                        .Select(x => x.AMPECOConnectorId.Value));


                    var entityUserDataDictionary = (entityUserDataTask).ToDictionary(x => x.AMPECOId);
                    var entityChargePointDataDictionary = (entityChargePointDataTask).ToDictionary(x => x.AMPECOId);
                    var entityEvseDataDictionary = (entityEvseDataTask).ToDictionary(x => x.AMPECOId);
                    var entityTaxDataDictionary = (entityTaxDataTask).ToDictionary(x => x.AMPECOId);
                    var entityConnectorDataDictionary = (entityConnectorDataTask).ToDictionary(x => x.AMPECOId);

                    var allChargingSession = await _appContext.ChargingSession.Where(x => x.IsActive).ToListAsync();
                    var allChargingSessionDictionary = allChargingSession.ToDictionary(x => x.AMPECOId);

                    var filterdChargingSessionEntities = ChargingSessionEntity
                        .Where(item => !allChargingSessionDictionary.ContainsKey(item.AMPECOId))
                        .Select(item =>
                        {
                            item.UserId = entityUserDataDictionary.GetValueOrDefault(item.AMPECOUserId)?.Id;
                            item.ChargePointId = entityChargePointDataDictionary.GetValueOrDefault(item.AMPECOChargePointId)?.Id;
                            item.EvseId = entityEvseDataDictionary.GetValueOrDefault(item.AMPECOEvseId)?.Id;
                            item.ConnectorId = item.AMPECOConnectorId.HasValue ? entityConnectorDataDictionary.GetValueOrDefault(item.AMPECOConnectorId.Value)?.Id : null;
                            item.TaxId = entityTaxDataDictionary.GetValueOrDefault(item.AMPECOTaxId)?.Id;
                            return item;
                        }).ToList();

                    if (filterdChargingSessionEntities.IsNotNullOrEmpty())
                    {
                        await _appContext.ChargingSession.AddRangeAsync(filterdChargingSessionEntities.DistinctBy(x => x.AMPECOId));
                        await _appContext.SaveChangesAsync();
                    }
                }
                #endregion

                var dateDiff = (DateTime.Now - startTime).TotalSeconds;
                FileLogger.WriteLog($"GetData LOG {DateTime.Now} {dateDiff}");
                return Ok(true);
            }
            catch (Exception ex)
            {
                return Ok($"Message: {ex.Message}\n InnerException: {ex.InnerException}");
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

        #region Private
        private async Task<IEnumerable<CATECEV.Models.Entity.AMPECO.Resources.Session.ChargingSessionEntity>> ProcessChargingSessionsAsync()
        {
            var pageNumber = 1;

            var chargingSessionData = await _aMPECOSessions.GetResourceDataList(pageNumber);
            if (chargingSessionData is null || !chargingSessionData.Data.IsNotNullOrEmpty() || chargingSessionData.TotalRecords <= 0)
                return new List<CATECEV.Models.Entity.AMPECO.Resources.Session.ChargingSessionEntity>();

            var totalPages = chargingSessionData.TotalPages;
            var batchSize = 30;
            var chargingSessionEntity = new ConcurrentBag<CATECEV.Models.Entity.AMPECO.Resources.Session.ChargingSessionEntity>();


            var dateTime = DateTime.Now;

            for (var i = 1; i <= totalPages; i += batchSize)
            {
                var tasks = Enumerable.Range(i, Math.Min(batchSize, totalPages - i + 1))
                    .Select(async page =>
                    {
                        var pageData = await _aMPECOSessions.GetResourceDataList(page);
                        if (pageData != null && pageData.Data.IsNotNullOrEmpty())
                        {
                            var mappedData = _mapper.Map<IEnumerable<Models.AMPECO.resource.Session.ChargingSession>,
                                                         IEnumerable<CATECEV.Models.Entity.AMPECO.Resources.Session.ChargingSessionEntity>>(pageData.Data);
                            foreach (var item in mappedData)
                            {
                                chargingSessionEntity.Add(item);
                            }
                        }
                    });

                await Task.WhenAll(tasks);
            }

            if (chargingSessionEntity.IsNotNullOrEmpty())
            {
                var entityUserDataTask = await _entityUserService.GetUsersByAMPECOIds(chargingSessionEntity.Select(x => x.AMPECOUserId));
                var entityChargePointDataTask = await _entityChargePointService.GetChargePointByAMPECOIds(chargingSessionEntity.Select(x => x.AMPECOChargePointId));
                var entityEvseDataTask = await _entityEvseService.GetEvseByAMPECOIds(chargingSessionEntity.Select(x => x.AMPECOEvseId));
                var entityTaxDataTask = await _entityTaxService.GetTaxesByAMPECOIds(chargingSessionEntity.Select(x => x.AMPECOTaxId));
                var entityConnectorDataTask = await _entityConnectorService.GetConnectorByAMPECOIds(chargingSessionEntity
                    .Where(x => x.AMPECOConnectorId.HasValue)
                    .Select(x => x.AMPECOConnectorId.Value));


                var entityUserDataDictionary = (entityUserDataTask).ToDictionary(x => x.AMPECOId);
                var entityChargePointDataDictionary = (entityChargePointDataTask).ToDictionary(x => x.AMPECOId);
                var entityEvseDataDictionary = (entityEvseDataTask).ToDictionary(x => x.AMPECOId);
                var entityTaxDataDictionary = (entityTaxDataTask).ToDictionary(x => x.AMPECOId);
                var entityConnectorDataDictionary = (entityConnectorDataTask).ToDictionary(x => x.AMPECOId);
                var allChargingSession = await _appContext.ChargingSession.Where(x => x.IsActive).ToListAsync();
                var allChargingSessionDictionary = allChargingSession.ToDictionary(x => x.AMPECOId);

                var newEntities = chargingSessionEntity
                    .Where(item => !allChargingSessionDictionary.ContainsKey(item.AMPECOId))
                    .Select(item =>
                    {
                        item.UserId = entityUserDataDictionary.GetValueOrDefault(item.AMPECOUserId)?.Id;
                        item.ChargePointId = entityChargePointDataDictionary.GetValueOrDefault(item.AMPECOChargePointId)?.Id;
                        item.EvseId = entityEvseDataDictionary.GetValueOrDefault(item.AMPECOEvseId)?.Id;
                        item.ConnectorId = item.AMPECOConnectorId.HasValue ? entityConnectorDataDictionary.GetValueOrDefault(item.AMPECOConnectorId.Value)?.Id : null;
                        item.TaxId = entityTaxDataDictionary.GetValueOrDefault(item.AMPECOTaxId)?.Id;
                        return item;
                    }).ToList();

                var dateDiff = (DateTime.Now - dateTime).TotalSeconds;
                if (newEntities.IsNotNullOrEmpty())
                {
                    return newEntities;
                }
            }

            return chargingSessionEntity;
        }

        #endregion
    }
}
