using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoreTracker
{
    class Controller
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

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
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
                    var response = await client.GetAsync("https://api.github.com/repos/Fhwang0926/CoreTracker/releases/latest");
                    if (response != null)
                    {
                        dynamic jsonString = await response.Content.ReadAsStringAsync();
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

        public Int32 stringToVersion(string v)
        {
            return Convert.ToInt32(v.Substring(1, 5).Replace(".", string.Empty));

        }

        public static async Task<bool> download(string url)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        using (var fs = new FileStream($"{Application.StartupPath}\\" + System.Diagnostics.Process.GetCurrentProcess().ProcessName+"_new.exe", FileMode.OpenOrCreate))
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

        private bool setupRestart()
        {
            string path = Application.StartupPath + @"\update.bat";
            try
            {
                string[] lines = { "ping 127.0.0.1 -n 2 > NULL", "cd %~dp0", "echo off", "cls", "echo start update : " + name, $"taskkill /IM {name}.exe /F", "timeout 2 > NUL",  $"move /Y {name}_new.exe {name}.exe", "timeout 1 > NUL", $"START /B {name}.exe", "del %0" };
                using (var outputFile = new StreamWriter(Path.Combine(Application.StartupPath, $"{name}.bat")))
                {
                    foreach (string line in lines) { outputFile.WriteLine(line); }
                }
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
            // start download
            if (await download(url))
            {
                // setup restart bat file
                if (setupRestart()) { return new updateFormat { msg = "download done :D, if click ok button restart program", is_error = false }; }
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

            if (stringToVersion(rs.tag_name) > v)
            {
                return new updateFormat { target = rs.target, msg = "Can you update the latest release version?" };
            }
            else
            {
                return new updateFormat { msg = "Recently version", latest = true };
            }
        }
        #endregion
    }
}
