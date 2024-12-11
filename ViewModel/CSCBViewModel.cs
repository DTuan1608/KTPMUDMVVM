using KTPMUDMVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTPMUDMVVM.ViewModel
{
    public class CSCBViewmodel : BaseViewModel
    {
        private ObservableCollection<CSCBmodel> _CSCBlist;

        public ObservableCollection<CSCBmodel> CSCBlist
        {
            get => _CSCBlist;
            set
            {
                _CSCBlist = value;

            }
        }


        public CSCBViewmodel()
        {
            LoadData();

        }

        private void LoadData()
        {
            CSCBlist = new ObservableCollection<CSCBmodel>
            {
                new CSCBmodel
                {
                    CoSoCheBien = new CoSoCheBien
                    {
                        MaCB = "1",
                        TenCB = "Trang Trại A",
                        MaXa = "Hà Nội",
                        SoDT = "0123456789"
                    }
                },
                new CSCBmodel
                {
                    CoSoCheBien = new CoSoCheBien
                    {
                        MaCB = "1",
                        TenCB = "Trang Trại A",
                        MaXa = "Hà Nội",
                        SoDT = "0123456789"
                    }
                },
                new CSCBmodel
                {
                    CoSoCheBien = new CoSoCheBien
                    {
                        MaCB = "1",
                        TenCB = "Trang Trại A",
                        MaXa = "Hà Nội",
                        SoDT = "0123456789"
                    }
                },
            };
        }
    }
}
