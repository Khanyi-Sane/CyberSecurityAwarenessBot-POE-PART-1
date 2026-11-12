namespace CybersecurityChatbotGUI.Models
{
    // One line in the activity log: when something happened and what it was.
    public class ActivityEntry
    {
        public DateTime Time { get; set; }
        public string Description { get; set; } = "";

        // This is how the entry looks when it is shown in the list box.
        public override string ToString()
        {
            return $"{Time:dd MMM HH:mm}  -  {Description}";
        }
    }
}