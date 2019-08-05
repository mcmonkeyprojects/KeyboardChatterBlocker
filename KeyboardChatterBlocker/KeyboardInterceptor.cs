using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows.Forms;

namespace KeyboardChatterBlocker
{
    /// <summary>
    /// Keyboard interceptor underlying hook tool, based on https://blogs.msdn.microsoft.com/toub/2006/05/03/low-level-keyboard-hook-in-c/
    /// </summary>
    public class KeyboardInterceptor : IDisposable
    {
        /// <summary>
        /// Hook ID for keyboard intercept.
        /// </summary>
        public const int WH_KEYBOARD_LL = 13;

        /// <summary>
        /// Key state change Windows message.
        /// </summary>
        public const int WM_KEYDOWN = 0x0100, WM_KEYUP = 0x0101,
            WM_SYSKEYDOWN = 0x0104, WM_SYSKEYUP = 0x0105;

        /// <summary>
        /// Reference to the Hook Callback.
        /// </summary>
        public LowLevelKeyboardProc KeyboardProcCallback;

        /// <summary>
        /// The relevant <see cref="KeyBlocker"/>.
        /// </summary>
        public KeyBlocker KeyBlockHandler;

        /// <summary>
        /// The current hook ID.
        /// </summary>
        public IntPtr HookID = IntPtr.Zero;

        public KeyboardInterceptor(KeyBlocker blocker)
        {
            KeyBlockHandler = blocker;
            KeyboardProcCallback = HookCallback;
            HookID = SetHook(KeyboardProcCallback);
        }

        /// <summary>
        /// Sets a hook for the current process onto the global Windows hook system.
        /// </summary>
        /// <param name="proc">The keyboard callback proc to use.</param>
        /// <returns></returns>
        public static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        /// <summary>
        /// Delegate type for keyboard callback functions.
        /// </summary>
        public delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// The primary hook callback.
        /// </summary>
        /// <param name="nCode">The 'n' code (unused).</param>
        /// <param name="wParam">The 'w' parameter (Windows message ID in this case).</param>
        /// <param name="lParam">The 'l' parameter (key pressed in this case).</param>
        /// <returns>The result of the hook callback continuation (or '1' to block).</returns>
        public IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                bool isDown = wParam == (IntPtr)WM_KEYDOWN || wParam == (IntPtr)WM_SYSKEYDOWN;
                if (isDown || wParam == (IntPtr)WM_KEYUP || wParam == (IntPtr)WM_SYSKEYUP)
                {
                    int vkCode = Marshal.ReadInt32(lParam);
                    Keys key = (Keys)vkCode;
                    if (isDown)
                    {
                        if (!KeyBlockHandler.AllowKeyDown(key))
                        {
                            return (IntPtr)1;
                        }
                    }
                    else
                    {
                        if (!KeyBlockHandler.AllowKeyUp(key))
                        {
                            return (IntPtr)1;
                        }
                    }
                }
            }
            return CallNextHookEx(HookID, nCode, wParam, lParam);
        }

        /// <summary>
        /// External Windows API call. Sets a global Windows hook.
        /// </summary>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        /// <summary>
        /// External Windows API call. Removes a global Windows hook.
        /// </summary>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnhookWindowsHookEx(IntPtr hhk);

        /// <summary>
        /// External Windows API call. Continues a hook callback procedure.
        /// </summary>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// External Windows API call. Gets a handle on a process module.
        /// </summary>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        /// <summary>
        /// Dispose the object, removing the hook.
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (HookID != IntPtr.Zero)
            {
                UnhookWindowsHookEx(HookID);
                HookID = IntPtr.Zero;
            }
        }

        /// <summary>
        /// Destruct the object, removing the hook.
        /// </summary>
        ~KeyboardInterceptor()
        {
            Dispose(false);
        }

        /// <summary>
        /// Dispose the object, removing the hook.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
