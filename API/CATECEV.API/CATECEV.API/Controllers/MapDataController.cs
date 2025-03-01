using CATECEV.API.Helper.IService;
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

        public MapDataController(IUser user, AppDBContext appContext)
        {
            _user = user;
            _appContext = appContext;
        }

        [HttpGet("GetUserListTest")]
        public async Task<IActionResult> GetUserListTest()
        {
            var userData = await _user.GetUsers();


            return Ok(userData);
        }
    }
}
