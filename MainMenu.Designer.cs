namespace Dupe_Finder
{
    partial class MainMenu
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenu));
            foundList = new ListBox();
            dupesList = new CheckedListBox();
            selectLocationsButton = new Button();
            startButton = new Button();
            folderSelector = new FolderBrowserDialog();
            whereSearching = new ListBox();
            titleLabel = new Label();
            locationDelete = new ContextMenuStrip(components);
            removeLocationToolStripMenuItem = new ToolStripMenuItem();
            progressBar = new ProgressBar();
            statusLabel = new Label();
            dupeOpen = new ContextMenuStrip(components);
            openFileToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            keepSelected = new Button();
            delSelected = new Button();
            returnButton = new Button();
            hashedLabel = new Label();
            countedLabel = new Label();
            dupesLabel = new Label();
            thread1 = new System.ComponentModel.BackgroundWorker();
            thread2 = new System.ComponentModel.BackgroundWorker();
            locationDelete.SuspendLayout();
            dupeOpen.SuspendLayout();
            SuspendLayout();
            // 
            // foundList
            // 
            foundList.BackColor = Color.FromArgb(64, 64, 64);
            foundList.BorderStyle = BorderStyle.None;
            foundList.Dock = DockStyle.Left;
            foundList.Enabled = false;
            foundList.ForeColor = Color.White;
            foundList.FormattingEnabled = true;
            foundList.HorizontalScrollbar = true;
            foundList.ItemHeight = 20;
            foundList.Location = new Point(0, 0);
            foundList.Name = "foundList";
            foundList.Size = new Size(299, 629);
            foundList.TabIndex = 0;
            foundList.Visible = false;
            foundList.SelectedIndexChanged += foundList_SelectedIndexChanged;
            // 
            // dupesList
            // 
            dupesList.BackColor = Color.FromArgb(64, 64, 64);
            dupesList.BorderStyle = BorderStyle.None;
            dupesList.CheckOnClick = true;
            dupesList.Dock = DockStyle.Left;
            dupesList.Enabled = false;
            dupesList.ForeColor = Color.White;
            dupesList.FormattingEnabled = true;
            dupesList.HorizontalScrollbar = true;
            dupesList.Location = new Point(299, 0);
            dupesList.Name = "dupesList";
            dupesList.Size = new Size(475, 629);
            dupesList.TabIndex = 1;
            dupesList.Visible = false;
            dupesList.ItemCheck += dupesList_ItemCheck;
            dupesList.MouseDown += dupesList_MouseDown;
            // 
            // selectLocationsButton
            // 
            selectLocationsButton.ForeColor = Color.Black;
            selectLocationsButton.Location = new Point(31, 49);
            selectLocationsButton.Name = "selectLocationsButton";
            selectLocationsButton.Size = new Size(218, 57);
            selectLocationsButton.TabIndex = 2;
            selectLocationsButton.Text = "Select where to search\r\n(can select multiple)";
            selectLocationsButton.UseVisualStyleBackColor = true;
            selectLocationsButton.Click += selectLocationsButton_Click;
            // 
            // startButton
            // 
            startButton.Enabled = false;
            startButton.ForeColor = Color.Black;
            startButton.Location = new Point(31, 134);
            startButton.Name = "startButton";
            startButton.Size = new Size(218, 40);
            startButton.TabIndex = 3;
            startButton.Text = "Start";
            startButton.UseVisualStyleBackColor = true;
            startButton.Click += startButton_Click;
            // 
            // folderSelector
            // 
            folderSelector.RootFolder = Environment.SpecialFolder.MyComputer;
            folderSelector.ShowNewFolderButton = false;
            // 
            // whereSearching
            // 
            whereSearching.BackColor = Color.FromArgb(64, 64, 64);
            whereSearching.BorderStyle = BorderStyle.None;
            whereSearching.ForeColor = Color.White;
            whereSearching.FormattingEnabled = true;
            whereSearching.HorizontalScrollbar = true;
            whereSearching.ItemHeight = 20;
            whereSearching.Location = new Point(289, 72);
            whereSearching.Name = "whereSearching";
            whereSearching.Size = new Size(523, 500);
            whereSearching.TabIndex = 4;
            whereSearching.Visible = false;
            whereSearching.MouseDown += whereSearching_MouseDown;
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Location = new Point(289, 49);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(93, 20);
            titleLabel.TabIndex = 5;
            titleLabel.Text = "Searching in:";
            titleLabel.Visible = false;
            // 
            // locationDelete
            // 
            locationDelete.Items.AddRange(new ToolStripItem[] { removeLocationToolStripMenuItem });
            locationDelete.Name = "locationDelete";
            locationDelete.Size = new Size(167, 26);
            // 
            // removeLocationToolStripMenuItem
            // 
            removeLocationToolStripMenuItem.Image = (Image)resources.GetObject("removeLocationToolStripMenuItem.Image");
            removeLocationToolStripMenuItem.Name = "removeLocationToolStripMenuItem";
            removeLocationToolStripMenuItem.Size = new Size(166, 22);
            removeLocationToolStripMenuItem.Text = "Remove Location";
            removeLocationToolStripMenuItem.Click += removeLocationToolStripMenuItem_Click;
            // 
            // progressBar
            // 
            progressBar.Enabled = false;
            progressBar.Location = new Point(11, 588);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(1035, 29);
            progressBar.Step = 1;
            progressBar.TabIndex = 6;
            progressBar.Visible = false;
            // 
            // statusLabel
            // 
            statusLabel.AutoSize = true;
            statusLabel.Enabled = false;
            statusLabel.Font = new Font("Segoe UI", 21.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            statusLabel.Location = new Point(12, 9);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(241, 40);
            statusLabel.TabIndex = 7;
            statusLabel.Text = "Counting the files";
            statusLabel.TextAlign = ContentAlignment.MiddleCenter;
            statusLabel.Visible = false;
            // 
            // dupeOpen
            // 
            dupeOpen.Items.AddRange(new ToolStripItem[] { openFileToolStripMenuItem, openToolStripMenuItem });
            dupeOpen.Name = "dupeOpen";
            dupeOpen.Size = new Size(180, 48);
            // 
            // openFileToolStripMenuItem
            // 
            openFileToolStripMenuItem.Image = (Image)resources.GetObject("openFileToolStripMenuItem.Image");
            openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            openFileToolStripMenuItem.Size = new Size(179, 22);
            openFileToolStripMenuItem.Text = "Open File";
            openFileToolStripMenuItem.Click += openFileToolStripMenuItem_Click;
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Image = (Image)resources.GetObject("openToolStripMenuItem.Image");
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new Size(179, 22);
            openToolStripMenuItem.Text = "View File in Explorer";
            openToolStripMenuItem.Click += openToolStripMenuItem_Click;
            // 
            // keepSelected
            // 
            keepSelected.Enabled = false;
            keepSelected.ForeColor = Color.Black;
            keepSelected.Location = new Point(829, 12);
            keepSelected.Name = "keepSelected";
            keepSelected.Size = new Size(218, 40);
            keepSelected.TabIndex = 9;
            keepSelected.Text = "Keep Selected";
            keepSelected.UseVisualStyleBackColor = true;
            keepSelected.Visible = false;
            keepSelected.Click += keepSelectedButton_Click;
            // 
            // delSelected
            // 
            delSelected.Enabled = false;
            delSelected.ForeColor = Color.Black;
            delSelected.Location = new Point(829, 72);
            delSelected.Name = "delSelected";
            delSelected.Size = new Size(218, 40);
            delSelected.TabIndex = 10;
            delSelected.Text = "Delete Selected";
            delSelected.UseVisualStyleBackColor = true;
            delSelected.Visible = false;
            delSelected.Click += delSelected_Click;
            // 
            // returnButton
            // 
            returnButton.Enabled = false;
            returnButton.ForeColor = Color.Black;
            returnButton.Location = new Point(829, 134);
            returnButton.Name = "returnButton";
            returnButton.Size = new Size(218, 40);
            returnButton.TabIndex = 11;
            returnButton.Text = "Return to Main Menu";
            returnButton.UseVisualStyleBackColor = true;
            returnButton.Visible = false;
            returnButton.Click += cancelButton_Click;
            // 
            // hashedLabel
            // 
            hashedLabel.AutoSize = true;
            hashedLabel.Enabled = false;
            hashedLabel.Location = new Point(879, 287);
            hashedLabel.Name = "hashedLabel";
            hashedLabel.Size = new Size(105, 20);
            hashedLabel.TabIndex = 12;
            hashedLabel.Text = "Files hashed in";
            hashedLabel.TextAlign = ContentAlignment.MiddleCenter;
            hashedLabel.Visible = false;
            // 
            // countedLabel
            // 
            countedLabel.AutoSize = true;
            countedLabel.Enabled = false;
            countedLabel.Location = new Point(875, 217);
            countedLabel.Name = "countedLabel";
            countedLabel.Size = new Size(112, 20);
            countedLabel.TabIndex = 13;
            countedLabel.Text = "Files counted in";
            countedLabel.TextAlign = ContentAlignment.MiddleCenter;
            countedLabel.Visible = false;
            // 
            // dupesLabel
            // 
            dupesLabel.AutoSize = true;
            dupesLabel.Enabled = false;
            dupesLabel.Location = new Point(862, 357);
            dupesLabel.Name = "dupesLabel";
            dupesLabel.Size = new Size(138, 20);
            dupesLabel.TabIndex = 14;
            dupesLabel.Text = "Found duplicates in";
            dupesLabel.TextAlign = ContentAlignment.MiddleCenter;
            dupesLabel.Visible = false;
            // 
            // thread1
            // 
            thread1.DoWork += thread1_DoWork;
            thread1.RunWorkerCompleted += thread1_RunWorkerCompleted;
            // 
            // thread2
            // 
            thread2.DoWork += thread2_DoWork;
            thread2.RunWorkerCompleted += thread2_RunWorkerCompleted;
            // 
            // MainMenu
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(1059, 629);
            Controls.Add(dupesLabel);
            Controls.Add(countedLabel);
            Controls.Add(hashedLabel);
            Controls.Add(returnButton);
            Controls.Add(delSelected);
            Controls.Add(keepSelected);
            Controls.Add(statusLabel);
            Controls.Add(progressBar);
            Controls.Add(titleLabel);
            Controls.Add(whereSearching);
            Controls.Add(startButton);
            Controls.Add(selectLocationsButton);
            Controls.Add(dupesList);
            Controls.Add(foundList);
            DoubleBuffered = true;
            Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            Name = "MainMenu";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Duplicate file finder";
            FormClosing += MainMenu_FormClosing;
            locationDelete.ResumeLayout(false);
            dupeOpen.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox foundList;
        private CheckedListBox dupesList;
        private Button selectLocationsButton;
        private Button startButton;
        private FolderBrowserDialog folderSelector;
        private ListBox whereSearching;
        private Label titleLabel;
        private ContextMenuStrip locationDelete;
        private ToolStripMenuItem removeLocationToolStripMenuItem;
        private ProgressBar progressBar;
        private Label statusLabel;
        private ContextMenuStrip dupeOpen;
        private ToolStripMenuItem openToolStripMenuItem;
        private Button keepSelected;
        private ToolStripMenuItem openFileToolStripMenuItem;
        private Button delSelected;
        private Button returnButton;
        private Label hashedLabel;
        private Label countedLabel;
        private Label dupesLabel;
        private System.ComponentModel.BackgroundWorker thread1;
        private System.ComponentModel.BackgroundWorker thread2;
    }
}
