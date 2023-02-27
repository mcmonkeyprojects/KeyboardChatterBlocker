using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Globalization;

namespace KeyboardChatterBlocker
{
    /// <summary>
    /// The user-interface control form.
    /// </summary>
    public partial class MainBlockerForm : Form
    {
        /// <summary>
        /// Whether the form is still loading.
        /// </summary>
        public bool Loading = true;

        /// <summary>
        /// Timer that automatically updates the stats view ocassionally, if it's visible.
        /// </summary>
        public Timer StatsUpdateTimer;

        /// <summary>
        /// Whether the form is currently hidden from view.
        /// </summary>
        public bool IsHidden => !Visible;

        /// <summary>
        /// Shows the form fully and properly.
        /// </summary>
        public void ShowForm()
        {
            Visible = true;
        }

        /// <summary>
        /// Hides the form fully and properly.
        /// </summary>
        public void HideForm()
        {
            TrayIcon.Visible = true;
            Visible = false;
        }

        /// <summary>
        /// Init the form.
        /// </summary>
        public MainBlockerForm()
        {
            if (Program.HideInSystemTray)
            {
                WindowState = FormWindowState.Minimized;
                ShowInTaskbar = false;
            }
            Program.Blocker.KeyBlockedEvent += LogKeyBlocked;
            InitializeComponent();
            versionAboutLabel.Text = "Version: " + Application.ProductVersion;
        }

        /// <summary>
        /// Method auto-called (by event) for when a key is blocked.
        /// </summary>
        /// <param name="e">The key blocked event details.</param>
        public void LogKeyBlocked(KeyBlockedEventArgs e)
        {
            ChatterLogGrid.Rows.Add(DateTime.Now.ToString("MM/dd HH:mm:ss", CultureInfo.InvariantCulture), e.Key.ToString(), e.Time.ToString(), "[Edit]");
        }

        /// <summary>
        /// Event method auto-called when the "Enable" check box is touched.
        /// </summary>
        public void EnabledCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (Loading)
            {
                return;
            }
            Program.Blocker.IsEnabled = EnabledCheckbox.Checked;
            Program.Blocker.SaveConfig();
        }

        /// <summary>
        /// Event method auto-called when the "Chatter Threshold" box is touched.
        /// </summary>
        public void ChatterThresholdBox_ValueChanged(object sender, EventArgs e)
        {
            if (Loading)
            {
                return;
            }
            Program.Blocker.GlobalChatterTimeLimit = (uint)ChatterThresholdBox.Value;
            Program.Blocker.SaveConfig();
        }

