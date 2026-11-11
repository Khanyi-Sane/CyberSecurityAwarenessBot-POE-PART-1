using CybersecurityChatbotGUI.Data;
using CybersecurityChatbotGUI.Models;

namespace CybersecurityChatbotGUI
{
    // The Task Assistant tab. This is the second half of ChatbotForm (a partial class),
    // so it can use the same colours and controls as the rest of the form.
    public partial class ChatbotForm
    {
        // The repository is the only thing that talks to MySQL.
        private readonly TaskRepository _taskRepository = new TaskRepository();

        // Controls on the Tasks tab.
        private readonly TextBox txtTaskTitle = new TextBox();
        private readonly TextBox txtTaskDescription = new TextBox();
        private readonly CheckBox chkUseReminder = new CheckBox();
        private readonly DateTimePicker dtpReminder = new DateTimePicker();
        private readonly Button btnAddTask = new Button();
        private readonly DataGridView dgvTasks = new DataGridView();
        private readonly Button btnCompleteTask = new Button();
        private readonly Button btnDeleteTask = new Button();
        private readonly Button btnRefreshTasks = new Button();
        private readonly Label lblTaskStatus = new Label();

        // Builds the whole Tasks tab in code and connects its events.
        private void BuildTasksTab()
        {
            // ---------- Top panel: the "add a task" form ----------
            Panel pnlTaskForm = new Panel();
            pnlTaskForm.Dock = DockStyle.Top;
            pnlTaskForm.Height = 215;
            pnlTaskForm.BackColor = _backgroundColour;

            pnlTaskForm.Controls.Add(CreateLabel("Add a cybersecurity task", 20, 10, true));

            pnlTaskForm.Controls.Add(CreateLabel("Title", 20, 45, false));
            txtTaskTitle.Location = new Point(20, 70);
            txtTaskTitle.Size = new Size(500, 27);
            txtTaskTitle.MaxLength = 150; // matches the title column in MySQL
            StyleTextBox(txtTaskTitle);
            pnlTaskForm.Controls.Add(txtTaskTitle);

            pnlTaskForm.Controls.Add(CreateLabel("Description", 20, 105, false));
            txtTaskDescription.Location = new Point(20, 130);
            txtTaskDescription.Size = new Size(500, 60);
            txtTaskDescription.Multiline = true;
            txtTaskDescription.MaxLength = 500; // matches the description column in MySQL
            StyleTextBox(txtTaskDescription);
            pnlTaskForm.Controls.Add(txtTaskDescription);

            chkUseReminder.Text = "Set a reminder";
            chkUseReminder.Location = new Point(560, 70);
            chkUseReminder.AutoSize = true;
            chkUseReminder.ForeColor = _textColour;
            chkUseReminder.Font = new Font("Segoe UI", 10);
            pnlTaskForm.Controls.Add(chkUseReminder);

            dtpReminder.Location = new Point(560, 100);
            dtpReminder.Size = new Size(180, 27);
            dtpReminder.Format = DateTimePickerFormat.Short;
            dtpReminder.MinDate = DateTime.Today;               // no reminders in the past
            dtpReminder.Value = DateTime.Today.AddDays(1);      // default: tomorrow
            dtpReminder.Enabled = false;                        // only on when the box is ticked
            pnlTaskForm.Controls.Add(dtpReminder);

            StyleButton(btnAddTask, "Add Task", 560, 140, 180);
            pnlTaskForm.Controls.Add(btnAddTask);

            // ---------- Bottom panel: action buttons and status message ----------
            Panel pnlTaskButtons = new Panel();
            pnlTaskButtons.Dock = DockStyle.Bottom;
            pnlTaskButtons.Height = 70;
            pnlTaskButtons.BackColor = _backgroundColour;

            StyleButton(btnCompleteTask, "Mark Completed", 20, 15, 160);
            StyleButton(btnDeleteTask, "Delete Task", 195, 15, 160);
            StyleButton(btnRefreshTasks, "Refresh", 370, 15, 160);
            pnlTaskButtons.Controls.Add(btnCompleteTask);
            pnlTaskButtons.Controls.Add(btnDeleteTask);
            pnlTaskButtons.Controls.Add(btnRefreshTasks);

            lblTaskStatus.Location = new Point(550, 26);
            lblTaskStatus.AutoSize = true;
            lblTaskStatus.ForeColor = _textColour;
            lblTaskStatus.Font = new Font("Segoe UI", 10);
            pnlTaskButtons.Controls.Add(lblTaskStatus);

            // ---------- The grid that shows the tasks from MySQL ----------
            dgvTasks.Dock = DockStyle.Fill;
            dgvTasks.ReadOnly = true;
            dgvTasks.AllowUserToAddRows = false;
            dgvTasks.AllowUserToDeleteRows = false;
            dgvTasks.MultiSelect = false;
            dgvTasks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTasks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTasks.RowHeadersVisible = false;
            dgvTasks.BorderStyle = BorderStyle.None;
            dgvTasks.BackgroundColor = _panelColour;
            dgvTasks.GridColor = _inputColour;
            dgvTasks.EnableHeadersVisualStyles = false;
            dgvTasks.ColumnHeadersDefaultCellStyle.BackColor = _inputColour;
            dgvTasks.ColumnHeadersDefaultCellStyle.ForeColor = _textColour;
            dgvTasks.DefaultCellStyle.BackColor = _panelColour;
            dgvTasks.DefaultCellStyle.ForeColor = _textColour;
            dgvTasks.DefaultCellStyle.SelectionBackColor = _botColour;
            dgvTasks.DefaultCellStyle.SelectionForeColor = Color.FromArgb(20, 20, 30);

            // Fill control is added FIRST so WinForms lays out the Top and Bottom panels first.
            tabTasks.Controls.Add(dgvTasks);
            tabTasks.Controls.Add(pnlTaskForm);
            tabTasks.Controls.Add(pnlTaskButtons);

            // ---------- Events ----------
            chkUseReminder.CheckedChanged += (s, e) => dtpReminder.Enabled = chkUseReminder.Checked;
            btnAddTask.Click += btnAddTask_Click;
            btnCompleteTask.Click += btnCompleteTask_Click;
            btnDeleteTask.Click += btnDeleteTask_Click;
            btnRefreshTasks.Click += btnRefreshTasks_Click;
        }

