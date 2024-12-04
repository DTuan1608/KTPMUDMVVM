using KTPMUDMVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTPMUDMVVM.ViewModel
{
    public class HomeVM : BaseViewModel
    {
        public readonly PageMode _pageMode;

        public HomeVM()
        {
            _pageMode = new PageMode();
        }
    }
}
