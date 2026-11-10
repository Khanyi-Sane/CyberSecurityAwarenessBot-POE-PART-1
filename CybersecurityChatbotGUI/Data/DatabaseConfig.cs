namespace CybersecurityChatbotGUI.Data
{
    // Holds the details C# needs to connect to MySQL.
    public static class DatabaseConfig
    {
        // Replace YOUR_PASSWORD with your own MySQL root password on your computer.
        // Never commit your real password to GitHub.
        public static string ConnectionString =
            "Server=localhost;Port=3306;" +
            "Database=CyberSecurityBotDB;" +
            "Uid=root;Pwd=YOUR_PASSWORD;";
    }
}