using System.Text.Json.Serialization;

namespace CATECEV.API.Models.Zuper
{
    public class ZuperUser
    {
        [JsonPropertyName("user_uid")]
        public string UserUid { get; set; }

        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string LastName { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("external_login_id")]
        public string ExternalLoginId { get; set; }

        [JsonPropertyName("home_phone_number")]
        public string HomePhoneNumber { get; set; }

        [JsonPropertyName("designation")]
        public string Designation { get; set; }

        [JsonPropertyName("emp_code")]
        public string EmpCode { get; set; }

        [JsonPropertyName("prefix")]
        public string Prefix { get; set; }

        [JsonPropertyName("work_phone_number")]
        public string WorkPhoneNumber { get; set; }

        [JsonPropertyName("mobile_phone_number")]
        public string MobilePhoneNumber { get; set; }

        [JsonPropertyName("profile_picture")]
        public string ProfilePicture { get; set; }

        [JsonPropertyName("hourly_labor_charge")]
        public decimal? HourlyLaborCharge { get; set; }

        [JsonPropertyName("is_active")]
        public bool IsActive { get; set; }

        [JsonPropertyName("is_deleted")]
        public bool IsDeleted { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }

        public ZuperUserRoles role { get; set; }

        public object access_role { get; set; }
    }

    public class ZuperUserRoles
    {
        public string role_uid { get; set; }
        public string role_name { get; set; }
        public string role_key { get; set; }
    }
}
