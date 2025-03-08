using CATECEV.Models.Entity.AMPECO.Resources.Tax;

namespace CATECEV.API.EntityHelper.IService
{
    public interface ITax
    {
        Task<TaxEntity> GetTaxByAMPECOId(int AMPECOId);
        Task<IEnumerable<TaxEntity>> GetTaxesByAMPECOIds(IEnumerable<int> lstAMPECOIds);
    }
}
