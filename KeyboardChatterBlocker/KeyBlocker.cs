using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;

namespace KeyboardChatterBlocker
{
    /// <summary>
    /// Class that handles deciding what key press to allow through or not.
    /// </summary>
    public class KeyBlocker
    {
        /// <summary>
        /// Location of the config file.
        /// </summary>
        public const string CONFIG_FILE = "./config.txt";

        /// <summary>
        /// External Windows API call. Gets the current tick count as a 64-bit (ulong) value.
        /// Similar to <see cref="Environment.TickCount"/> but less broken.
        /// </summary>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern ulong GetTickCount64();

        /// <summary>
        /// Load the <see cref="KeyBlocker"/> from config file settings.
        /// </summary>
        public KeyBlocker()
        {
            if (File.Exists(CONFIG_FILE))
            {
                string[] settings = File.ReadAllText(CONFIG_FILE).Replace("\r\n", "\n").Replace("\r", "").Split('\n');
                foreach (string line in settings)
                {
                    if (!string.IsNullOrWhiteSpace(line) && !line.StartsWith("#"))
                    {
                        ApplyConfigSetting(line);
                    }
                }
            }
        }

        /// <summary>
        /// Gets the boolean value of a setting string.
        /// </summary>
        /// <param name="setting">The setting string.</param>
        /// <returns>The boolean value result.</returns>
        public static bool SettingAsBool(string setting)
        {
            return setting.ToLowerInvariant() == "true";
        }

        /// <summary>
        /// Applies a setting line from a config file.
        /// </summary>
        /// <param name="setting">The setting line.</param>
        public void ApplyConfigSetting(string setting)
        {
            int colonIndex = setting.IndexOf(':');
            if (colonIndex == -1)
            {
                return;
            }
            string settingName = setting.Substring(0, colonIndex).Trim();
            string settingValue = setting.Substring(colonIndex + 1).Trim();
            if (settingName.StartsWith("key."))
            {
                if (!Enum.TryParse(settingName.Substring("key.".Length), out Keys key))
                {
                    MessageBox.Show("Config file contains setting '" + setting + "', which names an invalid key.", "KeyboardChatterBlocker Configuration Error", MessageBoxButtons.OK);
                    return;
                }
                KeysToChatterTime[key] = uint.Parse(settingValue);
                return;
            }
            switch (settingName)
            {
                case "is_enabled":
                    IsEnabled = SettingAsBool(settingValue);
                    break;
                case "global_chatter":
                    GlobalChatterTimeLimit = uint.Parse(settingValue);
                    break;
                case "hide_in_system_tray":
                    Program.HideInSystemTray = SettingAsBool(settingValue);
                    break;
            }
        }

        /// <summary>
        /// Saves the configuration data to file.
        /// </summary>
        public void SaveConfig()
        {
            string saveStr = GetConfigurationString();
            File.WriteAllText(CONFIG_FILE, saveStr);
        }

        /// <summary>
        /// Gets the full configuration string for the current setup.
        /// </summary>
        public string GetConfigurationString()
        {
            StringBuilder result = new StringBuilder(2048);
            result.Append("# KeyboardChatterBlocker configuration file\n");
            result.Append("# View README file at https://github.com/mcmonkeyprojects/KeyboardChatterBlocker\n");
            result.Append("\n");
            result.Append("is_enabled: ").Append(IsEnabled ? "true" : "false").Append("\n");
            result.Append("global_chatter: ").Append(GlobalChatterTimeLimit).Append("\n");
            result.Append("hide_in_system_tray: ").Append(Program.HideInSystemTray).Append("\n");
            result.Append("\n");
            foreach (KeyValuePair<Keys, uint?> chatterTimes in KeysToChatterTime.MainDictionary)
            {
                if (!chatterTimes.Value.HasValue)
                {
                    continue;
                }
                result.Append("key.").Append(chatterTimes.Key.ToString()).Append(": ").Append(chatterTimes.Value.Value).Append("\n");
            }
            return result.ToString();
        }

        /// <summary>
        /// Event for when a key is blocked.
        /// </summary>
        public Action<KeyBlockedEventArgs> KeyBlockedEvent;

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
        public uint GlobalChatterTimeLimit = 100;

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
            KeyBlockedEvent?.Invoke(new KeyBlockedEventArgs() { Key = key, Time = (uint)(timeNow - timeLast) });
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
