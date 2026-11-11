namespace CybersecurityChatbotGUI
{
    partial class ChatbotForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            tabMain = new TabControl();
            tabChat = new TabPage();
            tabTasks = new TabPage();
            tabQuiz = new TabPage();
            tabActivity = new TabPage();
            pnlInput = new Panel();
            rtbBanner = new RichTextBox();
            rtbChatHistory = new RichTextBox();
            txtUserInput = new TextBox();
            btnSend = new Button();
            tabMain.SuspendLayout();
            tabChat.SuspendLayout();
            pnlInput.SuspendLayout();
            SuspendLayout();
            //
            // tabMain: holds the four feature tabs and fills the whole window
            //
            tabMain.Controls.Add(tabChat);
            tabMain.Controls.Add(tabTasks);
            tabMain.Controls.Add(tabQuiz);
            tabMain.Controls.Add(tabActivity);
            tabMain.Dock = DockStyle.Fill;
            tabMain.Location = new Point(0, 0);
            tabMain.Name = "tabMain";
            tabMain.SelectedIndex = 0;
            tabMain.Size = new Size(1374, 859);
            tabMain.TabIndex = 0;
            //
            // tabChat: the existing Part 1 and Part 2 chatbot lives here
            //
            tabChat.Controls.Add(rtbChatHistory);
            tabChat.Controls.Add(rtbBanner);
            tabChat.Controls.Add(pnlInput);
            tabChat.Location = new Point(4, 29);
            tabChat.Name = "tabChat";
            tabChat.Size = new Size(1366, 826);
            tabChat.TabIndex = 0;
            tabChat.Text = "Chat";
            tabChat.UseVisualStyleBackColor = false;
            //
            // tabTasks: empty for now (Task Assistant comes next)
            //
            tabTasks.Location = new Point(4, 29);
            tabTasks.Name = "tabTasks";
            tabTasks.Size = new Size(1366, 826);
            tabTasks.TabIndex = 1;
            tabTasks.Text = "Tasks";
            tabTasks.UseVisualStyleBackColor = false;
            //
            // tabQuiz: empty for now
            //
            tabQuiz.Location = new Point(4, 29);
            tabQuiz.Name = "tabQuiz";
            tabQuiz.Size = new Size(1366, 826);
            tabQuiz.TabIndex = 2;
            tabQuiz.Text = "Cyber Quiz";
            tabQuiz.UseVisualStyleBackColor = false;
            //
            // tabActivity: empty for now
            //
            tabActivity.Location = new Point(4, 29);
            tabActivity.Name = "tabActivity";
            tabActivity.Size = new Size(1366, 826);
            tabActivity.TabIndex = 3;
            tabActivity.Text = "Activity Log";
            tabActivity.UseVisualStyleBackColor = false;
            //
            // pnlInput: bottom strip holding the text box and Send button
            //
            pnlInput.Controls.Add(txtUserInput);
            pnlInput.Controls.Add(btnSend);
            pnlInput.Dock = DockStyle.Bottom;
            pnlInput.Location = new Point(0, 756);
            pnlInput.Name = "pnlInput";
            pnlInput.Size = new Size(1366, 70);
            pnlInput.TabIndex = 2;
            //
            // rtbBanner: ASCII art logo along the top
            //
            rtbBanner.BorderStyle = BorderStyle.None;
            rtbBanner.Dock = DockStyle.Top;
            rtbBanner.Font = new Font("Consolas", 10F, FontStyle.Bold);
            rtbBanner.Location = new Point(0, 0);
            rtbBanner.Name = "rtbBanner";
            rtbBanner.ReadOnly = true;
            rtbBanner.Size = new Size(1366, 173);
            rtbBanner.TabIndex = 0;
            rtbBanner.Text = "";
            rtbBanner.WordWrap = false;
            //
            // rtbChatHistory: fills the space between banner and input strip
            //
            rtbChatHistory.Dock = DockStyle.Fill;
            rtbChatHistory.Location = new Point(0, 173);
            rtbChatHistory.Name = "rtbChatHistory";
            rtbChatHistory.ReadOnly = true;
            rtbChatHistory.Size = new Size(1366, 583);
            rtbChatHistory.TabIndex = 1;
            rtbChatHistory.Text = "";
            //
            // txtUserInput
            //
            txtUserInput.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtUserInput.Location = new Point(20, 22);
            txtUserInput.Name = "txtUserInput";
            txtUserInput.Size = new Size(1130, 27);
            txtUserInput.TabIndex = 0;
            //
            // btnSend
            //
            btnSend.Anchor = AnchorStyles.Right;
            btnSend.Location = new Point(1175, 11);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(165, 48);
            btnSend.TabIndex = 1;
            btnSend.Text = "Send";
            btnSend.UseVisualStyleBackColor = true;
            //
            // ChatbotForm
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1374, 859);
            Controls.Add(tabMain);
            Name = "ChatbotForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Cybersecurity Awareness Chatbot";
            tabMain.ResumeLayout(false);
            tabChat.ResumeLayout(false);
            pnlInput.ResumeLayout(false);
            pnlInput.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabMain;
        private TabPage tabChat;
        private TabPage tabTasks;
        private TabPage tabQuiz;
        private TabPage tabActivity;
        private Panel pnlInput;
        private RichTextBox rtbBanner;
        private RichTextBox rtbChatHistory;
        private TextBox txtUserInput;
        private Button btnSend;
    }
}