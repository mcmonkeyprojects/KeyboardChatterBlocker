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
            ((System.ComponentModel.ISupportInitialize)(this.ChatterLogGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChatterThresholdBox)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.ChatterLogTabPage.SuspendLayout();
            this.StatsTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StatsGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // ChatterLogGrid
            // 
            this.ChatterLogGrid.AllowUserToAddRows = false;
            this.ChatterLogGrid.AllowUserToDeleteRows = false;
            this.ChatterLogGrid.AllowUserToResizeRows = false;
            this.ChatterLogGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ChatterLogGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ChatterLogGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Time,
            this.Key,
            this.ChatterDelay});
            this.ChatterLogGrid.Location = new System.Drawing.Point(6, 6);
            this.ChatterLogGrid.Name = "ChatterLogGrid";
            this.ChatterLogGrid.ReadOnly = true;
            this.ChatterLogGrid.Size = new System.Drawing.Size(404, 298);
            this.ChatterLogGrid.TabIndex = 0;
            // 
            // Time
            // 
            this.Time.HeaderText = "Time";
            this.Time.Name = "Time";
            this.Time.ReadOnly = true;
            this.Time.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Key
            // 
            this.Key.HeaderText = "Key";
            this.Key.Name = "Key";
            this.Key.ReadOnly = true;
            this.Key.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // ChatterDelay
            // 
            this.ChatterDelay.HeaderText = "Chatter Delay (ms)";
            this.ChatterDelay.Name = "ChatterDelay";
            this.ChatterDelay.ReadOnly = true;
            this.ChatterDelay.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ChatterDelay.Width = 150;
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
            this.ChatterThresholdBox.Location = new System.Drawing.Point(354, 52);
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
            this.ChatterThresholdLabel.Location = new System.Drawing.Point(221, 54);
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
            this.TrayIconCheckbox.Location = new System.Drawing.Point(131, 53);
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
            this.StartWithWindowsCheckbox.Location = new System.Drawing.Point(5, 53);
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
            this.tabControl1.Location = new System.Drawing.Point(13, 78);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(439, 300);
            this.tabControl1.TabIndex = 7;
            this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.TabControl1_Selected);
            // 
            // ChatterLogTabPage
            // 
            this.ChatterLogTabPage.Controls.Add(this.ChatterLogGrid);
            this.ChatterLogTabPage.Location = new System.Drawing.Point(4, 22);
            this.ChatterLogTabPage.Name = "ChatterLogTabPage";
            this.ChatterLogTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.ChatterLogTabPage.Size = new System.Drawing.Size(416, 310);
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
            this.StatsTabPage.Size = new System.Drawing.Size(431, 274);
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
            this.StatsGrid.Size = new System.Drawing.Size(422, 261);
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
            this.KeysTabPage.Location = new System.Drawing.Point(4, 22);
            this.KeysTabPage.Name = "KeysTabPage";
            this.KeysTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.KeysTabPage.Size = new System.Drawing.Size(416, 310);
            this.KeysTabPage.TabIndex = 2;
            this.KeysTabPage.Text = "Configure Keys";
            this.KeysTabPage.UseVisualStyleBackColor = true;
            // 
            // MainBlockerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 383);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView ChatterLogGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Time;
        private System.Windows.Forms.DataGridViewTextBoxColumn Key;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChatterDelay;
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
    }
}

