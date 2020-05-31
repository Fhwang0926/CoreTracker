using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace CoreTracker
{
    static class Program
    {
        // for registration startup
        // if i change on setting, can't excute on starup
        //<requestedExecutionLevel level = "asInvoker" uiAccess="false" />
        //<requestedExecutionLevel level = "requireAdministrator" uiAccess="false" />
        //<requestedExecutionLevel level = "highestAvailable" uiAccess="false" />
        public static bool IsAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                if (!Program.IsAdministrator() && !args.Contains("-debug"))
                {
                    // Restart and run as admin
                    var exeName = Process.GetCurrentProcess().MainModule.FileName;
                    ProcessStartInfo startInfo = new ProcessStartInfo(exeName);
                    startInfo.Verb = "runas";
                    startInfo.Arguments = "restart";
                    Process.Start(startInfo);
                    Application.Exit();
                    return;
                }
                // real start main function
                System.Threading.Mutex mutex = new System.Threading.Mutex(false, System.Diagnostics.Process.GetCurrentProcess().ProcessName);
                try
                {
                    if (mutex.WaitOne(0, false))
                    {
                        // Run the application
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        Application.Run(new Form1());
                    }
                    else
                    {
                        MessageBox.Show("CoreTracker is already Running.", "CoreTracker", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                finally
                {
                    if (mutex != null)
                    {
                        mutex.Close();
                        mutex = null;
                    }
                }
                
            } catch(Exception e)
            {
                string[] lines = { e.Message.ToString(), e.StackTrace.ToString() };
                using (var outputFile = new StreamWriter(Path.Combine(@"C:\Temp\", $"{System.Diagnostics.Process.GetCurrentProcess().ProcessName}.log")))
                {
                    foreach (string line in lines)
                        outputFile.WriteLine(line);
                }
                Application.ExitThread();
                Application.Exit();
            }
            
        }
    }
}
