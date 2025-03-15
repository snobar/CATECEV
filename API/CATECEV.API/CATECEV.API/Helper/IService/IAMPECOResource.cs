using CATECEV.API.Models;
using CATECEV.API.Models.AMPECO.resource.ChargePoint;

namespace CATECEV.API.Helper.IService
{
    public interface IAMPECOResource<T>
    {
        Task<AMPECOResponseModel<T>> GetResourceData(int AMPECOId);
        Task<AMPECOResponseModel<IEnumerable<T>>> GetResourceDataList(int pageNumber, int pageSize = 100);
    }
}
