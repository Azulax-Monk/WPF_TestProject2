using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_TestProject2.Classes
{
    public class ColorUtils
    {
        public static float[] rgbToCmyk(int r, int g, int b)
        {
            float fr = r / 255F, fg = g / 255F, fb = b / 255F;
            float c, m, y, k;

            c = 1 - fr;
            m = 1 - fg;
            y = 1 - fb;
            k = Math.Min(c, Math.Min(m, y));
            if (k == 1)
            {
                return new float[] { 0, 0, 0, 1 };
            }
            else
            {
                return new float[] {
                    (c - k) / (1 - k),
                    (m - k) / (1 - k),
                    (y - k) / (1 - k),
                    k};
            }

        }

        public static int[] cmykToRgb(float c, float m, float y, float k)
        {
            c = c * (1 - k) + k;
            m = m * (1 - k) + k;
            y = y * (1 - k) + k;

            float r = 1 - c;
            float g = 1 - m;
            float b = 1 - y;

            return new int[] {Convert.ToInt32(r*255), Convert.ToInt32(g * 255), Convert.ToInt32(b * 255)};
        }

        public static float[] rgbToHsl(int r, int g, int b)
        {
            float fr = r / 255F, fg = g / 255F, fb = b / 255F;
            float max = Math.Max(fr, Math.Max(fg, fb));
            float min = Math.Min(fr, Math.Min(fg, fb));
            float h, s, l = 0.5F * (max + min);
            if (max == min || l == 0)
            {
                s = 0;
            }
            else
            {
                s = (max - min) / (1 - Math.Abs(2 * l - 1));

            }
            if(max==min)
            {
                h = 0;
            }
            else if(max==fr)
            {
                h = 60 * ((fg - fb) / (max - min)) + ((fg < fb) ? 360 : 0);
            }
            else if(max==fg)
            {
                h = 60 * ((fb - fr) / (max - min)) + 120;
            }
            else 
            {
                h = 60 * ((fr - fg) / (max - min)) + 240;
            }
            return new float[] { h, s, l };
        }

        public static float[] hslToRgb(float h, float s, float l)
        {
            h /= 60;
            float c = ((1 - Math.Abs(2f * l - 1)) * s), x = c * (1 - Math.Abs(h % 2f - 1));
            float[] rgb = null;
            if (h == 0)
            {
                rgb = new float[] { 0, 0, 0 };
            }
            else if (h >= 0 && h < 1)
            {
                rgb = new float[] { c, x, 0 };
            }
            else if (h >= 1 && h <= 2)
            {
                rgb = new float[] { x, c, 0 };
            }
            else if (h >= 2 && h <= 3)
            {
                rgb = new float[] { 0, c, x };
            }
            else if (h >= 3 && h <= 4)
            {
                rgb = new float[] { 0, x, c };
            }
            else if (h >= 4 && h <= 5)
            {
                rgb = new float[] { x, 0, c };
            }
            else if (h >= 5 && h <= 6)
            {
                rgb = new float[] { c, 0, x };
            }

            float m = l - 0.5f * c;
            return new float[] { rgb[0] + m, rgb[1] + m, rgb[2] + m };
        }
}
}
