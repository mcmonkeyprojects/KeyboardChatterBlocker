
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyboardChatterBlocker
{
    /// <summary>
    /// Event args for a key being blocked.
    /// </summary>
    public class KeyBlockedEventArgs : EventArgs
    {
        /// <summary>
        /// The key that was blocked.
        /// </summary>
        public Keys Key;

        /// <summary>
        /// The time separation in milliseconds.
        /// </summary>
        public uint Time;
    }
}
