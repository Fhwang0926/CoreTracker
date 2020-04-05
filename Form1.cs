using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoreTracker
{
    #region application
    public partial class Form1 : Form
    {
        private List<NotifyIcon> th_list = new List<NotifyIcon>();
        private bool run = false;
        private Thread th;
        private Int16 ModeSlow = 5000;
        private Int16 ModeNormarl = 3000;
        private Int16 ModeFast = 1000;
        private RegistryKey startup_key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        private string PN = "CoreTracker"; // program name

        public Form1()
        {
            InitializeComponent();

            // under code run after InitializeComponent

            // update
            l_core_value.Text = (Environment.ProcessorCount / 2).ToString();
            l_th_value.Text = Environment.ProcessorCount.ToString();

            // ass running status
            pic_status.Image = Properties.Resources.bad;
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddEllipse(0, 0, pic_status.Width - 4, pic_status.Height - 2);
            pic_status.Region = new Region(gp);

            // add NotifyIcon mouse right button menu
            ti_main.Visible = true;
            ti_main.ContextMenu = new ContextMenu();
            ti_main.ContextMenu.MenuItems.Add(0, new MenuItem("Exit", new System.EventHandler(Exit_Click)));
            ti_main.ContextMenu.MenuItems.Add(1, new MenuItem("Show", new System.EventHandler(Show_Click)));
            ti_main.ContextMenu.MenuItems.Add(2, new MenuItem("Hide", new System.EventHandler(Hide_Click)));
            ti_main.ContextMenu.MenuItems.Add(3, new MenuItem("Report", new System.EventHandler(Report_Click)));
            //ti_main.ContextMenu.MenuItems.Add(4, new MenuItem("Update", new System.EventHandler(Update_Click)));
            //ti_main.ContextMenu.MenuItems.Add(5, new MenuItem("Reset", new System.EventHandler(Reset_Click)));


            // add NotifyIcon by core count
            for (int c = 1; c <= Environment.ProcessorCount; c++)
            {
                th_list.Add(new NotifyIcon() { Visible = true, Icon = Properties.Resources._10 });
            }

            // intialize thread && check auto start
            var target = startup_key.GetValue(PN);
            bool auto_run = string.IsNullOrEmpty(target?.ToString()) ? false : true;
            if (auto_run) { ch_auto_start.Checked = true; }
            init_CPU_Watcher(auto_run);


            // not yet support func
            ch_auto_update.Enabled = false;
            ch_auto_update.Text = ch_auto_update.Text += "(coming soon)";
            ch_auto_bugreport.Enabled = false;
            ch_auto_bugreport.Text = ch_auto_bugreport.Text += "(coming soon)";

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
            // auto update latest
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
                        //l_cpu_value.Text = c.Usage.ToString();
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
            //Tray.RefreshTrayArea();
        }

        private void Form1_FormClosing(object sender, EventArgs e)
        {
            if (th != null) { th.Abort(); th = null; }
            foreach (var t in th_list )
            {
                t.Dispose();
            }
            Tray.RefreshTrayArea();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            // Hide The Form when it's minimized
            if (FormWindowState.Minimized == WindowState) { 
                Hide();
                ti_main.Visible = true;
                if (!run) { run = true; th.Start(); }
            }
                
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/Fhwang0926/CoreTracker");
        }

        private void ch_auto_start_CheckedChanged(object sender, EventArgs e)
        {
            if(ch_auto_start.Checked)
            {
                startup_key.SetValue(PN, Application.ExecutablePath);
                //startup_key.GetValue(PN);
            } else
            {
                startup_key.DeleteValue(PN, false);
            }

        }

    }
    #endregion

    #region "Refresh Notification Area Icons"

    public partial class Tray

    {

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass,
            string lpszWindow);

        [DllImport("user32.dll")]
        public static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint msg, int wParam, int lParam);

        public static void RefreshTrayArea()
        {
            IntPtr systemTrayContainerHandle = FindWindow("Shell_TrayWnd", null);
            IntPtr systemTrayHandle = FindWindowEx(systemTrayContainerHandle, IntPtr.Zero, "TrayNotifyWnd", null);
            IntPtr sysPagerHandle = FindWindowEx(systemTrayHandle, IntPtr.Zero, "SysPager", null);
            IntPtr notificationAreaHandle = FindWindowEx(sysPagerHandle, IntPtr.Zero, "ToolbarWindow32", "Notification Area");
            if (notificationAreaHandle == IntPtr.Zero) notificationAreaHandle = FindWindowEx(sysPagerHandle, IntPtr.Zero, "ToolbarWindow32", null);
            if (notificationAreaHandle == IntPtr.Zero)
            {
                notificationAreaHandle = FindWindowEx(sysPagerHandle, IntPtr.Zero, "ToolbarWindow32", "User Promoted Notification Area");
                IntPtr notifyIconOverflowWindowHandle = FindWindow("NotifyIconOverflowWindow", null);
                IntPtr overflowNotificationAreaHandle = FindWindowEx(notifyIconOverflowWindowHandle, IntPtr.Zero, "ToolbarWindow32", "Overflow Notification Area");
                RefreshTrayArea(overflowNotificationAreaHandle);
            }
            RefreshTrayArea(notificationAreaHandle);
        }

        private static void RefreshTrayArea(IntPtr windowHandle)
        {
            const uint wmMousemove = 0x0200;
            RECT rect;
            GetClientRect(windowHandle, out rect);
            for (var x = 0; x < rect.right; x += 5)
                for (var y = 0; y < rect.bottom; y += 5)
                    SendMessage(windowHandle, wmMousemove, 0, (y << 16) + x);
        }
        #endregion
    }
}
