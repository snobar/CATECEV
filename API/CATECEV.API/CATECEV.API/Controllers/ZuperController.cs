using AutoMapper;
using CATECEV.API.Helper.IService;
using CATECEV.API.Models.Zuper.Filter;
using CATECEV.CORE.Extensions;
using CATECEV.CORE.Framework;
using CATECEV.CORE.Logger;
using CATECEV.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

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
        public async Task<IActionResult> MapData(int? Count, string FromDate, string ToDate)
        {

            if (ModelState.IsValid)
            {

                string[] validFormats = new[]
                {
                    "yyyy-MM-dd HH:mm:ss.fff",
                    "yyyy-MM-dd HH:mm:ss",
                    "yyyy-MM-ddTHH:mm:ss.fff",
                    "yyyy-MM-ddTHH:mm:ss",
                    "MM/dd/yyyy HH:mm:ss",
                    "MM/dd/yyyy",
                    "dd-MM-yyyy",
                    "dd/MM/yyyy",
                    "dd.MM.yyyy HH:mm:ss",
                    "dd.MM.yyyy"
                };

                var count = Count ?? Utility.GetAppsettingsValue("ZuperConfiguration", "TimesheetCount").ToAnyType<int>();

                if (DateTime.TryParseExact(FromDate, validFormats, null, System.Globalization.DateTimeStyles.None, out var fromDate))
                {
                    FromDate = fromDate.ToString("yyyy-MM-dd");
                }

                if (DateTime.TryParseExact(ToDate, validFormats, null, System.Globalization.DateTimeStyles.None, out var toDate))
                {
                    ToDate = toDate.ToString("yyyy-MM-dd");
                }

                var zuperTimesheet = await _zuper.GetTimesheet(count, FromDate, ToDate);

                if (zuperTimesheet.status == "success" && zuperTimesheet.data.IsNotNullOrEmpty() && zuperTimesheet.data.total_records > 0 && zuperTimesheet.data.timesheets.IsNotNullOrEmpty())
                {
                    return Ok(zuperTimesheet.data.timesheets);
                }

                return Ok(zuperTimesheet);
            }

            return StatusCode(400, false);
        }

        [HttpGet("GetTimeoffRequestType")]
        public async Task<IActionResult> GetTimeoffRequestType()
        {
            try
            {
                var timeoffRequestType = await _zuper.GetTimeoffRequestType();

                return Ok(timeoffRequestType);
            }
            catch (Exception ex)
            {
                FileLogger.WriteLog($"GetTimeoffRequestType Message: {ex.Message}\n InnerException: {ex.InnerException}");
                return StatusCode(500, false);
            }
        }

        [HttpGet("GetZuperUsers")]
        public async Task<IActionResult> GetZuperUsers()
        {
            try
            {
                var users = await _zuper.GetZuperUsers();

                return Ok(users);
            }
            catch (Exception ex)
            {
                FileLogger.WriteLog($"GetZuperUsers Message: {ex.Message}\n InnerException: {ex.InnerException}");
                return StatusCode(500, false);
            }
        }
    }
}
