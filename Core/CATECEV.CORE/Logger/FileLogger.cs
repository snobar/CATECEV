namespace CATECEV.CORE.Logger
{
    public static class FileLogger
    {
        public static void WriteLog(string message)
        {
            try
            {
                string rootPath = AppDomain.CurrentDomain.BaseDirectory;
                string logFilePath = Path.Combine(rootPath, "log.txt");

                string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}{Environment.NewLine}";

                File.AppendAllText(logFilePath, logMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to write log: {ex.Message}");
            }
        }
    }
}
