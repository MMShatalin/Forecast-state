using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MathNet.Numerics;

using SD = System.Data;
using System.Windows.Forms.DataVisualization.Charting;

namespace Prognoz
{
    public partial class Form1 : Form
    {
        // private readonly Timer tmrShow;
        public Form1()
        {
            InitializeComponent();

      
        }


        private System.Collections.ArrayList customers = new System.Collections.ArrayList();
        private MyVirtualClass customerInEdit;
        private int rowInEdit = -1;
        private bool rowScopeCommit = true;
        public MyListOfSensors MyAllSensors = new MyListOfSensors();
        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_MouseDown(object sender, MouseEventArgs e)
        {

        }
        private void dataGridView1_CellValueNeeded(object sender,
        System.Windows.Forms.DataGridViewCellValueEventArgs e)
        {
            //this.dataGridView1.RowCount = 1;
            // If this is the row for new records, no values are needed.
            if (e.RowIndex == this.dataGridView1.RowCount - 1) return;
            //   if (e.RowIndex == this.dataGridView3.RowCount - 1) return;

            MyVirtualClass customerTmp = null;

            // Store a reference to the Customer object for the row being painted.
            if (e.RowIndex == rowInEdit)
            {
                customerTmp = this.customerInEdit;
            }
            else
            {
                customerTmp = (MyVirtualClass)this.customers[e.RowIndex];
            }

            // Set the cell value to paint using the Customer object retrieved.
            switch (this.dataGridView1.Columns[e.ColumnIndex].Name)
            {
                case "Time":
                    e.Value = customerTmp.time;
                    break;

                case "Value":
                    e.Value = customerTmp.value;
                    break;
            }
        }
        int X1, X2, X3, X4;




        private void Form1_Load(object sender, EventArgs e)
        {
           
                textBox2.Text = "8,94";
            
           // button4.Enabled = false;
            button5.Enabled = false;
            button3.Enabled = false;
            chart1.ChartAreas[0].AxisX.MinorGrid.Enabled = true;
            chart1.ChartAreas[0].AxisX.MinorGrid.LineDashStyle = ChartDashStyle.Dash;
            chart1.ChartAreas[0].AxisX.MinorGrid.LineColor = Color.Gray;


            chart1.ChartAreas[0].AxisY.MinorTickMark.Enabled = true;
            chart1.ChartAreas[0].AxisY.MinorTickMark.LineDashStyle = ChartDashStyle.Dash;
            chart1.ChartAreas[0].AxisY.MinorGrid.Enabled = true;
            chart1.ChartAreas[0].AxisY.MinorGrid.LineDashStyle = ChartDashStyle.Dash;
            chart1.ChartAreas[0].AxisY.MinorTickMark.LineDashStyle = ChartDashStyle.Dash;
            chart1.ChartAreas[0].AxisY.MinorGrid.LineColor = Color.Gray;
            chart1.ChartAreas[0].AxisY.MinorTickMark.LineColor = Color.Black;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Gray;
            chart1.ChartAreas[0].AxisY.MajorTickMark.LineColor = Color.Gray;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            chart1.ChartAreas[0].AxisY.MajorTickMark.LineDashStyle = ChartDashStyle.Dash;
            chart1.ChartAreas[0].AxisX.MinorTickMark.LineDashStyle = ChartDashStyle.Dash;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.Gray;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            chart1.ChartAreas[0].AxisX.MajorTickMark.LineDashStyle = ChartDashStyle.Dash;

            chart1.ChartAreas[0].AxisY.ArrowStyle = AxisArrowStyle.Lines;
            chart1.ChartAreas[0].AxisX.ArrowStyle = AxisArrowStyle.Lines;

            chart1.Series[0].Color = Color.Red;
            chart1.Series[1].Color = Color.Blue;
            chart1.Series[2].Color = Color.Green;
            chart1.Series[3].Color = Color.Violet;

            for (int i = 1; i < 5; i++)
            {
                chart1.Series["Series" + i].IsVisibleInLegend = false;
            }

            //  dataGridView1.Columns[0].Width = 200;
            //   dataGridView1.Columns[1].Width = 200;

            this.dataGridView1.VirtualMode = true;

            DataGridViewTextBoxColumn companyNameColumn = new DataGridViewTextBoxColumn();
            companyNameColumn.HeaderText = "Time";
            companyNameColumn.Name = "Time";
            this.dataGridView1.Columns.Add(companyNameColumn);

            DataGridViewTextBoxColumn companyNameColumn1 = new DataGridViewTextBoxColumn();
            companyNameColumn1.HeaderText = "Value";
            companyNameColumn1.Name = "Value";
            this.dataGridView1.Columns.Add(companyNameColumn1);
            dataGridView1.Columns[0].Width = 150;
            dataGridView1.Columns[1].Width = 150;
        }
        public List<string> MyListKKS = new List<string>();
        private void button1_Click(object sender, EventArgs e)
        {

            Time_Pump main = this.Owner as Time_Pump;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                foreach (var item in openFileDialog1.FileNames)
                {
                    MyAllSensors.LoadFromFile(item, MyAllSensors);
                }
                //    MyAllSensors.Sort();

                //checkedListBox1.Visible = false;

                for (int i = 0; i < MyAllSensors.Count; i++)
                {
                    comboBox1.Items.Add(MyAllSensors[i].KKS_Name);
                    MyListKKS.Add(MyAllSensors[i].KKS_Name);
                }
                //  checkedListBox1.Visible = true;
            }
            button5.Enabled = true;
            //button4.Enabled = true;
          //  button3.Enabled = true;
        }

