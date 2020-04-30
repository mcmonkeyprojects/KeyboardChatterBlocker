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
        }

        /// <summary>
        /// Method auto-called (by event) for when a key is blocked.
        /// </summary>
        /// <param name="e">The key blocked event details.</param>
        public void LogKeyBlocked(KeyBlockedEventArgs e)
        {
            ChatterLogGrid.Rows.Add(DateTime.Now.ToString("HH:mm:ss"), e.Key.ToString(), e.Time.ToString(), "[Edit]");
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

        /// <summary>
        /// Event method auto-called when the form loads.
        /// </summary>
        public void MainBlockerForm_Load(object sender, EventArgs e)
        {
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
            ChatterThresholdBox.Value = Program.Blocker.GlobalChatterTimeLimit;
            EnabledCheckbox.Checked = Program.Blocker.IsEnabled;
            StartWithWindowsCheckbox.Checked = File.Exists(StartupLinkPath);
            StatsUpdateTimer = new Timer { Interval = 1000 };
            StatsUpdateTimer.Tick += StatsUpdateTimer_Tick;
            StatsUpdateTimer.Start();
            PushKeysToGrid();
            Loading = false;
        }

        /// <summary>
        /// Automatic stats update timer, when the stats view is visible.
        /// </summary>
        public void StatsUpdateTimer_Tick(object sender, EventArgs e)
        {
            if (Program.Blocker.AnyKeyChange && !IsHidden && tabControl1.SelectedTab == StatsTabPage)
            {
                Program.Blocker.AnyKeyChange = false;
                PushStatsToGrid();
            }
        }

        /// <summary>
        /// Event method auto-called when the form close button is pressed.
        /// </summary>
        public void MainBlockerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
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
                StatsGrid.Rows.Add(keyData.Key.ToString(), keyData.Value, chatterTotal, percentage);
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
                ConfigureKeysGrid.Rows.Add(keyData.Key.ToString(), keyData.Value.Value, "[X]");
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
            if (!Enum.TryParse(ConfigureKeysGrid[0, e.RowIndex].Value.ToString(), true, out Keys key))
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
            ConfigureKeysGrid.Rows.Add(result.Value.ToString(), Program.Blocker.GlobalChatterTimeLimit.ToString(), "[X]");
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
                    ConfigureKeysGrid.Rows.Add(key.ToString(), Program.Blocker.GlobalChatterTimeLimit.ToString(), "[X]");
                }
                string keyText = key.ToString();
                tabControl1.SelectedTab = KeysTabPage;
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
    }
}
