using CybersecurityChatbotGUI.Models;
using CybersecurityChatbotGUI.Services;

namespace CybersecurityChatbotGUI
{
    // The Cyber Quiz tab. Another piece of the ChatbotForm partial class.
    public partial class ChatbotForm
    {
        private readonly QuizManager _quizManager = new QuizManager();

        // Controls on the Cyber Quiz tab.
        private readonly Label lblQuestionNumber = new Label();
        private readonly Label lblQuestion = new Label();
        private readonly RadioButton rdoA = new RadioButton();
        private readonly RadioButton rdoB = new RadioButton();
        private readonly RadioButton rdoC = new RadioButton();
        private readonly RadioButton rdoD = new RadioButton();
        private readonly Button btnStartQuiz = new Button();
        private readonly Button btnSubmitAnswer = new Button();
        private readonly Button btnNextQuestion = new Button();
        private readonly Label lblQuizFeedback = new Label();
        private readonly Label lblQuizScore = new Label();

        // Builds the whole quiz tab in code and connects its events.
        private void BuildQuizTab()
        {
            tabQuiz.Controls.Add(CreateLabel("Cybersecurity Quiz", 20, 15, true));

            lblQuestionNumber.Location = new Point(20, 55);
            lblQuestionNumber.AutoSize = true;
            lblQuestionNumber.ForeColor = _userColour;
            lblQuestionNumber.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            tabQuiz.Controls.Add(lblQuestionNumber);

            // AutoSize off + a fixed size makes long questions wrap onto several lines.
            lblQuestion.Location = new Point(20, 85);
            lblQuestion.Size = new Size(1000, 65);
            lblQuestion.AutoSize = false;
            lblQuestion.ForeColor = _textColour;
            lblQuestion.Font = new Font("Segoe UI", 12);
            tabQuiz.Controls.Add(lblQuestion);

            // The four answer options, stacked below the question.
            RadioButton[] options = { rdoA, rdoB, rdoC, rdoD };
            int top = 160;
            foreach (RadioButton option in options)
            {
                option.Location = new Point(40, top);
                option.AutoSize = true;
                option.ForeColor = _textColour;
                option.Font = new Font("Segoe UI", 11);
                option.Visible = false;
                tabQuiz.Controls.Add(option);
                top += 40;
            }

            StyleButton(btnStartQuiz, "Start Quiz", 20, 335, 160);
            StyleButton(btnSubmitAnswer, "Submit Answer", 195, 335, 160);
            StyleButton(btnNextQuestion, "Next Question", 370, 335, 160);
            tabQuiz.Controls.Add(btnStartQuiz);
            tabQuiz.Controls.Add(btnSubmitAnswer);
            tabQuiz.Controls.Add(btnNextQuestion);

            lblQuizFeedback.Location = new Point(20, 395);
            lblQuizFeedback.Size = new Size(1000, 90);
            lblQuizFeedback.AutoSize = false;
            lblQuizFeedback.ForeColor = _textColour;
            lblQuizFeedback.Font = new Font("Segoe UI", 11);
            tabQuiz.Controls.Add(lblQuizFeedback);

            lblQuizScore.Location = new Point(20, 495);
            lblQuizScore.AutoSize = true;
            lblQuizScore.ForeColor = _botColour;
            lblQuizScore.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            tabQuiz.Controls.Add(lblQuizScore);

            // Events
            btnStartQuiz.Click += btnStartQuiz_Click;
            btnSubmitAnswer.Click += btnSubmitAnswer_Click;
            btnNextQuestion.Click += btnNextQuestion_Click;

            ShowQuizWelcome();
        }

        // What the tab looks like before the quiz has started.
        private void ShowQuizWelcome()
        {
            lblQuestionNumber.Text = "";
            lblQuestion.Text = $"Test your cybersecurity knowledge with {_quizManager.TotalQuestions} questions. " +
                               "Press Start Quiz when you are ready.";
            lblQuizFeedback.Text = "";
            lblQuizScore.Text = "";
            btnSubmitAnswer.Enabled = false;
            btnNextQuestion.Enabled = false;
        }

