using System;

namespace IKAA_171rdb115_2
{
    public class PixelClassYUV
    {
        public float Yy;
        public float U;
        public float Vv;

        public PixelClassYUV()
        {
            Yy = 0;
            U = 0;
            Vv = 0;
        }

        public PixelClassYUV(byte r, byte g, byte b)
        {
            Yy = (float) ((0.299 * r) + (0.587 * g) + (0.114 * b));
            U = (float) ((-0.14713 * r) - (0.28886 * g) + (0.436 * b) +128);
            Vv = (float) ((0.615 * r) - (0.51499 * g) - (0.10001 * b) +128);
        }

    }
}
