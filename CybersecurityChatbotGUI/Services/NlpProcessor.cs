using System.Text.RegularExpressions;

namespace CybersecurityChatbotGUI.Services
{
    // The kinds of request the NLP can recognise. "None" means: not a Part 3 command,
    // so hand the message to the normal chatbot (keywords, sentiment, memory).
    public enum UserIntent
    {
        None,
        AddTask,
        StartQuiz,
        ShowTasks,
        ShowActivityLog
    }

    // Everything the NLP worked out from one message.
    public class IntentResult
    {
        public UserIntent Intent { get; set; } = UserIntent.None;
        public string OriginalText { get; set; } = "";
        public string TaskTitle { get; set; } = "";
        public DateTime? ReminderDate { get; set; }
        public bool ShowAll { get; set; }   // "show more activity" asks for the longer list
    }

    // Simulated NLP: clean the text, look for keywords and patterns, decide the intent.
    public class NlpProcessor
    {
        private static readonly string[] ActivityPhrases =
        {
            "activity log", "what have you done", "what did you do", "recent actions",
            "recent activity", "show my activity", "show activity", "show log"
        };

        private static readonly string[] QuizPhrases =
        {
            "quiz", "test my knowledge", "test me", "cybersecurity game",
            "mini game", "mini-game", "play a game", "trivia"
        };

        // Looks at one message and decides what the user wants.
        public IntentResult DetectIntent(string rawInput)
        {
            string text = rawInput.Trim().ToLower(); // lowercase copy used only for matching
            IntentResult result = new IntentResult { OriginalText = rawInput.Trim() };

            // 1. Activity log requests (the phrase may not contain the words "activity log").
            if (ContainsAny(text, ActivityPhrases))
            {
                result.Intent = UserIntent.ShowActivityLog;
                result.ShowAll = ContainsAny(text, "more", "all", "full", "everything", "10");
                return result;
            }

            // 2. Quiz requests.
            if (ContainsAny(text, QuizPhrases))
            {
                result.Intent = UserIntent.StartQuiz;
                return result;
            }

            bool hasTaskWord = Regex.IsMatch(text, @"\b(task|tasks|to-?do)\b");
            bool hasReminderWord = text.Contains("remind");
            bool hasAddVerb = Regex.IsMatch(text, @"\b(add|create|make|new|set|schedule|put)\b");

            // 3. Viewing tasks ("show my tasks", "what tasks do I have").
            bool viewVerb = Regex.IsMatch(text, @"\b(show|view|see|list|display|open|check)\b");
            if (!hasAddVerb &&
                ((viewVerb && Regex.IsMatch(text, @"\b(tasks?|reminders?|to-?do)\b"))
                 || ContainsAny(text, "what tasks", "my tasks", "my reminders", "pending tasks", "to-do list")))
            {
                result.Intent = UserIntent.ShowTasks;
                return result;
            }

            // 4. Adding a task or reminder, worded in many different ways.
            bool wantsTask = text.Contains("remind me")
                          || (hasReminderWord && hasAddVerb)
                          || (hasTaskWord && hasAddVerb);

            if (wantsTask)
            {
                string title = ExtractTaskTitle(rawInput);

                // "Can you remind me what phishing is?" is a question, not a task.
                if (!hasTaskWord && Regex.IsMatch(title.ToLower(), @"^(what|how|why|who|when|where|if)\b"))
                    return result; // None

                result.Intent = UserIntent.AddTask;
                result.TaskTitle = title;
                result.ReminderDate = ExtractReminderDate(rawInput);
                return result;
            }

            return result; // None: let the normal chatbot answer
        }

        // True if the text contains at least one of the phrases.
        private bool ContainsAny(string text, params string[] phrases)
        {
            foreach (string phrase in phrases)
            {
                if (text.Contains(phrase))
                    return true;
            }
            return false;
        }

