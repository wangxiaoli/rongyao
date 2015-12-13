using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 荣耀辅助
{
    class ImageFind
    {
        public static List<Point> ToCollectingResources(Bitmap srcImg)
        {
            List<Point> pList = new List<Point>();
            List<Rectangle> rectList = new List<Rectangle>();
            for (int x = 100; x < 750; x++)
            {
                for (int y =100; y < 600; y++)
                {
                    bool b = false;
                    foreach (Rectangle item in rectList)
                    {
                        if (item.Contains(x, y))
                        {
                            b = true;
                            break;
                        }
                    }
                    if (b)
                    {
                        continue;
                    }

                    Color c = srcImg.GetPixel(x, y);

                    if (c.G - c.B == 36) 
                    {

                        int count = 0;
                        for (int i = 1; i < 5; i++)
                        {
                            c = srcImg.GetPixel(x + i, y);
                            if (c.G - c.B == 36)  
                                count++;
                        }
                        if (count == 4)
                        {
                            Rectangle r = new Rectangle(x - 5, y - 5, 25, 25);
                            rectList.Add(r);
                            pList.Add(new Point(x + 5, y + 5));
                        }
                    }
                     
                }
            }
            return pList;
        }

        public static Point v1 = new Point(60, 60);
        public static Point v2 = new Point(405, 60);
        public static Point v3 = new Point(750, 60);
        public static Point v4 = new Point(60, 330);
        public static Point v5 = new Point(750, 330);
        public static Point v6 = new Point(60, 600);
        public static Point v7 = new Point(405, 600);
        public static Point v8 = new Point(750, 600);
        public static Point v9 = new Point(405, 330);
        public static int find1ine(Bitmap srcImg,string key)
        {  
            List<Color> colors = new List<Color>();

            string[] ss = key.Split(new char[] { ' ' }); 
            for (int i=0;i<5 ;i++)
            {
                colors.Add(Color.FromArgb(int.Parse(ss[i])));
            }

            Bitmap newBtm = new Bitmap(srcImg.Width, srcImg.Height);
            Graphics g = Graphics.FromImage(newBtm);
            g.Clear(Color.White);
            g.FillPolygon(Brushes.Black, new Point[] { v1, v2, v4 });
            g.FillPolygon(Brushes.Black, new Point[] { v2, v3, v5 });
            g.FillPolygon(Brushes.Black, new Point[] { v4, v6, v7 });
            g.FillPolygon(Brushes.Black, new Point[] { v5, v7, v8 });
            g.Dispose();
            int bv = Color.Black.ToArgb();
            List<Rectangle> rectLst = new List<Rectangle>();
            List<Point> ps = new List<Point>();
            int f =  int.Parse(ss[5]);
            DateTime dt1 = DateTime.Now;
            for (int y = 60; y < 600; y++)
            {
                for (int x = 60; x < 750; x++)
                {
                    bool b = false;

                    foreach (Rectangle item in rectLst)
                    {
                        if (item.Contains(x, y))
                        {
                            b = true;
                            break;
                        }
                    }
                    if (b)
                    {
                        continue;
                    }
                    Color c = newBtm.GetPixel(x, y);
                    if (c.ToArgb() == bv)
                    {
                        continue;
                    }
                    c = srcImg.GetPixel(x, y);
                    if (OPImage.ScanColor(colors[0], c, f))
                    {
                        b = true;
                        for (int i = 1; i < 5; i++)
                        {
                            c = srcImg.GetPixel(x + i, y);
                            b = b && OPImage.ScanColor(colors[i], c, f);
                        }
                        if (b)
                        {
                            Rectangle r = new Rectangle(x - 1, y - 1, 12, 12);
                            rectLst.Add(r);
                            ps.Add(new Point(x, y));
                            x += 5;
                        }
                    }
                }
            }
            newBtm.Dispose();
            return ps.Count;
        }

        public static List<Point> find1inex(Bitmap srcImg, string key)
        {
            List<Color> colors = new List<Color>();

            string[] ss = key.Split(new char[] { ' ' });
            for (int i = 0; i < 5; i++)
            {
                colors.Add(Color.FromArgb(int.Parse(ss[i])));
            }

            Bitmap newBtm = new Bitmap(srcImg.Width, srcImg.Height);
            Graphics g = Graphics.FromImage(newBtm);
            g.Clear(Color.White);
            g.FillPolygon(Brushes.Black, new Point[] { v1, v2, v4 });
            g.FillPolygon(Brushes.Black, new Point[] { v2, v3, v5 });
            g.FillPolygon(Brushes.Black, new Point[] { v4, v6, v7 });
            g.FillPolygon(Brushes.Black, new Point[] { v5, v7, v8 });
            g.Dispose();
            int bv = Color.Black.ToArgb();
            List<Rectangle> rectLst = new List<Rectangle>();
            List<Point> ps = new List<Point>();
            int f = int.Parse(ss[5]);
            DateTime dt1 = DateTime.Now;
            for (int y = 60; y < 600; y++)
            {
                for (int x = 60; x < 750; x++)
                {
                    bool b = false;

                    foreach (Rectangle item in rectLst)
                    {
                        if (item.Contains(x, y))
                        {
                            b = true;
                            break;
                        }
                    }
                    if (b)
                    {
                        continue;
                    }
                    Color c = newBtm.GetPixel(x, y);
                    if (c.ToArgb() == bv)
                    {
                        continue;
                    }
                    c = srcImg.GetPixel(x, y);
                    if (OPImage.ScanColor(colors[0], c, f))
                    {
                        b = true;
                        for (int i = 1; i < 5; i++)
                        {
                            c = srcImg.GetPixel(x + i, y);
                            b = b && OPImage.ScanColor(colors[i], c, f);
                        }
                        if (b)
                        {
                            Rectangle r = new Rectangle(x - 1, y - 1, 12, 12);
                            rectLst.Add(r);
                            ps.Add(new Point(x, y));
                            x += 5;
                        }
                    }
                }
            }
            newBtm.Dispose();
            return ps;
        }
        public static List<Point> findzyCanxxoo(Bitmap srcImg )
        { 
            Point v1 = new Point(60, 60);
            Point v2 = new Point(405, 60);
            Point v3 = new Point(750, 60);
            Point v4 = new Point(60, 330);
            Point v5 = new Point(750, 330);
            Point v6 = new Point(60, 600);
            Point v7 = new Point(405, 600);
            Point v8 = new Point(750, 600);
            Bitmap newBtm = new Bitmap(srcImg);
            Graphics g = Graphics.FromImage(newBtm);
            g.FillPolygon(Brushes.Black, new Point[] { v1, v2, v4 });
            g.FillPolygon(Brushes.Black, new Point[] { v2, v3, v5 });
            g.FillPolygon(Brushes.Black, new Point[] { v4, v6, v7 });
            g.FillPolygon(Brushes.Black, new Point[] { v5, v7, v8 });
            g.FillRectangle(Brushes.Black, new Rectangle(0, 540, newBtm.Width, newBtm.Height - 540));
            g.Dispose(); 
            List<Point> ps = new List<Point>();

            List<Rectangle> rectLst = new List<Rectangle>();
            for (int x = 60; x < 750; x++)
            {
                for (int y = 80; y < 600; y++)
                {
                    bool b = false;

                    foreach (Rectangle item in rectLst)
                    {
                        if (item.Contains(x, y))
                        {
                            b = true;
                            break;
                        }
                    }
                    if (b)
                    {
                        continue;
                    }
                    Color c = newBtm.GetPixel(x, y);
                    if (c.ToArgb() == Color.Black.ToArgb())
                    {
                        continue;
                    }
                    if (c.ToArgb() == Color.White.ToArgb())
                    {
                        continue;
                    }

                    string str = "";
                    if (c.B == 255 && c.G > 100 && c.G < 200 && c.R > 200)
                    {
                        for (int i = 1; i < 3; i++)
                        {
                            c = srcImg.GetPixel(x + i, y);
                            str = str + c.B;
                        }
                        for (int i = 1; i < 3; i++)
                        {
                            c = srcImg.GetPixel(x + i, y + 1);
                            str = str + c.B;
                        }
                        for (int i = 1; i < 3; i++)
                        {
                            c = srcImg.GetPixel(x + i, y + 2);
                            str = str + c.B;
                        }

                        if (str == "255255255255255255")
                        {
                            Rectangle r = new Rectangle(x - 1, y - 1, 12, 12);
                            rectLst.Add(r);
                            ps.Add(new Point(x, y));
                            continue;
                        }
                    }

                    if (c.B == 255 && c.G > 100 && c.G < 200 && c.R > 201)
                    {
                        int ck = 0;
                        for (int i = 1; i < 3; i++)
                        {
                            c = srcImg.GetPixel(x + i, y);
                            if (c.B == 255) ck++;
                        }
                        for (int i = 1; i < 3; i++)
                        {
                            c = srcImg.GetPixel(x + i, y + 1);
                            if (c.B == 255) ck++;
                        }
                        for (int i = 1; i < 3; i++)
                        {
                            c = srcImg.GetPixel(x + i, y + 2);
                            if (c.B == 255) ck++;
                        }

                        if (ck > 4)
                        {
                            Rectangle r = new Rectangle(x - 1, y - 1, 12, 12);
                            rectLst.Add(r);
                            ps.Add(new Point(x, y));
                            continue;
                        }
                    }

                    if (c.B == 0 && c.G < 110 && c.R < 200)//&& c.G > 60 && c.R >60
                    {
                        str = "";
                        for (int i = 1; i < 6; i++)
                        {
                            c = srcImg.GetPixel(x + i, y);
                            str = str + c.B;
                        }
                        if (str == "00000")
                        {
                            Rectangle r = new Rectangle(x - 1, y - 15, 30, 30);
                            rectLst.Add(r);
                            continue;
                        }
                        str = "";
                        for (int i = 1; i < 4; i++)
                        {
                            c = srcImg.GetPixel(x + i, y);
                            str = str + c.B;
                        }
                        for (int i = 2; i < 4; i++)
                        {
                            c = srcImg.GetPixel(x + i, y + 1);
                            str = str + c.B;
                        }
                        if (str == "00000")
                        {
                            Rectangle r = new Rectangle(x - 1, y - 15, 30, 30);
                            rectLst.Add(r);
                            continue;
                        }
                    }

                    if (c.B == 0 && c.G > 210 && c.R > 210)//
                    {
                        str = "";
                        for (int i = 1; i < 4; i++)
                        {
                            c = srcImg.GetPixel(x + i, y);
                            str = str + c.B;
                        }
                        for (int i = 1; i < 4; i++)
                        {
                            c = srcImg.GetPixel(x + i, y + 1);
                            str = str + c.B;
                        }
                        if (str == "000000")
                        {
                            Rectangle r = new Rectangle(x - 1, y - 1, 12, 12);
                            rectLst.Add(r);
                            ps.Add(new Point(x, y));
                            continue;
                        }
                        str = "";
                        for (int i = 1; i < 3; i++)
                        {
                            c = srcImg.GetPixel(x + i, y);
                            str = str + c.B;
                        }
                        for (int i = 1; i < 4; i++)
                        {
                            c = srcImg.GetPixel(x + i, y + 1);
                            str = str + c.B;
                        }
                        if (str == "00000")
                        {
                            Rectangle r = new Rectangle(x - 1, y - 1, 12, 12);
                            rectLst.Add(r);
                            ps.Add(new Point(x, y));
                            continue;
                        }

                    }
                    if (c.B == 0 && c.G < 130 && c.R < 200)//
                    {
                        str = "";
                        for (int i = 1; i < 5; i++)
                        {
                            c = srcImg.GetPixel(x + i, y);
                            str = str + c.B;
                        }
                        c = srcImg.GetPixel(x + 2, y + 1);
                        str = str + c.B;
                        if (str == "00000")
                        {
                            Rectangle r = new Rectangle(x - 1, y - 1, 12, 12);
                            rectLst.Add(r);
                            ps.Add(new Point(x, y));
                            continue;
                        }
                        str = "";
                        for (int i = 1; i < 4; i++)
                        {
                            c = srcImg.GetPixel(x + i, y);
                            str = str + c.B;
                        }
                        for (int i = 2; i < 4; i++)
                        {
                            c = srcImg.GetPixel(x + i, y + 1);
                            str = str + c.B;
                        }
                        if (str == "00000")
                        {
                            Rectangle r = new Rectangle(x - 1, y - 1, 12, 12);
                            rectLst.Add(r);
                            ps.Add(new Point(x, y));
                            continue;
                        }
                        str = "";
                        for (int i = 1; i < 3; i++)
                        {
                            c = srcImg.GetPixel(x + i, y);
                            str = str + c.B;
                        }
                        for (int i = 1; i < 4; i++)
                        {
                            c = srcImg.GetPixel(x + i, y + 1);
                            str = str + c.B;
                        }
                        if (str == "00000")
                        {
                            Rectangle r = new Rectangle(x - 1, y - 1, 12, 12);
                            rectLst.Add(r);
                            ps.Add(new Point(x, y));
                            continue;
                        }
                        str = "";
                        for (int i = 1; i < 4; i++)
                        {
                            c = srcImg.GetPixel(x + i, y);
                            str = str + c.B;
                        }
                        for (int i = 1; i < 3; i++)
                        {
                            c = srcImg.GetPixel(x + i, y + 1);
                            str = str + c.B;
                        }
                        if (str == "00000")
                        {
                            Rectangle r = new Rectangle(x - 1, y - 1, 12, 12);
                            rectLst.Add(r);
                            ps.Add(new Point(x, y));
                            continue;
                        }
                    }
                    if (c.B == 0 && c.G > 220 && c.G < 230 && c.R > 250)//
                    {
                        str = "";
                        for (int i = 1; i < 3; i++)
                        {
                            c = srcImg.GetPixel(x + i, y);
                            str = str + c.B;
                        }
                        for (int i = 0; i < 1; i++)
                        {
                            c = srcImg.GetPixel(x + i, y + 1);
                            str = str + c.B;
                        }
                        if (str == "000")
                        {
                            Rectangle r = new Rectangle(x - 1, y - 1, 12, 12);
                            rectLst.Add(r);
                            ps.Add(new Point(x, y));
                            continue;
                        }
                    }
                    if (c.B == 0 && c.G > 210 && c.R > 210)//
                    {
                        str = "";
                        for (int i = 1; i < 3; i++)
                        {
                            c = srcImg.GetPixel(x + i, y);
                            str = str + c.B;
                        }
                        for (int i = 0; i < 3; i++)
                        {
                            c = srcImg.GetPixel(x + i, y + 1);
                            str = str + c.B;
                        }
                        if (str == "00000")
                        {
                            Rectangle r = new Rectangle(x - 1, y - 1, 12, 12);
                            rectLst.Add(r);
                            ps.Add(new Point(x, y));
                            continue;
                        }

                    }
                    if (c.B == 0 && c.G > 140 && c.R > 201 && c.G < 150 && c.R < 210)//
                    {
                        str = "";
                        for (int i = 1; i < 3; i++)
                        {
                            c = srcImg.GetPixel(x + i, y);
                            str = str + c.B;
                        }
                        for (int i = 0; i < 2; i++)
                        {
                            c = srcImg.GetPixel(x + i, y + 1);
                            str = str + c.B;
                        }
                        if (str == "0000")
                        {
                            Rectangle r = new Rectangle(x - 1, y - 1, 12, 12);
                            rectLst.Add(r);
                            ps.Add(new Point(x, y));
                            continue;
                        }
                    }
                }
            }
            newBtm.Dispose();
            return ps;
        }
        public static List<Point> findssCanxxoo(Bitmap srcImg)
        {
            Point v1 = new Point(60, 60);
            Point v2 = new Point(405, 60);
            Point v3 = new Point(750, 60);
            Point v4 = new Point(60, 330);
            Point v5 = new Point(750, 330);
            Point v6 = new Point(60, 600);
            Point v7 = new Point(405, 600);
            Point v8 = new Point(750, 600);
            Bitmap newBtm = new Bitmap(srcImg);
            Graphics g = Graphics.FromImage(newBtm);
            g.FillPolygon(Brushes.Black, new Point[] { v1, v2, v4 });
            g.FillPolygon(Brushes.Black, new Point[] { v2, v3, v5 });
            g.FillPolygon(Brushes.Black, new Point[] { v4, v6, v7 });
            g.FillPolygon(Brushes.Black, new Point[] { v5, v7, v8 });
            g.FillRectangle(Brushes.Black, new Rectangle(0, 540, newBtm.Width, newBtm.Height - 540));
            g.Dispose();
            List<Point> ps = new List<Point>();

            List<Rectangle> rectLst = new List<Rectangle>();
            for (int x = 60; x < 750; x++)
            {
                for (int y = 80; y < 600; y++)
                {
                    bool b = false;

                    foreach (Rectangle item in rectLst)
                    {
                        if (item.Contains(x, y))
                        {
                            b = true;
                            break;
                        }
                    }
                    if (b)
                    {
                        continue;
                    }
                    Color c = newBtm.GetPixel(x, y);
                    if (c.ToArgb() == Color.Black.ToArgb())
                    {
                        continue;
                    }
                    if (c.ToArgb() == Color.White.ToArgb())
                    {
                        continue;
                    }

                    string str = "";
                    if (c.B == 255 && c.G > 100 && c.G < 200 && c.R > 200)
                    {
                        for (int i = 1; i < 3; i++)
                        {
                            c = srcImg.GetPixel(x + i, y);
                            str = str + c.B;
                        }
                        for (int i = 1; i < 3; i++)
                        {
                            c = srcImg.GetPixel(x + i, y + 1);
                            str = str + c.B;
                        }
                        for (int i = 1; i < 3; i++)
                        {
                            c = srcImg.GetPixel(x + i, y + 2);
                            str = str + c.B;
                        }

                        if (str == "255255255255255255")
                        {
                            Rectangle r = new Rectangle(x - 1, y - 1, 12, 12);
                            rectLst.Add(r);
                            ps.Add(new Point(x, y));
                            continue;
                        }
                    }

                    if (c.B == 255 && c.G > 100 && c.G < 200 && c.R > 201)
                    {
                        int ck = 0;
                        for (int i = 1; i < 3; i++)
                        {
                            c = srcImg.GetPixel(x + i, y);
                            if (c.B == 255) ck++;
                        }
                        for (int i = 1; i < 3; i++)
                        {
                            c = srcImg.GetPixel(x + i, y + 1);
                            if (c.B == 255) ck++;
                        }
                        for (int i = 1; i < 3; i++)
                        {
                            c = srcImg.GetPixel(x + i, y + 2);
                            if (c.B == 255) ck++;
                        }

                        if (ck > 4)
                        {
                            Rectangle r = new Rectangle(x - 1, y - 1, 12, 12);
                            rectLst.Add(r);
                            ps.Add(new Point(x, y));
                            continue;
                        }
                    }

                }
            }
            newBtm.Dispose();
            return ps;
        }


        public static List<Point> findssCanxxoo(Bitmap srcImg,Bitmap tempIMg,int f = 8)
        {
            List<Point> ps = new List<Point>();
            ; 
            ps = OPImage.GetImageContains(srcImg, tempIMg, f); 
             
            List<Point> pss = ps.ToList();
            for (int i = 0; i < ps.Count - 1; i++)
            {
                if (Math.Abs(ps[i].X - ps[i + 1].X) < 5)
                {
                    pss.Remove(ps[i]);
                    pss.Remove(ps[i + 1]);
                }
            }

            ps = pss;
            pss = new List<Point>();
            Rectangle r = new Rectangle(60, 80, 660, 520);
            foreach (var item in ps)
            {
                if (r.Contains(item))
                {
                    pss.Add(item);
                }
            }

            return pss;
        }

        public static int w = 806;
        public static int h = 729;

        public static List<Point>   getdownpiont(List<Point> ps)
        {

            int w = 736, h = 560;
            double d = w * 1.0 / h;
            Point v1 = new Point(34, 50);
            Point v2 = new Point(402, 50);
            Point v3 = new Point(770, 50);
            Point v4 = new Point(34, 330);
            Point v5 = new Point(770, 330);
            Point v6 = new Point(34, 610);
            Point v7 = new Point(402, 610);
            Point v8 = new Point(770, 610);
            Point v9 = new Point(402, 330);
            Rectangle r1 = new Rectangle(v1, new Size(368, 280));
            Rectangle r2 = new Rectangle(v2, new Size(368, 280));
            Rectangle r3 = new Rectangle(v4, new Size(368, 280));
            Rectangle r4 = new Rectangle(v9, new Size(368, 280));
            Bitmap bm = new Bitmap(w, h);
            Rectangle[] rects = new Rectangle[] { r1, r2, r3, r4 };
            Point[] pdds = new Point[] { new Point(-1, -1), new Point(1, -1), new Point(-1, 1), new Point(1, 1) };
            Graphics g = Graphics.FromImage(bm);
            g.Clear(Color.Black);
            g.FillRectangle(Brushes.White, new Rectangle(v1, new Size(w, h)));
            g.FillPolygon(Brushes.Black, new Point[] { v1, v2, v4 });
            g.FillPolygon(Brushes.Black, new Point[] { v2, v3, v5 });
            g.FillPolygon(Brushes.Black, new Point[] { v4, v6, v7 });
            g.FillPolygon(Brushes.Black, new Point[] { v5, v7, v8 });
            g.Dispose();
            List<Point> rps = new List<Point>();
            int brgb = Color.Black.ToArgb();
            foreach (Point p in ps)
            {
                for (int i = 0; i < rects.Length; i++)
                {
                    if (rects[i].Contains(p))
                    {
                        int c = 1;
                        int x = p.X + ((int)Math.Ceiling(c * d)) * pdds[i].X, y = p.Y + c * pdds[i].Y;

                        int rgb = bm.GetPixel(x, y).ToArgb();
                        while (rgb != brgb)
                        {
                            c++;
                            x = p.X + ((int)Math.Ceiling(c * d)) * pdds[i].X;
                            y = p.Y + c * pdds[i].Y;
                            rgb = bm.GetPixel(x, y).ToArgb();
                        }
                        if (i == 2)
                        {
                            x += 2; y -= 1;
                        }
                        rps.Add(new Point(x, y));
                        break;
                    }
                }
            }
            bm.Dispose();
            return rps;

        }



    } 

}
