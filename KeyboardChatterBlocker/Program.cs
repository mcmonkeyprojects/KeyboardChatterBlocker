using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Globalization;

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
        /// Forces the system to use standard Invariant culture to avoid bugs induced by Microsoft's broken auto-localization.
        /// See also https://github.com/FreneticLLC/FreneticUtilities/blob/master/FreneticUtilities/FreneticToolkit/SpecialTools.cs#L19
        /// </summary>
        public static void NormalizeCulture()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            NormalizeCulture();
            // If triggered by the installer, close this and relaunch, to avoid hanging up the installer.
            if (args.Length == 1 && args[0] == "_INSTALLER_AUTOBOUNCE")
            {
                Process.Start(Application.ExecutablePath);
                return;
            }
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
