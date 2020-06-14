using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Threading;
using System.Web.Query.Dynamic;
using System.Windows.Forms;

namespace CoreTracker
{
    #region application
    public partial class Form1 : Form
    {
        // Constant Definition
        private List<NotifyIcon> th_list = new List<NotifyIcon>();
        private NotifyIcon CpuTmpereaute = new NotifyIcon() { Visible = false, Icon = Properties.Resources._10_c, BalloonTipIcon = ToolTipIcon.Info, BalloonTipTitle = "Info From CPU Temperaute" };
        private NotifyIcon RamUsage = new NotifyIcon() { Visible = false, Icon = Properties.Resources._10_r, BalloonTipIcon = ToolTipIcon.Info, BalloonTipTitle = "Info From Memory Temperaute" };
        private NotifyIcon BoardTmpereaute = new NotifyIcon() { Visible = false, Icon = Properties.Resources._10_b, BalloonTipIcon = ToolTipIcon.Info, BalloonTipTitle = "Info From Marderboard Temperaute" };
        private NotifyIcon GraphicTmpereaute = new NotifyIcon() { Visible = false, Icon = Properties.Resources._10_g, BalloonTipIcon = ToolTipIcon.Info, BalloonTipTitle = "Info From GPU Temperaute" };
        private bool run = false;
        private Thread th = null;
        private Int16 ModeSlow = 5000;
        private Int16 ModeNormarl = 3000;
        private Int16 ModeFast = 1000;
        private string VERSION = "v0.9.8";
        private string GITHUB = "https://github.com/Fhwang0926/CoreTracker";

        private bool mouseDown;
        private bool is_updateing = false;
        private Point lastLocation;
        private Ragistry Ragistry = new Ragistry();
        private Controller controller = new Controller();
        public void SetTimeout(System.Action action, int timeout)
        {
            var timer = new System.Windows.Forms.Timer();
            var noti = new NotifyIcon() { Visible = true, Icon = Properties.Resources.form };
            timer.Interval = timeout;
            timer.Tick += delegate (object sender, EventArgs args)
            {
                action();
                timer.Stop();
                noti.Dispose();
                btn_tray.Enabled = true;
            };
            noti.ShowBalloonTip(2000, "[CoreTracker Notice] : Auto Start", "Soon minimize to tray icon", ToolTipIcon.Info);
            timer.Start();
        }

