using AutoMapper;
using CATECEV.API.Helper.IService;
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
        private readonly IUser _user;
        private readonly AppDBContext _appContext;
        private readonly IMapper _mapper;
        public MapDataController(IUser user, AppDBContext appContext, IMapper mapper)
        {
            _user = user;
            _appContext = appContext;
            _mapper = mapper;
        }

        [HttpGet("GetData")]
        public async Task<IActionResult> GetData()
        {
            var pageNumber = 1;
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
                        var checkUser = await _appContext.User.AnyAsync(x=>x.AMPECOId == item.AMPECOId);
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

            return Ok(userData);
        }
    }
}
