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
    // Fit exponential expression with three coefficients

    public partial class MainForm : Form
    {
        //Initialize global variables and objects
        
        // y = a + b * e^(c * x) where e is Euler's number.
        private double a { get; set; }
        private double b { get; set; }
        private double c { get; set; }

        private Vector<double> x { get; set; } = Vector<double>.Build.Dense(100);
        private Vector<double> y { get; set; } = Vector<double>.Build.Dense(100);
        private Vector<double> y_out { get; set; } = Vector<double>.Build.Dense(100);

        private Vector<double> InitCfs { get; } = 
            Vector<double>.Build.Dense(new[] { 3.0, 1.0, 0.6 });
        // The Nelder-Mead Simplex algorithm requires an initial guess at the
        // values of the coefficients for the fitted curve. This initial guess can
        // affect the algorithm's ability to find an appropriate solution. The values
        // entered here are somewhat arbitrary but not random; they have been found
        // to work well for purposes of this example.

        private Vector<double> FitCfs { get; set; }
        private double ErrorValue;

        private Random RanGen = new Random();
        GraphicsForm graphicsForm = new GraphicsForm();

        private double TheEquation(double a, double b, double c, double x)
        {
            // The desired form of the equation of the curve to fit to the data
            // is chosen by the researcher and entered here. In this example we use
            // y = a + b * e^(c * x) where e is Euler's number.

            double y;
            y = a + b * Math.Exp(c * x);
            return y;
        }

        private void SetSeedCoefficients()
        {
            // These initial coefficients are used to create a randomized data set.

            a = 2.0;
            b = 0.5;
            c = 0.05;

            DisplaySeedCoefficients();
            CreateDataSet();
        }

        private void SetRandomSeedCoefficients()
        {
            // These initial coefficients are used to create a randomized data set.

            a = 4.0 + 2.0 * RanGen.NextDouble();
            b = 0.5 * RanGen.NextDouble();
            c = 0.05 * RanGen.NextDouble();

            DisplaySeedCoefficients();
            CreateDataSet();
        }

        private void CreateOrdinates()
        {
            for (int i = 0; i < 100; i++) x[i] = Convert.ToDouble(i); // values span 0 to 99
        }

        private void CreateDataSet()
        {
            // Create a randomized data set of 100 (x, y) points to which to fit the curve.
            // These points are stored as an array of x-values and an array of y-values.
            // RanGen generates random values on the set [0..1] which are used to offset
            // the y-values of the data from a seed curve generated using TheEquation().
            // Because of this randomization, the curve coefficients of the fitted curve
            // returned by the Nelder-Mead Simplex solver will differ from those of the
            // seed curve. The randomization is arbitrarily set at plus-or-minus 2.5.

            double y_val;

            for (int i = 0; i < 100; i++)
            {
                y_val = TheEquation(a, b, c, x[i]);
                y[i] = y_val + 5 * (RanGen.NextDouble() - (1/2));  // add random error to y-value
            }
        }

        private void RunSolver(Vector<double> Coefficients)
        {
            //NelderMeadSimplex implementation begins here
            IObjectiveFunction objFunc = ObjectiveFunction.Value(SumOfSquaresOfErrors);
            MinimizationResult minResult = NelderMeadSimplex.Minimum(objectiveFunction: objFunc,
                                                                     initialGuess: Coefficients,
                                                                     convergenceTolerance: 1e-5,
                                                                     maximumIterations: 10000);
            //NelderMeadSimplex implementation ends here

            // Outputs
            FitCfs = minResult.MinimizingPoint;    // Fitted Coefficients
            ErrorValue = minResult.FunctionInfoAtMinimum.Value;
            for (int i = 0; i < 100; i++) y_out[i] = TheEquation(FitCfs[0], FitCfs[1], FitCfs[2], x[i]);

            DisplayResults(minResult.MinimizingPoint.ToString());
            graphicsForm.UpdateData(x, y, y_out, minResult, a, b, c);
        }

        private double SumOfSquaresOfErrors(Vector<double> v)
        {
            double err = 0;
            for (int i = 0; i < 100; i++)
            {
                double y_val = TheEquation(v[0], v[1], v[2], x[i]);
                err += Math.Pow(y_val - y[i], 2);
            }

            return err;
        }

        public MainForm()
        {
            InitializeComponent();

            CreateOrdinates();
            SetSeedCoefficients();

            RunSolver(InitCfs);
        }

        private void SolveButton_Click(object sender, EventArgs e)
        {
            CreateDataSet();
            RunSolver(InitCfs);
        }

        private void ReSolveButton_Click(object sender, EventArgs e)
        {
            if ((FitCfs == null) || (FitCfs.Count < 3))
            {
                Console.WriteLine("No Initial Guess Values");
                return;
            }

            RunSolver(FitCfs);
        }

        private void RandomizeButton_Click(object sender, EventArgs e)
        {
            SetRandomSeedCoefficients();
            RunSolver(InitCfs);
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            SetSeedCoefficients();
            RunSolver(InitCfs);
        }

        private void ShowGraphButton_Click(object sender, EventArgs e)
        {
            graphicsForm.Show();
        }

        private void DisplaySeedCoefficients()
        {
            String str;
            str = String.Format("{0,12:F4}{1,12:F4}{2,12:F4}", a, b, c);
            CoefficientsTextBox.Text = str;
        }

        private void DisplayResults(String str)
        {
            str += String.Format("\r\nSSE: {0,12:F4}", ErrorValue );
            str += String.Format("\r\n\r\nMinimizing Coefficients:\r\n" +
                "{0,12:F4}   {1,12:F4}   {2,12:F4}",
                FitCfs[0], FitCfs[1], FitCfs[2]);

            DisplayTextBox.Text = str;
        }

        private void MainForm_FormClosing(Object sender, FormClosingEventArgs e)
        {
            graphicsForm.Close();
        }
    }
}
