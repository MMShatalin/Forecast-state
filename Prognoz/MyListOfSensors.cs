using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Prognoz
{
    public class MyListOfSensors : List<Sencors>
    {
        public Sencors getSensorByKKSName(string kks)
        {
            foreach (Sencors item in this)
            {
                if (item.KKS_Name == kks)
                {
                    return item;
                }
            }
            return null;
        }
        public Sencors getOneKKSByIndex(int index)
        {
            return this[index];
        }

        private int DetectType(string filename)
        {
            string[] temp = filename.Split('.');
            switch (temp[1])
            {
                case "txt":
                    return 1;
                    break;
                case "rsa":
                    return 2;
                    break;
                case "dat":
                    return 3;
                    break;
                case "xxx":
                    return 4;
                    break;
                default:
                    return -1;
                    break;
            }
            return -1;
        }

        public void LoadFromFile(string filename, MyListOfSensors y)
        {
            if (DetectType(filename) != -1)
            {
                switch (DetectType(filename))
                {
                    case 1:
                        this.LoadAPIK(filename, y);
                        break;
                    default:
                        break;
                }
            }
        }

        private void LoadAPIK(string filename, MyListOfSensors p)
        {
            string line = "";
            StreamReader mysr = new StreamReader(filename, Encoding.GetEncoding("Windows-1251"));

            List<string> strarray = new List<string>();
            List<string> strarray1 = new List<string>();
            line = mysr.ReadLine();

            ///Пополняем лист OneKKS ,тоесть наш класс, членами. Добавим ккс.
           // strarray.Add("Время 1");
           // strarray.Add("Время 2");

          //  MessageBox.Show(strarray[0]);
            strarray1 = line.Split('\t').ToList();
            strarray1.RemoveAt(0);
         //   strarray1.RemoveAt(2);
            strarray.Add("Время реальное");
            strarray.Add("Время СКУД");
            strarray.AddRange(strarray1);
            strarray.RemoveAt(2);
//strarray.RemoveAt(2);
         //  strarray.RemoveAt(3);
          //strarray.RemoveAt(2);

            int i2 = 0;
            foreach (string item in strarray)
            {
               // i2++;
                if (i2 >= 0)
                {
                    Sencors myonekks = new Sencors();
                    myonekks.KKS_Name = item;
                    this.Add(myonekks);
                }
                i2++;

            }
        //   MessageBox.Show(this[0].KKS_Name);
       //    MessageBox.Show(this[1].KKS_Name);
        //   MessageBox.Show(this[2].KKS_Name);
         //  MessageBox.Show(this[3].KKS_Name);
        //   MessageBox.Show(this[4].KKS_Name);

            int N = strarray.Count() - 1;
            double[] mytempdouble = new double[strarray.Count];
            while (line != null)
            {
                line = mysr.ReadLine();
                if (line != null)
                {
                    mytempdouble = line.Replace('.', ',').Split('\t').Select(n => double.Parse(n)).ToArray();
                    //MessageBox.Show(mytempdouble[mytempdouble.Count()-1].ToString());

                    for (int i = 0; i < mytempdouble.Length; i++)
                    {
                        Record OneRec = new Record();

                        OneRec.DateTime = DateTime.FromOADate(mytempdouble[0]);
                        OneRec.Value = mytempdouble[i];

                        this[this.Count - N + i - 1].MyListRecordsForOneKKS.Add(OneRec);
                    }
                }
            }
            //Закрытие потока
            mysr.Close();
            //  p.AddRange(this);
        }
        public List<double> getХvaluesByIndexStartEnd(int len)
        {
            List<double> temp = new List<double>();

            for (int i = 0; i < len; i++)
            {
                temp.Add((double)i);
            }

            return temp;

        }
        public List<double> getYvaluesByIndexStartEnd(int index, int start, int end)
        {
            List<double> temp = new List<double>();

            for (int i = start; i < end; i++)
            {
                temp.Add(this[index].MyListRecordsForOneKKS[i].Value);
            }

            return temp;

        }
    }
}
