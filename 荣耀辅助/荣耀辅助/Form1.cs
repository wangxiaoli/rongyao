using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 荣耀辅助
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
        int tabIndex = 0;
        int startupCount = 0;
        int killoutCount = 0;
        bool orcme = true;
        int[] ressourcesValues = new int[] { 0, 0, 0, 0 };
        int[] firstressources = new int[] { 0, 0, 0, 0 };
        bool firstshow = true;
        bool show = false;
        List<Point> resourcePoint; 
        Bitmap nvwang;
        Bitmap icoCOC;

        List<Point> clickPoint1 = new List<Point>();
        List<Point> clickPoint2 = new List<Point>();
        List<Point> clickPoint3 = new List<Point>();
        List<Point> clickPoint4 = new List<Point>();

        DateTime dtbegin = DateTime.Now;
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            tabIndex = tabControl1.SelectedIndex;
            if (tabIndex == 1)
            {
                emulatorInit();
            }
            if (tabIndex == 3)
                richTextBox1.Text = buffer.ToString();
        }
        private void emulatorInit()
        {
            RegistryKey key = Registry.LocalMachine;

            RegistryKey myreg = key.OpenSubKey(@"SOFTWARE\BlueStacks\Guests\Android\FrameBuffer\0");
            string WindowWidth = myreg.GetValue("WindowWidth").ToString();
            string GuestWidth = myreg.GetValue("GuestWidth").ToString();
            string WindowHeight = myreg.GetValue("WindowHeight").ToString();
            string GuestHeight = myreg.GetValue("GuestHeight").ToString();
            myreg.Close();
            if (WindowHeight == GuestHeight && WindowWidth == GuestWidth)
            {
                label1.Text = "宽：" + WindowWidth + ",高：" + WindowHeight;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            COC.runbs();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            IntPtr gH = win32.FindWindow(null, "BlueStacks App Player");
            if (gH != IntPtr.Zero)
            {
                win32.SendMessage(gH, win32.WM_CLOSE, 0, 0);
                Thread.Sleep(1000);
            }
            RegistryKey key = Registry.LocalMachine;
            int w = 800, h = 700;
            RegistryKey myreg = key.OpenSubKey(@"SOFTWARE\BlueStacks\Guests\Android\FrameBuffer\0", true);
            myreg.SetValue("WindowWidth", w);
            myreg.SetValue("GuestWidth", w);
            myreg.SetValue("WindowHeight", h);
            myreg.SetValue("GuestHeight", h);
            myreg.Close();

            COC.bsQuit();
            Thread.Sleep(2000);
            COC.runbs();
        }
        bool isrunscript = false;
        private void button3_Click(object sender, EventArgs e)
        {
            //button3.Enabled = false;
            if (button3.Text=="运行")
            {
                tabControl1.SelectedIndex = 3;
                button3.Text = "停止";
                isrunscript = true;
                scriptRunning();
                return;
            }
            isrunscript = false;
        }

        private void addPoints(string ss, List<Point> list)
        {

            string[] pointsss = ss.Split(new char[] { ' ' });
            foreach (string item in pointsss)
            {
                string[] point = item.Split(new char[] { ',' });
                list.Add(new Point(int.Parse(point[0]), int.Parse(point[1])));
            }
        }

        private void scriptRunning()
        {
            setlog("运行脚本");
            startupCount = 0;
            int noreg = 0;
            int blackcount = 0;
            dtbegin = DateTime.Now;
            timer1.Interval = 1000;
            timer1.Enabled = true;
            Task.Factory.StartNew(() =>
            {
                icoCOC = OPImage.stringToBitmap("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JUaF/xIkU/8AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA==");
                nvwang = OPImage.stringToBitmap("CgAAAAoAAACxbxf/1ODm/66+3/+UoPj/uMX//8XX//+7z///sMb+/6a8/v+Ys///ejoj/7fL2f+Tncv/kZDx/6u5+/+70f//uM7//7DG+/+nvP7/oLb//1teWv9/kqb/bW66/4OB3/+gsPj/tMr+/7bK/f+rwfv/qLz+/6i6/P9LMi//Sk6S/11mu/95dNT/lqT4/6W7/v+nv/v/pr37/6jB+/+uwfr/QDNg/11swv+Fluz/k6T4/6G1+P+htfr/m6L4/5Oh9/+uxP//sMT5/0MtWP9nd8j/r8L7/8jZ///Q3///ydz//5ql8/9ITrP/jqHs/6G1+P81H0L/NzuT/2hz1f+Nme7/nabz/5ul9P94btr/KjeJ/1ppvv+Lm+T/Kxs7/yYtaf9PTaj/ZWO9/4B83P+PhOj/Y1PD/ycycv9QX7D/eYXV/xgcRP8nL2n/Q0ub/1pZrf+AbND/iHfd/01Ir/8vPYD/VGGz/3F+0/8eI1X/NDl1/0hPnv9jY7z/gHXZ/4B22v88SZX/RVem/2NyxP96ieH/AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA=");
                ORC.init();
                string pointsss = "251,163 226,177 208,190 345,94 363,86 325,110 120,256 116,271 91,280 85,298 99,283 125,257 135,254 148,245 167,218 194,206 227,182 251,169 263,160 281,151 296,138 312,125 338,110 358,96 346,97 336,105 327,110 311,119 291,141 262,161 227,182 245,160 210,197 188,209 170,222 159,236 136,253 125,257 123,261 121,265";
                addPoints(pointsss, clickPoint1);
                pointsss = "539,159 565,181 585,193 430,84 440,95 454,95 691,271 698,277 717,287 646,247 639,236 633,232 625,221 607,216 602,207 599,200 586,189 578,185 568,174 554,172 538,158 517,144 506,136 495,128 481,120 481,116 471,106 456,98 454,89";
                addPoints(pointsss, clickPoint2);
                pointsss = "203,445 242,474 272,497 337,550 355,558 100,375 115,381 74,347 96,366 82,355 76,352 106,380 122,390 132,395 146,402 154,409 169,422 185,433 192,441 197,444 206,449 220,455 222,464 234,471 245,486 251,489 263,497 280,503 286,510 290,522 304,522 317,540 336,551";
                addPoints(pointsss, clickPoint3);
                pointsss = "584,469 601,453 632,430 686,385 703,375 465,543 488,536 493,529 507,525 511,512 521,512 540,494 533,504 525,507 546,493 555,479 576,468 586,466 603,449 614,448 622,445 644,420 635,422 629,425 657,403 670,398 681,389 697,386 699,377 726,366 724,359 703,368 718,363";
                addPoints(pointsss, clickPoint4);

                while (!IsDisposed&&isrunscript)
                {
                    IntPtr gH = win32.FindWindow(null, "BlueStacks App Player");
                    if (gH != IntPtr.Zero)
                    {
                        Bitmap bm = OPImage.GetWindowCapture(gH);
                        if (bm.Width < 400)
                        {
                            bm.Dispose();
                            setlog("窗口最小化了，使之正常窗口。");
                            win32.ShowWindow(gH, 1);
                            sleep(0, 3);
                            continue;
                        }
                        if (OPImage.check(bm, "769 103 10 0 2b84d9055bc2b9b518275cfe775aaff loadok"))
                        {
                            killoutCount = 0;
                            if (OPImage.check(bm, "313 340 10 0 669ad4534db488e23d6f5fddab783a87 聊天收起"))
                            {
                                win32.MouseClick(gH, 310, 368, 2000);
                                bm.Dispose(); noreg = 0; blackcount = 0;
                                continue;
                            }
                            startupCount = 0;
                            searchTimeout = 0;
                            if (!OPImage.check(bm, "584 33 10 0 9662a9460e0638f888dc97a17c08927 缩放成功标志"))
                            {
                                bm.Dispose(); noreg = 0; blackcount = 0;
                                setlog("缩放。");
                                for (int i = 0; i < 7; i++)
                                {
                                    win32.keyClick(gH, (int)Keys.Down, 200);
                                }

                                win32.MouseWheel(gH, 700, 370, 500);
                                sleep(0, 2);
                                continue;
                            }

                            win32.MouseWheel(gH, 700, 370, 500);

                            setlog("COC启动成功。");
                            try
                            {
                                if (orcme)
                                    recogniseME(bm);
                            }
                            catch (Exception)
                            {
                                setlog("识别金水杯失败。");
                            }

                            List<Point> pClick = ImageFind.ToCollectingResources(bm);
                            List<Point> pT =
                            ImageFind.find1inex(bm, "-791208 -2113739 -1719754 -1061057 -532921 25 金币");
                            List<Rectangle> rT = new List<Rectangle>();
                            foreach (Point item in pClick)
                            {
                                rT.Add(new Rectangle(item.X - 5, item.Y - 5, 50, 50));
                            }
                            foreach (Point item in pT)
                            {
                                bool b = true;
                                foreach (Rectangle ritem in rT)
                                {
                                    if (ritem.Contains(item))
                                    {
                                        b = false; break;
                                    }
                                }
                                if (b) pClick.Add(item);
                            }

                            if (pClick.Count > 0)
                            {
                                setlog("收集资源：" + pClick.Count);
                                foreach (Point item in pClick)
                                {
                                    win32.MouseClick(gH, item.X, item.Y, 100);
                                }
                            }

                            bm.Dispose(); noreg = 0; blackcount = 0;
                            sleep(0, 5);

                            scriptDo();
                            sleep(0, 1);
                            continue;
                        }
                        if (OPImage.check(bm, "696 142 10 0 bee49251fd82af344d742de3f812b87 loading"))
                        {
                            bm.Dispose(); noreg = 0; blackcount = 0;
                            startupCount++;
                            setlog("COC正在启动。");
                            sleep(0, 5);
                            if (startupCount > 18)
                            {
                                scriptKill();
                                sleep(0, 5);
                            }
                            continue;
                        }
                        if (OPImage.check(bm, "80 170 60 0 d9e5ea20e5c12c1d8321a565a3c7cd3e bs启动成功"))
                        {
                            List<Point> ps = OPImage.GetImageContains(bm,icoCOC,0);
                            bm.Dispose(); noreg = 0; blackcount = 0;
                            startupCount = 0;
                            setlog("bs启动成功。");
                            if (ps.Count>0)
                            {
                                win32.MouseClick(gH, ps[0].X, ps[0].Y, 0);
                            }
                            else
                            {
                                sleep(0, 2);
                                COC.run();
                            }
                            sleep(0, 5);
                            continue;
                        }
                        if (OPImage.check(bm, "726 68 28 0 b1b41953d54aa4dd249375af767b16d bs启动中"))
                        {
                            bm.Dispose(); noreg = 0; blackcount = 0;
                            startupCount = 0;
                            setlog("bs启动中。");
                            sleep(0, 5);
                            continue;
                        }
                        if (OPImage.check(bm, "403 418 46 0 97672a88e51eaa972b4e9bcb93f6181 请重试"))
                        {
                            bm.Dispose(); noreg = 0; blackcount = 0;
                            setlog("请重试，处理掉线。");
                            win32.MouseClick(gH, 100, 100, 0);
                            sleep(0, 5);
                            startupCount = 0;
                            continue;
                        }
                        if (OPImage.check(bm, "403 417 100 30 1fc18f146c8dcea26d8843e4c61e0e9 重新载入游戏"))
                        {
                            bm.Dispose(); noreg = 0; blackcount = 0;
                            setlog("重新载入游戏");
                            win32.MouseClick(gH, 100, 100, 0);
                            sleep(0, 5);
                            startupCount = 0;
                            continue;
                        }
                        if (OPImage.check(bm, "402 413 100 30 6c278bda96dfc57f15488e724c5e6a7 休息6分钟重新载入"))
                        {
                            bm.Dispose(); noreg = 0; blackcount = 0;
                            setlog("休息6分钟");
                            sleep(6, 0);
                            win32.MouseClick(gH, 100, 100, 0);
                            sleep(0, 5);
                            startupCount = 0;
                            continue;
                        }
                        if (OPImage.check(bm, "403 424 60 25 6094dc49216328af3bc8985e4323ed 强制下线重新载入"))
                        {
                            bm.Dispose(); noreg = 0; blackcount = 0;
                            setlog("强制下线，休息1分钟");
                            win32.MouseClick(gH, 100, 100, 0);
                            sleep(1, 0);
                            sleep(0, 5);
                            startupCount = 0;
                            continue;
                        }
                        if (OPImage.check(bm, "402 418 80 20 c2e83fa7df62967a45f2992ef91efc 重新加载另一台设备连接"))
                        {
                            bm.Dispose(); noreg = 0; blackcount = 0;
                            setlog("另一台设备连接，休息2分钟");
                            sleep(10, 0);
                            win32.MouseClick(gH, 100, 100, 0);
                            sleep(0, 5);
                            startupCount = 0;
                            continue;
                        }

                        if (OPImage.check(bm, "62 608 10 0 114eacf9e50c26567a17c1a38651f73 回营"))
                        {
                            bm.Dispose(); noreg = 0; blackcount = 0;
                            setlog("回营");
                            win32.MouseClick(gH, 62, 600, 0);
                            sleep(0, 5);
                            searchTimeout++;
                            if (searchTimeout > 12)
                            {
                                setlog("回营超时。");
                                scriptKill();
                                searchTimeout = 0;
                            }
                            continue;
                        }

                        if (OPImage.check(bm, "360 562 10 0 fb71dee1307286d413f8ea869f27822 战斗回营"))
                        {
                            win32.MouseClick(gH, 404, 550, 0);
                            bm.Dispose(); noreg = 0; blackcount = 0;
                            setlog("战斗完成后回营");
                            orcme = true;
                            searchcount = 0;
                            sleep(0, 5);
                            continue;
                        }
                        if (OPImage.check(bm, "370 495 10 0 a04bdb3608ab5978951e228e0461e1a 村庄被打确定"))
                        {
                            win32.MouseClick(gH, 402, 495, 0);
                            bm.Dispose(); noreg = 0; blackcount = 0;
                            setlog("村庄被打");
                            sleep(0, 3);
                            continue;
                        }
                        if (OPImage.check(bm, "757 57 30 0 c2abecf67f87c146165e189ef268e7 开始战斗关闭按钮"))
                        {
                            win32.MouseClick(gH, 760, 50, 0);
                            bm.Dispose(); noreg = 0; blackcount = 0;
                            setlog("关闭");
                            sleep(0, 3);
                            continue;
                        }
                        if (OPImage.check(bm, "712 119 30 0 6fc36975c05299bcb17cafaa67356f5f 日志关闭按钮"))
                        {
                            win32.MouseClick(gH, 36, 165, 0);
                            bm.Dispose(); noreg = 0; blackcount = 0;
                            setlog("关闭");
                            sleep(0, 3);
                            continue;
                        }

                        if (OPImage.check(bm, "682 145 20 0 1b231143bcc139a192df17b825625cc 生产兵"))
                        {
                            win32.MouseClick(gH, 40, 540, 1000);//造兵按钮
                            bm.Dispose(); noreg = 0; blackcount = 0;
                            continue;
                        }
                        if (OPImage.check(bm, "218 420 60 20 eb95e4341fa2206fbf1fe61a2c50906 不再提示"))
                        {
                            setlog("不再提示");
                            win32.SetForegroundWindow(gH);
                            win32.MouseClickS(gH, 218, 420, 1000);
                            bm.Dispose(); noreg = 0; blackcount = 0;
                            continue;
                        }

                        if (OPImage.check(bm, "400 364 200 0 e938f3294ee865fa7cbc426581133bf5 黑屏"))
                        {
                            blackcount++;
                            sleep(0, 10);
                            if (blackcount > 6)
                            {
                                blackcount = 0;
                                scriptKill();
                            }
                            bm.Dispose(); noreg = 0;
                            continue;
                        }
                        if (OPImage.check(bm, "51 602 10 0 187ca4bcff9fbc2452f4baa2d4bed45a 回营"))
                        {
                            win32.MouseClick(gH, 51, 602, 0);
                            bm.Dispose(); noreg = 0; blackcount = 0;
                            setlog("回营");
                            sleep(0, 3);
                            continue;
                        }

                        bm.Dispose();
                        startupCount = 0; blackcount = 0;
                        setlog("无法识别。循环0");
                        sleep(0, 5);
                        noreg++;
                        if (noreg > 24)
                        {
                            setlog("无法识别。循环0,超时。");
                            noreg = 0;
                            scriptKill();
                        }
                    }
                    else
                    {
                        startupCount = 0;
                        COC.runbs();
                    }
                    sleep(0, 10);
                }
                isTimerstop = true;

            });
        }
        bool isTimerstop=false;

        private void recogniseME(Bitmap bm)
        {
            //
            orcme = false;
            int v1 = 0;
            string s = ORC.doORC(bm, 710, 54);
            if (!s.Contains("?"))
            {
                v1 = int.Parse(s);
            }
            int v2 = 0;
            s = ORC.doORC(bm, 710, 102);
            if (!s.Contains("?"))
            {
                v2 = int.Parse(s);
            }
            int v3 = 0;
            s = ORC.doORC(bm, 710, 147);
            if (!s.Contains("?"))
            {
                v3 = int.Parse(s);
            }
            int v4 = 0;
            try
            {
                s = ORC.doORC(bm, 90, 102, 44, 16);
                if (!s.Contains("?"))
                {
                    v4 = int.Parse(s);
                }
            }
            catch (Exception)
            {
            }
            ressourcesValues[0] = v1;
            ressourcesValues[1] = v2;
            ressourcesValues[2] = v3;
            ressourcesValues[3] = v4;
            show = true;
            /*
            if (!IsDisposed )
                Invoke((MethodInvoker)delegate ()
                {
                    if (!IsDisposed)
                    {
                    }
                });
            */
        }

        private void scriptKill()
        {
            while (!IsDisposed && isrunscript)
            {
                IntPtr gH = win32.FindWindow(null, "BlueStacks App Player");
                if (gH != IntPtr.Zero)
                {
                    if (killoutCount == 5)
                    {
                        setlog("关闭coc后台超过5次，安全关闭bs。");
                        win32.SendMessage(gH, win32.WM_CLOSE, 0, 0);
                        sleep(0, 10);
                        return;
                    }
                    killoutCount++;
                    for (int i = 0; i < 5; i++)
                        win32.MouseClick(gH, 130, 700, 500);//home按钮
                    win32.SetForegroundWindow(gH);
                    sleep(0, 2);
                    win32.MouseClick(gH, 205, 705, 2000);//list按钮
                    for (int i = 0; i < 3; i++)
                    {
                        win32.SetForegroundWindow(gH);
                        win32.MouseWheel(gH, 700, 370, 500);
                    }
                    return;
                }
            }
        }

        private void scriptDo()
        {
            int noreg = 0;
            while (!IsDisposed && isrunscript)
            {
                IntPtr gH = win32.FindWindow(null, "BlueStacks App Player");
                if (gH != IntPtr.Zero)
                {
                    Bitmap bm = OPImage.GetWindowCapture(gH);

                    if (OPImage.check(bm, "402 418 80 20 c2e83fa7df62967a45f2992ef91efc 重新加载另一台设备连接"))
                    {
                        bm.Dispose();
                        return;
                    }
                    win32.MouseClick(gH, 40, 540, 1000);//造兵按钮
                    bm.Dispose();
                    bm = OPImage.GetWindowCapture(gH);
                    if (OPImage.check(bm, "682 145 20 0 1b231143bcc139a192df17b825625cc 生产兵"))
                    {


                        /**/
                        if (OPImage.check(bm, "147 543 10 0 eafb8c1cf2181a3b48d6e4cad2c4b7b 兵营0"))
                        {
                            if (OPImage.check(bm, "658 314 10 0 dd67ccc21c3eb8d5346343d960805940 可以发送增援"))
                            {

                                bm.Dispose(); noreg = 0;
                                setlog("请求支援。");
                                win32.MouseClick(gH, 612, 325, 2000);
                                win32.MouseClick(gH, 490, 230, 2000);
                                continue;
                            }
                        }
                        if (OPImage.check(bm, "147 543 10 0 eafb8c1cf2181a3b48d6e4cad2c4b7b 兵营0"))
                        {
                            if (OPImage.check(bm, "126 179 10 0 54401a47be69243cb682dd3b7f721d99 军队满"))
                            {
                                bm.Dispose();


                                if (!isnvwang&&searchcount == 0)
                                {
                                    setlog("军队满，搜鱼前造兵。");
                                    win32.MouseClick(gH, 714, 337, 1000);
                                    for (int i = 0; i < 75; i++)
                                        win32.MouseClick(gH, 200, 360, 10);
                                    win32.MouseClick(gH, 714, 337, 1000);
                                    for (int i = 0; i < 75; i++)
                                        win32.MouseClick(gH, 300, 360, 10);
                                    win32.MouseClick(gH, 714, 337, 1000);
                                    for (int i = 0; i < 75; i++)
                                        win32.MouseClick(gH, 500, 360, 10);
                                    win32.MouseClick(gH, 714, 337, 1000);
                                    for (int i = 0; i < 75; i++)
                                        win32.MouseClick(gH, 500, 360, 10);
                                    win32.MouseClick(gH, 147, 547, 10);

                                }
                                setlog("开始搜鱼。");
                                sleep(0, 2);
                                win32.MouseClick(gH, 40, 540, 0);//造兵按钮
                                sleep(0, 2);
                                scriptSearcher();
                                sleep(0, 1);
                                return;
                            }
                        }

                        bm.Dispose(); noreg = 0;
                        setlog("生产兵");
                        win32.MouseClick(gH, 714, 337, 1000);
                        for (int i = 0; i < 75; i++)
                            win32.MouseClick(gH, 200, 360, 10);
                        win32.MouseClick(gH, 714, 337, 1000);
                        for (int i = 0; i < 75; i++)
                            win32.MouseClick(gH, 300, 360, 10);
                        win32.MouseClick(gH, 714, 337, 1000);
                        if(resourceCount>0)
                        {
                            resourceCount =0;
                            for (int i = 0; i < 75; i++)
                                win32.MouseClick(gH, 530, 205, 10);
                            for (int i = 0; i < 75; i++)
                                win32.MouseClick(gH, 400, 360, 10);
                        }
                        else
                        for (int i = 0; i < 75; i++)
                            win32.MouseClick(gH, 500, 360, 10);
                        win32.MouseClick(gH, 714, 337, 1000);
                        for (int i = 0; i < 75; i++)
                            win32.MouseClick(gH, 500, 360, 10);
                        win32.MouseClick(gH, 147, 547, 10);
                        sleep(0, 30);
                        return;
                        // continue;
                    }


                    if (OPImage.check(bm, "403 418 46 0 97672a88e51eaa972b4e9bcb93f6181 请重试"))
                    {
                        bm.Dispose();
                        return;
                    }
                    if (OPImage.check(bm, "403 417 100 30 1fc18f146c8dcea26d8843e4c61e0e9 重新载入游戏"))
                    {
                        bm.Dispose();
                        return;
                    }
                    if (OPImage.check(bm, "402 413 100 30 6c278bda96dfc57f15488e724c5e6a7 休息6分钟重新载入"))
                    {
                        bm.Dispose();
                        return;
                    }
                    if (OPImage.check(bm, "403 424 60 25 6094dc49216328af3bc8985e4323ed 强制下线重新载入"))
                    {
                        bm.Dispose();
                        return;
                    }
                    if (OPImage.check(bm, "696 142 10 0 bee49251fd82af344d742de3f812b87 loading"))
                    {
                        bm.Dispose();
                        return;
                    }

                    if (OPImage.check(bm, "360 562 10 0 fb71dee1307286d413f8ea869f27822 战斗回营"))
                    {
                        bm.Dispose();
                        return;
                    }

                    if (OPImage.check(bm, "80 170 60 0 d9e5ea20e5c12c1d8321a565a3c7cd3e bs启动成功"))
                    {
                        bm.Dispose();
                        return;
                    }

                    if (OPImage.check(bm, "400 364 200 0 e938f3294ee865fa7cbc426581133bf5 黑屏"))
                    {
                        bm.Dispose();
                        return;
                    }

                    if (OPImage.check(bm, "757 57 30 0 c2abecf67f87c146165e189ef268e7 开始战斗关闭按钮"))
                    {
                        bm.Dispose();
                        return;
                    }

                    if (OPImage.check(bm, "769 103 10 0 2b84d9055bc2b9b518275cfe775aaff loadok"))
                    {
                        if (!OPImage.check(bm, "584 33 10 0 9662a9460e0638f888dc97a17c08927 缩放成功标志"))
                        {
                            bm.Dispose();
                            return;
                        }
                        bm.Dispose(); noreg = 0;
                        continue;
                    }

                    if (OPImage.check(bm, "51 602 10 0 187ca4bcff9fbc2452f4baa2d4bed45a 回营"))
                    {
                        bm.Dispose();
                        return;
                    }
                    if (OPImage.check(bm, "712 119 30 0 6fc36975c05299bcb17cafaa67356f5f 日志关闭按钮"))
                    {
                        bm.Dispose();
                        return;
                    }
                    noreg++;
                    bm.Dispose();
                    setlog("无法识别。循环1");
                    sleep(0, 2);
                    if (noreg > 150)
                    {
                        noreg = 0;
                        scriptKill();
                        return;
                    }
                }
                else
                {
                    return;
                }
            }
        }
        int searchTimeout = 0;
        int searchcount = 0;
        string findStr = "";
        int resourceCount = 0;
        private void scriptSearcher()
        {

            //Music.prompt();
            bool start = true;
            // if (start)   return; 
            while (!IsDisposed && isrunscript)
            {
                IntPtr gH = win32.FindWindow(null, "BlueStacks App Player");
                if (gH != IntPtr.Zero)
                {
                    if (start)
                    {
                        win32.MouseClick(gH, 60, 620, 1000);//进攻按钮

                        win32.MouseClick(gH, 220, 520, 1000);//搜索按钮

                        win32.MouseClick(gH, 480, 410, 1000);//确定按钮
                        start = false;
                        searchTimeout = 0;
                    }


                    Bitmap bm = OPImage.GetWindowCapture(gH);

                    //
                    if (OPImage.check(bm, "659 520 10 0 56a54c4ab1fd559a451b9594b94b5b 下一个")
                       && (OPImage.check(bm, "35 99 10 0 28c4de62ae2a974c5746e672ca3b1b8c 搜索能ORC")
                        || OPImage.check(bm, "34 552 10 0 779d61f6bfb8ba4ffe71274c96d2c 结束战斗"))
                        )

                    {
                        //sleep(0, 1);
                        searchcount++;
                        int v1 = 0;
                        string s = ORC.doORC(bm, 80, 98, 70, 20, 240, 190, 240);
                        s = s.Replace('?', '0');
                        v1 = int.Parse(s);
                        int v2 = 0;
                        s = ORC.doORC(bm, 80, 124, 70, 20, 240, 240, 190);
                        s = s.Replace('?', '0');
                        v2 = int.Parse(s);
                        int v3 = 0;
                        if (OPImage.check(bm, "35 154 10 0 f03a3027f1a0bb86f4835e6fa0a86237 资源有黑水"))
                        {
                            s = ORC.doORC(bm, 80, 150, 70, 20, 230, 230, 230);
                            s = s.Replace('?', '0');
                            v3 = int.Parse(s);
                        }
                        resourceCount = 0;
                        resourcePoint = new List<Point>();
                        int ddline = 150000;
                        // int kkline = 280000;
                        int bbss = v2;
                        bool c2day = false;
                        int v4 = v1 + v2;
                        setlog("搜索" + searchcount + "\t金币：" + v1 + ",圣水：" + v2 + "，黑水：" + v3 + ",金+水：" + v4);
                        bool canfight = false;
                        /**/
                        if (OPImage.check(bm, "30 56 10 0 a3d1a240bafb7ea5fe8c47a6ada685bb 超过2天"))
                        {
                            setlog("超过2天");
                            ddline = 120000;
                            //  kkline = 220000;
                            c2day = true;
                            if (v4 > 240000)
                            {
                                canfight = true;
                            }
                        }
                        // sleep(0, 1);
                        if (!canfight && v4 > 300000)
                        {
                            canfight = true;
                        }
                        if (v1 < ddline || v2 < ddline)
                        {
                            canfight = false;
                        }
                        if (canfight)
                        {

                            int num = ImageFind.find1ine(bm, "-1949971 -2081554 -2343956 -1753107 -1620758 5 圣水65000x");
                            if (num == 4)
                                v2 = v2 - num * 80000;
                            else
                                v2 = v2 - num * 65000;
                            /**/
                            if (num == 0)
                            {
                                num = ImageFind.find1ine(bm, "-2013973 -1751828 -1817620 -2474771 -2540304 5 圣水75000x");
                                if (num == 4)
                                    v2 = v2 - num * 90000;
                                else
                                    v2 = v2 - num * 75000;
                                /**/
                                if (num == 0)
                                {
                                    num = ImageFind.find1ine(bm, "-460298 -525837 -530957 -676110 -752909 15 圣水90000x");
                                    if (num == 4)
                                        v2 = v2 - num * 100000;
                                    else
                                        v2 = v2 - num * 90000;
                                }
                            }
                            setlog("可撸的圣水资源:" + v2);
                            /**/
                            if (c2day && v2 == bbss)
                            {
                                canfight = true;
                            }
                            if (v1 < ddline || v2 < ddline)
                            {
                                canfight = false;
                            }
                            if (canfight)
                            {
                                List<Point> pointlist = ImageFind.findssCanxxoo(bm);
                                setlog("有资源的圣水采集器数量:" + pointlist.Count);
                                if (pointlist.Count < 5)//&&v2< kkline
                                {
                                    canfight = false;
                                }/**/
                                else
                                {
                                    pointlist = ImageFind.findzyCanxxoo(bm);
                                    resourcePoint.AddRange(pointlist);
                                    setlog("有资源的采集器数量:" + (resourceCount = resourcePoint.Count));
                                }

                            }

                        }
                        if (canfight)
                        {
                            findStr = "目标死鱼 ";
                        }
                        else
                        {
                            findStr = "目标非死鱼 ";
                        }
                        string ch = ImageFind.findcitadel(bm);
                        setlog(ch);
                        if (ch.StartsWith("没有发现本") || ch.StartsWith("本在里面"))
                        {

                        }
                        else
                        {
                            canfight = true;
                        }
                        findStr += ch;
                        if (!canfight && ressourcesValues[3] > 600)
                        {
                            canfight = true;
                            findStr = "降杯";
                        }
                        List<Point> nw = OPImage.GetImageContains(bm, nvwang, 0);
                        isnvwang = false;
                        if(nw.Count>0)
                        {
                            isnvwang = true;
                            nvwangp = nw[0];
                            setlog("有女王");
                        }
                        if (canfight)
                        {
                            COC.save(bm);
                            bm.Dispose(); bft = DateTime.Now;
                            Music.prompt();
                            setlog(findStr + " 资源数：" + resourcePoint.Count + "，开始战斗。");
                            scriptFight();
                            searchTimeout = 0;
                            //continue;
                            sleep(0, 1);
                            return;
                        }
                        bm.Dispose();
                        //sleep(0, 4);
                        bm = OPImage.GetWindowCapture(gH);
                        if (OPImage.check(bm, "659 520 10 0 56a54c4ab1fd559a451b9594b94b5b 下一个"))
                        {
                            win32.MouseClick(gH, 700, 530, 0);
                        }
                        bm.Dispose();
                        searchTimeout = 0;
                        sleep(0, 5);
                        continue;
                    }

                    if (OPImage.check(bm, "62 608 10 0 114eacf9e50c26567a17c1a38651f73 搜索回营"))// 
                    {
                        bm.Dispose();
                        setlog("搜索中。。。");
                        sleep(0, 5);
                        searchTimeout++;
                        if (searchTimeout > 12)
                        {
                            setlog("搜索超时。");
                            scriptKill();
                            searchTimeout = 0;
                            break;
                        }
                        continue;
                    }

                    if (OPImage.check(bm, "360 562 10 0 fb71dee1307286d413f8ea869f27822 战斗回营"))
                    {
                        bm.Dispose();
                        return;
                    }

                    if (OPImage.check(bm, "403 418 46 0 97672a88e51eaa972b4e9bcb93f6181 请重试"))
                    {
                        bm.Dispose();
                        return;
                    }
                    if (OPImage.check(bm, "403 417 100 30 1fc18f146c8dcea26d8843e4c61e0e9 重新载入游戏"))
                    {
                        bm.Dispose();
                        return;
                    }
                    if (OPImage.check(bm, "402 413 100 30 6c278bda96dfc57f15488e724c5e6a7 休息6分钟重新载入"))
                    {
                        bm.Dispose();
                        return;
                    }
                    if (OPImage.check(bm, "403 424 60 25 6094dc49216328af3bc8985e4323ed 强制下线重新载入"))
                    {
                        bm.Dispose();
                        return;
                    }
                    if (OPImage.check(bm, "696 142 10 0 bee49251fd82af344d742de3f812b87 loading"))
                    {
                        bm.Dispose();
                        return;
                    }
                    if (OPImage.check(bm, "769 103 10 0 2b84d9055bc2b9b518275cfe775aaff loadok"))
                    {
                        bm.Dispose();
                        return;
                    }
                    if (OPImage.check(bm, "80 170 60 0 d9e5ea20e5c12c1d8321a565a3c7cd3e bs启动成功"))
                    {
                        bm.Dispose();
                        return;
                    }

                    if (OPImage.check(bm, "400 364 200 0 e938f3294ee865fa7cbc426581133bf5 黑屏"))
                    {
                        bm.Dispose();
                        return;
                    }
                    if (OPImage.check(bm, "402 418 80 20 c2e83fa7df62967a45f2992ef91efc 重新加载另一台设备连接"))
                    {
                        bm.Dispose();
                        return;
                    }

                    if (OPImage.check(bm, "757 57 30 0 c2abecf67f87c146165e189ef268e7 开始战斗关闭按钮"))
                    {
                        bm.Dispose();
                        return;
                    }

                    bm.Dispose();
                    setlog("无法识别。循环2");
                    sleep(0, 1);
                }
                else
                {
                    return;
                }
            }
        }
        DateTime bft = DateTime.Now;
        Random random = new Random();
        bool isnvwang = false;
        Point nvwangp = new Point();

        private void downpoint(List<Point> clickPoint)
        {

            IntPtr gH = win32.FindWindow(null, "BlueStacks App Player");
            if (gH == IntPtr.Zero)
                return;
            //Bitmap bm = OPImage.GetWindowCapture(gH);


            //if (OPImage.check(bm, "203 632 25 0 2b589e7750b77d66fe6ea022fd94f1 3胖子"))
            {
                win32.MouseClick(gH, 180, 632, 500);
                for (int i = 0; i < 1; i++)
                {
                    Point po = clickPoint[random.Next(3)];
                    win32.MouseClick(gH, po.X, po.Y, 200);
                }
                //win32.MouseClick(gH, 64, 632, 1000);
            }
           // bm.Dispose();

            //win32.MouseClick(gH, 64, 632, 500);
            //bm = OPImage.GetWindowCapture(gH);

            //if (OPImage.check(bm, "64 632 25 0 ecf8733a06053f5ceaf7649b7ae560 1黄毛"))
            {

                win32.MouseClick(gH, 40, 632, 500);

                Point po = clickPoint[random.Next(clickPoint.Count)];
                for (int i = 0; i < 10; i++)
                {
                    win32.MouseClick(gH, po.X, po.Y, 200);
                }
                //win32.MouseClick(gH, 64, 632, 1000);
            }
            //bm.Dispose();
            //bm = OPImage.GetWindowCapture(gH);
            //if (OPImage.check(bm, "139 632 25 0 1bafed665c220fa145e592c9277d519 2mm"))
            {

                win32.MouseClick(gH, 110, 632, 500);

                Point po = clickPoint[random.Next(clickPoint.Count)];
                for (int i = 0; i < 10; i++)
                {
                    win32.MouseClick(gH, po.X, po.Y, 200);
                }
                //win32.MouseClick(gH, 64, 632, 1000);
            }
            //bm.Dispose();
        }

        private void scriptFight()
        {

            string[] strS = findStr.Split(new char[] { ' ' });
            bool iscit = false;
            if (strS.Length > 1)
                if (strS[1].StartsWith("没有发现本") || strS[1].StartsWith("本在里面"))
                {

                }
                else
                {
                    iscit = true;
                }
            if (findStr == "降杯") iscit = false;


            Rectangle r1 = new Rectangle(ImageFind.v1, new Size(345, 270));
            Rectangle r2 = new Rectangle(ImageFind.v2, new Size(345, 270));
            Rectangle r3 = new Rectangle(ImageFind.v4, new Size(345, 270));
            Rectangle r4 = new Rectangle(ImageFind.v9, new Size(345, 270));
            bool isw1 = false;
            int circleC = 0;
            int citdownC = 0;
            int vNoChangeC = 0;
            int[] nc = new int[] { 0, 0, 0, 0 };
            int[] nct = new int[] { 0, 0, 0, 0 };
            int[] vs = new int[] { 0, 0, 0 };
            IntPtr gH = win32.FindWindow(null, "BlueStacks App Player");
            /*
            if (gH != IntPtr.Zero)
            {
                Bitmap bm = OPImage.GetWindowCapture(gH);

                if (OPImage.check(bm, "337 632 25 0 271b95d2f75117ca5a345521c796fece 5蛮王"))
                {
                    isw1 = true;
                }
                if (OPImage.check(bm, "337 632 25 0 6ba2810edfcfaff1efa443df69ec56 5女王"))
                {
                    isw1 = true;
                }
                bm.Dispose();
                if (isw1)
                {
                    setlog("有王1。");
                }
            }*/
            foreach (Point item in resourcePoint)
            {
                if (r1.Contains(item))
                {
                    nc[0]++; nct[0]++;
                }
                if (r2.Contains(item))
                {
                    nc[1]++; nct[1]++;
                }
                if (r3.Contains(item))
                {
                    nc[2]++; nct[2]++;
                }
                if (r4.Contains(item))
                {
                    nc[3]++; nct[3]++;
                }
            }
            bool jsok = true;
            List<Point> downps = new List<Point>();
            try
            {
                downps = ImageFind.getdownpiont(resourcePoint);
            }
            catch (Exception)
            {
                jsok = false;
                setlog("垂直计算错误。");
            }
            if (iscit)
            {
                Point item = new Point(int.Parse(strS[3]), int.Parse(strS[4]));
                dodit(gH, item);
            }
            if (jsok)
            {
                foreach (Point item in downps)
                {
                    win32.MouseClick(gH, 250, 632, 500);
                    for (int i = 0; i < 1; i++)
                        win32.MouseClick(gH, item.X, item.Y, 200);
                    win32.MouseClick(gH, 40, 632, 500);
                    for (int i = 0; i < 6; i++)
                        win32.MouseClick(gH, item.X, item.Y, 200);
                    win32.MouseClick(gH, 180, 632, 500);
                    for (int i = 0; i < 6; i++)
                        win32.MouseClick(gH, item.X, item.Y, 200);
                    win32.MouseClick(gH, 110, 632, 500);
                    for (int i = 0; i < 6; i++)
                        win32.MouseClick(gH, item.X, item.Y, 200);
                }

            }
            else
            {
                /**/
                foreach (Point item in resourcePoint)
                {
                    if (r1.Contains(item))
                    {
                        win32.MouseClick(gH, 180, 632, 500);
                        for (int i = 0; i < 1; i++)
                            win32.MouseClick(gH, 239, 173, 200);
                        win32.MouseClick(gH, 40, 632, 500);
                        for (int i = 0; i < 6; i++)
                            win32.MouseClick(gH, 253, 166, 200);
                        win32.MouseClick(gH, 110, 632, 500);
                        for (int i = 0; i < 6; i++)
                            win32.MouseClick(gH, 208, 206, 200);
                        nc[0]++; nct[0]++;
                    }
                    if (r2.Contains(item))
                    {
                        win32.MouseClick(gH, 180, 632, 500);
                        for (int i = 0; i < 1; i++)
                            win32.MouseClick(gH, 575, 187, 200);
                        win32.MouseClick(gH, 40, 632, 500);
                        for (int i = 0; i < 6; i++)
                            win32.MouseClick(gH, 555, 172, 200);
                        win32.MouseClick(gH, 110, 632, 500);
                        for (int i = 0; i < 6; i++)
                            win32.MouseClick(gH, 613, 211, 200);
                        nc[1]++; nct[1]++;
                    }

                    if (r3.Contains(item))
                    {
                        win32.MouseClick(gH, 180, 632, 500);
                        for (int i = 0; i < 1; i++)
                            win32.MouseClick(gH, 236, 472, 200);
                        win32.MouseClick(gH, 40, 632, 500);
                        for (int i = 0; i < 6; i++)
                            win32.MouseClick(gH, 221, 460, 200);
                        win32.MouseClick(gH, 110, 632, 500);
                        for (int i = 0; i < 6; i++)
                            win32.MouseClick(gH, 242, 479, 200);
                        nc[2]++; nct[2]++;
                    }
                    if (r4.Contains(item))
                    {
                        win32.MouseClick(gH, 180, 632, 500);
                        for (int i = 0; i < 1; i++)
                            win32.MouseClick(gH, 603, 454, 200);
                        win32.MouseClick(gH, 40, 632, 500);
                        for (int i = 0; i < 6; i++)
                            win32.MouseClick(gH, 613, 450, 200);
                        win32.MouseClick(gH, 110, 632, 500);
                        for (int i = 0; i < 6; i++)
                            win32.MouseClick(gH, 604, 464, 200);
                        nc[3]++; nct[3]++;
                    }
                }

            }
            if (isw1)
            {
                setlog("下王1。");
                Array.Sort(nct);
                for (int i = 0; i < nc.Length; i++)
                    if (nc[i] == nct[3])
                    {
                        if (i == 0)
                        {
                            Point po = clickPoint1[0];
                            win32.MouseClick(gH, 337, 632, 1000);
                            win32.MouseClick(gH, po.X, po.Y, 1000);
                            win32.MouseClick(gH, 64, 632, 1000);
                            break;
                        }
                        if (i == 1)
                        {
                            Point po = clickPoint2[0];
                            win32.MouseClick(gH, 337, 632, 1000);
                            win32.MouseClick(gH, po.X, po.Y, 1000);
                            win32.MouseClick(gH, 64, 632, 1000);
                            break;
                        }
                        if (i == 2)
                        {
                            Point po = clickPoint3[0];
                            win32.MouseClick(gH, 337, 632, 1000);
                            win32.MouseClick(gH, po.X, po.Y, 1000);
                            win32.MouseClick(gH, 64, 632, 1000);
                            break;
                        }
                        if (i == 3)
                        {
                            Point po = clickPoint4[0];
                            win32.MouseClick(gH, 337, 632, 1000);
                            win32.MouseClick(gH, po.X, po.Y, 1000);
                            win32.MouseClick(gH, 64, 632, 1000);
                            break;
                        }
                    }

            }

            while (!IsDisposed && isrunscript)
            {
                gH = win32.FindWindow(null, "BlueStacks App Player");
                if (gH != IntPtr.Zero)
                {

                    if (findStr == "降杯")
                    {
                        dodownppp(gH);
                        sleep(0, 1);
                        // findStr = "降杯";
                        //continue;
                    }
                    Bitmap bm = OPImage.GetWindowCapture(gH);




                    if (OPImage.check(bm, "360 562 10 0 fb71dee1307286d413f8ea869f27822 战斗回营"))
                    {
                        bm.Dispose();
                        setlog("完成战斗。");
                        sleep(0, 1);
                        return;
                    }

                    if (OPImage.check(bm, "659 520 10 0 56a54c4ab1fd559a451b9594b94b5b 下一个"))
                    {
                        bm.Dispose();
                        //sleep(0, 2); 
                        break;
                    }

                    if (iscit && resourcePoint.Count == 0
                      && OPImage.check(bm, "667 555 5 0 404d32d1d5247f1ab4c5d04fab40cd3f 战斗有心"))
                    {
                        bm.Dispose();
                        setlog("偷本完成。");
                        win32.MouseClick(gH, 64, 545, 1000);
                        win32.MouseClick(gH, 514, 397, 200);
                        sleep(0, 1);
                        continue;
                    }
                    if (iscit && OPImage.check(bm, "514 397 10 0 f3d6d24e3d72d755794927d9dedfcc9 放弃战斗"))
                    {
                        bm.Dispose();
                        win32.MouseClick(gH, 514, 397, 1000);
                        sleep(0, 1);
                        continue;
                    }

                    if (OPImage.check(bm, "34 552 10 0 779d61f6bfb8ba4ffe71274c96d2c 结束战斗"))
                    {
                        if (!OPImage.check(bm, "659 520 10 0 56a54c4ab1fd559a451b9594b94b5b 下一个"))
                        {

                            int v1 = 0;
                            string s = ORC.doORC(bm, 80, 98, 70, 20, 240, 190, 240);
                            s = s.Replace('?', '0');
                            v1 = int.Parse(s);
                            int v2 = 0;
                            s = ORC.doORC(bm, 80, 124, 70, 20, 240, 240, 190);
                            s = s.Replace('?', '0');
                            v2 = int.Parse(s);
                            int v3 = 0;
                            if (OPImage.check(bm, "35 154 10 0 f03a3027f1a0bb86f4835e6fa0a86237 资源有黑水"))
                            {
                                s = ORC.doORC(bm, 80, 150, 70, 20, 230, 230, 230);
                                s = s.Replace('?', '0');
                                v3 = int.Parse(s);
                            }
                            if (v1 == vs[0] && v2 == vs[1] && v3 == vs[2])
                            {
                                vNoChangeC = -1;
                            }
                            vNoChangeC++;
                            if (vNoChangeC > 60)
                            {

                                bm.Dispose();
                                setlog("资源无增加。");
                                win32.MouseClick(gH, 64, 545, 1000);
                                win32.MouseClick(gH, 514, 397, 200);
                                sleep(0, 1);
                                if (circleC > 210)
                                {
                                    break;
                                }


                                continue;
                            }
                            vs[0] = v1; vs[1] = v2; vs[2] = v3;
                            if (findStr == "降杯")
                            {
                                bm.Dispose();
                                dodownppp(gH);
                                continue;
                            }

                            if (circleC == 10)
                            {
                                if (isw1)
                                {
                                    setlog("王1技能释放。");
                                    for (int i = 0; i < 2; i++)
                                        win32.MouseClick(gH, 337, 632, 200);
                                    win32.MouseClick(gH, 64, 632, 0);
                                }
                            }

                            setlog("战斗中。。。");
                            /*
                            if (circleC % 40 == 0)
                                Task.Factory.StartNew(() =>
                                {
                                    try
                                    {
                                        for (int k = 0; k < resourcePoint.Count && k < 18; k++)
                                        {
                                            Point item = resourcePoint[k];
                                            if (r1.Contains(item))
                                            {
                                                downpoint(clickPoint1);
                                            }
                                            if (r2.Contains(item))
                                            {
                                                downpoint(clickPoint2);
                                            }
                                            if (r3.Contains(item))
                                            {
                                                downpoint(clickPoint3);
                                            }
                                            if (r4.Contains(item))
                                            {
                                                downpoint(clickPoint4);
                                            }
                                        }

                                    }
                                    catch (Exception)
                                    {
                                    }
                                });*/


                            if (iscit && citdownC == 0 && (circleC > 60 || (circleC > 30 && vNoChangeC > 30)))
                            {
                                citdownC++;
                                Point item = new Point(int.Parse(strS[3]), int.Parse(strS[4]));
                                dodit(gH, item, citdownC);
                            }

                            circleC++;

                            if (circleC % 60 == 0)
                            {
                                try
                                {
                                    List<Point> rr = new List<Point>();
                                    rr.AddRange(ImageFind.findzyCanxxoo(bm));
                                    resourcePoint = rr;

                                }
                                catch (Exception)
                                {
                                    setlog("资源点计算错误");
                                }
                            }


                        }

                        bm.Dispose();
                        sleep(0, 1);
                        if (circleC > 210)
                        {
                            break;
                        }


                        continue;
                    }





                    if (OPImage.check(bm, "403 418 46 0 97672a88e51eaa972b4e9bcb93f6181 请重试"))
                    {
                        bm.Dispose();
                        return;
                    }
                    if (OPImage.check(bm, "403 417 100 30 1fc18f146c8dcea26d8843e4c61e0e9 重新载入游戏"))
                    {
                        bm.Dispose();
                        return;
                    }
                    if (OPImage.check(bm, "402 413 100 30 6c278bda96dfc57f15488e724c5e6a7 休息6分钟重新载入"))
                    {
                        bm.Dispose();
                        return;
                    }
                    if (OPImage.check(bm, "403 424 60 25 6094dc49216328af3bc8985e4323ed 强制下线重新载入"))
                    {
                        bm.Dispose();
                        return;
                    }
                    if (OPImage.check(bm, "696 142 10 0 bee49251fd82af344d742de3f812b87 loading"))
                    {
                        bm.Dispose();
                        return;
                    }
                    if (OPImage.check(bm, "769 103 10 0 2b84d9055bc2b9b518275cfe775aaff loadok"))
                    {
                        bm.Dispose();
                        return;
                    }
                    if (OPImage.check(bm, "62 608 10 0 114eacf9e50c26567a17c1a38651f73 搜索回营"))
                    {
                        bm.Dispose();
                        return;
                    }
                    if (OPImage.check(bm, "80 170 60 0 d9e5ea20e5c12c1d8321a565a3c7cd3e bs启动成功"))
                    {
                        bm.Dispose();
                        return;
                    }

                    if (OPImage.check(bm, "400 364 200 0 e938f3294ee865fa7cbc426581133bf5 黑屏"))
                    {
                        bm.Dispose();
                        return;
                    }
                    if (OPImage.check(bm, "402 418 80 20 c2e83fa7df62967a45f2992ef91efc 重新加载另一台设备连接"))
                    {
                        bm.Dispose();
                        return;
                    }

                    bm.Dispose();
                    setlog("无法识别。循环3");
                    sleep(0, 2);
                }
                else
                {
                    return;
                }
            }
        }

        private   void dodit(IntPtr gH, Point item, int cd = 0)
        {
            if (item.Y > 530)
            {
                win32.MouseWheelx(gH, 400, 350, 500);
                item.X = 340;
                item.Y = 555;
            }
            if (item.Y <150)
            {
                win32.MouseWheel(gH, 400, 350, 500);
                item.X = 400;
                item.Y = 60;
            }
            if (cd > 0)
            {
                win32.MouseClick(gH, 250, 632, 500);
                for (int i = 0; i < 1; i++)
                    win32.MouseClick(gH, item.X, item.Y, 200);
            }

            if (isnvwang&&cd==0)
            {
                win32.MouseClick(gH, nvwangp.X, nvwangp.Y, 500);
                win32.MouseClick(gH, item.X, item.Y, 500);
                win32.MouseClick(gH, nvwangp.X, nvwangp.Y, 500);
            }
            else
            {
                win32.MouseClick(gH, 40, 632, 500);
                for (int i = 0; i < 4; i++)
                    win32.MouseClick(gH, item.X, item.Y, 200);
                win32.MouseClick(gH, 180, 632, 500);
                for (int i = 0; i < 6; i++)
                    win32.MouseClick(gH, item.X, item.Y, 200);
                win32.MouseClick(gH, 110, 632, 500);
                for (int i = 0; i < 4; i++)
                    win32.MouseClick(gH, item.X, item.Y, 200);
                sleep(0, 40);
            }
        }

        private void dodownppp(IntPtr gH)
        {
            if (isnvwang)
            {
                win32.MouseClick(gH, nvwangp.X, nvwangp.Y, 500);
                win32.MouseClick(gH, 60, 335, 500);
                win32.MouseClick(gH, nvwangp.X, nvwangp.Y, 500);
            }
            else
            win32.MouseClick(gH, 297, 524, 2000);
            if (resourcePoint.Count != 0)
                return;
            win32.MouseClick(gH, 64, 545, 1000);
            win32.MouseClick(gH, 514, 397, 200);
        }

        string newLine = "\r\n";

        StringBuilder buffer = new StringBuilder();
        private void setlog(string s)
        {
            buffer.Insert(0, DateTime.Now.ToString() + "\t" + s + newLine);
            if (!IsDisposed && tabIndex == 3)
                try
                {
                    Invoke((MethodInvoker)delegate ()
                    {
                        if (!richTextBox1.IsDisposed)
                            richTextBox1.Text = buffer.ToString();
                    });
                }
                catch (Exception)
                {

                }
        }
        private void sleep(int m, int s)
        {
            int t = m * 60 + s;
            while (!IsDisposed && t > 0 && isrunscript)
            {
                Thread.Sleep(1000);
                //if (!puase)
                {
                    t--;
                }
            }
        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!COC.IsRegeditItemExist())
            {
                MessageBox.Show("BlueStacks不存在,程序无法运行》》》");
                Application.Exit();
            }
            else
            {
                setlog("2分钟后自动开始");
                Task.Factory.StartNew(new Action(() => {
                    int t = 1200;
                    while (!IsDisposed && t > 0)
                    {
                        Thread.Sleep(100); 
                        {
                            t--;
                        }
                    }
                    if (!IsDisposed)
                        Invoke((MethodInvoker)delegate ()
                        {
                            if (button3.Text=="运行")
                                button3_Click(sender, e);
                        });
                }));
            }
        }
        Bitmap showtj = null;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (isTimerstop)
            {
                setlog("停止脚本");
                isTimerstop = false;
                button3.Text = "运行";
                timer1.Enabled = false;
                return;
            }
            MemeryInfo.MEMORY_INFO meminfo = new MemeryInfo.MEMORY_INFO();
            MemeryInfo.GlobalMemoryStatus(ref meminfo);
            decimal m = decimal.Parse(meminfo.dwMemoryLoad.ToString());
            label11.Text = "内存占用百分比：" + m + "%";
            if (m > 80)
            {
                Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
                win32.SendMessage(Handle, win32.WM_CLOSE, 0, 0);
                return;
            }
            TimeSpan ts = DateTime.Now - dtbegin;
            label6.Text = "脚本已运行：" + ts.Days + " 天，" + ts.Hours + " 小时，" + ts.Minutes + " 分，" + ts.Seconds + " 秒。";
            if (show)
            {
                label2.Text = "金币：" + ressourcesValues[0];
                label3.Text = "圣水：" + ressourcesValues[1];
                label4.Text = "黑水：" + ressourcesValues[2];
                label5.Text = "杯子：" + ressourcesValues[3];
                show = false;
                if (firstshow)
                {
                    firstshow = false;
                    firstressources[0] = ressourcesValues[0];
                    firstressources[1] = ressourcesValues[1];
                    firstressources[2] = ressourcesValues[2];
                    firstressources[3] = ressourcesValues[3];
                    label10.Text = "金币：" + ressourcesValues[0];
                    label9.Text = "圣水：" + ressourcesValues[1];
                    label8.Text = "黑水：" + ressourcesValues[2];
                    label7.Text = "杯子：" + ressourcesValues[3];
                }
                else
                {
                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;
                    sf.FormatFlags = StringFormatFlags.NoWrap;
                    Bitmap bm = new Bitmap(400, 400);
                    Graphics g = Graphics.FromImage(bm);
                    g.Clear(Color.White);

                    GraphicsPath gph = new GraphicsPath();
                    gph.AddString("每小时撸的资源统计", new FontFamily("微软雅黑"), 0, 20, new Rectangle(0, 0, 400, 100), sf);
                    g.FillPath(new SolidBrush(Color.Black), gph);
                    gph.Dispose();
                    Rectangle r = new Rectangle(0, 100, 400, 100);
                    LinearGradientBrush brush = new LinearGradientBrush(r, Color.Gold, Color.Yellow, LinearGradientMode.Horizontal);
                    g.FillRectangle(brush, r);
                    brush.Dispose();
                    gph = new GraphicsPath();
                    gph.AddString(((int)((ressourcesValues[0] - firstressources[0]) / (ts.TotalSeconds / 3600))).ToString("N0"),
                        new FontFamily("微软雅黑"), 0, 20, r, sf);
                    g.FillPath(new SolidBrush(Color.Black), gph);
                    gph.Dispose();
                    r = new Rectangle(0, 200, 400, 100);
                    brush = new LinearGradientBrush(r, Color.Magenta, Color.DeepPink, LinearGradientMode.Horizontal);
                    g.FillRectangle(brush, r);
                    brush.Dispose();
                    gph = new GraphicsPath();
                    gph.AddString(((int)((ressourcesValues[1] - firstressources[1]) / (ts.TotalSeconds / 3600))).ToString("N0"),
                        new FontFamily("微软雅黑"), 0, 20, r, sf);
                    g.FillPath(new SolidBrush(Color.Black), gph);
                    gph.Dispose();
                    r = new Rectangle(0, 300, 400, 100);
                    brush = new LinearGradientBrush(r, Color.Black, Color.DimGray, LinearGradientMode.Horizontal);
                    g.FillRectangle(brush, r);
                    brush.Dispose();
                    gph = new GraphicsPath();
                    gph.AddString(((int)((ressourcesValues[2] - firstressources[2]) / (ts.TotalSeconds / 3600))).ToString("N0"),
                        new FontFamily("微软雅黑"), 0, 20, r, sf);
                    g.FillPath(new SolidBrush(Color.WhiteSmoke), gph);
                    gph.Dispose();

                    g.Dispose();
                    pictureBox1.Image = bm;
                    if (showtj != null)
                    {
                        showtj.Dispose();
                    }
                    showtj = bm;
                }
            }
        }
    }
}
