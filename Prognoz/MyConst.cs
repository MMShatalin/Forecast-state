using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prognoz
{
    static class Condition
    {
        public static double R12(double H12)
        {
            return -1.40488151 * Math.Pow(10, -6) * Math.Pow(H12, 3) + 1.89773004 * Math.Pow(10, -4) * Math.Pow(H12, 2) - 1.88012165 * Math.Pow(10, -3) * (H12);
        }

       
        public static double Search_Change_Reactivity12(double H12_start, double H12_finish)
        {
            return R12(H12_finish) - R12(H12_start);
        }
      
        public static double d_R12_d_C(double H12)
        {
            return 8.73773883 * Math.Pow(10, -13) * Math.Pow(H12, 6) - 2.53157917 * Math.Pow(10, -10) * Math.Pow(H12, 5) + 2.66688987 * Math.Pow(10, -8) * Math.Pow(H12, 4) - 1.16137613 * Math.Pow(10, -6) * Math.Pow(H12, 3) + 1.44018002 * Math.Pow(10, -5) * Math.Pow(H12, 2) - 4.19764137 * Math.Pow(10, -5) * (H12) - 1.85604031;
        }
 
        public static double Aver_d_R12_d_C(double H12_finish)
        {
            return d_R12_d_C(H12_finish);
        }
    }
}
