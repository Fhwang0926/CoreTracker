using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoreTracker
{
    static class Program
    {
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
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
