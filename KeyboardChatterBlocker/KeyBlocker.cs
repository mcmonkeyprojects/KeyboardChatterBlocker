using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace KeyboardChatterBlocker
{
    /// <summary>
    /// Class that handles deciding what key press to allow through or not.
    /// </summary>
    public class KeyBlocker
    {
        /// <summary>
        /// External Windows API call. Gets the current tick count as a 64-bit (ulong) value.
        /// Similar to <see cref="Environment.TickCount"/> but less broken.
        /// </summary>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern ulong GetTickCount64();

        /// <summary>
        /// Whether the blocker is currently enabled.
        /// </summary>
        public bool IsEnabled = false;

        /// <summary>
        /// A mapping of keys to the last press time.
        /// </summary>
        public AcceleratedKeyMap<ulong> KeysToLastPressTime = new AcceleratedKeyMap<ulong>();

        /// <summary>
        /// The global chatter time limit, in milliseconds.
        /// </summary>
        public uint GlobalChatterTimeLimit = 50;

        /// <summary>
        /// A mapping of keys to their allowed chatter time, in milliseconds. If HasValue is false, use global chatter time limit.
        /// </summary>
        public AcceleratedKeyMap<uint?> KeysToChatterTime = new AcceleratedKeyMap<uint?>();

        /// <summary>
        /// A mapping of keys to a bool indicating whether a down-stroke was blocked (so the up-stroke can be blocked as well).
        /// </summary>
        public AcceleratedKeyMap<bool> KeysWereDownBlocked = new AcceleratedKeyMap<bool>();

        /// <summary>
        /// Called when a key-down event is detected, to decide whether to allow it through.
        /// </summary>
        /// <param name="key">The key being pressed.</param>
        /// <returns>True to allow the press, false to deny it.</returns>
        public bool AllowKeyDown(Keys key)
        {
            if (!IsEnabled) // Not enabled = allow everything through.
            {
                return true;
            }
            ulong timeNow = GetTickCount64();
            ulong timeLast = KeysToLastPressTime[key];
            KeysToLastPressTime[key] = timeNow;
            if (timeLast > timeNow) // In the future = number handling mixup, just allow it.
            {
                return true;
            }
            uint maxTime = KeysToChatterTime[key] ?? GlobalChatterTimeLimit;
            if (timeNow > timeLast + maxTime) // Time past the chatter limit = enough delay passed, allow it.
            {
                return true;
            }
            // All else = not enough time elapsed, deny it.
            KeysWereDownBlocked[key] = true;
            return false;
        }

        /// <summary>
        /// Called when a key-up event is detected, to decide whether to allow it through.
        /// </summary>
        /// <param name="key">The key being released.</param>
        /// <returns>True to allow the key-up, false to deny it.</returns>
        public bool AllowKeyUp(Keys key)
        {
            if (!IsEnabled) // Not enabled = allow everything through.
            {
                return true;
            }
            if (!KeysWereDownBlocked[key]) // Down wan't blocked = allow it.
            {
                return true;
            }
            KeysWereDownBlocked[key] = false;
            return true;
        }
    }
}
