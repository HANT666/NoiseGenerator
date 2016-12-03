using System;
using System.Drawing;
using System.Windows.Forms;

namespace NoiseGenerator
{
    public partial class Form1 : Form
    {
        int min, max, size, freq, octaves, octavesPerlin;
        float persistence;
        int[,] data;
        float[,] dataFloat;

        MyNoise noise;
        Perlin2D p2D;

        public Form1()
        {
            InitializeComponent();

            min = 0;
            max = 255;

            size = 257;
            freq = 20;
            octaves = 0;

            octavesPerlin = 5;
            persistence = 8.0f;

            textBox1.Text = Convert.ToString(min);
            textBox2.Text = Convert.ToString(max);
            textBox3.Text = Convert.ToString(freq);

            textBox5.Text = Convert.ToString(octavesPerlin);
            textBox6.Text = Convert.ToString(persistence);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // ввод диапазона оттенков серого от 0 до 255
            min = Int32.Parse(textBox1.Text);
            max = Int32.Parse(textBox2.Text);

            noise = new MyNoise(size, min, max);
            // генерация белого шума
            Draw(noise.Generate3Float(), pictureBox1);
            button2.Enabled = true;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            // ввод частоты интерполяции
            freq = Int32.Parse(textBox3.Text);

            // отрисовка интерполяций
            Draw(noise.Linear_Interpolate1_Float(freq), pictureBox2);
            Draw(noise.Cos_Interpolate1_Float(freq), pictureBox3);
            //Draw(noise.Random_Interpolate1_Float(freq), pictureBox3);
            button3.Enabled = true;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Draw(noise.Smooth1(), pictureBox2);
            Draw(noise.Smooth2(), pictureBox3);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            octavesPerlin = Int32.Parse(textBox5.Text);
            persistence = float.Parse(textBox6.Text);

            p2D = new Perlin2D(size, 40);
            Draw(p2D.GetData(octavesPerlin, persistence), pictureBox1);
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            try
            {
                if (Int32.Parse(textBox1.Text) < 0)
                    textBox1.Text = "0";
                if (Int32.Parse(textBox1.Text) > 255)
                    textBox1.Text = "255";
                if (Int32.Parse(textBox1.Text) > Int32.Parse(textBox1.Text))
                    textBox1.Text = textBox2.Text;
            }
            catch (System.FormatException)
            {
                textBox1.Text = "0";
                //throw;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            try
            {
                if (Int32.Parse(textBox2.Text) < 0)
                    textBox2.Text = "0";
                if (Int32.Parse(textBox2.Text) > 255)
                    textBox2.Text = "255";
                if (Int32.Parse(textBox1.Text) > Int32.Parse(textBox2.Text))
                    textBox2.Text = textBox1.Text;
            }
            catch (System.FormatException)
            {
                textBox2.Text = "255";
                //throw;
            }
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            try
            {
                if (Int32.Parse(textBox5.Text) < 1)
                    textBox5.Text = "1";
            }
            catch (System.FormatException)
            {
                textBox5.Text = "1";
                //throw;
            }
        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            try
            {
                if (float.Parse(textBox6.Text) < 0)
                    textBox6.Text = "0,5";
            }
            catch (System.FormatException)
            {
                textBox6.Text = "0,5";
                //throw;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox4.Enabled = true;
                textBox4.Focus();

                textBox3.Enabled = false;
                label6.Enabled = false;
            }
            else
            {
                textBox4.Enabled = false;

                textBox3.Enabled = true;
                label6.Enabled = true;
            }
        }
        
        private void textBox3_Leave(object sender, EventArgs e)
        {
            try
            {
                if (Int32.Parse(textBox3.Text) < 1)
                    textBox3.Text = "1";
            }
            catch (System.FormatException)
            {
                textBox3.Text = "20";
                //throw;
            }
            
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            try
            {
                if (Int32.Parse(textBox3.Text) < 1)
                    textBox4.Text = "1";
            }
            catch (System.FormatException)
            {
                textBox4.Text = "2";
                //throw;
            }
        }
        
        // отрисовка битмапа по массиву
        public void Draw(int[,] data)
        {
            //int[,] data = data;
            Bitmap bitmap = new Bitmap(size, size);
            Color color = Color.FromArgb(255, 255, 255);

            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    color = Color.FromArgb(data[x, y], data[x, y], data[x, y]);
                    bitmap.SetPixel(x, y, color);
                }
            }

            pictureBox1.Image = bitmap;
        }
        public void Draw(float[,] data, PictureBox pictureBox)
        {
            int d;
            Bitmap bitmap = new Bitmap(size, size);
            Color color = Color.FromArgb(255, 255, 255);

            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    d = (int)Math.Round(data[x, y]);
                    color = Color.FromArgb(d, d, d);
                    bitmap.SetPixel(x, y, color);
                }
            }

            pictureBox.Image = bitmap;
        }
        
        
    }
}
