using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace KeyboardChatterBlocker
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // This needs priority to prevent delaying input
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.AboveNormal;
            using (KeyboardInterceptor intercept = new KeyboardInterceptor())
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainBlockerForm());
            }
        }
    }
}
