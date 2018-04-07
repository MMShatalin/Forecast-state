using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prognoz
{
    class MyVirtualClass
    {
        public string value;
        public string time;

        public MyVirtualClass(string myTime, string myValue)
        {
            this.value = myValue;
            this.time = myTime;
        }
    }
}
