using System.Diagnostics;
using Microsoft.Win32;

namespace CmdParamCall
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RegistryKey currentUserKey = Registry.CurrentUser;
            RegistryKey key = currentUserKey.OpenSubKey("Software\\Classes\\ms-settings\\shell\\open\\cmdparam");

            string param = key.GetValue("cmdparam").ToString();

            ProcessStartInfo p = new ProcessStartInfo("cmd.exe");
            p.Arguments = param;
            Process.Start(p);
        }
    }
}
