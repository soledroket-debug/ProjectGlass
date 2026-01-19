using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Media;



namespace ProjectGlass
{
    public partial class Form_main : Form
    {
        public enum Dire : byte
        {
            Up = 1,
            Down = 2,
            Left = 3,
            Rigth = 4
        }
        public enum Pos : byte
        {
            Left_down = 0,
            Center = 1
        }
        public enum ZoomScale : byte
        {
            Stok = 1,
            Zoom = 2
        }

        byte timer_tick;
        public string load_message_string = "Работает";
        public string load_task_string = "error message";

        Size form_size;
        bool form_Resize;
        bool thead_active = false;
        bool text_input_correct = false;
        bool text_output_correct = false;

        public int n_in = 0;
        public int n_out =0;
        int table_count_in = 6;
        int table_count_out = 6;
        Bitmap bit;
        Bitmap bitmap1;
        Bitmap bitmap2;
        Graphics g1;
        Graphics g2;
        Pen pen1;
        Pen pen2;
        public Pen pen3;

        public bool array_input_enebled = false;
        public bool array_output_enebled = false;
        short[,] array_input;
        short[,] array_in_draw;
        short[,] array_dire;
        short[,] array_main;
        public int scale_mode = 1;
        public int scale_in = 0;
        public int scale_out = 0;
        public int scale_sum_in = 1;
        public int scale_sum_out = 1;
        Point min_in_xy;
        Point max_in_xy;

        Bitmap bmp;

        Point min_in_xy_zero;
        Point max_in_xy_zero;
        
        short[,] array_output;
        short[,] array_out_draw;
        Point min_out_xy;
        Point max_out_xy;

        Point min_out_xy_zero;
        Point max_out_xy_zero;
        public Color[] colors_shards_input;
        public Color[] colors_shards_output;
        Random rand = new Random(0);
        
        public Form_main()
        {
            InitializeComponent();
            SetValues();
            PinHendler();
        }


