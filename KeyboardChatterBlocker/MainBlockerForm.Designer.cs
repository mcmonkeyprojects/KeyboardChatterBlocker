namespace KeyboardChatterBlocker
{
    partial class MainBlockerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainBlockerForm));
            this.ChatterLogGrid = new System.Windows.Forms.DataGridView();
            this.Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Key = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChatterDelay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChatterConfigure = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EnabledCheckbox = new System.Windows.Forms.CheckBox();
            this.ChatterThresholdBox = new System.Windows.Forms.NumericUpDown();
            this.ChatterThresholdLabel = new System.Windows.Forms.Label();
            this.TrayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.TrayIconCheckbox = new System.Windows.Forms.CheckBox();
            this.StartWithWindowsCheckbox = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.ChatterLogTabPage = new System.Windows.Forms.TabPage();
            this.StatsTabPage = new System.Windows.Forms.TabPage();
            this.StatsGrid = new System.Windows.Forms.DataGridView();
            this.StatsKeyColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StatsCountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StatsChatterColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StatsRateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KeysTabPage = new System.Windows.Forms.TabPage();
            this.AddKeyButton = new System.Windows.Forms.Button();
            this.ConfigureKeysGrid = new System.Windows.Forms.DataGridView();
            this.ConfigureKeyColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ConfigureThresholdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RemoveColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AutoDisableProgramsTabPage = new System.Windows.Forms.TabPage();
            this.AutoDisableProgramsList = new System.Windows.Forms.ListView();
            this.RemoveProgramButton = new System.Windows.Forms.Button();
            this.ShowProgramListButton = new System.Windows.Forms.Button();
            this.AddToListButton = new System.Windows.Forms.Button();
            this.AddProgramTextBox = new System.Windows.Forms.TextBox();
            this.AboutTab = new System.Windows.Forms.TabPage();
            this.AboutLinkLabel = new System.Windows.Forms.LinkLabel();
            this.AboutLabel5 = new System.Windows.Forms.Label();
            this.AboutLavel4 = new System.Windows.Forms.Label();
            this.AboutLabel3 = new System.Windows.Forms.Label();
            this.AboutLabel2 = new System.Windows.Forms.Label();
            this.AboutLabel1 = new System.Windows.Forms.Label();
            this.EnableNoteLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ChatterLogGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChatterThresholdBox)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.ChatterLogTabPage.SuspendLayout();
            this.StatsTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StatsGrid)).BeginInit();
            this.KeysTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ConfigureKeysGrid)).BeginInit();
            this.AutoDisableProgramsTabPage.SuspendLayout();
            this.AboutTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // ChatterLogGrid
            // 
            this.ChatterLogGrid.AllowUserToAddRows = false;
            this.ChatterLogGrid.AllowUserToDeleteRows = false;
            this.ChatterLogGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ChatterLogGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ChatterLogGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Time,
            this.Key,
            this.ChatterDelay,
            this.ChatterConfigure});
            this.ChatterLogGrid.Location = new System.Drawing.Point(6, 6);
            this.ChatterLogGrid.Name = "ChatterLogGrid";
            this.ChatterLogGrid.ReadOnly = true;
            this.ChatterLogGrid.Size = new System.Drawing.Size(419, 278);
            this.ChatterLogGrid.TabIndex = 0;
            this.ChatterLogGrid.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ChatterLogGrid_CellContentDoubleClick);
            // 
            // Time
            // 
            this.Time.HeaderText = "Time";
            this.Time.Name = "Time";
            this.Time.ReadOnly = true;
            this.Time.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Time.Width = 90;
            // 
            // Key
            // 
            this.Key.HeaderText = "Key";
            this.Key.Name = "Key";
            this.Key.ReadOnly = true;
            this.Key.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Key.Width = 75;
            // 
            // ChatterDelay
            // 
            this.ChatterDelay.HeaderText = "Chatter Delay (ms)";
            this.ChatterDelay.Name = "ChatterDelay";
            this.ChatterDelay.ReadOnly = true;
            this.ChatterDelay.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ChatterDelay.Width = 135;
            // 
            // ChatterConfigure
            // 
            this.ChatterConfigure.HeaderText = "Configure";
            this.ChatterConfigure.Name = "ChatterConfigure";
            this.ChatterConfigure.ReadOnly = true;
            this.ChatterConfigure.Width = 75;
            // 
            // EnabledCheckbox
            // 
            this.EnabledCheckbox.AutoSize = true;
            this.EnabledCheckbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EnabledCheckbox.Location = new System.Drawing.Point(152, 12);
            this.EnabledCheckbox.Name = "EnabledCheckbox";
            this.EnabledCheckbox.Size = new System.Drawing.Size(123, 35);
            this.EnabledCheckbox.TabIndex = 1;
            this.EnabledCheckbox.Text = "Enable";
            this.EnabledCheckbox.UseVisualStyleBackColor = true;
            this.EnabledCheckbox.CheckedChanged += new System.EventHandler(this.EnabledCheckbox_CheckedChanged);
            // 
            // ChatterThresholdBox
            // 
            this.ChatterThresholdBox.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.ChatterThresholdBox.Location = new System.Drawing.Point(347, 65);
            this.ChatterThresholdBox.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.ChatterThresholdBox.Name = "ChatterThresholdBox";
            this.ChatterThresholdBox.Size = new System.Drawing.Size(95, 20);
            this.ChatterThresholdBox.TabIndex = 2;
            this.ChatterThresholdBox.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.ChatterThresholdBox.ValueChanged += new System.EventHandler(this.ChatterThresholdBox_ValueChanged);
            // 
            // ChatterThresholdLabel
            // 
            this.ChatterThresholdLabel.AutoSize = true;
            this.ChatterThresholdLabel.Location = new System.Drawing.Point(221, 67);
            this.ChatterThresholdLabel.Name = "ChatterThresholdLabel";
            this.ChatterThresholdLabel.Size = new System.Drawing.Size(127, 13);
            this.ChatterThresholdLabel.TabIndex = 3;
            this.ChatterThresholdLabel.Text = "Global Chatter Threshold:\r\n";
            // 
            // TrayIcon
            // 
            this.TrayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("TrayIcon.Icon")));
            this.TrayIcon.Text = "Keyboard Chatter Blocker";
            this.TrayIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TrayIcon_MouseDoubleClick);
            // 
            // TrayIconCheckbox
            // 
            this.TrayIconCheckbox.AutoSize = true;
            this.TrayIconCheckbox.Location = new System.Drawing.Point(131, 66);
            this.TrayIconCheckbox.Name = "TrayIconCheckbox";
            this.TrayIconCheckbox.Size = new System.Drawing.Size(84, 17);
            this.TrayIconCheckbox.TabIndex = 5;
            this.TrayIconCheckbox.Text = "Start In Tray";
            this.TrayIconCheckbox.UseVisualStyleBackColor = true;
            this.TrayIconCheckbox.CheckedChanged += new System.EventHandler(this.TrayIconCheckbox_CheckedChanged);
            // 
            // StartWithWindowsCheckbox
            // 
            this.StartWithWindowsCheckbox.AutoSize = true;
            this.StartWithWindowsCheckbox.Location = new System.Drawing.Point(5, 66);
            this.StartWithWindowsCheckbox.Name = "StartWithWindowsCheckbox";
            this.StartWithWindowsCheckbox.Size = new System.Drawing.Size(120, 17);
            this.StartWithWindowsCheckbox.TabIndex = 6;
            this.StartWithWindowsCheckbox.Text = "Start With Windows";
            this.StartWithWindowsCheckbox.UseVisualStyleBackColor = true;
            this.StartWithWindowsCheckbox.CheckedChanged += new System.EventHandler(this.StartWithWindowsCheckbox_CheckedChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.ChatterLogTabPage);
            this.tabControl1.Controls.Add(this.StatsTabPage);
            this.tabControl1.Controls.Add(this.KeysTabPage);
            this.tabControl1.Controls.Add(this.AutoDisableProgramsTabPage);
            this.tabControl1.Controls.Add(this.AboutTab);
            this.tabControl1.Location = new System.Drawing.Point(13, 89);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(439, 316);
            this.tabControl1.TabIndex = 7;
            this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.TabControl1_Selected);
            // 
            // ChatterLogTabPage
            // 
            this.ChatterLogTabPage.Controls.Add(this.ChatterLogGrid);
            this.ChatterLogTabPage.Location = new System.Drawing.Point(4, 22);
            this.ChatterLogTabPage.Name = "ChatterLogTabPage";
            this.ChatterLogTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.ChatterLogTabPage.Size = new System.Drawing.Size(431, 290);
            this.ChatterLogTabPage.TabIndex = 0;
            this.ChatterLogTabPage.Text = "Chatter Log";
            this.ChatterLogTabPage.UseVisualStyleBackColor = true;
            // 
            // StatsTabPage
            // 
            this.StatsTabPage.Controls.Add(this.StatsGrid);
            this.StatsTabPage.Location = new System.Drawing.Point(4, 22);
            this.StatsTabPage.Name = "StatsTabPage";
            this.StatsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.StatsTabPage.Size = new System.Drawing.Size(431, 290);
            this.StatsTabPage.TabIndex = 1;
            this.StatsTabPage.Text = "Stats";
            this.StatsTabPage.UseVisualStyleBackColor = true;
            // 
            // StatsGrid
            // 
            this.StatsGrid.AllowUserToAddRows = false;
            this.StatsGrid.AllowUserToDeleteRows = false;
            this.StatsGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.StatsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.StatsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.StatsKeyColumn,
            this.StatsCountColumn,
            this.StatsChatterColumn,
            this.StatsRateColumn});
            this.StatsGrid.Location = new System.Drawing.Point(3, 6);
            this.StatsGrid.Name = "StatsGrid";
            this.StatsGrid.ReadOnly = true;
            this.StatsGrid.Size = new System.Drawing.Size(422, 277);
            this.StatsGrid.TabIndex = 0;
            // 
            // StatsKeyColumn
            // 
            this.StatsKeyColumn.HeaderText = "Key";
            this.StatsKeyColumn.Name = "StatsKeyColumn";
            this.StatsKeyColumn.ReadOnly = true;
            this.StatsKeyColumn.Width = 75;
            // 
            // StatsCountColumn
            // 
            this.StatsCountColumn.HeaderText = "Count";
            this.StatsCountColumn.Name = "StatsCountColumn";
            this.StatsCountColumn.ReadOnly = true;
            this.StatsCountColumn.Width = 75;
            // 
            // StatsChatterColumn
            // 
            this.StatsChatterColumn.HeaderText = "Chatter";
            this.StatsChatterColumn.Name = "StatsChatterColumn";
            this.StatsChatterColumn.ReadOnly = true;
            this.StatsChatterColumn.Width = 75;
            // 
            // StatsRateColumn
            // 
            this.StatsRateColumn.HeaderText = "Rate";
            this.StatsRateColumn.Name = "StatsRateColumn";
            this.StatsRateColumn.ReadOnly = true;
            this.StatsRateColumn.Width = 75;
            // 
            // KeysTabPage
            // 
            this.KeysTabPage.Controls.Add(this.AddKeyButton);
            this.KeysTabPage.Controls.Add(this.ConfigureKeysGrid);
            this.KeysTabPage.Location = new System.Drawing.Point(4, 22);
            this.KeysTabPage.Name = "KeysTabPage";
            this.KeysTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.KeysTabPage.Size = new System.Drawing.Size(431, 290);
            this.KeysTabPage.TabIndex = 2;
            this.KeysTabPage.Text = "Configure Keys";
            this.KeysTabPage.UseVisualStyleBackColor = true;
            // 
            // AddKeyButton
            // 
            this.AddKeyButton.Location = new System.Drawing.Point(6, 6);
            this.AddKeyButton.Name = "AddKeyButton";
            this.AddKeyButton.Size = new System.Drawing.Size(419, 23);
            this.AddKeyButton.TabIndex = 1;
            this.AddKeyButton.Text = "Add Key";
            this.AddKeyButton.UseVisualStyleBackColor = true;
            this.AddKeyButton.Click += new System.EventHandler(this.AddKeyButton_Click);
            // 
            // ConfigureKeysGrid
            // 
            this.ConfigureKeysGrid.AllowUserToAddRows = false;
            this.ConfigureKeysGrid.AllowUserToDeleteRows = false;
            this.ConfigureKeysGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfigureKeysGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ConfigureKeysGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ConfigureKeyColumn,
            this.ConfigureThresholdColumn,
            this.RemoveColumn});
            this.ConfigureKeysGrid.Location = new System.Drawing.Point(7, 35);
            this.ConfigureKeysGrid.Name = "ConfigureKeysGrid";
            this.ConfigureKeysGrid.Size = new System.Drawing.Size(418, 249);
            this.ConfigureKeysGrid.TabIndex = 0;
            this.ConfigureKeysGrid.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ConfigureKeysGrid_CellContentDoubleClick);
            // 
            // ConfigureKeyColumn
            // 
            this.ConfigureKeyColumn.HeaderText = "Key";
            this.ConfigureKeyColumn.Name = "ConfigureKeyColumn";
            this.ConfigureKeyColumn.ReadOnly = true;
            // 
            // ConfigureThresholdColumn
            // 
            this.ConfigureThresholdColumn.HeaderText = "Chatter Threshold";
            this.ConfigureThresholdColumn.Name = "ConfigureThresholdColumn";
            this.ConfigureThresholdColumn.ReadOnly = true;
            this.ConfigureThresholdColumn.Width = 150;
            // 
            // RemoveColumn
            // 
            this.RemoveColumn.HeaderText = "Remove";
            this.RemoveColumn.Name = "RemoveColumn";
            this.RemoveColumn.ReadOnly = true;
            // 
            // AutoDisableProgramsTabPage
            // 
            this.AutoDisableProgramsTabPage.Controls.Add(this.AutoDisableProgramsList);
            this.AutoDisableProgramsTabPage.Controls.Add(this.RemoveProgramButton);
            this.AutoDisableProgramsTabPage.Controls.Add(this.ShowProgramListButton);
            this.AutoDisableProgramsTabPage.Controls.Add(this.AddToListButton);
            this.AutoDisableProgramsTabPage.Controls.Add(this.AddProgramTextBox);
            this.AutoDisableProgramsTabPage.Location = new System.Drawing.Point(4, 22);
            this.AutoDisableProgramsTabPage.Name = "AutoDisableProgramsTabPage";
            this.AutoDisableProgramsTabPage.Size = new System.Drawing.Size(431, 290);
            this.AutoDisableProgramsTabPage.TabIndex = 4;
            this.AutoDisableProgramsTabPage.Text = "Auto Disable Programs";
            this.AutoDisableProgramsTabPage.UseVisualStyleBackColor = true;
            // 
            // AutoDisableProgramsList
            // 
            this.AutoDisableProgramsList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AutoDisableProgramsList.HideSelection = false;
            this.AutoDisableProgramsList.Location = new System.Drawing.Point(8, 59);
            this.AutoDisableProgramsList.Name = "AutoDisableProgramsList";
            this.AutoDisableProgramsList.Size = new System.Drawing.Size(417, 225);
            this.AutoDisableProgramsList.TabIndex = 5;
            this.AutoDisableProgramsList.UseCompatibleStateImageBehavior = false;
            this.AutoDisableProgramsList.View = System.Windows.Forms.View.List;
            this.AutoDisableProgramsList.Click += new System.EventHandler(this.AutoDisableProgramsList_Click);
            // 
            // RemoveProgramButton
            // 
            this.RemoveProgramButton.Enabled = false;
            this.RemoveProgramButton.Location = new System.Drawing.Point(265, 30);
            this.RemoveProgramButton.Name = "RemoveProgramButton";
            this.RemoveProgramButton.Size = new System.Drawing.Size(160, 23);
            this.RemoveProgramButton.TabIndex = 4;
            this.RemoveProgramButton.Text = "Remove From List";
            this.RemoveProgramButton.UseVisualStyleBackColor = true;
            this.RemoveProgramButton.Click += new System.EventHandler(this.RemoveProgramButton_Click);
            // 
            // ShowProgramListButton
            // 
            this.ShowProgramListButton.Location = new System.Drawing.Point(371, 3);
            this.ShowProgramListButton.Name = "ShowProgramListButton";
            this.ShowProgramListButton.Size = new System.Drawing.Size(54, 23);
            this.ShowProgramListButton.TabIndex = 2;
            this.ShowProgramListButton.Text = "(...)";
            this.ShowProgramListButton.UseVisualStyleBackColor = true;
            this.ShowProgramListButton.Click += new System.EventHandler(this.ShowProgramListButton_Click);
            // 
            // AddToListButton
            // 
            this.AddToListButton.Enabled = false;
            this.AddToListButton.Location = new System.Drawing.Point(8, 29);
            this.AddToListButton.Name = "AddToListButton";
            this.AddToListButton.Size = new System.Drawing.Size(250, 23);
            this.AddToListButton.TabIndex = 1;
            this.AddToListButton.Text = "Add Program To List";
            this.AddToListButton.UseVisualStyleBackColor = true;
            this.AddToListButton.Click += new System.EventHandler(this.AddToListButton_Click);
            // 
            // AddProgramTextBox
            // 
            this.AddProgramTextBox.Location = new System.Drawing.Point(8, 3);
            this.AddProgramTextBox.Name = "AddProgramTextBox";
            this.AddProgramTextBox.Size = new System.Drawing.Size(357, 20);
            this.AddProgramTextBox.TabIndex = 0;
            this.AddProgramTextBox.Text = "Program Name";
            this.AddProgramTextBox.TextChanged += new System.EventHandler(this.AddProgramTextBox_TextChanged);
            this.AddProgramTextBox.Enter += new System.EventHandler(this.AddProgramTextBox_Enter);
            this.AddProgramTextBox.Leave += new System.EventHandler(this.AddProgramTextBox_Leave);
            // 
            // AboutTab
            // 
            this.AboutTab.Controls.Add(this.AboutLinkLabel);
            this.AboutTab.Controls.Add(this.AboutLabel5);
            this.AboutTab.Controls.Add(this.AboutLavel4);
            this.AboutTab.Controls.Add(this.AboutLabel3);
            this.AboutTab.Controls.Add(this.AboutLabel2);
            this.AboutTab.Controls.Add(this.AboutLabel1);
            this.AboutTab.Location = new System.Drawing.Point(4, 22);
            this.AboutTab.Name = "AboutTab";
            this.AboutTab.Padding = new System.Windows.Forms.Padding(3);
            this.AboutTab.Size = new System.Drawing.Size(431, 293);
            this.AboutTab.TabIndex = 3;
            this.AboutTab.Text = "About";
            this.AboutTab.UseVisualStyleBackColor = true;
            // 
            // AboutLinkLabel
            // 
            this.AboutLinkLabel.AutoSize = true;
            this.AboutLinkLabel.Location = new System.Drawing.Point(47, 157);
            this.AboutLinkLabel.Name = "AboutLinkLabel";
            this.AboutLinkLabel.Size = new System.Drawing.Size(308, 13);
            this.AboutLinkLabel.TabIndex = 5;
            this.AboutLinkLabel.TabStop = true;
            this.AboutLinkLabel.Text = "https://github.com/mcmonkeyprojects/KeyboardChatterBlocker";
            this.AboutLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.AboutLinkLabel_LinkClicked);
            // 
            // AboutLabel5
            // 
            this.AboutLabel5.AutoSize = true;
            this.AboutLabel5.Location = new System.Drawing.Point(11, 131);
            this.AboutLabel5.Name = "AboutLabel5";
            this.AboutLabel5.Size = new System.Drawing.Size(230, 13);
            this.AboutLabel5.TabIndex = 4;
            this.AboutLabel5.Text = "License, source code, and more info on GitHub";
            // 
            // AboutLavel4
            // 
            this.AboutLavel4.AutoSize = true;
            this.AboutLavel4.Location = new System.Drawing.Point(11, 99);
            this.AboutLavel4.Name = "AboutLavel4";
            this.AboutLavel4.Size = new System.Drawing.Size(311, 13);
            this.AboutLavel4.TabIndex = 3;
            this.AboutLavel4.Text = "Made available to the public under the terms of the MIT License.";
            // 
            // AboutLabel3
            // 
            this.AboutLabel3.AutoSize = true;
            this.AboutLabel3.Location = new System.Drawing.Point(11, 71);
            this.AboutLabel3.Name = "AboutLabel3";
            this.AboutLabel3.Size = new System.Drawing.Size(223, 13);
            this.AboutLabel3.TabIndex = 2;
            this.AboutLabel3.Text = "Copyright (C) 2019-2021, All Rights Reserved.";
            // 
            // AboutLabel2
            // 
            this.AboutLabel2.AutoSize = true;
            this.AboutLabel2.Location = new System.Drawing.Point(11, 42);
            this.AboutLabel2.Name = "AboutLabel2";
            this.AboutLabel2.Size = new System.Drawing.Size(190, 13);
            this.AboutLabel2.TabIndex = 1;
            this.AboutLabel2.Text = "Created by Alex \"mcmonkey\" Goodwin";
            // 
            // AboutLabel1
            // 
            this.AboutLabel1.AutoSize = true;
            this.AboutLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AboutLabel1.Location = new System.Drawing.Point(7, 7);
            this.AboutLabel1.Name = "AboutLabel1";
            this.AboutLabel1.Size = new System.Drawing.Size(214, 20);
            this.AboutLabel1.TabIndex = 0;
            this.AboutLabel1.Text = "Keyboard Chatter Blocker";
            // 
            // EnableNoteLabel
            // 
            this.EnableNoteLabel.AutoSize = true;
            this.EnableNoteLabel.BackColor = System.Drawing.SystemColors.Control;
            this.EnableNoteLabel.Location = new System.Drawing.Point(2, 50);
            this.EnableNoteLabel.Name = "EnableNoteLabel";
            this.EnableNoteLabel.Size = new System.Drawing.Size(63, 13);
            this.EnableNoteLabel.TabIndex = 8;
            this.EnableNoteLabel.Text = "EnableNote";
            this.EnableNoteLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainBlockerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 407);
            this.Controls.Add(this.EnableNoteLabel);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.StartWithWindowsCheckbox);
            this.Controls.Add(this.TrayIconCheckbox);
            this.Controls.Add(this.ChatterThresholdLabel);
            this.Controls.Add(this.ChatterThresholdBox);
            this.Controls.Add(this.EnabledCheckbox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(480, 5000);
            this.MinimumSize = new System.Drawing.Size(480, 300);
            this.Name = "MainBlockerForm";
            this.Text = "Keyboard Chatter Blocker";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainBlockerForm_FormClosing);
            this.Load += new System.EventHandler(this.MainBlockerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ChatterLogGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChatterThresholdBox)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.ChatterLogTabPage.ResumeLayout(false);
            this.StatsTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.StatsGrid)).EndInit();
            this.KeysTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ConfigureKeysGrid)).EndInit();
            this.AutoDisableProgramsTabPage.ResumeLayout(false);
            this.AutoDisableProgramsTabPage.PerformLayout();
            this.AboutTab.ResumeLayout(false);
            this.AboutTab.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView ChatterLogGrid;
        private System.Windows.Forms.CheckBox EnabledCheckbox;
        private System.Windows.Forms.NumericUpDown ChatterThresholdBox;
        private System.Windows.Forms.Label ChatterThresholdLabel;
        private System.Windows.Forms.NotifyIcon TrayIcon;
        private System.Windows.Forms.CheckBox TrayIconCheckbox;
        private System.Windows.Forms.CheckBox StartWithWindowsCheckbox;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage ChatterLogTabPage;
        private System.Windows.Forms.TabPage StatsTabPage;
        private System.Windows.Forms.TabPage KeysTabPage;
        private System.Windows.Forms.DataGridView StatsGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn StatsKeyColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn StatsCountColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn StatsChatterColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn StatsRateColumn;
        private System.Windows.Forms.DataGridView ConfigureKeysGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn ConfigureKeyColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ConfigureThresholdColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn RemoveColumn;
        private System.Windows.Forms.Button AddKeyButton;
        private System.Windows.Forms.TabPage AboutTab;
        private System.Windows.Forms.LinkLabel AboutLinkLabel;
        private System.Windows.Forms.Label AboutLabel5;
        private System.Windows.Forms.Label AboutLavel4;
        private System.Windows.Forms.Label AboutLabel3;
        private System.Windows.Forms.Label AboutLabel2;
        private System.Windows.Forms.Label AboutLabel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Time;
        private System.Windows.Forms.DataGridViewTextBoxColumn Key;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChatterDelay;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChatterConfigure;
        private System.Windows.Forms.Label EnableNoteLabel;
        private System.Windows.Forms.TabPage AutoDisableProgramsTabPage;
        private System.Windows.Forms.Button ShowProgramListButton;
        private System.Windows.Forms.Button AddToListButton;
        private System.Windows.Forms.TextBox AddProgramTextBox;
        private System.Windows.Forms.Button RemoveProgramButton;
        private System.Windows.Forms.ListView AutoDisableProgramsList;
    }
}

