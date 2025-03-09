using CATECEV.API.Models;
using CATECEV.API.Models.AMPECO.resource.ChargePoint;

namespace CATECEV.API.Helper.IService
{
    public interface IAMPECOEvses
    {
        Task<AMPECOResponseModel<IEnumerable<Evse>>> GetEvse(int pageNumber, int pageSize = 100);
    }
}