        // ---------- Small helpers so the build code stays short ----------

        private Label CreateLabel(string text, int x, int y, bool isHeading)
        {
            Label label = new Label();
            label.Text = text;
            label.Location = new Point(x, y);
            label.AutoSize = true;
            label.ForeColor = isHeading ? _botColour : _textColour;
            label.Font = new Font("Segoe UI", isHeading ? 12 : 10, isHeading ? FontStyle.Bold : FontStyle.Regular);
            return label;
        }

        private void StyleTextBox(TextBox box)
        {
            box.BackColor = _inputColour;
            box.ForeColor = _textColour;
            box.Font = new Font("Segoe UI", 10);
            box.BorderStyle = BorderStyle.FixedSingle;
        }

        private void StyleButton(Button button, string text, int x, int y, int width)
        {
            button.Text = text;
            button.Location = new Point(x, y);
            button.Size = new Size(width, 40);
            button.BackColor = _botColour;
            button.ForeColor = Color.FromArgb(20, 20, 30);
            button.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
        }

        // Shows a friendly message under the grid: red for errors, normal colour otherwise.
        private void ShowTaskStatus(string message, bool isError)
        {
            lblTaskStatus.ForeColor = isError ? Color.IndianRed : _textColour;
            lblTaskStatus.Text = message;
        }

        // ---------- Loading and displaying tasks ----------

        // Reads every task from MySQL and shows it in the grid.
        private void LoadTasks()
        {
            try
            {
                List<CyberTask> tasks = _taskRepository.GetAllTasks();

                dgvTasks.DataSource = null;   // clear first so the grid rebinds properly
                dgvTasks.DataSource = tasks;
                FormatTaskGrid();
            }
            catch (Exception ex)
            {
                ShowTaskStatus("Could not load tasks: " + ex.Message, true);
            }
        }

