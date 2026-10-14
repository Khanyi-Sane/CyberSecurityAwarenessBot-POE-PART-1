namespace CybersecurityChatbotGUI
{
    public partial class ChatbotForm : Form
    {
        private Chatbot chatBot = new Chatbot();

        public ChatbotForm()
        {
            InitializeComponent(); // builds the controls you dragged on. Never delete this.

            // THE DELEGATE: point it at the method that displays messages.
            chatBot.MessageHandler = DisplayBotResponse;

            // Connect the events (instead of using the lightning bolt).
            this.Load += ChatbotForm_Load;
            btnSend.Click += btnSend_Click;
            txtUserInput.KeyDown += txtUserInput_KeyDown;
        }

        private void ChatbotForm_Load(object? sender, EventArgs e)
        {
            rtbBanner.Text = chatBot.Logo;
            chatBot.Start();
        }

        private void DisplayBotResponse(string message)
        {
            rtbChatHistory.AppendText("Bot > " + message + Environment.NewLine + Environment.NewLine);
            rtbChatHistory.ScrollToCaret(); // keeps the newest message visible
        }

        private void btnSend_Click(object? sender, EventArgs e)
        {
            SubmitMessage();
        }

        private void txtUserInput_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // stops the "ding"
                SubmitMessage();
            }
        }

        private void SubmitMessage()
        {
            string userMessage = txtUserInput.Text.Trim();

            if (userMessage != "")
            {
                rtbChatHistory.AppendText("You > " + userMessage + Environment.NewLine + Environment.NewLine);
            }

            chatBot.ProcessInput(userMessage);
            txtUserInput.Clear();
            txtUserInput.Focus();
        }
    }
}
