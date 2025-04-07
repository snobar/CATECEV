using CATECEV.API.Models;
using CATECEV.API.Models.AMPECO.resource.ChargePoint;

namespace CATECEV.API.Helper.IService
{
    public interface IAMPECOResource<T>
    {
        Task<Models.ResponseModel<T>> GetResourceData(int AMPECOId);
        Task<Models.ResponseModel<IEnumerable<T>>> GetResourceDataList(int pageNumber, int pageSize = 100, string fromDate = "", string toDate = "");
        Task<IEnumerable<T>> GetFullResourcesData(string _fromDate = "", string _toDate = "");
    }
}
