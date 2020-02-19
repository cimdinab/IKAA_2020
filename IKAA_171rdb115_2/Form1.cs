using System;
using System.Drawing;
using System.Drawing.Imaging; //nolasām pixeļa formātu
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace IKAA_171rdb115_2
{
    public partial class Form1 : Form
    {
        public imgData imgData = new imgData();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Bitmap.FromFile(openFileDialog1.FileName);
            }
            Bitmap bmp = (Bitmap)pictureBox1.Image.Clone();
            imgData.readImage(bmp);
            pictureBox2.Image = imgData.drawImage("RGB");

        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (pictureBox2.Image != null)
            {
                Bitmap bmpo = pictureBox1.Image as Bitmap;
                Bitmap bmpi = pictureBox2.Image as Bitmap;
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
                Color colororg = bmpo.GetPixel(Convert.ToInt32(kx), Convert.ToInt32(ky));
                Color colorinv = bmpi.GetPixel(Convert.ToInt32(kx), Convert.ToInt32(ky));
                textBox1.BackColor = colororg;
                textBox2.BackColor = colorinv;
                label1.Text = "Krāsa oriģinālajā attēlā \n" + Convert.ToString(colororg);
                label2.Text = "Krāsa pārveidotajā attēlā \n" + Convert.ToString(colorinv);
                colorDialog1.ShowDialog();
                bmpi.SetPixel(Convert.ToInt32(kx), Convert.ToInt32(ky), colorDialog1.Color);
                textBox3.BackColor = colorDialog1.Color;
                label3.Text = "Izvēlētā krāsa \n" + Convert.ToString(colorDialog1.Color);
                pictureBox2.Refresh();
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        { //RGB
            radioButton3.Checked = true; //Composite
            radioButton4.Text = "Red";
            radioButton5.Text = "Green";
            radioButton6.Text = "Blue";
            radioButton7.Visible = true; //Intensity
            if (imgData.img != null)
            {
                pictureBox2.Image = imgData.drawImage("RGB");
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
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (imgData.img != null)
            {
                if (radioButton1.Checked)
                {
                    pictureBox2.Image = imgData.drawImage("RGB");
                }
                else
                {
                    pictureBox2.Image = imgData.drawImage("HSV");
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
                }
                else
                {
                    pictureBox2.Image = imgData.drawImage("H");
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
                }
                else
                {
                    pictureBox2.Image = imgData.drawImage("S");
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
                }
                else
                {
                    pictureBox2.Image = imgData.drawImage("V");
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
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                pictureBox2.Image.Save(saveFileDialog1.FileName);
        }

        private void button3_Click(object sender, EventArgs e)
        {

            Bitmap bmp = (Bitmap)pictureBox1.Image.Clone();
            imgData.readImage(bmp);
            pictureBox2.Image = imgData.drawImage("Invert");
        }
    }
}
