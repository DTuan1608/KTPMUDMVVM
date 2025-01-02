using KTPMUDMVVM.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows;
using KTPMUDMVVM.Views;

namespace KTPMUDMVVM.ViewModel
{
    public class XLCTViewModel : BaseViewModel
    {
        private ObservableCollection<SanPhamXuLyChatThai> _XLCTlist;
        public ObservableCollection<SanPhamXuLyChatThai> XLCTlist
        {
            get => _XLCTlist;
            set
            {
                _XLCTlist = value;
                OnPropertyChangedEventHandler();
            }
        }

        private string _MaSP;
        public string MaSP
        {
            get => _MaSP;
            set
            {
                _MaSP = value;
                OnPropertyChangedEventHandler();
            }
        }

        private string _TenSP;
        public string TenSP
        {
            get => _TenSP;
            set
            {
                _TenSP = value;
                OnPropertyChangedEventHandler();
            }
        }

        private Nullable<System.DateTime> _HanSD;
        public Nullable<System.DateTime> HanSD
        {
            get => _HanSD;
            set
            {
                _HanSD = value;
                OnPropertyChangedEventHandler();
            }
        }

        private string _LoaiSP;
        public string LoaiSP
        {
            get => _LoaiSP;
            set
            {
                _LoaiSP = value;
                OnPropertyChangedEventHandler();
            }
        }

        private SanPhamXuLyChatThai _SelectedItem;
        public SanPhamXuLyChatThai SelectedItem
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
                        MaSP = SelectedItem.MaSP;
                        TenSP = SelectedItem.TenSP;
                        LoaiSP = SelectedItem.LoaiSP;
                        HanSD = SelectedItem.HanSD;
                    }
                }
            }
        }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public XLCTViewModel()
        {
            LoadData();

            AddCommand = new RelayCommand<object>(
                (p) =>
                {
                    if (DataProvide.Ins.DB.SanPhamXuLyChatThais.Any(x => x.MaSP == MaSP) == true)
                    {
                        return false;
                    }
                    return !string.IsNullOrEmpty(MaSP) &&
                           DataProvide.Ins.DB.SanPhamXuLyChatThais.Any(x => x.MaSP == LoaiSP);
                },
                (p) =>
                {
                    if (HanSD == null || MaSP == null || TenSP == null || LoaiSP == null)
                    {
                        MessageBox.Show("Chua dien du thong tin can thiet!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    

                    var unit = new SanPhamXuLyChatThai
                    {
                        MaSP = MaSP,
                        TenSP = TenSP,
                        LoaiSP = LoaiSP,
                        HanSD = HanSD
                    };

                    DataProvide.Ins.DB.SanPhamXuLyChatThais.Add(unit);
                    DataProvide.Ins.DB.SaveChanges();
                    XLCTlist.Add(unit);
                    ClearInputFields();
                });

            EditCommand = new RelayCommand<object>(
     (p) =>
     {

         return SelectedItem != null &&
                !string.IsNullOrEmpty(MaSP) &&
                DataProvide.Ins.DB.SanPhamXuLyChatThais.Any(x => x.MaSP == MaSP);
     },
     (p) =>
     {
         var unit = DataProvide.Ins.DB.SanPhamXuLyChatThais.SingleOrDefault(x => x.MaSP == MaSP);
         if (unit != null)
         {

             unit.MaSP = MaSP;
             unit.TenSP = TenSP;
             unit.LoaiSP = LoaiSP;
             unit.HanSD = HanSD;


             DataProvide.Ins.DB.SaveChanges();


             SelectedItem.MaSP = MaSP;
             SelectedItem.TenSP = TenSP;
             SelectedItem.LoaiSP = LoaiSP;
             SelectedItem.HanSD = HanSD;


             OnPropertyChangedEventHandler(nameof(XLCTlist));
             MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
         }
         else
         {
             MessageBox.Show("Không tìm thấy cơ sở chan nuoi này!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
         }
     });


            SearchCommand = new RelayCommand<object>(
                (p) =>
                {
                    //if (DataProvide.Ins.DB.CoSoChanNuois.SingleOrDefault(x => x.MaCN == MaCN || x.TenCN == TenCN || x.MaXa == MaXa || x.SoDT == SoDT) == null || MaCN != null)
                    //{

                    //    return false;
                    //}
                    return true;
                },
                (p) =>
                {
                    if (MaSP == null || LoaiSP == null || TenSP == null || HanSD == null)
                    {
                        LoadData();
                    }
                    var results = DataProvide.Ins.DB.SanPhamXuLyChatThais.Where(x =>
                   (string.IsNullOrEmpty(MaSP) || x.MaSP.Contains(MaSP)) &&
                   (string.IsNullOrEmpty(TenSP) || x.TenSP.Contains(TenSP)) &&
                   (string.IsNullOrEmpty(LoaiSP) || x.LoaiSP.Contains(LoaiSP)))
                    .ToList();
                    if (results != null)
                    {


                        XLCTlist.Clear();
                        foreach (var item in results)
                        {
                            XLCTlist.Add(item);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy San pham thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadData();
                    }
                });

            DeleteCommand = new RelayCommand<object>(
                (p) => {
                    if (SelectedItem == null)
                    {
                        return false;
                    }
                    return true;
                },
                (p) =>
                {
                    var unit = DataProvide.Ins.DB.SanPhamXuLyChatThais.SingleOrDefault(x => x.MaSP == MaSP);
                    if (unit != null)
                    {
                        DataProvide.Ins.DB.SanPhamXuLyChatThais.Remove(unit);
                        DataProvide.Ins.DB.SaveChanges();
                        XLCTlist.Remove(SelectedItem);
                        ClearInputFields();
                        MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                });
        }

        private void LoadData()
        {
            XLCTlist = new ObservableCollection<SanPhamXuLyChatThai>(
                DataProvide.Ins.DB.SanPhamXuLyChatThais);
            OnPropertyChangedEventHandler();
        }

        private void ClearInputFields()
        {
            MaSP = null;
            TenSP = null;
            LoaiSP = null;
            HanSD = null;
            SelectedItem = null;
        }
    }
}