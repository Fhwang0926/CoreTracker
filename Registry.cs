using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoreTracker
{
    class Ragistry
    {
        private RegistryKey startup_key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        private string PN = "CoreTracker"; // program name

        public bool CheckAutoRun()
        {
            var target = startup_key.GetValue(PN);
            return string.IsNullOrEmpty(target?.ToString()) ? false : true;
        }

        public void Register()
        {
            startup_key.SetValue(PN, Application.ExecutablePath);
        }

        public void Unregister()
        {
            startup_key.DeleteValue(PN, false);
        }


    }
}
