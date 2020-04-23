using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace IKAA_171rdb115_2
{
    public class imgData
    {
        public PixelClassRGB[,] img;
        public PixelClassHSV[,] imghsv;
        public PixelClassCMYK[,] imgcmyk;
        public PixelClassYUV[,] imgyuv;
        public PixelClassRGB[,] imgnew;
        public PixelClassHSV[,] imghsvnew;
        public Histogram hist1; //original image
        public Histogram hist2; //edited image
        public Filter filters;
        public Filter filters2;

        ~imgData()
        {
            img = null;
            imghsv = null;
            imgcmyk = null;
            imgyuv = null;
            imgnew = null;
            imghsvnew = null;
            hist1 = null;
            hist2 = null;
        }


        public void readImage(Bitmap bmp)
        {
            var watchread = System.Diagnostics.Stopwatch.StartNew();
            img = new PixelClassRGB[bmp.Width, bmp.Height];
            imgnew = new PixelClassRGB[bmp.Width, bmp.Height];
            imghsv = new PixelClassHSV[bmp.Width, bmp.Height];
            imghsvnew = new PixelClassHSV[bmp.Width, bmp.Height];
            imgcmyk = new PixelClassCMYK[bmp.Width, bmp.Height];
            imgyuv = new PixelClassYUV[bmp.Width, bmp.Height];
            hist1 = new Histogram();
            hist2 = new Histogram();
            //nolasām datus no attēla
            var bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, bmp.PixelFormat);
            //nolasām atmiņā datus par attēlu
            IntPtr ptr = IntPtr.Zero; //mēģinām nolasīt rindu
            int pixelComponents; //kanālu skaits
            if (bmpData.PixelFormat == PixelFormat.Format24bppRgb) //ja ir 24 bitu formāts
            {
                pixelComponents = 3; //kanālu skaits
            }
            else if (bmpData.PixelFormat == PixelFormat.Format32bppRgb) //ja ir 32 bitu formāts
            {
                pixelComponents = 4;
            }
            else pixelComponents = 0;
            var line = new byte[bmp.Width * pixelComponents]; //the length of row array we scan from image
            for (int y = 0; y < bmpData.Height; y++)
            {
                ptr = bmpData.Scan0 + y * bmpData.Stride;
                //nolasām no pirmā pixeļa un stride-pixeļu rinas platums
                Marshal.Copy(ptr, line, 0, line.Length);
                for (int x = 0; x < bmpData.Width; x++)
                {
                    img[x, y] = new PixelClassRGB(line[pixelComponents * x + 2], line[pixelComponents * x + 1], line[pixelComponents * x]); //BGR
                    imgnew[x, y] = new PixelClassRGB(line[pixelComponents * x + 2], line[pixelComponents * x + 1], line[pixelComponents * x]); //BGR
                    imghsv[x, y] = new PixelClassHSV(img[x, y].R, img[x, y].G, img[x, y].B);
                    imghsvnew[x, y] = new PixelClassHSV(img[x, y].R, img[x, y].G, img[x, y].B);
                    imgcmyk[x, y] = new PixelClassCMYK(img[x, y].R, img[x, y].G, img[x, y].B);
                    imgyuv[x, y] = new PixelClassYUV(img[x, y].R, img[x, y].G, img[x, y].B);
                }
            }
            bmp.UnlockBits(bmpData); //nolasīšanas rezultāts
            hist1.readHistogramHSV(img, imghsv);
            hist2.readHistogramHSV(imgnew, imghsv);
            watchread.Stop();
            var elapsedMs = watchread.ElapsedMilliseconds;
            Console.WriteLine("Image Read time: " + elapsedMs);
        }

        public void filterImage(Filter f)
        {
            if (img != null)
            {
                for (int x = 1; x < img.GetLength(0) - 1; x++)
                {
                    for (int y = 1; y < img.GetLength(1) - 1; y++)
                    {
                        int r = 0;
                        int g = 0;
                        int b = 0;
                        int i = 0;

                        for (int fi = 0; fi < 3; fi++)
                        {
                            for (int fj = 0; fj < 3; fj++)
                            { //attēla pikseļu reizināšana ar filtra elementiem
                                r += img[x + fi - 1, y + fj - 1].R * f.F[fi, fj];
                                g += img[x + fi - 1, y + fj - 1].G * f.F[fi, fj];
                                b += img[x + fi - 1, y + fj - 1].B * f.F[fi, fj];
                                i += img[x + fi - 1, y + fj - 1].I * f.F[fi, fj];
                            }
                        }

                        // izkaitļojam koeficientus katram kanālam
                        r = Math.Max(0, Math.Min(255, r /= f.K));
                        g = Math.Max(0, Math.Min(255, g /= f.K));
                        b = Math.Max(0, Math.Min(255, b /= f.K));
                        i = Math.Max(0, Math.Min(255, i /= f.K));

                        //piešķīram jaunas vērtības
                        imgnew[x, y].R = (byte)r;
                        imgnew[x, y].G = (byte)g;
                        imgnew[x, y].B = (byte)b;
                        imgnew[x, y].I = (byte)i;
                    }
                }
            }
        }

        public void histogramSegmentation(bool isAutomatic, int T, int T2, string colormode, string thresholdmode)
        {
            if (img != null)
            {
                if (isAutomatic)
                    T = hist2.calculateAutomaticThreshold(hist2.hI);

                //izskaitļojam sliekšņa vērtību adaptīvi
                //izskrienam cauri visiem attēla pikseļiem
                for (int x = 1; x < img.GetLength(0) - 1; x++)
                {
                    for (int y = 1; y < img.GetLength(1) - 1; y++)
                    {
                        switch (colormode)
                        {
                            case "StretchI":
                                {
                                    switch (thresholdmode)
                                    {
                                        case "Vertical":
                                            {
                                                //ja intensitāte mazāka par slieksni - objekts
                                                if (img[x, y].I <= T) { imgnew[x, y].I = 0; }
                                                //fons
                                                else { imgnew[x, y].I = 255; }
                                                break;
                                            }
                                        case "Horizontal":
                                            {
                                                if (hist1.hI[img[x, y].I] < T) { imgnew[x, y].I = 0; }
                                                else { imgnew[x, y].I = 255; }
                                                break;
                                            }
                                        case "Two":
                                            {
                                                if (img[x, y].I <= T) { imgnew[x, y].I = 0; }
                                                else if (img[x, y].I > T && img[x, y].I <= T2) { imgnew[x, y].I = 128; }
                                                else { imgnew[x, y].I = 255; }
                                                break;
                                            }
                                    }
                                    break;
                                }
                            case "StretchR":
                                {
                                    switch (thresholdmode)
                                    {
                                        case "Vertical":
                                            {
                                                //ja intensitāte mazāka par slieksni - objekts
                                                if (img[x, y].R <= T) { imgnew[x, y].R = 0; }
                                                //fons
                                                else { imgnew[x, y].R = 255; }
                                                break;
                                            }
                                        case "Horizontal":
                                            {
                                                if (hist1.hR[img[x, y].R] < T) { imgnew[x, y].R = 0; }
                                                else { imgnew[x, y].R = 255; }
                                                break;
                                            }
                                        case "Two":
                                            {
                                                if (img[x, y].R <= T) { imgnew[x, y].R = 0; }
                                                else if (img[x, y].R > T && img[x, y].R <= T2) { imgnew[x, y].R = 128; }
                                                else { imgnew[x, y].R = 255; }
                                                break;
                                            }
                                    }
                                    break;
                                }
                            case "StretchG":
                                {
                                    switch (thresholdmode)
                                    {
                                        case "Vertical":
                                            {
                                                //ja intensitāte mazāka par slieksni - objekts
                                                if (img[x, y].G <= T) { imgnew[x, y].G = 0; }
                                                //fons
                                                else { imgnew[x, y].G = 255; }
                                                break;
                                            }
                                        case "Horizontal":
                                            {
                                                if (hist1.hG[img[x, y].G] < T) { imgnew[x, y].G = 0; }
                                                else { imgnew[x, y].G = 255; }
                                                break;
                                            }
                                        case "Two":
                                            {
                                                if (img[x, y].G <= T) { imgnew[x, y].G = 0; }
                                                else if (img[x, y].G > T && img[x, y].G <= T2) { imgnew[x, y].G = 128; }
                                                else { imgnew[x, y].G = 255; }
                                                break;
                                            }
                                    }
                                    break;
                                }
                            case "StretchB":
                                {
                                    switch (thresholdmode)
                                    {
                                        case "Vertical":
                                            {
                                                //ja intensitāte mazāka par slieksni - objekts
                                                if (img[x, y].B <= T) { imgnew[x, y].B = 0; }
                                                //fons
                                                else { imgnew[x, y].B = 255; }
                                                break;
                                            }
                                        case "Horizontal":
                                            {
                                                if (hist1.hB[img[x, y].B] < T) { imgnew[x, y].B = 0; }
                                                else { imgnew[x, y].B = 255; }
                                                break;
                                            }
                                        case "Two":
                                            {
                                                if (img[x, y].B <= T) { imgnew[x, y].B = 0; }
                                                else if (img[x, y].B > T && img[x, y].B <= T2) { imgnew[x, y].B = 128; }
                                                else { imgnew[x, y].B = 255; }
                                                break;
                                            }
                                    }
                                    break;
                                }
                        }
                    }
                }
            }
        }

        public void edgeSegmentation(Filter Fx, Filter Fy, string filter, string colormode)
        {
            if (img != null)
            {
                //izskrienam cauri attēlam
                for (int x = 1; x < img.GetLength(0) - 1; x++)
                {
                    for (int y = 1; y < img.GetLength(1) - 1; y++)
                    {
                        int Gx = 0;
                        int Gy = 0;
                        int Rx = 0;
                        int Ry = 0;
                        int Bx = 0;
                        int By = 0;
                        int G;
                        int B;
                        int R;

                        switch (filter)
                        {
                            case "Roberts":
                                {
                                    for (int fi = 0; fi < 2; fi++)
                                    {
                                        for (int fj = 0; fj < 2; fj++)
                                        {
                                            switch (colormode)
                                            {
                                                case "RGB":
                                                    {
                                                        Rx += img[x + fi - 1, y + fj - 1].R * Fx.Fr[fi, fj];
                                                        Ry += img[x + fi - 1, y + fj - 1].R * Fy.Fr[fi, fj];
                                                        Gx += img[x + fi - 1, y + fj - 1].G * Fx.Fr[fi, fj];
                                                        Gy += img[x + fi - 1, y + fj - 1].G * Fy.Fr[fi, fj];
                                                        Bx += img[x + fi - 1, y + fj - 1].B * Fx.Fr[fi, fj];
                                                        By += img[x + fi - 1, y + fj - 1].B * Fy.Fr[fi, fj];
                                                        break;
                                                    }
                                                case "Intensity":
                                                    {
                                                        Gx += img[x + fi - 1, y + fj - 1].I * Fx.Fr[fi, fj];
                                                        Gy += img[x + fi - 1, y + fj - 1].I * Fy.Fr[fi, fj];
                                                        break;
                                                    }
                                            }
                                        }
                                    }
                                    break;
                                }
                            default:
                                {
                                    for (int fi = 0; fi < 3; fi++)
                                    {
                                        for (int fj = 0; fj < 3; fj++)
                                        {
                                            switch (colormode)
                                            {
                                                case "RGB":
                                                    {
                                                        Rx += img[x + fi - 1, y + fj - 1].R * Fx.F[fi, fj];
                                                        Ry += img[x + fi - 1, y + fj - 1].R * Fy.F[fi, fj];
                                                        Gx += img[x + fi - 1, y + fj - 1].G * Fx.F[fi, fj];
                                                        Gy += img[x + fi - 1, y + fj - 1].G * Fy.F[fi, fj];
                                                        Bx += img[x + fi - 1, y + fj - 1].B * Fx.F[fi, fj];
                                                        By += img[x + fi - 1, y + fj - 1].B * Fy.F[fi, fj];
                                                        break;
                                                    }
                                                case "Intensity":
                                                    {
                                                        Gx += img[x + fi - 1, y + fj - 1].I * Fx.F[fi, fj];
                                                        Gy += img[x + fi - 1, y + fj - 1].I * Fy.F[fi, fj];
                                                        break;
                                                    }
                                            }
                                        }
                                    }
                                    break;
                                }
                        }

                        switch (colormode)
                        {
                            case "RGB":
                                {
                                    R = Convert.ToInt32(Math.Sqrt(Rx * Rx + Ry * Ry));
                                    G = Convert.ToInt32(Math.Sqrt(Gx * Gx + Gy * Gy));
                                    B = Convert.ToInt32(Math.Sqrt(Bx * Bx + By * By));
                                    if (R < 128) { imgnew[x, y].R = 0; }
                                    else { imgnew[x, y].R = 255; }
                                    if (G < 128) { imgnew[x, y].G = 0; }
                                    else { imgnew[x, y].G = 255; }
                                    if (B < 128) { imgnew[x, y].B = 0; }
                                    else { imgnew[x, y].B = 255; }
                                    break;
                                }
                            case "Intensity":
                                {
                                    G = Convert.ToInt32(Math.Sqrt(Gx * Gx + Gy * Gy));
                                    if (G < 128) { imgnew[x, y].I = 0; }
                                    else { imgnew[x, y].I = 255; }
                                    break;
                                }
                        }
                    }
                }
            }
        }

        public void contrastByHistogram(string mode, int value, bool isStretch)
        {
            int[] hRGBI = new int[257];
            if (mode == "R") { hRGBI = hist2.hR; }
            else if (mode == "G") { hRGBI = hist2.hG; }
            else if (mode == "B") { hRGBI = hist2.hB; }
            else if (mode == "I") { hRGBI = hist2.hI; }
            else if (mode == "S") { hRGBI = hist2.hS; }
            else if (mode == "V") { hRGBI = hist2.hV; }
            int dBegin, dEnd;
            if (isStretch)
            {
                dBegin = hist2.FindFirst(hRGBI, 0);
                dEnd = hist2.FindLast(hRGBI, 0);
            }
            else
            {
                dBegin = hist2.FindFirst(hRGBI, value);
                dEnd = hist2.FindLast(hRGBI, value);
            }

            int dOriginal = dEnd - dBegin;
            int dDesired = 255;
            double k = dDesired / (double)dOriginal;
            for (int x = 0; x < imgnew.GetLength(0); x++)
            {
                for (int y = 0; y < imgnew.GetLength(1); y++)
                {
                    if (mode == "I")
                    {
                        imgnew[x, y].I = (byte)Math.Min(255, Math.Max(0, k * (img[x, y].I - dBegin)));
                    }
                    else if (mode == "R")
                    {
                        imgnew[x, y].R = (byte)Math.Min(255, Math.Max(0, k * (img[x, y].R - dBegin)));
                    }
                    else if (mode == "G")
                    {
                        imgnew[x, y].G = (byte)Math.Min(255, Math.Max(0, k * (img[x, y].G - dBegin)));
                    }
                    else if (mode == "B")
                    {
                        imgnew[x, y].B = (byte)Math.Min(255, Math.Max(0, k * (img[x, y].B - dBegin)));
                    }
                    else if (mode == "S")
                    {
                        imghsvnew[x, y].S = (byte)Math.Min(255, Math.Max(0, k * (imghsv[x, y].S - dBegin)));
                    }
                    else if (mode == "V")
                    {
                        imghsvnew[x, y].V = (byte)Math.Min(255, Math.Max(0, k * (imghsv[x, y].V - dBegin)));
                    }
                }
            }

        }

        public Bitmap drawImage(string mode)
        {
            var watchdraw = System.Diagnostics.Stopwatch.StartNew();
            if (img != null)
            {
                IntPtr ptr = IntPtr.Zero;
                int Height = img.GetLength(1);
                int Width = img.GetLength(0);
                var bmp = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);
                var bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.WriteOnly, bmp.PixelFormat);
                var line = new byte[bmp.Width * 3]; //3 kanāli
                for (int y = 0; y < bmpData.Height; y++)
                {
                    for (int x = 0; x < bmpData.Width; x++)
                    {
                        switch (mode)
                        {
                            case "RGB":
                                {
                                    line[3 * x] = img[x, y].B; //blue
                                    line[3 * x + 1] = img[x, y].G; //green
                                    line[3 * x + 2] = img[x, y].R; //red
                                    imgnew[x, y].R = line[3 * x + 2];
                                    imgnew[x, y].G = line[3 * x + 1];
                                    imgnew[x, y].B = line[3 * x];
                                    imgnew[x, y].I = Convert.ToByte(0.0722f * imgnew[x, y].B + 0.7152f * imgnew[x, y].G + 0.2126f * imgnew[x, y].R);
                                    break;
                                } //rgb
                            case "R":
                                {
                                    line[3 * x] = 0; //blue
                                    line[3 * x + 1] = 0; //green
                                    line[3 * x + 2] = img[x, y].R; //red
                                    imgnew[x, y].R = line[3 * x + 2];
                                    imgnew[x, y].G = line[3 * x + 1];
                                    imgnew[x, y].B = line[3 * x];
                                    break;
                                } //red
                            case "G":
                                {
                                    line[3 * x] = 0; //blue
                                    line[3 * x + 1] = img[x, y].G; //green
                                    line[3 * x + 2] = 0; //red
                                    imgnew[x, y].R = line[3 * x + 2];
                                    imgnew[x, y].G = line[3 * x + 1];
                                    imgnew[x, y].B = line[3 * x];
                                    break;
                                } //green
                            case "B":
                                {
                                    line[3 * x] = img[x, y].B; //blue
                                    line[3 * x + 1] = 0; //green
                                    line[3 * x + 2] = 0; //red
                                    imgnew[x, y].R = line[3 * x + 2];
                                    imgnew[x, y].G = line[3 * x + 1];
                                    imgnew[x, y].B = line[3 * x];
                                    break;
                                } //blue
                            case "I":
                                {
                                    line[3 * x] = img[x, y].I; //blue
                                    line[3 * x + 1] = img[x, y].I; //green
                                    line[3 * x + 2] = img[x, y].I; //red
                                    imgnew[x, y].R = line[3 * x + 2];
                                    imgnew[x, y].G = line[3 * x + 1];
                                    imgnew[x, y].B = line[3 * x];
                                    imgnew[x, y].I = Convert.ToByte(0.0722f * imgnew[x, y].B + 0.7152f * imgnew[x, y].G + 0.2126f * imgnew[x, y].R);
                                    break;
                                } //grayscale
                            case "Transition":
                                {
                                    line[3 * x + 2] = img[x, y].R;
                                    line[3 * x + 1] = img[x, y].G;
                                    line[3 * x] = img[x, y].B;
                                    break;
                                }
                            case "StretchRGB":
                                {
                                    line[3 * x] = imgnew[x, y].B; //blue
                                    line[3 * x + 1] = imgnew[x, y].G; //green
                                    line[3 * x + 2] = imgnew[x, y].R; //red
                                    imgnew[x, y].I = Convert.ToByte(0.0722f * imgnew[x, y].B + 0.7152f * imgnew[x, y].G + 0.2126f * imgnew[x, y].R);
                                    break;
                                } //rgb
                            case "StretchR":
                                {
                                    line[3 * x] = 0; //blue
                                    line[3 * x + 1] = 0; //green
                                    line[3 * x + 2] = imgnew[x, y].R; //red
                                    break;
                                }
                            case "StretchG":
                                {
                                    line[3 * x] = 0; //blue
                                    line[3 * x + 1] = imgnew[x, y].G; //green
                                    line[3 * x + 2] = 0; //red
                                    break;
                                }
                            case "StretchB":
                                {
                                    line[3 * x] = imgnew[x, y].B; //blue
                                    line[3 * x + 1] = 0; //green
                                    line[3 * x + 2] = 0; //red
                                    break;
                                }
                            case "StretchI":
                                {
                                    line[3 * x] = imgnew[x, y].I; //blue
                                    line[3 * x + 1] = imgnew[x, y].I; //green
                                    line[3 * x + 2] = imgnew[x, y].I; //red
                                    break;
                                }
                            case "StretchHSV":
                                {
                                    line[3 * x] = img[x, y].hsvToRGB(imghsv[x, y].H, imghsvnew[x, y].S, imghsvnew[x, y].V).B; //blue
                                    line[3 * x + 1] = img[x, y].hsvToRGB(imghsv[x, y].H, imghsvnew[x, y].S, imghsvnew[x, y].V).G; //green
                                    line[3 * x + 2] = img[x, y].hsvToRGB(imghsv[x, y].H, imghsvnew[x, y].S, imghsvnew[x, y].V).R; //red
                                    break;
                                }
                            case "StretchS":
                                {
                                    line[3 * x] = imghsvnew[x, y].S; //blue
                                    line[3 * x + 1] = imghsvnew[x, y].S; //green
                                    line[3 * x + 2] = imghsvnew[x, y].S; //red
                                    break;
                                }
                            case "StretchV":
                                {
                                    line[3 * x] = imghsvnew[x, y].V; //blue
                                    line[3 * x + 1] = imghsvnew[x, y].V; //green
                                    line[3 * x + 2] = imghsvnew[x, y].V; //red
                                    break;
                                }
                            case "Invert":
                                {
                                    line[3 * x] = Convert.ToByte(255 - img[x, y].B); //blue
                                    line[3 * x + 1] = Convert.ToByte(255 - img[x, y].G); //green
                                    line[3 * x + 2] = Convert.ToByte(255 - img[x, y].R); //red
                                    break;
                                } //inverted
                            case "HSV":
                                {
                                    line[3 * x] = img[x, y].hsvToRGB(imghsv[x, y].H, imghsv[x, y].S, imghsv[x, y].V).B; //blue
                                    line[3 * x + 1] = img[x, y].hsvToRGB(imghsv[x, y].H, imghsv[x, y].S, imghsv[x, y].V).G; //green
                                    line[3 * x + 2] = img[x, y].hsvToRGB(imghsv[x, y].H, imghsv[x, y].S, imghsv[x, y].V).R; //red
                                    break;
                                } //hue saturation value
                            case "H":
                                {
                                    line[3 * x] = img[x, y].hsvToRGB(imghsv[x, y].H, 255, 255).B; //blue
                                    line[3 * x + 1] = img[x, y].hsvToRGB(imghsv[x, y].H, 255, 255).G; //green
                                    line[3 * x + 2] = img[x, y].hsvToRGB(imghsv[x, y].H, 255, 255).R; //red
                                    break;
                                } //hue
                            case "S":
                                {
                                    line[3 * x] = imghsv[x, y].S; //blue
                                    line[3 * x + 1] = imghsv[x, y].S; //green
                                    line[3 * x + 2] = imghsv[x, y].S; //red
                                    break;
                                } //saturation
                            case "V":
                                {
                                    line[3 * x] = imghsv[x, y].V;
                                    line[3 * x + 1] = imghsv[x, y].V;
                                    line[3 * x + 2] = imghsv[x, y].V;
                                    break;
                                } //value
                            case "CMYK":
                                {
                                    line[3 * x] = img[x, y].cmykToRGB(imgcmyk[x, y].C, imgcmyk[x, y].M, imgcmyk[x, y].Y, imgcmyk[x, y].K).B; //blue
                                    line[3 * x + 1] = img[x, y].cmykToRGB(imgcmyk[x, y].C, imgcmyk[x, y].M, imgcmyk[x, y].Y, imgcmyk[x, y].K).G; //green
                                    line[3 * x + 2] = img[x, y].cmykToRGB(imgcmyk[x, y].C, imgcmyk[x, y].M, imgcmyk[x, y].Y, imgcmyk[x, y].K).R; //red
                                    break;
                                }//cmyk
                            case "C":
                                {
                                    line[3 * x] = img[x, y].cmykToRGB(imgcmyk[x, y].C, 0, 0, 0).B; //blue
                                    line[3 * x + 1] = img[x, y].cmykToRGB(imgcmyk[x, y].C, 0, 0, 0).G; //green
                                    line[3 * x + 2] = img[x, y].cmykToRGB(imgcmyk[x, y].C, 0, 0, 0).R; //red
                                    break;
                                }//cyan
                            case "M":
                                {
                                    line[3 * x] = img[x, y].cmykToRGB(0, imgcmyk[x, y].M, 0, 0).B; //blue
                                    line[3 * x + 1] = img[x, y].cmykToRGB(0, imgcmyk[x, y].M, 0, 0).G; //green
                                    line[3 * x + 2] = img[x, y].cmykToRGB(0, imgcmyk[x, y].M, 0, 0).R; //red
                                    break;
                                }//magenta
                            case "Y":
                                {
                                    line[3 * x] = img[x, y].cmykToRGB(0, 0, imgcmyk[x, y].Y, 0).B; //blue
                                    line[3 * x + 1] = img[x, y].cmykToRGB(0, 0, imgcmyk[x, y].Y, 0).G; //green
                                    line[3 * x + 2] = img[x, y].cmykToRGB(0, 0, imgcmyk[x, y].Y, 0).R; //red
                                    break;
                                }//yellow
                            case "K":
                                {
                                    line[3 * x] = img[x, y].cmykToRGB(0, 0, 0, imgcmyk[x, y].K).B; //blue
                                    line[3 * x + 1] = img[x, y].cmykToRGB(0, 0, 0, imgcmyk[x, y].K).G; //green
                                    line[3 * x + 2] = img[x, y].cmykToRGB(0, 0, 0, imgcmyk[x, y].K).R; //red
                                    break;
                                }//key
                            case "YUV":
                                {
                                    line[3 * x] = img[x, y].yuvToRGB(imgyuv[x, y].Yy, imgyuv[x, y].U, imgyuv[x, y].Vv).B; //blue
                                    line[3 * x + 1] = img[x, y].yuvToRGB(imgyuv[x, y].Yy, imgyuv[x, y].U, imgyuv[x, y].Vv).G; //green
                                    line[3 * x + 2] = img[x, y].yuvToRGB(imgyuv[x, y].Yy, imgyuv[x, y].U, imgyuv[x, y].Vv).R; //red
                                    break;
                                }//yuv
                            case "Yy":
                                {
                                    line[3 * x] = img[x, y].yuvToRGB(imgyuv[x, y].Yy, 128, 128).B; //blue
                                    line[3 * x + 1] = img[x, y].yuvToRGB(imgyuv[x, y].Yy, 128, 128).G; //green
                                    line[3 * x + 2] = img[x, y].yuvToRGB(imgyuv[x, y].Yy, 128, 128).R; //red
                                    break;
                                }
                            case "U":
                                {
                                    line[3 * x] = img[x, y].yuvToRGB(128, imgyuv[x, y].U, 128).B; //blue
                                    line[3 * x + 1] = img[x, y].yuvToRGB(128, imgyuv[x, y].U, 128).G; //green
                                    line[3 * x + 2] = img[x, y].yuvToRGB(128, imgyuv[x, y].U, 128).R; //red
                                    break;
                                }
                            case "Vv":
                                {
                                    line[3 * x] = img[x, y].yuvToRGB(128, 128, imgyuv[x, y].Vv).B; //blue
                                    line[3 * x + 1] = img[x, y].yuvToRGB(128, 128, imgyuv[x, y].Vv).G; //green
                                    line[3 * x + 2] = img[x, y].yuvToRGB(128, 128, imgyuv[x, y].Vv).R; //red
                                    break;
                                }
                        } //switch
                    }
                    ptr = bmpData.Scan0 + y * bmpData.Stride;
                    Marshal.Copy(line, 0, ptr, line.Length);
                }
                bmp.UnlockBits(bmpData);
                //hist2.readHistogramHSV(imgnew, imghsv);
                watchdraw.Stop();
                var elapsedMs = watchdraw.ElapsedMilliseconds;
                Console.WriteLine("Image draw time " + elapsedMs);
                return bmp;
            }
            else
            {
                watchdraw.Stop();
                var elapsedMs = watchdraw.ElapsedMilliseconds;
                Console.WriteLine("Image draw time " + elapsedMs);
                return null;
            }
        }
    }
}

