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
        /// Hook ID for mouse intercept.
        /// </summary>
        public const int WH_MOUSE_LL = 14;

        /// <summary>
        /// Key state change Windows message.
        /// </summary>
        public const int WM_KEYDOWN = 0x0100, WM_KEYUP = 0x0101,
            WM_SYSKEYDOWN = 0x0104, WM_SYSKEYUP = 0x0105;

        /// <summary>
        /// Mouse state change Windows message.
        /// </summary>
        public const int WM_LBUTTONDOWN = 0x0201, WM_LBUTTONUP = 0x0202,
            WM_RBUTTONDOWN = 0x0204, WM_RBUTTONUP = 0x0205,
            WM_MBUTTONDOWN = 0x0207, WM_MBUTTONUP = 0x0208,
            WM_XBUTTONDOWN = 0x020B, WM_XBUTTONUP = 0x020C;

        /// <summary>
        /// An array of falses, except for the WParam values that are handled by this program.
        /// This exists as an optimization structure to reduce potential input delay caused by this running.
        /// </summary>
        public static bool[] HANDLED_WPARAMS = new bool[1024];

        static KeyboardInterceptor()
        {
            foreach (int wparam in new[] { WM_KEYDOWN, WM_KEYUP, WM_SYSKEYDOWN, WM_SYSKEYUP,
                    WM_LBUTTONDOWN, WM_LBUTTONUP, WM_RBUTTONDOWN, WM_RBUTTONUP,
                    WM_MBUTTONDOWN, WM_MBUTTONUP, WM_XBUTTONDOWN, WM_XBUTTONUP })
            {
                HANDLED_WPARAMS[wparam] = true;
            }
        }

        /// <summary>
        /// Reference to the keyboard Hook Callback.
        /// </summary>
        public LowLevelKeyboardProc KeyboardProcCallback;

        /// <summary>
        /// Reference to the mouse Hook Callback.
        /// </summary>
        public LowLevelKeyboardProc MouseProcCallback;

        /// <summary>
        /// The relevant <see cref="KeyBlocker"/>.
        /// </summary>
        public KeyBlocker KeyBlockHandler;

        /// <summary>
        /// The current keyboard hook ID.
        /// </summary>
        public IntPtr KeyboardHookID = IntPtr.Zero;

        /// <summary>
        /// The current mouse hook ID.
        /// </summary>
        public IntPtr MouseHookID = IntPtr.Zero;

        public KeyboardInterceptor(KeyBlocker blocker)
        {
            KeyBlockHandler = blocker;
            KeyboardProcCallback = KeyboardHookCallback;
            MouseProcCallback = MouseHookCallback;
            KeyboardHookID = SetKeyboardHook(KeyboardProcCallback);
            EnableMouseHook();
        }

        /// <summary>
        /// Enables the mouse hook (if not already enabled).
        /// </summary>
        public void EnableMouseHook()
        {
            if (MouseHookID == IntPtr.Zero)
            {
                MouseHookID = SetMouseHook(MouseProcCallback);
            }
        }

        /// <summary>
        /// Sets a keyboard hook for the current process onto the global Windows hook system.
        /// </summary>
        /// <param name="proc">The keyboard callback proc to use.</param>
        public static IntPtr SetKeyboardHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        /// <summary>
        /// Sets a mouse hook for the current process onto the global Windows hook system.
        /// </summary>
        /// <param name="proc">The keyboard callback proc to use.</param>
        public static IntPtr SetMouseHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_MOUSE_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        /// <summary>
        /// Delegate type for keyboard callback functions.
        /// </summary>
        public delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// The primary keyboard hook callback.
        /// </summary>
        /// <param name="nCode">The 'n' code (unused).</param>
        /// <param name="wParam">The 'w' parameter (Windows message ID in this case).</param>
        /// <param name="lParam">The 'l' parameter (key pressed in this case).</param>
        /// <returns>The result of the hook callback continuation (or '1' to block).</returns>
        public IntPtr KeyboardHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            int wParamInt = (int)wParam;
            if (nCode >= 0 && wParamInt < 1024 && HANDLED_WPARAMS[wParamInt])
            {
                bool isDown = wParamInt == WM_KEYDOWN || wParamInt == WM_SYSKEYDOWN;
                if (isDown || wParamInt == WM_KEYUP || wParamInt == WM_SYSKEYUP)
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
            return CallNextHookEx(KeyboardHookID, nCode, wParam, lParam);
        }

        /// <summary>
        /// The primary mouse hook callback.
        /// </summary>
        /// <param name="nCode">The 'n' code (unused).</param>
        /// <param name="wParam">The 'w' parameter (Windows message ID in this case).</param>
        /// <param name="lParam">The 'l' parameter (key pressed in this case).</param>
        /// <returns>The result of the hook callback continuation (or '1' to block).</returns>
        public IntPtr MouseHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            int wParamInt = (int)wParam;
            if (nCode >= 0 && wParamInt < 1024 && HANDLED_WPARAMS[wParamInt])
            {
                bool isDown = wParamInt == WM_LBUTTONDOWN || wParamInt == WM_RBUTTONDOWN || wParamInt == WM_MBUTTONDOWN || wParamInt == WM_XBUTTONDOWN;
                if (isDown || wParamInt == WM_LBUTTONUP || wParamInt == WM_RBUTTONUP || wParamInt == WM_MBUTTONUP || wParamInt == WM_XBUTTONUP)
                {
                    Keys key;
                    if (wParamInt == WM_LBUTTONDOWN || wParamInt == WM_LBUTTONUP)
                    {
                        key = KeysHelper.KEY_MOUSE_LEFT;
                    }
                    else if (wParamInt == WM_RBUTTONDOWN || wParamInt == WM_RBUTTONUP)
                    {
                        key = KeysHelper.KEY_MOUSE_RIGHT;
                    }
                    else if (wParamInt == WM_MBUTTONDOWN || wParamInt == WM_MBUTTONUP)
                    {
                        key = KeysHelper.KEY_MOUSE_RIGHT;
                    }
                    else
                    {
                        MSLLHOOKSTRUCT hookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));
                        if (hookStruct.mouseData == 0x20000)
                        {
                            key = KeysHelper.KEY_MOUSE_FORWARD;
                        }
                        else
                        {
                            key = KeysHelper.KEY_MOUSE_BACKWARD;
                        }
                    }
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
            return CallNextHookEx(MouseHookID, nCode, wParam, lParam);
        }

        /// <summary>
        /// Helper struct for mouse event data.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        private struct MSLLHOOKSTRUCT
        {
            public int coordX;
            public int coordY;
            public uint mouseData;
            public uint flags;
            public uint time;
            public IntPtr dwExtraInfo;
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
        /// Disables the mouse hook (to avoid input latency impact).
        /// </summary>
        public void DisableMouseHook()
        {
            if (MouseHookID != IntPtr.Zero)
            {
                UnhookWindowsHookEx(MouseHookID);
                MouseHookID = IntPtr.Zero;
            }
        }

        /// <summary>
        /// Dispose the object, removing the hook.
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (KeyboardHookID != IntPtr.Zero)
            {
                UnhookWindowsHookEx(KeyboardHookID);
                KeyboardHookID = IntPtr.Zero;
            }
            DisableMouseHook();
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
