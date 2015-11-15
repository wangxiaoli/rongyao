using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace 荣耀辅助
{
    class ORC
    {
        public static Hashtable tz = new Hashtable();
        
        public static void init ()
        {
            tz.Clear();
            tz.Add("111111111111000111000111001110001110001100011100011000111000", "7");
            tz.Add("00011110001111100011011001100110011001101100011011111111011011100000011000000110", "4");
            tz.Add("111111111111000111000111000110001110001100011100011000111000", "7");
            tz.Add("01111110111110001110000011100000111111001111111111000111110001101111111000111100", "6");
            tz.Add("0111110111111111100001100000111100011111110000111000011101111101111000", "5");
            tz.Add("111111110111000111000111000110001110001100011100011000011000", "7");
            tz.Add("000001000011111110111100111111000111111000111111000111111000111111000111111001111011111110", "0");
            tz.Add("01111110111001111100011111000110011111000111111011000111110001111110011101111110", "8");
            tz.Add("00011110001111100011011001100110111001101100011011111111000011110000011000000110", "4");
            tz.Add("00011110001111100011011001100110111001101110011011111111000011110000011000000110", "4");
            tz.Add("00011110001111100011011001100110011001101100011011111111000011100000011000000110", "4");
            tz.Add("111111111111000111000111000110001110001100011100011000011000", "7");
            tz.Add("1111110110111100011100001110000110000111000011000011100001100000110000", "7");
            tz.Add("00011110001111100011011001100110111001101110011011111111111111110000011000000110", "4");
            tz.Add("0111110111111111000001100000111100001111110000111000011101111101111000", "5");
            tz.Add("01111110111110001110000011100000111111101111111111000111110001111111111000111100", "6");
            tz.Add("011111011011011011011011011011", "1");
            tz.Add("00011110001111100011011001100110011001101100011011111111000011110000011000000110", "4");
            tz.Add("000001000011111110111101111111000111111000111111000111111000111111000111111101111011111110", "0");
            tz.Add("0111110111111111100111100011111111111111110000011000001101111110111000", "9");
            tz.Add("0111110111111011000001100000111100001111110000111000011101111101111000", "5");
            tz.Add("011111110111101111111000111111000111111000111111000111111000111111001110011111110", "0");
            tz.Add("0111110111111011100001100000111100011111110000111000011101111101111000", "5");
            tz.Add("00011110001111100011011001100110111001101100011011111111011011100000011000000110", "4");
            tz.Add("01111110111110001110000011100000111111001111111111000111110001111111111000111100", "6");
            tz.Add("000011000011111110111101111111000111111000111111000111111000111111000111111001110011111110", "0");
            tz.Add("011111110111100111111000111111000111111000111111000111111000111111001110011111110", "0");
            tz.Add("000001000011111110111101111111000111111000111111000111111000111111000111111101110011111110", "0");
            tz.Add("011111011011011011011011011010", "1");
            tz.Add("1111110111111000001100000111001111001111001110000110000011111101111110", "2");
            tz.Add("111111000011110111111100111110000111000110110000111001100110001110001100110001110011000110001100011111111011100000001110011000000000110011000000000110", "74");
            tz.Add("0111110111111011000001100000111100011111110000111000011101111101111000", "5");
            tz.Add("00011110001111100011011001100110011001101100011011111111010011100000011000000110", "4");
            tz.Add("00011110001111100011011001100110111001101110011011111111011011110000011000000110", "4");
            tz.Add("111111111111000011000011001111011110111000110000111111111111", "2");
            tz.Add("01111110111110001110000011100000111111101111111111000111110001111111111001111100", "6");
            tz.Add("1111110111111000001100000110001110000001100000111000011101111101111100", "3");
            tz.Add("0111110111111111100111100011111111111111110000011000001100111110111000", "9");
            tz.Add("0111110111111111000001100000111100011111110000111000011101111101111000", "5");
            tz.Add("1111110111111000001100000110001110000011100000111000011101111101111100", "3");
            tz.Add("0111111111111111100111100011111111111111110000011000001100111110111000", "9");
            tz.Add("000001000011111110111101111111000111111000111111000111111000111111000111111001110011111110", "0");
            tz.Add("00011110001111100011011001100110111001101110011011111111000011100000011000000110", "4");
            tz.Add("1111110111111100011100001110001110000111000011000011100001100001110000", "7");
            tz.Add("01111110111001111100011111100110011111000111111111000111110001111110011101111110", "8");
            tz.Add("111111111111000111000111001110001110001100011100011000011000", "7");
            tz.Add("1111110111111100011100001110000110000111000011000011100001100000110000", "7");
            tz.Add("00011110001111100011011001100110111001101100011011111111000011100000011000000110", "4");
            tz.Add("01111110111001111100011111000110011111000111111111000111110001111110011101111110", "8");
            tz.Add("00011110001111100011011001100110111001101100011011111111011011110000011000000110", "4");
            tz.Add("1111110111111000001100000111001111011111001110000111000011111101111110", "2");
            tz.Add("01111110111110001110000011100000111111101111111111000111110001101111111000111100", "6");
            tz.Add("0111111111111111100111100011111111111111110000011000001101111110111000", "9");
            tz.Add("000001000011111110111101111111000111111000111111000111111000111111000111111111110011111110", "0");
            tz.Add("01111110111001111100011111100110011111000111111011000111110001111110011101111110", "8");
            tz.Add("0111110111111111000111100011111111111111110000011000001100111110111000", "9");
            tz.Add("00011110001111100011011001100110011001101100011011111111011011110000011000000110", "4");
            tz.Add("111111111111000011000011001111011100111000110000111111111111", "2");
            tz.Add("1111110111111100011100001110001110000111000011000011100001100000110000", "7");
            tz.Add("111111111111000011000011001111011110111000111000111111111111", "2"); 
        }


        public static Bitmap Cut(Bitmap b, int StartX, int StartY, int iWidth, int iHeight)
        {
            if (b == null)
            {
                return null;
            }
            int w = b.Width;
            int h = b.Height;
            if (StartX >= w || StartY >= h)
            {
                return null;
            }
            if (StartX + iWidth > w)
            {
                iWidth = w - StartX;
            }
            if (StartY + iHeight > h)
            {
                iHeight = h - StartY;
            }
            try
            {
                Bitmap bmpOut = new Bitmap(iWidth, iHeight, PixelFormat.Format24bppRgb);
                Graphics g = Graphics.FromImage(bmpOut);
                g.DrawImage(b, new Rectangle(0, 0, iWidth, iHeight), new Rectangle(StartX, StartY, iWidth, iHeight), GraphicsUnit.Pixel);
                g.Dispose();
                return bmpOut;
            }
            catch
            {
                return null;
            }
        }

        static List<Rectangle> parts = new List<Rectangle>();

        #region 分割图片  
        /// <summary>  
        /// 分割图片  
        /// </summary>  
        /// <returns>处理后的验证码</returns>  
        public static Bitmap CutImg(Bitmap tempImg)
        {
            Bitmap img = new Bitmap(tempImg);
            parts.Clear();
            //Y轴分割  
            CutY(img);
            //区域个数  
            int __count = 0;
            if (XList.Count > 1)
            {
                //x起始值  
                int __start = XList[0];
                //x结束值  
                int __end = XList[XList.Count - 1];
                //x索引  
                int __idx = 0;
                while (__start != __end)
                {
                    //区域宽度  
                    int __w = __start;
                    //区域个数自加  
                    __count++;
                    while (XList.Contains(__w) && __idx < XList.Count)
                    {
                        //区域宽度自加  
                        __w++;
                        //x索引自加  
                        __idx++;
                    }
                    //区域X轴坐标  
                    int x = __start;
                    //区域Y轴坐标  
                    int y = 0;
                    //区域宽度  
                    int width = __w - __start;
                    //区域高度  
                    int height = img.Height;
                    /* 
                     * X轴分割当前区域 
                     */
                    CutX(img.Clone(new Rectangle(x, y, width, height), img.PixelFormat));
                    if (YList.Count > 1 && YList.Count != img.Height)
                    {
                        int y1 = YList[0];
                        int y2 = YList[YList.Count - 1];
                        if (y1 != 1)
                        {
                            y = y1 - 1;
                        }
                        height = y2 - y1 + 1;
                    }
                    //GDI+绘图对象  
                    /*Graphics g = Graphics.FromImage(img);
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.CompositingMode = CompositingMode.SourceOver;
                    g.PixelOffsetMode = PixelOffsetMode.HighSpeed;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;*/
                    //画出验证码区域  
                    Rectangle r = new Rectangle(x, y + 1, width, height);
                    parts.Add(r);
                    //g.DrawRectangle(new Pen(Brushes.Green), r);
                   // g.Dispose();
                    //起始值指向下一组  
                    if (__idx < XList.Count)
                    {
                        __start = XList[__idx];
                    }
                    else
                    {
                        __start = __end;
                    }

                }
            }
            return img;
        }
        #endregion
        static List<int> XList = new List<int>();
        static List<int> YList = new List<int>();
        private static bool isWhilteLine;
        #region Y轴字符分割图片  
        /// <summary>  
        /// 得到Y轴分割点  
        /// 判断每一竖行是否有黑色  
        /// 有则添加  
        /// </summary>  
        /// <param name="img">要验证的图片</param>  
        private static void CutY(Bitmap tempImg)
        {
            XList.Clear();
            Bitmap img = tempImg;
            for (int x = 0; x < img.Width; x++)
            {
                isWhilteLine = false;
                for (int y = 0; y < img.Height; y++)
                {
                    Color __c = img.GetPixel(x, y);
                    if (__c.R == 255)
                    {
                        isWhilteLine = true;
                    }
                    else
                    {
                        isWhilteLine = false;
                        break;
                    }
                }
                if (!isWhilteLine)
                {
                    XList.Add(x);
                }
            }
        }
        #endregion

        #region X轴字符分割图片  
        /// <summary>  
        /// 得到X轴分割点  
        /// 判断每一横行是否有黑色  
        /// 有则添加  
        /// </summary>  
        /// <param name="tempImg">临时区域</param>  
        private static void CutX(Bitmap tempImg)
        {
            YList.Clear();
            for (int x = 0; x < tempImg.Height; x++)
            {
                isWhilteLine = false;
                for (int y = 0; y < tempImg.Width; y++)
                {
                    Color __c = tempImg.GetPixel(y, x);
                    if (__c.R == 255)
                    {
                        isWhilteLine = true;
                    }
                    else
                    {
                        isWhilteLine = false;
                        break;
                    }
                }
                if (!isWhilteLine)
                {
                    YList.Add(x);
                }
            }
            tempImg.Dispose();
        }
        #endregion

        public static Bitmap ConvertTo1Bpp2(Bitmap bm, int r = 240, int b = 240, int g = 240)
        {
            //int value = 240;
            for (int i = 0; i < bm.Width; i++)
                for (int j = 0; j < bm.Height; j++)
                {
                    Color c = bm.GetPixel(i, j);
                    if (c.R > r && c.G > g && c.B > b)
                        bm.SetPixel(i, j, Color.Black);
                    else
                        bm.SetPixel(i, j, Color.White);
                }
            return bm;
        }

        public static string GetSingleBmpCode(Bitmap singlepic, int dgGrayValue = 0)
        {
            Color piexl;
            string code = "";
            for (int posy = 0; posy < singlepic.Height; posy++)
                for (int posx = 0; posx < singlepic.Width; posx++)
                {
                    piexl = singlepic.GetPixel(posx, posy);
                    if (piexl.R == dgGrayValue)    // Color.Black )
                        code = code + "1";
                    else
                        code = code + "0";
                }
            return code;
        }

        public static string Compare(Bitmap bm)
        {
            string text = "";
            foreach (Rectangle item in parts)
            {
                Bitmap b = bm.Clone(item, bm.PixelFormat);
                string key = GetSingleBmpCode(b);
                if (tz.ContainsKey(key))
                {
                    text += tz[key];
                }
                else
                {
                    string v = "?";
                    foreach (string k in tz.Keys)
                    {
                        try
                        {
                            if (k.Length == key.Length && CalcSimilarDegree(key, k) == 1)
                            {
                                v = tz[k] + ""; break;
                            }

                        }
                        catch (Exception)
                        {
                        }
                    }
                    if (v=="?"&&key.Length>110)
                    {
                        v = "74";
                    }
                    text += v;
                }
                b.Dispose();
            }
            bm.Dispose();
            return text;
        }
         
        public static Int32 CalcSimilarDegree(string a, string b)
        {
            if (a.Length != b.Length)
                throw new ArgumentException();
            int count = 0;
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i])
                    count++;
            }
            return count;
        }

        public static List<string> toORC(IntPtr h)
        {
            List<string> code = new List<string>();
            // Bitmap bm = sc.CaptureWindow(h);Capture(h);
            code.Add(doORC(710, 52));
            code.Add(doORC(710, 96));
            code.Add(doORC(710, 138));
            // bm.Dispose();
            return code;
        }
        public static Bitmap show;
        /*public static string doORC(Bitmap bm, int x, int y, int w = 100, int h = 20)
        {
            //string t = "";
            Bitmap src = Cut(bm, x - w / 2, y - h / 2, w, h);
            src = ConvertTo1Bpp2(src);
            show = CutImg(src);
            return Compare(src);
        }*/
        public static string doORC(int x, int y, int w = 100, int h = 20)
        {
            //string t = "";
            Bitmap src = new Bitmap(w, h);
            Graphics g = Graphics.FromImage(src);
            g.CopyFromScreen(x - w / 2, y - h / 2, 0, 0, new Size(w, h));
            g.Dispose();
            src = ConvertTo1Bpp2(src);
            show = CutImg(src);
            return Compare(src);
        }
        public static string doORC(Bitmap bm, int x, int y, int w = 100, int h = 20, int r = 240, int b = 240, int g = 240)
        {
            //string t = "";
            Bitmap src = Cut(bm, x - w / 2, y - h / 2, w, h);
            src = ConvertTo1Bpp2(src,r,b,g);
            show = CutImg(src);
            return Compare(src);
        }
    }
}