        // Hides the database ID and gives the columns friendly headings.
        private void FormatTaskGrid()
        {
            if (dgvTasks.Columns["TaskId"] != null)
                dgvTasks.Columns["TaskId"]!.Visible = false;

            if (dgvTasks.Columns["Title"] != null)
                dgvTasks.Columns["Title"]!.HeaderText = "Title";

            if (dgvTasks.Columns["Description"] != null)
                dgvTasks.Columns["Description"]!.HeaderText = "Description";

            if (dgvTasks.Columns["ReminderDate"] != null)
            {
                dgvTasks.Columns["ReminderDate"]!.HeaderText = "Reminder";
                dgvTasks.Columns["ReminderDate"]!.DefaultCellStyle.Format = "dd MMM yyyy";
                dgvTasks.Columns["ReminderDate"]!.DefaultCellStyle.NullValue = "None";
            }

            if (dgvTasks.Columns["IsCompleted"] != null)
                dgvTasks.Columns["IsCompleted"]!.HeaderText = "Completed";
        }

        // Finds which task row the user has selected (or null if none).
        private CyberTask? GetSelectedTask()
        {
            if (dgvTasks.CurrentRow == null)
                return null;

            return dgvTasks.CurrentRow.DataBoundItem as CyberTask;
        }

        // ---------- Button events ----------

        private void btnAddTask_Click(object? sender, EventArgs e)
        {
            string title = txtTaskTitle.Text.Trim();
            string description = txtTaskDescription.Text.Trim();

            // Validation: both fields are required.
            if (title == "" || description == "")
            {
                ShowTaskStatus("Please enter both a task title and a description.", true);
                return;
            }

            CyberTask task = new CyberTask
            {
                Title = title,
                Description = description,
                // Only keep the date if the reminder box is ticked; otherwise null (stored as NULL).
                ReminderDate = chkUseReminder.Checked ? dtpReminder.Value.Date : null,
                IsCompleted = false
            };

            try
            {
                _taskRepository.AddTask(task);

                // NEW: record the action (and the reminder, if there is one) in the activity log
                LogActivity("Task added: " + task.Title);
                if (task.ReminderDate.HasValue)
                {
                    LogActivity("Reminder set: " + task.Title + " on " + task.ReminderDate.Value.ToString("dd MMM yyyy"));
                }

                txtTaskTitle.Clear();
                txtTaskDescription.Clear();
                chkUseReminder.Checked = false;

                LoadTasks(); // reload from MySQL so the grid always matches the database
                ShowTaskStatus("Task added successfully.", false);
            }
            catch (Exception ex)
            {
                ShowTaskStatus("Task could not be saved: " + ex.Message, true);
            }
        }

        private void btnCompleteTask_Click(object? sender, EventArgs e)
        {
            CyberTask? task = GetSelectedTask();

            if (task == null)
            {
                ShowTaskStatus("Please select a task first.", true);
                return;
            }

            if (task.IsCompleted)
            {
                ShowTaskStatus("That task is already completed.", true);
                return;
            }

            try
            {
                _taskRepository.MarkTaskCompleted(task.TaskId);
                LogActivity("Task completed: " + task.Title); // NEW
                LoadTasks();
                ShowTaskStatus("Task marked as completed.", false);
            }
            catch (Exception ex)
            {
                ShowTaskStatus("Could not complete task: " + ex.Message, true);
            }
        }

        private void btnDeleteTask_Click(object? sender, EventArgs e)
        {
            CyberTask? task = GetSelectedTask();

            if (task == null)
            {
                ShowTaskStatus("Please select a task first.", true);
                return;
            }

            // Ask first, because deleting cannot be undone.
            DialogResult answer = MessageBox.Show(
                "Delete this task?" + Environment.NewLine + task.Title,
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (answer != DialogResult.Yes)
                return;

            try
            {
                _taskRepository.DeleteTask(task.TaskId);
                LogActivity("Task deleted: " + task.Title); // NEW
                LoadTasks();
                ShowTaskStatus("Task deleted.", false);
            }
            catch (Exception ex)
            {
                ShowTaskStatus("Could not delete task: " + ex.Message, true);
            }
        }

        private void btnRefreshTasks_Click(object? sender, EventArgs e)
        {
            LoadTasks();
            ShowTaskStatus("Task list refreshed from MySQL.", false);
        }
    }
}