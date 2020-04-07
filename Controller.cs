using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
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
                        var jsonString = await response.Content.ReadAsStringAsync();
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
            catch (Exception e)
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
            } catch (Exception e)
            {
                return false;
            }
        }

        private void restart()
        {
            string path = Application.StartupPath + @"\update.bat";
            string name = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
            try
            {
                string[] lines = { "cd %~dp0", "echo off", "cls", "echo start update : " + name, "timeout 3 > NUL", $"xcopy {name}_new.exe {name}.exe /K /D /H /Y", $"del /F {name}.exe", $"START /B {name}.exe" };
                using (var outputFile = new StreamWriter(Path.Combine(Application.StartupPath, $"{name}.bat")))
                {
                    foreach (string line in lines)
                        outputFile.WriteLine(line);
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            try
            {
                Process.Start(Application.StartupPath + $"\\{name}.bat");
                Process.GetCurrentProcess().Kill();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }


        public async void Update(Int32 v)
        {
            github_result rs = await CheckVersion();
            if(stringToVersion(rs.tag_name) > v)
            {
                Console.WriteLine("need update");
                if(await download(rs.target))
                {
                    Console.WriteLine("start update");
                    restart();

                } else
                {
                    Console.WriteLine("download failed");
                }
            }
            else
            {
                Console.WriteLine("recently version");
            }
            
        }

        #endregion
    }
}
