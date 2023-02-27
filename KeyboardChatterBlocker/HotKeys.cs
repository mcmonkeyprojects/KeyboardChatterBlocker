using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Security.Permissions;
using System.Xml.Linq;

namespace KeyboardChatterBlocker
{
    public static class HotKeys
    {
        [Flags]
        public enum KeyModifiers : uint
        {
            None = 0,
            Alt = 1,
            Control = 2,
            Shift = 4,
            Windows = 8
        }

        public static class Internal
        {
            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern bool RegisterHotKey(IntPtr hWnd, int id, KeyModifiers fsModifiers, Keys vk);

            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern int UnregisterHotKey(IntPtr hWnd, int id);

            public static IntPtr RegisteredForm;

            public static int LastId = 100;

            public const int WM_HOTKEY = 0x0312;

            [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
            public class MessageFilter : IMessageFilter
            {
                public bool PreFilterMessage(ref Message m)
                {
                    if (m.Msg == WM_HOTKEY && m.HWnd == RegisteredForm)
                    {
                        if (m.Msg == WM_HOTKEY)
                        {
                            if (RegisteredKeyIds.TryGetValue((int) m.WParam, out Action hotkeyAction))
                            {
                                hotkeyAction();
                            }
                        }
                        return true;
                    }
                    return false;
                }
            }
        }

        public static Dictionary<int, Action> RegisteredKeyIds = new Dictionary<int, Action>();

        public static void ClearAll()
        {
            foreach (int id in RegisteredKeyIds.Keys)
            {
                Internal.UnregisterHotKey(Internal.RegisteredForm, id);
            }
            RegisteredKeyIds.Clear();
        }

        public static int Register(string combo, Action action)
        {
            int id = Internal.LastId++;
            Keys vk = 0;
            KeyModifiers modifiers = 0;
            string[] keys = combo.Split('+').Select(k => k.Trim().ToLowerInvariant()).Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();
            foreach (string key in keys)
            {
                switch (key)
                {
                    case "alt": modifiers |= KeyModifiers.Alt; break;
                    case "control": modifiers |= KeyModifiers.Control; break;
                    case "shift": modifiers |= KeyModifiers.Shift; break;
                    case "win": modifiers |= KeyModifiers.Windows; break;
                    default:
                        if (vk != 0)
                        {
                            throw new Exception("Invalid key combo - cannot have multiple non-modifier keys");
                        }
                        vk = (Keys) Enum.Parse(typeof(Keys), key, true);
                        break;
                }
            }
            Internal.RegisteredForm = Program.MainForm.Handle;
            Internal.RegisterHotKey(Internal.RegisteredForm, id, modifiers, vk);
            RegisteredKeyIds.Add(id, action);
            return id;
        }
    }
}
