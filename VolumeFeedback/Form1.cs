using Microsoft.Win32;
using NAudio.CoreAudioApi;
using System;
using System.Diagnostics;
using System.IO;
using System.Media;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using VolumeFeedback.Properties;

namespace VolumeFeedback
{
    public partial class Form1 : Form
    {
        private static MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
        private MMDevice device = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);

        private static string datapath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"VolumeFeedback\Data");
        private string customfile = Path.Combine(datapath, "custom.wav");

        private RegistryKey rk = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);

        public Form1()
        {
            InitializeComponent();
            FormClosing += Form1_FormClosing;
            notifyIcon1.MouseUp += notifyIcon1_MouseUp;
            device.AudioEndpointVolume.OnVolumeNotification += AudioEndpointVolume_OnVolumeNotification;

            if (Settings.Default.newvol == true)
            {
                radioButton2.Checked = true;
            }
            else if (Settings.Default.custom == true)
            {
                radioButton3.Checked = true;
            }
            else
            {
                radioButton1.Checked = true;
            }

            if (Settings.Default.silentstart == true)
            {
                checkBox1.Checked = true;
                Hide();
                notifyIcon1.Visible = true;
                ShowInTaskbar = false;
            }
            else
            {
                checkBox1.Checked = false;
                Show();
                notifyIcon1.Visible = false;
                ShowInTaskbar = true;
            }
            if (rk.GetValue("VolumeFeedback") == null)
            {
                checkBox2.Checked = false;
            }
            else
            {
                checkBox2.Checked = true;
            }
            if (!File.Exists(datapath))
            {
                Directory.CreateDirectory(datapath);
            }
            if (!File.Exists(customfile))
            {
                File.Copy(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), @"Media\Windows Background.wav"), Path.Combine(datapath, "custom.wav"));
            }
        }

        // play sounds

        private void AudioEndpointVolume_OnVolumeNotification(AudioVolumeNotificationData data)
        {
            if (Settings.Default.newvol == true)
            {
                SoundPlayer snd = new SoundPlayer(Properties.Resources.volumenew);
                snd.Play();
            }
            else if (radioButton3.Checked == true)
            {
                SoundPlayer snd = new SoundPlayer(customfile);
                snd.Play();
            }
            else
            {
                SoundPlayer snd = new SoundPlayer(Properties.Resources.volume);
                snd.Play();
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.newvol = false;
            Settings.Default.Save();
            originalToolStripMenuItem.Checked = true;
            yosemiteToolStripMenuItem.Checked = false;
            customToolStripMenuItem.Checked = false;
            chooseFileToolStripMenuItem.Enabled = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.newvol = true;
            Settings.Default.Save();
            originalToolStripMenuItem.Checked = false;
            yosemiteToolStripMenuItem.Checked = true;
            customToolStripMenuItem.Checked = false;
            chooseFileToolStripMenuItem.Enabled = false;
        }

        private async void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.newvol = false;
            Settings.Default.newvol = false;
            Settings.Default.custom = true;
            Settings.Default.Save();

            originalToolStripMenuItem.Checked = false;
            yosemiteToolStripMenuItem.Checked = false;
            customToolStripMenuItem.Checked = true;
            chooseFileToolStripMenuItem.Enabled = true;

            switch (radioButton3.Checked)
            {
                case true:
                    button1.Enabled = true;
                    tabControl1Anim(false);
                    for (double i = 155; i < 200.01; i += 5)
                    {
                        i = Math.Round(i, 2);

                        await Task.Delay(1);

                        var heightint = Convert.ToInt32(i);

                        Height = heightint;
                    }

                    return;

                case false:
                    tabControl1Anim(true);
                    for (double i = 200; i > 155.99; i -= 5)
                    {
                        i = Math.Round(i, 2);

                        await Task.Delay(1);

                        var heightint = Convert.ToInt32(i);

                        Height = heightint;
                    }

                    button1.Enabled = false;
                    return;
            }
        }

        private async void tabControl1Anim(bool reverse)
        {
            switch (reverse)
            {
                case false:
                    for (double i = 100; i < 140.01; i += 5)
                    {
                        i = Math.Round(i, 2);

                        await Task.Delay(1);

                        var tabheightint = Convert.ToInt32(i);

                        tabControl1.Height = tabheightint;
                    }
                    return;

                case true:
                    for (double i = 80; i < 100.99; i += 5)
                    {
                        i = Math.Round(i, 2);

                        await Task.Delay(1);

                        var tabheightint = Convert.ToInt32(i);

                        tabControl1.Height = tabheightint;
                    }
                    return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog
            {
                Filter = ".wav files|*.wav",
                Title = "Select a .wav file",
                Multiselect = false
            };

            ofd.ShowDialog();
            ofd.InitialDirectory = Settings.Default.lastpath;

            if (ofd.FileName != "")
            {
                File.Copy(ofd.FileName, customfile, true);
                Settings.Default.lastpath = Path.GetDirectoryName(ofd.FileName);
                Settings.Default.Save();
                Settings.Default.Reload();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
            notifyIcon1.Visible = true;
            ShowInTaskbar = false;
        }

        private void notifyIcon1_MouseUp(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    Show();
                    notifyIcon1.Visible = false;
                    ShowInTaskbar = true;
                    return;

                case MouseButtons.Right:
                    MethodInfo mi = typeof(NotifyIcon).GetMethod("ShowContextMenu", BindingFlags.Instance | BindingFlags.NonPublic);
                    mi.Invoke(notifyIcon1, null);
                    return;
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            ShowInTaskbar = true;
            Show();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            switch (checkBox1.Checked)
            {
                case true:
                    Settings.Default.silentstart = true;
                    Settings.Default.Save();
                    return;

                case false:
                    Settings.Default.silentstart = false;
                    Settings.Default.Save();
                    return;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            switch (checkBox2.Checked)
            {
                case true:
                    if (!Debugger.IsAttached)
                    {
                        rk.SetValue("VolumeFeedback", Application.ExecutablePath.ToString());
                    }
                    return;

                case false:
                    if (!Debugger.IsAttached)
                    {
                        rk.DeleteValue("VolumeFeedback", false);
                    }
                    return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Are you sure you want to reset ALL settings?", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            switch (confirm)
            {
                case DialogResult.Yes:
                    Settings.Default.lastpath = "";
                    Settings.Default.silentstart = true;
                    checkBox1.Checked = true;
                    checkBox2.Checked = true;
                    radioButton2.Checked = true;
                    if (File.Exists(customfile))
                    {
                        File.Delete(customfile);
                        File.Copy(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), @"Media\Windows Background.wav"), Path.Combine(datapath, "custom.wav"));
                    }
                    return;

                case DialogResult.No:
                    return;
            }
        }

        private void chooseFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Show();
            button1.PerformClick();
        }

        private void customToolStripMenuItem_Click(object sender, EventArgs e)
        {
            radioButton3.Select();
            chooseFileToolStripMenuItem.Enabled = true;
        }

        private void yosemiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            radioButton2.Select();
            chooseFileToolStripMenuItem.Enabled = false;
        }

        private void originalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            radioButton1.Select();
            chooseFileToolStripMenuItem.Enabled = false;
        }
    }
}