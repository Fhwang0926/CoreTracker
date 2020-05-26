using Microsoft.Win32;
using System.Windows.Forms;
using System;
using System.Security.Principal;

namespace CoreTracker
{
    class Ragistry
    {
        private readonly RegistryKey startup_key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        private readonly RegistryKey auto_update_key = Registry.CurrentUser.CreateSubKey("auto_update_key");
        private readonly RegistryKey cpu_temperature_key = Registry.CurrentUser.CreateSubKey("cpu_temperature_key");
        private readonly RegistryKey ram_temperature_key = Registry.CurrentUser.CreateSubKey("ram_temperature_key");
        private readonly RegistryKey graphic_temperature_key = Registry.CurrentUser.CreateSubKey("graphic_temperature_key");
        private readonly RegistryKey board_temperature_key = Registry.CurrentUser.CreateSubKey("board_temperature_key");
        private readonly RegistryKey disable_busy_alert_key = Registry.CurrentUser.CreateSubKey("disable_busy_alert_key");
        private readonly string PN = "CoreTracker"; // program name
        private RegistryKey UserKey = Registry.Users;
        private readonly RegistryKey trayicon_key = Registry.Users.OpenSubKey($"{WindowsIdentity.GetCurrent().User.ToString()}\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer", true);
        //private readonly RegistryKey current_trayicon_key = Registry.CurrentUser.OpenSubKey($"Software\\Microsoft\\Windows\\CurrentVersion\\Explorer", true);

        public void ToggleTrayIconConfig()
        {
            bool toggle = Convert.ToBoolean(trayicon_key.GetValue("EnableAutoTray"));
            trayicon_key.SetValue("EnableAutoTray", toggle ? 0 : 1); // not
            //current_trayicon_key.SetValue("EnableAutoTray", toggle ? 0 : 1); // not
        }

        public bool CheckTrayIconConfig()
        {
            return !Convert.ToBoolean(trayicon_key.GetValue("EnableAutoTray"));
        }

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

        public bool CheckCpuTemperature()
        {
            var target = cpu_temperature_key.GetValue(PN);
            return string.IsNullOrEmpty(target?.ToString()) ? false : true;
        }

        public bool CheckRamTemperature()
        {
            var target = ram_temperature_key.GetValue(PN);
            return string.IsNullOrEmpty(target?.ToString()) ? false : true;
        }

        public bool CheckBoardTemperature()
        {
            var target = board_temperature_key.GetValue(PN);
            return string.IsNullOrEmpty(target?.ToString()) ? false : true;
        }

        public bool ChecGraphicTemperature()
        {
            var target = graphic_temperature_key.GetValue(PN);
            return string.IsNullOrEmpty(target?.ToString()) ? false : true;
        }

        public bool ChecDisableBusyAlert()
        {
            var target = disable_busy_alert_key.GetValue(PN);
            return string.IsNullOrEmpty(target?.ToString()) ? false : true;
        }

        public void enable_auto_run()
        {
            startup_key.SetValue(PN, '"' + Application.ExecutablePath + "'");
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

        public void disable_cpu_temperature()
        {
            cpu_temperature_key.DeleteValue(PN, false);
        }

        public void disable_ram_temperature()
        {
            ram_temperature_key.DeleteValue(PN, false);
        }

        public void disable_board_temperature()
        {
            board_temperature_key.DeleteValue(PN, false);
        }

        public void disable_graphic_temperature()
        {
            graphic_temperature_key.DeleteValue(PN, false);
        }

        public void enable_cpu_temperature()
        {
            cpu_temperature_key.SetValue(PN, true);
        }

        public void enable_ram_temperature()
        {
            ram_temperature_key.SetValue(PN, true);
        }

        public void enable_board_temperature()
        {
            board_temperature_key.SetValue(PN, false);
        }

        public void enable_graphic_temperature()
        {
            graphic_temperature_key.SetValue(PN, true);
        }

        public void enable_busy_alert()
        {
            disable_busy_alert_key.SetValue(PN, false);
        }

        public void disable_busy_alert()
        {
            disable_busy_alert_key.SetValue(PN, true);
        }
    }
}
