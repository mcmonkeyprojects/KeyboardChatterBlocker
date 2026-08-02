using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace KeyboardChatterBlocker
{
    /// <summary>
    /// Class that handles deciding what key press to allow through or not.
    /// </summary>
    public class KeyBlocker
    {
        /// <summary>
        /// Folder path where config and related files will be stored.
        /// </summary>
        public static readonly string ConfigFolder = Application.ExecutablePath.Contains("Program Files") ? $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}/KeyboardChatterBlocker" : Path.GetDirectoryName(Application.ExecutablePath);

        /// <summary>
        /// Location of the config file.
        /// </summary>
        public static readonly string CONFIG_FILE = $"{ConfigFolder}/config.txt";

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
        /// Map of hotkey ID to keymapping.
        /// </summary>
        public Dictionary<string, string> Hotkeys = new Dictionary<string, string>();

        /// <summary>
        /// Enum to represent when to measure the time delay from.
        /// </summary>
        public enum MeasureFrom
        {
            Press, Release
        }

        /// <summary>
        /// When to measure the time delay from.
        /// </summary>
        public MeasureFrom MeasureMode = MeasureFrom.Press;

        /// <summary>
        /// If marked, the blocker is disabled, temporarily.
        /// </summary>
        public bool TempDisable = false;

        /// <summary>
        /// Whether to exclude keyboard events that look like they were injected.
        /// </summary>
        public bool ExcludeInjected = false;

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
                        try
                        {
                            ApplyConfigSetting(line);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Could not apply setting: {line}:\n{ex}", "Failed to load config", MessageBoxButtons.OK);
                            Program.Close();
                            return;
                        }
                    }
                }
            }
            if (SaveStats)
            {
                try
                {
                    LoadStatsFromFile();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Could not load stats file - delete 'blocker_stats.csv' to bypass this error:\n{ex}", "Failed to load stats", MessageBoxButtons.OK);
                    Program.Close();
                    return;
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
                || KeysToChatterTime[KeysHelper.KEY_MOUSE_FORWARD].HasValue || KeysToChatterTime[KeysHelper.KEY_MOUSE_BACKWARD].HasValue
                || KeysToChatterTime[KeysHelper.KEY_WHEEL_CHANGE].HasValue)
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
                case "minimum_chatter_time":
                    MinimumChatterTime = uint.Parse(settingValue);
                    break;
                case "hide_in_system_tray":
                    Program.HideInSystemTray = SettingAsBool(settingValue);
                    break;
                case "auto_disable_programs":
                    AutoDisablePrograms.AddRange(settingValue.ToLowerInvariant().Split('/'));
                    break;
                case "auto_disable_on_fullscreen":
                    AutoDisableOnFullscreen = SettingAsBool(settingValue);
                    break;
                case "other_key_resets_timeout":
                    OtherKeyResetsTimeout = SettingAsBool(settingValue);
                    break;
                case "hotkey_toggle":
                    Hotkeys["toggle"] = settingValue;
                    HotKeys.Register(settingValue, () => Program.MainForm.SetEnabled(!IsEnabled));
                    break;
                case "hotkey_enable":
                    Hotkeys["enable"] = settingValue;
                    HotKeys.Register(settingValue, () => Program.MainForm.SetEnabled(true));
                    break;
                case "hotkey_disable":
                    Hotkeys["disable"] = settingValue;
                    HotKeys.Register(settingValue, () => Program.MainForm.SetEnabled(false));
                    break;
                case "hotkey_tempenable":
                    Hotkeys["tempenable"] = settingValue;
                    HotKeys.Register(settingValue, () => TempDisable = false);
                    break;
                case "hotkey_tempdisable":
                    Hotkeys["tempdisable"] = settingValue;
                    HotKeys.Register(settingValue, () => TempDisable = true);
                    break;
                case "hotkey_showform":
                    Hotkeys["showform"] = settingValue;
                    HotKeys.Register(settingValue, () =>
                    {
                        if (Program.MainForm.Visible)
                        {
                            Program.MainForm.HideForm();
                        }
                        else
                        {
                            Program.MainForm.ShowForm();
                        }
                    });
                    break;
                case "hotkey_tempblock":
                    BlockAllInputsKeySet = settingValue.Split('+').Select(s => s.Trim().ToLowerInvariant()).Where(s => !string.IsNullOrWhiteSpace(s)).Select(s => (Keys)Enum.Parse(typeof(Keys), s, true)).ToArray();
                    break;
                case "measure_from":
                    MeasureMode = (MeasureFrom)Enum.Parse(typeof(MeasureFrom), settingValue, true);
                    break;
                case "disable_tray_icon":
                    Program.DisableTrayIcon = SettingAsBool(settingValue);
                    break;
                case "save_stats":
                    SaveStats = SettingAsBool(settingValue);
                    break;
                case "exclude_injected":
                    ExcludeInjected = SettingAsBool(settingValue);
                    break;
            }
        }

        /// <summary>
        /// Saves the configuration data to file.
        /// </summary>
        /// <param name="shouldVerify">If true, the user directly triggered a save, and a failure should ask the user to check. If false, this is a background save.</param>
        public void SaveConfig(bool shouldVerify = false)
        {
            AutoEnableMouse();
            string saveStr = GetConfigurationString();
            Directory.CreateDirectory(Path.GetDirectoryName(CONFIG_FILE));
            File.WriteAllText(CONFIG_FILE, saveStr);
            if (SaveStats)
            {
                SaveStatsToFile(shouldVerify);
            }
        }

        /// <summary>
        /// Gets the full configuration string for the current setup.
        /// </summary>
        public string GetConfigurationString()
        {
            StringBuilder result = new StringBuilder(2048);
            result.Append("# KeyboardChatterBlocker configuration file\n");
            result.Append("# View README file at https://github.com/FreneticLLC/KeyboardChatterBlocker\n");
            result.Append("\n");
            result.Append("is_enabled: ").Append(IsEnabled ? "true" : "false").Append("\n");
            result.Append("global_chatter: ").Append(GlobalChatterTimeLimit).Append("\n");
            result.Append("hide_in_system_tray: ").Append(Program.HideInSystemTray ? "true" : "false").Append("\n");
            if (Program.DisableTrayIcon)
            {
                result.Append("disable_tray_icon: true\n");
            }
            if (SaveStats)
            {
                result.Append($"save_stats: true\n");
            }
            result.Append($"measure_from: {MeasureMode}\n");
            result.Append($"minimum_chatter_time: {MinimumChatterTime}\n");
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
                result.Append("auto_disable_programs: ").Append(string.Join("/", AutoDisablePrograms)).Append("\n");
            }
            result.Append("auto_disable_on_fullscreen: ").Append(AutoDisableOnFullscreen ? "true" : "false").Append("\n");
            result.Append("other_key_resets_timeout: ").Append(OtherKeyResetsTimeout ? "true" : "false").Append("\n");
            result.Append("exclude_injected: ").Append(ExcludeInjected ? "true" : "false").Append("\n");
            result.Append("\n");
            foreach (KeyValuePair<string, string> pair in Hotkeys)
            {
                result.Append($"hotkey_{pair.Key}: {pair.Value}\n");
            }
            if (BlockAllInputsKeySet != null)
            {
                result.Append($"hotkey_tempblock: {string.Join(" + ", BlockAllInputsKeySet)}\n");
            }
            return result.ToString();
        }

        /// <summary>
        /// A special set of keys that when pressed together will block all other key or mouse inputs until released.
        /// </summary>
        public Keys[] BlockAllInputsKeySet = null;

        /// <summary>
        /// Whether ALL key inputs should be temporarily blocked.
        /// </summary>
        public bool ShouldBlockAll => BlockAllInputsKeySet != null && BlockAllInputsKeySet.Length > 0 && BlockAllInputsKeySet.All(k => KeyIsDown[k]);

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
        /// A mapping of keys to the last release time.
        /// </summary>
        public AcceleratedKeyMap<ulong> KeysToLastReleaseTime = new AcceleratedKeyMap<ulong>();

        /// <summary>
        /// The global chatter time limit, in milliseconds.
        /// </summary>
        public uint GlobalChatterTimeLimit = 100;

        /// <summary>
        /// The global minimum chatter time, in milliseconds.
        /// </summary>
        public uint MinimumChatterTime = 0;

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
        /// Whether to automatically disable the blocker when any program is full screen.
        /// </summary>
        public bool AutoDisableOnFullscreen = false;

        /// <summary>
        /// If true, reset timeouts for keys when another key is pressed.
        /// </summary>
        public bool OtherKeyResetsTimeout = false;

        /// <summary>
        /// If true, save persistent stats to file.
        /// </summary>
        public bool SaveStats = false;

        /// <summary>
        /// Ticker, how many seconds since the last automatic stats save.
        /// </summary>
        public int SaveStatsTicker = 0;

        /// <summary>
        /// Whether any key presses have occurred (and thus stats have changed).
        /// </summary>
        public bool AnyKeyChange = false;

        /// <summary>
        /// The last key pressed, for <see cref="OtherKeyResetsTimeout"/>.
        /// </summary>
        public static Keys LastPressed = Keys.None;

        /// <summary>
        /// Action to play a sound when chatter is detected.
        /// </summary>
        public static Action PlayNotification = KBCUtils.GetSoundPlayer("chatter.wav");

        /// <summary>
        /// Path of the stats data file from either local app data or the same directory as the executable
        /// </summary>
        private static readonly string BlockerStatsFilePath = $"{ConfigFolder}/blocker_stats.csv";

        /// <summary>
        /// Save stats data to file.
        /// </summary>
        /// <param name="shouldVerify">If true, the user directly triggered a save, and a failure should ask the user to check. If false, this is a background save.</param>
        public void SaveStatsToFile(bool shouldVerify)
        {
            StringBuilder output = new StringBuilder();
            foreach (KeyValuePair<Keys, int> keyData in StatsKeyCount.MainDictionary)
            {
                int chatterTotal = StatsKeyChatter[keyData.Key];
                output.Append($"{keyData.Key.Stringify()},{keyData.Value},{chatterTotal},\n");
            }
            try
            {
                File.WriteAllText(BlockerStatsFilePath, output.ToString());
            }
            catch (Exception ex)
            {
                if (!shouldVerify)
                {
                    return;
                }
                Console.WriteLine($"Stats failed to save: {ex}");
                MessageBox.Show($"Could not save stats file: is the data folder invalid, or have you opened the CSV in locking software?\nClose any locks before pressing OK to retry.", "Failed to save stats", MessageBoxButtons.OK);
                try
                {
                    File.WriteAllText(BlockerStatsFilePath, output.ToString());
                }
                catch (Exception)
                {
                    // Ignore second failure
                }
            }
        }

        /// <summary>
        /// Load stats data from file.
        /// </summary>
        public void LoadStatsFromFile()
        {
            if (!File.Exists(BlockerStatsFilePath))
            {
                return;
            }
            string[] lines = File.ReadAllText(BlockerStatsFilePath).Replace('\r', '\n').Split('\n');
            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }
                string[] parts = line.Split(',');
                if (parts.Length < 3
                    || !Enum.TryParse(parts[0], out Keys key)
                    || !int.TryParse(parts[1], out int keyCount)
                    || !int.TryParse(parts[2], out int chatterTotal))
                {
                    continue;
                }
                StatsKeyCount[key] = keyCount;
                StatsKeyChatter[key] = chatterTotal;
            }
        }

        /// <summary>
        /// Called when a key-down event is detected, to decide whether to allow it through.
        /// </summary>
        /// <param name="key">The key being pressed.</param>
        /// <param name="defaultZero">If true, defaults to zero instead of <see cref="GlobalChatterTimeLimit"/>.</param>
        /// <returns>True to allow the press, false to deny it.</returns>
        public bool AllowKeyDown(Keys key, bool defaultZero)
        {
            if (!IsEnabled || IsAutoDisabled || TempDisable) // Not enabled = allow everything through.
            {
                return true;
            }
            uint? chatterTimeLimit = KeysToChatterTime[key];
            if (!ShouldBlockAll && !OtherKeyResetsTimeout && GlobalChatterTimeLimit == 0 && !chatterTimeLimit.HasValue) // Explicit no reason to listen to this key = discard fast, no tracking.
            {
                return true;
            }
            AnyKeyChange = true;
            if (KeyIsDown[key]) // Key seems already down = key is being held, not chattering, so allow it.
            {
                return true;
            }
            KeyIsDown[key] = true;
            if (GlobalChatterTimeLimit != 0 || chatterTimeLimit.HasValue) // Only track stats if it's a monitored key.
            {
                StatsKeyCount[key]++;
            }
            ulong timeNow = GetTickCount64();
            ulong timeLast = MeasureMode == MeasureFrom.Release ? KeysToLastReleaseTime[key] : KeysToLastPressTime[key];
            if (ShouldBlockAll)
            {
                return false;
            }
            if (OtherKeyResetsTimeout)
            {
                if (key != LastPressed)
                {
                    LastPressed = key;
                    KeysToLastPressTime[key] = timeNow;
                    return true;
                }
                LastPressed = key;
            }
            if (timeLast > timeNow) // In the future = number handling mixup, just allow it.
            {
                KeysToLastPressTime[key] = timeNow;
                return true;
            }
            uint maxTime = chatterTimeLimit ?? (defaultZero ? 0 : GlobalChatterTimeLimit);
            ulong timePassed = unchecked(timeNow - timeLast); // unchecked means if time runs backwards it will wrap to super large and be ignored
            if (timePassed >= maxTime // Time past the chatter limit = enough delay passed, allow it.
                || timePassed < MinimumChatterTime) // Or too fast (below user configured minimum) = possible bug or similar oddity, allow it.
            {
                KeysToLastPressTime[key] = timeNow;
                return true;
            }
            // All else = not enough time elapsed, deny it.
            StatsKeyChatter[key]++;
            KeysWereDownBlocked[key] = true;
            KeyBlockedEvent?.Invoke(new KeyBlockedEventArgs() { Key = key, Time = (uint)timePassed });
            PlayNotification();
            return false;
        }

        /// <summary>
        /// Called when a key-up event is detected, to decide whether to allow it through.
        /// </summary>
        /// <param name="key">The key being released.</param>
        /// <returns>True to allow the key-up, false to deny it.</returns>
        public bool AllowKeyUp(Keys key)
        {
            ulong timeNow = GetTickCount64();
            if (!IsEnabled || IsAutoDisabled || TempDisable) // Not enabled = allow everything through.
            {
                KeysToLastReleaseTime[key] = timeNow;
                return true;
            }
            KeyIsDown[key] = false;
            if (ShouldBlockAll)
            {
                return false;
            }
            if (!KeysWereDownBlocked[key]) // Down wasn't blocked = allow it.
            {
                KeysToLastReleaseTime[key] = timeNow;
                return true;
            }
            KeysWereDownBlocked[key] = false;
            if (key == KeysHelper.KEY_MOUSE_FORWARD || key == KeysHelper.KEY_MOUSE_BACKWARD) // Forward/Backward listeners listen to the Up, not the Down, so must be blocked
            {
                return false;
            }
            // In most cases, it's better to just let the duplicate 'up' through anyway.
            return true;
        }
    }
}
