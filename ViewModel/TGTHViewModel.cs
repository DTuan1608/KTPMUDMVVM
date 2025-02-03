using KTPMUDMVVM.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows;
using KTPMUDMVVM.Views;

namespace KTPMUDMVVM.ViewModel
{
    public class TGTHViewModel : BaseViewModel
    {
        private ObservableCollection<KhuTamGiu> _TGTHlist;
        public ObservableCollection<KhuTamGiu> TGTHlist
        {
            get => _TGTHlist;
            set
            {
                _TGTHlist = value;
                OnPropertyChangedEventHandler();
            }
        }

        private string _MaKhu;
        public string MaKhu
        {
            get => _MaKhu;
            set
            {
                _MaKhu = value;
                OnPropertyChangedEventHandler();
            }
        }

        private string _TenKhu;
        public string TenKhu
        {
            get => _TenKhu;
            set
            {
                _TenKhu = value;
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

        private string _MaDV;
        public string MaDV
        {
            get => _MaDV;
            set
            {
                _MaDV = value;
                OnPropertyChangedEventHandler();
            }
        }

        private KhuTamGiu _SelectedItem;
        public KhuTamGiu SelectedItem
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
                        MaKhu = SelectedItem.MaKhu;
                        MaDV = SelectedItem.MaDV;
                        MaXa = SelectedItem.MaXa;
                        TenKhu = SelectedItem.TenKhu;
                    }
                }
            }
        }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public TGTHViewModel()
        {
            LoadData();

            AddCommand = new RelayCommand<object>(
                (p) =>
                {
                    //if (DataProvide.Ins.DB.KhuTamGius.Any(x => x.MaKhu == MaKhu) == true)
                    //{
                    //    return false;
                    //}
                    return !string.IsNullOrEmpty(MaKhu) &&
                           DataProvide.Ins.DB.KhuTamGius.Any(x => x.MaKhu == MaKhu);
                },
                (p) =>
                {
                    if (MaXa == null || MaKhu == null || TenKhu == null || MaDV == null)
                    {
                        MessageBox.Show("Chua dien du thong tin can thiet!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    var existingMaXa = DataProvide.Ins.DB.CoSoChanNuois.Any(x => x.MaXa == MaXa);
                    if (!existingMaXa)
                    {
                        MessageBox.Show("Mã xã không tồn tại trong hệ thống!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        MaXa = null;
                        return;
                    }

                    var unit = new KhuTamGiu
                    {
                        MaKhu = MaKhu,
                        TenKhu = TenKhu,
                        MaXa = MaXa,
                        MaDV = MaDV
                    };

                    DataProvide.Ins.DB.KhuTamGius.Add(unit);
                    DataProvide.Ins.DB.SaveChanges();
                    TGTHlist.Add(unit);
                    ClearInputFields();
                });

            EditCommand = new RelayCommand<object>(
     (p) =>
     {

         return SelectedItem != null &&
                !string.IsNullOrEmpty(MaKhu) &&
                DataProvide.Ins.DB.KhuTamGius.Any(x => x.MaKhu == MaKhu);
     },
     (p) =>
     {

         var existingMaXa = DataProvide.Ins.DB.KhuTamGius.Any(x => x.MaXa == MaXa);
         if (!existingMaXa)
         {
             MessageBox.Show("Mã xã không tồn tại trong hệ thống!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
             MaXa = null;
             return;

         }
         var unit = DataProvide.Ins.DB.KhuTamGius.SingleOrDefault(x => x.MaKhu == MaKhu);
         if (unit != null)
         {

             unit.MaKhu = MaKhu;
             unit.TenKhu = TenKhu;
             unit.MaXa = MaXa;
             unit.MaDV = MaDV;
             unit.Xa = FindXaByMaXa(MaXa);

             DataProvide.Ins.DB.SaveChanges();


             SelectedItem.MaKhu = MaKhu;
             SelectedItem.TenKhu = TenKhu;
             SelectedItem.MaXa = MaXa;
             SelectedItem.MaDV = MaDV;


             OnPropertyChangedEventHandler(nameof(TGTHlist));
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
                    if (MaKhu == null || MaXa == null || TenKhu == null || MaDV == null)
                    {
                        LoadData();
                    }
                    var results = DataProvide.Ins.DB.KhuTamGius.Where(x =>
                   (string.IsNullOrEmpty(MaKhu) || x.MaKhu.Contains(MaKhu)) &&
                   (string.IsNullOrEmpty(TenKhu) || x.TenKhu.Contains(TenKhu)) &&
                   (string.IsNullOrEmpty(MaXa) || x.MaXa.Contains(MaXa)) &&
                   (string.IsNullOrEmpty(MaDV) || x.MaDV.Contains(MaDV)))
                    .ToList();
                    if (results != null)
                    {


                        TGTHlist.Clear();
                        foreach (var item in results)
                        {
                            TGTHlist.Add(item);
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
                    var unit = DataProvide.Ins.DB.KhuTamGius.SingleOrDefault(x => x.MaKhu == MaKhu);
                    if (unit != null)
                    {
                        DataProvide.Ins.DB.KhuTamGius.Remove(unit);
                        DataProvide.Ins.DB.SaveChanges();
                        TGTHlist.Remove(SelectedItem);
                        ClearInputFields();
                        MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                });
        }

        private void LoadData()
        {
            TGTHlist = new ObservableCollection<KhuTamGiu>(
                DataProvide.Ins.DB.KhuTamGius);
            OnPropertyChangedEventHandler();
        }

        private Xa FindXaByMaXa(string maXa)
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

                return xa;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        private void ClearInputFields()
        {
            MaKhu = null;
            TenKhu = null;
            MaXa = null;
            MaDV = null;
            SelectedItem = null;
        }
    }
}