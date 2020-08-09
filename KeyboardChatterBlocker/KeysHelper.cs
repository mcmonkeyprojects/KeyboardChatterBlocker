using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyboardChatterBlocker
{
    /// <summary>
    /// Helper class for key type handling.
    /// </summary>
    public static class KeysHelper
    {
        /// <summary>
        /// 'Keys' enum values offset from the main enum option, as stand-ins for mouse keys.
        /// </summary>
        public static Keys KEY_MOUSE_LEFT = (Keys)513, KEY_MOUSE_RIGHT = (Keys)514, KEY_MOUSE_MIDDLE = (Keys)515,
            KEY_MOUSE_FORWARD = (Keys)516, KEY_MOUSE_BACKWARD = (Keys)517;

        /// <summary>
        /// Tries to get the key for the given name.
        /// </summary>
        /// <param name="name">The name of the key to get.</param>
        /// <param name="key">The gotten key output.</param>
        /// <returns>True if a key is gotten, false if not.</returns>
        public static bool TryGetKey(string name, out Keys key)
        {
            if (Enum.TryParse(name, true, out key))
            {
                return true;
            }
            string lowered = name.ToLowerInvariant();
            if (lowered == "mouse_left")
            {
                key = KEY_MOUSE_LEFT;
                return true;
            }
            else if (lowered == "mouse_right")
            {
                key = KEY_MOUSE_RIGHT;
                return true;
            }
            else if (lowered == "mouse_middle")
            {
                key = KEY_MOUSE_MIDDLE;
                return true;
            }
            else if (lowered == "mouse_forward")
            {
                key = KEY_MOUSE_FORWARD;
                return true;
            }
            else if (lowered == "mouse_backward")
            {
                key = KEY_MOUSE_BACKWARD;
                return true;
            }
            key = default;
            return false;
        }

        /// <summary>
        /// Gets the string representation of a key.
        /// </summary>
        /// <param name="key">The key to stringify.</param>
        public static string Stringify(this Keys key)
        {
            if ((int)key >= 513 && (int)key <= 517)
            {
                switch ((int)key)
                {
                    case 513: return "mouse_left";
                    case 514: return "mouse_right";
                    case 515: return "mouse_middle";
                    case 516: return "mouse_forward";
                    case 517: return "mouse_backward";
                }
            }
            return key.ToString();
        }
    }
}
