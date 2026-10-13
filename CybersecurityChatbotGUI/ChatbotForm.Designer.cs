namespace CybersecurityChatbotGUI
{
    partial class ChatbotForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            rtbBanner = new RichTextBox();
            rtbChatHistory = new RichTextBox();
            SuspendLayout();
            // 
            // rtbBanner
            // 
            rtbBanner.BorderStyle = BorderStyle.None;
            rtbBanner.Dock = DockStyle.Top;
            rtbBanner.Font = new Font("Consolas", 10F, FontStyle.Bold);
            rtbBanner.Location = new Point(0, 0);
            rtbBanner.Name = "rtbBanner";
            rtbBanner.ReadOnly = true;
            rtbBanner.Size = new Size(1374, 149);
            rtbBanner.TabIndex = 0;
            rtbBanner.Text = "";
            rtbBanner.WordWrap = false;
            // 
            // rtbChatHistory
            // 
            rtbChatHistory.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            rtbChatHistory.Location = new Point(12, 155);
            rtbChatHistory.Name = "rtbChatHistory";
            rtbChatHistory.ReadOnly = true;
            rtbChatHistory.Size = new Size(1362, 490);
            rtbChatHistory.TabIndex = 1;
            rtbChatHistory.Text = "";
            // 
            // ChatbotForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1374, 859);
            Controls.Add(rtbChatHistory);
            Controls.Add(rtbBanner);
            Name = "ChatbotForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Cybersecurity Awareness Chatbot";
            UseWaitCursor = true;
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox rtbBanner;
        private RichTextBox rtbChatHistory;
    }
}
