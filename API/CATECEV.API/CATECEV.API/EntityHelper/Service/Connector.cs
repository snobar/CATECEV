using CATECEV.API.EntityHelper.IService;
using CATECEV.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CATECEV.API.EntityHelper.Service
{
    public class Connector : IConnector
    {
        private readonly AppDBContext _appContext;
        public Connector(AppDBContext appContext)
        {
            _appContext = appContext;
        }

        public async Task<IEnumerable<CATECEV.Models.Entity.AMPECO.Resources.ChargePoint.ConnectorEntity>> GetConnectorByAMPECOIds(IEnumerable<int> lstAMPECOIds)
        {
            var _data = await _appContext.Connector.Where(x => lstAMPECOIds.Contains(x.AMPECOId)).ToListAsync();
            return _data;
        }

        public async Task<CATECEV.Models.Entity.AMPECO.Resources.ChargePoint.ConnectorEntity> GetConnectorByAMPECOId(int AMPECOId)
        {
            return await _appContext.Connector.FirstOrDefaultAsync(x => x.AMPECOId == AMPECOId);
        }
    }
}
