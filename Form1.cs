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
           double x = Double.Parse(output.Text);
            double result = sinFunction(x);
            output.Text = result.ToString();


            chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart1.Series[1].Points.AddXY(x, result);


            for (double i = Math.Round(result)- 4; i < Math.Round(result) + 4; i++) {
                chart1.Series[0].Points.AddXY(i, sinFunction(i));
            }


        }
       
        double sinFunction(double x)
        {
            double degrees = x * (180 / Math.PI);
            return Math.Sin(degrees);
        }
    
    
    }



}
