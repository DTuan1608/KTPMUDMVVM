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

        private CSCNmodel _SelectedItem1;
        public CSCNmodel SelectedItem1
        {
            get => _SelectedItem1;
            set
            {
                if (_SelectedItem1 != value)
                {
                    _SelectedItem1 = value;
                    OnPropertyChangedEventHandler();
                }
            }
        }
        public CSCNViewmodel()
        {
            LoadData();
            OnPropertyChangedEventHandler();
        }

        private void LoadData()
        {
            CSCNlist = new ObservableCollection<CSCNmodel>
            {
                new CSCNmodel
                {
                    CoSoChanNuoi = new CoSoChanNuoi
                    {
                        MaCN = "1",
                        TenCN = "Trang Trại A",
                        MaXa = "Hà Nội",
                        SoDT = "0123456789"
                    }
                },
                new CSCNmodel
                {
                    CoSoChanNuoi = new CoSoChanNuoi
                    {
                        MaCN = "2",
                        TenCN = "Trang Trại B",
                        MaXa = "Hồ Chí Minh",
                        SoDT = "0987654321"
                    }
                },
                new CSCNmodel
                {
                    CoSoChanNuoi = new CoSoChanNuoi
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