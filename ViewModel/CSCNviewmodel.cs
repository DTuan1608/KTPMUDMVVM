using KTPMUDMVVM.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows;

namespace KTPMUDMVVM.ViewModel
{
    public class CSCNviewmodel : BaseViewModel
    {
        private ObservableCollection<CoSoChanNuoi> _CSCNlist;
        public ObservableCollection<CoSoChanNuoi> CSCNlist
        {
            get => _CSCNlist;
            set
            {
                _CSCNlist = value;
                OnPropertyChangedEventHandler();
            }
        }

        private string _MaCN;
        public string MaCN
        {
            get => _MaCN;
            set
            {
                _MaCN = value;
                OnPropertyChangedEventHandler();
            }
        }

        private string _Ten;
        public string Ten
        {
            get => _Ten;
            set
            {
                _Ten = value;
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

        private string _Dongvat;
        public string Dongvat
        {
            get=> _Dongvat;
            set
            {
                _Dongvat = value;
            }
        }

        private CoSoChanNuoi _SelectedItem;
        public CoSoChanNuoi SelectedItem
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
                        
                        MaCN = SelectedItem.MaCN;
                        SoDT = SelectedItem.SoDT;
                        MaXa = SelectedItem.MaXa;
                        Ten = SelectedItem.Ten;
                    }
                }
            }
        }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public CSCNviewmodel()
        {
            LoadData();

            AddCommand = new RelayCommand<object>(
                (p) =>
                {
                    if (DataProvide.Ins.DB.CoSoChanNuois.Any(x => x.MaCN == MaCN) == true)
                    {
                        return false;
                    }
                    return !string.IsNullOrEmpty(MaCN) &&
                           DataProvide.Ins.DB.CoSoChanNuois.Any(x => x.MaCN == MaCN);
                },
                (p) =>
                {
                    if (MaXa == null || MaCN == null || Ten == null || SoDT == null)
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

                    var unit = new CoSoChanNuoi
                    {
                        MaCN = MaCN,
                        Ten = Ten,
                        MaXa = MaXa,
                        SoDT = SoDT
                    };

                    DataProvide.Ins.DB.CoSoChanNuois.Add(unit);
                    DataProvide.Ins.DB.SaveChanges();
                    CSCNlist.Add(unit);
                    ClearInputFields();
                    OnPropertyChangedEventHandler();
                });

            EditCommand = new RelayCommand<object>(
     (p) =>
     {

         return SelectedItem != null &&
                !string.IsNullOrEmpty(MaCN) &&
                DataProvide.Ins.DB.CoSoChanNuois.Any(x => x.MaCN == MaCN);
     },
     (p) =>
     {

         var existingMaXa = DataProvide.Ins.DB.CoSoChanNuois.Any(x => x.MaXa == MaXa);
         if (!existingMaXa)
         {
             MessageBox.Show("Mã xã không tồn tại trong hệ thống!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
             MaXa = null;
             return;
         }

         var unit = DataProvide.Ins.DB.CoSoChanNuois.SingleOrDefault(x => x.MaCN == MaCN);
         if (unit != null)
         {

             unit.MaCN = MaCN;
             unit.Ten = Ten;
             unit.MaXa = MaXa;
             unit.SoDT = SoDT;


             DataProvide.Ins.DB.SaveChanges();


             SelectedItem.MaCN = MaCN;
             SelectedItem.Ten = Ten;
             SelectedItem.MaXa = MaXa;
             SelectedItem.SoDT = SoDT;


             OnPropertyChangedEventHandler(nameof(CSCNlist));
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
                    //if (DataProvide.Ins.DB.CoSoChanNuois.SingleOrDefault(x => x.MaCN == MaCN || x.Ten == Ten || x.MaXa == MaXa || x.SoDT == SoDT) == null || MaCN != null)
                    //{

                    //    return false;
                    //}
                    return true;
                },
                (p) =>
                {
                    if (MaCN == null || MaXa == null || Ten == null || SoDT == null)
                    {
                        LoadData();
                    }
                    var results = DataProvide.Ins.DB.CoSoChanNuois.Where(x =>
                   (string.IsNullOrEmpty(MaCN) || x.MaCN.Contains(MaCN)) &&
                   (string.IsNullOrEmpty(Ten) || x.Ten.Contains(Ten)) &&
                   (string.IsNullOrEmpty(MaXa) || x.MaXa.Contains(MaXa)) &&
                   (string.IsNullOrEmpty(SoDT) || x.SoDT.Contains(SoDT)))
                    .ToList();
                    if (results != null)
                    {


                        CSCNlist.Clear();
                        foreach (var item in results)
                        {
                            CSCNlist.Add(item);
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
                    var unit = DataProvide.Ins.DB.CoSoChanNuois.SingleOrDefault(x => x.MaCN == MaCN);
                    if (unit != null)
                    {
                        DataProvide.Ins.DB.CoSoChanNuois.Remove(unit);
                        DataProvide.Ins.DB.SaveChanges();
                        CSCNlist.Remove(SelectedItem);
                        ClearInputFields();
                        MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                });
        }

        private void LoadData()
        {
            CSCNlist = new ObservableCollection<CoSoChanNuoi>(
                DataProvide.Ins.DB.CoSoChanNuois);
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
            MaCN = null;
            Ten = null;
            MaXa = null;
            SoDT = null;
            SelectedItem = null;
        }
    }
}