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
    public partial class GraphicsForm : Form
    {
        //Initialize global variables
        private Vector<double> x { get; set; } = Vector<double>.Build.Dense(100);
        private Vector<double> y { get; set; } = Vector<double>.Build.Dense(100);
        private Vector<double> y_out { get; set; } = Vector<double>.Build.Dense(100);
        private MinimizationResult minResult { get; set; }
        private double a { get; set; }
        private double b { get; set; }
        private double c { get; set; }
        private double X { get; set;}
        private double Y { get; set; }

        public GraphicsForm()
        {
            InitializeComponent();
        }

        public void UpdateData(Vector<double> xx, Vector<double> yy, Vector<double> yy_out, 
                                MinimizationResult minRes, double aa, double bb, double cc)
        {
            x = xx;
            y = yy;
            y_out = yy_out;
            minResult = minRes;
            a = aa;
            b = bb;
            c = cc;

            DrawImage();
        }

        private void GraphicsForm_Load(object sender, EventArgs e)
        {
            DrawImage();
        }

        private void DrawImage()
        {
            if (x.Count < 100 || y.Count < 100 || y_out.Count < 100)
            {
                Console.WriteLine("Exiting DrawImage--no data");
                return;
            }

            int PixelOffset = 0;
            int PixelSize = PixelOffset * 2;
            PixelSize = PixelSize >= 1 ? PixelSize : 1;

            int borderpadding = PixelOffset + 15;

            int DisplayWidth = PictureBox.DisplayRectangle.Width;
            int DisplayHeight = PictureBox.DisplayRectangle.Height;

            int ImageWidth = DisplayWidth - 2 * borderpadding;
            int ImageHeight = DisplayHeight - 2 * borderpadding;

            double Xmin = Math.Floor(x.Min());
            double Xmax = Math.Ceiling(x.Max());
            double Ymin = Math.Floor(Math.Min(y.Min(), y_out.Min()));
            double Ymax = Math.Ceiling(Math.Max(y.Max(), y_out.Max()));

            double xExtent = Math.Abs(Math.Ceiling(Xmax - Xmin));
            double yExtent = Math.Abs(Math.Ceiling(Ymax - Ymin));

            double WidthScale = (double)ImageWidth / xExtent;
            double HeightScale = (double)ImageHeight / yExtent;

            Bitmap imageBuffer = new Bitmap(DisplayWidth, DisplayHeight);
            Graphics iBgraphics = Graphics.FromImage(imageBuffer);

            iBgraphics.Clear(BackColor);

            Color color = Color.Black;
            Pen pen = new Pen(color, PixelSize)
            {
                StartCap = System.Drawing.Drawing2D.LineCap.Round,
                EndCap = System.Drawing.Drawing2D.LineCap.Round
            };

            Point startPoint;
            Point endPoint;

            //Draw fitted curve
            X = x[0] - Xmin;
            Y = y_out[0] - Ymin;

            X *= WidthScale; Y *= HeightScale;
            X -= PixelOffset; Y -= PixelOffset;
            X += borderpadding; Y += borderpadding;

            startPoint = new Point((int)X, (int)Y);

            for (int i = 1; i < 100; i++)
            {
                X = x[i] - Xmin;
                Y = y_out[i] - Ymin;

                X *= WidthScale; Y *= HeightScale;
                X -= PixelOffset; Y -= PixelOffset;
                X += borderpadding; Y += borderpadding;

                endPoint = new Point((int)X, (int)Y);

                iBgraphics.DrawLine(pen, startPoint, endPoint);

                startPoint = endPoint;
            }

            color = Color.Red;
            SolidBrush brush = new SolidBrush(color);

            PixelSize = PixelSize >= 2 ? PixelSize : 2;

            //Draw data points
            for (int i = 0; i < 100; i++)
            {
                X = x[i] - Xmin;
                Y = y[i] - Ymin;

                X *= WidthScale; Y *= HeightScale;
                X -= PixelOffset; Y -= PixelOffset;
                X += borderpadding; Y += borderpadding;

                Rectangle rectangle = new Rectangle((int)X, (int)Y, PixelSize, PixelSize);

                iBgraphics.FillEllipse(brush, rectangle);
            }

            //Display results
            imageBuffer.RotateFlip(RotateFlipType.RotateNoneFlipY);
            PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBox.Image = imageBuffer;
            if (minResult != null) SSETextBox.Text = String.Format("{0,4}", minResult.FunctionInfoAtMinimum.Value);

            pen.Dispose();
            brush.Dispose();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
