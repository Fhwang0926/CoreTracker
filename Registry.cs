using Microsoft.Win32;
using System.Windows.Forms;

namespace CoreTracker
{
    class Ragistry
    {
        private readonly RegistryKey startup_key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        private readonly RegistryKey auto_update_key = Registry.CurrentUser.CreateSubKey("auto_update_key");
        private readonly string PN = "CoreTracker"; // program name

        public bool CheckAutoRun()
        {
            var target = startup_key.GetValue(PN);
            return string.IsNullOrEmpty(target?.ToString()) ? false : true;
        }

        public bool CheckAutoUpdate()
        {
            var target = auto_update_key.GetValue(PN);
            return string.IsNullOrEmpty(target?.ToString()) ? false : true;
        }

        public void enable_auto_run()
        {
            startup_key.SetValue(PN, Application.ExecutablePath);
        }

        public void disable_auto_run()
        {
            startup_key.DeleteValue(PN, false);
        }

        public void enable_auto_update()
        {
            auto_update_key.SetValue(PN, Application.ExecutablePath);
        }

        public void disable_auto_update()
        {
            auto_update_key.DeleteValue(PN, false);
        }


    }
}
