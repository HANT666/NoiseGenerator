using System;
using System.Drawing;

namespace NoiseGenerator
{
    class MyNoise
    {
        int min, max, size;
        int[,] data;
        float[,] dataFloat, dataFloatBuff1, dataFloatBuff2;

        float treshold = 130;

        Random randomValue;

        public float Treshold
        {
            get
            {
                return treshold;
            }

            set
            {
                treshold = value;
            }
        }

        public float[,] DataFloatBuff1
        {
            get
            {
                return dataFloatBuff1;
            }

            set
            {
                dataFloatBuff1 = value;
            }
        }

        public float[,] DataFloatBuff2
        {
            get
            {
                return dataFloatBuff2;
            }

            set
            {
                dataFloatBuff2 = value;
            }
        }

        public MyNoise(int size, int min, int max)
        {
            data = new int[size, size];
            dataFloat = new float[size, size];
            this.size = size;
            this.min = min;
            this.max = max;

            randomValue = new Random();
        }
        public int[,] Generate1()
        {
            //min = Int32.Parse(textBox1.Text);
            //max = Int32.Parse(textBox2.Text);

            int size = 500;
            Bitmap bitmap = new Bitmap(size, size);

            int value = 0;
            int value1 = 0;
            int value2 = 0;
            Random randomValue = new Random();
            Color color = Color.FromArgb(255, 255, 255);

            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    value = randomValue.Next(min, max);
                    value1 = randomValue.Next(min, max);
                    value2 = randomValue.Next(min, max);
                    //color = Color.FromArgb(value, value, value);
                    color = Color.FromArgb(value, value1, value2);
                    bitmap.SetPixel(x, y, color);
                }
            }

