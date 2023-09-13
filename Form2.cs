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
    public partial class Form2 : Form
    {
        //Initialize global variables
        private double[] x { get; set; } = new double[100];
        private double[] y { get; set; } = new double[100];
        private double[] y_out { get; set; } = new double[100];
        private MinimizationResult minResult { get; set; }
        private double a { get; set; }
        private double b { get; set; }
        private double c { get; set; }

        public Form2()
        {
            InitializeComponent();
        }

        public void UpdateData(double[] xx, double[] yy, double[] yy_out, MinimizationResult minRes, double aa, double bb, double cc)
        {
            x = xx;
            y = yy;
            y_out = yy_out;
            minResult = minRes;
            a = aa;
            b = bb;
            c = cc;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            DrawImage();
        }

        public void DrawImage()
        {
            if (x.Length < 100 || y.Length < 100 || y_out.Length < 100)
            {
                Console.WriteLine("Exiting DrawImage--no data");
                return;
            }

            int ImageWidth = pictureBox1.Width;
            int ImageHeight = pictureBox1.Height;

            Bitmap imageBuffer = new Bitmap(ImageWidth, ImageHeight);
            Graphics iBgraphics = Graphics.FromImage(imageBuffer);

            int borderpadding = 10;
            int xExtent = (int)(Math.Ceiling(x.Max()) - Math.Floor(x.Min())) + borderpadding;
            int yExtent = (int)(Math.Ceiling(y.Max()) - Math.Floor(y.Min())) + borderpadding;
            float WidthScale = ImageWidth / xExtent;
            float HeightScale = ImageHeight / yExtent;
            float PixelScale = (float)5.0;

            Color color = Color.Black;
            Pen pen = new Pen(color, 2)
            {
                StartCap = System.Drawing.Drawing2D.LineCap.Round,
                EndCap = System.Drawing.Drawing2D.LineCap.Round,
                Width = PixelScale
            };

            Point startPoint;
            Point endPoint;

            //Draw fitted curve
            startPoint = new Point((int)(x[0] * WidthScale - PixelScale / 2) + borderpadding, (int)(y_out[0] * HeightScale - PixelScale / 2) + borderpadding);
            for (int i = 1; i < 100; i++)
            {
                endPoint = new Point((int)(x[i] * WidthScale - PixelScale / 2) + borderpadding, (int)(y_out[i] * HeightScale - PixelScale / 2) + borderpadding);
                iBgraphics.DrawLine(pen, startPoint, endPoint);
                startPoint = endPoint;
            }

            color = Color.Red;
            SolidBrush brush = new SolidBrush(color);

            //Draw data points
            for (int i = 0; i < 100; i++)
            {
                Rectangle rectangle = new Rectangle((int)(x[i] * WidthScale - PixelScale / 2) + borderpadding, (int)(y[i] * HeightScale - PixelScale / 2) + borderpadding, (int)PixelScale, (int)PixelScale);
                iBgraphics.FillEllipse(brush, rectangle);
            }

            //Display results
            pictureBox1.Image = imageBuffer;
            if (minResult != null) textBox1.Text = String.Format("{0,4}", minResult.FunctionInfoAtMinimum.Value);

            pen.Dispose();
            brush.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
