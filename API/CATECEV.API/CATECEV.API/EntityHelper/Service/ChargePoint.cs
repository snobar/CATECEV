using CATECEV.API.EntityHelper.IService;
using CATECEV.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CATECEV.API.EntityHelper.Service
{
    public class ChargePoint : IChargePoint
    {
        private readonly AppDBContext _appContext;
        public ChargePoint(AppDBContext appContext)
        {
            _appContext = appContext;
        }

        public async Task<IEnumerable<CATECEV.Models.Entity.AMPECO.Resources.ChargePoint.ChargePointEntity>> GetChargePointByAMPECOIds(IEnumerable<int> lstAMPECOIds)
        {
            var _data = await _appContext.ChargePoint.Where(x => lstAMPECOIds.Contains(x.AMPECOId)).ToListAsync();
            return _data;
        }

        public async Task<CATECEV.Models.Entity.AMPECO.Resources.ChargePoint.ChargePointEntity> GetChargePointByAMPECOId(int AMPECOId)
        {
            return await _appContext.ChargePoint.FirstOrDefaultAsync(x => x.AMPECOId == AMPECOId);
        }
    }
}