            return data;
            //pictureBox1.Image = bitmap;
        }
        public int[,] Generate2()
        {


            int size = 500;
            Bitmap bitmap = new Bitmap(size, size);

            int value = 0;
            int value1 = 0;
            int value2 = 0;
            Random randomValue = new Random();
            Color color = Color.FromArgb(255, 255, 255);


            //Graphics gr = pictureBox1.CreateGraphics();
            //gr.Clear(Color.Yellow);
            Pen pen = new Pen(Color.Black);
            //gr.DrawLine(pen, 100, 100, 200, 200);

            Brush brush;
            /*brush = 
            gr.FillRectangle(brush, 30, 30, 30, 30);*/

            /*for (int i = 0; i < size; i++)
            {

            }*/

            /*for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    value = randomValue.Next(min, max);
                    value1 = randomValue.Next(min, max);
                    value2 = randomValue.Next(min, max);
                    //color = Color.FromArgb(value, value, value);
                    color = Color.FromArgb(value, value, value);
                    bitmap.SetPixel(x, y, color);
                }
            }*/

            //pictureBox1.Image = bitmap;
            return data;
        }

        public int[,] Generate3()
        {
            data = new int[size, size];

            int value = 0;
            //int value1 = 0;
            //int value2 = 0;
            Random randomValue = new Random();

            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {

                    value = randomValue.Next(min, max);
                    //value1 = randomValue.Next(min, max);
                    //value2 = randomValue.Next(min, max);
                    //color = Color.FromArgb(value, value, value);
                    data[x, y] = value;
                }
            }


            //Warpage(data, size);

            //Draw();
            return data;

        }

        public float[,] Generate3Float()
        {
            dataFloat = new float[size, size];

            int value = 0;
            //int value1 = 0;
            //int value2 = 0;
            Random randomValue = new Random();


            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {

                    value = randomValue.Next(min, max);
                    dataFloat[x, y] = (float)value;
                }
            }

            //Warpage(data, size);

            return dataFloat;

        }

        public void Warpage(int[,] data, int size)
        {
            Random randomValue = new Random();

            int x0, y0, height, radius;

            for (int i = 0; i < 1; i++)
            {
                x0 = randomValue.Next(0, size);
                y0 = randomValue.Next(0, size);
                height = randomValue.Next(150, 150);
                radius = randomValue.Next(0, 50);
                for (int y = 0; y < size; y++)
                {
                    for (int x = 0; x < size; x++)
                    {
                        float d = radius * radius - ((x0 - x) * (x0 - x) + (y0 - y) * (y0 - y));
                        //float d = 1.0f / ( 1 + ((x0 - x) * (x0 - x) + (y0 - y) * (y0 - y)) );
                        data[x, y] += (int)Math.Round(d * height);
                        if (data[x, y] < 0)
                            data[x, y] = 0;
                        if (data[x, y] > 255)
                            data[x, y] = 255;
                    }
                }
            }


        }

        public void Linear_Interpolate(int x1, int y1, int x2, int y2)
        {
            int newX = Mid(x1, x2); //(int)Math.Floor((x1 + x2) / 2.0);
            int newY = Mid(y1, y2); //(int)Math.Floor((y1 + y2) / 2.0);
            if (newX == x1 && newY == y1 || newX == x2 && newY == y2)
                //if (x1 == x2 ||  y1 == y2)
                return;
            data[newX, y1] = Mid(data[x1, y1], data[x2, y1]); //(int)Math.Floor((data[x1, y1] + data[x2, y1]) / 2.0);
            data[newX, y2] = Mid(data[x1, y2], data[x2, y2]); //(int)Math.Floor((data[x1, y2] + data[x2, y2]) / 2.0);
            data[x1, newY] = Mid(data[x1, y1], data[x1, y2]); //(int)Math.Floor((data[x1, y1] + data[x1, y2]) / 2.0);
            data[x2, newY] = Mid(data[x2, y1], data[x2, y2]); //(int)Math.Floor((data[x2, y1] + data[x2, y2]) / 2.0);
            data[newX, newY] = Mid(data[newX, y1], data[newX, y2]); //(int)Math.Floor((data[newX, y1] + data[newX, y2]) / 2.0);

            Linear_Interpolate(x1, y1, newX, newY);
            Linear_Interpolate(newX, y1, x2, newY);
            Linear_Interpolate(x1, newY, newX, y2);
            Linear_Interpolate(newX, newY, x2, y2);
            //return a * (1 - x) + b * x;
        }

        public void Linear_Interpolate1(int freq)
        {
            float g = size / (float)freq;
            for (int i = 0; i < freq; i++)
            {
                for (int j = 0; j < freq; j++)
                {
                    Linear_Interpolate((int)Math.Round(j * g), (int)Math.Round(i * g), (int)Math.Round((j + 1) * g - 1), (int)Math.Round((i + 1) * g - 1));
                }
            }
        }

        public int Mid(int v1, int v2)
        {
            return (int)Math.Floor((v1 + v2) / 2.0);
        }

        public void Linear_Interpolate_Float(int x1, int y1, int x2, int y2)
        {
            // находим координаты посередине
            int newX = Mid(x1, x2);
            int newY = Mid(y1, y2);

            if (newX == x1 && newY == y1 || newX == x2 && newY == y2)
                return;

            DataFloatBuff1[newX, y1] = (DataFloatBuff1[x1, y1] + DataFloatBuff1[x2, y1]) / 2.0f;
            DataFloatBuff1[newX, y2] = (DataFloatBuff1[x1, y2] + DataFloatBuff1[x2, y2]) / 2.0f;
            DataFloatBuff1[x1, newY] = (DataFloatBuff1[x1, y1] + DataFloatBuff1[x1, y2]) / 2.0f;
            DataFloatBuff1[x2, newY] = (DataFloatBuff1[x2, y1] + DataFloatBuff1[x2, y2]) / 2.0f;
            DataFloatBuff1[newX, newY] = (DataFloatBuff1[newX, y1] + DataFloatBuff1[newX, y2]) / 2.0f;

            // рекурсивный вызов для прохода всех четвертей
            Linear_Interpolate_Float(x1, y1, newX, newY);
            Linear_Interpolate_Float(newX, y1, x2, newY);
            Linear_Interpolate_Float(x1, newY, newX, y2);
            Linear_Interpolate_Float(newX, newY, x2, y2);
        }

        public float[,] Linear_Interpolate1_Float(int freq)
        {
            /*dataFloatBuff1 = new float[size, size];

            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    dataFloatBuff1[x, y] = this.dataFloat[x, y];
                }
            }*/

            DataFloatBuff1 = (float[,])dataFloat.Clone();

            float g = (size - 1) / (float)freq;

            for (int i = 0; i < freq; i++)
            {
                for (int j = 0; j < freq; j++)
                {
                    Linear_Interpolate_Float((int)Math.Round(j * g), (int)Math.Round(i * g), (int)Math.Round((j + 1) * g), (int)Math.Round((i + 1) * g));
                }
            }
            return DataFloatBuff1;
        }

        public void Cos_Interpolate_Float(int x1, int y1, int x2, int y2)
        {
            double ft;
            float f;

            for (int x = x1 + 1; x < x2; x++)
            {
                ft = (((float)x - x1) / (x2 - x1) * Math.PI);
                f = (float)((1 - Math.Cos(ft)) * 0.5);
                DataFloatBuff2[x, y1] = DataFloatBuff2[x1, y1] * (1.0f - f) + DataFloatBuff2[x2, y1] * f;

                ft = (((float)x - x1) / (x2 - x1) * Math.PI);
                f = (float)((1 - Math.Cos(ft)) * 0.5);
                DataFloatBuff2[x, y2] = DataFloatBuff2[x1, y2] * (1.0f - f) + DataFloatBuff2[x2, y2] * f;
            }

            for (int x = x1; x <= x2; x++)
            {
                for (int y = y1 + 1; y < y2; y++)
                {
                    ft = (((float)y - y1) / (y2 - y1) * Math.PI);
                    f = (float)((1 - Math.Cos(ft)) * 0.5);
                    DataFloatBuff2[x, y] = DataFloatBuff2[x, y1] * (1 - f) + DataFloatBuff2[x, y2] * f;
                }
            }

        }

        public float[,] Cos_Interpolate1_Float(int freq)
        {
            /*dataFloatBuff2 = new float[size, size];

            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    dataFloatBuff2[x, y] = this.dataFloat[x, y];
                }
            }*/

            DataFloatBuff2 = (float[,])dataFloat.Clone();

            float g = (size - 1) / (float)freq;

            for (int i = 0; i < freq; i++)
            {
                for (int j = 0; j < freq; j++)
                {
                    Cos_Interpolate_Float((int)Math.Round(j * g), (int)Math.Round(i * g), (int)Math.Round((j + 1) * g), (int)Math.Round((i + 1) * g));
                }
            }
            return DataFloatBuff2;
        }

        public void Perlin_Interpolate1_Float(int freq)
        {
            //Perlin2D perlin = new Perlin2D();

            float g = size / (float)freq;
            for (int i = 0; i < freq; i++)
            {
                for (int j = 0; j < freq; j++)
                {
                    //Linear_Interpolate_Float((int)Math.Round(j * g), (int)Math.Round(i * g), (int)Math.Round((j + 1) * g - 1), (int)Math.Round((i + 1) * g - 1));
                    /*for (int y = (int)Math.Round(i * g); y < (int)Math.Round((i + 1) * g); y++)
                        for (int x = (int)Math.Round(j * g); x < (int)Math.Round((j + 1) * g); x++)
                        {
                            dataFloat[x, y] = (perlin.Noise((x / (float)(size - 1)), (y / (float)(size - 1)), 10) + 1) * 127.5f;
                        }*/

                    for (int y = 0; y < (int)Math.Round(g); y++)
                        for (int x = 0; x < (int)Math.Round(g); x++)
                        {
                            //dataFloat[(int)Math.Round(j * g) + x, (int)Math.Round(i * g) + y] = (perlin.Noise((x / (g - 1)), (y / (g - 1)), 10) + 1) * 127.5f;
                        }
                }
            }
        }

        public void Random_Interpolate_Float(int x1, int y1, int x2, int y2)
        {
            int newX = Mid(x1, x2);
            int newY = Mid(y1, y2);

            if (newX == x1 && newY == y1 || newX == x2 && newY == y2)
                return;

            DataFloatBuff1[newX, y1] = randomValue.Next((int)Math.Min(DataFloatBuff1[x1, y1], DataFloatBuff1[x2, y1]), (int)Math.Max(DataFloatBuff1[x1, y1], DataFloatBuff1[x2, y1]));
            DataFloatBuff1[newX, y2] = randomValue.Next((int)Math.Min(DataFloatBuff1[x1, y2], DataFloatBuff1[x2, y2]), (int)Math.Max(DataFloatBuff1[x1, y2], DataFloatBuff1[x2, y2]));
            DataFloatBuff1[x1, newY] = randomValue.Next((int)Math.Min(DataFloatBuff1[x1, y1], DataFloatBuff1[x1, y2]), (int)Math.Max(DataFloatBuff1[x1, y1], DataFloatBuff1[x1, y2]));
            DataFloatBuff1[x2, newY] = randomValue.Next((int)Math.Min(DataFloatBuff1[x2, y1], DataFloatBuff1[x2, y2]), (int)Math.Max(DataFloatBuff1[x2, y1], DataFloatBuff1[x2, y2]));
            DataFloatBuff1[newX, newY] = randomValue.Next((int)Math.Min(DataFloatBuff1[newX, y1], DataFloatBuff1[newX, y2]), (int)Math.Max(DataFloatBuff1[newX, y1], DataFloatBuff1[newX, y2]));

            Random_Interpolate_Float(x1, y1, newX, newY);
            Random_Interpolate_Float(newX, y1, x2, newY);
            Random_Interpolate_Float(x1, newY, newX, y2);
            Random_Interpolate_Float(newX, newY, x2, y2);
        }

        public float[,] Random_Interpolate1_Float(int freq)
        {
            DataFloatBuff1 = new float[size, size];

            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    DataFloatBuff1[x, y] = this.dataFloat[x, y];
                }
            }
            float g = (size - 1) / (float)freq;

            for (int i = 0; i < freq; i++)
            {
                for (int j = 0; j < freq; j++)
                {
                    Random_Interpolate_Float((int)Math.Round(j * g), (int)Math.Round(i * g), (int)Math.Round((j + 1) * g), (int)Math.Round((i + 1) * g));
                }
            }
            return DataFloatBuff1;
        }

        // сглаживание для первого интерполированного массива
        public float[,] Smooth1()
        {
            float corners, sides, center;
            for (int y = 1; y < size - 1; y++)
            {
                for (int x = 1; x < size - 1; x++)
                {
                    corners = (DataFloatBuff1[x - 1, y - 1] + DataFloatBuff1[x + 1, y - 1] + DataFloatBuff1[x - 1, y + 1] + DataFloatBuff1[x + 1, y + 1]) / 16;
                    sides = (DataFloatBuff1[x, y - 1] + DataFloatBuff1[x + 1, y] + DataFloatBuff1[x, y + 1] + DataFloatBuff1[x - 1, y]) / 8;
                    center = DataFloatBuff1[x, y] / 4;
                    DataFloatBuff1[x, y] = corners + sides + center;
                }
            }

            return DataFloatBuff1;
        }

        // сглаживание для второго интерполированного массива
        public float[,] Smooth2()
        {
            float corners, sides, center;
            for (int y = 1; y < size - 1; y++)
            {
                for (int x = 1; x < size - 1; x++)
                {
                    corners = (DataFloatBuff2[x - 1, y - 1] + DataFloatBuff2[x + 1, y - 1] + DataFloatBuff2[x - 1, y + 1] + DataFloatBuff2[x + 1, y + 1]) / 16;
                    sides = (DataFloatBuff2[x, y - 1] + DataFloatBuff2[x + 1, y] + DataFloatBuff2[x, y + 1] + DataFloatBuff2[x - 1, y]) / 8;
                    center = DataFloatBuff2[x, y] / 4;
                    DataFloatBuff2[x, y] = corners + sides + center;
                }
            }

            return DataFloatBuff2;
        }

        public void Fractal(int octaves)
        {
            DataFloatBuff1 = new float[size, size];
            DataFloatBuff2 = new float[size, size];
            
            if (octaves == 1)
            {
                DataFloatBuff1 = SumArray(DataFloatBuff1, Linear_Interpolate1_Float(2));
                DataFloatBuff2 = SumArray(DataFloatBuff2, Cos_Interpolate1_Float(2));

                return;
            }

            for (int i = 0; i < octaves; i++)
            {
                DataFloatBuff1 = SumArray( DataFloatBuff1, Linear_Interpolate1_Float( (int)Math.Pow(2, (i + 1)) ) );
                DataFloatBuff2 = SumArray(DataFloatBuff2,     Cos_Interpolate1_Float( (int)Math.Pow(2, (i + 1)) ) );

                /*DataFloatBuff1 = SumArray(DataFloatBuff1, Linear_Interpolate1_Float(i + 1));
                DataFloatBuff2 = SumArray(DataFloatBuff2, Cos_Interpolate1_Float(i + 1));*/
            }
            int g = 6;
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    DataFloatBuff1[x, y] /= octaves;
                    DataFloatBuff2[x, y] /= octaves;
                }
            }
            
            //return dataFloatBuff1;
        }

        public float[,] SumArray(float[,] data1, float[,] data2)
        {
            float[,] data0 = new float[size, size];

            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {

                    data0[x, y] = data1[x, y] + data2[x, y];
                }
            }
            
            return data0;
        }

        public float[,] Function1()
        {
            float[,] data = (float[,])DataFloatBuff1.Clone();


            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    if (data[x, y] < Treshold)
                        data[x, y] = 0;
                    else
                        data[x, y] = 255;
                }
            }


            return data;
        }

        public float[,] Function2()
        {
            float[,] data = (float[,])DataFloatBuff2.Clone();


            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    if (data[x, y] < Treshold)
                        data[x, y] = 0;
                    else
                        data[x, y] = 255;
                }
            }


            return data;
        }
    }
}
