using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace ProjectGlass
{
    public partial class Form_breake_glass : Form
    {
        enum Dire : byte
        {
            Up = 1,
            Rigth = 2,
            Down = 3,
            Left = 4
        }
        Point pos;
        int w;
        int h;
        int n;
        int scale;
        Bitmap bitmap;
        Graphics g;
        Pen pen;
        Random r = new Random();
        Point pos_hammer;
        bool breake_mode = false;
        bool glass_full = false;
        int step = 1;
        int step_sup;
        byte num;
        public short[,] array_broken;
        short[,] array_dire;
        short[] arr_up;
        short[] arr_right;
        short[] arr_down;
        short[] arr_left;

        SoundPlayer sound1 = new SoundPlayer(Properties.Resources.Breake);
        SoundPlayer sound2 = new SoundPlayer(Properties.Resources.ibreakeglass);
        public Form_breake_glass(Form f)
        {
            InitializeComponent();
            InitValues();
            PinHandlers();
        }
        private void InitValues()
        {
            
            groupBox1.Visible = false;
            numericUpDown_glass_width.Value = Properties.Settings.Default.glass_creat_width;
            numericUpDown_glass_height.Value = Properties.Settings.Default.glass_creat_height;
            numericUpDown_holst_W.Value = Properties.Settings.Default.draw_width;
            numericUpDown_holst_H.Value = Properties.Settings.Default.draw_height;
            //DrawGlass();
        }
        private void PinHandlers()
        {

            button_new_glass.Click += (s, n) => { breake_mode = false; groupBox1.Visible = false; DrawGlass(); };

            button_left.Click += (s, n) => { SetPosHam(1, -step); };
            button_up.Click += (s, n) => { SetPosHam(2, +step); };
            button_right.Click += (s, n) => { SetPosHam(1, +step); };
            button_down.Click += (s, n) => { SetPosHam(2, -step); };

            button_hit.Click += (s, n) => { CreateBrokenGlass(); sound1.PlaySync(); sound2.Play(); };

            numericUpDown_hit_x.ValueChanged += (s, n) => { pos_hammer = new Point((int)numericUpDown_hit_x.Value, pos_hammer.Y); DrawGlass(); };
            numericUpDown_hit_y.ValueChanged += (s, n) => { pos_hammer = new Point(pos_hammer.X, (int)numericUpDown_hit_y.Value); DrawGlass(); };
        }

        private void DrawGlass()
        {

            if (bitmap != null)
            {
                bitmap.Dispose();
                pictureBox1.Image.Dispose();
            }

            Form_main f = new Form_main();
            scale = 1;
            w = (int)numericUpDown_glass_width.Value;
            h = (int)numericUpDown_glass_height.Value;
            bitmap = new Bitmap((int)numericUpDown_holst_W.Value, (int)numericUpDown_holst_H.Value);
            g = Graphics.FromImage(bitmap);
            pen = new Pen(Properties.Settings.Default.pen1_color, Properties.Settings.Default.pen1_value);
            int max;
            if (w > h) max = w; else max = h;

            if (bitmap.Width < bitmap.Height) scale = f.SetScale(bitmap.Width, max);
            else scale = f.SetScale(bitmap.Height, max);

            pos = new Point((bitmap.Width - 1 - (w * scale)) / 2, (bitmap.Height - 1 - (h * scale)) / 2);

            g.Clear(Properties.Settings.Default.g1_color);
            g.DrawRectangle(pen, pos.X, pos.Y, w * scale, h * scale);
            bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
            if (breake_mode == true)
            {
                if (checkBox1.Checked == true)
                g.DrawImage(Properties.Resources.Hamer, pos_hammer.X * scale + pos.X, bitmap.Height - (pos_hammer.Y * scale + pos.Y + 256));
                g.DrawEllipse(new Pen(Color.Red, 5), pos_hammer.X * scale + pos.X, bitmap.Height - (pos_hammer.Y * scale +pos.Y), 1, 1);
                numericUpDown_hit_x.Value = pos_hammer.X;
                numericUpDown_hit_y.Value = pos_hammer.Y;
            }

            pictureBox1.Image = bitmap;

            pictureBox1.Refresh();
            glass_full = true;
            f.Dispose();
            GC.Collect();




        }
        private void SortArrShort(ref short[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = i; j < arr.Length; j++)
                {
                    try
                    {
                        if (arr[i] > arr[j])
                        {
                            short min = arr[i];
                            arr[i] = arr[j];
                            arr[j] = min;
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Ошибка");
                    }
                    
                }

            }
        }
        private void CreateBrokenGlass()
        {
            n = (int)numericUpDown_count_shards.Value;
            array_broken = new short[n, 6];
            array_dire = new short[n-4, 1];
            short[] shard_value = new short[n];
            short[] shard_value_sup = new short[n];
            int up = w;
            int right = h;
            int down = w;
            int left = h;

            int w1 = 1;
            int w2 = 1;
            int w3 = 1;
            int w4 = 1;

            short[] arr_w1;
            short[] arr_w2;
            short[] arr_w3;
            short[] arr_w4;
            byte stor;
            bool restart;

            int line = (up * 2) + (right * 2);
            for (int i = 3; i < n; i++)
            {
                stor = (byte)r.Next(4);
                stor += 1;
                switch (stor)
                {
                    case 1:
                        //array_dire[i, 0] = 1;
                        w1 += 1;
                        break;
                    case 2:
                        //array_dire[i, 0] = 2;
                        w2 += 1;
                        break;
                    case 3:
                        //array_dire[i, 0] = 3;
                        w3 += 1;
                        break;
                    case 4:
                        //array_dire[i, 0] = 4;
                        w4 += 1;
                        break;
                }

            }

            arr_w1 = new short[w1-1];
            arr_w2 = new short[w2-1];
            arr_w3 = new short[w3-1];
            arr_w4 = new short[w4-1];
            for (int i = 0; i < w1-1; i++) arr_w1[i] = Convert.ToInt16(1 + r.Next(w - 1));
            for (int i = 0; i < w2-1; i++) arr_w2[i] = Convert.ToInt16(1 + r.Next(h - 1));
            for (int i = 0; i < w3-1; i++) arr_w3[i] = Convert.ToInt16(1 + r.Next(w - 1));
            for (int i = 0; i < w4-1; i++) arr_w4[i] = Convert.ToInt16(1 + r.Next(h - 1));
            SortArrShort(ref arr_w1);
            SortArrShort(ref arr_w2);
            SortArrShort(ref arr_w3);
            SortArrShort(ref arr_w4);
            for (int i = 0; i < n; i++)
            {
                array_broken[i, 0] = (short)pos_hammer.X;
                array_broken[i, 1] = (short)pos_hammer.Y;
            }
            int num_shard = 0;
            int s(int num)
            {
                num = num - 1;
                return num;
            }
            //------------------------------------------------------------
            if (arr_w1.Length == 1)
            {
                array_broken[num_shard, 2] = 0;
                array_broken[num_shard, 3] = (short)h;
                array_broken[num_shard, 4] = (short)w;
                array_broken[num_shard, 5] = (short)h;
                num_shard += 1;
            }
            else
            {
                for (int i = 0;  i < w1; i++)
                {
                    if (i == 0)
                    {
                        array_broken[num_shard, 2] = 0;
                        array_broken[num_shard, 3] = (short)h;

                    }
                    else
                    {
                        array_broken[num_shard, 2] = array_broken[num_shard-1, 4];
                        array_broken[num_shard, 3] = array_broken[num_shard-1, 5];
                    }

                    if (i == w1-1)
                    {
                        array_broken[num_shard, 4] = (short)w;
                        array_broken[num_shard, 5] = (short)h;
                    }
                    else
                    {
                        
                        array_broken[num_shard, 4] = arr_w1[i];
                        array_broken[num_shard, 5] = (short)h;
                    }
                    num_shard += 1;
                }
            }
            
            //MessageArray();
            //----------------------------------------------------------
            if (arr_w2.Length == 1)
            {
                array_broken[num_shard, 2] = (short)w;
                array_broken[num_shard, 3] = (short)h;
                array_broken[num_shard, 4] = (short)w;
                array_broken[num_shard, 5] = 0;
                num_shard += 1;
            }
            else
            {
                for (int i = 0; i < w2; i++)
                {
                    if (i == 0)
                    {
                        array_broken[num_shard, 2] = (short)w;
                        array_broken[num_shard, 3] = (short)h;

                    }
                    else
                    {
                        array_broken[num_shard, 2] = array_broken[num_shard - 1, 4];
                        array_broken[num_shard, 3] = array_broken[num_shard - 1, 5];
                    }

                    if (i == w2 - 1)
                    {
                        array_broken[num_shard, 4] = (short)w;
                        array_broken[num_shard, 5] = 0;
                    }
                    else
                    {
                        
                        array_broken[num_shard, 4] = (short)w;
                        array_broken[num_shard, 5] = arr_w2[i];
                    }
                    num_shard += 1;
                }
            }
            //------------------------------------------------------
            if (arr_w3.Length == 1)
            {
                array_broken[num_shard, 2] = (short)w;
                array_broken[num_shard, 3] = 0;
                array_broken[num_shard, 4] = 0;
                array_broken[num_shard, 5] = 0;
                num_shard += 1;
            }
            else
            {
                for (int i = 0; i < w3; i++)
                {
                    if (i == 0)
                    {
                        array_broken[num_shard, 2] = (short)w;
                        array_broken[num_shard, 3] = 0;

                    }
                    else
                    {
                        array_broken[num_shard, 2] = array_broken[num_shard - 1, 4];
                        array_broken[num_shard, 3] = array_broken[num_shard - 1, 5];
                    }
                    if (i == w3 - 1)
                    {
                        array_broken[num_shard, 4] = 0;
                        array_broken[num_shard, 5] = 0;
                    }
                    else
                    {
                        
                        array_broken[num_shard, 4] = arr_w3[i];
                        array_broken[num_shard, 5] = 0;
                    }
                    num_shard += 1;
                }
            }
            //----------------------------------------------------------
            if (arr_w4.Length == 1)
            {
                //if (num_shard == n) return;
                array_broken[num_shard, 2] = 0;
                array_broken[num_shard, 3] = 0;
                array_broken[num_shard, 4] = 0;
                array_broken[num_shard, 5] = (short)h;
                num_shard += 1;
            }
            else
            {
                for (int i = 0; i < w4; i++)
                {
                    if (i == 0)
                    {
                        array_broken[num_shard, 2] = 0;
                        array_broken[num_shard, 3] = 0;
                    }
                    else
                    {
                        array_broken[num_shard, 2] = array_broken[num_shard - 1, 4];
                        array_broken[num_shard, 3] = array_broken[num_shard - 1, 5];
                    }

                    if (i == w4 - 1)
                    {
                        array_broken[num_shard, 4] = 0;
                        array_broken[num_shard, 5] = (short)h;
                    }
                    else
                    {
                        
                        array_broken[num_shard, 4] = 0;
                        array_broken[num_shard, 5] = arr_w4[i];
                    }
                    num_shard += 1;
                    
                }
            }
            MessageBox.Show($"{num_shard}");
            //MessageArray();
            DrawbBrokenGlass();
        }
        private void MessageArray()
        {
            string str = ($"{array_broken[0, 0]} {array_broken[0, 1]} {array_broken[0, 2]} {array_broken[0, 3]} {array_broken[0, 4]} {array_broken[0, 5]}" +
    $"\r\n{array_broken[1, 0]} {array_broken[1, 1]} {array_broken[1, 2]} {array_broken[1, 3]} {array_broken[1, 4]} {array_broken[1, 5]}" +
    $"\r\n{array_broken[2, 0]} {array_broken[2, 1]} {array_broken[2, 2]} {array_broken[2, 3]} {array_broken[2, 4]} {array_broken[2, 5]}" +
    $"\r\n{array_broken[3, 0]} {array_broken[3, 1]} {array_broken[3, 2]} {array_broken[3, 3]} {array_broken[3, 4]} {array_broken[3, 5]}" +
    $"\r\n{array_broken[4, 0]} {array_broken[4, 1]} {array_broken[4, 2]} {array_broken[4, 3]} {array_broken[4, 4]} {array_broken[4, 5]}");
            MessageBox.Show(str);
        }
        private void DrawbBrokenGlass()
        {
            if (bitmap != null)
            {
                bitmap.Dispose();
                pictureBox1.Image.Dispose();
            }

            Form_main f = new Form_main();
            scale = 1;
            w = (int)numericUpDown_glass_width.Value;
            h = (int)numericUpDown_glass_height.Value;
            bitmap = new Bitmap((int)numericUpDown_holst_W.Value, (int)numericUpDown_holst_H.Value);
            g = Graphics.FromImage(bitmap);
            pen = new Pen(Properties.Settings.Default.pen1_color, Properties.Settings.Default.pen1_value);
            int max;
            if (w > h) max = w; else max = h;

            if (bitmap.Width < bitmap.Height) scale = f.SetScale(bitmap.Width, max);
            else scale = f.SetScale(bitmap.Height, max);

            pos = new Point((bitmap.Width - 1 - (w * scale)) / 2, (bitmap.Height - 1 - (h * scale)) / 2);

            g.Clear(Properties.Settings.Default.g1_color);

            for (int i = 0; i < n; i++)
            {
                g.DrawPolygon(pen,new Point[] { 
                    new Point(array_broken[i, 0] * scale + pos.X, array_broken[i, 1] * scale + pos.Y), 
                    new Point(array_broken[i, 2] * scale + pos.X, array_broken[i, 3] * scale + pos.Y), 
                    new Point(array_broken[i, 4] * scale + pos.X, array_broken[i, 5] * scale + pos.Y)});
                
            }

            bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
            pictureBox1.Image = bitmap;

            pictureBox1.Refresh();
            glass_full = true;
            f.Dispose();
            //GC.Collect();
        }
        private void GiveArr2(short[,] arr) //delete
        {
            string str = "";
            for (int i = 0; i < n; i++)
            {
                str += $"{arr[i, 0]} {arr[i, 1]} {arr[i, 2]} {arr[i, 3]} {arr[i, 4]} {arr[i, 5]}\n";
            }
            MessageBox.Show(str);

        }
        private void SetPosHam(byte n, int step)
        {
            if (n == 1)
            {
                pos_hammer = new Point(pos_hammer.X + step, pos_hammer.Y);
                 
            }
            else
            {
                pos_hammer = new Point(pos_hammer.X, pos_hammer.Y + step);
            }
            if (pos_hammer.X >= w) pos_hammer.X = (int)numericUpDown_hit_x.Maximum;
            if (pos_hammer.X < 1) pos_hammer.X = 1;

            if (pos_hammer.Y >= h) pos_hammer.Y = (int)numericUpDown_hit_y.Maximum;
            if (pos_hammer.Y < 1) pos_hammer.Y = 1;

            DrawGlass();
        }


        private void button_take_hammer_Click(object sender, EventArgs e)
        {
            if (!glass_full) return;
            groupBox1.Visible = true;
            
            breake_mode = true;
            pos_hammer = new Point(w / 2, h / 2);
            numericUpDown_hit_x.Value = pos_hammer.X;
            numericUpDown_hit_x.Maximum = w - 1;

            numericUpDown_hit_y.Value = pos_hammer.Y;
            numericUpDown_hit_y.Maximum = h - 1;
            DrawGlass();
        }


        private void numericUpDown_step_ValueChanged(object sender, EventArgs e)
        {
            step = (int)numericUpDown_step.Value;
        }
        
        private void button_random_shard_pos_Click(object sender, EventArgs e)
        {
            short max_x = 0;
            short min_x = 0;
            short max_y = 0;
            short min_y = 0;

            short shir = Math.Abs(Convert.ToInt16(max_x - min_x));
            short post = Math.Abs(Convert.ToInt16(max_y - min_y));

            short r_w = (short)r.Next(shir);
            short r_h = (short)r.Next(post);
            for (int i = 0; i < n; i++)
            {
                

                if (array_broken[i, 0] >= array_broken[i, 2] && array_broken[i, 0] >= array_broken[i, 4]) max_x = array_broken[i, 0];
                if (array_broken[i, 2] >= array_broken[i, 4] && array_broken[i, 2] >= array_broken[i, 0]) max_x = array_broken[i, 2];
                if (array_broken[i, 4] >= array_broken[i, 0] && array_broken[i, 4] >= array_broken[i, 2]) max_x = array_broken[i, 4];

                if (array_broken[i, 0] <= array_broken[i, 2] && array_broken[i, 0] <= array_broken[i, 4]) min_x = array_broken[i, 0];
                if (array_broken[i, 2] <= array_broken[i, 4] && array_broken[i, 2] <= array_broken[i, 0]) min_x = array_broken[i, 2];
                if (array_broken[i, 4] <= array_broken[i, 0] && array_broken[i, 4] <= array_broken[i, 2]) min_x = array_broken[i, 4];

                if (array_broken[i, 1] >= array_broken[i, 3] && array_broken[i, 1] >= array_broken[i, 5]) max_y = array_broken[i, 1];
                if (array_broken[i, 3] >= array_broken[i, 5] && array_broken[i, 3] >= array_broken[i, 1]) max_y = array_broken[i, 3];
                if (array_broken[i, 5] >= array_broken[i, 1] && array_broken[i, 5] >= array_broken[i, 3]) max_y = array_broken[i, 5];

                if (array_broken[i, 1] <= array_broken[i, 3] && array_broken[i, 1] <= array_broken[i, 5]) min_y = array_broken[i, 1];
                if (array_broken[i, 3] <= array_broken[i, 5] && array_broken[i, 3] <= array_broken[i, 1]) min_y = array_broken[i, 3];
                if (array_broken[i, 5] <= array_broken[i, 1] && array_broken[i, 5] <= array_broken[i, 3]) min_y = array_broken[i, 5];

                
                shir = Math.Abs(Convert.ToInt16(max_x - min_x));
                post = Math.Abs(Convert.ToInt16(max_y - min_y));
                r_w = (short)r.Next(shir);
                r_h = (short)r.Next(post);

                array_broken[i, 0] += Math.Abs(Convert.ToInt16(array_broken[i, 0] - r_w));
                array_broken[i, 1] += Math.Abs(Convert.ToInt16(array_broken[i, 1] - r_h));
                array_broken[i, 2] += Math.Abs(Convert.ToInt16(array_broken[i, 2] - r_w));
                array_broken[i, 3] += Math.Abs(Convert.ToInt16(array_broken[i, 3] - r_h));
                array_broken[i, 4] += Math.Abs(Convert.ToInt16(array_broken[i, 4] - r_w));
                array_broken[i, 5] += Math.Abs(Convert.ToInt16(array_broken[i, 5] - r_h));
            }
            
            g.Clear(Properties.Settings.Default.g1_color);

            for (int i = 0; i < n; i++)
            {
                

                g.DrawPolygon(pen, new Point[] {
                    new Point(array_broken[i, 0], array_broken[i, 1]),
                    new Point(array_broken[i, 2], array_broken[i, 3]),
                    new Point(array_broken[i, 4], array_broken[i, 5])});

            }
            for (int i = 0; i < n; i++)
            {

            }
            bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
            pictureBox1.Image = bitmap;

            pictureBox1.Refresh();
            
            //if (w > h) max = w; else max = h;

            //if (bitmap.Width < bitmap.Height) scale = SetScale(bitmap.Width, max);
            //else scale = SetScale(bitmap.Height, max);

            //pos = new Point((bitmap.Width - 1 - (w * scale)) / 2, (bitmap.Height - 1 - (h * scale)) / 2);

            //g.Clear(Properties.Settings.Default.g1_color);

            //for (int i = 0; i < n; i++)
            //{
            //    g.DrawPolygon(pen, new Point[] {
            //        new Point(array_broken[i, 0] * scale + pos.X, array_broken[i, 1] * scale + pos.Y),
            //        new Point(array_broken[i, 2] * scale + pos.X, array_broken[i, 3] * scale + pos.Y),
            //        new Point(array_broken[i, 4] * scale + pos.X, array_broken[i, 5] * scale + pos.Y)});

            //}

            //bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
            //pictureBox1.Image = bitmap;

            //pictureBox1.Refresh();
        }

        private void button_load_glass_Click(object sender, EventArgs e)
        {
            Form_main main = this.Owner as Form_main;
            if (array_broken == null) return;
            main.Write_from_Array(array_broken, ref main.textBox1);
            Close();
        }
    }



}