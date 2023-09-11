using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.Optimization;
using MathNet.Numerics.LinearAlgebra.Double;

namespace NelderMeadSimplex
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String str;
            str = String.Format("{0}\r\n{1}\r\n\r\n", "Thank you.", "This is great.");
            textBox1.Text = str;

            // Insert NelderMeadSimplex implementation here

            MathNet.Numerics.Optimization.NelderMeadSimplex simplex = new MathNet.Numerics.Optimization.NelderMeadSimplex(.00001,10000);
            textBox1.Text = simplex.ConvergenceTolerance.ToString();

        }
    }
}
