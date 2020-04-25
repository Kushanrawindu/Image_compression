using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace ImageCompression
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Compress(Image srcImg, int imgQuality, string savePath)
        {
            try
            {
                ImageCodecInfo jpgCodec = null;

                EncoderParameter imgQualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, imgQuality);

                ImageCodecInfo[] allCodecs = ImageCodecInfo.GetImageEncoders();

                EncoderParameters codecParam = new EncoderParameters(1);
                codecParam.Param[0] = imgQualityParam;

                for (int x = 0; x < allCodecs.Length; x++)
                {
                    if (allCodecs[x].MimeType == "image/jpeg")
                    {
                        jpgCodec = allCodecs[x];
                        break;
                    }
                }

                srcImg.Save(savePath, jpgCodec, codecParam);

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Title = "Select image";
            open.Filter = "Png Images(*.png)|*.png|Jpeg Images(*.jpg)|*.jpg";
            open.Filter += "|Bitmap Images(*.bmp)|*.bmp";
            open.FilterIndex = 1;

            if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox1.Text = open.FileName;
                Image img = Image.FromFile(textBox1.Text);
                pictureBox1.Image = img;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Please select image first");
            }
            else if (textBox1.Text.Contains(".jpg"))
            {
                Compress(Image.FromFile(textBox1.Text), 30, textBox1.Text.Insert(textBox1.Text.Length - 4, "JPEG image compressed"));
                MessageBox.Show("Image compressed");
            }
            else 
            {
                string cmp = textBox1.Text.Insert(textBox1.Text.Length - 4, "JPEG image compressed");
                string c = "abc";

                Compress(Image.FromFile(textBox1.Text), 30, textBox1.Text.Insert(textBox1.Text.Length - 4, "JPEG image compressed").Substring(0, cmp.Length - 4) + ".jpg");
                MessageBox.Show("Image compressed");
            }
        }


    }
}
