using CybersecurityChatbotGUI.Models;

namespace CybersecurityChatbotGUI.Services
{
    // Holds the questions and keeps track of the score and which question we're on.
    public class QuizManager
    {
        private readonly List<QuizQuestion> _questions;

        public int CurrentIndex { get; private set; }
        public int Score { get; private set; }
        public bool QuizStarted { get; private set; }

        public int TotalQuestions => _questions.Count;
        public QuizQuestion CurrentQuestion => _questions[CurrentIndex];
        public bool IsLastQuestion => CurrentIndex == _questions.Count - 1;

        public QuizManager()
        {
            _questions = BuildQuestions();
        }

        // Resets everything so the quiz can be played again.
        public void StartQuiz()
        {
            CurrentIndex = 0;
            Score = 0;
            QuizStarted = true;
        }

        // Checks the chosen option, updates the score and builds the feedback text.
        public QuizAnswerResult CheckAnswer(int selectedIndex)
        {
            bool correct = selectedIndex == CurrentQuestion.CorrectOptionIndex;

            if (correct)
                Score++;

            return new QuizAnswerResult
            {
                IsCorrect = correct,
                Feedback = (correct ? "Correct! " : "Not quite. ") + CurrentQuestion.Explanation
            };
        }

        // Moves to the next question. Returns false when there are no more.
        public bool MoveNext()
        {
            if (CurrentIndex < _questions.Count - 1)
            {
                CurrentIndex++;
                return true;
            }

            QuizStarted = false;
            return false;
        }

        // Gives an encouraging comment based on the final percentage.
        public string GetFinalComment()
        {
            double percentage = (double)Score / TotalQuestions * 100;

            if (percentage >= 80)
                return "Great job! You're a cybersecurity pro!";
            if (percentage >= 60)
                return "Good work! Review the topics you missed and try again.";
            return "Keep learning to stay safe online! Check the chatbot's tips and try again.";
        }

        // All 12 questions live here: a mix of multiple choice and true/false.
        private List<QuizQuestion> BuildQuestions()
        {
            return new List<QuizQuestion>
            {
                new QuizQuestion
                {
                    QuestionText = "An email says your bank account is locked and asks you to click a link and enter your PIN. What should you do?",
                    Options = new List<string>
                    {
                        "Click the link and enter your PIN quickly",
                        "Contact your bank using the number on its official website or your card",
                        "Reply to the email to ask if it is real",
                        "Forward it to your friends as a warning"
                    },
                    CorrectOptionIndex = 1,
                    Explanation = "Banks never ask for your PIN by email. Always contact them through details you already trust."
                },
                new QuizQuestion
                {
                    QuestionText = "Which of these is the strongest password?",
                    Options = new List<string>
                    {
                        "Password123",
                        "yourname1990",
                        "Tr!ver-Moss-Lantern-84",
                        "qwerty"
                    },
                    CorrectOptionIndex = 2,
                    Explanation = "Long, unpredictable passwords with a mix of characters are much harder to guess or crack."
                },
                new QuizQuestion
                {
                    QuestionText = "True or False: Using the same password on all your accounts is fine as long as it is long.",
                    Options = new List<string> { "True", "False" },
                    CorrectOptionIndex = 1,
                    Explanation = "If one site is breached, attackers try that password everywhere else. Use a unique password for each account."
                },
                new QuizQuestion
                {
                    QuestionText = "What does two-factor authentication (2FA) add to your login?",
                    Options = new List<string>
                    {
                        "A second proof of identity beyond your password",
                        "A longer password",
                        "A faster way to log in",
                        "An automatic password reset"
                    },
                    CorrectOptionIndex = 0,
                    Explanation = "Even if someone steals your password, they still need your second factor, such as a code on your phone."
                },
                new QuizQuestion
                {
                    QuestionText = "A caller says they are from IT support and asks for your password to fix a problem. What is this?",
                    Options = new List<string>
                    {
                        "Routine maintenance",
                        "Social engineering",
                        "A software update",
                        "Data encryption"
                    },
                    CorrectOptionIndex = 1,
                    Explanation = "Social engineering tricks people into giving away information. Real IT staff should never need your password."
                },
                new QuizQuestion
                {
                    QuestionText = "What does the padlock and https in your browser's address bar tell you?",
                    Options = new List<string>
                    {
                        "The website is guaranteed to be safe",
                        "The connection is encrypted, but the site itself could still be malicious",
                        "The website is run by the government",
                        "Nothing at all"
                    },
                    CorrectOptionIndex = 1,
                    Explanation = "Encryption protects data in transit, but scam sites can use https too, so still check the address carefully."
                },
                new QuizQuestion
                {
                    QuestionText = "True or False: It is safe to do your online banking on free public Wi-Fi.",
                    Options = new List<string> { "True", "False" },
                    CorrectOptionIndex = 1,
                    Explanation = "Public networks can be monitored or faked. Use your mobile data or a trusted network for banking."
                },
                new QuizQuestion
                {
                    QuestionText = "Which email attachment deserves the most suspicion?",
                    Options = new List<string>
                    {
                        "A PDF report you were expecting from a colleague",
                        "An unexpected .exe file from someone you don't know",
                        "A photo from a family member you were chatting with",
                        "A file you attached to the email yourself"
                    },
                    CorrectOptionIndex = 1,
                    Explanation = "Unexpected executable files are a common way to install malware on your device."
                },
                new QuizQuestion
                {
                    QuestionText = "How can a password manager help you?",
                    Options = new List<string>
                    {
                        "It creates and stores unique passwords for you",
                        "It publishes your passwords online",
                        "It removes the need for any security",
                        "It makes every password the same"
                    },
                    CorrectOptionIndex = 0,
                    Explanation = "A trusted password manager makes unique, strong passwords easy because you only remember one master password."
                },
                new QuizQuestion
                {
                    QuestionText = "Why is it important to install software updates?",
                    Options = new List<string>
                    {
                        "They only change the colours of the app",
                        "They fix known security weaknesses",
                        "They delete your old files",
                        "They make phishing impossible"
                    },
                    CorrectOptionIndex = 1,
                    Explanation = "Updates patch vulnerabilities that attackers already know about and actively try to exploit."
                },
                new QuizQuestion
                {
                    QuestionText = "True or False: A message can be a scam even if it looks like it comes from a company you know.",
                    Options = new List<string> { "True", "False" },
                    CorrectOptionIndex = 0,
                    Explanation = "Scammers copy logos and email addresses. Check the sender and the link before you trust a message."
                },
                new QuizQuestion
                {
                    QuestionText = "What is the best protection against losing your files to ransomware?",
                    Options = new List<string>
                    {
                        "Regular backups kept separate from your computer",
                        "Paying the ransom as quickly as possible",
                        "Ignoring security warnings",
                        "Turning off your antivirus"
                    },
                    CorrectOptionIndex = 0,
                    Explanation = "With a recent offline backup you can restore your files without paying criminals."
                }
            };
        }
    }
}