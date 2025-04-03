
using CATECEV.API.Models.Zuper;
using CATECEV.API.Models.Zuper.Teams;
using CATECEV.API.Models.Zuper.Timesheet;

namespace CATECEV.API.Helper.IService
{
    public interface IZuper
    {
        Task<Models.ResponseModel<IEnumerable<ZuperTeam>>> GetTeams();
        Task<ZuperResponse<ZuperTimesheetData>> GetTimesheet(int count, string date, string to_date);
        Task<ZuperResponse<List<TimeoffRequestType>>> GetTimeoffRequestType();
        Task<ZuperResponse<List<ZuperUser>>> GetZuperUsers();
    }
}
