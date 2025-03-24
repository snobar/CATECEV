
using CATECEV.API.Models.Zuper.Teams;

namespace CATECEV.API.Helper.IService
{
    public interface IZuper
    {
        Task<Models.ResponseModel<IEnumerable<ZuperTeam>>> GetTeams();
        Task<int> GetTimesheet();
    }
}
