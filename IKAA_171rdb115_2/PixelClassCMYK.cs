using System;

namespace IKAA_171rdb115_2
{
    public class PixelClassCMYK
    {
        public float C;
        public float M;
        public float Y;
        public float K;

        public PixelClassCMYK()
        {
            C = 0;
            M = 0;
            Y = 0;
            K = 0;
        }

        public PixelClassCMYK(byte r, byte g, byte b)
        {
            float Ri = r / 255f;
            float Gi = g / 255f;
            float Bi = b / 255f;
            K = ClampCMYK(1 - Math.Max(Ri, Math.Max(Gi, Bi)));
            C = ClampCMYK((1 - Ri - K) / (1 - K));
            M = ClampCMYK((1 - Gi - K) / (1 - K));
            Y = ClampCMYK((1 - Bi - K) / (1 - K));
        }

        private static float ClampCMYK(float value)
        {
            if(value < 0 || float.IsNaN(value))
            {
                value = 0;
            }
            return value;
        }
    }
}
