using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms.DataVisualization.Charting;

namespace IKAA_171rdb115_2
{
    public class Histogram
    {
        public int[] hR; //red
        public int[] hG; //green
        public int[] hB; //blue
        public int[] hI; //intensity
        public int[] hHr;
        public int[] hHg;
        public int[] hHb;
        public int[] hS;
        public int[] hV;

        public Histogram()
        {
            hR = new int[257];
            hG = new int[257];
            hB = new int[257];
            hI = new int[257];
            hHr = new int[257];
            hHg = new int[257];
            hHb = new int[257];
            hS = new int[257];
            hV = new int[257];
        }

        public int FindFirst(int [] H, int x)
        { //meklējam histogrammas sākumu
            int i = 0;
            while (H[i] <= x) { i++; }
            return i;
        }

        public int FindLast(int [] H, int x)
        { //meklējam histogrammas galapunktu
            int i = 255;
            while (H[i] <=x) { i--; }
            return i;
        }

        public void eraseHistogram()
        {
            for (int i = 0; i < 256; i++)
            {
                hR[i] = 0;
                hG[i] = 0;
                hB[i] = 0;
                hI[i] = 0;
                hHr[i] = 0;
                hHg[i] = 0;
                hHb[i] = 0;
                hS[i] = 0;
                hV[i] = 0;
            }
        }

        public void readHistogram(PixelClassRGB[,] imgArray, PixelClassHSV[,] imgArrayHSV)
        {
            eraseHistogram();
            for (int x = 0; x < imgArray.GetLength(0); x++)
            {
                for (int y = 0; y < imgArray.GetLength(1); y++)
                {
                    hR[imgArray[x, y].R]++;
                    hG[imgArray[x, y].G]++;
                    hB[imgArray[x, y].B]++;
                    hI[imgArray[x, y].I]++;
                    hHr[imgArray[x, y].hsvToRGB(imgArrayHSV[x, y].H, 255, 255).R]++;
                    hHg[imgArray[x, y].hsvToRGB(imgArrayHSV[x, y].H, 255, 255).G]++;
                    hHb[imgArray[x, y].hsvToRGB(imgArrayHSV[x, y].H, 255, 255).B]++;
                    hS[imgArrayHSV[x, y].S]++;
                    hV[imgArrayHSV[x, y].V]++;
                }
            }

            for (int i = 0; i < 256; i++)
            {
                hR[256] = Math.Max(hR[i], hR[256]);
                hG[256] = Math.Max(hG[i], hG[256]);
                hB[256] = Math.Max(hB[i], hB[256]);
                hI[256] = Math.Max(hI[i], hI[256]);
                hHr[256] = Math.Max(hHr[i], hHr[256]);
                hHg[256] = Math.Max(hHg[i], hHg[256]);
                hHb[256] = Math.Max(hHb[i], hHb[256]);
            }
        }

