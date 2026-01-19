using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectGlass
{
    public partial class Form_settings : Form
    {
        public int seting;
        bool bmp1_change = false;
        bool bmp2_change = false;
        public Form_settings(Form f)
        {
            InitializeComponent();
            SetValues();
            PinHandlers();
        }
        private void SetValues()
        {
            
            trackBar1_load_max.Value = Properties.Settings.Default.load_max;
            label1_load.Text = Convert.ToString(trackBar1_load_max.Value) + " %";
            checkBox1_load_enebled.Checked = Properties.Settings.Default.load_eneble;
            textBox1.Text = Properties.Settings.Default.open_file_ext;
            textBox2.Text = Properties.Settings.Default.save_file_ext;
            numericUpDown1.Value = Properties.Settings.Default.draw_width;
            numericUpDown2.Value = Properties.Settings.Default.draw_height;
            checkBox1.Checked = Properties.Settings.Default.draw_output_post_input;

            pictureBox_pen1.BackColor = Properties.Settings.Default.pen1_color;
            pictureBox_g1.BackColor = Properties.Settings.Default.g1_color;

            pictureBox_pen2.BackColor = Properties.Settings.Default.pen2_color;
            pictureBox_g2.BackColor = Properties.Settings.Default.g2_color;

            numericUpDown3.Value = Properties.Settings.Default.pen1_value;
            numericUpDown4.Value = Properties.Settings.Default.pen2_value;
            numericUpDown_glass_width.Value = Properties.Settings.Default.glass_creat_width;
            numericUpDown_glass_height.Value = Properties.Settings.Default.glass_creat_height;

            pictureBox_g1_position.Image = imageList1.Images[Properties.Settings.Default.pic_g1_pos];
            pictureBox_g2_position.Image = imageList1.Images[Properties.Settings.Default.pic_g2_pos];

            pictureBox_g1_enebled_zoom.Image = imageList1.Images[Properties.Settings.Default.pic_g1_draw_zoom];
            pictureBox_g2_enebled_zoom.Image = imageList1.Images[Properties.Settings.Default.pic_g2_draw_zoom];

            checkBox_reload_post_end.Checked = Properties.Settings.Default.Reload_post_end_setting;
            checkBox_one_to_one.Checked = Properties.Settings.Default.scale_one_to_one;
            checkBox_animate.Checked = Properties.Settings.Default.anim_enebled;
            checkBox_glass_create_mode.Checked = Properties.Settings.Default.glass_creat_mode;
            checkBox_test_func.Checked = Properties.Settings.Default.test_func_eneble;

            comboBox_fill_glass_mode.SelectedIndex = Properties.Settings.Default.filll_glass_mode;
        }
        private void PinHandlers () 
        {
            
            numericUpDown1.ValueChanged += (s, r) => { Properties.Settings.Default.draw_width = (ushort)numericUpDown1.Value; bmp1_change = true; };
            numericUpDown2.ValueChanged += (s, r) => { Properties.Settings.Default.draw_height = (ushort)numericUpDown2.Value;bmp2_change = true; };
            numericUpDown_glass_width.ValueChanged += (s, r) => { Properties.Settings.Default.glass_creat_width = (ushort)numericUpDown_glass_width.Value; };
            numericUpDown_glass_height.ValueChanged += (s, r) => { Properties.Settings.Default.glass_creat_height = (ushort)numericUpDown_glass_height.Value; };
            numericUpDown3.ValueChanged += (s, r) => { Properties.Settings.Default.pen1_value = (byte)numericUpDown3.Value; };
            numericUpDown4.ValueChanged += (s, r) => { Properties.Settings.Default.pen2_value = (byte)numericUpDown4.Value; };

            pictureBox_pen1.Click += (s, r) => SetColor(pictureBox_pen1, r);
            pictureBox_pen2.Click += (s, r) => SetColor(pictureBox_pen2, r);
            pictureBox_g1.Click += (s, r) => SetColor(pictureBox_g1, r);
            pictureBox_g2.Click += (s, r) => SetColor(pictureBox_g2, r);

            checkBox_reload_post_end.CheckedChanged += (s, r) => { Properties.Settings.Default.Reload_post_end_setting = checkBox_reload_post_end.Checked; };
            checkBox_one_to_one.CheckedChanged += (s,r) => { Properties.Settings.Default.scale_one_to_one = checkBox_one_to_one.Checked; };
            checkBox_animate.CheckedChanged += (s, r) => { Properties.Settings.Default.anim_enebled = checkBox_animate.Checked; };
            checkBox_test_func.CheckedChanged += (s, r) => { Properties.Settings.Default.test_func_eneble = checkBox_test_func.Checked; };

            pictureBox_pen1.BackColorChanged += (s, r) => { Properties.Settings.Default.pen1_color = pictureBox_pen1.BackColor; };
            pictureBox_pen2.BackColorChanged += (s, r) => { Properties.Settings.Default.pen2_color = pictureBox_pen2.BackColor; };
            pictureBox_g1.BackColorChanged += (s, r) => { Properties.Settings.Default.g1_color = pictureBox_g1.BackColor; };
            pictureBox_g2.BackColorChanged += (s, r) => { Properties.Settings.Default.g2_color = pictureBox_g2.BackColor; };

            comboBox_fill_glass_mode.SelectedIndexChanged += (s, r) => { Form_main main = this.Owner as Form_main; Properties.Settings.Default.filll_glass_mode = (byte)comboBox_fill_glass_mode.SelectedIndex; main.SetColorsChards(ref main.colors_shards_output, main.n_out); main.SetColorsChards(ref main.colors_shards_input, main.n_in); };

           pictureBox_g1_position.Click += (s, r) =>
            {
                if (Properties.Settings.Default.pic_g1_pos == 0) Properties.Settings.Default.pic_g1_pos = 1;
                else
                if (Properties.Settings.Default.pic_g1_pos == 1) Properties.Settings.Default.pic_g1_pos = 0;
                pictureBox_g1_position.Image = imageList1.Images[Properties.Settings.Default.pic_g1_pos];

            };
            pictureBox_g2_position.Click += (s, r) =>
            {
                if (Properties.Settings.Default.pic_g2_pos == 0) Properties.Settings.Default.pic_g2_pos = 1;
                else
                if (Properties.Settings.Default.pic_g2_pos == 1) Properties.Settings.Default.pic_g2_pos = 0;
                pictureBox_g2_position.Image = imageList1.Images[Properties.Settings.Default.pic_g2_pos];

            };

            pictureBox_g1_enebled_zoom.Click += (s, r) =>
            {
                if (Properties.Settings.Default.pic_g1_draw_zoom == 1) Properties.Settings.Default.pic_g1_draw_zoom = 2;
                else
                if (Properties.Settings.Default.pic_g1_draw_zoom == 2) Properties.Settings.Default.pic_g1_draw_zoom = 1;
                pictureBox_g1_enebled_zoom.Image = imageList1.Images[Properties.Settings.Default.pic_g1_draw_zoom];
                bmp1_change = true;
            };

            pictureBox_g2_enebled_zoom.Click += (s, r) =>
            {
                if (Properties.Settings.Default.pic_g2_draw_zoom == 1) Properties.Settings.Default.pic_g2_draw_zoom = 2;
                else
                if (Properties.Settings.Default.pic_g2_draw_zoom == 2) Properties.Settings.Default.pic_g2_draw_zoom = 1;
                pictureBox_g2_enebled_zoom.Image = imageList1.Images[Properties.Settings.Default.pic_g2_draw_zoom];
                bmp2_change = true;
            };
        }
        private void SetColor(PictureBox p, EventArgs e)
        {
            Color c;

            if (colorDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            c = colorDialog1.Color;
            p.BackColor = c;
        }
        private void trackBar1_load_max_Scroll(object sender, EventArgs e)
        {
            
            Properties.Settings.Default.load_max = Convert.ToUInt16(trackBar1_load_max.Value);
            label1_load.Text = Convert.ToString(trackBar1_load_max.Value) + " %";
        }

        private void checkBox1_load_enebled_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.load_eneble = checkBox1_load_enebled.Checked;
            Form_main main = this.Owner as Form_main;
            if (!checkBox1_load_enebled.Checked) trackBar1_load_max.Enabled = false;
            else trackBar1_load_max.Enabled = true;
        }
        // reset
        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reset();
            SetValues();
        }
        // save
        private void button2_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
            
        }
        private void text_input_output_catch(TextBox t)
        {
            t.Text = t.Text.Replace(".", "").Replace(" ", "").Replace(@"\", "").Replace("|", "").Replace("/", "").Replace(@":", "").Replace(@"*", "").Replace(@"?", "").Replace(Convert.ToString((char)34), "").Replace("<", "").Replace("!", "").Replace(">", "");
            t.SelectionStart = t.Text.Length;
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            text_input_output_catch(textBox1);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            text_input_output_catch(textBox2);
        }


        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            //Properties.Settings.Default.draw_height = Convert.ToUInt16(numericUpDown2.Value);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.draw_output_post_input = checkBox1.Checked;
        }
        private void Form_settings_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form_main main = this.Owner as Form_main;
            Properties.Settings.Default.draw_width = Convert.ToUInt16(numericUpDown1.Value);
            Properties.Settings.Default.draw_height = Convert.ToUInt16(numericUpDown2.Value);
            Properties.Settings.Default.pen1_value = Convert.ToByte(numericUpDown3.Value);
            Properties.Settings.Default.pen2_value = Convert.ToByte(numericUpDown4.Value);
            
            main.SetValues();

            if (Properties.Settings.Default.Reload_post_end_setting)
            {
                
                if (main.array_input_enebled)
                {
                    if (bmp1_change)
                    {
                        main.scale_in = 0;
                        main.BmpInit(1);
                        main.Draw_from_how_arr(1);
                    }
                    else main.Draw_from_how_arr(1);
                }

                if (main.array_output_enebled)
                {
                    if (bmp2_change)
                    {
                        main.scale_out = 0;
                        main.BmpInit(2);
                        main.Draw_from_how_arr(2);
                    }
                    else main.Draw_from_how_arr(2);
                }

            }
            
            this.FormClosed -= (this.Form_settings_FormClosed);
            GC.Collect();
            this.Dispose();
            
        }
    }
}
