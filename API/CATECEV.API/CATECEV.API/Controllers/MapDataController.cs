using AutoMapper;
using CATECEV.API.Helper.IService;
using CATECEV.API.EntityHelper.IService;
using CATECEV.CORE.Extensions;
using CATECEV.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        private readonly AppDBContext _appContext;
        private readonly IMapper _mapper;
        public MapDataController(IAMPECOUser user, AppDBContext appContext, IMapper mapper, IUser entityUserService, IAMPECOChargePoints aMPECOChargePoints, IAMPECOSessions aMPECOSessions, IAMPECOTaxes aMPECOTaxes)
        {
            _user = user;
            _appContext = appContext;
            _mapper = mapper;
            _entityUserService = entityUserService;
            _aMPECOChargePoints = aMPECOChargePoints;
            _aMPECOSessions = aMPECOSessions;
            _aMPECOTaxes = aMPECOTaxes;
        }

        [HttpGet("GetData")]
        public async Task<IActionResult> GetData()
        {
            var pageNumber = 1;

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

                while (userData.TotalPages > pageNumber)
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
                    var entityUserDataForIds = await _entityUserService.GetUsersByAMPECOIds(chargePointEntity.Where(x => x.OwnerUserId.HasValue).Select(x => x.OwnerUserId.Value));
                    var entityUserDataDictionary = entityUserDataForIds.ToDictionary(x => x.AMPECOId, x => x.Id);

                    foreach (var item in chargePointEntity.ToList())
                    {
                        var checkUser = await _appContext.ChargePoint.AnyAsync(x => x.AMPECOId == item.AMPECOId);
                        if (checkUser)
                        {
                            chargePointEntity.Remove(item);
                        }
                        else if (item.OwnerUserId.HasValue && entityUserDataDictionary.TryGetValue(item.OwnerUserId.Value,out var entityUserId))
                        {
                            item.OwnerUserId = entityUserId;
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
                var mappedChargePointData = _mapper.Map<IEnumerable<Models.AMPECO.resource.Tax.Tax>, IEnumerable<CATECEV.Models.Entity.AMPECO.Resources.Tax.TaxEntity>>(taxesData.Data);
                taxesEntity.AddRange(mappedChargePointData.ToList());

                while (userData.TotalPages > pageNumber)
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
                    if (taxesEntity.IsNotNullOrEmpty())
                    {
                        await _appContext.Tax.AddRangeAsync(taxesEntity);
                        await _appContext.SaveChangesAsync();
                    }
                }
            }
            #endregion


            return Ok(true);
        }
    }
}
