using Newtonsoft.Json;
using OpenHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoreTracker
{
    internal class Controller
    {
        #region "Refresh Notification Area Icons"

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass,
            string lpszWindow);

        [DllImport("user32.dll")]
        public static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint msg, int wParam, int lParam);

        public void RefreshTrayArea()
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

        #region "self controller - update, restart"
        string name = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
        

        public static async Task<github_result> CheckVersion()
        {
            
            github_result github_result = new github_result();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("User-Agent", "request");
                    var response = await client.GetAsync(new Uri("https://api.github.com/repos/Fhwang0926/CoreTracker/releases/latest")).ConfigureAwait(false);
                    if (response != null)
                    {
                        dynamic jsonString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        github github = JsonConvert.DeserializeObject<github>(jsonString);

                        // update result info
                        github_result.body = github.body;
                        github_result.tag_name = github.tag_name;
                        if (github.assets.Length == 0) { github_result.is_error = true; }
                        github_result.target = github.assets.FirstOrDefault().browser_download_url;
                    }
                    else { github_result.is_error = true; }

                }
            }
            catch (Exception)
            {
                github_result.is_error = true;
            }
            return github_result;
        }

        public Int32 StringToVersion(string v)
        {
            return Convert.ToInt32(v.Substring(1, 5).Replace(".", string.Empty));
        }

        public static async Task<bool> download(string url, string target)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        using (var fs = new FileStream(target, FileMode.OpenOrCreate))
                        {
                            var responseTask = response.Content.CopyToAsync(fs);
                            responseTask.Wait();
                            return true;
                        }
                    } else
                    {
                        return false;
                    }
                }
            } catch (Exception)
            {
                return false;
            }
        }

        public bool restart()
        {
            try
            {
                string cmd = Application.StartupPath + $"\\{name}.bat";

                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        WindowStyle = ProcessWindowStyle.Hidden,
                        FileName = cmd
                    }
                };

                
                process.Start();
                //Process.Start(Application.StartupPath + $"\\{name}.bat");
                return true;
            } catch (Exception)
            {
                return false;
            }
            
            //Process.GetCurrentProcess().Kill();
        }

        private bool setupRestart(string target)
        {
            try
            {
                string[] lines = { "ping 127.0.0.1 -n 2 > NULL", "cd %~dp0", "echo off", "cls", "echo start update : " + name, $"taskkill /IM {name}.exe /F", "timeout 2 > NUL",  $"START /B {target}", "del %0" };
                using (var outputFile = new StreamWriter(Path.Combine(Application.StartupPath, $"{name}.bat"))) { foreach (string line in lines) { outputFile.WriteLine(line); } }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
            return true;
        }

        public async Task<updateFormat> startDownload(string url)
        {
            string target = $"{Application.UserAppDataPath}\\" + System.Diagnostics.Process.GetCurrentProcess().ProcessName + "_new_installer.exe";
            // start download
            if (await download(url, target))
            {
                // setup restart bat file
                if (setupRestart(target)) { return new updateFormat { msg = "download done :D, if click ok button restart program", is_error = false }; }
                else { return new updateFormat { msg = "download done :D, but is failed to setup override new version", is_error = true }; }
            }
            else
            {
                return new updateFormat { msg = "download failed, try later or check internet status", is_error = true };
            }

        }

        public async Task<updateFormat> CompareVersion(Int32 v)
        {
            github_result rs = await CheckVersion();
            if (rs.is_error) { return new updateFormat { msg = "version check failed, try later or check internet status", is_error = true }; }

            if (StringToVersion(rs.tag_name) > v)
            {
                return new updateFormat { target = rs.target, msg = "Can you update the latest release version?" };
            }
            else
            {
                return new updateFormat { msg = "Recently version", latest = true };
            }
        }
        #endregion

        #region "hardware"
        public Computer computer = new Computer();
        public scoreBox sb = new scoreBox();
        public UpdateVisitor updateVisitor = new UpdateVisitor();
        public List<ISensor> cpuList;

        public void Dispose()
        {
            computer.Close();
        }

        public void hardwareMoniterInit()
        {
            computer.Open();
        }
        public void hardwareInfo()
        {
            // enable or disable setting
            /*computer.CPUEnabled = true;         // default
            computer.RAMEnabled = true;         // optional
            computer.MainboardEnabled = true;   // optional
            computer.GPUEnabled = true;         // optional*/
            
            // update info
            computer.GetReport();
            computer.Accept(updateVisitor);

            foreach (IHardware h in computer.Hardware)
            {
                ISensor info = null;
                switch (h.HardwareType)
                {
                    case HardwareType.CPU:

                        // sub
                        if (!(h.SubHardware is null))
                        {
                            foreach (var s in h.SubHardware)
                            {
                                info = s.Sensors.Where(ss => ss.SensorType == SensorType.Temperature && ss.Name == "CPU Package").FirstOrDefault();
                                if (info is null) { break; }
                                sb.addCpuScore((int)info.Value);

                            }
                        }
                        // standartd
                        info = h.Sensors.Where(ss => ss.SensorType == SensorType.Temperature).FirstOrDefault();
                        if (!(info is null) && !(info.Value is null)) { sb.addCpuScore((int)info.Value); }

                        // usage by core
                        cpuList = h.Sensors.Where(ss => ss.SensorType == SensorType.Load).ToList<ISensor>();
                        //Console.WriteLine("{0} : {1}", h.HardwareType, sb.cpu_temperature);
                        break;
                    case HardwareType.GpuAti:

                        // sub
                        if (!(h.SubHardware is null))
                        {
                            foreach (var s in h.SubHardware)
                            {
                                var sc_sub = s.Sensors.Where(ss => ss.SensorType == SensorType.Temperature && ss.Name == "GPU Core").FirstOrDefault();
                                if (sc_sub is null) { break; }
                                sb.addGpuScore((int)sc_sub.Value);

                            }
                        }
                        // standartd
                        info = h.Sensors.Where(ss => ss.SensorType == SensorType.Temperature && ss.Name == "GPU Core").FirstOrDefault();
                        if (!(info is null) && !(info.Value is null)) { sb.addGpuScore((int)info.Value); }

                        // Console.WriteLine("{0} : {1}", h.HardwareType, sb.gpu_temperature);
                        break;
                    case HardwareType.GpuNvidia:

                        // sub
                        if (!(h.SubHardware is null))
                        {
                            foreach (var s in h.SubHardware)
                            {
                                info = s.Sensors.Where(ss => ss.SensorType == SensorType.Temperature && ss.Name == "GPU Core").FirstOrDefault();
                                if (info is null) { break; }
                                sb.addGpuScore((int)info.Value);

                            }
                        }
                        // standartd
                        info = h.Sensors.Where(ss => ss.SensorType == SensorType.Temperature && ss.Name == "GPU Core").FirstOrDefault();
                        if (!(info is null) && !(info.Value is null)) { sb.addGpuScore((int)info.Value); }

                        // Console.WriteLine("{0} : {1}", h.HardwareType, sb.gpu_temperature);
                        break;
                    case HardwareType.RAM:

                        // sub
                        if (!(h.SubHardware is null))
                        {
                            foreach (var s in h.SubHardware)
                            {
                                info = s.Sensors.Where(ss => ss.SensorType == SensorType.Load && ss.Name == "Memory").FirstOrDefault();
                                if (info is null) { break; }
                                sb.addRamScore((int)info.Value);

                            }
                        }
                        // standartd
                        info = h.Sensors.Where(ss => ss.SensorType == SensorType.Load && ss.Name == "Memory").FirstOrDefault();
                        if (!(info is null) && !(info.Value is null)) { sb.addRamScore((int)info.Value); }

                        // Console.WriteLine("{0} : {1}", h.HardwareType, sb.ram_usage);
                        break;
                    case HardwareType.Mainboard:

                        // sub
                        if (!(h.SubHardware is null))
                        {
                            foreach (var sh in h.SubHardware)
                            {
                                foreach (var ss in sh.Sensors.Where(ss => ss.SensorType == SensorType.Temperature).ToList())
                                {
                                    sb.addboardScore((int)(ss.Value ?? 0));
                                }
                            }
                        }
                        // standartd
                        info = h.Sensors.Where(ss => ss.SensorType == SensorType.Temperature).FirstOrDefault();
                        if (!(info is null) && !(info.Value is null)) { sb.addboardScore((int)info.Value); }

                        // Console.WriteLine("{0} : {1}", h.HardwareType, sb.board_temperature);
                        break;
                }
            }
        }
        #endregion
    }

    #region extenstion

    public class UpdateVisitor : IVisitor
    {
        public void VisitComputer(IComputer computer)
        {
            computer.Traverse(this);
        }
        public void VisitHardware(IHardware hardware)
        {
            hardware.Update();
            foreach (IHardware subHardware in hardware.SubHardware) subHardware.Accept(this);
        }
        public void VisitSensor(ISensor sensor) { }
        public void VisitParameter(IParameter parameter) { }
    }

    #endregion
}
