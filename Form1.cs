using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Threading;
using System.Windows.Forms;

namespace CoreTracker
{
    #region application
    public partial class Form1 : Form
    {
        // Constant Definition
        private List<NotifyIcon> th_list = new List<NotifyIcon>();
        private bool run = false;
        private Thread th;
        private Int16 ModeSlow = 5000;
        private Int16 ModeNormarl = 3000;
        private Int16 ModeFast = 1000;
        private string VERSION = "v0.1.3";

        private bool mouseDown;
        private Point lastLocation;
        private Ragistry Ragistry = new Ragistry();
        private Controller controller = new Controller();


        public Form1()
        {
            InitializeComponent();
            KeyPreview = true;

            // under code run after InitializeComponent

            // update
            l_core_value.Text = (Environment.ProcessorCount / 2).ToString();
            l_th_value.Text = Environment.ProcessorCount.ToString();
            ch_auto_bugreport.Enabled = false;
            ch_auto_bugreport.Text = ch_auto_bugreport.Text += "(coming soon)";

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
            ti_main.ContextMenu.MenuItems.Add(3, new MenuItem("Report", new System.EventHandler(Report_Click)));
            ti_main.ContextMenu.MenuItems.Add(4, new MenuItem("Reset", new System.EventHandler(Reset_Click)));
            ti_main.ContextMenu.MenuItems.Add(4, new MenuItem("Update", new System.EventHandler(Update_Click)));

            // add NotifyIcon by core count
            for (int c = 1; c <= Environment.ProcessorCount; c++)
            {
                th_list.Add(new NotifyIcon() { Visible = true, Icon = Properties.Resources._10 });
            }

            // intialize thread && check auto start
            bool auto_run = Ragistry.CheckAutoRun();
            if (auto_run)
            {
                ch_auto_start.Checked = true;
                Hide();
            }
            init_CPU_Watcher(auto_run);
            l_version.Text = VERSION;

            // check auto update
            bool auto_update = Ragistry.CheckAutoUpdate();
            if (auto_update) { ch_auto_update.Checked = true; self_update(); }

            Activate();

        }
        private void init_CPU_Watcher(bool Immediate_start = true)
        {
            if (th != null) { th.Abort(); th = null; }
            th = new Thread(new ThreadStart(runner));
            if (Immediate_start) { th.Start(); }
        }
        private void Report_Click(Object sender, System.EventArgs e)
        {
            // github report
            Process.Start("https://github.com/Fhwang0926/CoreTracker/issues/new");

        }
        private void Update_Click(Object sender, System.EventArgs e)
        {
            self_update(true);
        }

        // update function with msgbox
        private async void self_update(bool updateAnswer = false)
        {
            // auto update latest
            Int32 v = controller.stringToVersion(VERSION);
            updateFormat rs = await controller.CompareVersion(v);
            if (rs.is_error)
            {
                MessageBox.Show(rs?.msg.ToString(), "Update failed!! :/", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (rs.latest) {
                    if (updateAnswer) { MessageBox.Show(rs?.msg.ToString(), $"{Process.GetCurrentProcess().ProcessName}", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                }
                else
                {
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
                            MessageBox.Show("failed update :/", "Update Failed", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        }
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
            Hide();
        }
        private void Show_Click(Object sender, System.EventArgs e)
        {
            Show();
        }

        private void btn_tray_Click(object sender, EventArgs e)
        {
            Hide();
            ti_main.Visible = true;
            if (!run && !th.IsAlive) { run = true; th.Start(); }
        }

        private void runner()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from Win32_PerfFormattedData_PerfOS_Processor");
            pic_status.Image = Properties.Resources.good;

            while (true)
            {
                if (rb_fast.Checked) { System.Threading.Thread.Sleep(ModeFast); }
                else if (rb_fast.Checked) { System.Threading.Thread.Sleep(ModeNormarl); }
                else { System.Threading.Thread.Sleep(ModeSlow); }

                var cpu_info = searcher.Get().Cast<ManagementObject>().Select(mo => new { Name = mo["Name"], Usage = Convert.ToInt32(mo["PercentProcessorTime"]) }).ToList();
                foreach (var c in cpu_info)
                {
                    if (c.Name.ToString() == "_Total")
                    {
                        continue;
                    }
                    else
                    {
                        th_list[Convert.ToInt32(c.Name)].Icon = setTrayIcon(c.Usage);
                    }
                }
            }
        }


        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            ti_main.Visible = false;
            
        }

        private dynamic setTrayIcon(int value)
        {
            if (0 <= value && value <= 10) { return Properties.Resources._10; }
            else if (10 < value && value <= 20) { return Properties.Resources._20; }
            else if (20 < value && value <= 40) { return Properties.Resources._40; }
            else if (40 < value && value <= 60) { return Properties.Resources._60; }
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
            foreach (var t in th_list )
            {
                t.Dispose();
            }
            controller.RefreshTrayArea();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            // Hide The Form when it's minimized
            if (FormWindowState.Minimized == WindowState) { 
                Hide();
                ti_main.Visible = true;
                if (!run && !th.IsAlive) { run = true; th.Start(); }
            }
                
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/Fhwang0926/CoreTracker");
        }

        private void ch_auto_start_CheckedChanged(object sender, EventArgs e)
        {
            if(ch_auto_start.Checked) { Ragistry.enable_auto_run(); }
            else { Ragistry.disable_auto_run(); }

        }

        private void l_close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void l_hide_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point((this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);
                this.Update();
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
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
            if (ch_auto_update.Checked) { Ragistry.enable_auto_update(); }
            else { Ragistry.disable_auto_update(); }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if(ti_main.Visible) { return; }
            if(e.KeyData == Keys.Escape)
            {
                Hide();
                ti_main.Visible = true;
            }
            //base.OnKeyUp(e);
        }
    }
    #endregion
}
