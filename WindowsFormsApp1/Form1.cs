using Concurrency;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private readonly Chapter01 _chapter01;

        public Form1()
        {
            InitializeComponent();

            _chapter01 = new Chapter01();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = _chapter01.Deadlock().ToString();
        }
    }
}
