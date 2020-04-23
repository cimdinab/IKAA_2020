using System;
using System.Drawing;
using System.Windows.Forms;

namespace IKAA_171rdb115_2
{
    public partial class Form2 : Form
    {
        public imgData imgData1 = new imgData();
        public imgData imgData2 = new imgData();
        public imgData imgData3 = new imgData();

        public int transitionStep;
        public Form2()
        {
            InitializeComponent();
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Bitmap.FromFile(openFileDialog1.FileName);
                //nolasām attēlu no dialoga
                Bitmap bmp = (Bitmap)pictureBox1.Image.Clone();
                //veidojam bmp
                imgData1.readImage(bmp);
                //nolasām attēlu
                pictureBox1.Image = imgData1.drawImage("RGB");
                //zīmējam rezultātu
                GC.Collect(); //attīrām attēlu
            }
        }

        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                pictureBox2.Image = Bitmap.FromFile(openFileDialog2.FileName);
                Bitmap bmp = (Bitmap)pictureBox2.Image.Clone();
                imgData2.readImage(bmp); //nolasām attēlu
                pictureBox2.Image = imgData2.drawImage("RGB"); //zīmējam rezultātu
                //izmantojam metodes no imgData klases
                GC.Collect(); //attīrām attēlu
            }
        }

        public void applyTransition(int s)
        {
            for (int x = 0; x < imgData1.img.GetLength(0); x++)
            {
                for (int y = 0; y < imgData1.img.GetLength(1); y++)
                {
                    imgData3.img[x, y] = new PixelClassRGB();
                    if (fadeRadio.Checked)
                    {
                        imgData3.img[x, y].R = Convert.ToByte(((double)s / 100) * imgData2.img[x, y].R
                            + (1 - (double)s / 100) * imgData1.img[x, y].R);
                        imgData3.img[x, y].G = Convert.ToByte(((double)s / 100) * imgData2.img[x, y].G
                            + (1 - (double)s / 100) * imgData1.img[x, y].G);
                        imgData3.img[x, y].B = Convert.ToByte(((double)s / 100) * imgData2.img[x, y].B
                            + (1 - (double)s / 100) * imgData1.img[x, y].B);
                    }
                    else if (wipeHorizontalRadio.Checked)
                    {
                        if (x <= imgData1.img.GetLength(0) * s / 100)
                        {
                            imgData3.img[x, y].R = imgData2.img[x, y].R;
                            imgData3.img[x, y].G = imgData2.img[x, y].G;
                            imgData3.img[x, y].B = imgData2.img[x, y].B;
                        }
                        else
                        {
                            imgData3.img[x, y].R = imgData1.img[x, y].R;
                            imgData3.img[x, y].G = imgData1.img[x, y].G;
                            imgData3.img[x, y].B = imgData1.img[x, y].B;
                        }
                    }
                    else if (wipeVerticalRadio.Checked)
                    {
                        if (y <= imgData1.img.GetLength(1) * s / 100)
                        {
                            imgData3.img[x, y].R = imgData2.img[x, y].R;
                            imgData3.img[x, y].G = imgData2.img[x, y].G;
                            imgData3.img[x, y].B = imgData2.img[x, y].B;
                        }
                        else
                        {
                            imgData3.img[x, y].R = imgData1.img[x, y].R;
                            imgData3.img[x, y].G = imgData1.img[x, y].G;
                            imgData3.img[x, y].B = imgData1.img[x, y].B;
                        }
                    }
                    else if (splitHorizontalRadio.Checked)
                    {
                        if (Math.Abs(x - imgData1.img.GetLength(0)/2) < imgData1.img.GetLength(0) * s/200)
                        {
                            imgData3.img[x, y].R = imgData2.img[x, y].R;
                            imgData3.img[x, y].G = imgData2.img[x, y].G;
                            imgData3.img[x, y].B = imgData2.img[x, y].B;
                        }
                        else
                        {
                            imgData3.img[x, y].R = imgData1.img[x, y].R;
                            imgData3.img[x, y].G = imgData1.img[x, y].G;
                            imgData3.img[x, y].B = imgData1.img[x, y].B;
                        }
                    }
                    else if (splitVerticalRadio.Checked)
                    {
                        if (Math.Abs(y - imgData1.img.GetLength(1) / 2) < imgData1.img.GetLength(1) * s / 200)
                        {
                            imgData3.img[x, y].R = imgData2.img[x, y].R;
                            imgData3.img[x, y].G = imgData2.img[x, y].G;
                            imgData3.img[x, y].B = imgData2.img[x, y].B;
                        }
                        else
                        {
                            imgData3.img[x, y].R = imgData1.img[x, y].R;
                            imgData3.img[x, y].G = imgData1.img[x, y].G;
                            imgData3.img[x, y].B = imgData1.img[x, y].B;
                        }
                    }
                }
            }
            pictureBox3.Image = imgData3.drawImage("Transition");
        }

        public void applyEffect(string method)
        {
            if (imgData1 != null && imgData2 != null && imgData1.img.GetLength(0) == imgData2.img.GetLength(0)
                && imgData1.img.GetLength(1) == imgData2.img.GetLength(1))
            {
                imgData3.img = new PixelClassRGB[imgData1.img.GetLength(0), imgData2.img.GetLength(1)];
                if (imgData3.img != null)
                {
                    for (int x = 0; x < imgData1.img.GetLength(0); x++)
                    {
                        for (int y = 0; y < imgData1.img.GetLength(1); y++)
                        {
                            imgData3.img[x, y] = new PixelClassRGB();
                            switch (method)
                            {
                                case "Opacity":
                                    {
                                        imgData3.img[x, y].effectOpacity(imgData1.img[x, y], imgData2.img[x, y], (double)trackBar1.Value / 100);
                                        break;
                                    }
                                case "Screen":
                                    {
                                        imgData3.img[x, y].effectScreen(imgData1.img[x, y], imgData2.img[x, y]);
                                        break;
                                    }
                                case "Darken":
                                    {
                                        imgData3.img[x, y].effectDarken(imgData1.img[x, y], imgData2.img[x, y]);
                                        break;
                                    }
                                case "Lighten":
                                    {
                                        imgData3.img[x, y].effectLighten(imgData1.img[x, y], imgData2.img[x, y]);
                                        break;
                                    }
                                case "Multiply":
                                    {
                                        imgData3.img[x, y].effectMultiply(imgData1.img[x, y], imgData2.img[x, y]);
                                        break;
                                    }
                                case "Addition":
                                    {
                                        imgData3.img[x, y].effectAddition(imgData1.img[x, y], imgData2.img[x, y]);
                                        break;
                                    }
                                case "Subtract":
                                    {
                                        imgData3.img[x, y].effectSubtract(imgData1.img[x, y], imgData2.img[x, y]);
                                        break;
                                    }
                                case "Overlay":
                                    {
                                        imgData3.img[x, y].effectOverlay(imgData1.img[x, y], imgData2.img[x, y]);
                                        break;
                                    }
                                case "HardLight":
                                    {
                                        imgData3.img[x, y].effectHardLight(imgData1.img[x, y], imgData2.img[x, y]);
                                        break;
                                    }
                                case "SoftLight":
                                    {
                                        imgData3.img[x, y].effectSoftLight(imgData1.img[x, y], imgData2.img[x, y]);
                                        break;
                                    }
                                case "Difference":
                                    {
                                        imgData3.img[x, y].effectDifference(imgData1.img[x, y], imgData2.img[x, y]);
                                        break;
                                    }
                            }
                        }
                    }
                }
                pictureBox3.Image = imgData3.drawImage("Transition");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            transitionStep++; //pāreju skaits
            if (transitionStep > 100)
            {
                timer1.Enabled = false;
                panel1.Enabled = true;
                progressBar1.Value = 100;
            }
            else
            {
                progressBar1.Value = transitionStep;
                progressBar1.Value = transitionStep - 1;
                applyTransition(transitionStep);
            }
        }
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label1.Text = "Opacity = " + trackBar1.Value + "%";
            toolTip1.SetToolTip(trackBar1, trackBar1.Value.ToString());
        }

        private void opacityButton_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null && pictureBox2.Image != null)
            {
                applyEffect("Opacity"); //pielietoam efektu caurpīdīgums

            }
        }

        private void screenButton_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null && pictureBox2.Image != null)
            {
                applyEffect("Screen");
            }
        }

        private void transitionButton_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null && pictureBox2.Image != null)
            {
                imgData3.img = new PixelClassRGB[imgData1.img.GetLength(0), imgData2.img.GetLength(1)];
                transitionStep = 0;
                timer1.Enabled = true;
            }
        }

        private void darkenButton_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null && pictureBox2.Image != null)
            {
                applyEffect("Darken");
            }
        }

        private void additionButton_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null && pictureBox2.Image != null)
            {
                applyEffect("Addition");
            }
        }

        private void subtractButton_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null && pictureBox2.Image != null)
            {
                applyEffect("Subtract");
            }
        }

        private void overlayButton_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null && pictureBox2.Image != null)
            {
                applyEffect("Overlay");
            }
        }

        private void differenceButton_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null && pictureBox2.Image != null)
            {
                applyEffect("Difference");
            }
        }

        private void hardLightButton_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null && pictureBox2.Image != null)
            {
                applyEffect("HardLight");
            }
        }

        private void softLightButton_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null && pictureBox2.Image != null)
            {
                applyEffect("SoftLight");
            }
        }

        private void multiplyButton_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null && pictureBox2.Image != null)
            {
                applyEffect("Multiply");
            }
        }

        private void lightenButton_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null && pictureBox2.Image != null)
            {
                applyEffect("Lighten");
            }
        }
    }
}