        /// <summary>
        /// Event method auto-called when the tray icon is touched.
        /// </summary>
        public void TrayIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowForm();
        }

        /// <summary>
        /// Event method auto-called when the "Start In Tray" box is touched.
        /// </summary>
        public void TrayIconCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (Loading)
            {
                return;
            }
            Program.HideInSystemTray = TrayIconCheckbox.Checked;
            Program.Blocker.SaveConfig();
        }

        /// <summary>
        /// Location of the Windows startup folder (within the APPDATA environment variable).
        /// </summary>
        public const string STARTUP_FOLDER = "/Microsoft/Windows/Start Menu/Programs/Startup/";

        public static string StartupLinkPath => Environment.GetEnvironmentVariable("appdata") + STARTUP_FOLDER + "KeyboardChatterBlocker.lnk";

        /// <summary>
        /// Event method auto-called when the "Start With Windows" box is touched.
        /// </summary>
        public void StartWithWindowsCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (Loading)
            {
                return;
            }
            if (StartWithWindowsCheckbox.Checked)
            {
                string folder = Environment.GetEnvironmentVariable("appdata") + STARTUP_FOLDER;
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                IWshRuntimeLibrary.IWshShortcut shortcut = (IWshRuntimeLibrary.IWshShortcut)new IWshRuntimeLibrary.WshShell().CreateShortcut(StartupLinkPath);
                shortcut.Description = "Auto-Start Keyboard Chatter Blocker.";
                shortcut.TargetPath = Application.ExecutablePath;
                shortcut.WorkingDirectory = Path.GetDirectoryName(Application.ExecutablePath);
                shortcut.Save();
            }
            else
            {
                if (File.Exists(StartupLinkPath))
                {
                    File.Delete(StartupLinkPath);
                }
            }
        }

        public void SetAutoDisable(bool disable, string reason)
        {
            if (Program.Blocker.IsAutoDisabled == disable)
            {
                if (disable)
                {
                    string disableText = $"(Automatically disabled due to {reason})";
                    if (EnableNoteLabel.Text != disableText) // Redundant check to discourage redraw
                    {
                        EnableNoteLabel.Text = disableText;
                    }
                }
                return;
            }
            Program.Blocker.IsAutoDisabled = disable;
            if (disable)
            {
                Program.Blocker.Interceptor.DisableKeyboardHook();
                Program.Blocker.Interceptor.DisableMouseHook();
                EnableNoteLabel.Text = $"(Automatically disabled due to {reason})";
                EnableNoteLabel.BackColor = Color.FromArgb(64, 255, 0, 0);
            }
            else
            {
                Program.Blocker.Interceptor.EnableKeyboardHook();
                Program.Blocker.AutoEnableMouse();
                EnableNoteLabel.Text = "";
                EnableNoteLabel.BackColor = Color.Transparent;
                foreach (string notBlocking in Program.Blocker.AutoDisablePrograms)
                {
                    SetAutoDisableProgramHighlight(notBlocking, false);
                }
            }
        }

        public void SetAutoDisableProgramHighlight(string program, bool isBlocking)
        {
            foreach (ListViewItem item in AutoDisableProgramsList.Items)
            {
                if (item.Text == program)
                {
                    item.BackColor = isBlocking ? Color.FromArgb(255, 128, 128) : Color.Transparent;
                }
            }
        }

        /// <summary>
        /// Check whether to auto-disable, and apply the correct value.
        /// </summary>
        public void CheckAutoDisable()
        {
            if (Program.Blocker.AutoDisableOnFullscreen)
            {
                if (FullScreenDetectHelper.IsFullscreen())
                {
                    SetAutoDisable(true, "fullscreen application");
                    foreach (ListViewItem item in AutoDisableProgramsList.Items)
                    {
                        item.BackColor = Color.Transparent;
                    }
                    return;
                }
            }
            HashSet<string> programsToCheck = new HashSet<string>(Program.Blocker.AutoDisablePrograms);
            if (programsToCheck.Count == 0)
            {
                SetAutoDisable(false, "none");
                return;
            }
            bool any = false;
            foreach (string proc in Process.GetProcesses().Select(p => p.ProcessName.ToLowerInvariant()))
            {
                if (programsToCheck.Contains(proc))
                {
                    SetAutoDisableProgramHighlight(proc, true);
                    programsToCheck.Remove(proc);
                    SetAutoDisable(true, $"open process ({proc})");
                    any = true;
                }
            }
            if (!any)
            {
                SetAutoDisable(false, "none");
            }
            else
            {
                foreach (string notBlocking in programsToCheck)
                {
                    SetAutoDisableProgramHighlight(notBlocking, false);
                }
            }
        }

        /// <summary>
        /// Event method auto-called when the form loads.
        /// </summary>
        public void MainBlockerForm_Load(object sender, EventArgs e)
        {
            EnableNoteLabel.Text = "";
            EnableNoteLabel.BackColor = Color.Transparent;
            TrayIconCheckbox.Checked = Program.HideInSystemTray;
            if (Program.HideInSystemTray)
            {
                WindowState = FormWindowState.Minimized;
                ShowInTaskbar = false;
                TrayIcon.Visible = true;
                Timer hideProperlyTimer = new Timer() { Interval = 100 };
                hideProperlyTimer.Tick += (tickSender, tickArgs) =>
                {
                    WindowState = FormWindowState.Normal;
                    ShowInTaskbar = true;
                    Visible = false;
                    hideProperlyTimer.Stop();
                };
                hideProperlyTimer.Start();
            }
            Timer AutoDisableTimer = new Timer() { Interval = 2000 };
            AutoDisableTimer.Tick += (tickSender, tickArgs) => CheckAutoDisable();
            AutoDisableTimer.Start();
            AutoDisableProgramsList.Items.AddRange(Program.Blocker.AutoDisablePrograms.Select(s => new ListViewItem(s)).ToArray());
            AutoDisableOnFullscreenCheckbox.Checked = Program.Blocker.AutoDisableOnFullscreen;
            ChatterThresholdBox.Value = Program.Blocker.GlobalChatterTimeLimit;
            EnabledCheckbox.Checked = Program.Blocker.IsEnabled;
            StartWithWindowsCheckbox.Checked = File.Exists(StartupLinkPath);
            StatsUpdateTimer = new Timer { Interval = 1000 };
            StatsUpdateTimer.Tick += StatsUpdateTimer_Tick;
            StatsUpdateTimer.Start();
            PushKeysToGrid();
            Loading = false;
            CheckAutoDisable();
        }

        /// <summary>
        /// Automatic stats update timer, when the stats view is visible.
        /// </summary>
        public void StatsUpdateTimer_Tick(object sender, EventArgs e)
        {
            if (Program.Blocker.AnyKeyChange && !IsHidden && TabControl1.SelectedTab == StatsTabPage)
            {
                Program.Blocker.AnyKeyChange = false;
                PushStatsToGrid();
            }
        }

        /// <summary>
        /// If enabled, any close should fully close the form and exit the program.
        /// </summary>
        public bool ShouldForceClose = false;

        /// <summary>
        /// Event method auto-called when the form close button is pressed.
        /// </summary>
        public void MainBlockerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ShouldForceClose)
            {
                return;
            }
            if (e.CloseReason != CloseReason.UserClosing) // Don't block windows shutdown, etc.
            {
                return;
            }
            if (IsHidden) // If already hidden, any close must be an actual full close, so close.
            {
                return;
            }
            if (Program.HideInSystemTray)
            {
                e.Cancel = true;
                HideForm();
            }
        }

        /// <summary>
        /// Pushes all stats to the GUI grid.
        /// Stats are tracked locally, not directly on the grid, to avoid performance impact.
        /// This needs to be called to update stats properly.
        /// </summary>
        public void PushStatsToGrid()
        {
            StatsGrid.SuspendLayout();
            StatsGrid.Rows.Clear();
            foreach (KeyValuePair<Keys, int> keyData in Program.Blocker.StatsKeyCount.MainDictionary)
            {
                int chatterTotal = Program.Blocker.StatsKeyChatter[keyData.Key];
                string percentage = chatterTotal == 0 ? "" : ((chatterTotal * 100.0f / keyData.Value).ToString("00.00") + "%");
                StatsGrid.Rows.Add(keyData.Key.Stringify(), keyData.Value, chatterTotal, percentage);
            }
            StatsGrid.ResumeLayout(true);
        }

        /// <summary>
        /// Pushes all keys to the GUI grid.
        /// Key configurations are tracked locally, not directly on the grid, to avoid performance impact.
        /// This needs to be called to update key configuration GUI properly.
        /// </summary>
        public void PushKeysToGrid()
        {
            ConfigureKeysGrid.SuspendLayout();
            ConfigureKeysGrid.Rows.Clear();
            foreach (KeyValuePair<Keys, uint?> keyData in Program.Blocker.KeysToChatterTime.MainDictionary)
            {
                if (!keyData.Value.HasValue)
                {
                    continue;
                }
                ConfigureKeysGrid.Rows.Add(keyData.Key.Stringify(), keyData.Value.Value, "[X]");
            }
            ConfigureKeysGrid.ResumeLayout(true);
        }

        /// <summary>
        /// Event method auto-called when the tab control box is touched.
        /// </summary>
        public void TabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (Loading)
            {
                return;
            }
            PushStatsToGrid();
        }

        /// <summary>
        /// Event method auto-called when the "configure keys" box is double-clicked.
        /// </summary>
        public void ConfigureKeysGrid_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!KeysHelper.TryGetKey(ConfigureKeysGrid[0, e.RowIndex].Value.ToString(), out Keys key))
            {
                MessageBox.Show("Error: configure keys grid misconfigured, invalid key name!", "Keyboard Chatter Blocker", MessageBoxButtons.OK);
                return;
            }
            if (e.ColumnIndex == 1) // Value column
            {
                uint resultValue = Program.Blocker.KeysToChatterTime[key] ?? Program.Blocker.GlobalChatterTimeLimit;
                KeyConfigurationForm keyConfigForm = new KeyConfigurationForm()
                {
                    Key = key,
                    SetResult = (i) =>
                    {
                        resultValue = i;
                    }
                };
                keyConfigForm.ShowDialog(this);
                Program.Blocker.KeysToChatterTime[key] = resultValue;
                Program.Blocker.SaveConfig();
                ConfigureKeysGrid[1, e.RowIndex].Value = resultValue.ToString();
            }
            else if (e.ColumnIndex == 2) // Remove column
            {
                Program.Blocker.KeysToChatterTime[key] = null;
                Program.Blocker.SaveConfig();
                ConfigureKeysGrid.Rows.RemoveAt(e.RowIndex);
            }
        }

        /// <summary>
        /// Event method auto-called when the "chatter log" box has a key pressed.
        /// </summary>
        private void ConfigureKeysGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && ConfigureKeysGrid.SelectedCells.Count == 1)
            {
                e.Handled = true;
                ConfigureKeysGrid_CellContentDoubleClick(null, new DataGridViewCellEventArgs(1, ConfigureKeysGrid.SelectedCells[0].RowIndex));
            }
            else if (e.KeyCode == Keys.Delete)
            {
                e.Handled = true;
                ConfigureKeysGrid_CellContentDoubleClick(null, new DataGridViewCellEventArgs(2, ConfigureKeysGrid.SelectedCells[0].RowIndex));
            }
        }

        /// <summary>
        /// Event method auto-called when the "Add Key" button is pressed.
        /// </summary>
        public void AddKeyButton_Click(object sender, EventArgs e)
        {
            Keys? result = null;
            NeedInputForm form = new NeedInputForm()
            {
                SetResultKey = (k) =>
                {
                    result = k;
                }
            };
            form.ShowDialog(this);
            if (!result.HasValue)
            {
                // The enter key doesn't track right, so just make sure it's always present.
                if (Program.Blocker.KeysToChatterTime[Keys.Enter].HasValue)
                {
                    return;
                }
                result = Keys.Enter;
            }
            Program.Blocker.KeysToChatterTime[result.Value] = Program.Blocker.GlobalChatterTimeLimit;
            Program.Blocker.SaveConfig();
            ConfigureKeysGrid.Rows.Add(result.Value.Stringify(), Program.Blocker.GlobalChatterTimeLimit.ToString(), "[X]");
        }

        /// <summary>
        /// Event method auto-called when the website link in the "About" tab is pressed.
        /// </summary>
        private void AboutLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/mcmonkeyprojects/KeyboardChatterBlocker");
        }

        /// <summary>
        /// Event method auto-called when the "chatter log" box is double-clicked.
        /// </summary>
        private void ChatterLogGrid_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!Enum.TryParse(ChatterLogGrid[1, e.RowIndex].Value.ToString(), true, out Keys key))
            {
                MessageBox.Show("Error: chatter log grid misconfigured, invalid key name!", "Keyboard Chatter Blocker", MessageBoxButtons.OK);
                return;
            }
            else if (e.ColumnIndex == 3) // 'Configure' column
            {
                if (!Program.Blocker.KeysToChatterTime[key].HasValue)
                {
                    Program.Blocker.KeysToChatterTime[key] = Program.Blocker.GlobalChatterTimeLimit;
                    Program.Blocker.SaveConfig();
                    ConfigureKeysGrid.Rows.Add(key.Stringify(), Program.Blocker.GlobalChatterTimeLimit.ToString(), "[X]");
                }
                string keyText = key.Stringify();
                TabControl1.SelectedTab = KeysTabPage;
                foreach (DataGridViewRow row in ConfigureKeysGrid.Rows)
                {
                    if (row.Cells[0].Value.ToString() == keyText)
                    {
                        ConfigureKeysGrid.Select();
                        ConfigureKeysGrid.ClearSelection();
                        row.Selected = true;
                        ConfigureKeysGrid.FirstDisplayedScrollingRowIndex = row.Index;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Event method to keep a placeholder in the 'add program' text box.
        /// </summary>
        private void AddProgramTextBox_Enter(object sender, EventArgs e)
        {
            if (AddProgramTextBox.Text == "Program Name")
            {
                AddProgramTextBox.Text = "";
            }
        }


        /// <summary>
        /// Event method to keep a placeholder in the 'add program' text box.
        /// </summary>
        private void AddProgramTextBox_Leave(object sender, EventArgs e)
        {
            if (AddProgramTextBox.Text == "")
            {
                AddProgramTextBox.Text = "Program Name";
            }
        }

        /// <summary>
        /// A few common processes that are always running in Windows and thus don't need to be listed (for convenience).
        /// </summary>
        public static HashSet<string> StandardWindowsProcesses = new HashSet<string>()
        {
            "svchost", "smartscreen", "spoolsv", "explorer", "services", "registry", "taskhost", "taskhostw", "smss", "ctfmon", "idle", "csrss", "dwm", "fontdrvhost", "lsass", "sgrmbroker",
            "onedrive", "searchhost", "searchindexer", "shellexperiencehost", "startmenuexperiencehost", "system", "systemsettings", "systemsettingsbroker", "wininit", "winlogin", "winlogon", "wmiprvse",
            "applicationframehost", "textinputhost", "aggregatorhost", "audiodg", "gamebar", "gamebarftserver", "gamingservices", "gamingservicesnet", "microsoft.photos",
            "minisearchhost", "memory compression", "msmpeng", "msmpengcp", "nissrv", "runtimebroker", "sihost", "uhssvc", "unsecapp", "wudfhost", "yourphone",
            "rtkauduservice64", // realtek audio
            "nvcontainer", "nvidia share", "nvsphelper64", "nvbroadcast.container", "nvidia broadcast", "nvidia broadcast ui", "nvdisplay.container", "nvidia web helper", // nvidia
             "jhi_service", "lms", // intel
        };

        /// <summary>
        /// Event method to show a list of current program names for the add program box.
        /// </summary>
        private void ShowProgramListButton_Click(object sender, EventArgs e)
        {
            // Get a hashset of exclusions by combining the list of already-disabled programs with the set of common windows programs
            HashSet<string> excludeProcessNames = StandardWindowsProcesses.Union(Program.Blocker.AutoDisablePrograms).ToHashSet();
            // Get a list of processes, and make them unique by process name - prioritize processes with a window title over those without
            List<Process> processes = Process.GetProcesses().GroupBy(p => p.ProcessName).Select(g => g.FirstOrDefault(p => !string.IsNullOrEmpty(p.MainWindowTitle)) ?? g.First())
                // then exclude the set of excludes
                .Where(p => !excludeProcessNames.Contains(p.ProcessName.ToLowerInvariant()))
                // Sort the list alphabetically, but pull those with main window titles to the top (and those without below)
                .OrderBy(p => p.ProcessName).OrderBy(p => string.IsNullOrEmpty(p.MainWindowTitle)).ToList();
            // Now turn it into a set of clickable menu items.
            MenuItem[] items = processes.Select(p => new MenuItem(string.IsNullOrEmpty(p.MainWindowTitle) ? p.ProcessName : $"{p.ProcessName} ({p.MainWindowTitle})", (s, a) => AddProgramTextBox.Text = p.ProcessName.ToLowerInvariant())).ToArray();
            // To feed into a context menu to be shown.
            new ContextMenu(items).Show(ShowProgramListButton, Point.Empty);
        }

        /// <summary>
        /// Event method to add an auto-disabled program.
        /// </summary>
        private void AddToListButton_Click(object sender, EventArgs e)
        {
            if (AddProgramTextBox.Text.Trim().Length == 0)
            {
                return;
            }
            string program = AddProgramTextBox.Text.ToLowerInvariant().Trim();
            if (Program.Blocker.AutoDisablePrograms.Contains(program))
            {
                return;
            }
            Program.Blocker.AutoDisablePrograms.Add(program);
            Program.Blocker.SaveConfig();
            AutoDisableProgramsList.Items.Add(new ListViewItem(program));
            AddProgramTextBox_TextChanged(null, null);
        }

        /// <summary>
        /// Event method to set whether the add-program button is clickable.
        /// </summary>
        private void AddProgramTextBox_TextChanged(object sender, EventArgs e)
        {
            bool eitherEnabled = AddProgramTextBox.Text.Trim().Length != 0 && AddProgramTextBox.Text != "Program Name";
            bool isExistingProgram = Program.Blocker.AutoDisablePrograms.Contains(AddProgramTextBox.Text.ToLowerInvariant().Trim());
            AddToListButton.Enabled = eitherEnabled && !isExistingProgram;
            RemoveProgramButton.Enabled = eitherEnabled && isExistingProgram;
        }

        /// <summary>
        /// Event method to remove an auto-disabled program.
        /// </summary>
        private void RemoveProgramButton_Click(object sender, EventArgs e)
        {
            if (AddProgramTextBox.Text.Trim().Length == 0)
            {
                return;
            }
            string program = AddProgramTextBox.Text.ToLowerInvariant().Trim();
            if (!Program.Blocker.AutoDisablePrograms.Contains(program))
            {
                return;
            }
            Program.Blocker.AutoDisablePrograms.Remove(program);
            Program.Blocker.SaveConfig();
            foreach (ListViewItem item in AutoDisableProgramsList.Items)
            {
                if (item.Text == program)
                {
                    AutoDisableProgramsList.Items.Remove(item);
                    break;
                }
            }
            AddProgramTextBox_TextChanged(null, null);
        }

        /// <summary>
        /// Event method to auto-alter the current program text box input to be an item selected from the list.
        /// </summary>
        private void AutoDisableProgramsList_Click(object sender, EventArgs e)
        {
            ListViewItem selected = AutoDisableProgramsList.SelectedItems.OfType<ListViewItem>().FirstOrDefault();
            if (selected != null)
            {
                AddProgramTextBox.Text = selected.Text;
            }
        }

        /// <summary>
        /// Event method to handle clicks of the tray icon.
        /// </summary>
        private void TrayIconContextMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == ContextMenuShowButton)
            {
                ShowForm();
            }
            else if (e.ClickedItem == ContextMenuExitButton)
            {
                ShouldForceClose = true;
                Close();
            }
        }

        /// <summary>
        /// Event method to handle the 'auto disable on fullscreen' checkbox state changing.
        /// </summary>
        private void AutoDisableOnFullscreenCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (Loading)
            {
                return;
            }
            Program.Blocker.AutoDisableOnFullscreen = AutoDisableOnFullscreenCheckbox.Checked;
            Program.Blocker.SaveConfig();
        }
    }
}
