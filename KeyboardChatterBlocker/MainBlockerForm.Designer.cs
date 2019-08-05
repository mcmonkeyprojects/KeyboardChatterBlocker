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
            this.ConfigKeysButton = new System.Windows.Forms.Button();
            this.TrayIconCheckbox = new System.Windows.Forms.CheckBox();
            this.StartWithWindowsCheckbox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.ChatterLogGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChatterThresholdBox)).BeginInit();
            this.SuspendLayout();
            // 
            // ChatterLogGrid
            // 
            this.ChatterLogGrid.AllowUserToAddRows = false;
            this.ChatterLogGrid.AllowUserToDeleteRows = false;
            this.ChatterLogGrid.AllowUserToResizeRows = false;
            this.ChatterLogGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ChatterLogGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Time,
            this.Key,
            this.ChatterDelay});
            this.ChatterLogGrid.Location = new System.Drawing.Point(12, 105);
            this.ChatterLogGrid.Name = "ChatterLogGrid";
            this.ChatterLogGrid.ReadOnly = true;
            this.ChatterLogGrid.Size = new System.Drawing.Size(425, 333);
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
            this.ChatterThresholdBox.Location = new System.Drawing.Point(152, 76);
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
            this.ChatterThresholdLabel.Location = new System.Drawing.Point(19, 78);
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
            // ConfigKeysButton
            // 
            this.ConfigKeysButton.Location = new System.Drawing.Point(260, 73);
            this.ConfigKeysButton.Name = "ConfigKeysButton";
            this.ConfigKeysButton.Size = new System.Drawing.Size(151, 23);
            this.ConfigKeysButton.TabIndex = 4;
            this.ConfigKeysButton.Text = "Configure Specific Keys";
            this.ConfigKeysButton.UseVisualStyleBackColor = true;
            this.ConfigKeysButton.Click += new System.EventHandler(this.ConfigKeysButton_Click);
            // 
            // TrayIconCheckbox
            // 
            this.TrayIconCheckbox.AutoSize = true;
            this.TrayIconCheckbox.Location = new System.Drawing.Point(142, 53);
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
            this.StartWithWindowsCheckbox.Location = new System.Drawing.Point(13, 53);
            this.StartWithWindowsCheckbox.Name = "StartWithWindowsCheckbox";
            this.StartWithWindowsCheckbox.Size = new System.Drawing.Size(120, 17);
            this.StartWithWindowsCheckbox.TabIndex = 6;
            this.StartWithWindowsCheckbox.Text = "Start With Windows";
            this.StartWithWindowsCheckbox.UseVisualStyleBackColor = true;
            this.StartWithWindowsCheckbox.CheckedChanged += new System.EventHandler(this.StartWithWindowsCheckbox_CheckedChanged);
            // 
            // MainBlockerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 450);
            this.Controls.Add(this.StartWithWindowsCheckbox);
            this.Controls.Add(this.TrayIconCheckbox);
            this.Controls.Add(this.ConfigKeysButton);
            this.Controls.Add(this.ChatterThresholdLabel);
            this.Controls.Add(this.ChatterThresholdBox);
            this.Controls.Add(this.EnabledCheckbox);
            this.Controls.Add(this.ChatterLogGrid);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainBlockerForm";
            this.Text = "Keyboard Chatter Blocker";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainBlockerForm_FormClosing);
            this.Load += new System.EventHandler(this.MainBlockerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ChatterLogGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChatterThresholdBox)).EndInit();
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
        private System.Windows.Forms.Button ConfigKeysButton;
        private System.Windows.Forms.CheckBox TrayIconCheckbox;
        private System.Windows.Forms.CheckBox StartWithWindowsCheckbox;
    }
}

