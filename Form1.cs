using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoreTracker
{
    public partial class Form1 : Form
    {
        private List<NotifyIcon> th_list = new List<NotifyIcon>();
        private bool run = false;
        private Thread th;

        public Form1()
        {
            InitializeComponent();
            // under code run after InitializeComponent
            l_core_value.Text = (Environment.ProcessorCount / 2).ToString();
            l_th_value.Text = Environment.ProcessorCount.ToString();

            ti_main.Visible = true;
            ti_main.ContextMenu = new ContextMenu();
            ti_main.ContextMenu.MenuItems.Add(0, new MenuItem("Show", new System.EventHandler(Show_Click)));
            ti_main.ContextMenu.MenuItems.Add(1, new MenuItem("Hide", new System.EventHandler(Hide_Click)));
            ti_main.ContextMenu.MenuItems.Add(2, new MenuItem("Exit", new System.EventHandler(Exit_Click)));

            for (int c = 1; c <= Environment.ProcessorCount; c++)
            {
                th_list.Add(new NotifyIcon() { Visible = true, Icon = Properties.Resources._10 });
            }
            // intialize thread
            th = new Thread(new ThreadStart(runner));

        }
        protected void Exit_Click(Object sender, System.EventArgs e)
        {
            Close();
        }
        protected void Hide_Click(Object sender, System.EventArgs e)
        {
            Hide();
        }
        protected void Show_Click(Object sender, System.EventArgs e)
        {
            Show();
        }

        private void btn_tray_Click(object sender, EventArgs e)
        {
            Hide();
            ti_main.Visible = true;

            if (!run) { run = true; th.Start(); }
        }

        private void runner()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from Win32_PerfFormattedData_PerfOS_Processor");
            while (true)
            {
                System.Threading.Thread.Sleep(3000);
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
            
        }

        private void Form1_Resize(object sender, System.EventArgs e)
        {
            // Hide The Form when it's minimized
            if (FormWindowState.Minimized == WindowState) { 
                Hide();
                ti_main.Visible = true;
                if (!run) { run = true; th.Start(); }
            }
                
        }

    }
}
