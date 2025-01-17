using KTPMUDMVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace KTPMUDMVVM.ViewModel
{
    public class TCVViewModel : BaseViewModel
    {
        private  ToChucCaNhan _SelectedItem {  get; set; }

        public ToChucCaNhan SelectedItem {
            get => _SelectedItem;
            set 
            {
                _SelectedItem = value;
            } 
        }

        private string _LoaiTCCN { get; set; }
        public string LoaiTCCN
        {
            get { return _LoaiTCCN; }
            set { _LoaiTCCN = value; OnPropertyChangedEventHandler(); }
        }

        private ObservableCollection<string> _LoaiTCCNlist { get; set; }      

        public ObservableCollection<string> LoaiTCCNlist
        {
            get => _LoaiTCCNlist;
            set
            {
                _LoaiTCCNlist = value;
                OnPropertyChangedEventHandler();
            }
        }
        private ObservableCollection<ToChucCaNhan> _TCCNlist { get; set;}
        public ObservableCollection<ToChucCaNhan> TCCNlist
        {
            get => _TCCNlist;
            set { _TCCNlist = value; OnPropertyChangedEventHandler(nameof(TCCNlist)); }
        } 
        public ICommand SearchCommand { get; set; }
        public ICommand DetailCommand { get; set; }


        public TCVViewModel()
        {
            LoadData();
            SearchCommand = new RelayCommand<object>
            (
                (p) =>
                {
                    return true;
                },
            (p) =>
            {
                if (LoaiTCCN != null) 
                { 
                    TCCNlist.Clear();
                    
                    var Data = DataProvide.Ins.DB.ToChucCaNhans.ToList().Where(x => x.LoaiTCCN == LoaiTCCN);
                    
                    OnPropertyChangedEventHandler(nameof(Data));
                    
                    TCCNlist = new ObservableCollection<ToChucCaNhan>(Data);
                }
            }
            );
            DetailCommand = new RelayCommand<object>
                (
                (p) =>
                {
                    return true;
                },
                (p)=>
                {

                }
            );
        }
        public void LoadData()
        {
            var data = DataProvide.Ins.DB.ToChucCaNhans.ToList();
            
            TCCNlist = new ObservableCollection<ToChucCaNhan>(data);

            var type = DataProvide.Ins.DB.ToChucCaNhans.Select(item => item.LoaiTCCN).Distinct().ToList();
            if (type.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu trong LoaiTCCNlist");
            }
            LoaiTCCNlist = new ObservableCollection<string>(type);

        }
    }
}
