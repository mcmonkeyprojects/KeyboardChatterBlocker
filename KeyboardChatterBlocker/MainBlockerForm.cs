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
    /// The user-interface control form.
    /// </summary>
    public partial class MainBlockerForm : Form
    {
        /// <summary>
        /// Init the form.
        /// </summary>
        public MainBlockerForm()
        {
            Program.Blocker.KeyBlockedEvent += LogKeyBlocked;
            InitializeComponent();
        }

        /// <summary>
        /// Method auto-called (by event) for when a key is blocked.
        /// </summary>
        /// <param name="e">The key blocked event details.</param>
        public void LogKeyBlocked(KeyBlockedEventArgs e)
        {
            dataGridView1.Rows.Add(DateTime.Now.ToString("HH:mm:ss"), e.Key.ToString(), e.Time.ToString());
        }
    }
}
