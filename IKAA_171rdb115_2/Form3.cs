using System;
using System.Drawing;
using System.Windows.Forms;

namespace IKAA_171rdb115_2
{
    public partial class Form3 : Form
    {
        public imgData imgData1 = new imgData();
        public imgData imgData2 = new imgData();
        public Form3()
        {
            InitializeComponent();
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Bitmap.FromFile(openFileDialog1.FileName);
                Bitmap bmp = (Bitmap)pictureBox1.Image.Clone();
                imgData1.readImage(bmp);
                imgData2.readImage(bmp);
                pictureBox1.Image = imgData1.drawImage("RGB");
                imgData1.x0 = imgData1.img.GetLength(0) / 2;
                imgData1.y0 = imgData1.img.GetLength(1) / 2;
                GC.Collect();
            }
        }

        private void Translation(int tx, int ty)
        {
            for (int x = 0; x < imgData1.img.GetLength(0); x++)
            {
                for (int y = 0; y < imgData1.img.GetLength(1); y++)
                {
                    if (imgData1.img != null)
                    {
                        imgData1.img[x, y].X = imgData1.img[x, y].X + tx;
                        imgData1.img[x, y].Y = imgData1.img[x, y].Y + ty;
                    }
                }
            }
        }

        private void Rotation(int angle)
        {
            for (int x = 0; x < imgData1.img.GetLength(0); x++)
            {
                for (int y = 0; y < imgData1.img.GetLength(1); y++)
                {
                    if (imgData1.img != null)
                    {
                        int tx = imgData1.img[x, y].X;
                        int ty = imgData1.img[x, y].Y;
                        imgData1.img[x, y].X = (int)Math.Round(tx * Math.Cos((Math.PI / 180) * angle) -
                            ty * Math.Sin((Math.PI / 180) * angle));
                        imgData1.img[x, y].Y = (int)Math.Round(tx * Math.Cos((Math.PI / 180) * angle) +
                            ty * Math.Sin((Math.PI / 180) * angle));
                    }
                }
            }
        }

        private void Skew(int a, int b)
        {
            for (int x = 0; x < imgData1.img.GetLength(0); x++)
            {
                for (int y = 0; y < imgData1.img.GetLength(1); y++)
                {
                    if (imgData1.img != null)
                    {
                        imgData1.img[x, y].X = imgData1.img[x, y].X + a * imgData1.img[x, y].Y;
                        imgData1.img[x, y].Y = imgData1.img[x, y].Y + b * imgData1.img[x, y].X;
                    }
                }
            }
        }

        private void Wave(int a, int b, int alpha, int beta)
        {
            for (int x = 0; x < imgData1.img.GetLength(0); x++)
            {
                for (int y = 0; y < imgData1.img.GetLength(1); y++)
                {
                    if (imgData1.img != null)
                    {
                        imgData1.img[x, y].X = Convert.ToInt32(imgData1.img[x, y].X + a * Math.Sin(2 * Math.PI * imgData1.img[x, y].Y / alpha));
                        imgData1.img[x, y].Y = Convert.ToInt32(imgData1.img[x, y].Y + b * Math.Sin(2 * Math.PI * imgData1.img[x, y].X / beta));
                    }
                }
            }
        }

        private void Warp()
        {
            for (int x = 0; x < imgData1.img.GetLength(0); x++)
            {
                for (int y = 0; y < imgData1.img.GetLength(1); y++)
                {
                    if (imgData1.img != null)
                    {
                        int xMid = imgData1.img.GetLength(0) / 2;
                        int xOrig = imgData1.img[x, y].X + xMid;
                        imgData1.img[x, y].X = (int)Math.Round((double)(Math.Sign(xOrig - xMid)
                            * Math.Pow((xOrig - xMid), 2.0) / xMid) + xMid) - xMid;
                        imgData1.img[x, y].Y = imgData1.img[x, y].Y;
                    }
                }
            }
        }

