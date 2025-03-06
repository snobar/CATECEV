using CATECEV.API.Models;
using CATECEV.API.Models.AMPECO.resource.ChargePoint;

namespace CATECEV.API.Helper.IService
{
    public interface IAMPECOChargePoints
    {
        Task<AMPECOResponseModel<IEnumerable<ChargePoint>>> GetChargePoints(int pageNumber, int pageSize = 100);
    }
}
