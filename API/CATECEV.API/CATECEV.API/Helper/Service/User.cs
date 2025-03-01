using CATECEV.API.Helper.IService;
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

        public async Task<IEnumerable<Models.AMPECO.resource.users.User>> GetUsers(int pageNumber, int pageSize = 100)
        {

            var apiUrl = $"{_userApi}?page={pageNumber}&per_page={pageSize}";
            var userData = await _httpClientService.GetAsync<IEnumerable<Models.AMPECO.resource.users.User>>(apiUrl, _token);
            if (userData.IsNotNullOrEmpty() && userData.Data.IsNotNullOrEmpty() && userData.IsSuccess && userData.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return userData.Data;
            }

            return new List<Models.AMPECO.resource.users.User>();
        }
    }
}
