namespace CybersecurityChatbotGUI.Models
{
    // One quiz question with its answer options and the explanation shown afterwards.
    public class QuizQuestion
    {
        public string QuestionText { get; set; } = "";

        // Multiple choice has 4 options; true/false has 2.
        public List<string> Options { get; set; } = new List<string>();

        // 0 = first option, 1 = second option, and so on.
        public int CorrectOptionIndex { get; set; }

        public string Explanation { get; set; } = "";
    }
}