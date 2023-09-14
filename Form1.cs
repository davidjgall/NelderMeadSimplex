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
using MathNet.Numerics.Optimization;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace NelderMeadSimplexTest
{
    // Fit exponential expression with three parameters

    public partial class Form1 : Form
    {
        //Initialize global variables
        private Random RanGen = new Random();
        private double[] x { get; set; } = new double[100];
        private double[] y { get; set; } = new double[100];
        private double[] y_out { get; set; } = new double[100];
        private MinimizationResult minResult { get; set; }
        private double a { get; set; }
        private double b { get; set; }
        private double c { get; set; }
        Vector<double> initialGuess { get; } = Vector<double>.Build.Dense(new[] { 3.0, 1.0, 0.6 });

        Form2 GraphicsForm = new Form2();
        
        private void SetCoefficients()
        {
            a = 2.0;
            b = 0.5;
            c = 0.05;
        }

        private void RandomizeCoefficients()
        {
            a = 4.0 + 2.0 * RanGen.NextDouble();
            b = 0.5 * RanGen.NextDouble();
            c = 0.05 * RanGen.NextDouble();
        }

        private void InitializeData()
        {
            // create data set
            double y_val;

            for (int i = 0; i < 100; i++) x[i] = Convert.ToDouble(i); // values span 0 to 100
            for (int i = 0; i < 100; i++)
            {
                y_val = TheEquation(a, b, c, x[i]);
                y[i] = y_val + 5 * (RanGen.NextDouble() - (1/2));  // add error term to y-value
            }
        }

        private double TheEquation(double a, double b, double c, double x)
        {
            double y;
            y = a + b * Math.Exp(c * x);
            return y;
        }

        private void Solve()
        {
            InitializeData();
            RunSolver(initialGuess);
        }

        private void ReSolve()
        {
            if ((minResult == null) || (minResult.MinimizingPoint.Count < 3))
            {
                Console.WriteLine("No Initial Guess Values");
                return;
            }
            RunSolver(minResult.MinimizingPoint);
        }

        private void RunSolver(Vector<double> initialGuess)
        {
            //NelderMeadSimplex implementation begins here

            NelderMeadSimplex Simplex = new NelderMeadSimplex(convergenceTolerance: 1e-5, maximumIterations: 10000);
            var obj = ObjectiveFunction.Value(SumSqError);
            minResult = Simplex.FindMinimum(obj, initialGuess);

            StoreOutput();
            ShowResult();
            GraphicsForm.UpdateData(x, y, y_out, minResult, a, b, c);
            GraphicsForm.DrawImage();

            //Console.WriteLine(String.Format("minResult = {0}  iterations = {1}", minResult.FunctionInfoAtMinimum.Value, minResult.Iterations));
        }

        private double SumSqError(Vector<double> v)
        {
            double err = 0;
            for (int i = 0; i < 100; i++)
            {
                double y_val = TheEquation(v[0], v[1], v[2], x[i]);
                err += Math.Pow(y_val - y[i], 2);
            }
            //Console.WriteLine(String.Format("err = {0}", err));
            return err;
        }

        private void StoreOutput()
        {
            for (int i = 0; i < 100; i++)
            {
                y_out[i] = minResult.MinimizingPoint[0] + minResult.MinimizingPoint[1] * Math.Exp(minResult.MinimizingPoint[2] * x[i]);
            }
        }

        public Form1()
        {
            InitializeComponent();
            SetCoefficients();
            ShowFactors();
            GraphicsForm.ControlBox = false;
            Solve();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Solve();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ReSolve();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RandomizeCoefficients();
            ShowFactors();
            Solve();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SetCoefficients();
            ShowFactors();
            Solve();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            GraphicsForm.Show();
        }

        private void ShowFactors()
        {
            String str;
            str = String.Format("{0,12:F4}{1,12:F4}{2,12:F4}", a, b, c);
            textBox2.Text = str;
        }

        private void ShowResult()
        {
            String str;
            str = String.Format($"{minResult.MinimizingPoint.ToString()}");
            str += String.Format("\r\nSSE: {0,12:F4}\r\n\r\nMinimizing Coefficients:\r\n", minResult.FunctionInfoAtMinimum.Value);
            str += String.Format("{0,12:F4}   {1,12:F4}   {2,12:F4}", minResult.MinimizingPoint[0], minResult.MinimizingPoint[1], minResult.MinimizingPoint[2]);

            textBox1.Text = str;
        }

        private void Form1_FormClosing(Object sender, FormClosingEventArgs e)
        {
            GraphicsForm.Close();
        }
    }
}
