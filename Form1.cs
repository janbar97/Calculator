using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Calculator
{
    public partial class Form1 : Form
    {
        Double val = 0;
        string text = "";
        bool oper_pres = false;
        double precision = 0.1;
        double upLimitSinusCosinus = 1.0;


        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button_click(object sender, EventArgs e)
        {
            if((output.Text == "0")||(oper_pres))
            {
                output.Clear();
            }

            Button button = (Button)sender;
            output.Text = output.Text + button.Text;
            oper_pres = false;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            output.Text = "0";
        }

        private void op_pres(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            text = button.Text;
            val = Double.Parse(output.Text);
            oper_pres = true;
        }

        private void op_res(object sender, EventArgs e)
        {
            switch (text)
            {
                case "+":
                    output.Text = (val + Double.Parse(output.Text)).ToString();
                    break;
                case "-":
                    output.Text = (val - Double.Parse(output.Text)).ToString();
                    break;
                case "/":
                    output.Text = (val / Double.Parse(output.Text)).ToString();
                    break;
                case "*":
                    output.Text = (val * Double.Parse(output.Text)).ToString();
                    break;
                default:
                    break;
            }
            oper_pres = false;
        }

        private void c_pres(object sender, EventArgs e)
        {
            output.Clear();
            val = 0;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void sin_Click(object sender, EventArgs e)
        {
            clearChart();
            double x = Double.Parse(output.Text);
            double xRadian = DegreestoRadian (x);
            double result = Math.Sin((xRadian));
            output.Text = result.ToString();

            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Maximum = 2 * Math.PI;
            chart1.ChartAreas[0].AxisY.Minimum = -upLimitSinusCosinus;
            chart1.ChartAreas[0].AxisY.Maximum = upLimitSinusCosinus;

            // draw result point on chart
            chart1.Series[1].Points.AddXY(xRadian, result);


            for (double i =- 5; i <  5; i+= precision) {
                chart1.Series[0].Points.AddXY(i, Math.Sin(i));
            }

        }
       void clearChart() {
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            chart1.Series[2].Points.Clear();
        }
        double DegreestoRadian(double number) {
            return number * (Math.PI / 180);
        }

        private void B_Click(object sender, EventArgs e)
        {
            clearChart();

            double x = Double.Parse(output.Text);
            double xRadian = DegreestoRadian(x);
            double result = Math.Cos(xRadian);

            // WriteMode result to calculator
            output.Text = result.ToString();

            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Maximum = 2 * Math.PI;
            
            chart1.ChartAreas[0].AxisY.Minimum = -upLimitSinusCosinus;
            chart1.ChartAreas[0].AxisY.Maximum = upLimitSinusCosinus;

            // draw point
            chart1.Series[1].Points.AddXY(xRadian, result);

            // draw function
            for (double i = -5.0; i < 5.0; i += precision)
            {
                chart1.Series[0].Points.AddXY(i, Math.Cos(i));
            }
        }

        private void btnDraw_Click(object sender, EventArgs e)
        {

            clearChart();
            bool isError = false;
            double a = 0.0, b = 0.0, c = 0.0;
            errorProvider1.Clear();
            try
            {
               a = Double.Parse(txtBoxA.Text);
            }
            catch (Exception error) {
                errorProvider1.SetError(txtBoxA, "Podaj liczbę!");
                isError = true;
            }
            try
            {
                b = Double.Parse(txtBoxB.Text);
            }
            catch (Exception error)
            {
                errorProvider1.SetError(txtBoxB, "Podaj liczbę!");
                isError = true;
            }
            try
            {
                c = Double.Parse(txtBoxC.Text);
            }
            catch (Exception error)
            {
                errorProvider1.SetError(txtBoxC, "Podaj liczbę!");
                isError = true;
            }
            if (isError != true) {
                double x = 0;
                try {
                    x = Double.Parse(output.Text);
                }
                catch (Exception error) { }
                double result = square(a, b, c, Double.Parse(output.Text));
                // WriteMode result to calculator
                output.Text = result.ToString();

                // draw point result
                chart1.Series[1].Points.AddXY(x, result);

                // draw point zero
                if (delta(a, b, c) > 0)
                {
                    chart1.Series[2].Points.AddXY(x1(a, b, c), 0);
                    chart1.Series[2].Points.AddXY(x2(a, b, c), 0);
                }
                else if (delta(a, b, c) == 1) chart1.Series[2].Points.AddXY(x1(a, b, c), 0);


                // draw function
                for (double i = x - 5; i < x + 5; i += precision)
                {
                    chart1.Series[0].Points.AddXY(i, square(a, b, c, i));
                }
            }

        }

        double square(double a, double b, double c, double x) {
            return a * x * x + b * x + c;
        }
        double delta(double a, double b, double c) {
            return (b* b) - (4 * a * c);
        }
        double x1(double a, double b, double c) {
            return (-b - Math.Sqrt(delta(a, b, c))) / (2 * a);
        }
        double x2(double a, double b, double c)
        {
            return (-b + Math.Sqrt(delta(a, b, c))) / (2 * a);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            double x = Double.Parse(output.Text);
            clearChart();
            double xRadian = DegreestoRadian(x);
            double result = Math.Tan(xRadian);

            // WriteMode result to calculator
            output.Text = result.ToString();

            chart1.ChartAreas[0].AxisX.Minimum = -(Math.PI / 2);
            chart1.ChartAreas[0].AxisX.Maximum = (Math.PI / 2);


            chart1.ChartAreas[0].AxisY.Minimum = -10;
            chart1.ChartAreas[0].AxisY.Maximum = 10;

            // draw point
            chart1.Series[1].Points.AddXY(xRadian, result);

            // draw function
            for (double i = -(Math.PI / 2); i < (Math.PI / 2); i += precision)
            {
                chart1.Series[0].Points.AddXY(i, Math.Tan(i));
            }
        }
    }



}
