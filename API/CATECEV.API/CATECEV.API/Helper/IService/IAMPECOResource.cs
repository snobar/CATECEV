using CATECEV.API.Models;
using CATECEV.API.Models.AMPECO.resource.ChargePoint;

namespace CATECEV.API.Helper.IService
{
    public interface IAMPECOResource<T>
    {
        Task<AMPECOResponseModel<IEnumerable<T>>> GetResourceData(int pageNumber, int pageSize = 100);
    }
}
