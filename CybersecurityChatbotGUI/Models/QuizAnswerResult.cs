namespace CybersecurityChatbotGUI.Models
{
    // What the quiz tells the form after an answer is checked.
    public class QuizAnswerResult
    {
        public bool IsCorrect { get; set; }
        public string Feedback { get; set; } = "";
    }
}