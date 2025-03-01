using AutoMapper;
using CATECEV.API.Helper.IService;
using CATECEV.CORE.Extensions;
using CATECEV.Data.Context;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("GetUserListTest")]
        public async Task<IActionResult> GetUserListTest()
        {
            var pageNumber = 1;
            var userData1 = await _user.GetUsers(pageNumber);
            var userData2 = await _user.GetUsers(2);

            if (userData1.IsNotNullOrEmpty())
            {
                var mappedUserData = _mapper.Map<IEnumerable<Models.AMPECO.resource.users.User>, IEnumerable<CATECEV.Models.Entity.AMPECO.Resources.User.User>>(userData1);
            }

            return Ok(userData1);
        }
    }
}
