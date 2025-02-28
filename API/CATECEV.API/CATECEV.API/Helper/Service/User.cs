using CATECEV.API.Helper.IService;
using CATECEV.API.Models.AMPECO;
using CATECEV.CORE.Extensions;
using CATECEV.CORE.Framework;

namespace CATECEV.API.Helper.Service
{
    public class User : IUser
    {
        private string _userApi;
        private string _token;

        private readonly IHttpClientService _httpClientService;

        public User(IHttpClientService httpClientService)
        {
            _userApi = $"{Utility.GetAppsettingsValue("AmpecoBaseUrl")}{Utility.GetAppsettingsValue("Resources", "User")}";
            _token = Utility.GetAppsettingsValue("AccessToken");
            _httpClientService = httpClientService;
        }

        public async Task<IEnumerable<Models.AMPECO.resource.users.User>> GetUsers()
        {

            var userData = await _httpClientService.GetAsync<Data<IEnumerable<Models.AMPECO.resource.users.User>>>(_userApi, _token);
            if (userData.IsNotNullOrEmpty() && userData.data.IsNotNullOrEmpty())
            {
                return userData.data;
            }

            return new List<Models.AMPECO.resource.users.User>();
        }
    }
}
