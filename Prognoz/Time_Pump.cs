using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prognoz
{
    public partial class Time_Pump : Form
    {
        public Time_Pump()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                double Time_Working_Pump = -(double.Parse(textBox4.Text) / double.Parse(textBox3.Text)) * Math.Log((double.Parse(textBox2.Text)) / (double.Parse(textBox1.Text)));

                TimeSpan time = TimeSpan.FromHours(Time_Working_Pump);
                label1.Text = time.ToString();

                DateTime R = DateTime.Now;

                label7.Text = R.AddHours(Time_Working_Pump).ToString("dd.MM.yy HH:mm:ss");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Time_Pump_Load(object sender, EventArgs e)
        {
            double Krit_concentrathion = 7.92;
            double Total_amount = 290;
            textBox2.Text = Krit_concentrathion.ToString();
            textBox4.Text = Total_amount.ToString();
          
            Form1 main = this.Owner as Form1;

            for (int i = 0; i < main.MyAllSensors.Count; i++)
            {
                comboBox1.Items.Add(main.MyAllSensors[i].KKS_Name);
            }
        }
      int IndexBor;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int IndexBor = 0;
            Form1 main = this.Owner as Form1;

             for (int i = 0; i < main.MyAllSensors.Count; i++)
                {
                    if (comboBox1.Text == main.MyAllSensors[i].KKS_Name)
                    {

                        IndexBor = i;
                        MessageBox.Show(IndexBor.ToString() + " " + main.MyAllSensors[IndexBor].KKS_Name);

                        break;

                    }
                }
             
          //   MessageBox.Show(main.MyAllSensors[IndexBor].KKS_Name);
             textBox1.Text = main.MyAllSensors[IndexBor].MyListRecordsForOneKKS[main.MyAllSensors[IndexBor].MyListRecordsForOneKKS.Count-1].Value.ToString();
       
        
        }
    }
}
