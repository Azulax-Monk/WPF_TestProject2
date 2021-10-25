using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace WPF_TestProject2.Classes
{
    class GraphicsUtils
    {
        public static IEnumerable<Point> GetPointsOnLine(Point p1, Point p2)
        {
            bool steep = Math.Abs(p2.Y - p1.Y) > Math.Abs(p2.X - p1.X);
            if (steep)
            {
                int t;
                t = p1.X; // swap x0 and y0
                p1.X = p1.Y;
                p1.Y = t;
                t = p2.X; // swap x1 and y1
                p2.X = p2.Y;
                p2.Y = t;
            }
            if (p1.X > p2.X)
            {
                int t;
                t = p1.X; // swap x0 and x1
                p1.X = p2.X;
                p2.X = t;
                t = p1.Y; // swap y0 and y1
                p1.Y = p2.Y;
                p2.Y = t;
            }
            int dx = p2.X - p1.X;
            int dy = Math.Abs(p2.Y - p1.Y);
            int error = dx / 2;
            int ystep = (p1.Y < p2.Y) ? 1 : -1;
            int y = p1.Y;
            for (int x = p1.X; x <= p2.X; x++)
            {
                yield return new Point((steep ? y : x), (steep ? x : y));
                error = error - dy;
                if (error < 0)
                {
                    y += ystep;
                    error += dx;
                }
            }
            yield break;
        }

        public static float GetLength(Point p1, Point p2)
        {
            return (float)Math.Sqrt(Math.Pow((p2.X - p1.X), 2.0) +
                    Math.Pow((p2.Y - p1.Y), 2.0));
        }

        public static Point GetEndpoint(double angle, Point start, float lenght)
        {
            double radians = (Math.PI / 180) * angle;
            double xOffset = (lenght * Math.Cos(radians));
            double yOffset = (lenght * Math.Sin(radians));
            double x2 = start.X + xOffset;
            double y2 = start.Y + yOffset;

            return new Point((int)Math.Round(x2), (int)Math.Round(y2));
        }

        // returns 1 when arc = 0 ????????????
        public static float GetAngle(Point p1, Point p2)
        {
            Point c = new Point(p2.X, p1.Y);
            float leg = GetLength(p1, c);
            float hupotenuse = GetLength(p1, p2);
            float angle = (float)Math.Acos(Math.Cos(leg / hupotenuse));

            if (p2.X >= p1.X && p2.Y >= p1.Y)
                angle = angle;
            else if (p2.X < p1.X && p2.Y >= p1.Y)
                angle = 180 - angle;
            else if (p2.X <= p1.X && p2.Y < p1.Y)
                angle = 180 + angle;
            else if (p2.X > p1.X && p2.Y > p1.Y)
                angle = 0 - angle;

            return angle;
        }

        public static void DrawLine(Bitmap bmp, IEnumerable<Point> line, System.Drawing.Color color)
        {
            foreach (Point pixel in line)
            {
                bmp.SetPixel(pixel.X, pixel.Y, color);
            }
        }
    
        public static WriteableBitmap ConvertToWriteableBitmap(Bitmap bmp)
        {
            BitmapSource bitmapSource =
                System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                bmp.GetHbitmap(), IntPtr.Zero, System.Windows.Int32Rect.Empty,
            BitmapSizeOptions.FromEmptyOptions());

            return new WriteableBitmap(bitmapSource);
        }
    }
}
