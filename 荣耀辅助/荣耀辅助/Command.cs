using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 荣耀辅助
{
    class Command
    {
        public static string startcmd(string command)
        {
            string output = "";
            try
            {

                Process cmd = new Process();
                cmd.StartInfo.FileName = command;

                cmd.StartInfo.UseShellExecute = false;

                cmd.StartInfo.RedirectStandardInput = true;
                cmd.StartInfo.RedirectStandardOutput = true;

                cmd.StartInfo.CreateNoWindow = true;
                cmd.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

                cmd.Start();

                output = cmd.StandardOutput.ReadToEnd();
                Console.WriteLine(output);
                cmd.WaitForExit();
                cmd.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return output;
        }
        public static string startcmd(string command, string argument)
        {
            string output = "";
            try
            {
                Process cmd = new Process();

                cmd.StartInfo.FileName = command;
                cmd.StartInfo.Arguments = argument;

                cmd.StartInfo.UseShellExecute = false;

                cmd.StartInfo.RedirectStandardInput = true;
                cmd.StartInfo.RedirectStandardOutput = true;

                cmd.StartInfo.CreateNoWindow = true;
                cmd.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

                cmd.Start();

                output = cmd.StandardOutput.ReadToEnd();
                Console.WriteLine(output);
                cmd.WaitForExit();
                cmd.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return output;
        }

    }
}
