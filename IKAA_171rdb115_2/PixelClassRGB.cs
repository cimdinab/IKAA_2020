using System;

namespace IKAA_171rdb115_2
{
    public class PixelClassRGB
    {

        public byte R; //red
        public byte G; //green
        public byte B; //blue
        public byte I; //intensity

        public PixelClassRGB()
        {
            R = 0;
            G = 0;
            B = 0;
            I = 0;
        }
        public PixelClassRGB(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
            I = (byte)Math.Round(0.0722f * b + 0.7152f * g + 0.2126f * r);
        }

        public PixelClassRGB hsvToRGB(int h, byte s, byte v)
        {
            byte r = 0;
            byte g = 0;
            byte b = 0;

            int Hi = Convert.ToInt32(h / 60);
            byte Vmin = Convert.ToByte((255 - s) * v / 255);
            int a = Convert.ToInt32((v - Vmin) * (h % 60) / 60);
            byte Vinc = Convert.ToByte(Vmin + a);
            byte Vdec = Convert.ToByte(v - a);

            switch (Hi)
            {
                case 0: { r = v; g = Vinc; b = Vmin; break; }
                case 1: { r = Vdec; g = v; b = Vmin; break; }
                case 2: { r = Vmin; g = v; b = Vinc; break; }
                case 3: { r = Vmin; g = Vdec; b = v; break; }
                case 4: { r = Vinc; g = Vmin; b = v; break; }
                case 5: { r = v; g = Vmin; b = Vdec; break; }
            }
            PixelClassRGB rgbPix = new PixelClassRGB(r, g, b);
            return rgbPix;
        }

        public PixelClassRGB cmykToRGB(float c, float m, float y, float k)
        {
            byte r, g, b;
            r = Convert.ToByte(255 * (1 - c) * (1 - k));
            g = Convert.ToByte(255 * (1 - m) * (1 - k));
            b = Convert.ToByte(255 * (1 - y) * (1 - k));
            PixelClassRGB rgbPix = new PixelClassRGB(r, g, b);
            return rgbPix;
        }

        public PixelClassRGB yuvToRGB(float Yy, float U, float Vv)
        {
            byte r, g, b;
            r = Convert.ToByte(ClampYUV(Yy + 1.13983f * (Vv - 128f)));
            g = Convert.ToByte(ClampYUV(Yy - 0.39465f * (U - 128f) - 0.58060f * (Vv - 128f)));
            b = Convert.ToByte(ClampYUV(Yy + 2.03211f * (U - 128f)));/*
            byte R = Convert.ToByte(r);
            byte G = Convert.ToByte(g);
            byte B = Convert.ToByte(b);*/
            PixelClassRGB rgbPix = new PixelClassRGB(r, g, b);
            return rgbPix;
        }

        private static float ClampYUV(float value)
        {
            if (value > 255)
            {
                value = 255;
            }else if (value < 0)
            {
                value = 0;
            }
            return value;
        }
    }
}
