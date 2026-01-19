using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace ProjectGlass
{

    public partial class Form_loading : Form
    {
        public bool result = true;
        //int Properties.Settings.Default.load_max;
        public string load_task_string;

        public Form_loading(Form_main f)
        {

            InitializeComponent();
            progressBar1.Value = 0;
            progressBar1.Maximum = Properties.Settings.Default.load_max + 1;
            progressBar1.Step = 1;
            load_task_string = f.load_task_string;
            label1.Text = f.load_message_string;
            //Properties.Settings.Default.load_max = Properties.Settings.Default.load_max + 1;
        }

        private void Form2_Activated(object sender, EventArgs e)
        {
            Properties.Settings.Default.load_active = true;
            CenterToParent();
            Progress();
        }

        public void Form2_Leave(object sender, EventArgs e)
        {
            
        }

        async void Progress()
        {
            
            while (progressBar1.Value < Properties.Settings.Default.load_max)
            {
                await Task.Delay(10);
                progressBar1.Increment(1);
                progressBar1.Refresh();
            }
            await Task.Delay(500);
            
            
            Properties.Settings.Default.load_active = false;
            Close();
            
        }
    }
}
