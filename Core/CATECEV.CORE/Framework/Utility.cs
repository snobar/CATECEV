using Microsoft.Extensions.Configuration;

namespace CATECEV.CORE.Framework
{
    public static class Utility
    {
        public static IConfiguration AppSetting { get; }
        static Utility()
        {
            AppSetting = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        }

        public static string GetAppsettingsValue(string sectionPath, string key = "")
        {
            // Check environment variables first
            var environmentVariableKey = sectionPath.Replace(":", "__");
            if (!string.IsNullOrEmpty(key))
            {
                environmentVariableKey += $"__{key}";
            }

            var environmentVariableValue = Environment.GetEnvironmentVariable(environmentVariableKey);
            if (!string.IsNullOrEmpty(environmentVariableValue))
            {
                return environmentVariableValue;
            }

            // If the environment variable is not found, check AppSettings
            if (AppSetting != null)
            {
                var section = AppSetting.GetSection(sectionPath);
                if (section.Exists())
                {
                    if (!string.IsNullOrEmpty(key))
                    {
                        var value = section[key];
                        if (value != null)
                        {
                            return value;
                        }
                    }
                    else
                    {
                        // If no specific key is provided, return the entire section as JSON string
                        return section.Value;
                    }
                }
            }

            // If the section or key is not found in either environment variables or AppSettings, return null or handle the scenario as needed
            return null;
        }
    }
}
