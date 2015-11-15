using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 荣耀辅助
{
    class COC
    {
        public static void run()
        {
            Task.Factory.StartNew(() => {
                Command.startcmd(
                     dir+"HD-RunApp.exe",
                    "-p com.supercell.clashofclans -a com.supercell.clashofclans.GameApp"
                    );
            });
        }

        public static void runbs()
        {
            Task.Factory.StartNew(() =>
            {
                Process.Start(dir + "HD-StartLauncher.exe");
            });
        }
        public static void bsQuit()
        {
            Task.Factory.StartNew(() =>
            {
                Process.Start(dir + "HD-Quit.exe");
            });
        }
        public static string dir;
        public static  bool IsRegeditItemExist()
        {
            string[] subkeyNames;
            RegistryKey hkml = Registry.LocalMachine;
            RegistryKey software = hkml.OpenSubKey("SOFTWARE");
            subkeyNames = software.GetSubKeyNames();
            //取得该项下所有子项的名称的序列，并传递给预定的数组中  
            foreach (string keyName in subkeyNames)
            //遍历整个数组  
            {
                if (keyName == "BlueStacks")
                //判断子项的名称  
                {
                    //InstallDir
                    RegistryKey InstallDir=  software.OpenSubKey("BlueStacks");
                    dir = InstallDir.GetValue("InstallDir").ToString();
                    hkml.Close();
                    return true;
                }
            }
            hkml.Close();
            return false;
        }


        public static void save(Bitmap bm)
        {
            if (Directory.Exists("date"))
            {
            int i = 0;
            while (File.Exists(@"date\" + i))
            {
                i++;
            }
           savebitmap(bm, @"date\" + i); 

            }
        }


        public static void savebitmap(Bitmap bms, string s)
        {
            Bitmap bm = bms.Clone(new Rectangle(0, 0, bms.Width, bms.Height), bms.PixelFormat);
            FileStream fs = File.Open(s, FileMode.Create);
            BinaryWriter sw = new BinaryWriter(fs);
            sw.Write(bm.Width);
            sw.Write(bm.Height);
            for (int i = 0; i < bm.Width; i++)
            {
                for (int j = 0; j < bm.Height; j++)
                {
                    sw.Write(bm.GetPixel(i, j).ToArgb());
                }
            }
            sw.Close();
            fs.Close();
            bm.Dispose();
        }
    }
}
