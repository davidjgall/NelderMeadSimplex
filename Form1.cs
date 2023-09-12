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
    public partial class Form1 : Form
    {
        //Initialize global variables
        private Random RanGen => new Random();
        private double[] x { get; set; } = new double[100];
        private double[] y { get; set; } = new double[100];
        private MinimizationResult minResult { get; set; }
        private double a { get; set; }
        private double b { get; set; }
        private double c { get; set; }

        private void SetCoefficients()
        {
            if ((a == 5.0) && (b == 0.5) && (c == 0.05))
            {
                Console.WriteLine("Not Resetting Coefficients");
                return;
            }

            a = 5.0;
            b = 0.5;
            c = 0.05;

            Console.WriteLine("Coefficients set to: a = {0,12:F4}   b = {0,12:F4}   c = {0,12:F4}", a, b, c);

            InitializeData();
        }

        private void RandomizeCoefficients()
        {
            a = 5.0 * RanGen.NextDouble();
            b = 0.5 * RanGen.NextDouble();
            c = 0.05 * RanGen.NextDouble();

            Console.WriteLine("Coefficients set to: a = {0,12:F4}   b = {1,12:F4}   c = {2,12:F4}", a, b, c);

            InitializeData();
        }

        private void InitializeData()
        {
            // create data set
            for (int i = 0; i < 100; i++) x[i] = 10 + Convert.ToDouble(i) * 90.0 / 99.0; // values span 10 to 100
            for (int i = 0; i < 100; i++)
            {
                double y_val = a + b * Math.Exp(c * x[i]);
                y[i] = y_val + 0.1 * RanGen.NextDouble() * y_val;  // add error term scaled to y-value
            }

            Console.WriteLine("Data Initialized");

            ShowFactors();
        }

        private void Solve()
        {
            InitializeData();

            //Fit exponential expression with three parameters
            //NelderMeadSimplex implementation begins here
            NelderMeadSimplex Simplex = new NelderMeadSimplex(convergenceTolerance: 1e-5, maximumIterations: 10000);

            //Vector<double> initialGuess = Vector<double>.Build.Dense(3);
            //var initialGuess = new DenseVector(new[] { 3.0, 6.0, 0.6 });
            //DenseVector initialGuess = new DenseVector(3);
            //DenseVector initialGuess = new DenseVector(new[] { 3.0, 6.0, 0.6 });
            Vector<double> initialGuess = Vector<double>.Build.Dense(new[] { 3.0, 6.0, 0.6 });

            //f1 is a dummy lambda expression for testing
            //var f1 = new Func<Vector<double>, double>(x => 2.5);
            //var obj = ObjectiveFunction.Value(f1);
            var obj = ObjectiveFunction.Value(SumSqError);

            minResult = Simplex.FindMinimum(obj, initialGuess);

            ShowResult();
        }

        private void ReSolve()
        {
            //Initialize();
            if ((minResult == null) || (minResult.MinimizingPoint.Count < 3))
            {
                Console.WriteLine("No Initial Guess Values");
                return;
            }

            //Fit exponential expression with three parameters
            //NelderMeadSimplex implementation begins here
            NelderMeadSimplex Simplex = new NelderMeadSimplex(convergenceTolerance: 1e-5, maximumIterations: 10000);

            //Vector<double> initialGuess = Vector<double>.Build.Dense(3);
            //var initialGuess = new DenseVector(new[] { 3.0, 6.0, 0.6 });
            //DenseVector initialGuess = new DenseVector(3);
            //DenseVector initialGuess = new DenseVector(new[] { 3.0, 6.0, 0.6 });

            //DenseVector initialGuess = (DenseVector)minResult.MinimizingPoint;
            Vector<double> initialGuess = minResult.MinimizingPoint;

            Console.WriteLine(initialGuess.ToString());


            //f1 is a dummy lambda expression for testing
            //var f1 = new Func<Vector<double>, double>(x => 2.5);
            //var obj = ObjectiveFunction.Value(f1);
            var obj = ObjectiveFunction.Value(SumSqError);

            minResult = Simplex.FindMinimum(obj, initialGuess);

            ShowResult();
        }

        private double SumSqError(Vector<double> v)
        {
            double err = 0;
            for (int i = 0; i < 100; i++)
            {
                double y_val = v[0] + v[1] * Math.Exp(v[2] * x[i]);
                err += Math.Pow(y_val - y[i], 2);
            }
            return err;
        }

        public Form1()
        {
            InitializeComponent();
            SetCoefficients();
            ShowFactors();
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
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SetCoefficients();
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
    }
}