        private void splitContainer2_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer2_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Visible = false;
                Sencors myOneKKS = new Sencors();
                myOneKKS = MyAllSensors.getOneKKSByIndex(comboBox1.SelectedIndex);
                this.dataGridView1.CellValueNeeded += new
                    DataGridViewCellValueEventHandler(dataGridView1_CellValueNeeded);

                dataGridView1.Rows.Clear();
                customers.Clear();

                for (int i = 0; i < myOneKKS.MyListRecordsForOneKKS.Count; i++)
                {
                    this.customers.Add(new MyVirtualClass(myOneKKS.MyListRecordsForOneKKS[i].DateTime.ToString(), myOneKKS.MyListRecordsForOneKKS[i].Value.ToString()));

                }
                if (this.dataGridView1.RowCount == 0)
                {
                    this.dataGridView1.RowCount = 1;

                }

                this.dataGridView1.RowCount = myOneKKS.MyListRecordsForOneKKS.Count;
                dataGridView1.Visible = true;
            }
            catch (Exception ex0)
            {
                MessageBox.Show(ex0.Message);
            }
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }
        int NumberSeries = 0;
        int MyBeginIndex;

        int IndexAFP;

        List<double> X = new List<double>();
        List<double> Y = new List<double>();

        int IndexBor;
        
        private void krit_prognoz_concentration()
        {
            for (int i = 0; i < MyAllSensors.Count; i++)
            {
                if (comboBox1.Text == MyAllSensors[i].KKS_Name)
                {
                    IndexBor = i;
                    break;
                }
            }
          //  double sum = 0;
            double C0 = (MyAllSensors[IndexBor].MyListRecordsForOneKKS[0].Value + MyAllSensors[IndexBor].MyListRecordsForOneKKS[1].Value)/2;
            //double t0 = MyAllSensors[IndexBor].MyListRecordsForOneKKS[0].DateTime.ToOADate();
         
            for (int j = 0; j < MyAllSensors[IndexBor].MyListRecordsForOneKKS.Count; j++)
            {
                Y.Add(Math.Log( MyAllSensors[IndexBor].MyListRecordsForOneKKS[j].Value/C0));
                X.Add(MyAllSensors[IndexBor].MyListRecordsForOneKKS[j].DateTime.ToOADate());
                chart1.Series[NumberSeries].Points.AddXY(X[X.Count - 1], Y[Y.Count - 1]);   
          
            }
           // chart1.Series[NumberSeries].Points.AddXY(X[X.Count - 1], Y[Y.Count - 1]);   
       //     MessageBox.Show(MyVal.ToString());
            NumberSeries++;

            Tuple<double, double> myLine1 = Fit.Line(X.ToArray(), Y.ToArray());

         //  double MyCritika = 0;
            //for (int j = 0; j < X.Count; j++)
            //{
            //    chart1.Series[NumberSeries].Points.AddXY(X[j], X[j] * myLine1.Item2 + myLine1.Item1);
            //}
            chart1.Series[NumberSeries].Points.AddXY(X[0],(X[0]) * myLine1.Item2 + myLine1.Item1);

            chart1.Series[NumberSeries].Points.AddXY(X[X.Count - 1], (X[X.Count - 1]) * myLine1.Item2 + myLine1.Item1);

          //  chart1.Series[NumberSeries-1].Points.AddXY(X[X.Count - 1], Y[Y.Count - 1]);   
            double t = (Math.Log(double.Parse(textBox2.Text) / C0) - myLine1.Item1) / myLine1.Item2;


            chart1.Series[NumberSeries].Points.AddXY(t, Math.Log(double.Parse(textBox2.Text) / C0));  
         //   MessageBox.Show(t.ToString());


            double MyCritika =  t;           
          //  MessageBox.Show(myLine1.Item2.ToString());
         // MessageBox.Show(MyCritika.ToString());
           chart1.Series[0].XValueType = ChartValueType.Time;
          chart1.ChartAreas[0].AxisX.LabelStyle.Format = "HH:mm:ss";
          label1.Text = " в " + DateTime.FromOADate(MyCritika);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
               // NumberSeries =0 ;
            
                for (int i = 0; i < MyAllSensors.Count; i++)
                {
                    if (comboBox1.Text == MyAllSensors[i].KKS_Name)
                    {

                        IndexAFP = i;
                        break;

                    }
                }
              //  if (textBox1.Text == null || textBox1.Text == "")
            //    {
                //    textBox1.Text = MyAllSensors[IndexAFP].MyListRecordsForOneKKS.Count.ToString();
               // }
                int MyAverage = int.Parse(textBox1.Text.Trim());

                MyBeginIndex = MyAllSensors[IndexAFP].MyListRecordsForOneKKS.Count - MyAverage;

                //вообще здесь j должен равняться MyBeginIndex
                for (int j = MyBeginIndex; j < MyAllSensors[IndexAFP].MyListRecordsForOneKKS.Count; j++)
                {
                    double MyVal = MyAllSensors[IndexAFP].MyListRecordsForOneKKS[MyBeginIndex].Value / MyAllSensors[IndexAFP].MyListRecordsForOneKKS[j].Value;
                    //  MessageBox.Show(MyAllSensors[i].MyListRecordsForOneKKS[MyBeginIndex].Value.ToString() + " " + MyAllSensors[i].MyListRecordsForOneKKS[j].Value.ToString());

                    Y.Add(MyVal);
                    X.Add(MyAllSensors[IndexAFP].MyListRecordsForOneKKS[j].DateTime.ToOADate() - MyAllSensors[IndexAFP].MyListRecordsForOneKKS[MyBeginIndex].DateTime.ToOADate());
                    chart1.Series[NumberSeries].Points.AddXY(MyAllSensors[IndexAFP].MyListRecordsForOneKKS[j].DateTime, MyVal);
                }

                //chart1.Series[0].XValueType = ChartValueType.Time;
                NumberSeries++;

                Tuple<double, double> myLine1 = Fit.Line(X.ToArray(), Y.ToArray());

                double MyCritika = 0;
                for (int j = 0; j < X.Count; j++)
                {
                    chart1.Series[NumberSeries].Points.AddXY(MyAllSensors[IndexAFP].MyListRecordsForOneKKS[MyBeginIndex].DateTime.ToOADate() + X[j], X[j] * myLine1.Item2 + myLine1.Item1);
                }

               chart1.Series[NumberSeries].Points.AddXY(MyAllSensors[IndexAFP].MyListRecordsForOneKKS[MyBeginIndex].DateTime.ToOADate() + -myLine1.Item1 / myLine1.Item2, 0);

                MyCritika = MyAllSensors[IndexAFP].MyListRecordsForOneKKS[MyBeginIndex].DateTime.ToOADate() + -myLine1.Item1 / myLine1.Item2;

                chart1.Series[0].XValueType = ChartValueType.Time;
                chart1.ChartAreas[0].AxisX.LabelStyle.Format = "HH:mm:ss";
                label1.Text = "Критическое состояние в " + DateTime.FromOADate(MyCritika);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            button3.Enabled = true;
            button5.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           // NumberSeries = 0;
            for (int i = 0; i < chart1.Series.Count; i++)
            {
                chart1.Series[i].Points.Clear();
            }
            chart1.Series[0].XValueType = ChartValueType.Double;

            X.Clear();
            Y.Clear();
            NumberSeries = 0;
            MyAllSensors.Clear();
            comboBox1.Items.Clear();

            MyAllSensors.LoadFromFile(openFileDialog1.FileName, MyAllSensors);

            for (int i = 0; i < MyAllSensors.Count; i++)
            {
                comboBox1.Items.Add(MyAllSensors[i].KKS_Name);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
         //   NumberSeries = 0;
            timer1.Enabled = true;
            timer1.Interval = 10000;
            timer1.Tick += button5_Click;
            button3.Enabled = false;
            button5.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
        //{
        //    for (int i = 0; i < chart1.Series.Count; i++)
        //    {
        //        chart1.Series[i].Points.Clear();
        //    }
        //    chart1.Series[0].XValueType = ChartValueType.Double;

        //    X.Clear();
        //    Y.Clear();
        //    NumberSeries = 0;
                      
        //    int r;
        //    if (textBox1.Text == "")
        //    {
        //        textBox1.Text = "0";
        //        r = int.Parse(textBox1.Text.ToString().Trim());
        //    }
        //    else
        //    {
        //        r = int.Parse(textBox1.Text.ToString().Trim());
        //        MessageBox.Show(r.ToString());
        //    }

        //    Restrucrure_Grafic_On_Main_Axis();
        }

        private void tableLayoutPanel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {

        }

        private void button4_Click_2(object sender, EventArgs e)
        {

        }

        private void button4_Click_3(object sender, EventArgs e)
        {
            Time_Pump MyTimePump = new Time_Pump();
            MyTimePump.Owner = this;
            MyTimePump.Show();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            krit_prognoz_concentration();
            button3.Enabled = true;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            textBox2.Text = textBox1.Text;
        }



    }
    public class Record
    {
        public DateTime DateTime;
        public double Value;

        public double value1;
        //   public double ValueTimeForDAT;
        public Record()
        {

        }
        public Record(DateTime r, double t)
        {
            this.DateTime = r;
            this.Value = t;
        }
    }

   public class Sencors : IComparable
    {
        int L;
        public string KKS_Name { get; set; }
        public List<Record> MyListRecordsForOneKKS;
        public Sencors()
        {
            this.MyListRecordsForOneKKS = new List<Record>();
            L = this.MyListRecordsForOneKKS.Count;

        }

        public Sencors(string sss)
        {
            Record OneRecord = new Record();
            OneRecord.DateTime = Convert.ToDateTime(sss.Split('\t')[0].Replace('.', '/').Replace(',', '.').Trim());//line.Split('\t')[0]//для вывода с милисекундами
            OneRecord.Value = double.Parse(sss.Split('\t')[2].Replace('.', ',').Replace('-', '0').Trim());//line.Split('\t')[2]//и прочерками, но в стрингах

            this.MyListRecordsForOneKKS = new List<Record>();
            this.KKS_Name = sss.Split('\t')[1];
            this.MyListRecordsForOneKKS.Add(OneRecord);
        }

        //public Sencors(DateTime t , double y, int l)
        //{
        //    this.MyListRecordsForOneKKS[l].Value = y;

        //}
        public int CompareTo(object other)
        {
            var oth = other as Sencors;
            return this.KKS_Name.CompareTo(oth.KKS_Name);
        }
    }
}
