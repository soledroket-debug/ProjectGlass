
namespace ProjectGlass
{
    partial class Form_breake_glass
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_breake_glass));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown_glass_height = new System.Windows.Forms.NumericUpDown();
            this.label29 = new System.Windows.Forms.Label();
            this.numericUpDown_glass_width = new System.Windows.Forms.NumericUpDown();
            this.label30 = new System.Windows.Forms.Label();
            this.button_take_hammer = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown_count_shards = new System.Windows.Forms.NumericUpDown();
            this.button_random_shard_pos = new System.Windows.Forms.Button();
            this.button_new_glass = new System.Windows.Forms.Button();
            this.numericUpDown_holst_H = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDown_holst_W = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button_left = new System.Windows.Forms.Button();
            this.button_up = new System.Windows.Forms.Button();
            this.button_right = new System.Windows.Forms.Button();
            this.button_down = new System.Windows.Forms.Button();
            this.button_hit = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.numericUpDown_hit_y = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_hit_x = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.numericUpDown_step = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.button_load_glass = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_glass_height)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_glass_width)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_count_shards)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_holst_H)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_holst_W)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_hit_y)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_hit_x)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_step)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(357, 379);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(604, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Параметры стекла";
            // 
            // numericUpDown_glass_height
            // 
            this.numericUpDown_glass_height.Location = new System.Drawing.Point(675, 66);
            this.numericUpDown_glass_height.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown_glass_height.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown_glass_height.Name = "numericUpDown_glass_height";
            this.numericUpDown_glass_height.Size = new System.Drawing.Size(100, 22);
            this.numericUpDown_glass_height.TabIndex = 63;
            this.numericUpDown_glass_height.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(608, 66);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(66, 17);
            this.label29.TabIndex = 62;
            this.label29.Text = "- Высота";
            // 
            // numericUpDown_glass_width
            // 
            this.numericUpDown_glass_width.Location = new System.Drawing.Point(675, 38);
            this.numericUpDown_glass_width.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown_glass_width.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown_glass_width.Name = "numericUpDown_glass_width";
            this.numericUpDown_glass_width.Size = new System.Drawing.Size(100, 22);
            this.numericUpDown_glass_width.TabIndex = 61;
            this.numericUpDown_glass_width.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(608, 38);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(68, 17);
            this.label30.TabIndex = 60;
            this.label30.Text = "- Ширина";
            // 
            // button_take_hammer
            // 
            this.button_take_hammer.Location = new System.Drawing.Point(499, 397);
            this.button_take_hammer.Name = "button_take_hammer";
            this.button_take_hammer.Size = new System.Drawing.Size(129, 41);
            this.button_take_hammer.TabIndex = 64;
            this.button_take_hammer.Text = "Взять молоток";
            this.button_take_hammer.UseVisualStyleBackColor = true;
            this.button_take_hammer.Click += new System.EventHandler(this.button_take_hammer_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(226, 17);
            this.label2.TabIndex = 65;
            this.label2.Text = "Желаемое количество осколков:";
            // 
            // numericUpDown_count_shards
            // 
            this.numericUpDown_count_shards.Location = new System.Drawing.Point(282, 18);
            this.numericUpDown_count_shards.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown_count_shards.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericUpDown_count_shards.Name = "numericUpDown_count_shards";
            this.numericUpDown_count_shards.Size = new System.Drawing.Size(100, 22);
            this.numericUpDown_count_shards.TabIndex = 66;
            this.numericUpDown_count_shards.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // button_random_shard_pos
            // 
            this.button_random_shard_pos.Location = new System.Drawing.Point(634, 397);
            this.button_random_shard_pos.Name = "button_random_shard_pos";
            this.button_random_shard_pos.Size = new System.Drawing.Size(136, 41);
            this.button_random_shard_pos.TabIndex = 67;
            this.button_random_shard_pos.Text = "Перемешать";
            this.button_random_shard_pos.UseVisualStyleBackColor = true;
            this.button_random_shard_pos.Click += new System.EventHandler(this.button_random_shard_pos_Click);
            // 
            // button_new_glass
            // 
            this.button_new_glass.Location = new System.Drawing.Point(375, 397);
            this.button_new_glass.Name = "button_new_glass";
            this.button_new_glass.Size = new System.Drawing.Size(118, 41);
            this.button_new_glass.TabIndex = 68;
            this.button_new_glass.Text = "Новое стекло";
            this.button_new_glass.UseVisualStyleBackColor = true;
            // 
            // numericUpDown_holst_H
            // 
            this.numericUpDown_holst_H.Location = new System.Drawing.Point(446, 69);
            this.numericUpDown_holst_H.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown_holst_H.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown_holst_H.Name = "numericUpDown_holst_H";
            this.numericUpDown_holst_H.Size = new System.Drawing.Size(100, 22);
            this.numericUpDown_holst_H.TabIndex = 73;
            this.numericUpDown_holst_H.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(379, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 17);
            this.label3.TabIndex = 72;
            this.label3.Text = "- Высота";
            // 
            // numericUpDown_holst_W
            // 
            this.numericUpDown_holst_W.Location = new System.Drawing.Point(446, 41);
            this.numericUpDown_holst_W.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown_holst_W.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown_holst_W.Name = "numericUpDown_holst_W";
            this.numericUpDown_holst_W.Size = new System.Drawing.Size(100, 22);
            this.numericUpDown_holst_W.TabIndex = 71;
            this.numericUpDown_holst_W.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(379, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 17);
            this.label4.TabIndex = 70;
            this.label4.Text = "- Ширина";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(375, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(132, 17);
            this.label5.TabIndex = 69;
            this.label5.Text = "Параметры холста";
            // 
            // button_left
            // 
            this.button_left.Location = new System.Drawing.Point(7, 120);
            this.button_left.Name = "button_left";
            this.button_left.Size = new System.Drawing.Size(66, 63);
            this.button_left.TabIndex = 75;
            this.button_left.Text = "Левее";
            this.button_left.UseVisualStyleBackColor = true;
            // 
            // button_up
            // 
            this.button_up.Location = new System.Drawing.Point(78, 51);
            this.button_up.Name = "button_up";
            this.button_up.Size = new System.Drawing.Size(63, 63);
            this.button_up.TabIndex = 76;
            this.button_up.Text = "Выше";
            this.button_up.UseVisualStyleBackColor = true;
            // 
            // button_right
            // 
            this.button_right.Location = new System.Drawing.Point(150, 120);
            this.button_right.Name = "button_right";
            this.button_right.Size = new System.Drawing.Size(63, 63);
            this.button_right.TabIndex = 77;
            this.button_right.Text = "Правее";
            this.button_right.UseVisualStyleBackColor = true;
            // 
            // button_down
            // 
            this.button_down.Location = new System.Drawing.Point(78, 192);
            this.button_down.Name = "button_down";
            this.button_down.Size = new System.Drawing.Size(63, 63);
            this.button_down.TabIndex = 78;
            this.button_down.Text = "Ниже";
            this.button_down.UseVisualStyleBackColor = true;
            // 
            // button_hit
            // 
            this.button_hit.Location = new System.Drawing.Point(79, 120);
            this.button_hit.Name = "button_hit";
            this.button_hit.Size = new System.Drawing.Size(63, 63);
            this.button_hit.TabIndex = 79;
            this.button_hit.Text = "Удар";
            this.button_hit.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.numericUpDown_hit_y);
            this.groupBox1.Controls.Add(this.numericUpDown_hit_x);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.numericUpDown_step);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.numericUpDown_count_shards);
            this.groupBox1.Controls.Add(this.button_hit);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.button_up);
            this.groupBox1.Controls.Add(this.button_down);
            this.groupBox1.Controls.Add(this.button_right);
            this.groupBox1.Controls.Add(this.button_left);
            this.groupBox1.Location = new System.Drawing.Point(382, 131);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(388, 260);
            this.groupBox1.TabIndex = 80;
            this.groupBox1.TabStop = false;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(215, 48);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(168, 21);
            this.checkBox1.TabIndex = 88;
            this.checkBox1.Text = "Показывать молоток";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(242, 175);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 17);
            this.label9.TabIndex = 87;
            this.label9.Text = "Y";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(242, 145);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 17);
            this.label7.TabIndex = 86;
            this.label7.Text = "X";
            // 
            // numericUpDown_hit_y
            // 
            this.numericUpDown_hit_y.Location = new System.Drawing.Point(266, 173);
            this.numericUpDown_hit_y.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown_hit_y.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_hit_y.Name = "numericUpDown_hit_y";
            this.numericUpDown_hit_y.Size = new System.Drawing.Size(90, 22);
            this.numericUpDown_hit_y.TabIndex = 85;
            this.numericUpDown_hit_y.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericUpDown_hit_x
            // 
            this.numericUpDown_hit_x.Location = new System.Drawing.Point(266, 145);
            this.numericUpDown_hit_x.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown_hit_x.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_hit_x.Name = "numericUpDown_hit_x";
            this.numericUpDown_hit_x.Size = new System.Drawing.Size(90, 22);
            this.numericUpDown_hit_x.TabIndex = 84;
            this.numericUpDown_hit_x.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(250, 77);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(36, 17);
            this.label8.TabIndex = 83;
            this.label8.Text = "Шаг:";
            // 
            // numericUpDown_step
            // 
            this.numericUpDown_step.Location = new System.Drawing.Point(293, 75);
            this.numericUpDown_step.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown_step.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_step.Name = "numericUpDown_step";
            this.numericUpDown_step.Size = new System.Drawing.Size(90, 22);
            this.numericUpDown_step.TabIndex = 82;
            this.numericUpDown_step.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_step.ValueChanged += new System.EventHandler(this.numericUpDown_step_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(219, 120);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(137, 17);
            this.label6.TabIndex = 80;
            this.label6.Text = "Координаты удара:";
            // 
            // button_load_glass
            // 
            this.button_load_glass.Location = new System.Drawing.Point(12, 397);
            this.button_load_glass.Name = "button_load_glass";
            this.button_load_glass.Size = new System.Drawing.Size(357, 41);
            this.button_load_glass.TabIndex = 81;
            this.button_load_glass.Text = "Загрузить стекло";
            this.button_load_glass.UseVisualStyleBackColor = true;
            this.button_load_glass.Click += new System.EventHandler(this.button_load_glass_Click);
            // 
            // Form_breake_glass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 453);
            this.Controls.Add(this.button_load_glass);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.numericUpDown_holst_H);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numericUpDown_holst_W);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button_new_glass);
            this.Controls.Add(this.button_random_shard_pos);
            this.Controls.Add(this.button_take_hammer);
            this.Controls.Add(this.numericUpDown_glass_height);
            this.Controls.Add(this.label29);
            this.Controls.Add(this.numericUpDown_glass_width);
            this.Controls.Add(this.label30);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(800, 500);
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "Form_breake_glass";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Режим ломания стекла";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_glass_height)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_glass_width)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_count_shards)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_holst_H)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_holst_W)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_hit_y)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_hit_x)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_step)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown_glass_height;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.NumericUpDown numericUpDown_glass_width;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Button button_take_hammer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown_count_shards;
        private System.Windows.Forms.Button button_random_shard_pos;
        private System.Windows.Forms.Button button_new_glass;
        private System.Windows.Forms.NumericUpDown numericUpDown_holst_H;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDown_holst_W;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button_left;
        private System.Windows.Forms.Button button_up;
        private System.Windows.Forms.Button button_right;
        private System.Windows.Forms.Button button_down;
        private System.Windows.Forms.Button button_hit;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numericUpDown_step;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numericUpDown_hit_y;
        private System.Windows.Forms.NumericUpDown numericUpDown_hit_x;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button button_load_glass;
    }
}