        // Strips the command words and the date wording, leaving just the task itself.
        // Works on the ORIGINAL text so the task keeps the user's capital letters.
        public string ExtractTaskTitle(string rawInput)
        {
            string t = rawInput.Trim();
            RegexOptions ci = RegexOptions.IgnoreCase;

            // Polite lead-ins: "hey", "please", "can you", "I want you to".
            t = Regex.Replace(t, @"^(hey|hi|hello)[,!\s]+", "", ci);
            t = Regex.Replace(t, @"^(please|pls|kindly)\s+", "", ci);
            t = Regex.Replace(t, @"^(can|could|would|will)\s+you\s+(please\s+)?", "", ci);
            t = Regex.Replace(t, @"^i\s+(want|need|would like|'d like)\s+(you\s+)?to\s+", "", ci);

            // Command phrase: "add a task to", "create task -", "set a reminder to", "new task:".
            t = Regex.Replace(t,
                @"^(add|create|make|set\s+up|setup|set|schedule|put)\s+(me\s+)?((a|an|new)\s+)*(task|reminder|to-?do)\s*(for\s+me)?\s*(to|that|about|:|-|–)?\s*",
                "", ci);

            // "remind me to ...", also "remind me in 3 days to ..." / "remind me tomorrow to ...".
            t = Regex.Replace(t,
                @"^remind\s+me\s+(in\s+\d+\s+(days?|weeks?)\s+|tomorrow\s+|today\s+)?(to\s+)?",
                "", ci);

            // Date wording at the end: "... tomorrow", "... in 3 days", "... next week".
            t = Regex.Replace(t,
                @"\s*,?\s*(in\s+\d+\s+(days?|weeks?)|in\s+a\s+(day|week)|tomorrow|today|tonight|next\s+week)\s*[.!?]*$",
                "", ci);

            t = t.Trim().TrimEnd('.', '!', '?').Trim();

            if (t.Length < 3)
                return "";

            if (t.Length > 150)
                t = t.Substring(0, 150); // matches the title column in MySQL

            // Start the title with a capital letter.
            return char.ToUpper(t[0]) + t.Substring(1);
        }

        // Turns phrases like "tomorrow" or "in 3 days" into a real date (null = none found).
        public DateTime? ExtractReminderDate(string rawInput)
        {
            string text = rawInput.ToLower();

            if (text.Contains("tomorrow"))
                return DateTime.Today.AddDays(1);

            if (text.Contains("today") || text.Contains("tonight"))
                return DateTime.Today;

            // "in 3 days", "in 2 weeks"
            Match number = Regex.Match(text, @"in\s+(\d+)\s+(day|days|week|weeks)");
            if (number.Success && int.TryParse(number.Groups[1].Value, out int amount) && amount <= 365)
            {
                bool weeks = number.Groups[2].Value.StartsWith("week");
                return DateTime.Today.AddDays(weeks ? amount * 7 : amount);
            }

            // "in three days", "in two weeks"
            Match word = Regex.Match(text, @"in\s+(one|two|three|four|five|six|seven|ten)\s+(day|days|week|weeks)");
            if (word.Success)
            {
                int amountFromWord = word.Groups[1].Value switch
                {
                    "one" => 1,
                    "two" => 2,
                    "three" => 3,
                    "four" => 4,
                    "five" => 5,
                    "six" => 6,
                    "seven" => 7,
                    _ => 10
                };
                bool weeks = word.Groups[2].Value.StartsWith("week");
                return DateTime.Today.AddDays(weeks ? amountFromWord * 7 : amountFromWord);
            }

            if (Regex.IsMatch(text, @"in\s+a\s+week|next\s+week"))
                return DateTime.Today.AddDays(7);

            if (Regex.IsMatch(text, @"in\s+a\s+day"))
                return DateTime.Today.AddDays(1);

            return null;
        }

        // Used when the bot has asked "Would you like a reminder?"
        public bool IsYes(string rawInput)
        {
            return Regex.IsMatch(rawInput.Trim().ToLower(), @"^(yes|yeah|yep|yup|sure|ok|okay|please|y)\b");
        }

        public bool IsNo(string rawInput)
        {
            return Regex.IsMatch(rawInput.Trim().ToLower(), @"^(no|nope|nah|n|not now|no thanks)\b");
        }
    }
}