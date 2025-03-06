using CATECEV.API.Models;
using CATECEV.API.Models.AMPECO.resource.users;

namespace CATECEV.API.EntityHelper.IService
{
    public interface IUser
    {
        Task<CATECEV.Models.Entity.AMPECO.Resources.User.User> GetUserByAMPECOId(int AMPECOId);
        Task<IEnumerable<CATECEV.Models.Entity.AMPECO.Resources.User.User>> GetUsersByAMPECOIds(IEnumerable<int> lstAMPECOIds);
    }
}
