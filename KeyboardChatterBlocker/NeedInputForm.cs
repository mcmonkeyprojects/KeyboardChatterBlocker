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
    /// The form for when you're going to press a key to be added to the keyboard chatter list.
    /// </summary>
    public partial class NeedInputForm : Form
    {
        /// <summary>
        /// Construct the form.
        /// </summary>
        public NeedInputForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Event method auto-called when the "Cancel" button is pressed.
        /// </summary>
        public void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// An action to set the result key.
        /// </summary>
        public Action<Keys> SetResultKey;

        /// <summary>
        /// Event method auto-called when any key is pressed within the form.
        /// </summary>
        public void NeedInputForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (Program.Blocker.KeysToChatterTime[e.KeyCode].HasValue)
            {
                return;
            }
            SetResultKey(e.KeyCode);
            Close();
        }

        /// <summary>
        /// Event method auto-called when any mouse button is pressed within the form.
        /// </summary>
        private void NeedInputForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (GUICancelButton.Bounds.Contains(e.Location))
            {
                return;
            }
            if (e.Button == MouseButtons.Left)
            {
                SetResultKey(KeysHelper.KEY_MOUSE_LEFT);
            }
            else if (e.Button == MouseButtons.Right)
            {
                SetResultKey(KeysHelper.KEY_MOUSE_RIGHT);
            }
            else if (e.Button == MouseButtons.Middle)
            {
                SetResultKey(KeysHelper.KEY_MOUSE_MIDDLE);
            }
            else if (e.Button == MouseButtons.XButton1)
            {
                SetResultKey(KeysHelper.KEY_MOUSE_BACKWARD);
            }
            else if (e.Button == MouseButtons.XButton2)
            {
                SetResultKey(KeysHelper.KEY_MOUSE_FORWARD);
            }
            Close();
        }
    }
}
