namespace CybersecurityChatbotGUI.Models
{
    // One cybersecurity task. Each object matches one row in the cyber_tasks table.
    public class CyberTask
    {
        public int TaskId { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";

        // The "?" makes this nullable, because a reminder is optional.
        public DateTime? ReminderDate { get; set; }

        public bool IsCompleted { get; set; }
    }
}
