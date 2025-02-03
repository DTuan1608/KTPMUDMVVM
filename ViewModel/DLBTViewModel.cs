using KTPMUDMVVM.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Windows;
using System.Windows.Input;

namespace KTPMUDMVVM.ViewModel
{
    public class DLBTViewModel : BaseViewModel
    {
        private ObservableCollection<DaiLyBanThuoc> _DLBTlist;
        public ObservableCollection<DaiLyBanThuoc> DLBTlist
        {
            get => _DLBTlist;
            set
            {
                _DLBTlist = value;
                OnPropertyChangedEventHandler();
            }
        }

        private string _MaDL;
        public string MaDL
        {
            get => _MaDL;
            set
            {
                _MaDL = value;
                OnPropertyChangedEventHandler();
            }
        }

        private string _TenDL;
        public string TenDL
        {
            get => _TenDL;
            set
            {
                _TenDL = value;
                OnPropertyChangedEventHandler();
            }
        }

        public string DiaChi
        {
            get
            {
                if (string.IsNullOrEmpty(MaXa))
                    return "Không có địa chỉ";
                
                Xa xa = DataProvide.Ins.DB.Xas.SingleOrDefault(x => x.MaXa == MaXa);
                return xa.TenXa;
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

        private DaiLyBanThuoc _SelectedItem;
        public DaiLyBanThuoc SelectedItem
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
                        MaDL = SelectedItem.MaDL;
                        SoDT = SelectedItem.SoDT;
                        MaXa = SelectedItem.MaXa;
                        TenDL = SelectedItem.TenDL;
                        
                    }
                }
            }
        }

        private string _MaXa;
        public string MaXa
        {
            get => _MaXa;
            set
            {

                    _MaXa = value;
            }
        }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public ICommand DetailCommand { get; set; }

        public DLBTViewModel()
        {
            LoadData();

            AddCommand = new RelayCommand<object>(
                    (p) =>
                    {
                        //if (DataProvide.Ins.DB.DaiLyBanThuocs.Any(x => x.MaDL == MaDL) == true)
                        //{
                        //    return false;
                        //}
                        return !string.IsNullOrEmpty(MaDL) && DataProvide.Ins.DB.DaiLyBanThuocs.Any(x => x.MaDL == SelectedItem.MaDL);
                    },
                    (p) =>
                    {
                        if (MaXa == null || MaDL == null || TenDL == null || SoDT == null)
                        {
                            MessageBox.Show("Chua dien du thong tin can thiet!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        var existingMaXa = DataProvide.Ins.DB.DaiLyBanThuocs.Any(x => x.MaXa == MaXa);
                        if (!existingMaXa)
                        {
                            MessageBox.Show("Mã xã không tồn tại trong hệ thống!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                            MaXa = null;
                            return;
                        }

                        var unit = new DaiLyBanThuoc
                        {
                            MaDL = MaDL,
                            TenDL = TenDL,
                            MaXa = MaXa,
                            SoDT = SoDT
                        };

                        DataProvide.Ins.DB.DaiLyBanThuocs.Add(unit);
                        DataProvide.Ins.DB.SaveChanges();
                        DLBTlist.Add(unit);
                        ClearInputFields();
                    });

            EditCommand = new RelayCommand<object>(
     (p) =>
     {

         return SelectedItem != null &&
                !string.IsNullOrEmpty(MaDL) &&
                DataProvide.Ins.DB.DaiLyBanThuocs.Any(x => x.MaDL == MaDL);
     },
     (p) =>
     {

         var existingMaXa = DataProvide.Ins.DB.DaiLyBanThuocs.Any(x => x.MaXa == MaXa);
         if (!existingMaXa)
         {
             MessageBox.Show("Mã xã không tồn tại trong hệ thống!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
             MaXa = null;
             return;
         }

         var unit = DataProvide.Ins.DB.DaiLyBanThuocs.SingleOrDefault(x => x.MaDL == MaDL);
         if (unit != null)
         {

             unit.MaDL = MaDL;
             unit.TenDL = TenDL;
             unit.MaXa = MaXa;
             unit.SoDT = SoDT;


             DataProvide.Ins.DB.SaveChanges();

             SelectedItem.MaDL = MaDL;
             SelectedItem.TenDL = TenDL;
             SelectedItem.MaXa = MaXa;
             SelectedItem.SoDT = SoDT;


             OnPropertyChangedEventHandler(nameof(DLBTlist));
             MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
         }
         else
         {
             MessageBox.Show("Không tìm thấy cơ sở chế biến này!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
         }
     });


            SearchCommand = new RelayCommand<object>(
                (p) =>
                {
                    return true;
                },
                (p) =>
                {
                    if (MaDL == null || MaXa == null || TenDL == null || SoDT == null)
                    {
                        LoadData();
                    }
                    var results = DataProvide.Ins.DB.DaiLyBanThuocs.Where(x =>
                   (string.IsNullOrEmpty(MaDL) || x.MaDL.Contains(MaDL)) &&
                   (string.IsNullOrEmpty(TenDL) || x.TenDL.Contains(TenDL)) &&
                   (string.IsNullOrEmpty(MaXa) || x.MaXa.Contains(MaXa)) &&
                   (string.IsNullOrEmpty(SoDT) || x.SoDT.Contains(SoDT)))
                    .ToList();
                    if (results != null)
                    {


                        DLBTlist.Clear();
                        foreach (var item in results)
                        {
                            DLBTlist.Add(item);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy Cơ sở thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
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
                    var unit = DataProvide.Ins.DB.DaiLyBanThuocs.SingleOrDefault(x => x.MaDL == MaDL);
                    if (unit != null)
                    {
                        DataProvide.Ins.DB.DaiLyBanThuocs.Remove(unit);
                        DataProvide.Ins.DB.SaveChanges();
                        DLBTlist.Remove(SelectedItem);
                        ClearInputFields();
                        MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                });
        }

        private void LoadData()
        {
            DLBTlist = new ObservableCollection<DaiLyBanThuoc>(
                DataProvide.Ins.DB.DaiLyBanThuocs);
        }

        private string FindXaByMaXa(string maXa)
        {
            try
            {
                // Kiểm tra nếu maXa không rỗng
                if (string.IsNullOrEmpty(maXa))
                {
                    MessageBox.Show("Mã xã không hợp lệ.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }

                // Tìm xã trong cơ sở dữ liệu
                Xa xa = DataProvide.Ins.DB.Xas.SingleOrDefault(x => x.MaXa == maXa);

                if (xa == null)
                {
                    MessageBox.Show($"Không tìm thấy xã với Mã: {maXa}", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                return xa.TenXa;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }
        private void ClearInputFields()
        {
            MaDL = null;
            TenDL = null;
            MaXa = null;
            SoDT = null;
            SelectedItem = null;
        }
    }
}

