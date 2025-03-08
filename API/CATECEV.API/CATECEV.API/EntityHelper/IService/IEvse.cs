using CATECEV.Models.Entity.AMPECO.Resources.ChargePoint;

namespace CATECEV.API.EntityHelper.IService
{
    public interface IEvse
    {
        Task<EvseEntity> GetEvseByAMPECOId(int AMPECOId);
        Task<IEnumerable<EvseEntity>> GetEvseByAMPECOIds(IEnumerable<int> lstAMPECOIds);
    }
}
