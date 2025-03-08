using CATECEV.API.EntityHelper.IService;
using CATECEV.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CATECEV.API.EntityHelper.Service
{
    public class Evse : IEvse
    {
        private readonly AppDBContext _appContext;
        public Evse(AppDBContext appContext)
        {
            _appContext = appContext;
        }

        public async Task<IEnumerable<CATECEV.Models.Entity.AMPECO.Resources.ChargePoint.EvseEntity>> GetEvseByAMPECOIds(IEnumerable<int> lstAMPECOIds)
        {
            var _data = await _appContext.Evse.Where(x => lstAMPECOIds.Contains(x.AMPECOId)).ToListAsync();
            return _data;
        }

        public async Task<CATECEV.Models.Entity.AMPECO.Resources.ChargePoint.EvseEntity> GetEvseByAMPECOId(int AMPECOId)
        {
            return await _appContext.Evse.FirstOrDefaultAsync(x => x.AMPECOId == AMPECOId);
        }
    }
}
