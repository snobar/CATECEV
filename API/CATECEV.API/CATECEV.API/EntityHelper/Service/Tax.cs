using CATECEV.API.EntityHelper.IService;
using CATECEV.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CATECEV.API.EntityHelper.Service
{
    public class Tax : ITax
    {
        private readonly AppDBContext _appContext;
        public Tax(AppDBContext appContext)
        {
            _appContext = appContext;
        }

        public async Task<IEnumerable<CATECEV.Models.Entity.AMPECO.Resources.Tax.TaxEntity>> GetTaxesByAMPECOIds(IEnumerable<int> lstAMPECOIds)
        {
            var _data = await _appContext.Tax.Where(x => lstAMPECOIds.Contains(x.AMPECOId)).ToListAsync();
            return _data;
        }

        public async Task<CATECEV.Models.Entity.AMPECO.Resources.Tax.TaxEntity> GetTaxByAMPECOId(int AMPECOId)
        {
            return await _appContext.Tax.FirstOrDefaultAsync(x => x.AMPECOId == AMPECOId);
        }
    }
}
