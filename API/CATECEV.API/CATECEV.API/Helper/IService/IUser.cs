using CATECEV.API.Models;
using CATECEV.API.Models.AMPECO.resource.users;

namespace CATECEV.API.Helper.IService
{
    public interface IUser
    {
        Task<User> GetUser(int userId);
        Task<AMPECOResponseModel<IEnumerable<User>>> GetUsers(int pageNumber, int pageSize = 100);
    }
}
