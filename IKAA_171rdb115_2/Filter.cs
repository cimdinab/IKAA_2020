using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace IKAA_171rdb115_2
{
    public class Filter
    {
        public int[,] F; //vektors ar filtra vērtībam
        public int K; // koeficients
        public int[,] Fr;
        public Filter()
        {
            F = new int[3, 3]; //veidojam jaunu filtru
            Fr = new int[2, 2];
        }
        public int calculateCoefficient(int[,] f)
        //izskaitļojam koefficientus
        {
            int k = 0;
            for (int i = 0; i < f.GetLength(0); i++)
            {
                for (int j = 0; j < f.GetLength(1); j++)
                {
                    k += f[i, j]; //saskaitam filtra elementu summu
                }
            }
            return k;
        }
        public void filter3x3Blur() //Pirmais blur
        {
            F = new int[,] { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } }; //filtrs
            K = calculateCoefficient(F); //izskaitļojam koefficientu
        }

        public void filter3x3Blur2() //Otrais blur
        {
            F = new int[,] { { 1, 1, 1 }, { 1, 2, 1 }, { 1, 1, 1 } };
            K = calculateCoefficient(F);
        }

        public void filter3x3Blur3() //Tresais blur
        {
            F = new int[,] { { 1, 2, 1 }, { 2, 4, 2 }, { 1, 2, 1 } };
            K = calculateCoefficient(F);
        }

        public void filter3x3Sharpen() //Pirmais sharpen
        {
            F = new int[,] { { 0, -1, 0 }, { -1, 5, -1 }, { 0, -1, 0 } };
            K = calculateCoefficient(F);
        }

        public void filter3x3Sharpen2() //Otrais sharpen
        {
            F = new int[,] { { -1, -1, -1 }, { -1, 9, -1 }, { -1, -1, -1 } };
            K = calculateCoefficient(F);
        }

        public void filter3x3Sharpen3() //Tresais sharpen
        {
            F = new int[,] { { 0, -2, 0 }, { -2, 9, -2 }, { 0, -2, 0 } };
            K = calculateCoefficient(F);
        }

        public void xFilter3x3Sobel()
        {
            F = new int[,] { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
        }

        public void yFilter3x3Sobel()
        {
            F = new int[,] { { 1, 2, 1 }, { 0, 0, 0 }, { -1, -2, -1 } };
        }

        public void xFilter3x3Prewitt()
        {
            F = new int[,] { { -1, 0, 1 }, { -1, 0, 1 }, { -1, 0, 1 } };
        }

        public void yFilter3x3Prewitt()
        {
            F = new int[,] { { 1, 1, 1 }, { 0, 0, 0 }, { -1, -1, -1 } };
        }

        public void xFilter2x2Roberts()
        {
            Fr = new int[,] { { -1, 0 }, { 0, 1 } };
        }

        public void yFilter2x2Roberts()
        {
            Fr = new int[,] { { 1, 0 }, { 0, -1 } };
        }

        public void MedianFilter(
            Bitmap bmp,
            PixelClassRGB[,] img,
            int matrixSize,
            int bias = 0,
            bool grayscale = false)
        {
            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            byte[] pixelBuffer = new byte[bmpData.Stride * bmpData.Height];
            byte[] resultBuffer = new byte[bmpData.Stride * bmpData.Height];
            Marshal.Copy(bmpData.Scan0, pixelBuffer, 0, pixelBuffer.Length);
            bmp.UnlockBits(bmpData);

            if (grayscale == true)
            {
                float rgb = 0;
                for (int k = 0; k < pixelBuffer.Length; k += 4)
                {
                    rgb = pixelBuffer[k] * 0.0722f + pixelBuffer[k + 1] * 0.7152f + pixelBuffer[k + 2] * 0.2126f;

                    pixelBuffer[k] = (byte)rgb;
                    pixelBuffer[k + 1] = pixelBuffer[k];
                    pixelBuffer[k + 2] = pixelBuffer[k];
                    pixelBuffer[k + 3] = 255;
                }
            }

            int filterOffset = (matrixSize - 1) / 2;
            int calcOffset = 0;
            int byteOffset = 0;

            List<int> neighbourPixels = new List<int>();
            byte[] middlePixel;

            for (int offsetY = filterOffset; offsetY < bmp.Height - filterOffset; offsetY++)
            {
                for (int offsetX = filterOffset; offsetX < bmp.Width - filterOffset; offsetX++)
                {
                    byteOffset = offsetY * bmpData.Stride + offsetX * 4;
                    neighbourPixels.Clear();

                    for (int filterY = -filterOffset; filterY <= filterOffset; filterY++)
                    {
                        for (int filterX = -filterOffset; filterX <= filterOffset; filterX++)
                        {
                            calcOffset = byteOffset + (filterX * 4) + (filterY * bmpData.Stride);
                            neighbourPixels.Add(BitConverter.ToInt32(pixelBuffer, calcOffset));
                        }
                    }

                    neighbourPixels.Sort();
                    middlePixel = BitConverter.GetBytes(neighbourPixels[filterOffset]);

                    resultBuffer[byteOffset] = middlePixel[0];
                    resultBuffer[byteOffset + 1] = middlePixel[1];
                    resultBuffer[byteOffset + 2] = middlePixel[2];
                    resultBuffer[byteOffset + 3] = middlePixel[3];

                    img[offsetX, offsetY].R = resultBuffer[byteOffset + 2];
                    img[offsetX, offsetY].G = resultBuffer[byteOffset + 1];
                    img[offsetX, offsetY].B = resultBuffer[byteOffset];
                    img[offsetX, offsetY].I = resultBuffer[byteOffset + 3];
                }
            }
        }
    }
}
