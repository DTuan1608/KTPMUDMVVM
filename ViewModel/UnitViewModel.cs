using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KTPMUDMVVM.Model;

namespace KTPMUDMVVM.ViewModel
{
    public class UnitViewModel
    {
        private ObservableCollection<CSCNmodel> _List;
        public ObservableCollection<CSCNmodel> List { get { return _List; } set { _List = value; } }
        public UnitViewModel() 
        { 
            List = new ObservableCollection<CSCNmodel>((IEnumerable<CSCNmodel>)DataProvide.Ins.DB.CoSoChanNuois);

        }
    }
}
