namespace KeyboardChatterBlocker
{
    partial class KeyConfigurationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KeyConfigurationForm));
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.ConfigureKeyLabel = new System.Windows.Forms.Label();
            this.DoneButton = new System.Windows.Forms.Button();
            this.WasLabel = new System.Windows.Forms.Label();
            this.GlobalLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown1.Location = new System.Drawing.Point(168, 58);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(184, 20);
            this.numericUpDown1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(165, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(187, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "New chatter threshold, in milliseconds:";
            // 
            // ConfigureKeyLabel
            // 
            this.ConfigureKeyLabel.AutoSize = true;
            this.ConfigureKeyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConfigureKeyLabel.Location = new System.Drawing.Point(125, 9);
            this.ConfigureKeyLabel.Name = "ConfigureKeyLabel";
            this.ConfigureKeyLabel.Size = new System.Drawing.Size(108, 20);
            this.ConfigureKeyLabel.TabIndex = 2;
            this.ConfigureKeyLabel.Text = "Configure Key";
            // 
            // DoneButton
            // 
            this.DoneButton.Location = new System.Drawing.Point(12, 86);
            this.DoneButton.Name = "DoneButton";
            this.DoneButton.Size = new System.Drawing.Size(357, 23);
            this.DoneButton.TabIndex = 3;
            this.DoneButton.Text = "Done";
            this.DoneButton.UseVisualStyleBackColor = true;
            this.DoneButton.Click += new System.EventHandler(this.DoneButton_Click);
            // 
            // WasLabel
            // 
            this.WasLabel.AutoSize = true;
            this.WasLabel.Location = new System.Drawing.Point(70, 60);
            this.WasLabel.Name = "WasLabel";
            this.WasLabel.Size = new System.Drawing.Size(53, 13);
            this.WasLabel.TabIndex = 4;
            this.WasLabel.Text = "Was: 100";
            // 
            // GlobalLabel
            // 
            this.GlobalLabel.AutoSize = true;
            this.GlobalLabel.Location = new System.Drawing.Point(25, 42);
            this.GlobalLabel.Name = "GlobalLabel";
            this.GlobalLabel.Size = new System.Drawing.Size(98, 13);
            this.GlobalLabel.TabIndex = 5;
            this.GlobalLabel.Text = "Global Default: 100";
            // 
            // KeyConfigurationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 120);
            this.Controls.Add(this.GlobalLabel);
            this.Controls.Add(this.WasLabel);
            this.Controls.Add(this.DoneButton);
            this.Controls.Add(this.ConfigureKeyLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDown1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "KeyConfigurationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Keyboard Chatter Blocker - Configure Key";
            this.Load += new System.EventHandler(this.KeyConfigurationForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label ConfigureKeyLabel;
        private System.Windows.Forms.Button DoneButton;
        private System.Windows.Forms.Label WasLabel;
        private System.Windows.Forms.Label GlobalLabel;
    }
}