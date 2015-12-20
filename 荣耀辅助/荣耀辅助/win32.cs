using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 荣耀辅助
{
    class win32
    {

        public const int BM_CLICK = 0xF5;
        public const int WM_CLOSE = 0x0010;
        public const int WM_LBUTTONDOWN = 0x0201; //按下鼠标左键 
        public const int WM_LBUTTONUP = 0x0202; //释放鼠标左键 
        public const int WM_MOUSEWHEEL = 0x020A; //当鼠标轮子转动时发送此消息个当前有焦点的控件 
        public const int WM_KEYDOWN = 0x0100; //按下一个键 
        public const int WM_KEYUP = 0x0101; //释放一个键 
        public const int WHEEL_DELTA = 120;
        public const int MOUSEEVENTF_LEFTDOWN = 0x2;
        public const int MOUSEEVENTF_LEFTUP = 0x4;
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool PostMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);
        [DllImport("user32.dll", EntryPoint = "FindWindowEx")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("User32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int SetCursorPos(int x, int y);

        /// <summary>
        /// 最大化窗口 3，最小化窗口 2，正常大小窗口 1；
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "ShowWindow", CharSet = CharSet.Auto)]
        public static extern int ShowWindow(IntPtr hwnd, int nCmdShow);


        public static void MouseWheel(IntPtr handle, int x, int y, int t)
        {

            PostMessage(handle, WM_MOUSEWHEEL, 0 + (WHEEL_DELTA << 16), x + (y << 16));
            if (t != 0) Thread.Sleep(t);
        }
        public static void MouseWheelx(IntPtr handle, int x, int y, int t)
        {

            PostMessage(handle, WM_MOUSEWHEEL, 0 + ((-WHEEL_DELTA) << 16), x + (y << 16));
            if (t != 0) Thread.Sleep(t);
        }

        public static void MouseClick(IntPtr handle, int x, int y, int t)
        {
            PostMessage(handle, WM_LBUTTONDOWN, 0, x + (y << 16));
            PostMessage(handle, WM_LBUTTONUP, 0, x + (y << 16));
            if (t != 0) Thread.Sleep(t);
        }
        public static void MouseClickS(IntPtr handle, int x, int y, int t)
        {

            OPImage.User32.RECT windowRect = new OPImage.User32.RECT();
            OPImage.User32.GetWindowRect(handle, ref windowRect);
            SetCursorPos(x+windowRect.left, y+windowRect.top);
            Thread.Sleep(200);

            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
            Thread.Sleep(200);
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
            if (t != 0) Thread.Sleep(t);
        }
        public static void keyClick(IntPtr handle,  int key, int t)
        {
             PostMessage( handle,  WM_KEYDOWN, key, 0);
            PostMessage( handle,  WM_KEYUP, key, 0);
            if (t != 0) Thread.Sleep(t);
        }
    }
    public class MemeryInfo
    {
        //使用GlobalMemoryStatus API取物理内存大小及状态 
        [DllImport("kernel32")]
        public static extern void GlobalMemoryStatus(ref MEMORY_INFO meminfo);

        //定义内存的信息结构 
        [StructLayout(LayoutKind.Sequential)]
        public struct MEMORY_INFO
        {
            public uint dwLength;
            public uint dwMemoryLoad;//正在使用 
            public UInt64 dwTotalPhys;//物理内存大小 
            public UInt64 dwAvailPhys;//可使用的物理内存 
            public UInt64 dwTotalPageFile;//交换文件总大小 
            public UInt64 dwAvailPageFile;
            public UInt64 dwTotalVirtual;//总虚拟内存 
            public UInt64 dwAvailVirtual;
        }
    }
}
