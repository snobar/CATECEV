using CATECEV.Models.Entity.AMPECO.Resources.ChargePoint;

namespace CATECEV.API.EntityHelper.IService
{
    public interface IConnector
    {
        Task<ConnectorEntity> GetConnectorByAMPECOId(int AMPECOId);
        Task<IEnumerable<ConnectorEntity>> GetConnectorByAMPECOIds(IEnumerable<int> lstAMPECOIds);
    }
}
