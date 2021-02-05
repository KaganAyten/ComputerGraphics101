using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BGFinal
{
    public partial class BGForm : Form
    {
        public BGForm()
        {
            InitializeComponent();
        }
        System.Drawing.Graphics Line;
        Pen Pen = new Pen(Color.Red, 2);

        //default triangle
        Double[,] triangeMatrix = new Double[3, 3] { { 2, 3, 1 }, { 7, 3, 1 }, { 2, 6, 1 } };
        Double[,] reflectMatrix = new Double[3, 3] { { 1, 0, 0 }, { 0, -1, 0 }, { 0, 0, 1 } };
        Double[,] deleteTranslateMatrix = new Double[3, 3];
        private void Form1_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
        }

        private void kapatLabel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void isimLabel_Click(object sender, EventArgs e)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo("https://kaganayten.com/");
            Process.Start(sInfo);
        }
        private int CalculateCoordinateX(Double tempX)
        {
            return Convert.ToInt32(300 + 250 + (200 * tempX));
        }
        private int CalculateCoordinateY(Double tempY)
        {
            return Convert.ToInt32(250 + (-200 * tempY));
        }
        private void GridCiz(Double limitX, Double limitY)
        {
            Line = this.CreateGraphics();
            Pen.Color = Color.SlateGray;
            for(double i = (-limitY); i < limitY; i+=0.1)
            {
                Line.DrawLine(Pen, CalculateCoordinateX(-limitX), CalculateCoordinateY(i),CalculateCoordinateX(limitX),CalculateCoordinateY(i));
            }
            for (double i = (-limitX); i < limitX; i += 0.1)
            {
                Line.DrawLine(Pen, CalculateCoordinateX(i), CalculateCoordinateY(-limitY), CalculateCoordinateX(i), CalculateCoordinateY(limitY));
            }
        }
        private void eksenCizButton_Click(object sender, EventArgs e)
        {
            GridCiz(1,1);
            Line = this.CreateGraphics();
            Pen.Color = Color.White;
            Line.DrawLine(Pen, CalculateCoordinateX(-1), CalculateCoordinateY(0), CalculateCoordinateX(1), CalculateCoordinateY(0));
            Line.DrawLine(Pen, CalculateCoordinateX(0), CalculateCoordinateY(-1), CalculateCoordinateX(0), CalculateCoordinateY(1));
        }

        private Double downSize(Double value)
        {
            //ekrana sığdırmak için
            return value*0.1;
        }

        private void nesneyiCizButton_Click(object sender, EventArgs e)
        {
            Line = this.CreateGraphics();
            Pen.Color = Color.Red;

            if(!(aX.Text==""||aY.Text==""|| bX.Text == "" || bY.Text == "" || cX.Text == "" || cY.Text == ""))
            {
                triangeMatrix[0, 0] = Convert.ToInt32(aX.Text);
                triangeMatrix[0, 1] = Convert.ToInt32(aY.Text);
                triangeMatrix[1, 0] = Convert.ToInt32(bX.Text);
                triangeMatrix[1, 1] = Convert.ToInt32(bY.Text);
                triangeMatrix[2, 0] = Convert.ToInt32(cX.Text);
                triangeMatrix[2, 1] = Convert.ToInt32(cY.Text);
                Line.DrawLine(Pen, CalculateCoordinateX(downSize(triangeMatrix[0, 0])), CalculateCoordinateY(downSize(triangeMatrix[0, 1])), CalculateCoordinateX(downSize(triangeMatrix[1, 0])), CalculateCoordinateY(downSize(triangeMatrix[1, 1])));
                Line.DrawLine(Pen, CalculateCoordinateX(downSize(triangeMatrix[1, 0])), CalculateCoordinateY(downSize(triangeMatrix[1, 1])), CalculateCoordinateX(downSize(triangeMatrix[2, 0])), CalculateCoordinateY(downSize(triangeMatrix[2, 1])));
                Line.DrawLine(Pen, CalculateCoordinateX(downSize(triangeMatrix[2, 0])), CalculateCoordinateY(downSize(triangeMatrix[2, 1])), CalculateCoordinateX(downSize(triangeMatrix[0, 0])), CalculateCoordinateY(downSize(triangeMatrix[0, 1])));
            }
            else
            {
                Line.DrawLine(Pen, CalculateCoordinateX(downSize(triangeMatrix[0, 0])), CalculateCoordinateY(downSize(triangeMatrix[0, 1])), CalculateCoordinateX(downSize(triangeMatrix[1, 0])), CalculateCoordinateY(downSize(triangeMatrix[1, 1])));
                Line.DrawLine(Pen, CalculateCoordinateX(downSize(triangeMatrix[1, 0])), CalculateCoordinateY(downSize(triangeMatrix[1, 1])), CalculateCoordinateX(downSize(triangeMatrix[2, 0])), CalculateCoordinateY(downSize(triangeMatrix[2, 1])));
                Line.DrawLine(Pen, CalculateCoordinateX(downSize(triangeMatrix[2, 0])), CalculateCoordinateY(downSize(triangeMatrix[2, 1])), CalculateCoordinateX(downSize(triangeMatrix[0, 0])), CalculateCoordinateY(downSize(triangeMatrix[0, 1])));
            }
            
        }

        private void yansitButton_Click(object sender, EventArgs e)
        {
            Double[,] tempReflectMatrix = new Double[3, 3];
            Line = this.CreateGraphics();
            Pen.Color = Color.Cyan;
            reflectMatrix[1, 1] = -1;

            reflectMatrix[0, 0] = 1;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    tempReflectMatrix[i, j] = 0;
                    for(int k = 0; k < 3; k++)
                    {
                        tempReflectMatrix[i, j] += triangeMatrix[i, k] * reflectMatrix[k, j];
                    }
                }
            }

            Line.DrawLine(Pen, CalculateCoordinateX(downSize(tempReflectMatrix[0, 0])), CalculateCoordinateY(downSize(tempReflectMatrix[0, 1])), CalculateCoordinateX(downSize(tempReflectMatrix[1, 0])), CalculateCoordinateY(downSize(tempReflectMatrix[1, 1])));
            Line.DrawLine(Pen, CalculateCoordinateX(downSize(tempReflectMatrix[1, 0])), CalculateCoordinateY(downSize(tempReflectMatrix[1, 1])), CalculateCoordinateX(downSize(tempReflectMatrix[2, 0])), CalculateCoordinateY(downSize(tempReflectMatrix[2, 1])));
            Line.DrawLine(Pen, CalculateCoordinateX(downSize(tempReflectMatrix[2, 0])), CalculateCoordinateY(downSize(tempReflectMatrix[2, 1])), CalculateCoordinateX(downSize(tempReflectMatrix[0, 0])), CalculateCoordinateY(downSize(tempReflectMatrix[0, 1])));
        }
        private void reflectYButton_Click(object sender, EventArgs e)
        {
            Double[,] tempReflectMatrix = new Double[3, 3];
            Line = this.CreateGraphics();
            Pen.Color = Color.Cyan;
            reflectMatrix[0, 0] = -1;
            reflectMatrix[1, 1] = 1;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    tempReflectMatrix[i, j] = 0;
                    for (int k = 0; k < 3; k++)
                    {
                        tempReflectMatrix[i, j] += triangeMatrix[i, k] * reflectMatrix[k, j];
                    }
                }
            }

            Line.DrawLine(Pen, CalculateCoordinateX(downSize(tempReflectMatrix[0, 0])), CalculateCoordinateY(downSize(tempReflectMatrix[0, 1])), CalculateCoordinateX(downSize(tempReflectMatrix[1, 0])), CalculateCoordinateY(downSize(tempReflectMatrix[1, 1])));
            Line.DrawLine(Pen, CalculateCoordinateX(downSize(tempReflectMatrix[1, 0])), CalculateCoordinateY(downSize(tempReflectMatrix[1, 1])), CalculateCoordinateX(downSize(tempReflectMatrix[2, 0])), CalculateCoordinateY(downSize(tempReflectMatrix[2, 1])));
            Line.DrawLine(Pen, CalculateCoordinateX(downSize(tempReflectMatrix[2, 0])), CalculateCoordinateY(downSize(tempReflectMatrix[2, 1])), CalculateCoordinateX(downSize(tempReflectMatrix[0, 0])), CalculateCoordinateY(downSize(tempReflectMatrix[0, 1])));
        }

        private void dondurButton_Click(object sender, EventArgs e)
        {

            
            if (aciInput.Text == "")
            {
                MessageBox.Show("PLEASE ENTER VALID DEGREE!", "WARNING",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else
            {

                int inputDeg = Convert.ToInt32(aciInput.Text);
                double val = inputDeg * (Math.PI/180);
                Double[,] tempTranslateMatrix = new Double[3, 3];
                Double[,] translateMatrix = new Double[3, 3];
                for (int i = 0; i < 3; i++)
                {
                    for(int j = 0; j < 3; j++)
                    {
                        translateMatrix[i, j] = 0;
                    }
                }
                translateMatrix[0, 0] = Math.Cos(val);
                translateMatrix[0, 1] = -1*Math.Sin(val);
                translateMatrix[1, 0] = Math.Sin(val);
                translateMatrix[1, 1] = Math.Cos(val);
                translateMatrix[2, 2] = 1;

                for(int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        tempTranslateMatrix[i, j] = 0;
                        for(int k = 0; k < 3; k++)
                        {
                            tempTranslateMatrix[i, j] += triangeMatrix[i, k] * translateMatrix[k, j];
                        }
                    }
                }
                deleteTranslateMatrix = tempTranslateMatrix;
                Line = this.CreateGraphics();
                Pen.Color = Color.LightGreen;
                Line.DrawLine(Pen, CalculateCoordinateX(downSize(tempTranslateMatrix[0, 0])), CalculateCoordinateY(downSize(tempTranslateMatrix[0, 1])), CalculateCoordinateX(downSize(tempTranslateMatrix[1, 0])), CalculateCoordinateY(downSize(tempTranslateMatrix[1, 1])));
                Line.DrawLine(Pen, CalculateCoordinateX(downSize(tempTranslateMatrix[1, 0])), CalculateCoordinateY(downSize(tempTranslateMatrix[1, 1])), CalculateCoordinateX(downSize(tempTranslateMatrix[2, 0])), CalculateCoordinateY(downSize(tempTranslateMatrix[2, 1])));
                Line.DrawLine(Pen, CalculateCoordinateX(downSize(tempTranslateMatrix[2, 0])), CalculateCoordinateY(downSize(tempTranslateMatrix[2, 1])), CalculateCoordinateX(downSize(tempTranslateMatrix[0, 0])), CalculateCoordinateY(downSize(tempTranslateMatrix[0, 1])));
            }
        }
        private void temizleButton_Click(object sender, EventArgs e)
        {
            Pen.Color = Color.SlateGray;
            Line.DrawLine(Pen, CalculateCoordinateX(downSize(deleteTranslateMatrix[0, 0])), CalculateCoordinateY(downSize(deleteTranslateMatrix[0, 1])), CalculateCoordinateX(downSize(deleteTranslateMatrix[1, 0])), CalculateCoordinateY(downSize(deleteTranslateMatrix[1, 1])));
            Line.DrawLine(Pen, CalculateCoordinateX(downSize(deleteTranslateMatrix[1, 0])), CalculateCoordinateY(downSize(deleteTranslateMatrix[1, 1])), CalculateCoordinateX(downSize(deleteTranslateMatrix[2, 0])), CalculateCoordinateY(downSize(deleteTranslateMatrix[2, 1])));
            Line.DrawLine(Pen, CalculateCoordinateX(downSize(deleteTranslateMatrix[2, 0])), CalculateCoordinateY(downSize(deleteTranslateMatrix[2, 1])), CalculateCoordinateX(downSize(deleteTranslateMatrix[0, 0])), CalculateCoordinateY(downSize(deleteTranslateMatrix[0, 1])));
            aciInput.Text = "";
            MessageBox.Show("LAST ROTATED TRIANGLE IS FADED", "TAKEN BACK", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        
    }
}
