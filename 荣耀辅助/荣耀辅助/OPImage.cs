using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace 荣耀辅助
{
    class OPImage
    {
         
        public static Bitmap GetWindowCapture(IntPtr hWnd)
        {
            IntPtr hscrdc = User32.GetWindowDC(hWnd);
            User32.RECT windowRect = new User32.RECT();
            User32.GetWindowRect(hWnd, ref windowRect);
            int width = windowRect.right - windowRect.left;
            int height = windowRect.bottom - windowRect.top;
            IntPtr hbitmap = GDI32.CreateCompatibleBitmap(hscrdc, width, height);
            IntPtr hmemdc = GDI32.CreateCompatibleDC(hscrdc);
            GDI32.SelectObject(hmemdc, hbitmap);
            User32.PrintWindow(hWnd, hmemdc, 0);
            Bitmap bmp = Bitmap.FromHbitmap(hbitmap);
            GDI32.DeleteDC(hscrdc);//删除用过的对象
            GDI32.DeleteDC(hmemdc);//删除用过的对象
            return bmp;
        }

        public static string toMD5(Bitmap bm)
        {
            MemoryStream ms = new MemoryStream();
            bm.Save(ms, ImageFormat.Jpeg);
            string s = "";
            byte[] targetData = md5.ComputeHash(ms.GetBuffer());

            for (int i = 0; i < targetData.Length; i++)
            {
                s += targetData[i].ToString("x");
            } 
            ms.Close();
            ms.Dispose();
            bm.Dispose();
            return s;
        }
        static  MD5 md5 = new MD5CryptoServiceProvider();
        /// <summary>
        /// 辅助类 定义 Gdi32 API 函数
        /// </summary>
        public class GDI32
        {
            public const int SRCCOPY = 0x00CC0020;
            [DllImport("gdi32.dll")]
            public static extern bool BitBlt(IntPtr hObject, int nXDest, int nYDest,
                int nWidth, int nHeight, IntPtr hObjectSource,
                int nXSrc, int nYSrc, int dwRop);
            [DllImport("gdi32.dll")]
            public static extern IntPtr CreateCompatibleBitmap(IntPtr hDC, int nWidth,
                int nHeight);
            [DllImport("gdi32.dll")]
            public static extern IntPtr CreateCompatibleDC(IntPtr hDC);
            [DllImport("gdi32.dll")]
            public static extern bool DeleteDC(IntPtr hDC);
            [DllImport("gdi32.dll")]
            public static extern bool DeleteObject(IntPtr hObject);
            [DllImport("gdi32.dll")]
            public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);
        }
        /// <summary>
        /// 辅助类 定义User32 API函数
        /// </summary>
        public class User32
        {
            [StructLayout(LayoutKind.Sequential)]
            public struct RECT
            {
                public int left;
                public int top;
                public int right;
                public int bottom;
            }
            [DllImport("user32.dll")]
            public static extern IntPtr GetDesktopWindow();
            [DllImport("user32.dll")]
            public static extern IntPtr GetWindowDC(IntPtr hWnd);
            [DllImport("user32.dll")]
            public static extern IntPtr GetDC(IntPtr hWnd);
            [DllImport("user32.dll")]
            public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDC);
            [DllImport("user32.dll")]
            public static extern IntPtr GetWindowRect(IntPtr hWnd, ref RECT rect);
            [DllImport("user32.dll")]
            public static extern bool PrintWindow(
            IntPtr hwnd,                // Window to copy,Handle to the window that will be copied.
            IntPtr hdcBlt,              // HDC to print into,Handle to the device context.
            UInt32 nFlags               // Optional flags,Specifies the drawing options. It can be one of the following values.
            );
        }

        

        public static bool check(Bitmap bm, string v)
        {
            Bitmap srcImg = bm;
            string[] ss = v.Split(new char[] { ' ' });
            int w = int.Parse(ss[2]);
            int h = int.Parse(ss[3]);
            if (h == 0)
                h = w;
            int x = int.Parse(ss[0]);
            int y = int.Parse(ss[1]);
            Bitmap tempImg = srcImg.Clone(new Rectangle(x - w / 2, y - h / 2, w, h), srcImg.PixelFormat);
            return toMD5(tempImg)==ss[4];
        }

        public static bool ScanColor(Color p_CurrentlyColor, Color p_CompareColor, int p_Float)
        {
            int _R = p_CurrentlyColor.R;
            int _G = p_CurrentlyColor.G;
            int _B = p_CurrentlyColor.B;

            return (_R <= p_CompareColor.R + p_Float && _R >= p_CompareColor.R - p_Float) && (_G <= p_CompareColor.G + p_Float && _G >= p_CompareColor.G - p_Float) && (_B <= p_CompareColor.B + p_Float && _B >= p_CompareColor.B - p_Float);

        }


        public static Bitmap stringToBitmap(string s)
        {
            MemoryStream ms = new MemoryStream(Convert.FromBase64String(s));
            BinaryReader br = new BinaryReader(ms);
            Bitmap bm = new Bitmap(br.ReadInt32(), br.ReadInt32());
            for (int x = 0; x < bm.Width; x++)
            {
                for (int y = 0; y < bm.Height; y++)
                {
                    bm.SetPixel(x, y, Color.FromArgb(br.ReadInt32()));
                }
            }
            br.Close();
            ms.Close();
            return bm;
        }
        public static List<Point> GetImageContains(Bitmap p_SourceBitmap, Bitmap p_PartBitmap, int p_Float)
        {
            List<Rectangle> rectLst = new List<Rectangle>();
            int _SourceWidth = p_SourceBitmap.Width;
            int _SourceHeight = p_SourceBitmap.Height;

            int _PartWidth = p_PartBitmap.Width;
            int _PartHeight = p_PartBitmap.Height;
            Bitmap _SourceBitmap = p_SourceBitmap.Clone() as Bitmap;
            //  Bitmap _SourceBitmap = new Bitmap(_SourceWidth, _SourceHeight);
            /*Graphics _Graphics = Graphics.FromImage(_SourceBitmap);
            _Graphics.DrawImage(p_SourceBitmap, new Rectangle(0, 0, _SourceWidth, _SourceHeight));
            _Graphics.Dispose();*/
            BitmapData _SourceData = _SourceBitmap.LockBits(new Rectangle(0, 0, _SourceWidth, _SourceHeight), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            byte[] _SourceByte = new byte[_SourceData.Stride * _SourceHeight];
            Marshal.Copy(_SourceData.Scan0, _SourceByte, 0, _SourceByte.Length);  //复制出p_SourceBitmap的相素信息 

            Bitmap _PartBitmap = p_PartBitmap.Clone() as Bitmap;
            /*Bitmap _PartBitmap = new Bitmap(_PartWidth, _PartHeight);
            _Graphics = Graphics.FromImage(_PartBitmap);
            _Graphics.DrawImage(p_PartBitmap, new Rectangle(0, 0, _PartWidth, _PartHeight));
            _Graphics.Dispose();*/
            BitmapData _PartData = _PartBitmap.LockBits(new Rectangle(0, 0, _PartWidth, _PartHeight), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            byte[] _PartByte = new byte[_PartData.Stride * _PartHeight];
            Marshal.Copy(_PartData.Scan0, _PartByte, 0, _PartByte.Length);   //复制出p_PartBitmap的相素信息 
            List<Point> findPoints = new List<Point>();
            // List<Rectangle> findPoints = new List<Point>();


            for (int i = 0; i != _SourceHeight; i++)
            {
                if (_SourceHeight - i < _PartHeight) break;  //如果 剩余的高 比需要比较的高 还要小 就直接返回             
                int _PointX = -1;    //临时存放坐标 需要包正找到的是在一个X点上
                bool _SacnOver = true;   //是否都比配的上
                for (int z = 0; z != _PartHeight - 1; z++)       //循环目标进行比较
                {
                    bool iscontaint = false;

                    foreach (var item in findPoints)
                    {
                        Rectangle r = new Rectangle(item, new Size(_PartWidth, _PartHeight));
                        if (r.Contains(i, z))
                        {
                            iscontaint = true; break;
                        }
                    }
                    if (iscontaint)
                    {
                        continue;
                    }

                    int _TrueX = GetImageContains(_SourceByte, _PartByte, i * _SourceData.Stride, _SourceWidth, _PartWidth, p_Float);

                    if (_TrueX == -1)   //如果没找到 
                    {
                        _PointX = -1;    //设置坐标为没找到
                        _SacnOver = false;   //设置不进行返回
                        break;
                    }
                    else
                    {
                        if (z == 0) _PointX = _TrueX;
                        if (_PointX != _TrueX)   //如果找到了 也的保证坐标和上一行的坐标一样 否则也返回
                        {
                            _PointX = -1;//设置坐标为没找到
                            _SacnOver = false;  //设置不进行返回
                            break;
                        }
                    }
                }
                if (_SacnOver)
                {
                    bool b = false;
                    foreach (Rectangle item in rectLst)
                    {
                        if (item.Contains(_PointX, i))
                        {
                            b = true;
                            break;
                        }
                    }
                    if (b)
                    {
                        continue;
                    }
                    Rectangle r = new Rectangle(_PointX, i, _PartWidth, _PartHeight);
                    rectLst.Add(r);
                    findPoints.Add(new Point(_PointX, i));
                }
            }
            _PartBitmap.UnlockBits(_PartData);
            _SourceBitmap.UnlockBits(_SourceData);
            _SourceBitmap.Dispose();
            _PartBitmap.Dispose();
            return findPoints;
        }
        /// <summary>
        /// 判断图形里是否存在另外一个图形 所在行的索引
        /// </summary>
        /// <param name="p_Source">原始图形数据</param>
        /// <param name="p_Part">小图形数据</param>
        /// <param name="p_SourceIndex">开始位置</param>
        /// <param name="p_SourceWidth">原始图形宽</param>
        /// <param name="p_PartWidth">小图宽</param>
        /// <param name="p_Float">溶差</param>
        /// <returns>所在行的索引 如果找不到返回-1</returns>
        private static int GetImageContains(byte[] p_Source, byte[] p_Part, int p_SourceIndex, int p_SourceWidth, int p_PartWidth, int p_Float)
        {
            int _PartIndex = 0;
            int _SourceIndex = p_SourceIndex;
            for (int i = 0; i < p_SourceWidth; i++)
            {
                if (p_SourceWidth - i < p_PartWidth) return -1;
                Color _CurrentlyColor = Color.FromArgb((int)p_Source[_SourceIndex + 3], (int)p_Source[_SourceIndex + 2], (int)p_Source[_SourceIndex + 1], (int)p_Source[_SourceIndex]);
                Color _CompareColoe = Color.FromArgb((int)p_Part[3], (int)p_Part[2], (int)p_Part[1], (int)p_Part[0]);
                _SourceIndex += 4;

                bool _ScanColor = ScanColor(_CurrentlyColor, _CompareColoe, p_Float);

                if (_ScanColor)
                {
                    _PartIndex += 4;
                    int _SourceRVA = _SourceIndex;
                    bool _Equals = true;
                    for (int z = 0; z != p_PartWidth - 1; z++)
                    {
                        _CurrentlyColor = Color.FromArgb((int)p_Source[_SourceRVA + 3], (int)p_Source[_SourceRVA + 2], (int)p_Source[_SourceRVA + 1], (int)p_Source[_SourceRVA]);
                        _CompareColoe = Color.FromArgb((int)p_Part[_PartIndex + 3], (int)p_Part[_PartIndex + 2], (int)p_Part[_PartIndex + 1], (int)p_Part[_PartIndex]);

                        if (!ScanColor(_CurrentlyColor, _CompareColoe, p_Float))
                        {
                            _PartIndex = 0;
                            _Equals = false;
                            break;
                        }
                        _PartIndex += 4;
                        _SourceRVA += 4;
                    }
                    if (_Equals) return i;
                }
                else
                {
                    _PartIndex = 0;
                }
            }
            return -1;
        }

    }
}
