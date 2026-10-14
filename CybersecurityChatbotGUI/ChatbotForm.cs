namespace CybersecurityChatbotGUI
{
    public partial class ChatbotForm : Form
    {
        private Chatbot chatBot = new Chatbot();

        // The colour palette lives in one place so it's easy to change later.
        private readonly Color _backgroundColour = Color.FromArgb(30, 30, 46);
        private readonly Color _panelColour = Color.FromArgb(24, 24, 37);
        private readonly Color _inputColour = Color.FromArgb(49, 50, 68);
        private readonly Color _textColour = Color.FromArgb(230, 230, 240);
        private readonly Color _botColour = Color.FromArgb(0, 230, 180);   // teal-green
        private readonly Color _userColour = Color.FromArgb(255, 200, 80); // warm yellow
        private readonly Color _bannerColour = Color.Cyan;

        public ChatbotForm()
        {
            InitializeComponent(); // builds the controls you dragged on. Never delete this.

            ApplyTheme();

            // THE DELEGATE: point it at the method that displays messages.
            chatBot.MessageHandler = DisplayBotResponse;

            // Connect the events (instead of using the lightning bolt).
            this.Load += ChatbotForm_Load;
            btnSend.Click += btnSend_Click;
            txtUserInput.KeyDown += txtUserInput_KeyDown;
        }

        // Sets colours, fonts and styles for every control.
        private void ApplyTheme()
        {
            this.BackColor = _backgroundColour;

            // Banner: monospaced font so the ASCII art lines up.
            rtbBanner.BackColor = _backgroundColour;
            rtbBanner.ForeColor = _bannerColour;
            rtbBanner.Font = new Font("Consolas", 10);
            rtbBanner.WordWrap = false;
            rtbBanner.ReadOnly = true;
            rtbBanner.BorderStyle = BorderStyle.None;

            // Chat area
            rtbChatHistory.BackColor = _panelColour;
            rtbChatHistory.ForeColor = _textColour;
            rtbChatHistory.Font = new Font("Segoe UI", 10);
            rtbChatHistory.ReadOnly = true;
            rtbChatHistory.BorderStyle = BorderStyle.None;

            // Input box
            txtUserInput.BackColor = _inputColour;
            txtUserInput.ForeColor = _textColour;
            txtUserInput.Font = new Font("Segoe UI", 10);
            txtUserInput.BorderStyle = BorderStyle.FixedSingle;

            // Send button
            btnSend.BackColor = _botColour;
            btnSend.ForeColor = Color.FromArgb(20, 20, 30);
            btnSend.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnSend.FlatStyle = FlatStyle.Flat;
            btnSend.FlatAppearance.BorderSize = 0;
        }

        private void ChatbotForm_Load(object? sender, EventArgs e)
        {
            rtbBanner.Text = chatBot.Logo;
            chatBot.Start();
        }

        // Adds text to the chat in a chosen colour, then resets the colour.
        private void AppendColoured(string text, Color colour)
        {
            rtbChatHistory.SelectionStart = rtbChatHistory.TextLength; // jump to the end
            rtbChatHistory.SelectionLength = 0;
            rtbChatHistory.SelectionColor = colour;
            rtbChatHistory.AppendText(text);
            rtbChatHistory.SelectionColor = rtbChatHistory.ForeColor;  // reset
        }

        // The method the delegate points to.
        private void DisplayBotResponse(string message)
        {
            AppendColoured("Bot > ", _botColour);
            AppendColoured(message + Environment.NewLine + Environment.NewLine, _textColour);
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

            // Only show the user's message if they typed something.
            if (userMessage != "")
            {
                AppendColoured("You > ", _userColour);
                AppendColoured(userMessage + Environment.NewLine + Environment.NewLine, _textColour);
            }

            chatBot.ProcessInput(userMessage);
            txtUserInput.Clear();
            txtUserInput.Focus();
        }
    }
}