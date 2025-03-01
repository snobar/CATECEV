using CATECEV.API.Models.AMPECO.resource.users;

namespace CATECEV.API.Helper.IService
{
    public interface IUser
    {
        Task<IEnumerable<User>> GetUsers(int pageNumber, int pageSize = 100);
    }
}
