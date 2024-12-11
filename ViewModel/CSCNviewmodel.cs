using KTPMUDMVVM.Model;
using System;
using System.Collections.ObjectModel;

namespace KTPMUDMVVM.ViewModel
{
    public class CSCNViewmodel : BaseViewModel
    {
        private ObservableCollection<CSCNmodel> _CSCNlist;

        public ObservableCollection<CSCNmodel> CSCNlist
        {
            get => _CSCNlist;
            set
            {
                _CSCNlist = value;
            }
        }


        public CSCNViewmodel()
        {
            LoadData();
        }

        private void LoadData()
        {
            CSCNlist = new ObservableCollection<CSCNmodel>
            {
                new CSCNmodel
                {
                    CoSoChanNuoi1 = new CoSoChanNuoi
                    {
                        MaCN = "1",
                        TenCN = "Trang Trại A",
                        MaXa = "Hà Nội",
                        SoDT = "0123456789"
                    }
                },
                new CSCNmodel
                {
                    CoSoChanNuoi1 = new CoSoChanNuoi
                    {
                        MaCN = "2",
                        TenCN = "Trang Trại B",
                        MaXa = "Hồ Chí Minh",
                        SoDT = "0987654321"
                    }
                },
                new CSCNmodel
                {
                    CoSoChanNuoi1 = new CoSoChanNuoi
                    {
                        MaCN = "3",
                        TenCN = "Trang Trại C",
                        MaXa = "Đà Nẵng",
                        SoDT = "0765432198"
                    }
                }
            };
        }
    }
}