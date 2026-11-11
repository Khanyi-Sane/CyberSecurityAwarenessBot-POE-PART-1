using CybersecurityChatbotGUI.Models;
using CybersecurityChatbotGUI.Services;

namespace CybersecurityChatbotGUI
{
    // Connects the chat box to the Part 3 features through the NLP.
    public partial class ChatbotForm
    {
        private readonly NlpProcessor _nlp = new NlpProcessor();

        // When the bot has just added a task and asked "Would you like a reminder?",
        // these remember which task the user's next answer is about.
        private int _pendingReminderTaskId = 0;
        private string _pendingReminderTitle = "";

        // Returns true if the message was a Part 3 command (so the normal chatbot is skipped).
        private bool HandlePart3Command(string message)
        {
            // 1. Are we waiting for an answer to "Would you like a reminder?"
            if (_pendingReminderTaskId != 0)
            {
                if (TryAnswerPendingReminder(message))
                    return true;

                // The user moved on to something else, so forget the question.
                _pendingReminderTaskId = 0;
                _pendingReminderTitle = "";
            }

            // 2. Work out what the user wants.
            IntentResult result = _nlp.DetectIntent(message);

            switch (result.Intent)
            {
                case UserIntent.AddTask:
                    DisplayBotResponse(AddTaskFromChat(result));
                    return true;

                case UserIntent.StartQuiz:
                    LogActivity("Chat command understood: start the quiz");
                    tabMain.SelectedTab = tabQuiz;
                    btnStartQuiz_Click(this, EventArgs.Empty); // same code as clicking Start Quiz
                    DisplayBotResponse("Sure! I've opened the cybersecurity quiz and started it for you. Good luck!");
                    return true;

                case UserIntent.ShowTasks:
                    LogActivity("Chat command understood: show tasks");
                    DisplayBotResponse(BuildTaskSummary());
                    return true;

                case UserIntent.ShowActivityLog:
                    DisplayBotResponse(BuildActivitySummary(result.ShowAll));
                    return true;

                default:
                    return false; // not a Part 3 command
            }
        }

        // Saves a task from a chat message, then either sets the reminder or asks about one.
        private string AddTaskFromChat(IntentResult result)
        {
            if (result.TaskTitle == "")
            {
                return "I can see you'd like to add a task or reminder, but I need the details. " +
                       "Try: Add a task to enable two-factor authentication.";
            }

            string description = "Created from chat: \"" + result.OriginalText + "\"";
            if (description.Length > 500)
                description = description.Substring(0, 500); // matches the MySQL column

            CyberTask task = new CyberTask
            {
                Title = result.TaskTitle,
                Description = description,
                ReminderDate = result.ReminderDate,
                IsCompleted = false
            };

            try
            {
                int newId = _taskRepository.AddTask(task);
                LogActivity("Task added through chat: " + task.Title);
                LoadTasks(); // keep the Tasks tab in sync with MySQL

                if (task.ReminderDate.HasValue)
                {
                    LogActivity("Reminder set: " + task.Title + " on " + task.ReminderDate.Value.ToString("dd MMM yyyy"));
                    return $"Reminder set for '{task.Title}' on {task.ReminderDate.Value:dddd, dd MMM yyyy}.";
                }

                // No date given: remember this task and ask about a reminder.
                _pendingReminderTaskId = newId;
                _pendingReminderTitle = task.Title;
                return $"Task added: '{task.Title}'. Would you like a reminder? " +
                       "You can say something like 'yes, in 3 days' or 'no'.";
            }
            catch (Exception ex)
            {
                return "Sorry, I couldn't save that task: " + ex.Message;
            }
        }

        // Handles the user's reply to "Would you like a reminder?" Returns false if it wasn't an answer.
        private bool TryAnswerPendingReminder(string message)
        {
            DateTime? date = _nlp.ExtractReminderDate(message);

            if (date.HasValue)
            {
                try
                {
                    _taskRepository.UpdateReminder(_pendingReminderTaskId, date.Value);
                    LogActivity("Reminder set: " + _pendingReminderTitle + " on " + date.Value.ToString("dd MMM yyyy"));
                    LoadTasks();
                    DisplayBotResponse($"Got it! I'll remind you about '{_pendingReminderTitle}' on {date.Value:dddd, dd MMM yyyy}.");
                }
                catch (Exception ex)
                {
                    DisplayBotResponse("Sorry, I couldn't set that reminder: " + ex.Message);
                }

                _pendingReminderTaskId = 0;
                _pendingReminderTitle = "";
                return true;
            }

            if (_nlp.IsNo(message))
            {
                DisplayBotResponse("No problem. The task is saved without a reminder.");
                _pendingReminderTaskId = 0;
                _pendingReminderTitle = "";
                return true;
            }

            if (_nlp.IsYes(message))
            {
                DisplayBotResponse("When should I remind you? For example: tomorrow, or in 3 days.");
                return true; // still waiting for the date
            }

            return false;
        }

        // A short list of the user's pending tasks, shown in the chat.
        private string BuildTaskSummary()
        {
            try
            {
                List<CyberTask> all = _taskRepository.GetAllTasks();
                List<CyberTask> pending = all.Where(t => !t.IsCompleted).ToList();

                if (all.Count == 0)
                    return "You don't have any tasks yet. Try: Add a task to enable two-factor authentication.";

                if (pending.Count == 0)
                    return $"All {all.Count} of your tasks are completed. Well done!";

                List<string> lines = new List<string>();
                for (int i = 0; i < pending.Count && i < 5; i++)
                {
                    string line = $"{i + 1}. {pending[i].Title}";
                    if (pending[i].ReminderDate.HasValue)
                        line += $" (reminder: {pending[i].ReminderDate.Value:dd MMM yyyy})";
                    lines.Add(line);
                }

                string more = pending.Count > 5 ? $"\n...and {pending.Count - 5} more in the Tasks tab." : "";
                return $"You have {pending.Count} pending task(s):\n" + string.Join("\n", lines) + more +
                       "\nUse the Tasks tab to complete or delete them.";
            }
            catch (Exception ex)
            {
                return "Sorry, I couldn't load your tasks: " + ex.Message;
            }
        }

        // The recent actions as a numbered list in the chat (5 normally, 10 for "show more").
        private string BuildActivitySummary(bool showAll)
        {
            int count = showAll ? 10 : 5;
            List<ActivityEntry> recent = _activityLogger.GetRecent(count);

            // Fetch the list BEFORE logging this request, so the list shows earlier actions.
            LogActivity("Chat command understood: show activity log");

            if (recent.Count == 0)
                return "I haven't done anything for you yet. Try adding a task or starting the quiz!";

            List<string> lines = new List<string>();
            for (int i = 0; i < recent.Count; i++)
            {
                lines.Add($"{i + 1}. {recent[i].Description} ({recent[i].Time:HH:mm})");
            }

            string hint = (!showAll && _activityLogger.TotalCount > 6)
                ? "\nSay 'show more activity' to see up to 10 actions."
                : "";

            return "Here's a summary of recent actions:\n" + string.Join("\n", lines) + hint;
        }
    }
}