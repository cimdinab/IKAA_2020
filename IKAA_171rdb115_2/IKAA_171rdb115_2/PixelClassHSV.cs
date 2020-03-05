using System;

namespace IKAA_171rdb115_2
{
    public class PixelClassHSV
    {
        public int H; //hue
        public byte S; //saturation
        public byte V; //value

        public PixelClassHSV()
        {
            H = 0;
            S = 0;
            V = 0;
        }

        public PixelClassHSV(byte r, byte g, byte b)
        {
            int MAX = Math.Max(r, Math.Max(g, b));
            int MIN = Math.Min(r, Math.Min(g, b));
            if (MAX == MIN) { H = 0; }
            else if ((MAX == r) && (g >= b)) { H = 60 * (g - b) / (MAX - MIN); }
            else if ((MAX == r) && (g < b)) { H = 60 * (g - b) / (MAX - MIN) + 360; }
            else if (MAX == g) { H = 60 * (b - r) / (MAX - MIN) + 120; }
            else { H = 60 * (r - g) / (MAX - MIN) + 240; };
            if (H == 360) { H = 0; }
            if (MAX == 0) { S = 0; }
            else { S = Convert.ToByte(255 * (1 - ((float)MIN / MAX))); }
            V = (byte)(MAX);
        }
    }
}
