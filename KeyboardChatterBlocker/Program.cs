using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace KeyboardChatterBlocker
{
    /// <summary>
    /// Main entry point class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The Key Blocker instance.
        /// </summary>
        public static KeyBlocker Blocker;

        /// <summary>
        /// Whether the program should hide in the system tray.
        /// </summary>
        public static bool HideInSystemTray = false;

        /// <summary>
        /// The main form, <see cref="MainBlockerForm"/>.
        /// </summary>
        public static MainBlockerForm MainForm;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // This needs priority to prevent delaying input
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.AboveNormal;
            Blocker = new KeyBlocker();
            using (KeyboardInterceptor intercept = new KeyboardInterceptor(Blocker))
            {
                Blocker.AutoEnableMouse();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                MainForm = new MainBlockerForm();
                Application.Run(MainForm);
            }
        }
    }
}
