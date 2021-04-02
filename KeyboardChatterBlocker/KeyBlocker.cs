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
        /// Path of the local app data config file IF used.
        /// </summary>
        private static readonly string LOCAL_APP_DATA_PATH = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}/KeyboardChatterBlocker/config.txt";

        /// <summary>
        /// Location of the config file.
        /// </summary>
        public static readonly string CONFIG_FILE = Application.ExecutablePath.Contains("Program Files") ? LOCAL_APP_DATA_PATH : "./config.txt";

        /// <summary>
        /// External Windows API call. Gets the current tick count as a 64-bit (ulong) value.
        /// Similar to <see cref="Environment.TickCount"/> but less broken.
        /// </summary>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern ulong GetTickCount64();

        /// <summary>
        /// The relevant <see cref="KeyboardInterceptor"/> instance.
        /// </summary>
        public KeyboardInterceptor Interceptor;

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
        /// Enables the internal mouse hook if it's needed, and disables if it's not.
        /// </summary>
        public void AutoEnableMouse()
        {
            if (KeysToChatterTime[KeysHelper.KEY_MOUSE_LEFT].HasValue || KeysToChatterTime[KeysHelper.KEY_MOUSE_RIGHT].HasValue
                || KeysToChatterTime[KeysHelper.KEY_MOUSE_MIDDLE].HasValue
                || KeysToChatterTime[KeysHelper.KEY_MOUSE_FORWARD].HasValue || KeysToChatterTime[KeysHelper.KEY_MOUSE_BACKWARD].HasValue)
            {
                Interceptor.EnableMouseHook();
            }
            else
            {
                Interceptor.DisableMouseHook();
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
                if (!KeysHelper.TryGetKey(settingName.Substring("key.".Length), out Keys key))
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
                case "auto_disable_programs":
                    AutoDisablePrograms.AddRange(settingValue.ToLowerInvariant().Split('/'));
                    break;
            }
        }

        /// <summary>
        /// Saves the configuration data to file.
        /// </summary>
        public void SaveConfig()
        {
            AutoEnableMouse();
            string saveStr = GetConfigurationString();
            Directory.CreateDirectory(Path.GetDirectoryName(CONFIG_FILE));
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
            result.Append("hide_in_system_tray: ").Append(Program.HideInSystemTray ? "true" : "false").Append("\n");
            result.Append("\n");
            foreach (KeyValuePair<Keys, uint?> chatterTimes in KeysToChatterTime.MainDictionary)
            {
                if (!chatterTimes.Value.HasValue)
                {
                    continue;
                }
                result.Append("key.").Append(chatterTimes.Key.Stringify()).Append(": ").Append(chatterTimes.Value.Value).Append("\n");
            }
            if (AutoDisablePrograms.Count > 0)
            {
                result.Append("auto_disable_programs: ").Append(string.Join("/", AutoDisablePrograms));
            }
            return result.ToString();
        }

        /// <summary>
        /// Event for when a key is blocked.
        /// </summary>
        public Action<KeyBlockedEventArgs> KeyBlockedEvent;

        /// <summary>
        /// If this is true, some feature (such as an open program in the auto-disable-programs list) is causing the blocker to automatically disable.
        /// </summary>
        public bool IsAutoDisabled = false;

        /// <summary>
        /// Whether the blocker is currently enabled.
        /// </summary>
        public bool IsEnabled = false;

        /// <summary>
        /// A set of program executable names that will cause the blocker to automatically disable if they are open.
        /// </summary>
        public List<string> AutoDisablePrograms = new List<string>();

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
        /// A mapping of keys to total press count, for statistics tracking.
        /// </summary>
        public AcceleratedKeyMap<int> StatsKeyCount = new AcceleratedKeyMap<int>();

        /// <summary>
        /// A mapping of keys to total chatter count, for statistics tracking.
        /// </summary>
        public AcceleratedKeyMap<int> StatsKeyChatter = new AcceleratedKeyMap<int>();

        /// <summary>
        /// A mapping of keys to a bool indicating if they are thought to be down (to catch holding down a key and not bork it).
        /// </summary>
        public AcceleratedKeyMap<bool> KeyIsDown = new AcceleratedKeyMap<bool>();

        /// <summary>
        /// Whether any key presses have occurred (and thus stats have changed).
        /// </summary>
        public bool AnyKeyChange = false;

        /// <summary>
        /// Called when a key-down event is detected, to decide whether to allow it through.
        /// </summary>
        /// <param name="key">The key being pressed.</param>
        /// <param name="defaultZero">If true, defaults to zero instead of <see cref="GlobalChatterTimeLimit"/>.</param>
        /// <returns>True to allow the press, false to deny it.</returns>
        public bool AllowKeyDown(Keys key, bool defaultZero)
        {
            if (!IsEnabled || IsAutoDisabled) // Not enabled = allow everything through.
            {
                return true;
            }
            AnyKeyChange = true;
            if (KeyIsDown[key]) // Key seems already down = key is being held, not chattering, so allow it.
            {
                return true;
            }
            KeyIsDown[key] = true;
            StatsKeyCount[key]++;
            ulong timeNow = GetTickCount64();
            ulong timeLast = KeysToLastPressTime[key];
            if (timeLast > timeNow) // In the future = number handling mixup, just allow it.
            {
                KeysToLastPressTime[key] = timeNow;
                return true;
            }
            uint maxTime = KeysToChatterTime[key] ?? (defaultZero ? 0 : GlobalChatterTimeLimit);
            if (timeNow >= timeLast + maxTime) // Time past the chatter limit = enough delay passed, allow it.
            {
                KeysToLastPressTime[key] = timeNow;
                return true;
            }
            if (timeNow <= timeLast + maxTime - 9000) // If more than 9 seconds behind, something's gone wrong, so let through anyway.
            {
                KeysToLastPressTime[key] = timeNow;
                return true;
            }
            // All else = not enough time elapsed, deny it.
            StatsKeyChatter[key]++;
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
            if (!IsEnabled || IsAutoDisabled) // Not enabled = allow everything through.
            {
                return true;
            }
            KeyIsDown[key] = false;
            if (!KeysWereDownBlocked[key]) // Down wan't blocked = allow it.
            {
                return true;
            }
            KeysWereDownBlocked[key] = false;
            return true;
        }
    }
}
