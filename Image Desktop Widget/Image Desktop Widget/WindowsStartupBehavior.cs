using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Image_Desktop_Widget
{
    class WindowsStartupBehavior : StartupBehavior
    {

        private static readonly string AppName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

        public override bool IsRunAtStartup => Properties.Settings.Default.AllowStartup;
        
        private RegistryKey GetApplicationStartUpKey()
        {
            return Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        }
        
        public override void RunAtStartup(bool enabled)
        {
            if(IsUserAdministrator())
            {
                if(enabled)
                {
                    using (RegistryKey key = GetApplicationStartUpKey())
                    {
                        try
                        {
                            key.SetValue(AppName, "\"" + System.Reflection.Assembly.GetExecutingAssembly().Location + "\"");
                            Properties.Settings.Default.AllowStartup = true;
                            Properties.Settings.Default.Save();
                            OnRunAtStartupSet(true, Properties.Settings.Default.AllowStartup);
                        }
                        catch
                        {
                            OnRunAtStartupSet(false, Properties.Settings.Default.AllowStartup);
                        }
                    }
                }
                else
                {
                    using (RegistryKey key = GetApplicationStartUpKey())
                    {
                        try
                        {
                            key.DeleteValue(AppName, false);
                            Properties.Settings.Default.AllowStartup = false;
                            Properties.Settings.Default.Save();
                            OnRunAtStartupSet(true, Properties.Settings.Default.AllowStartup);
                        }
                        catch
                        {
                            OnRunAtStartupSet(false, Properties.Settings.Default.AllowStartup);
                        }
                    }
                }
            }
            else
            {
                OnRunAtStartupSet(false, Properties.Settings.Default.AllowStartup);
            }
        }
        
        private static bool IsUserAdministrator()
        {
            //bool value to hold our return value
            bool isAdmin;
            try
            {
                //get the currently logged in user
                WindowsIdentity user = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(user);
                isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch (UnauthorizedAccessException ex)
            {
                ex.ToString();
                isAdmin = false;
            }
            catch (Exception ex)
            {
                ex.ToString();
                isAdmin = false;
            }
            return isAdmin;
        }
        
    }
}
