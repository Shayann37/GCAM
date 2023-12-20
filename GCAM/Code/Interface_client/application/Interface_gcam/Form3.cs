using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interface_gcam
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 Form2 = new Form2();
            Form2.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}
