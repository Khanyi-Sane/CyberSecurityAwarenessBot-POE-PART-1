using CybersecurityChatbotGUI.Models;

namespace CybersecurityChatbotGUI.Services
{
    // Remembers the actions the chatbot has taken (tasks, reminders, quiz, NLP commands).
    public class ActivityLogger
    {
        // A list is enough for the brief; the log lives in memory while the app runs.
        private readonly List<ActivityEntry> _entries = new List<ActivityEntry>();

        // How many actions have been recorded in total.
        public int TotalCount => _entries.Count;

        // Adds one action to the end of the list with the current time.
        public void Log(string description)
        {
            _entries.Add(new ActivityEntry
            {
                Time = DateTime.Now,
                Description = description
            });
        }

        // Returns the newest actions first, limited to 'count' items.
        public List<ActivityEntry> GetRecent(int count)
        {
            return _entries
                .AsEnumerable()
                .Reverse()      // newest first (the list is stored oldest first)
                .Take(count)
                .ToList();
        }
    }
}