        public void drawHistogram(Chart chart, string Channels)
        {
            chart.Series.Clear();
            chart.ChartAreas.Clear();
            chart.ChartAreas.Add("ChartArea");
            chart.ChartAreas["ChartArea"].BackColor = Color.Transparent;

            switch (Channels)
            {
                case "RGB":
                    {
                        chart.Series.Add("R");
                        chart.Series["R"].Color = Color.Red;
                        chart.Series.Add("G");
                        chart.Series["G"].Color = Color.Green;
                        chart.Series.Add("B");
                        chart.Series["B"].Color = Color.Blue;
                        chart.Series.Add("I");
                        chart.Series["I"].Color = Color.Gray;
                        chart.ChartAreas["ChartArea"].AxisX.Maximum = 255;
                        chart.ChartAreas["ChartArea"].AxisX.Minimum = 0;
                        for (int i = 0; i < 256; i++)
                        {
                            chart.Series["R"].Points.AddXY(i, hR[i]);
                            chart.Series["G"].Points.AddXY(i, hG[i]);
                            chart.Series["B"].Points.AddXY(i, hB[i]);
                            chart.Series["I"].Points.AddXY(i, hI[i]);
                        }
                        break;
                    }
                case "R":
                    {
                        chart.Series.Add("R");
                        chart.Series["R"].Color = Color.Red;
                        chart.ChartAreas["ChartArea"].AxisX.Maximum = 255;
                        chart.ChartAreas["ChartArea"].AxisX.Minimum = 0;
                        for (int i = 0; i < 256; i++)
                        {
                            chart.Series["R"].Points.AddXY(i, hR[i]);
                        }
                        break;
                    }
                case "G":
                    {
                        chart.Series.Add("G");
                        chart.Series["G"].Color = Color.Green;
                        chart.ChartAreas["ChartArea"].AxisX.Maximum = 255;
                        chart.ChartAreas["ChartArea"].AxisX.Minimum = 0;
                        for (int i = 0; i < 256; i++)
                        {
                            chart.Series["G"].Points.AddXY(i, hG[i]);
                        }
                        break;
                    }
                case "B":
                    {
                        chart.Series.Add("B");
                        chart.Series["B"].Color = Color.Blue;
                        chart.ChartAreas["ChartArea"].AxisX.Maximum = 255;
                        chart.ChartAreas["ChartArea"].AxisX.Minimum = 0;
                        for (int i = 0; i < 256; i++)
                        {
                            chart.Series["B"].Points.AddXY(i, hB[i]);
                        }
                        break;
                    }
                case "I":
                    {
                        chart.Series.Add("I");
                        chart.Series["I"].Color = Color.Gray;
                        chart.ChartAreas["ChartArea"].AxisX.Maximum = 255;
                        chart.ChartAreas["ChartArea"].AxisX.Minimum = 0;
                        for (int i = 0; i < 256; i++)
                        {
                            chart.Series["I"].Points.AddXY(i, hI[i]);
                        }
                        break;
                    }
                case "HSV":
                    {
                        chart.Series.Add("H");
                        chart.Series["H"].Color = Color.Red;
                        chart.Series.Add("S");
                        chart.Series["S"].Color = Color.Orange;
                        chart.Series.Add("V");
                        chart.Series["V"].Color = Color.Gray;
                        chart.ChartAreas["ChartArea"].AxisX.Maximum = 255;
                        chart.ChartAreas["ChartArea"].AxisX.Minimum = 0;
                        for (int i = 0; i < 256; i++)
                        {
                            chart.Series["H"].Points.AddXY(i, hHr[i]);
                            chart.Series["H"].Points.AddXY(i, hHg[i]);
                            chart.Series["H"].Points.AddXY(i, hHb[i]);
                            chart.Series["H"].Points.AddXY(i, hS[i]);
                            chart.Series["H"].Points.AddXY(i, hV[i]);
                        }
                        break;
                    }
                case "H":
                    {
                        chart.Series.Add("H");
                        chart.Series["H"].Color = Color.Red;
                        chart.ChartAreas["ChartArea"].AxisX.Maximum = 255;
                        chart.ChartAreas["ChartArea"].AxisX.Minimum = 0;
                        for (int i = 0; i < 256; i++)
                        {
                            chart.Series["H"].Points.AddXY(i, hHr[i]);
                            chart.Series["H"].Points.AddXY(i, hHg[i]);
                            chart.Series["H"].Points.AddXY(i, hHb[i]);
                        }
                        break;
                    }
                case "S":
                    {
                        chart.Series.Add("S");
                        chart.Series["S"].Color = Color.Orange;
                        chart.ChartAreas["ChartArea"].AxisX.Maximum = 255;
                        chart.ChartAreas["ChartArea"].AxisX.Minimum = 0;
                        for (int i = 0; i < 256; i++)
                        {
                            chart.Series["S"].Points.AddXY(i, hS[i]);
                        }
                        break;
                    }
                case "V":
                    {
                        chart.Series.Add("V");
                        chart.Series["V"].Color = Color.Gray;
                        chart.ChartAreas["ChartArea"].AxisX.Maximum = 255;
                        chart.ChartAreas["ChartArea"].AxisX.Minimum = 0;
                        for (int i = 0; i < 256; i++)
                        {
                            chart.Series["V"].Points.AddXY(i, hV[i]);
                        }
                        break;
                    }
            }

        }
    }
}
