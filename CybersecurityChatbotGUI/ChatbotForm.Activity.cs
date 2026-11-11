using CybersecurityChatbotGUI.Models;
using CybersecurityChatbotGUI.Services;

namespace CybersecurityChatbotGUI
{
    // The Activity Log tab. Another piece of the ChatbotForm partial class.
    public partial class ChatbotForm
    {
        private readonly ActivityLogger _activityLogger = new ActivityLogger();

        // Controls on the Activity Log tab.
        private readonly ListBox lstActivity = new ListBox();
        private readonly Button btnRefreshActivity = new Button();
        private readonly Button btnShowMore = new Button();
        private readonly Label lblActivityCount = new Label();

        // The log shows 5 actions at first; "Show More" adds 5 each time.
        private const int ActivityPageSize = 5;
        private int _activityToShow = ActivityPageSize;

        // Builds the whole Activity Log tab in code and connects its events.
        private void BuildActivityTab()
        {
            // ---------- Top panel: heading and "showing X of Y" ----------
            Panel pnlActivityHeader = new Panel();
            pnlActivityHeader.Dock = DockStyle.Top;
            pnlActivityHeader.Height = 70;
            pnlActivityHeader.BackColor = _backgroundColour;

            pnlActivityHeader.Controls.Add(CreateLabel("Recent chatbot activity", 20, 10, true));

            lblActivityCount.Location = new Point(20, 40);
            lblActivityCount.AutoSize = true;
            lblActivityCount.ForeColor = _textColour;
            lblActivityCount.Font = new Font("Segoe UI", 10);
            pnlActivityHeader.Controls.Add(lblActivityCount);

            // ---------- Bottom panel: the two buttons ----------
            Panel pnlActivityButtons = new Panel();
            pnlActivityButtons.Dock = DockStyle.Bottom;
            pnlActivityButtons.Height = 70;
            pnlActivityButtons.BackColor = _backgroundColour;

            StyleButton(btnShowMore, "Show More", 20, 15, 160);
            StyleButton(btnRefreshActivity, "Refresh", 195, 15, 160);
            pnlActivityButtons.Controls.Add(btnShowMore);
            pnlActivityButtons.Controls.Add(btnRefreshActivity);

            // ---------- The list of actions ----------
            lstActivity.Dock = DockStyle.Fill;
            lstActivity.BackColor = _panelColour;
            lstActivity.ForeColor = _textColour;
            lstActivity.Font = new Font("Segoe UI", 11);
            lstActivity.BorderStyle = BorderStyle.None;
            lstActivity.IntegralHeight = false;

            // Fill control first, so the Top and Bottom panels are laid out before it.
            tabActivity.Controls.Add(lstActivity);
            tabActivity.Controls.Add(pnlActivityHeader);
            tabActivity.Controls.Add(pnlActivityButtons);

            // ---------- Events ----------
            btnShowMore.Click += btnShowMore_Click;
            btnRefreshActivity.Click += btnRefreshActivity_Click;

            DisplayRecentActivity(); // starts as "No activity recorded yet."
        }

        // Records an action AND refreshes the list. Every feature calls this one method.
        private void LogActivity(string description)
        {
            _activityLogger.Log(description);
            DisplayRecentActivity();
        }

        // Shows the newest actions in the list box.
        private void DisplayRecentActivity()
        {
            lstActivity.Items.Clear();

            List<ActivityEntry> recent = _activityLogger.GetRecent(_activityToShow);

            if (recent.Count == 0)
            {
                lstActivity.Items.Add("No activity recorded yet.");
                lblActivityCount.Text = "";
                btnShowMore.Enabled = false;
                return;
            }

            foreach (ActivityEntry entry in recent)
            {
                lstActivity.Items.Add(entry.ToString());
            }

            lblActivityCount.Text = $"Showing {recent.Count} of {_activityLogger.TotalCount} actions";

            // Only allow "Show More" when there is more to show.
            btnShowMore.Enabled = recent.Count < _activityLogger.TotalCount;
        }

        private void btnShowMore_Click(object? sender, EventArgs e)
        {
            _activityToShow += ActivityPageSize;
            DisplayRecentActivity();
        }

        private void btnRefreshActivity_Click(object? sender, EventArgs e)
        {
            _activityToShow = ActivityPageSize; // go back to the 5 most recent
            DisplayRecentActivity();
        }
    }
}