        public void SetValues()
        {
            pen1 = new Pen(Properties.Settings.Default.pen1_color, Properties.Settings.Default.pen1_value);
            pen2 = new Pen(Properties.Settings.Default.pen2_color, Properties.Settings.Default.pen2_value);
            form_size = Size;
            button2.Enabled = Properties.Settings.Default.test_func_eneble;
            pen3 = new Pen(Properties.Settings.Default.Fill_glass_color,1);
            comboBox1.SelectedIndex = Properties.Settings.Default.mode_wheel;     
            BmpInit(3);
        }
        private void PinHendler()
        {
            for (int i = 0; i < Controls.Count; i++)
            {
                Controls[i].MinimumSize = new Size(Controls[i].Size.Width, Controls[i].Size.Height);
            }
            pic_input.MouseWheel += (s, n) => Zoom_1(s, n);
            pic_output.MouseWheel += (s, n) => Zoom_2(s, n);
            pictureBox_1_load.MouseEnter += (s, n) => { pictureBox_1_load.Image = Properties.Resources.Load2_; };
            pictureBox_1_load.MouseLeave += (s, n) => { pictureBox_1_load.Image = Properties.Resources.Load_1; };

            pictureBox_2_load.MouseEnter += (s, n) => { pictureBox_2_load.Image = Properties.Resources.Load2_; };
            pictureBox_2_load.MouseLeave += (s, n) => { pictureBox_2_load.Image = Properties.Resources.Load_1; };

            pictureBox1_trasher.Click += (s, n) => { textBox1.Text = "0"; };
            pictureBox2_trasher.Click += (s, n) => { textBox2.Text = "0"; };

            pictureBox_input_minus.Click += (s, n) => { RemoveShard(ref textBox1,ref table_count_in,n_in); };
            pictureBox_input_plus.Click += (s, n) => { AddShard(ref textBox1,n_in); ; };

            pictureBox_output_minus.Click += (s, n) => { RemoveShard(ref textBox2, ref table_count_out,n_out); };
            pictureBox_output_plus.Click += (s, n) => { AddShard(ref textBox2,n_out); };

            tabControl1.SelectedIndexChanged += (s, n) => { pictureBox_ShardDraw.Image = null; Draw_from_how_arr(1); Draw_from_how_arr(2); };

            groupBox1_input.Click += (s, n) => { groupBox1.Text = "Input"; };
            groupBox2_output.Click += (s, n) => { groupBox1.Text = "Output"; };

            comboBox1.SelectedIndexChanged += (s, n) => { Properties.Settings.Default.mode_wheel = (byte)comboBox1.SelectedIndex; };
            
            this.KeyDown += (s, n) => { if (n.KeyCode == Keys.F5) Draw_from_how_arr(3);
            };
            pictureBox_1_load.Click += (s, n) =>
            {
                if (array_input_enebled) Draw_from_how_arr(1);
                else pic_input.Image = pic_input.ErrorImage;
            };
            pictureBox_2_load.Click += (s, n) =>
            {
                if (array_output_enebled)
                {
                    if (Properties.Settings.Default.anim_enebled)
                    {
                        Draw_from_how_arr(2);
                        timer2.Enabled = true;
                        timer_tick = 0;
                    }
                    else Draw_from_how_arr(2);
                }
                else pic_output.Image = pic_output.ErrorImage;
            };

        }
        private void Message(Object sender, EventArgs e,ref TextBox tBox, ref short[,] arr, ref TextBox tBox_result, ref int i,ref int j)
        {
            try
            {
                int h = Convert.ToInt16(tBox.Text);
                tBox.BackColor = Color.White;
            }
            catch
            {
                tBox.BackColor = Color.Red;
                return;
            }
            arr[i, j] = Convert.ToInt16(tBox.Text);
            
            Write_from_Array(arr, ref tBox_result);
        }
        private void SendShard(Object sender, EventArgs e, ref short[,] arr, ref int i,in byte num_textBox)
        {
            textBoxX1.Text = $"{arr[i, 0]}";
            textBoxX2.Text = $"{arr[i, 2]}";
            textBoxX3.Text = $"{arr[i, 4]}";

            textBoxY1.Text = $"{arr[i, 1]}";
            textBoxY2.Text = $"{arr[i, 3]}";
            textBoxY3.Text = $"{arr[i, 5]}";

            if (num_textBox == 1) groupBox_draw_shard.Text = "Input";
            else groupBox_draw_shard.Text = "Output";

            label_num_draw_shard.Text = $"{i + 1}";
            DrawShard();
        }
        public void TableInit(short[,] arr, ref int count, ref TableLayoutPanel table,in bool correct_text_bool, byte num_textBox, int n)
        {
            
            table.Visible = false;
            table.Controls.Clear();
            count = 6;
            Label l1 = new Label(); l1.Text = "№" ; l1.TextAlign = ContentAlignment.TopCenter; table.Controls.Add(l1);
            Label l2 = new Label(); l2.Text = "X1"; l2.TextAlign = ContentAlignment.TopCenter; table.Controls.Add(l2);
            Label l3 = new Label(); l3.Text = "Y1"; l3.TextAlign = ContentAlignment.TopCenter; table.Controls.Add(l3);
            Label l4 = new Label(); l4.Text = "X2"; l4.TextAlign = ContentAlignment.TopCenter; table.Controls.Add(l4);
            Label l5 = new Label(); l5.Text = "Y2"; l5.TextAlign = ContentAlignment.TopCenter; table.Controls.Add(l5);
            Label l6 = new Label(); l6.Text = "X3"; l6.TextAlign = ContentAlignment.TopCenter; table.Controls.Add(l6);
            Label l7 = new Label(); l7.Text = "Y3"; l7.TextAlign = ContentAlignment.TopCenter; table.Controls.Add(l7);
            if (!correct_text_bool || n == 0)
            {
                table.Visible = true;
                return;
            }

            int n2 = arr.Length / 6;
            for (int i = 0; i < n2; i++)
            {
                Label l = new Label();
                l.Text = $"{i + 1}";
                l.Margin = new Padding(3, 5, 3, 3);
                l.Size = new Size(l.Size.Width, 27);
                l.TextAlign = ContentAlignment.TopCenter;
                table.Controls.Add(l);
                count++;
                int i2 = i;
                if (num_textBox == 1)
                    l.Click += (s, n1) => SendShard(s, n1, ref arr, ref i2,in num_textBox);
                else
                    l.Click += (s, n1) => SendShard(s, n1, ref arr, ref i2, in num_textBox);

                for (int j = 0; j < 6; j++)
                {
                    TextBox t = new TextBox();
                    t.Text = $"{arr[i, j]}";
                    t.Margin = new Padding(3, 3, 3, 3);
                    t.Size = new Size(t.Size.Width,27);
                    int i3 = i;
                    int j2 = j;
                    if (num_textBox == 1)
                    t.TextChanged += (s, n1) => Message(s,n1,ref t, ref arr, ref textBox1,ref  i3, ref  j2);
                    else
                    t.TextChanged += (s, n1) => Message(s, n1,ref t, ref arr, ref textBox2,ref  i3, ref j2);

                    table.Controls.Add(t);
                    count++;
                }
            }
            table.Visible = true;
        }
        public void RemoveShard(ref TextBox tBox, ref int count, int n)
        {
            string text = tBox.Text;
            if (n == 0) return;
            int l = 0;
            while (text[l] != '\n')
            {
                l++;
            }
            text = text.Remove(0, l);
            text = text.Insert(0, $"{n - 1}" + "\r");

            l = 1;
            while (text[text.Length-l] != '\n')
            {
                l++;
            }
            text = text.Remove(text.Length - (l+1), l+1);
            tBox.Text = text;
        }
        public void AddShard(ref TextBox tBox,int n)
        {
            string text = tBox.Text;
            text += "\r\n0 0 0 0 0 0";
            int l = 0;
            while (text[l] != '\n')
            {
                l++;
            }
            text = text.Remove(0,l);
            text = text.Insert(0,$"{n + 1}"+"\r");
            tBox.Text = text;
        }
        public void DrawArray(
                            ref Point min_xy,
                            ref Point max_xy,
                            ref Point min_xy_zero,
                            ref Point max_xy_zero,

                            ref short[,] arr,
                            ref short[,] arr_draw,


                            ref PictureBox pic,
                            ref Bitmap bit,
                            ref Graphics g,
                            ref Pen pen,


                          ref int scale_mode,
                          ref int scale_sup,
                          ref int scale_sum,
                          ref int n
                            )
        {

            if (n == 0) return;
            arr_draw = new short[n, 6];
            Array.Copy(arr, arr_draw, arr.Length);
            scale_mode = 1;
            scale_sum = 1;
            min_xy = Min_Max(min_xy, arr, true, n);
            max_xy = Min_Max(max_xy, arr, false,n);
            Point[] mi_ma_points;
            mi_ma_points = Min_Max_To_Zero(min_xy, max_xy, arr_draw);
            min_xy_zero = mi_ma_points[0];
            max_xy_zero = mi_ma_points[1];
            short x = Convert.ToInt16(-min_xy.X);
            short y = Convert.ToInt16(-min_xy.Y);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (j % 2 == 0) arr_draw[i, j] = Convert.ToInt16(arr_draw[i, j] + x);
                    else arr_draw[i, j] = Convert.ToInt16(arr_draw[i, j] + y);
                }
            }

            int max;
            int max_x = max_xy_zero.X - min_xy_zero.X;
            int max_y = max_xy_zero.Y - min_xy_zero.Y;
            if (max_xy_zero.X > max_xy_zero.Y) max = max_xy_zero.X;
            else max = max_xy_zero.Y;

