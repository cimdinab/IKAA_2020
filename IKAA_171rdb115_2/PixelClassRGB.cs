using System;

namespace IKAA_171rdb115_2
{
    public class PixelClassRGB
    {

        public byte R; //red
        public byte G; //green
        public byte B; //blue
        public byte I; //intensity
        public int X;
        public int Y;

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
            }
            else if (value < 0)
            {
                value = 0;
            }
            return value;
        }

        public void effectOpacity(PixelClassRGB a, PixelClassRGB b, double d)
        {
            R = Convert.ToByte(d * a.R + (1 - d) * b.R);
            G = Convert.ToByte(d * a.G + (1 - d) * b.G);
            B = Convert.ToByte(d * a.B + (1 - d) * b.B);
        }

        public void effectScreen(PixelClassRGB a, PixelClassRGB b)
        {
            double first = (double)a.R / 255;
            double second = (double)b.R / 255;
            R = Convert.ToByte((1 - (1 - first) * (1 - second)) * 255);

            first = (double)a.G / 255;
            second = (double)b.G / 255;
            G = Convert.ToByte((1 - (1 - first) * (1 - second)) * 255);

            first = (double)a.B / 255;
            second = (double)b.B / 255;
            B = Convert.ToByte((1 - (1 - first) * (1 - second)) * 255);
        }

        public void effectDarken(PixelClassRGB a, PixelClassRGB b)
        {
            if (b.R <= a.R) R = b.R;
            else R = a.R;
            if (b.G <= a.G) G = b.G;
            else G = a.G;
            if (b.B <= a.B) B = b.B;
            else B = a.B;
        }

        public void effectLighten(PixelClassRGB a, PixelClassRGB b)
        {
            if (b.R <= a.R) R = a.R;
            else R = b.R;
            if (b.G <= a.G) G = a.G;
            else G = b.G;
            if (b.B <= a.B) B = a.B;
            else B = b.B;
        }

        public void effectMultiply(PixelClassRGB a, PixelClassRGB b)
        {
            double first = (double)a.R / 255;
            double second = (double)b.R / 255;
            R = Convert.ToByte(first * second * 255);

            first = (double)a.G / 255;
            second = (double)b.G / 255;
            G = Convert.ToByte(first * second * 255);

            first = (double)a.B / 255;
            second = (double)b.B / 255;
            B = Convert.ToByte(first * second * 255);
        }

        public void effectAddition(PixelClassRGB a, PixelClassRGB b)
        {
            if (a.R + b.R > 255) R = 255;
            else if (a.R + b.R < 0) R = 0;
            else R = Convert.ToByte(a.R + b.R);

            if (a.G + b.G > 255) G = 255;
            else if (a.G + b.G < 0) G = 0;
            else G = Convert.ToByte(a.G + b.G);

            if (a.B + b.B > 255) B = 255;
            else if (a.B + b.B < 0) B = 0;
            else B = Convert.ToByte(a.B + b.B);
        }

        public void effectSubtract(PixelClassRGB a, PixelClassRGB b)
        {
            if (a.R + b.R - 255 > 255) R = 255;
            else if (a.R + b.R - 255 < 0) R = 0;
            else R = Convert.ToByte(a.R + b.R - 255);

            if (a.G + b.G - 255 > 255) G = 255;
            else if (a.G + b.G - 255 < 0) G = 0;
            else G = Convert.ToByte(a.G + b.G - 255);

            if (a.B + b.B - 255 > 255) B = 255;
            else if (a.B + b.B - 255 < 0) B = 0;
            else B = Convert.ToByte(a.B + b.B - 255);
        }

        public void effectOverlay(PixelClassRGB a, PixelClassRGB b)
        {
            double first;
            double second;

            first = (double)a.R / 255;
            second = (double)b.R / 255;
            if (second <= 0.5) R = Convert.ToByte((2 * first * second) * 255);
            else R = Convert.ToByte((1 - 2 * (1 - first) * (1 - second)) * 255);

            first = (double)a.G / 255;
            second = (double)b.G / 255;
            if (second <= 0.5) G = Convert.ToByte((2 * first * second) * 255);
            else G = Convert.ToByte((1 - 2 * (1 - first) * (1 - second)) * 255);

            first = (double)a.B / 255;
            second = (double)b.B / 255;
            if (second <= 0.5) B = Convert.ToByte((2 * first * second) * 255);
            else B = Convert.ToByte((1 - 2 * (1 - first) * (1 - second)) * 255);
        }

        public void effectHardLight(PixelClassRGB a, PixelClassRGB b)
        {
            double first;
            double second;

            first = (double)a.R / 255;
            second = (double)b.R / 255;
            if (first <= 0.5) R = Convert.ToByte((2 * first * second) * 255);
            else R = Convert.ToByte((1 - 2 * (1 - first) * (1 - second)) * 255);

            first = (double)a.G / 255;
            second = (double)b.G / 255;
            if (first <= 0.5) G = Convert.ToByte((2 * first * second) * 255);
            else G = Convert.ToByte((1 - 2 * (1 - first) * (1 - second)) * 255);

            first = (double)a.B / 255;
            second = (double)b.B / 255;
            if (first <= 0.5) B = Convert.ToByte((2 * first * second) * 255);
            else B = Convert.ToByte((1 - 2 * (1 - first) * (1 - second)) * 255);
        }

        public void effectSoftLight(PixelClassRGB a, PixelClassRGB b)
        {
            double first;
            double second;

            first = (double)a.R / 255;
            second = (double)b.R / 255;
            if (first <= 0.5) R = Convert.ToByte(((2 * first - 1) * (second - Math.Pow(second, 2)) + second) * 255);
            else R = Convert.ToByte(((2 * first - 1) * (Math.Sqrt(second) - second) + second) * 255);

            first = (double)a.G / 255;
            second = (double)b.G / 255;
            if (first <= 0.5) G = Convert.ToByte(((2 * first - 1) * (second - Math.Pow(second, 2)) + second) * 255);
            else G = Convert.ToByte(((2 * first - 1) * (Math.Sqrt(second) - second) + second) * 255);

            first = (double)a.B / 255;
            second = (double)b.B / 255;
            if (first <= 0.5) B = Convert.ToByte(((2 * first - 1) * (second - Math.Pow(second, 2)) + second) * 255);
            else B = Convert.ToByte(((2 * first - 1) * (Math.Sqrt(second) - second) + second) * 255);
        }

        public void effectDifference(PixelClassRGB a, PixelClassRGB b)
        {
            R = Convert.ToByte(Math.Abs(a.R - b.R));
            G = Convert.ToByte(Math.Abs(a.G - b.G));
            B = Convert.ToByte(Math.Abs(a.B - b.B));
        }
    }
}
