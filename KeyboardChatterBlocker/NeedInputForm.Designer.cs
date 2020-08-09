namespace KeyboardChatterBlocker
{
    partial class NeedInputForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NeedInputForm));
            this.MessageLabel = new System.Windows.Forms.Label();
            this.GUICancelButton = new System.Windows.Forms.Button();
            this.TabThief = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // MessageLabel
            // 
            this.MessageLabel.AutoSize = true;
            this.MessageLabel.Location = new System.Drawing.Point(13, 13);
            this.MessageLabel.Name = "MessageLabel";
            this.MessageLabel.Size = new System.Drawing.Size(384, 26);
            this.MessageLabel.TabIndex = 0;
            this.MessageLabel.Text = "Press a key, any key. The key you press will be added (unless it\'s already listed" +
    ").\r\nYou can also press a mouse button.";
            this.MessageLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.NeedInputForm_MouseDown);
            // 
            // GUICancelButton
            // 
            this.GUICancelButton.Location = new System.Drawing.Point(12, 47);
            this.GUICancelButton.Name = "GUICancelButton";
            this.GUICancelButton.Size = new System.Drawing.Size(385, 23);
            this.GUICancelButton.TabIndex = 2;
            this.GUICancelButton.Text = "Cancel";
            this.GUICancelButton.UseVisualStyleBackColor = true;
            this.GUICancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // TabThief
            // 
            this.TabThief.Location = new System.Drawing.Point(-4, 51);
            this.TabThief.Name = "TabThief";
            this.TabThief.Size = new System.Drawing.Size(10, 23);
            this.TabThief.TabIndex = 1;
            this.TabThief.Text = "button1";
            this.TabThief.UseVisualStyleBackColor = true;
            // 
            // NeedInputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 80);
            this.Controls.Add(this.GUICancelButton);
            this.Controls.Add(this.MessageLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "NeedInputForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Keyboard Chatter Blocker: Need Input";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NeedInputForm_KeyDown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.NeedInputForm_MouseDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label MessageLabel;
        private System.Windows.Forms.Button GUICancelButton;
        private System.Windows.Forms.Button TabThief;
    }
}
