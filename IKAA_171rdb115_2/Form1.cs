using System;
using System.Drawing;
using System.Windows.Forms;

namespace IKAA_171rdb115_2
{
    public partial class Form1 : Form
    {
        public imgData imgData = new imgData();
        private string thresholdmode = "";
        private string colormode = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "JPG Files|*.jpg";
            dialog.Title = "Please select an image";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Bitmap.FromFile(dialog.FileName);
                Bitmap bmp = (Bitmap)pictureBox1.Image.Clone();
                imgData.readImage(bmp);
                pictureBox2.Image = imgData.drawImage("RGB");
                imgData.hist2.drawHistogram(chart1, "RGB");
                imgData.hist2.drawHistogram(chart2, "RGB");
            }
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                Bitmap bmpi = pictureBox1.Image as Bitmap;
                double kX = (double)pictureBox1.Image.Width / pictureBox1.Width;
                double kY = (double)pictureBox1.Image.Height / pictureBox1.Height;
                double k = Math.Max(kX, kY);
                //centrējam attēlu pēc pictureBox izmēra
                double nobideX = (pictureBox1.Width * k - pictureBox1.Image.Width) / 2;
                double nobideY = (pictureBox1.Height * k - pictureBox1.Image.Height) / 2;
                //zīmējam attēlu mērogojot pēc pictureBox
                double kx = Math.Round(e.X * k - nobideX);
                double ky = Math.Round(e.Y * k - nobideY);
                try
                {
                    bmpi.SetPixel(Convert.ToInt32(kx), Convert.ToInt32(ky), colorDialog1.Color);
                    pictureBox1.Refresh();
                }
                catch (Exception) { label5.Text = "Can't color pixel outside image"; }
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                Bitmap bmpo = pictureBox1.Image as Bitmap;
                double kX = (double)pictureBox1.Image.Width / pictureBox1.Width;
                double kY = (double)pictureBox1.Image.Height / pictureBox1.Height;
                double k = Math.Max(kX, kY);
                //centrējam attēlu pēc pictureBox izmēra
                double nobideX = (pictureBox1.Width * k - pictureBox1.Image.Width) / 2;
                double nobideY = (pictureBox1.Height * k - pictureBox1.Image.Height) / 2;
                //zīmējam attēlu mērogojot pēc pictureBox
                double kx = Math.Round(e.X * k - nobideX);
                double ky = Math.Round(e.Y * k - nobideY);
                //izvadam label teksta laukā konvērtētu vērtību no vesela skaitļa uz tekstu
                try
                {
                    Color colororg = bmpo.GetPixel(Convert.ToInt32(kx), Convert.ToInt32(ky));
                    PixelClassHSV hsvPixel = new PixelClassHSV(colororg.R, colororg.G, colororg.B);
                    PixelClassCMYK cmykPixel = new PixelClassCMYK(colororg.R, colororg.G, colororg.B);
                    PixelClassYUV yuvPixel = new PixelClassYUV(colororg.R, colororg.G, colororg.B);
                    label1.Text = "RGB \nR = " + colororg.R + ", G = " + colororg.G + ", B = " + colororg.B;
                    label2.Text = "RGB (inversed) \nR = " + (255 - colororg.R) + ", G = " + (255 - colororg.G) + ", B = " + (255 - colororg.B);
                    label3.Text = "HSV \nH = " + hsvPixel.H + ", S = " + hsvPixel.S + "%, V = " + hsvPixel.V + "%";
                    label4.Text = "CMYK \nC = " + Convert.ToInt32(cmykPixel.C * 100) + "%, M = " + Convert.ToInt32(cmykPixel.M * 100)
                        + "%, Y = " + Convert.ToInt32(cmykPixel.Y * 100) + "%, K = " + Convert.ToInt32(cmykPixel.K * 100) + "%";
                    label5.Text = "x, y = " + Convert.ToString(kx) + "," + Convert.ToString(ky);
                    label6.Text = "YUV \nY = " + Convert.ToInt32(yuvPixel.Yy) + ", U = " + Convert.ToInt32(yuvPixel.U) + ", V = " + Convert.ToInt32(yuvPixel.Vv);
                }
                catch (Exception) { label5.Text = "Can't read coordinates outside image"; }


            }
        }

        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            if (pictureBox2.Image != null)
            {
                Bitmap bmpi = pictureBox2.Image as Bitmap;
                double kX = (double)pictureBox2.Image.Width / pictureBox2.Width;
                double kY = (double)pictureBox2.Image.Height / pictureBox2.Height;
                double k = Math.Max(kX, kY);
                //centrējam attēlu pēc pictureBox izmēra
                double nobideX = (pictureBox2.Width * k - pictureBox2.Image.Width) / 2;
                double nobideY = (pictureBox2.Height * k - pictureBox2.Image.Height) / 2;
                //zīmējam attēlu mērogojot pēc pictureBox
                double kx = Math.Round(e.X * k - nobideX);
                double ky = Math.Round(e.Y * k - nobideY);
                try
                {
                    bmpi.SetPixel(Convert.ToInt32(kx), Convert.ToInt32(ky), colorDialog1.Color);
                    pictureBox2.Refresh();
                }
                catch (Exception) { label5.Text = "Can't color pixel outside image"; }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        { //RGB
            radioButton3.Checked = true; //Composite
            radioButton4.Text = "Red";
            radioButton5.Text = "Green";
            radioButton6.Text = "Blue";
            radioButton7.Text = "Intensity";
            radioButton7.Visible = true; //Intensity
            if (imgData.img != null)
            {
                pictureBox2.Image = imgData.drawImage("RGB");
                imgData.hist2.readHistogramHSV(imgData.img, imgData.imghsv);
                imgData.hist2.drawHistogram(chart2, "RGB");
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        { //HSV
            radioButton3.Checked = true; //Composite
            radioButton4.Text = "Hue";
            radioButton5.Text = "Saturation";
            radioButton6.Text = "Value";
            radioButton7.Visible = false; //Intensity
            if (imgData.img != null)
            {
                pictureBox2.Image = imgData.drawImage("HSV");
                imgData.hist2.readHistogramHSV(imgData.img, imgData.imghsv);
                imgData.hist2.drawHistogram(chart2, "HSV");
            }
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {   //CMYK
            radioButton3.Checked = true; //Composite
            radioButton4.Text = "Cyan";
            radioButton5.Text = "Magenta";
            radioButton6.Text = "Yellow";
            radioButton7.Text = "Key";
            radioButton7.Visible = true;
            if (imgData.img != null)
            {
                pictureBox2.Image = imgData.drawImage("CMYK");
            }
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            radioButton3.Checked = true; //Composite
            radioButton4.Text = "Luminance (Y)";
            radioButton5.Text = "Blue-luminance (U)";
            radioButton6.Text = "Red-luminance (V)";
            radioButton7.Visible = false;
            if (imgData.img != null)
            {
                pictureBox2.Image = imgData.drawImage("YUV");
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (imgData.img != null)
            {
                if (radioButton1.Checked)
                {
                    pictureBox2.Image = imgData.drawImage("RGB");
                    imgData.hist2.readHistogramRGB(imgData.img);
                    imgData.hist2.drawHistogram(chart2, "RGB");
                }
                else if (radioButton2.Checked)
                {
                    pictureBox2.Image = imgData.drawImage("HSV");
                    imgData.hist2.readHistogramHSV(imgData.img, imgData.imghsv);
                    imgData.hist2.drawHistogram(chart2, "HSV");
                }
                else if (radioButton8.Checked)
                {
                    pictureBox2.Image = imgData.drawImage("CMYK");
                }
                else
                {
                    pictureBox2.Image = imgData.drawImage("YUV");
                }

            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (imgData.img != null)
            {
                if (radioButton1.Checked)
                {
                    pictureBox2.Image = imgData.drawImage("R");
                    imgData.hist2.readHistogramRGB(imgData.img);
                    imgData.hist2.drawHistogram(chart2, "R");
                }
                else if (radioButton2.Checked)
                {
                    pictureBox2.Image = imgData.drawImage("H");
                    imgData.hist2.readHistogramHSV(imgData.img, imgData.imghsv);
                    imgData.hist2.drawHistogram(chart2, "H");
                }
                else if (radioButton8.Checked)
                {
                    pictureBox2.Image = imgData.drawImage("C");
                }
                else
                {
                    pictureBox2.Image = imgData.drawImage("Yy");
                }
            }
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (imgData.img != null)
            {
                if (radioButton1.Checked)
                {
                    pictureBox2.Image = imgData.drawImage("G");
                    imgData.hist2.readHistogramRGB(imgData.img);
                    imgData.hist2.drawHistogram(chart2, "G");
                }
                else if (radioButton2.Checked)
                {
                    pictureBox2.Image = imgData.drawImage("S");
                    imgData.hist2.readHistogramHSV(imgData.img, imgData.imghsv);
                    imgData.hist2.drawHistogram(chart2, "S");
                }
                else if (radioButton8.Checked)
                {
                    pictureBox2.Image = imgData.drawImage("M");
                }
                else
                {
                    pictureBox2.Image = imgData.drawImage("U");
                }
            }
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            if (imgData.img != null)
            {
                if (radioButton1.Checked)
                {
                    pictureBox2.Image = imgData.drawImage("B");
                    imgData.hist2.readHistogramRGB(imgData.img);
                    imgData.hist2.drawHistogram(chart2, "B");
                }
                else if (radioButton2.Checked)
                {
                    pictureBox2.Image = imgData.drawImage("V");
                    imgData.hist2.readHistogramHSV(imgData.img, imgData.imghsv);
                    imgData.hist2.drawHistogram(chart2, "V");
                }
                else if (radioButton8.Checked)
                {
                    pictureBox2.Image = imgData.drawImage("Y");
                }
                else
                {
                    pictureBox2.Image = imgData.drawImage("Vv");
                }
            }
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            if (imgData.img != null)
            {
                if (radioButton1.Checked)
                {
                    pictureBox2.Image = imgData.drawImage("I");
                    imgData.hist2.readHistogramRGB(imgData.img);
                    imgData.hist2.drawHistogram(chart2, "I");
                }
                else
                {
                    pictureBox2.Image = imgData.drawImage("K");
                }
            }
        }

        private void invertButton_Click(object sender, EventArgs e)
        {

            Bitmap bmp = (Bitmap)pictureBox1.Image.Clone();
            imgData.readImage(bmp);
            pictureBox2.Image = imgData.drawImage("Invert");
        }

        private void colorButton_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            colorButton.BackColor = colorDialog1.Color;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                pictureBox2.Image.Save(saveFileDialog1.FileName);
        }

        private void stretchingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imgData.img != null)
            {
                bool isStretch = true;
                int value = 0;
                stretchHistrogram(isStretch, value);
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(trackBar1, trackBar1.Value.ToString() + "%");
        }

        private void normalizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imgData.img != null)
            {
                bool isStretch = false;
                int value = trackBar1.Value;
                stretchHistrogram(isStretch, value);
            }
        }

        private void stretchHistrogram(bool isStretch, int value)
        {
            if (radioButton1.Checked)
            {
                if (radioButton3.Checked)
                {
                    imgData.hist2.readHistogramRGB(imgData.img);
                    imgData.contrastByHistogram("R", value, isStretch);
                    imgData.contrastByHistogram("G", value, isStretch);
                    imgData.contrastByHistogram("B", value, isStretch);
                    imgData.contrastByHistogram("I", value, isStretch);
                    pictureBox2.Image = imgData.drawImage("StretchRGB");
                    imgData.hist2.readHistogramRGB(imgData.imgnew);
                    imgData.hist2.drawHistogram(chart2, "RGB");
                }
                else if (radioButton4.Checked)
                {
                    imgData.hist2.readHistogramRGB(imgData.img);
                    imgData.contrastByHistogram("R", value, isStretch);
                    pictureBox2.Image = imgData.drawImage("StretchR");
                    imgData.hist2.readHistogramRGB(imgData.imgnew);
                    imgData.hist2.drawHistogram(chart2, "R");
                }
                else if (radioButton5.Checked)
                {
                    imgData.hist2.readHistogramRGB(imgData.img);
                    imgData.contrastByHistogram("G", value, isStretch);
                    pictureBox2.Image = imgData.drawImage("StretchG");
                    imgData.hist2.readHistogramRGB(imgData.imgnew);
                    imgData.hist2.drawHistogram(chart2, "G");
                }
                else if (radioButton6.Checked)
                {
                    imgData.hist2.readHistogramRGB(imgData.img);
                    imgData.contrastByHistogram("B", value, isStretch);
                    pictureBox2.Image = imgData.drawImage("StretchB");
                    imgData.hist2.readHistogramRGB(imgData.imgnew);
                    imgData.hist2.drawHistogram(chart2, "B");
                }
                else if (radioButton7.Checked)
                {
                    imgData.hist2.readHistogramRGB(imgData.img);
                    imgData.contrastByHistogram("I", value, isStretch);
                    pictureBox2.Image = imgData.drawImage("StretchI");
                    imgData.hist2.readHistogramRGB(imgData.imgnew);
                    imgData.hist2.drawHistogram(chart2, "I");
                }
            }
            else if (radioButton2.Checked)
            {
                if (radioButton3.Checked)
                {
                    imgData.hist2.readHistogramHSV(imgData.img, imgData.imghsv);
                    imgData.contrastByHistogram("S", value, isStretch);
                    imgData.contrastByHistogram("V", value, isStretch);
                    pictureBox2.Image = imgData.drawImage("StretchHSV");
                    imgData.hist2.readHistogramHSV(imgData.imgnew, imgData.imghsvnew);
                    imgData.hist2.drawHistogram(chart2, "HSV");
                }
                else if (radioButton5.Checked)
                {
                    imgData.hist2.readHistogramHSV(imgData.img, imgData.imghsv);
                    imgData.contrastByHistogram("S", value, isStretch);
                    pictureBox2.Image = imgData.drawImage("StretchS");
                    imgData.hist2.readHistogramHSV(imgData.imgnew, imgData.imghsvnew);
                    imgData.hist2.drawHistogram(chart2, "S");
                }
                else if (radioButton6.Checked)
                {
                    imgData.hist2.readHistogramHSV(imgData.img, imgData.imghsv);
                    imgData.contrastByHistogram("V", value, isStretch);
                    pictureBox2.Image = imgData.drawImage("StretchV");
                    imgData.hist2.readHistogramHSV(imgData.imgnew, imgData.imghsvnew);
                    imgData.hist2.drawHistogram(chart2, "V");
                }
            }
        }

        private void filter1blurToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imgData.img != null)
            {
                imgData.filters = new Filter(); //jauns filtrs
                imgData.filters.filter3x3Blur(); //veidojam filtru
                imgData.filterImage(imgData.filters); //filtrējam attēlu
                imgData.hist2.readHistogramRGB(imgData.img); //noalasam histogrammu
                imgData.hist2.drawHistogram(chart2, "RGB"); //zīmējam histogrammu
                radioButton1.Checked = true; //RGB
                radioButton3.Checked = true; //Composite
                pictureBox2.Image = imgData.drawImage("StretchRGB"); //izvadam attēlu
                GC.Collect();
            }
        }

        private void filter2blurToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imgData.img != null)
            {
                imgData.filters = new Filter(); //jauns filtrs
                imgData.filters.filter3x3Blur2(); //veidojam filtru
                imgData.filterImage(imgData.filters); //filtrējam attēlu
                imgData.hist2.readHistogramRGB(imgData.img); //noalasam histogrammu
                imgData.hist2.drawHistogram(chart2, "RGB"); //zīmējam histogrammu
                radioButton1.Checked = true; //RGB
                radioButton3.Checked = true; //Composite
                pictureBox2.Image = imgData.drawImage("StretchRGB"); //izvadam attēlu
                GC.Collect();
            }
        }

        private void filter3blurToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imgData.img != null)
            {
                imgData.filters = new Filter(); //jauns filtrs
                imgData.filters.filter3x3Blur3(); //veidojam filtru
                imgData.filterImage(imgData.filters); //filtrējam attēlu
                imgData.hist2.readHistogramRGB(imgData.img); //noalasam histogrammu
                imgData.hist2.drawHistogram(chart2, "RGB"); //zīmējam histogrammu
                radioButton1.Checked = true; //RGB
                radioButton3.Checked = true; //Composite
                pictureBox2.Image = imgData.drawImage("StretchRGB"); //izvadam attēlu
                GC.Collect();
            }
        }

        private void filter1sharpenToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (imgData.img != null)
            {
                imgData.filters = new Filter(); //jauns filtrs
                imgData.filters.filter3x3Sharpen(); //veidojam filtru
                imgData.filterImage(imgData.filters); //filtrējam attēlu
                imgData.hist2.readHistogramRGB(imgData.img); //noalasam histogrammu
                imgData.hist2.drawHistogram(chart2, "RGB"); //zīmējam histogrammu
                radioButton1.Checked = true; //RGB
                radioButton3.Checked = true; //Composite
                pictureBox2.Image = imgData.drawImage("StretchRGB"); //izvadam attēlu
                GC.Collect();
            }
        }

        private void filter2sharpenToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (imgData.img != null)
            {
                imgData.filters = new Filter(); //jauns filtrs
                imgData.filters.filter3x3Sharpen2(); //veidojam filtru
                imgData.filterImage(imgData.filters); //filtrējam attēlu
                imgData.hist2.readHistogramRGB(imgData.img); //noalasam histogrammu
                imgData.hist2.drawHistogram(chart2, "RGB"); //zīmējam histogrammu
                radioButton1.Checked = true; //RGB
                radioButton3.Checked = true; //Composite
                pictureBox2.Image = imgData.drawImage("StretchRGB"); //izvadam attēlu
                GC.Collect();
            }
        }

        private void filter3sharpenToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (imgData.img != null)
            {
                imgData.filters = new Filter(); //jauns filtrs
                imgData.filters.filter3x3Sharpen3(); //veidojam filtru
                imgData.filterImage(imgData.filters); //filtrējam attēlu
                imgData.hist2.readHistogramRGB(imgData.img); //noalasam histogrammu
                imgData.hist2.drawHistogram(chart2, "RGB"); //zīmējam histogrammu
                radioButton1.Checked = true; //RGB
                radioButton3.Checked = true; //Composite
                pictureBox2.Image = imgData.drawImage("StretchRGB"); //izvadam attēlu
                GC.Collect();
            }
        }

        private void median3x3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap sourceBitmap = (Bitmap)pictureBox1.Image.Clone();
            imgData.filters = new Filter(); //jauns filtrs
            imgData.filters.MedianFilter(sourceBitmap, imgData.imgnew, 3, 0, false);
            pictureBox2.Image = imgData.drawImage("StretchRGB"); //izvadam attēlu
            imgData.hist2.readHistogramRGB(imgData.imgnew); //noalasam histogrammu
            imgData.hist2.drawHistogram(chart2, "I"); //zīmējam histogrammu
            radioButton1.Checked = true; //RGB
            radioButton3.Checked = true; //Composite

        }

        private void median5x5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap sourceBitmap = (Bitmap)pictureBox1.Image.Clone();
            imgData.filters = new Filter(); //jauns filtrs
            imgData.filters.MedianFilter(sourceBitmap, imgData.imgnew, 5, 0, false);
            pictureBox2.Image = imgData.drawImage("StretchRGB"); //izvadam attēlu
            imgData.hist2.readHistogramRGB(imgData.imgnew); //noalasam histogrammu
            imgData.hist2.drawHistogram(chart2, "I"); //zīmējam histogrammu
            radioButton1.Checked = true; //RGB
            radioButton3.Checked = true; //Composite

        }

        private void median7x7ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap sourceBitmap = (Bitmap)pictureBox1.Image.Clone();
            imgData.filters = new Filter(); //jauns filtrs
            imgData.filters.MedianFilter(sourceBitmap, imgData.imgnew, 7, 0, false);
            pictureBox2.Image = imgData.drawImage("StretchRGB"); //izvadam attēlu
            imgData.hist2.readHistogramRGB(imgData.imgnew); //noalasam histogrammu
            imgData.hist2.drawHistogram(chart2, "I"); //zīmējam histogrammu
            radioButton1.Checked = true; //RGB
            radioButton3.Checked = true; //Composite

        }

        private void sobelRGBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imgData.img != null)
            {
                imgData.filters = new Filter();
                imgData.filters2 = new Filter();
                imgData.filters.xFilter3x3Sobel();
                imgData.filters2.yFilter3x3Sobel();
                radioButton1.Checked = true; //RGB
                radioButton3.Checked = true; //Composite
                imgData.edgeSegmentation(imgData.filters, imgData.filters2, "Sobel", "RGB");
                pictureBox2.Image = imgData.drawImage("StretchRGB");
            }
        }

        private void sobelRedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imgData.img != null)
            {
                imgData.filters = new Filter();
                imgData.filters2 = new Filter();
                imgData.filters.xFilter3x3Sobel();
                imgData.filters2.yFilter3x3Sobel();
                radioButton1.Checked = true; //RGB
                radioButton4.Checked = true; //Red
                imgData.edgeSegmentation(imgData.filters, imgData.filters2, "Sobel", "RGB");
                pictureBox2.Image = imgData.drawImage("StretchR");
            }
        }

        private void sobelGreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imgData.img != null)
            {
                imgData.filters = new Filter();
                imgData.filters2 = new Filter();
                imgData.filters.xFilter3x3Sobel();
                imgData.filters2.yFilter3x3Sobel();
                radioButton1.Checked = true; //RGB
                radioButton5.Checked = true; //Green
                imgData.edgeSegmentation(imgData.filters, imgData.filters2, "Sobel", "RGB");
                pictureBox2.Image = imgData.drawImage("StretchG");
            }
        }

        private void sobelBlueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imgData.img != null)
            {
                imgData.filters = new Filter();
                imgData.filters2 = new Filter();
                imgData.filters.xFilter3x3Sobel();
                imgData.filters2.yFilter3x3Sobel();
                radioButton1.Checked = true; //RGB
                radioButton6.Checked = true; //Blue
                imgData.edgeSegmentation(imgData.filters, imgData.filters2, "Sobel", "RGB");
                pictureBox2.Image = imgData.drawImage("StretchB");
            }
        }

        private void sobelIntensityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imgData.img != null)
            {
                imgData.filters = new Filter();
                imgData.filters2 = new Filter();
                imgData.filters.xFilter3x3Sobel();
                imgData.filters2.yFilter3x3Sobel();
                radioButton1.Checked = true; //RGB
                radioButton7.Checked = true; //Intensity
                imgData.edgeSegmentation(imgData.filters, imgData.filters2, "Sobel", "Intensity");
                pictureBox2.Image = imgData.drawImage("StretchI");
            }
        }

        private void prewittRGBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imgData.img != null)
            {
                imgData.filters = new Filter();
                imgData.filters2 = new Filter();
                imgData.filters.xFilter3x3Prewitt();
                imgData.filters2.yFilter3x3Prewitt();
                radioButton1.Checked = true; //RGB
                radioButton3.Checked = true; //Composite
                imgData.edgeSegmentation(imgData.filters, imgData.filters2, "Prewitt", "RGB");
                pictureBox2.Image = imgData.drawImage("StretchRGB");
            }
        }

        private void prewittRedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imgData.img != null)
            {
                imgData.filters = new Filter();
                imgData.filters2 = new Filter();
                imgData.filters.xFilter3x3Prewitt();
                imgData.filters2.yFilter3x3Prewitt();
                radioButton1.Checked = true; //RGB
                radioButton4.Checked = true; //Red
                imgData.edgeSegmentation(imgData.filters, imgData.filters2, "Prewitt", "RGB");
                pictureBox2.Image = imgData.drawImage("StretchR");
            }
        }

        private void prewittGreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imgData.img != null)
            {
                imgData.filters = new Filter();
                imgData.filters2 = new Filter();
                imgData.filters.xFilter3x3Prewitt();
                imgData.filters2.yFilter3x3Prewitt();
                radioButton1.Checked = true; //RGB
                radioButton5.Checked = true; //Green
                imgData.edgeSegmentation(imgData.filters, imgData.filters2, "Prewitt", "RGB");
                pictureBox2.Image = imgData.drawImage("StretchG");
            }
        }

        private void prewittBlueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imgData.img != null)
            {
                imgData.filters = new Filter();
                imgData.filters2 = new Filter();
                imgData.filters.xFilter3x3Prewitt();
                imgData.filters2.yFilter3x3Prewitt();
                radioButton1.Checked = true; //RGB
                radioButton6.Checked = true; //Blue
                imgData.edgeSegmentation(imgData.filters, imgData.filters2, "Prewitt", "RGB");
                pictureBox2.Image = imgData.drawImage("StretchB");
            }
        }

        private void prewittIntensityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imgData.img != null)
            {
                imgData.filters = new Filter();
                imgData.filters2 = new Filter();
                imgData.filters.xFilter3x3Prewitt();
                imgData.filters2.yFilter3x3Prewitt();
                radioButton1.Checked = true; //RGB
                radioButton7.Checked = true; //Intensity
                imgData.edgeSegmentation(imgData.filters, imgData.filters2, "Prewitt", "Intensity");
                pictureBox2.Image = imgData.drawImage("StretchI");
            }
        }

        private void robertsRGBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imgData.img != null)
            {
                imgData.filters = new Filter();
                imgData.filters2 = new Filter();
                imgData.filters.xFilter2x2Roberts();
                imgData.filters2.yFilter2x2Roberts();
                radioButton1.Checked = true; //RGB
                radioButton3.Checked = true; //Composite
                imgData.edgeSegmentation(imgData.filters, imgData.filters2, "Roberts", "RGB");
                pictureBox2.Image = imgData.drawImage("StretchRGB");
            }
        }

        private void robertsRedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imgData.img != null)
            {
                imgData.filters = new Filter();
                imgData.filters2 = new Filter();
                imgData.filters.xFilter2x2Roberts();
                imgData.filters2.yFilter2x2Roberts();
                radioButton1.Checked = true; //RGB
                radioButton4.Checked = true; //Red
                imgData.edgeSegmentation(imgData.filters, imgData.filters2, "Roberts", "RGB");
                pictureBox2.Image = imgData.drawImage("StretchR");
            }
        }

        private void robertsGreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imgData.img != null)
            {
                imgData.filters = new Filter();
                imgData.filters2 = new Filter();
                imgData.filters.xFilter2x2Roberts();
                imgData.filters2.yFilter2x2Roberts();
                radioButton1.Checked = true; //RGB
                radioButton5.Checked = true; //Green
                imgData.edgeSegmentation(imgData.filters, imgData.filters2, "Roberts", "RGB");
                pictureBox2.Image = imgData.drawImage("StretchG");
            }
        }

        private void robertsBlueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imgData.img != null)
            {
                imgData.filters = new Filter();
                imgData.filters2 = new Filter();
                imgData.filters.xFilter2x2Roberts();
                imgData.filters2.yFilter2x2Roberts();
                radioButton1.Checked = true; //RGB
                radioButton6.Checked = true; //Blue
                imgData.edgeSegmentation(imgData.filters, imgData.filters2, "Roberts", "RGB");
                pictureBox2.Image = imgData.drawImage("StretchB");
            }
        }

        private void robertsIntensityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imgData.img != null)
            {
                imgData.filters = new Filter();
                imgData.filters2 = new Filter();
                imgData.filters.xFilter2x2Roberts();
                imgData.filters2.yFilter2x2Roberts();
                radioButton1.Checked = true; //RGB
                radioButton7.Checked = true; //Intensity
                imgData.edgeSegmentation(imgData.filters, imgData.filters2, "Roberts", "Intensity");
                pictureBox2.Image = imgData.drawImage("StretchI");
            }
        }

        private void adaptiveVerticalIntensityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imgData.img != null)
            {
                bool isAutomatic = true;
                colormode = "StretchI";
                thresholdmode = "Vertical";
                radioButton1.Checked = true; //RGB
                radioButton7.Checked = true; //Intensity
                imgData.histogramSegmentation(isAutomatic, 0, 0, colormode, thresholdmode); //segmentējam attēlu
                pictureBox2.Image = imgData.drawImage("StretchI"); //pārzīmējam rezultātu
                label10.Text = "Threshold: " + Convert.ToString(imgData.hist2.calculateAutomaticThreshold(imgData.hist2.hI));
                groupBox5.Visible = true;
                trackBar2.Visible = false; // threshold 1 trackbar
                trackBar3.Visible = false; // threshold 2 trackbar
                label11.Visible = false; // threshold 2 value
                label12.Visible = false; // trackbar 1 0
                label13.Visible = false; // trackbar 2 0
                label14.Visible = false; // trackbar 1 255
                label15.Visible = false; // trackbar 2 255
            }
        }

        private void adaptiveVerticalRedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imgData.img != null)
            {
                bool isAutomatic = true;
                colormode = "StretchR";
                thresholdmode = "Vertical";
                radioButton1.Checked = true; //RGB
                radioButton4.Checked = true; //Red
                imgData.histogramSegmentation(isAutomatic, 0, 0, colormode, thresholdmode); //segmentējam attēlu
                pictureBox2.Image = imgData.drawImage("StretchR"); //pārzīmējam rezultātu
                label10.Text = "Threshold: " + Convert.ToString(imgData.hist2.calculateAutomaticThreshold(imgData.hist2.hR));
                groupBox5.Visible = true;
                trackBar2.Visible = false; // threshold 1 trackbar
                trackBar3.Visible = false; // threshold 2 trackbar
                label11.Visible = false; // threshold 2 value
                label12.Visible = false; // trackbar 1 0
                label13.Visible = false; // trackbar 2 0
                label14.Visible = false; // trackbar 1 255
                label15.Visible = false; // trackbar 2 255
            }
        }

        private void adaptiveVerticalGreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imgData.img != null)
            {
                bool isAutomatic = true;
                colormode = "StretchG";
                thresholdmode = "Vertical";
                radioButton1.Checked = true; //RGB
                radioButton5.Checked = true; //Green
                imgData.histogramSegmentation(isAutomatic, 0, 0, colormode, thresholdmode); //segmentējam attēlu
                pictureBox2.Image = imgData.drawImage("StretchG"); //pārzīmējam rezultātu
                label10.Text = "Threshold: " + Convert.ToString(imgData.hist2.calculateAutomaticThreshold(imgData.hist2.hG));
                groupBox5.Visible = true;
                trackBar2.Visible = false; // threshold 1 trackbar
                trackBar3.Visible = false; // threshold 2 trackbar
                label11.Visible = false; // threshold 2 value
                label12.Visible = false; // trackbar 1 0
                label13.Visible = false; // trackbar 2 0
                label14.Visible = false; // trackbar 1 255
                label15.Visible = false; // trackbar 2 255
            }
        }

        private void adaptiveVerticalBlueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imgData.img != null)
            {
                bool isAutomatic = true;
                colormode = "StretchB";
                thresholdmode = "Vertical";
                radioButton1.Checked = true; //RGB
                radioButton6.Checked = true; //Blue
                imgData.histogramSegmentation(isAutomatic, 0, 0, colormode, thresholdmode); //segmentējam attēlu
                pictureBox2.Image = imgData.drawImage("StretchB"); //pārzīmējam rezultātu
                label10.Text = "Threshold: " + Convert.ToString(imgData.hist2.calculateAutomaticThreshold(imgData.hist2.hB));
                groupBox5.Visible = true;
                trackBar2.Visible = false; // threshold 1 trackbar
                trackBar3.Visible = false; // threshold 2 trackbar
                label11.Visible = false; // threshold 2 value
                label12.Visible = false; // trackbar 1 0
                label13.Visible = false; // trackbar 2 0
                label14.Visible = false; // trackbar 1 255
                label15.Visible = false; // trackbar 2 255
            }
        }

        private void verticalManualIntensityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imgData.img != null)
            {
                bool isAutomatic = false;
                colormode = "StretchI";
                thresholdmode = "Vertical";
                int T = trackBar2.Value;
                radioButton1.Checked = true; //RGB
                radioButton7.Checked = true; //Intensity
                trackBar2.Maximum = 255;
                label14.Text = "255";
                trackBar2.Minimum = 0;
                label12.Text = "0";
                imgData.histogramSegmentation(isAutomatic, T, 0, colormode, thresholdmode); //segmentējam attēlu
                pictureBox2.Image = imgData.drawImage("StretchI"); //pārzīmējam rezultātu
                label10.Text = "Threshold: " + Convert.ToString(trackBar2.Value);
                groupBox5.Visible = true;
                trackBar2.Visible = true; // threshold 1 trackbar
                trackBar3.Visible = false; // threshold 2 trackbar
                label11.Visible = false; // threshold 2 value
                label12.Visible = true; // trackbar 1 0
                label13.Visible = false; // trackbar 2 0
                label14.Visible = true; // trackbar 1 255
                label15.Visible = false; // trackbar 2 255
            }
        }

        private void verticalManualRedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imgData.img != null)
            {
                bool isAutomatic = false;
                colormode = "StretchR";
                thresholdmode = "Vertical";
                int T = trackBar2.Value;
                radioButton1.Checked = true; //RGB
                radioButton4.Checked = true; //Red
                trackBar2.Maximum = 255;
                label14.Text = "255";
                trackBar2.Minimum = 0;
                label12.Text = "0";
                imgData.histogramSegmentation(isAutomatic, T, 0, colormode, thresholdmode); //segmentējam attēlu
                pictureBox2.Image = imgData.drawImage("StretchR"); //pārzīmējam rezultātu
                label10.Text = "Threshold: " + Convert.ToString(trackBar2.Value);
                groupBox5.Visible = true;
                trackBar2.Visible = true; // threshold 1 trackbar
                trackBar3.Visible = false; // threshold 2 trackbar
                label11.Visible = false; // threshold 2 value
                label12.Visible = true; // trackbar 1 0
                label13.Visible = false; // trackbar 2 0
                label14.Visible = true; // trackbar 1 255
                label15.Visible = false; // trackbar 2 255
            }
        }

        private void verticalManualGreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imgData.img != null)
            {
                bool isAutomatic = false;
                colormode = "StretchG";
                thresholdmode = "Vertical";
                int T = trackBar2.Value;
                radioButton1.Checked = true; //RGB
                radioButton5.Checked = true; //Green
                trackBar2.Maximum = 255;
                label14.Text = "255";
                trackBar2.Minimum = 0;
                label12.Text = "0";
                imgData.histogramSegmentation(isAutomatic, T, 0, colormode, thresholdmode); //segmentējam attēlu
                pictureBox2.Image = imgData.drawImage("StretchG"); //pārzīmējam rezultātu
                label10.Text = "Threshold: " + Convert.ToString(trackBar2.Value);
                groupBox5.Visible = true;
                trackBar2.Visible = true; // threshold 1 trackbar
                trackBar3.Visible = false; // threshold 2 trackbar
                label11.Visible = false; // threshold 2 value
                label12.Visible = true; // trackbar 1 0
                label13.Visible = false; // trackbar 2 0
                label14.Visible = true; // trackbar 1 255
                label15.Visible = false; // trackbar 2 255
            }
        }

        private void verticalManualBlueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imgData.img != null)
            {
                bool isAutomatic = false;
                colormode = "StretchB";
                thresholdmode = "Vertical";
                int T = trackBar2.Value;
                radioButton1.Checked = true; //RGB
                radioButton6.Checked = true; //Blue
                trackBar2.Maximum = 255;
                label14.Text = "255";
                trackBar2.Minimum = 0;
                label12.Text = "0";
                imgData.histogramSegmentation(isAutomatic, T, 0, colormode, thresholdmode); //segmentējam attēlu
                pictureBox2.Image = imgData.drawImage("StretchB"); //pārzīmējam rezultātu
                label10.Text = "Threshold: " + Convert.ToString(trackBar2.Value);
                groupBox5.Visible = true;
                trackBar2.Visible = true; // threshold 1 trackbar
                trackBar3.Visible = false; // threshold 2 trackbar
                label11.Visible = false; // threshold 2 value
                label12.Visible = true; // trackbar 1 0
                label13.Visible = false; // trackbar 2 0
                label14.Visible = true; // trackbar 1 255
                label15.Visible = false; // trackbar 2 255
            }
        }

        private void horizontalIntensityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imgData.img != null)
            {
                bool isAutomatic = false;
                colormode = "StretchI";
                thresholdmode = "Horizontal";
                trackBar2.Maximum = imgData.hist1.hI[256];
                label14.Text = Convert.ToString(imgData.hist1.hI[256]);
                trackBar2.Minimum = 0;
                label12.Text = "0";
                int T = trackBar2.Value;
                radioButton1.Checked = true; //RGB
                radioButton7.Checked = true; //Intensity
                imgData.histogramSegmentation(isAutomatic, T, 0, colormode, thresholdmode); //segmentējam attēlu
                pictureBox2.Image = imgData.drawImage("StretchI"); //pārzīmējam rezultātu
                label10.Text = "Threshold: " + Convert.ToString(trackBar2.Value);
                groupBox5.Visible = true;
                trackBar2.Visible = true; // threshold 1 trackbar
                trackBar3.Visible = false; // threshold 2 trackbar
                label11.Visible = false; // threshold 2 value
                label12.Visible = true; // trackbar 1 0
                label13.Visible = false; // trackbar 2 0
                label14.Visible = true; // trackbar 1 255
                label15.Visible = false; // trackbar 2 255
            }
        }

        private void horizontalRedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imgData.img != null)
            {
                bool isAutomatic = false;
                colormode = "StretchR";
                thresholdmode = "Horizontal";
                trackBar2.Maximum = imgData.hist1.hR[256];
                label14.Text = Convert.ToString(imgData.hist1.hR[256]);
                trackBar2.Minimum = 0;
                label12.Text = "0";
                int T = trackBar2.Value;
                radioButton1.Checked = true; //RGB
                radioButton4.Checked = true; //Red
                imgData.histogramSegmentation(isAutomatic, T, 0, colormode, thresholdmode); //segmentējam attēlu
                pictureBox2.Image = imgData.drawImage("StretchR"); //pārzīmējam rezultātu
                label10.Text = "Threshold: " + Convert.ToString(trackBar2.Value);
                groupBox5.Visible = true;
                trackBar2.Visible = true; // threshold 1 trackbar
                trackBar3.Visible = false; // threshold 2 trackbar
                label11.Visible = false; // threshold 2 value
                label12.Visible = true; // trackbar 1 0
                label13.Visible = false; // trackbar 2 0
                label14.Visible = true; // trackbar 1 255
                label15.Visible = false; // trackbar 2 255
            }
        }

        private void horizontalGreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imgData.img != null)
            {
                bool isAutomatic = false;
                colormode = "StretchG";
                thresholdmode = "Horizontal";
                trackBar2.Maximum = imgData.hist1.hG[256];
                label14.Text = Convert.ToString(imgData.hist1.hG[256]);
                trackBar2.Minimum = 0;
                label12.Text = "0";
                int T = trackBar2.Value;
                radioButton1.Checked = true; //RGB
                radioButton5.Checked = true; //Green
                imgData.histogramSegmentation(isAutomatic, T, 0, colormode, thresholdmode); //segmentējam attēlu
                pictureBox2.Image = imgData.drawImage("StretchG"); //pārzīmējam rezultātu
                //tagad es gribu redzēt, kur man uz histogrammas atrodas vertikālais slieksnis, izvadām vērtību label laukā - intensitātei
                label10.Text = "Threshold: " + Convert.ToString(trackBar2.Value);
                groupBox5.Visible = true;
                trackBar2.Visible = true; // threshold 1 trackbar
                trackBar3.Visible = false; // threshold 2 trackbar
                label11.Visible = false; // threshold 2 value
                label12.Visible = true; // trackbar 1 0
                label13.Visible = false; // trackbar 2 0
                label14.Visible = true; // trackbar 1 255
                label15.Visible = false; // trackbar 2 255
            }
        }

        private void horizontalBlueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imgData.img != null)
            {
                bool isAutomatic = false;
                colormode = "StretchB";
                thresholdmode = "Horizontal";
                trackBar2.Maximum = imgData.hist1.hB[256];
                label14.Text = Convert.ToString(imgData.hist1.hB[256]);
                trackBar2.Minimum = 0;
                label12.Text = "0";
                int T = trackBar2.Value;
                radioButton1.Checked = true; //RGB
                radioButton6.Checked = true; //Blue
                imgData.histogramSegmentation(isAutomatic, T, 0, colormode, thresholdmode); //segmentējam attēlu
                pictureBox2.Image = imgData.drawImage("StretchB"); //pārzīmējam rezultātu
                //tagad es gribu redzēt, kur man uz histogrammas atrodas vertikālais slieksnis, izvadām vērtību label laukā - intensitātei
                label10.Text = "Threshold: " + Convert.ToString(trackBar2.Value);
                groupBox5.Visible = true;
                trackBar2.Visible = true; // threshold 1 trackbar
                trackBar3.Visible = false; // threshold 2 trackbar
                label11.Visible = false; // threshold 2 value
                label12.Visible = true; // trackbar 1 0
                label13.Visible = false; // trackbar 2 0
                label14.Visible = true; // trackbar 1 255
                label15.Visible = false; // trackbar 2 255
            }
        }

        private void twoVerticalIntensityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imgData.img != null)
            {
                bool isAutomatic = false;
                colormode = "StretchI";
                thresholdmode = "Two";
                trackBar2.Maximum = 255;
                label14.Text = "255";
                trackBar2.Minimum = 0;
                label12.Text = "0";
                trackBar2.Value = 30;
                trackBar3.Value = 210;
                int T = trackBar2.Value;
                int T2 = trackBar3.Value;
                radioButton1.Checked = true; //RGB
                radioButton7.Checked = true; //Intensity
                imgData.histogramSegmentation(isAutomatic, T, T2, colormode, thresholdmode); //segmentējam attēlu
                pictureBox2.Image = imgData.drawImage("StretchI"); //pārzīmējam rezultātu
                //tagad es gribu redzēt, kur man uz histogrammas atrodas vertikālais slieksnis, izvadām vērtību label laukā - intensitātei
                label10.Text = "Threshold 1: " + Convert.ToString(trackBar2.Value);
                label11.Text = "Threshold 2: " + Convert.ToString(trackBar3.Value);
                groupBox5.Visible = true;
                trackBar2.Visible = true; // threshold 1 trackbar
                trackBar3.Visible = true; // threshold 2 trackbar
                label11.Visible = true; // threshold 2 value
                label12.Visible = true; // trackbar 1 0
                label13.Visible = true; // trackbar 2 0
                label14.Visible = true; // trackbar 1 255
                label15.Visible = true; // trackbar 2 255
            }
        }

        private void twoVerticalRedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imgData.img != null)
            {
                bool isAutomatic = false;
                colormode = "StretchR";
                thresholdmode = "Two";
                trackBar2.Maximum = 255;
                label14.Text = "255";
                trackBar2.Minimum = 0;
                label12.Text = "0";
                trackBar2.Value = 30;
                trackBar3.Value = 210;
                int T = trackBar2.Value;
                int T2 = trackBar3.Value;
                radioButton1.Checked = true; //RGB
                radioButton4.Checked = true; //Red
                imgData.histogramSegmentation(isAutomatic, T, T2, colormode, thresholdmode); //segmentējam attēlu
                pictureBox2.Image = imgData.drawImage("StretchR"); //pārzīmējam rezultātu
                //tagad es gribu redzēt, kur man uz histogrammas atrodas vertikālais slieksnis, izvadām vērtību label laukā - intensitātei
                label10.Text = "Threshold 1: " + Convert.ToString(trackBar2.Value);
                label11.Text = "Threshold 2: " + Convert.ToString(trackBar3.Value);
                groupBox5.Visible = true;
                trackBar2.Visible = true; // threshold 1 trackbar
                trackBar3.Visible = true; // threshold 2 trackbar
                label11.Visible = true; // threshold 2 value
                label12.Visible = true; // trackbar 1 0
                label13.Visible = true; // trackbar 2 0
                label14.Visible = true; // trackbar 1 255
                label15.Visible = true; // trackbar 2 255
            }
        }

        private void twoVerticalGreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imgData.img != null)
            {
                bool isAutomatic = false;
                colormode = "StretchG";
                thresholdmode = "Two";
                trackBar2.Maximum = 255;
                label14.Text = "255";
                trackBar2.Minimum = 0;
                label12.Text = "0";
                trackBar2.Value = 30;
                trackBar3.Value = 210;
                int T = trackBar2.Value;
                int T2 = trackBar3.Value;
                radioButton1.Checked = true; //RGB
                radioButton5.Checked = true; //Green
                imgData.histogramSegmentation(isAutomatic, T, T2, colormode, thresholdmode); //segmentējam attēlu
                pictureBox2.Image = imgData.drawImage("StretchG"); //pārzīmējam rezultātu
                //tagad es gribu redzēt, kur man uz histogrammas atrodas vertikālais slieksnis, izvadām vērtību label laukā - intensitātei
                label10.Text = "Threshold 1: " + Convert.ToString(trackBar2.Value);
                label11.Text = "Threshold 2: " + Convert.ToString(trackBar3.Value);
                groupBox5.Visible = true;
                trackBar2.Visible = true; // threshold 1 trackbar
                trackBar3.Visible = true; // threshold 2 trackbar
                label11.Visible = true; // threshold 2 value
                label12.Visible = true; // trackbar 1 0
                label13.Visible = true; // trackbar 2 0
                label14.Visible = true; // trackbar 1 255
                label15.Visible = true; // trackbar 2 255
            }
        }

        private void twoVerticalBlueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imgData.img != null)
            {
                bool isAutomatic = false;
                colormode = "StretchB";
                thresholdmode = "Two";
                trackBar2.Maximum = 255;
                label14.Text = "255";
                trackBar2.Minimum = 0;
                label12.Text = "0";
                trackBar2.Value = 30;
                trackBar3.Value = 210;
                int T = trackBar2.Value;
                int T2 = trackBar3.Value;
                radioButton1.Checked = true; //RGB
                radioButton6.Checked = true; //Blue
                imgData.histogramSegmentation(isAutomatic, T, T2, colormode, thresholdmode); //segmentējam attēlu
                pictureBox2.Image = imgData.drawImage("StretchB"); //pārzīmējam rezultātu
                //tagad es gribu redzēt, kur man uz histogrammas atrodas vertikālais slieksnis, izvadām vērtību label laukā - intensitātei
                label10.Text = "Threshold 1: " + Convert.ToString(trackBar2.Value);
                label11.Text = "Threshold 2: " + Convert.ToString(trackBar3.Value);
                groupBox5.Visible = true;
                trackBar2.Visible = true; // threshold 1 trackbar
                trackBar3.Visible = true; // threshold 2 trackbar
                label11.Visible = true; // threshold 2 value
                label12.Visible = true; // trackbar 1 0
                label13.Visible = true; // trackbar 2 0
                label14.Visible = true; // trackbar 1 255
                label15.Visible = true; // trackbar 2 255
            }
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            if (imgData.img != null)
            {
                radioButton1.Checked = true;
                toolTip1.SetToolTip(trackBar2, trackBar2.Value.ToString());
                if (thresholdmode == "Vertical")
                {
                    trackBar2.Maximum = 255;
                    trackBar2.Minimum = 0;
                    label12.Text = "0";
                    label14.Text = "255";
                    imgData.histogramSegmentation(false, trackBar2.Value, 0, colormode, thresholdmode);
                    pictureBox2.Image = imgData.drawImage(colormode);
                    label10.Text = "Threshold: " + Convert.ToString(trackBar2.Value);
                }
                else if(thresholdmode == "Horizontal")
                {
                    imgData.histogramSegmentation(false, trackBar2.Value, 0, colormode, thresholdmode);
                    pictureBox2.Image = imgData.drawImage(colormode);
                    label10.Text = "Threshold: " + Convert.ToString(trackBar2.Value);
                }
                else if (thresholdmode == "Two")
                {
                    trackBar2.Maximum = 255;
                    trackBar2.Minimum = 0;
                    if (trackBar2.Value > trackBar3.Value) trackBar2.Value = trackBar3.Value;
                    else if (trackBar3.Value < trackBar2.Value) trackBar3.Value = trackBar2.Value;
                    imgData.histogramSegmentation(false, trackBar2.Value, trackBar3.Value, colormode, thresholdmode);
                    pictureBox2.Image = imgData.drawImage(colormode);
                    label10.Text = "Threshold 1: " + Convert.ToString(trackBar2.Value);
                    label11.Text = "Threshold 2: " + Convert.ToString(trackBar3.Value);
                }
            }
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            if(imgData.img != null)
            {
                toolTip1.SetToolTip(trackBar3, trackBar3.Value.ToString());
                if (trackBar2.Value > trackBar3.Value) trackBar2.Value = trackBar3.Value;
                else if (trackBar3.Value < trackBar2.Value) trackBar3.Value = trackBar2.Value;
                imgData.histogramSegmentation(false, trackBar2.Value, trackBar3.Value, colormode, thresholdmode);
                pictureBox2.Image = imgData.drawImage(colormode);
                label10.Text = "Threshold 1: " + Convert.ToString(trackBar2.Value);
                label11.Text = "Threshold 2: " + Convert.ToString(trackBar3.Value);
            }
        }

        private void transitionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 openForm = new Form2();
            openForm.Show();
        }
    }
}

