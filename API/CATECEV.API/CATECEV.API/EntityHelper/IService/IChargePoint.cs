using CATECEV.Models.Entity.AMPECO.Resources.ChargePoint;

namespace CATECEV.API.EntityHelper.IService
{
    public interface IChargePoint
    {
        Task<ChargePointEntity> GetChargePointByAMPECOId(int AMPECOId);
        Task<IEnumerable<ChargePointEntity>> GetChargePointByAMPECOIds(IEnumerable<int> lstAMPECOIds);
    }
}
