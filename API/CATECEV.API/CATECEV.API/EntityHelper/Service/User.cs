using CATECEV.API.EntityHelper.IService;
using CATECEV.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CATECEV.API.EntityHelper.Service
{
    public class User : IUser
    {
        private readonly AppDBContext _appContext;
        public User(AppDBContext appContext)
        {
            _appContext = appContext;
        }

        public async Task<IEnumerable<CATECEV.Models.Entity.AMPECO.Resources.User.User>> GetUsersByAMPECOIds(IEnumerable<int> lstAMPECOIds)
        {
            var _data = await _appContext.User.Where(x => lstAMPECOIds.Contains(x.AMPECOId)).ToListAsync();

            var sss = _appContext.User.Where(x => lstAMPECOIds.Contains(x.AMPECOId)).ToQueryString();

            return _data;
        }

        public async Task<CATECEV.Models.Entity.AMPECO.Resources.User.User> GetUserByAMPECOId(int AMPECOId)
        {
            return await _appContext.User.FirstOrDefaultAsync(x=>x.AMPECOId == AMPECOId);
        }
    }
}