        public Form1()
        {
            InitializeComponent();
            KeyPreview = true;

            // under code run after InitializeComponent

            // update
            l_core_value.Text = (Environment.ProcessorCount / 2).ToString();
            l_th_value.Text = Environment.ProcessorCount.ToString();

            // make north
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20,20));
            btn_north.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, btn_north.Height, btn_north.Width, -400, 20, 20));

            // ass running status
            pic_status.Image = Properties.Resources.bad;
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddEllipse(0, 0, pic_status.Width - 4, pic_status.Height - 2);
            pic_status.Region = new Region(gp);

            // add NotifyIcon mouse right button menu
            ti_main.ContextMenu = new ContextMenu();
            ti_main.ContextMenu.MenuItems.Add(0, new MenuItem("Exit", new System.EventHandler(Exit_Click)));
            ti_main.ContextMenu.MenuItems.Add(1, new MenuItem("Show", new System.EventHandler(Show_Click)));
            ti_main.ContextMenu.MenuItems.Add(2, new MenuItem("Hide", new System.EventHandler(Hide_Click)));
            ti_main.ContextMenu.MenuItems.Add(3, new MenuItem("BUG Report(Github)", new System.EventHandler(Report_Click)));
            ti_main.ContextMenu.MenuItems.Add(4, new MenuItem("Reset", new System.EventHandler(Reset_Click)));
            ti_main.ContextMenu.MenuItems.Add(4, new MenuItem("Update", new System.EventHandler(Update_Click)));

            // add NotifyIcon by core count
            for (int c = 1; c <= Environment.ProcessorCount; c++) { th_list.Add(new NotifyIcon() { Visible = true, Icon = Properties.Resources._10 }); }

            // intialize thread && check auto start
            ch_graphic_temperature.Checked = Ragistry.ChecGraphicTemperature();
            ch_ram_temperature.Checked = Ragistry.CheckRamTemperature();
            ch_cpu_temperature.Checked = Ragistry.CheckCpuTemperature();
            ch_board_temperature.Checked = Ragistry.CheckBoardTemperature();
            chk_disable_alert.Checked = Ragistry.ChecDisableBusyAlert();

            bool auto_run = Ragistry.CheckAutoRun();
            init_CPU_Watcher(auto_run);
            l_version.Text = VERSION;

            // check auto update
            bool auto_update = Ragistry.CheckAutoUpdate();
            if (auto_update) { ch_auto_update.Checked = true; self_update(); }

            // option area
            ManagementObjectCollection searcher = new ManagementObjectSearcher("SELECT * FROM Win32_DisplayConfiguration").Get();
            if (searcher.Count > 0) { ch_graphic_temperature.Enabled = true; }
            searcher.Dispose();

            // display trayicon
            ch_trayicon_setting.Checked = Ragistry.CheckTrayIconConfig();
            if (!is_updateing && !ch_trayicon_setting.Checked)
            {
                // recommended enable this setting
                ch_trayicon_setting.Checked = toggleTraySetting();
            }


            if (auto_run)
            {
                btn_tray.Enabled = false;
                // auto run
                ch_auto_start.Checked = true;
                // start new thred for hide
                SetTimeout(toggleMe, 3000);
            }

            Activate();
        }

        private void init_CPU_Watcher(bool Immediate_start = true)
        {
            if (th != null) { th.Abort(); th = null; }
            th = new Thread(new ThreadStart(runner));
            GraphicTmpereaute.Icon = setTrayIcon(0, "graphic");
            CpuTmpereaute.Icon = setTrayIcon(0, "cpu");
            RamUsage.Icon = setTrayIcon(0, "ram");
            BoardTmpereaute.Icon = setTrayIcon(0, "board");
            foreach (var item in th_list) { item.Icon = Properties.Resources._10; }

            if (Immediate_start) {
                th.Start();
                ti_main.ShowBalloonTip(1000, "[CoreTracker Notice] : Reset", "CPU Status Checker Refreshed", ToolTipIcon.Info);
            }
        }
        private void Report_Click(Object sender, System.EventArgs e)
        {
            // github report
            Process.Start($"{GITHUB}/issues/new");

        }
        private void Update_Click(Object sender, System.EventArgs e)
        {
            self_update(true);
        }

        // update function with msgbox
        private async void self_update(bool updateAnswer = false)
        {
            // auto update latest
            updateFormat rs = await controller.CompareVersion(VERSION.Replace("v", string.Empty));
            if (rs.is_error)
            {
                MessageBox.Show(rs?.msg.ToString(), "Update failed!! compare version", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (rs.latest) {
                    if (updateAnswer) { MessageBox.Show(rs?.msg.ToString(), $"{Process.GetCurrentProcess().ProcessName}", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                }
                else
                {
                    is_updateing = true;
                    DialogResult result = MessageBox.Show(rs?.msg.ToString(), $"{Process.GetCurrentProcess().ProcessName}", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (result == System.Windows.Forms.DialogResult.OK)
                    {
                        // unload start
                        updateFormat restart = await controller.startDownload(rs.target);
                        // real restart
                        if (!restart.is_error)
                        {
                            if (controller.restart()) { Close(); }
                        }
                        else
                        {
                            MessageBox.Show("failed update :/", "Update Failed : restart", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        }
                        is_updateing = false;
                    }
                }
            }
            
        }
        private void Reset_Click(Object sender, System.EventArgs e)
        {
            init_CPU_Watcher();
        }
        private void Exit_Click(Object sender, System.EventArgs e)
        {
            Close();
        }
        private void Hide_Click(Object sender, System.EventArgs e)
        {
            toggleMe();
        }
        private void Show_Click(Object sender, System.EventArgs e)
        {
            toggleMe();
        }

        private void btn_tray_Click(object sender, EventArgs e)
        {
            toggleMe();
            if (!run && !th.IsAlive) { run = true; th.Start(); }
        }

        private void runner()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from Win32_PerfFormattedData_PerfOS_Processor");
            try
            {

                pic_status.Image = Properties.Resources.good;
                bool noticeStatus = false;
                Int16 busyCount = 0;

                // init
                controller.hardwareMoniterInit();

                while (true)
                {
                    // refresh temperaute
                    if (ch_board_temperature.Checked || ch_cpu_temperature.Checked || ch_graphic_temperature.Checked || ch_ram_temperature.Checked)
                    {
                        controller.hardwareInfo();
                    }

                    if (ch_graphic_temperature.Checked)
                    {
                        controller.computer.GPUEnabled = ch_graphic_temperature.Checked; GraphicTmpereaute.Icon = setTrayIcon(controller.sb.gpu_temperature, "graphic");
                        GraphicTmpereaute.BalloonTipText = $"GPU Temperature : {controller.sb.gpu_temperature}";
                    }
                    if (ch_cpu_temperature.Checked)
                    {
                        controller.computer.CPUEnabled = ch_cpu_temperature.Checked; CpuTmpereaute.Icon = setTrayIcon(controller.sb.cpu_temperature, "cpu");
                        CpuTmpereaute.BalloonTipText = $"CPU Temperature : {controller.sb.cpu_temperature}";
                    }
                    if (ch_ram_temperature.Checked)
                    {
                        controller.computer.RAMEnabled = ch_ram_temperature.Checked; RamUsage.Icon = setTrayIcon(controller.sb.ram_usage, "ram");
                        RamUsage.BalloonTipText = $"RAM Usage status : {controller.sb.ram_usage}";
                    }
                    if (ch_board_temperature.Checked)
                    {
                        controller.computer.MainboardEnabled = ch_board_temperature.Checked; BoardTmpereaute.Icon = setTrayIcon(controller.sb.board_temperature, "board");
                        BoardTmpereaute.BalloonTipText = $"Marderboard Temperature : {controller.sb.board_temperature}";
                    }

                    var cpu_info = searcher.Get().Cast<ManagementObject>().Select(mo => new { Name = mo["Name"], Usage = Convert.ToInt32(mo["PercentProcessorTime"]) }).ToList();
                    foreach (var c in cpu_info)
                    {

                        if (c.Name.ToString() == "_Total")
                        {
                            if (chk_disable_alert.Checked) { continue; }
                            // if show windows system notification more then 80% usage
                            if (busyCount >= 10) { noticeStatus = false; busyCount = 0; continue; }
                            else if (noticeStatus) { busyCount++; continue; }
                            else if (Convert.ToInt32(c.Usage) > 80)
                            {
                                ti_main.ShowBalloonTip(5000, "[CoreTracker Notice]CPU Busy", "CPU so busy, the reason is your action? if is not, scan cryptojacking malware on PC", ToolTipIcon.Warning);
                                noticeStatus = true;

                            }
                            continue;
                        }
                        else
                        {
                            th_list[Convert.ToInt32(c.Name)].Icon = setTrayIcon(c.Usage);
                        }
                    }
                    if (rb_normal.Checked) { Thread.Sleep(ModeNormarl); }
                    else if (rb_slow.Checked) { Thread.Sleep(ModeSlow); }
                    else if (rb_fast.Checked) { Thread.Sleep(ModeFast); }
                }
            } catch (Exception e) {
                Console.WriteLine($"runner Error : {e.Message}\n\n{e.StackTrace}");
                searcher.Dispose();
            }
            
        }

        private void toggleMe()
        {
            if (ti_main.Visible)
            {
                Show();
                this.WindowState = FormWindowState.Normal;
                ti_main.Visible = false;
                ti_main.ContextMenu.MenuItems[2].Enabled = true;
            } else
            {
                Hide();
                ti_main.Visible = true;
                ti_main.ContextMenu.MenuItems[2].Enabled = false;
            }
        }


        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            toggleMe();
        }

        private static Icon setTrayIcon(int value, string type = "")
        {
            // setTrayIcon find type nogada
            switch (type)
            {
                case "cpu":
                    if (0 <= value && value < 20) { return Properties.Resources._10_c; }
                    else if (20 <= value && value < 40) { return Properties.Resources._20_c; }
                    else if (40 <= value && value < 60) { return Properties.Resources._40_c; }
                    else if (60 <= value && value < 80) { return Properties.Resources._60_c; }
                    else { return Properties.Resources._80_c; }
                case "ram":
                    if (0 <= value && value < 20) { return Properties.Resources._10_r; }
                    else if (20 <= value && value < 40) { return Properties.Resources._20_r; }
                    else if (40 <= value && value < 60) { return Properties.Resources._40_r; }
                    else if (60 <= value && value < 80) { return Properties.Resources._60_r; }
                    else { return Properties.Resources._80_r; }
                case "board":
                    if (0 <= value && value < 20) { return Properties.Resources._10_b; }
                    else if (20 <= value && value < 40) { return Properties.Resources._20_b; }
                    else if (40 <= value && value < 60) { return Properties.Resources._40_b; }
                    else if (60 <= value && value < 80) { return Properties.Resources._60_b; }
                    else { return Properties.Resources._80_b; }
                case "graphic":
                    if (0 <= value && value < 20) { return Properties.Resources._10_g; }
                    else if (20 <= value && value < 40) { return Properties.Resources._20_g; }
                    else if (40 <= value && value < 60) { return Properties.Resources._40_g; }
                    else if (60 <= value && value < 80) { return Properties.Resources._60_g; }
                    else { return Properties.Resources._80_g; }
                default:
                    if (0 <= value && value < 20) { return Properties.Resources._10; }
                    else if (20 <= value && value < 40) { return Properties.Resources._20; }
                    else if (40 <= value && value < 60) { return Properties.Resources._40; }
                    else if (60 <= value && value < 80) { return Properties.Resources._60; }
                    else { return Properties.Resources._80; }
            }
        }

        private static Icon setTrayIcon(int value)
        {
            if (0 <= value && value < 20) { return Properties.Resources._10; }
            else if (20 <= value && value < 40) { return Properties.Resources._20; }
            else if (40 <= value && value < 60) { return Properties.Resources._40; }
            else if (60 <= value && value < 80) { return Properties.Resources._60; }
            else { return Properties.Resources._80; }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // not run here, too slow loading
            // change using taryicon dispose > windows auto refresh
            controller.RefreshTrayArea();
        }

        private void Form1_FormClosing(object sender, EventArgs e)
        {
            if (th != null) { th.Abort(); th = null; }
            foreach (var t in th_list) { t.Dispose(); }
            
            controller.RefreshTrayArea();
            controller.Dispose();
            BoardTmpereaute.Dispose();
            CpuTmpereaute.Dispose();
            GraphicTmpereaute.Dispose();
            RamUsage.Dispose();
            
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            // Hide The Form when it's minimized
            if (FormWindowState.Minimized == WindowState) { 
                toggleMe();
                if (!run && !th.IsAlive) { run = true; th.Start(); }
            }
                
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(GITHUB);
        }

        private void ch_auto_start_CheckedChanged(object sender, EventArgs e)
        {
            ch_auto_start.Enabled = false;
            string me = Process.GetCurrentProcess().MainModule.FileName.Split('\\').LastOrDefault().Replace(".exe", "");
            if (ch_auto_start.Checked)
            {
                string cmd = $"schtasks /Create /TR \"{Application.ExecutablePath}\" /TN CoreTracker /SC ONLOGON /IT /RL HIGHEST /F";
                if (!controller.exeCmd(cmd))
                {
                    MessageBox.Show("Add Auto start Scheduler Failed");
                    return;
                }
                Ragistry.enable_auto_run();

            }
            else
            {

                string cmd = $"schtasks /Delete /TN \"{"CoreTracker"}\" /F";
                if (!controller.exeCmd(cmd))
                {
                    MessageBox.Show("Delete Auto start Scheduler Failed");
                    return;
                }
                Ragistry.disable_auto_run();
            }
            ch_auto_start.Enabled = true;
        }

        private void l_close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void l_hide_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btn_north_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        // north move
        private void btn_north_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point((this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);
                this.Update();
            }
        }

        private void btn_north_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void l_close_MouseHover(object sender, EventArgs e)
        {
            l_close.BackColor = System.Drawing.Color.DodgerBlue;
        }

        private void l_close_MouseLeave(object sender, EventArgs e)
        {
            l_close.BackColor = System.Drawing.Color.Transparent;
        }

        private void l_hide_MouseHover(object sender, EventArgs e)
        {
            l_hide.BackColor = System.Drawing.Color.DodgerBlue;
        }

        private void l_hide_MouseLeave(object sender, EventArgs e)
        {
            l_hide.BackColor = System.Drawing.Color.Transparent;
        }

        private void ch_auto_update_CheckedChanged(object sender, EventArgs e)
        {
            ch_auto_update.Enabled = false;
            if (ch_auto_update.Checked) { Ragistry.enable_auto_update(); }
            else { Ragistry.disable_auto_update(); }
            ch_auto_update.Enabled = true;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e == null) { return; }
            if(ti_main.Visible) { return; }
            else if (e.KeyData == Keys.Escape) { toggleMe(); }
            //base.OnKeyUp(e);
        }

        private void ch_cpu_temperature_CheckedChanged(object sender, EventArgs e)
        {
            ch_cpu_temperature.Enabled = false;
            if (ch_cpu_temperature.Checked) { Ragistry.enable_cpu_temperature(); }
            else { Ragistry.disable_cpu_temperature(); }
            CpuTmpereaute.Visible = ch_cpu_temperature.Checked;
            ch_cpu_temperature.Enabled = true;
        }

        private void ch_ram_temperature_CheckedChanged(object sender, EventArgs e)
        {
            ch_ram_temperature.Enabled = false;
            if (ch_ram_temperature.Checked) { Ragistry.enable_ram_temperature(); }
            else { Ragistry.disable_ram_temperature(); }
            RamUsage.Visible = ch_ram_temperature.Checked;
            ch_ram_temperature.Enabled = true;
        }

        private void ch_board_temperature_CheckedChanged(object sender, EventArgs e)
        {
            ch_board_temperature.Enabled = false;
            if (ch_board_temperature.Checked) { Ragistry.enable_board_temperature(); }
            else { Ragistry.disable_board_temperature(); }
            BoardTmpereaute.Visible = ch_board_temperature.Checked;
            ch_board_temperature.Enabled = true;
        }

        private void ch_graphic_temperature_CheckedChanged(object sender, EventArgs e)
        {
            ch_graphic_temperature.Enabled = false;
            if (ch_graphic_temperature.Checked) { Ragistry.enable_graphic_temperature(); }
            else { Ragistry.disable_graphic_temperature(); }
            GraphicTmpereaute.Visible = ch_graphic_temperature.Checked;
            ch_graphic_temperature.Enabled = true; ;
        }

        private void chk_disable_alert_CheckedChanged(object sender, EventArgs e)
        {
            chk_disable_alert.Enabled = false;
            if (chk_disable_alert.Checked) { Ragistry.enable_busy_alert(); }
            else { Ragistry.disable_busy_alert(); }
            chk_disable_alert.Enabled = true;
        }

        private void ch_trayicon_setting_Click(object sender, EventArgs e)
        {
            toggleTraySetting();
            ch_trayicon_setting.Checked = Ragistry.CheckTrayIconConfig();
        }

        private bool toggleTraySetting()
        {
            try
            {
                var rs = MessageBox.Show(
                    "Requires setting to place tray icon area on taskbar\n\nWhen set, Explorer will restart.\n\nOther unsaved programs may be affected",
                    "Notice",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                    );
                if (rs == DialogResult.Yes)
                { 
                    Ragistry.ToggleTrayIconConfig();
                    bool restarted = controller.RestartExplorer();
                    if (!restarted) { MessageBox.Show("Explorer Restart Failed, tray later or self restart plz", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
                }
                return true;
            } catch (Exception)
            {
                return false;
            }
        }
    }
    #endregion
}

























































