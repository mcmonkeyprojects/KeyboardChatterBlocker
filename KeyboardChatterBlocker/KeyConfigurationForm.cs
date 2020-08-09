using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyboardChatterBlocker
{
    /// <summary>
    /// Helper form to configure a key's chatter threshold.
    /// </summary>
    public partial class KeyConfigurationForm : Form
    {
        /// <summary>
        /// Construct the form.
        /// </summary>
        public KeyConfigurationForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// The key to configure.
        /// </summary>
        public Keys Key;

        /// <summary>
        /// Action to set the result.
        /// </summary>
        public Action<uint> SetResult;

        /// <summary>
        /// Event method auto-called when the form is loaded.
        /// </summary>
        public void KeyConfigurationForm_Load(object sender, EventArgs e)
        {
            ConfigureKeyLabel.Text = "Configure Key: " + Key.Stringify();
            GlobalLabel.Text = "Global Default: " + Program.Blocker.GlobalChatterTimeLimit;
            uint curVal = Program.Blocker.KeysToChatterTime[Key] ?? Program.Blocker.GlobalChatterTimeLimit;
            WasLabel.Text = "Was: " + curVal;
            numericUpDown1.Value = curVal;
        }

        /// <summary>
        /// Event method auto-called when the "Done" button is pressed.
        /// </summary>
        public void DoneButton_Click(object sender, EventArgs e)
        {
            SetResult((uint)numericUpDown1.Value);
            Close();
        }
    }
}
