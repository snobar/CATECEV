using CATECEV.API.Models;
using CATECEV.API.Models.AMPECO.resource.Tax;

namespace CATECEV.API.Helper.IService
{
    public interface IAMPECOTaxes
    {
        Task<AMPECOResponseModel<IEnumerable<Tax>>> GetTaxes(int pageNumber, int pageSize = 100);
    }
}
