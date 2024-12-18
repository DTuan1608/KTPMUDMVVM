using KTPMUDMVVM.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace KTPMUDMVVM.ViewModel
{
    public class CSCBViewmodel : BaseViewModel
    {
        private ObservableCollection<CoSoCheBien> _CSCBlist;
        public ObservableCollection<CoSoCheBien> CSCBlist
        {
            get => _CSCBlist;
            set
            {
                _CSCBlist = value;
                OnPropertyChangedEventHandler();
            }
        }

        private string _MaCB;
        public string MaCB
        {
            get => _MaCB;
            set
            {
                _MaCB = value;
                OnPropertyChangedEventHandler();
            }
        }
        private string _TenCB;
        public string TenCB
        {
            get => _TenCB;
            set
            {
                _TenCB = value;
                OnPropertyChangedEventHandler();
            }
        }
        private string _MaXa;
        public string MaXa
        {
            get => _MaXa;
            set
            {
                _MaXa = value;
                OnPropertyChangedEventHandler();
            }
        }
        private string _SoDT;
        public string SoDT
        {
            get => _SoDT;
            set
            {
                _SoDT = value;
                OnPropertyChangedEventHandler();
            }
        }

        private CoSoCheBien _SelectedItem;
        public CoSoCheBien SelectedItem
        {
            get => _SelectedItem;
            set
            {
                if (_SelectedItem != value)
                {
                    _SelectedItem = value;
                    OnPropertyChangedEventHandler();
                    if (SelectedItem != null)
                    {
                        MaCB = SelectedItem.MaCB;
                        SoDT = SelectedItem.SoDT;
                        MaXa = SelectedItem.MaXa;
                        TenCB = SelectedItem.TenCB;
                    }
                }
            }
        }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand SearchCommand { get; set; }

        public CSCBViewmodel()
        {
            LoadData();

            AddCommand = new RelayCommand<object>(
                (p) =>
                {
                    if (string.IsNullOrEmpty(MaCB))
                        return false;

                    // Kiểm tra trùng lặp mã CB
                    var unitlist = DataProvide.Ins.DB.CoSoCheBiens.Where(x => x.MaCB == MaCB);
                    return !unitlist.Any();
                },
                (p) =>
                {
                    var unit = new CoSoCheBien
                    {
                        MaCB = MaCB
                    };

                    // Thêm vào cơ sở dữ liệu
                    DataProvide.Ins.DB.CoSoCheBiens.Add(unit);
                    DataProvide.Ins.DB.SaveChanges();

                    // Cập nhật danh sách hiển thị
                    CSCBlist.Add(unit);
                });

            EditCommand = new RelayCommand<object>(
                (p) =>
                {
                    if (SelectedItem == null || string.IsNullOrEmpty(MaCB))
                        return false;

                    var unitlist = DataProvide.Ins.DB.CoSoCheBiens.Where(x => x.MaCB == SelectedItem.MaCB);
                    return unitlist.Any();
                },
                (p) =>
                {
                    // Sửa dữ liệu trong cơ sở dữ liệu
                    var unit1 = DataProvide.Ins.DB.CoSoCheBiens.SingleOrDefault(x => x.MaCB == SelectedItem.MaCB);
                    if (unit1 != null)
                    {
                        unit1.MaCB = MaCB;
                        DataProvide.Ins.DB.SaveChanges();

                        // Cập nhật danh sách hiển thị
                        SelectedItem.MaCB = MaCB;
                        OnPropertyChangedEventHandler();
                    }
                });

            SearchCommand = new RelayCommand<object>(
     (p) =>
     {
         if( MaCB == null)
         {
             return true;
         }
         return DataProvide.Ins.DB.CoSoCheBiens.Any(x => x.MaCB == MaCB);
     },
     (p) =>
     {

         var unit2 = DataProvide.Ins.DB.CoSoCheBiens.SingleOrDefault(x => x.MaCB == MaCB);
         if (unit2 != null)
         {
             if (SelectedItem == null)
                 SelectedItem = new CoSoCheBien();

             SelectedItem.MaCB = unit2.MaCB;
             SelectedItem.TenCB = unit2.TenCB;
             SelectedItem.MaXa = unit2.MaXa;
             SelectedItem.SoDT = unit2.SoDT;

             OnPropertyChangedEventHandler(nameof(SelectedItem));

             // Không thay danh sách mà chỉ cập nhật CSCBlist
             CSCBlist.Clear();
             CSCBlist.Add(SelectedItem);
         }
     });



        }

        private void LoadData()
        {
            CSCBlist = new ObservableCollection<CoSoCheBien>
            {
                new CoSoCheBien
                {
                    MaCB = "a",
                    TenCB = "Trang Trại A",
                    MaXa = "Hà Nội",
                    SoDT = "0123456789"
                },
                new CoSoCheBien
                {
                    MaCB = "b",
                    TenCB = "Trang Trại B",
                    MaXa = "Hải Phòng",
                    SoDT = "0987654321"
                },
                new CoSoCheBien
                {
                    MaCB = "c",
                    TenCB = "Trang Trại C",
                    MaXa = "Đà Nẵng",
                    SoDT = "0112233445"
                }
            };
        }
    }
}
