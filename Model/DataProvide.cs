using KTPMUDMVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTPMUDMVVM.Model
{
    public class DataProvide
    {
        private static DataProvide _ins;
        public static DataProvide Ins { get { if (_ins == null) _ins = new DataProvide(); return _ins; }
            set { _ins = value; } }
        public DataEntities DB { get; set; }
         
        private DataProvide()
        {
            DB = new DataEntities();
        }
    }
}