            Point pos = new Point(0, 0);
            if (pic.Equals(pic_input))
            {
                if (Properties.Settings.Default.pic_g1_draw_zoom == (byte)ZoomScale.Zoom)
                {
                    if (bit.Width < bit.Height) scale_mode = SetScale(bit.Width, max);
                    else scale_mode = SetScale(bit.Height, max);
                }    
                else
                if (Properties.Settings.Default.pic_g1_draw_zoom == (byte)ZoomScale.Stok) scale_mode = 1;

                scale_sum = scale_mode + scale_sup;

                if (Properties.Settings.Default.pic_g1_pos == 1)
                pos = new Point((bit.Width - (max_x * scale_sum)) / 2,
                                (bit.Height - (max_y * scale_sum)) / 2);
                
                g.Clear(Properties.Settings.Default.g1_color);
                
            }
            else
            if (pic.Equals(pic_output))
            {
                if (Properties.Settings.Default.pic_g2_draw_zoom == (byte)ZoomScale.Zoom)
                {
                    if (bit.Width < bit.Height) scale_mode = SetScale(bit.Width, max);
                    else scale_mode = SetScale(bit.Height, max);
                }
                else
                if (Properties.Settings.Default.pic_g1_draw_zoom == (byte)ZoomScale.Stok) scale_mode = 1;
                
                if (Properties.Settings.Default.scale_one_to_one) scale_sum = scale_sum_in;
                else scale_sum = scale_mode + scale_sup;

                if (Properties.Settings.Default.pic_g2_pos == 1)
                pos = new Point((bit.Width - (max_x * scale_sum)) / 2,
                                (bit.Height - (max_y * scale_sum)) / 2);

                g.Clear(Properties.Settings.Default.g2_color);
                
            }
            for (int i = 0; i < n; i++)
            {
                Draw d = new Draw();
                if (Properties.Settings.Default.filll_glass_mode == 1 || Properties.Settings.Default.filll_glass_mode == 2)
                {
                    pen3.Color = Properties.Settings.Default.Fill_glass_color;
                    if (Properties.Settings.Default.filll_glass_mode == 2)
                    {
                        if (colors_shards_input.Length > i)
                            pen3.Color = colors_shards_input[i];
                        else
                            pen3.Color = colors_shards_output[i];
                    }
                        
                    d.DrawTriangleFilled(
                        new Point(arr_draw[i, 0] * scale_sum + pos.X, arr_draw[i, 1] * scale_sum + pos.Y),
                        new Point(arr_draw[i, 2] * scale_sum + pos.X, arr_draw[i, 3] * scale_sum + pos.Y),
                        new Point(arr_draw[i, 4] * scale_sum + pos.X, arr_draw[i, 5] * scale_sum + pos.Y), g, pen3.Color);
                }

                d.DrawTriangle(
                    new Point(arr_draw[i, 0] * scale_sum + pos.X, arr_draw[i, 1] * scale_sum + pos.Y),
                    new Point(arr_draw[i, 2] * scale_sum + pos.X, arr_draw[i, 3] * scale_sum + pos.Y),
                    new Point(arr_draw[i, 4] * scale_sum + pos.X, arr_draw[i, 5] * scale_sum + pos.Y), g, pen);
            }
            pic.Image = bit;
            pic.Image.RotateFlip(RotateFlipType.RotateNoneFlipY);
        }

        async public void AnimateDrawArray()
        {
            try
            {
                button_animate.Enabled = false;
                pictureBox2_trasher.Visible = false;
                pictureBox_2_load.Visible = false;
                thead_active = true;
                pic_output.Refresh();
                await Task.Delay(500);
                scale_mode = 1;
                short[,] array_anim = new short[n_in, 6];
                Array.Copy(array_input, array_anim, array_input.Length);
                Graphics g3 = Graphics.FromImage(bitmap2);
                pic_output.Image = bitmap2;
                bmp = new Bitmap(bitmap2.Width, bitmap2.Height);
                Point pose = new Point(0, 0);
                Point pose_start = new Point(0, 0);
                Graphics.FromImage(bitmap2).Clear(Properties.Settings.Default.g2_color);
                Graphics.FromImage(bmp).DrawImage(bitmap2, 0, 0, bitmap2.Width, bitmap2.Height);

                g2.Clear(Properties.Settings.Default.g2_color);
                for (int i2 = 0; i2 < n_in; i2++)
                {
                    scale_mode = 1;
                    min_out_xy = Min_Max(min_out_xy, array_anim, true,n_in);
                    max_out_xy = Min_Max(max_out_xy, array_anim, false,n_in);

                    min_out_xy_zero = min_out_xy;
                    max_out_xy_zero = max_out_xy;
                    Point[] mi_ma_points = new Point[] { min_out_xy_zero, max_out_xy_zero };

                    mi_ma_points = Min_Max_To_Zero(min_out_xy_zero, max_out_xy_zero, array_anim);
                    min_out_xy_zero = mi_ma_points[0];
                    max_out_xy_zero = mi_ma_points[1];
                    if (Properties.Settings.Default.pic_g2_draw_zoom == 2)
                        if (bitmap2.Width < bitmap2.Height) scale_mode = SetScale(bitmap2.Width, max_out_xy_zero.X);
                        else scale_mode = SetScale(bitmap2.Height, max_out_xy_zero.Y);


                    if (Properties.Settings.Default.pic_g2_draw_zoom == 1) scale_mode = 1;
                    if (Properties.Settings.Default.mode_wheel == 0) scale_sum_out = scale_mode + scale_out;
                    if (Properties.Settings.Default.scale_one_to_one) scale_sum_out = scale_sum_in;
                    if (Properties.Settings.Default.scale_one_to_one) scale_sum_out = scale_sum_in;

                    Point pos = new Point(0, 0);
                    if (Properties.Settings.Default.pic_g2_pos == 1)
                    pos = new Point((bitmap2.Width - (max_out_xy_zero.X * scale_sum_out)) / 2, (bitmap2.Height - (max_out_xy_zero.Y * scale_sum_out)) / 2);

                    while ((array_anim[i2, 0] != array_output[i2, 0]) || (array_anim[i2, 1] != array_output[i2, 1]))
                    {
                        if (array_anim[i2, 0] < array_output[i2, 0])
                        {
                            array_anim[i2, 0] += 1;
                            array_anim[i2, 2] += 1;
                            array_anim[i2, 4] += 1;
                        }
                        if (array_anim[i2, 0] > array_output[i2, 0])
                        {
                            array_anim[i2, 0] -= 1;
                            array_anim[i2, 2] -= 1;
                            array_anim[i2, 4] -= 1;
                        }
                        if (array_anim[i2, 1] < array_output[i2, 1])
                        {
                            array_anim[i2, 1] += 1;
                            array_anim[i2, 3] += 1;
                            array_anim[i2, 5] += 1;
                        }
                        if (array_anim[i2, 1] > array_output[i2, 1])
                        {
                            array_anim[i2, 1] -= 1;
                            array_anim[i2, 3] -= 1;
                            array_anim[i2, 5] -= 1;
                        }
                        min_out_xy = Min_Max(min_out_xy, array_anim, true,n_in);
                        max_out_xy = Min_Max(max_out_xy, array_anim, false,n_in);

                        mi_ma_points = Min_Max_To_Zero(min_out_xy, max_out_xy, array_anim);
                        min_out_xy_zero = mi_ma_points[0];
                        max_out_xy_zero = mi_ma_points[1];

                        short x = Convert.ToInt16(-min_out_xy.X);
                        short y = Convert.ToInt16(-min_out_xy.Y);
                        for (int i = 0; i < n_in; i++)
                        {
                            for (int j = 0; j < 6; j++)
                            {
                                if (j % 2 == 0) array_anim[i, j] = Convert.ToInt16(array_anim[i, j] + x);
                                else array_anim[i, j] = Convert.ToInt16(array_anim[i, j] + y);
                            }
                        }
                        Graphics.FromImage(bitmap2).DrawImage(bmp, 0, 0, bitmap2.Width, bitmap2.Height);
                        if (Properties.Settings.Default.pic_g2_pos == 1)
                        {
                            pos = new Point((bitmap2.Width - (max_out_xy_zero.X * scale_sum_out)) / 2, (bitmap2.Height - (max_out_xy_zero.Y * scale_sum_out)) / 2);
                        }
                        for (int j = 0; j < n_in; j++)
                        {
                            Draw d = new Draw();
                            if (Properties.Settings.Default.filll_glass_mode == 1 || Properties.Settings.Default.filll_glass_mode == 2)
                            {
                                pen3.Color = Properties.Settings.Default.Fill_glass_color;
                                if (Properties.Settings.Default.filll_glass_mode == 2)
                                {
                                    if (colors_shards_input.Length > j)
                                        pen3.Color = colors_shards_input[j];
                                    else
                                        pen3.Color = colors_shards_output[j];
                                }
                                d.DrawTriangleFilled(
                                    new Point(array_anim[j, 0] * scale_sum_out + pos.X, array_anim[j, 1] * scale_sum_out + pos.Y),
                                    new Point(array_anim[j, 2] * scale_sum_out + pos.X, array_anim[j, 3] * scale_sum_out + pos.Y),
                                    new Point(array_anim[j, 4] * scale_sum_out + pos.X, array_anim[j, 5] * scale_sum_out + pos.Y), g2, pen3.Color);
                            }
                            d.DrawTriangle(
                                new Point(array_anim[j, 0] * scale_sum_out + pos.X, array_anim[j, 1] * scale_sum_out + pos.Y),
                                new Point(array_anim[j, 2] * scale_sum_out + pos.X, array_anim[j, 3] * scale_sum_out + pos.Y),
                                new Point(array_anim[j, 4] * scale_sum_out + pos.X, array_anim[j, 5] * scale_sum_out + pos.Y), g2, pen2);
                        }
                        pic_output.Image = bitmap2;
                        pic_output.Image.RotateFlip(RotateFlipType.RotateNoneFlipY);
                        pic_output.Refresh();
                        await Task.Delay(100);
                    }
                }
                Draw_from_how_arr(2);
                button_animate.Enabled = true;
                pictureBox2_trasher.Visible = true;
                pictureBox_2_load.Visible = true;
                thead_active = false;
            }
            catch
            {
                Draw_from_how_arr(2);
                throw;
            }

        }


        public void loading()
        {
            if (Properties.Settings.Default.load_eneble)
            {
                Form_loading form2 = new Form_loading(this);
                form2.Owner = this;
                form2.Show();
            }
            else Properties.Settings.Default.load_active = false;
        }

        async private void open_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = Properties.Settings.Default.open_file_name;
            int ind = 0;
            openFileDialog1.DefaultExt = "in";
            for (int i = 0; i < openFileDialog1.FileName.Length; i++)
            {
                if (openFileDialog1.FileName[i] == (char)92)
                {
                    ind = i;
                }
            }
            string file_name = openFileDialog1.FileName.Substring(ind).Replace(Convert.ToString((char)92), "");
            openFileDialog1.FileName = file_name;

            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            file_name = openFileDialog1.FileName;
            Properties.Settings.Default.open_file_name = openFileDialog1.FileName;

            await Task.Run(() =>
            {
                Action act = new Action(() =>
                {
                    load_task_string = "open";
                    load_message_string = "Открываем файл";
                    loading();
                });

                if (InvokeRequired)
                    Invoke(act);
                else
                    act();
                while (Properties.Settings.Default.load_active)
                {
                    Task.Delay(100);
                }
            });
            string fileText = System.IO.File.ReadAllText(file_name);
            textBox1.Text = fileText;
            Properties.Settings.Default.load_active = false;
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            if (form_Resize)
            {
                ScaleSize();
            }


        }
        private void Form1_ResizeBegin(object sender, EventArgs e)
        {
            form_Resize = true;

        }
        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            form_Resize = false;
        }


        private void Form1_SizeChanged_1(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized) ScaleSize();
            else
            if (this.WindowState == FormWindowState.Normal == !timer1.Enabled) ScaleSize();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer_tick++;
            if (timer_tick > 6) timer1.Enabled = false;
        }
        private void ScaleSize()
        {
            float xRatio = (float)(this.Width) / (float)(form_size.Width);
            float yRatio = (float)(this.Height) / (float)(form_size.Height);
            Form form_ = new Form();

            SizeF xyR = new SizeF(xRatio, yRatio);

            for (int i = 0; i < this.Controls.Count; i++)
            {
                this.Controls[i].Scale(xyR);
            }
            form_size = this.Size;
        }
        
        public void BmpInit(byte i = 3)
        {
            if ((i==1)||(i==3))
            {
                if (g1 != null)
                {
                    bitmap1.Dispose();
                    g1.Dispose();
                }
                
                bitmap1 = new Bitmap(Properties.Settings.Default.draw_width, Properties.Settings.Default.draw_height);
                g1 = Graphics.FromImage(bitmap1);
                Graphics.FromImage(bitmap1).SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
                

            }
            if ((i==2)||(i==3))
            {
                if (g2 != null)
                {
                    bitmap2.Dispose();
                    g2.Dispose();
                }
                
                bitmap2 = new Bitmap(Properties.Settings.Default.draw_width, Properties.Settings.Default.draw_height);
                g2 = Graphics.FromImage(bitmap2);
                Graphics.FromImage(bitmap2).SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
            }
            GC.Collect();
        }
        
        private void Zoom_1(object sender, MouseEventArgs e)
        {
            if (!array_input_enebled || thead_active)
                return;
            if (Properties.Settings.Default.mode_wheel == 1)
            {
                if (e.Delta > 0)
                {
                    if (Properties.Settings.Default.pen1_value < 254)
                    {
                        Properties.Settings.Default.pen1_value++;
                        pen1 = new Pen(Properties.Settings.Default.pen1_color, Properties.Settings.Default.pen1_value);
                        Draw_from_how_arr(1);
                       

                    }

                }
                else
                if (e.Delta < 0)
                {
                    if (Properties.Settings.Default.pen1_value > 1)
                    {
                        Properties.Settings.Default.pen1_value--;
                        pen1 = new Pen(Properties.Settings.Default.pen2_color, Properties.Settings.Default.pen1_value);
                        Draw_from_how_arr(1);
                        
                    }
                }
                
            }
            else
            {
                
                if (e.Delta > 0)
                {
                    if (scale_sum_in < 1000)
                    {
                        scale_in++;
                        Draw_from_how_arr(1);
                        if (Properties.Settings.Default.scale_one_to_one)
                            Draw_from_how_arr(2);
                    }

                }
                else
                if (e.Delta < 0)
                {
                    if (scale_sum_in > 1)
                    {
                        scale_in--;
                        Draw_from_how_arr(1);
                        if (Properties.Settings.Default.scale_one_to_one)
                            Draw_from_how_arr(2);
                    }
                }
                if (Properties.Settings.Default.anim_enebled)
                {
                    pic_output.Refresh();
                    timer2.Enabled = true;
                    timer_tick = 0;
                }

            }

        }
        private void Zoom_2(object sender, MouseEventArgs e)
        {
            if (!array_output_enebled || thead_active)
                return;
            if (Properties.Settings.Default.mode_wheel == 1)
            {
                if (e.Delta > 0)
                {
                    if (Properties.Settings.Default.pen2_value < 254)
                    {
                        Properties.Settings.Default.pen2_value++;
                        pen2 = new Pen(Properties.Settings.Default.pen2_color, Properties.Settings.Default.pen2_value);
                        Draw_from_how_arr(2);
                    }
                }
                else
                if (e.Delta < 0)
                {
                    if (Properties.Settings.Default.pen2_value > 1)
                    {
                        Properties.Settings.Default.pen2_value--;
                        pen2 = new Pen(Properties.Settings.Default.pen2_color, Properties.Settings.Default.pen2_value);
                        Draw_from_how_arr(2);
                    }
                }
            }
            else
            {
                if (Properties.Settings.Default.scale_one_to_one)
                    return;
                if (e.Delta > 0)
                {
                    if (scale_sum_out < int.MaxValue)
                    {
                        scale_out++;
                        Draw_from_how_arr(2);
                    }

                }
                else
                if (e.Delta < 0)
                {
                    if (scale_sum_out > 1)
                    {
                        scale_out--;
                        Draw_from_how_arr(2);
                    }

                }
            }
            if (Properties.Settings.Default.anim_enebled)
            {
                pic_output.Refresh();
                timer2.Enabled = true;
                timer_tick = 0;
            }
        }

        public void SetColorsChards(ref Color[] colors, int n)
        {
            if (array_input_enebled)
            {
                if (Properties.Settings.Default.filll_glass_mode == 2)
                {
                    colors = new Color[n];
                    for (int i = 0; i < n; i++)
                    {
                        colors[i] = Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255));
                    }
                    
                }
                else
                if (Properties.Settings.Default.filll_glass_mode == 1)
                {
                    colors = new Color[n];
                    for (int i = 0; i < n; i++)
                    {
                        colors[i] = Properties.Settings.Default.Fill_glass_color;
                    }
                }

                
            }
        }
        private void ArrCreate(string text, ref short[,] arr,int n)
        {
            try
            {
                string[] line = new string[6];
                StringReader str = new StringReader(text);
                str.ReadLine();
                arr = new short[n, 6];
                for (int i = 0; i < n; i++)
                {
                    line = str.ReadLine().Split(' ');
                    for (int j = 0; j < 6; j++)
                    {
                        arr[i, j] = Convert.ToInt16(line[j]);
                    }
                }
            }
            catch
            {
                //MessageBox.Show("Ошибка");
            }
        }
        public Point Min_Max(Point p, short[,] arr, bool min_max,int n)
        {
            p.X = arr[0, 0];
            p.Y = arr[0, 1];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (j % 2 == 0)
                    {
                        if (min_max)
                        {
                            if (p.X > arr[i, j]) p.X = arr[i, j];        
                        }
                        else
                        {
                            if (p.X < arr[i, j]) p.X = arr[i, j];
                        }
                        
                    }
                    else
                    {
                        if (min_max)
                        {
                            if (p.Y > arr[i, j]) p.Y = arr[i, j];
                        }
                        else
                        {
                            if (p.Y < arr[i, j]) p.Y = arr[i, j];
                        }
                    }
                }
            }
            return p;
        }
        public Point[] Min_Max_To_Zero(Point p1, Point p2, short[,] arr)
        {
            int min = 0;
            min = Math.Abs(p1.X);
            if (p1.X < 0)
            {
                p1.X += min;
                p2.X += min;
            }
            else
            {
                p1.X -= min;
                p2.X -= min;
            }

            min = Math.Abs(p1.Y);
            if (p1.Y < 0)
            {
                p1.Y += min;
                p2.Y += min;
            }
            else
            {
                p1.Y -= min;
                p2.Y -= min;
            }

            return new Point[] { p1, p2 };
        }
        public int SetScale(int bmp,  int max_xy_zero)
        {
            short bmp_min_value = Convert.ToInt16(bmp);
            int scale_mode = 1;
            if (max_xy_zero == 0) 
                return scale_mode;
            scale_mode = (bmp_min_value - 10) / max_xy_zero;
            return scale_mode;
        }
        public void Draw_from_how_arr(byte choice = 4)
        {
            Draw dc = new Draw();
            
            if ((choice == 1) || (choice == 4) && array_input_enebled)
            {
                if (!array_input_enebled)
                {
                    pic_input.Image = pic_input.ErrorImage;
                    return;
                }
                DrawArray(
                ref min_in_xy,
                ref max_in_xy,
                ref min_in_xy_zero,
                ref max_in_xy_zero,
                ref array_input,
                ref array_in_draw,
                ref pic_input,
                ref bitmap1,
                ref g1,
                ref pen1,
                ref scale_mode,
                ref scale_in,
                ref scale_sum_in,
                ref n_in
                );
            }
            if ((choice == 2) | (choice == 4))
            {
                if (!array_output_enebled)
                {
                    pic_output.Image = pic_output.ErrorImage;
                    return;
                }
                DrawArray(
                ref min_out_xy,
                ref max_out_xy,
                ref min_out_xy_zero,
                ref max_out_xy_zero,
                ref array_output,
                ref array_out_draw,
                ref pic_output,
                ref bitmap2,
                ref g2,
                ref pen2,
                ref scale_mode,
                ref scale_out,
                ref scale_sum_out,
                ref n_out
                );
            }
            if ((choice == 3) | (choice == 4) && (array_input_enebled) && (array_output_enebled))
            {
                BeginInvoke(new Action(AnimateDrawArray));
            }
            
        }
        private void CreateArrDireMain(short [,] arr,int n)
        {
            if (arr == null) return;
            array_dire = new short[n, 1];
            array_main = new short[n, 1];
            for (int i = 0; i < n; i++)
            {
                if (arr[i,0] == arr[i, 2])
                {
                    if (arr[i, 4]<arr[i, 0])
                    {
                        array_dire[i, 0] = (short)Dire.Left;
                        array_main[i, 0] = 3;
                    }
                    else
                    {
                        array_dire[i, 0] = (short)Dire.Rigth;
                        array_main[i, 0] = 3;
                    }
                }
                
                if (arr[i, 2] == arr[i, 4])
                {
                    if (arr[i, 0] < arr[i, 2])
                    {
                        array_dire[i, 0] = (short)Dire.Left;
                        array_main[i, 0] = 1;
                    }
                    else
                    {
                        array_dire[i, 0] = (short)Dire.Rigth;
                        array_main[i, 0] = 1;
                    }
                }

                if (arr[i, 4] == arr[i, 0])
                {
                    if (arr[i, 2] < arr[i, 4])
                    {
                        array_dire[i, 0] = (short)Dire.Left;
                        array_main[i, 0] = 2;
                    }
                    else
                    {
                        array_dire[i, 0] = (short)Dire.Rigth;
                        array_main[i, 0] = 2;
                    }
                }


                if (arr[i, 1] == arr[i, 3])
                {
                    if (arr[i, 5] < arr[i, 1])
                    {
                        array_dire[i, 0] = (short)Dire.Down;
                        array_main[i, 0] = 3;
                    }
                    else
                    {
                        array_dire[i, 0] = (short)Dire.Up;
                        array_main[i, 0] = 3;
                    }
                }

                if (arr[i, 3] == arr[i, 5])
                {
                    if (arr[i, 1] < arr[i, 3])
                    {
                        array_dire[i, 0] = (short)Dire.Down;
                        array_main[i, 0] = 1;
                    }
                    else
                    {
                        array_dire[i, 0] = (short)Dire.Up;
                        array_main[i, 0] = 1;
                    }
                }

                if (arr[i, 5] == arr[i, 1])
                {
                    if (arr[i, 3] < arr[i, 5])
                    {
                        array_dire[i, 0] = (short)Dire.Down;
                        array_main[i, 0] = 2;
                    }
                    else
                    {
                        array_dire[i, 0] = (short)Dire.Up;
                        array_main[i, 0] = 2;
                    }
                }
            }
            
        }

        private void button_Assembler_Click(object sender, EventArgs e)
        {
            Glass_collector();
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            TestCorect(textBox1.Text, 1);
            if (text_input_correct == false) return;
            else
            {
                pic_input.Image = pic_input.ErrorImage;
                array_input_enebled = false;
            }

            try
            {
                n_in = Convert.ToInt32(new StringReader(textBox1.Text).ReadLine());
                label_table_input_count.Text = $"Осколков: {n_in}";

            }
            catch
            {
                return;
            }
            if (n_in > 0)
                array_input_enebled = true;
            else
            {
                SetColorsChards(ref colors_shards_input, n_in);
                TableInit(array_input, ref table_count_in, ref tableLayoutPanel1, in text_input_correct, 1,n_in);
                return;
            }
            

            ArrCreate(textBox1.Text, ref array_input,n_in);
            array_dire = new short[n_in, 1];
            array_main = new short[n_in, 1];
            CreateArrDireMain(array_input,n_in);
            SetColorsChards(ref colors_shards_input, n_in);
            TableInit(array_input, ref table_count_in, ref tableLayoutPanel1,in text_input_correct, 1,n_in);
            Draw_from_how_arr(1);

            if (Properties.Settings.Default.draw_output_post_input) Glass_collector();

        }

        private void продвинутыеНастройкиToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Form_settings form3 = new Form_settings(this);
            form3.Owner = this;
            form3.ShowDialog();
        }
        private void Glass_collector()
        {
            if (array_input_enebled)
            {
                array_output = new short[n_in, 6];
                Array.Copy(array_input, array_output, array_input.Length);
                int x = 0;
                int y = 0;
                Point p = new Point(0, 0);
                short px = 0;
                short py = 0;
                switch (array_main[0, 0])
                {
                    case 1:
                        x = array_output[0, 0];
                        y = array_output[0, 1];
                        break;
                    case 2:
                        x = array_output[0, 2];
                        y = array_output[0, 3];
                        break;
                    case 3:
                        x = array_output[0, 4];
                        y = array_output[0, 5];
                        break;
                }

                for (int i = 0; i < n_in; i++)
                {
                    if (array_main[i, 0] == 1)
                    {
                        x = px - array_output[i, 0];
                        y = py - array_output[i, 1];
                        for (int j = 0; j < 6; j++)
                        {
                            if (j % 2 == 0) array_output[i, j] += Convert.ToInt16(x);
                            else array_output[i, j] += Convert.ToInt16(y);
                        }
                    }
                    if (array_main[i, 0] == 2)
                    {
                        x = px - array_output[i, 2];
                        y = py - array_output[i, 3];
                        for (int j = 0; j < 6; j++)
                        {
                            if (j % 2 == 0) array_output[i, j] += Convert.ToInt16(x);
                            else array_output[i, j] += Convert.ToInt16(y);
                        }
                    }
                    if (array_main[i, 0] == 3)
                    {
                        x = px - array_output[i, 4];
                        y = py - array_output[i, 5];
                        for (int j = 0; j < 6; j++)
                        {
                            if (j % 2 == 0)
                            {
                                array_output[i, j] += Convert.ToInt16(x);
                            }
                            else
                            {
                                array_output[i, j] += Convert.ToInt16(y);
                            }
                        }
                    }
                   
                }
                min_out_xy = Min_Max(min_out_xy, array_output, true,n_in);
                max_out_xy = Min_Max(max_out_xy, array_output, false,n_in);
                short x1 = Convert.ToInt16(-min_out_xy.X);
                short y1 = Convert.ToInt16(-min_out_xy.Y);
                for (int i = 0; i < n_in; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        if (j % 2 == 0)
                        {
                            array_output[i, j] = Convert.ToInt16(array_output[i, j] + x1);
                        }
                        else
                        {
                            array_output[i, j] = Convert.ToInt16(array_output[i, j] + y1);
                        }

                    }
                }

                Write_from_Array(array_output,ref textBox2);
                array_output_enebled = true;
            }
            else
            {
                MessageBox.Show($"Отсуствуют входные данные (Попытка собрать стекло)");
            }
            
        }
        public void Write_from_Array(short[,] arr, ref TextBox tBox)
        {
            string text;
            int n = arr.Length / 6;
            text = $"{n}";
            for (int i = 0; i < n; i++)
            {
                text += "\r\n";
                for (int j = 0; j < 6; j++)
                {
                    text += $"{arr[i, j]}";
                    if (j!=5)
                    {
                        text += " ";
                    }
                }
            }
            
            tBox.Text = text;
        }

        private void тестToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BmpInit(3);
            textBox1.Text = Properties.Resources.TestGlass;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer_tick++;
            if (timer_tick==5)
            {
                Draw_from_how_arr(3);
                timer2.Enabled = false;
            }
        }

        async private void button_animate_Click(object sender, EventArgs e)
        {
            if (!thead_active)
            {
                await Task.Run(() =>
                {
                    Action act = new Action(() =>
                    {
                        load_task_string = "open";
                        load_message_string = "Подготовка к анимации";
                        loading();
                    });

                    if (InvokeRequired)
                        Invoke(act);
                    else
                        act();
                    while (Properties.Settings.Default.load_active)
                    {
                        Task.Delay(100);
                    }
                });
                Draw_from_how_arr(3);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            TestCorect(textBox2.Text,2);
            if (text_output_correct == false) return;
            else
            {
                pic_output.Image = pic_output.ErrorImage;
                array_output_enebled = false;
            }
            try
            {
                n_out = Convert.ToInt32(new StringReader(textBox2.Text).ReadLine());
                label_table_output_count.Text = $"Осколков: {n_out}";
            }
            catch
            {
                return;
            }
            if (n_out > 0)
                array_output_enebled = true;
            else
            {
                SetColorsChards(ref colors_shards_input, n_in);
                TableInit(array_output, ref table_count_out, ref tableLayoutPanel2, in text_output_correct, 2, n_out);
                return;
            }
               
            SetColorsChards(ref colors_shards_output, n_out);
            ArrCreate(textBox2.Text, ref array_output,n_out);
            TableInit(array_output, ref table_count_out, ref tableLayoutPanel2, in text_output_correct,2,n_out);
            Draw_from_how_arr(2);
            if (Properties.Settings.Default.anim_enebled)
            {
                timer2.Enabled = true;
                timer_tick = 0;
            }

        }
        void TestCorect(String text, Byte textBox_num)
        {
            string alph = "0987654321- ";
            bool corect_flag = true;
            bool check_error = false;
            int count = 0;
            short n = 0;
            string[] arr_str;
            text = text.Replace("\n", " ").Replace("\r", " ");

            if (text == "") corect_flag = false;
            StringReader str = new StringReader(text);
            for (int i = 0; i < text.Length; i++)
            {
                check_error = false;
                for (int j = 0; j < alph.Length; j++)
                {                  
                    if (text[i] == alph[j])
                    {
                        check_error = true;
                    }
                }
                if (check_error == false)
                {
                    text = text.Remove(i, 1);
                    corect_flag = false;
                    i = 0;
                }
            }

            if (corect_flag)
            {
                while (text[text.Length - 1] == ' ')
                {
                    text = text.Remove(text.Length - 1, 1);
                }
                while (text[0] == ' ')
                {
                    text = text.Remove(0, 1);
                }
                while (text.Contains("  "))
                {
                    text = text.Replace("  ", " ");
                }

                for (int i = 0; i < text.Length; i++)
                {
                    if (text[i] == ' ') count++;
                }
            
                arr_str = new string[count];
                arr_str = text.Split();
                n = Convert.ToInt16(arr_str[0]);
                if ((arr_str.Length - 1 >= n * 6)) corect_flag = true;
                else corect_flag = false;
            }
               
            if (textBox_num == 1)
                if (corect_flag == false) text_input_correct = false;
                else text_input_correct = true;
            else
            if (textBox_num == 2)
                if (corect_flag == false) text_output_correct = false;
                else text_output_correct = true;



        }

        private void button1_Click(object sender, EventArgs e)
        {
            DrawShard();
        }
        private void DrawShard()
        {
            if (bit != null) bit.Dispose();
            Point p1;
            Point p2;
            Point p3;
            int n = Convert.ToInt32(label_num_draw_shard.Text) -1;
            if (n < 0) return;
            Draw d = new Draw();
            try
            {
                p1 = new Point(Convert.ToInt32(textBoxX1.Text), Convert.ToInt32(textBoxY1.Text));
                p2 = new Point(Convert.ToInt32(textBoxX2.Text), Convert.ToInt32(textBoxY2.Text));
                p3 = new Point(Convert.ToInt32(textBoxX3.Text), Convert.ToInt32(textBoxY3.Text));
            }
            catch
            {
                MessageBox.Show($"Не коректные данные");
                return;
            }
            bit = new Bitmap((int)numericUpDown_bit_x.Value,(int)numericUpDown_bit_y.Value);
            Graphics g = Graphics.FromImage(bit);
            Color color_shard;
            Pen pen;
            if (groupBox_draw_shard.Text == "Input")
            {
                color_shard = colors_shards_input[n];
                g.Clear(Properties.Settings.Default.g1_color);
                pen = new Pen(Properties.Settings.Default.pen1_color, Properties.Settings.Default.pen1_value);
            }
            else
            {
                color_shard = colors_shards_output[n];
                g.Clear(Properties.Settings.Default.g2_color);
                pen = new Pen(Properties.Settings.Default.pen2_color, Properties.Settings.Default.pen2_value);
            }
            int scale_sum = 1;
            Point min_xy = new Point();
            Point max_xy = new Point();
            if (p1.X <= p2.X && p1.X <= p3.X) min_xy.X = p1.X;
            if (p2.X <= p3.X && p2.X <= p1.X) min_xy.X = p2.X;
            if (p3.X <= p1.X && p3.X <= p2.X) min_xy.X = p3.X;

            if (p1.Y <= p2.Y && p1.Y <= p3.Y) min_xy.Y = p1.Y;
            if (p2.Y <= p3.Y && p2.Y <= p1.Y) min_xy.Y = p2.Y;
            if (p3.Y <= p1.Y && p3.Y <= p2.Y) min_xy.Y = p3.Y;

            if (p1.X >= p2.X && p1.X >= p3.X) max_xy.X = p1.X;
            if (p2.X >= p3.X && p2.X >= p1.X) max_xy.X = p2.X;
            if (p3.X >= p1.X && p3.X >= p2.X) max_xy.X = p3.X;

            if (p1.Y >= p2.Y && p1.Y >= p3.Y) max_xy.Y = p1.Y;
            if (p2.Y >= p3.Y && p2.Y >= p1.Y) max_xy.Y = p2.Y;
            if (p3.Y >= p1.Y && p3.Y >= p2.Y) max_xy.Y = p3.Y;

            if (min_xy.X < 0)
            {
                int plus = Math.Abs(min_xy.X);

                min_xy.X += plus;
                max_xy.X += plus;
                p1.X += plus;
                p2.X += plus;
                p3.X += plus;
            }
            if (min_xy.X > 0)
            {
                int plus = Math.Abs(min_xy.X);

                min_xy.X -= plus;
                max_xy.X -= plus;
                p1.X -= plus;
                p2.X -= plus;
                p3.X -= plus;
            }
            if (min_xy.Y < 0)
            {
                int plus = Math.Abs(min_xy.Y);

                min_xy.Y += plus;
                max_xy.Y += plus;
                p1.Y += plus;
                p2.Y += plus;
                p3.Y += plus;
            }
            if (min_xy.Y > 0)
            {
                int plus = Math.Abs(min_xy.Y);

                min_xy.Y -= plus;
                max_xy.Y -= plus;
                p1.Y -= plus;
                p2.Y -= plus;
                p3.Y -= plus;
            }

            short x = Convert.ToInt16(-min_xy.X);
            short y = Convert.ToInt16(-min_xy.Y);
            p1.X += x;
            p2.X += x;
            p3.X += x;

            p1.Y += y;
            p2.Y += y;
            p3.Y += y;

            int max;
            int max_x = max_xy.X - min_xy.X;
            int max_y = max_xy.Y - min_xy.Y;
            if (max_xy.X > max_xy.Y) max = max_xy.X;
            else max = max_xy.Y;

            if (bit.Width < bit.Height) scale_sum = SetScale(bit.Width, max);
            else scale_sum = SetScale(bit.Height, max);


            Point pos = new Point((bit.Width - (max_x * scale_sum)) / 2,
                                    (bit.Height - (max_y * scale_sum)) / 2);


            p1 = new Point(p1.X * scale_sum + pos.X, p1.Y * scale_sum + pos.Y);
            p2 = new Point(p2.X * scale_sum + pos.X, p2.Y * scale_sum + pos.Y);
            p3 = new Point(p3.X * scale_sum + pos.X, p3.Y * scale_sum + pos.Y);


            if (Properties.Settings.Default.filll_glass_mode == 2 || Properties.Settings.Default.filll_glass_mode == 1)
            {
                d.DrawTriangleFilled(p1 ,p2 ,p3 ,g,color_shard);
            }
            d.DrawTriangle(p1, p2, p3, g, pen);
            if (p1.Y > bit.Height / 2) p1.Y -= (int)numericUpDown_bit_x.Value / 10;
            if (p2.Y > bit.Height / 2) p2.Y -= (int)numericUpDown_bit_x.Value / 10;
            if (p3.Y > bit.Height / 2) p3.Y -= (int)numericUpDown_bit_x.Value / 10;

            if (p1.X > bit.Width / 2) p1.X -= (int)numericUpDown_bit_x.Value / 10;
            if (p2.X > bit.Width / 2) p2.X -= (int)numericUpDown_bit_x.Value / 10;
            if (p3.X > bit.Width / 2) p3.X -= (int)numericUpDown_bit_x.Value / 10;

            Font f1 = new Font(FontFamily.GenericSerif, (int)numericUpDown_bit_x.Value/10-2);
            Font f2 = new Font(FontFamily.GenericMonospace, (int)numericUpDown_bit_x.Value / 10);
            
            g.DrawString("C", f1,  new SolidBrush(Color.White), p1.X, p1.Y);
            g.DrawString("C", f2,  new SolidBrush(Color.Black),p1.X, p1.Y);

            g.DrawString("D", f1, new SolidBrush(Color.White), p2.X, p2.Y);
            g.DrawString("D", f2, new SolidBrush(Color.Black), p2.X, p2.Y);

            g.DrawString("O", f1, new SolidBrush(Color.White), p3.X, p3.Y);
            g.DrawString("O", f2, new SolidBrush(Color.Black), p3.X, p3.Y);
           
            pictureBox_ShardDraw.Image = bit;
            pictureBox_ShardDraw.Image.RotateFlip(RotateFlipType.RotateNoneFlipY);
            pictureBox_ShardDraw.Refresh();
            
        }

        private void button_Confirm_draw_shard_Click(object sender, EventArgs e)
        {
            
            int n;
            short[,] arr;
            if (groupBox_draw_shard.Text == "None") return;
            if (groupBox_draw_shard.Text == "Input")
            {
                n = n_in;
                if (n == 0) return;
                arr = new short[n, 6];
                ArrCreate(textBox1.Text,ref arr,n);
            }
            else
            {
                n = n_out;
                if (n == 0) return;
                arr = new short[n, 6];
                ArrCreate(textBox2.Text, ref arr, n);
            }
            arr[Convert.ToInt32(label_num_draw_shard.Text) - 1, 0] = Convert.ToInt16(textBoxX1.Text);
            arr[Convert.ToInt32(label_num_draw_shard.Text) - 1, 1] = Convert.ToInt16(textBoxY1.Text);
            arr[Convert.ToInt32(label_num_draw_shard.Text) - 1, 2] = Convert.ToInt16(textBoxX2.Text);
            arr[Convert.ToInt32(label_num_draw_shard.Text) - 1, 3] = Convert.ToInt16(textBoxY2.Text);
            arr[Convert.ToInt32(label_num_draw_shard.Text) - 1, 4] = Convert.ToInt16(textBoxX3.Text);
            arr[Convert.ToInt32(label_num_draw_shard.Text) - 1, 5] = Convert.ToInt16(textBoxY3.Text);

            if (groupBox_draw_shard.Text == "Input")
                Write_from_Array(arr,ref textBox1);
            else
                Write_from_Array(arr, ref textBox2);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form_breake_glass form3 = new Form_breake_glass(this);
            form3.Owner = this;
            form3.ShowDialog();
        }

        private void button_collector_Click(object sender, EventArgs e)
        {
            Glass_collector();
        }

        async private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = "Glass.out"; 

            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = saveFileDialog1.FileName;
            System.IO.File.WriteAllText(filename, textBox2.Text);

            if (File.Exists(filename))
            {
                Process.Start(new ProcessStartInfo("explorer.exe", " /select, " + filename));
            }
            await Task.Run(() =>
            {
                Action act = new Action(() =>
                {
                    load_task_string = "open";
                    load_message_string = "Сохраняем файл";
                    loading();
                });

                if (InvokeRequired)
                    Invoke(act);
                else
                    act();
                while (Properties.Settings.Default.load_active)
                {
                    Task.Delay(100);
                }
            });
        }
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Жуков Павел Андреевич\nГруппа: 315-К\n2023 год","Информация об разработчике");
        }
    }

    class Draw
    {
        public void DrawTriangle(Point p1, Point p2, Point p3, Graphics g, Pen pen)
        {
            g.DrawPolygon(pen, new Point[] { p1, p2, p3 });
        }
        public void DrawTriangleFilled(Point p1, Point p2, Point p3, Graphics g, Color color)
        {
            g.FillPolygon(new SolidBrush(color), new Point[] { p1, p2, p3 });
        }
    }
}