        private void Swirl()
        {
            for (int x = 0; x < imgData1.img.GetLength(0); x++)
            {
                for (int y = 0; y < imgData1.img.GetLength(1); y++)
                {
                    if (imgData1.img != null)
                    {
                        int xMid = imgData1.img.GetLength(0) / 2;
                        int yMid = imgData1.img.GetLength(1) / 2;
                        int xOrig = imgData1.img[x, y].X + xMid;
                        int yOrig = imgData1.img[x, y].Y + yMid;
                        double r = Math.Sqrt(Math.Pow((xOrig - xMid), 2) + Math.Pow((yOrig - yMid), 2));
                        double teta = Math.PI * r / imgData1.img.GetLength(0);
                        imgData1.img[x, y].X = Convert.ToInt32((xOrig - xMid) * Math.Cos(teta)
                            + (yOrig - yMid) * Math.Sin(teta) + xMid) - xMid;
                        imgData1.img[x, y].Y = Convert.ToInt32(-(xOrig - xMid) * Math.Sin(teta)
                            + (yOrig - yMid) * Math.Cos(teta) + yMid) - yMid;
                    }
                }
            }
        }

        private void translateButton_Click(object sender, EventArgs e)
        {
            if (imgData1 != null && !string.IsNullOrWhiteSpace(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox2.Text))
            {
                try
                {
                    int tx = Convert.ToInt32(textBox1.Text);
                    int ty = Convert.ToInt32(textBox2.Text);
                    Translation(tx, ty);
                    pictureBox1.Image = imgData1.drawImage("Transformation");
                }
                catch (Exception)
                {
                    MessageBox.Show("Input has to be a number", "Input is not a number");
                }
            }
        }

        private void rotateButton_Click(object sender, EventArgs e)
        {
            if (imgData1 != null && !string.IsNullOrWhiteSpace(textBox3.Text))
            {
                try
                {
                    int angle = Convert.ToInt32(textBox3.Text);
                    Rotation(angle);
                    pictureBox1.Image = imgData1.drawImage("Transformation");
                }

                catch (Exception)
                {
                    MessageBox.Show("Input has to be a number", "Input is not a number");
                }
            }
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            for (int x = 0; x < imgData1.img.GetLength(0); x++)
            {
                for (int y = 0; y < imgData1.img.GetLength(1); y++)
                {
                    if (imgData2.img != null)
                    {
                        imgData1.img[x, y].R = imgData2.img[x, y].R;
                        imgData1.img[x, y].G = imgData2.img[x, y].G;
                        imgData1.img[x, y].B = imgData2.img[x, y].B;
                        imgData1.img[x, y].X = x - imgData1.img.GetLength(0) / 2;
                        imgData1.img[x, y].Y = y - imgData1.img.GetLength(1) / 2;
                    }
                }
            }
            pictureBox1.Image = imgData1.drawImage("RGB");
            imgData1.x0 = pictureBox1.Image.Width / 2;
            imgData2.y0 = pictureBox1.Image.Height / 2;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image.Save(saveFileDialog1.FileName);
            }
        }

        private void skewButton_Click(object sender, EventArgs e)
        {
            if (imgData1 != null && !string.IsNullOrWhiteSpace(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox5.Text))
            {
                try
                {
                    int a = Convert.ToInt32(textBox4.Text);
                    int b = Convert.ToInt32(textBox5.Text);
                    Skew(a, b);
                    pictureBox1.Image = imgData1.drawImage("Transformation");
                }
                catch (Exception)
                {
                    MessageBox.Show("Input has to be a number", "Input is not a number");
                }
            }
        }

        private void waveButton_Click(object sender, EventArgs e)
        {
            if (imgData1 != null && !string.IsNullOrWhiteSpace(textBox6.Text) && !string.IsNullOrWhiteSpace(textBox7.Text)
                && !string.IsNullOrWhiteSpace(textBox8.Text) && !string.IsNullOrWhiteSpace(textBox9.Text))
            {
                try
                {
                    int a = Convert.ToInt32(textBox6.Text);
                    int b = Convert.ToInt32(textBox7.Text);
                    int alpha = Convert.ToInt32(textBox8.Text);
                    int beta = Convert.ToInt32(textBox9.Text);
                    Wave(a, b, alpha, beta);
                    pictureBox1.Image = imgData1.drawImage("Transformation");
                }
                catch (Exception)
                {
                    MessageBox.Show("Input has to be a number", "Input is not a number");
                }
            }
        }

        private void warpButton_Click(object sender, EventArgs e)
        {
            if (imgData1 != null)
            {
                Warp();
                pictureBox1.Image = imgData1.drawImage("Transformation");
            }
        }

        private void swirlButton_Click(object sender, EventArgs e)
        {
            if (imgData1 != null)
            {
                Swirl();
                pictureBox1.Image = imgData1.drawImage("Transformation");
            }
        }
    }
}