        // Shows the current question and its options (true/false only needs two radio buttons).
        private void DisplayCurrentQuestion()
        {
            QuizQuestion question = _quizManager.CurrentQuestion;

            lblQuestionNumber.Text = $"Question {_quizManager.CurrentIndex + 1} of {_quizManager.TotalQuestions}";
            lblQuestion.Text = question.QuestionText;

            RadioButton[] buttons = { rdoA, rdoB, rdoC, rdoD };

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].Checked = false;
                buttons[i].Enabled = true;

                if (i < question.Options.Count)
                {
                    buttons[i].Text = question.Options[i];
                    buttons[i].Visible = true;
                }
                else
                {
                    buttons[i].Visible = false; // hide unused options
                }
            }

            lblQuizScore.Text = $"Score: {_quizManager.Score}/{_quizManager.TotalQuestions}";
        }

        // Works out which radio button the user ticked (-1 means none).
        private int GetSelectedQuizOption()
        {
            if (rdoA.Checked) return 0;
            if (rdoB.Checked) return 1;
            if (rdoC.Checked) return 2;
            if (rdoD.Checked) return 3;
            return -1;
        }

        private void btnStartQuiz_Click(object? sender, EventArgs e)
        {
            _quizManager.StartQuiz();
            LogActivity("Cybersecurity quiz started");

            lblQuizFeedback.Text = "";
            btnStartQuiz.Text = "Restart Quiz";
            btnSubmitAnswer.Enabled = true;
            btnNextQuestion.Enabled = false;
            btnNextQuestion.Text = "Next Question";

            DisplayCurrentQuestion();
        }

        private void btnSubmitAnswer_Click(object? sender, EventArgs e)
        {
            int selectedIndex = GetSelectedQuizOption();

            if (selectedIndex == -1)
            {
                lblQuizFeedback.ForeColor = Color.IndianRed;
                lblQuizFeedback.Text = "Please select an answer first.";
                return;
            }

            QuizAnswerResult result = _quizManager.CheckAnswer(selectedIndex);

            lblQuizFeedback.ForeColor = result.IsCorrect ? _botColour : Color.IndianRed;
            lblQuizFeedback.Text = result.Feedback;
            lblQuizScore.Text = $"Score: {_quizManager.Score}/{_quizManager.TotalQuestions}";

            // Lock the answer in so the score can only go up once per question.
            btnSubmitAnswer.Enabled = false;
            rdoA.Enabled = rdoB.Enabled = rdoC.Enabled = rdoD.Enabled = false;

            btnNextQuestion.Text = _quizManager.IsLastQuestion ? "Finish Quiz" : "Next Question";
            btnNextQuestion.Enabled = true;
        }

        private void btnNextQuestion_Click(object? sender, EventArgs e)
        {
            bool hasAnotherQuestion = _quizManager.MoveNext();

            if (hasAnotherQuestion)
            {
                lblQuizFeedback.Text = "";
                btnSubmitAnswer.Enabled = true;
                btnNextQuestion.Enabled = false;
                DisplayCurrentQuestion();
                return;
            }

            // The quiz is finished: show the final score and a comment.
            lblQuestionNumber.Text = "Quiz complete!";
            lblQuestion.Text = $"You scored {_quizManager.Score} out of {_quizManager.TotalQuestions}.";
            rdoA.Visible = rdoB.Visible = rdoC.Visible = rdoD.Visible = false;

            lblQuizFeedback.ForeColor = _botColour;
            lblQuizFeedback.Text = _quizManager.GetFinalComment();
            lblQuizScore.Text = $"Final score: {_quizManager.Score}/{_quizManager.TotalQuestions}";

            LogActivity($"Quiz completed - score {_quizManager.Score}/{_quizManager.TotalQuestions}");

            btnSubmitAnswer.Enabled = false;
            btnNextQuestion.Enabled = false;
            btnStartQuiz.Text = "Play Again";
        }
    }
}