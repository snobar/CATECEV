using AutoMapper;
using CATECEV.API.Helper.IService;
using CATECEV.CORE.Logger;
using CATECEV.Data.Context;
using Microsoft.AspNetCore.Mvc;

namespace CATECEV.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZuperController : ControllerBase
    {
        private readonly AppDBContext _appContext;
        private readonly IMapper _mapper;
        private readonly IZuper _zuper;

        public ZuperController(AppDBContext appContext, IMapper mapper, IZuper zuper)
        {
            _appContext = appContext;
            _mapper = mapper;
            _zuper = zuper;
        }

        [HttpGet("GetTeams")]
        public async Task<IActionResult> GetTeams()
        {
            try
            {
                var teams = await _zuper.GetTeams();

                return Ok(teams);
            }
            catch (Exception ex)
            {
                FileLogger.WriteLog($"Message: {ex.Message}\n InnerException: {ex.InnerException}");
                return StatusCode(500, false);
            }
        }

        [HttpGet("MapTimesheetData")]
        public async Task<IActionResult> MapData()
        {

            var sss = await _zuper.GetTimesheet();

            return Ok();
        }
    }
}
