using KTPMUDMVVM.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Windows;
using System.Windows.Input;

namespace KTPMUDMVVM.ViewModel
{
    public class CSKNViewModel : BaseViewModel
    {
        private ObservableCollection<CoSoKhaoNghiemSP> _CSKNlist;
        public ObservableCollection<CoSoKhaoNghiemSP> CSKNlist
        {
            get => _CSKNlist;
            set
            {
                _CSKNlist = value;
                OnPropertyChangedEventHandler();
            }
        }

        private string _MaKN;
        public string MaKN
        {
            get => _MaKN;
            set
            {
                _MaKN = value;
                OnPropertyChangedEventHandler();
            }
        }

        private string _TenKN;
        public string TenKN
        {
            get => _TenKN;
            set
            {
                _TenKN = value;
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

        

        private CoSoKhaoNghiemSP _SelectedItem;
        public CoSoKhaoNghiemSP SelectedItem
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
                        MaKN = SelectedItem.MaKN;
                        SoDT = SelectedItem.SoDT;
                        MaXa = SelectedItem.MaXa;
                        TenKN = SelectedItem.TenKN;
                        
                    }
                }
            }
        }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand DetailCommand { get; set; }

        public CSKNViewModel()
        {
            LoadData();

            AddCommand = new RelayCommand<object>(
                    (p) =>
                    {
                        if (DataProvide.Ins.DB.CoSoKhaoNghiemSPs.Any(x => x.MaKN == MaKN) == true)
                        {
                            return false;
                        }
                        return !string.IsNullOrEmpty(MaKN) && DataProvide.Ins.DB.CoSoKhaoNghiemSPs.Any(x => x.MaKN == SelectedItem.MaKN);
                    },
                    (p) =>
                    {
                        if (MaXa == null || MaKN == null || TenKN == null || SoDT == null )
                        {
                            MessageBox.Show("Chua dien du thong tin can thiet!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        var existingMaXa = DataProvide.Ins.DB.CoSoKhaoNghiemSPs.Any(x => x.MaXa == MaXa);
                        if (!existingMaXa)
                        {
                            MessageBox.Show("Mã xã không tồn tại trong hệ thống!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                            MaXa = null;
                            return;
                        }

                        var unit = new CoSoKhaoNghiemSP
                        {
                            MaKN = MaKN,
                            TenKN = TenKN,
                            MaXa = MaXa,
                            SoDT = SoDT
                        };

                        DataProvide.Ins.DB.CoSoKhaoNghiemSPs.Add(unit);
                        DataProvide.Ins.DB.SaveChanges();
                        CSKNlist.Add(unit);
                        OnPropertyChangedEventHandler();
                        ClearInputFields();
                    });

            EditCommand = new RelayCommand<object>(
     (p) =>
     {

         return SelectedItem != null &&
                !string.IsNullOrEmpty(MaKN) &&
                DataProvide.Ins.DB.CoSoKhaoNghiemSPs.Any(x => x.MaKN == MaKN);
     },
     (p) =>
     {

         var existingMaXa = DataProvide.Ins.DB.CoSoKhaoNghiemSPs.Any(x => x.MaXa == MaXa);
         if (!existingMaXa)
         {
             MessageBox.Show("Mã xã không tồn tại trong hệ thống!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
             MaXa = null;
             return;
         }

         var unit = DataProvide.Ins.DB.CoSoKhaoNghiemSPs.SingleOrDefault(x => x.MaKN == MaKN);
         if (unit != null)
         {

             unit.MaKN = MaKN;
             unit.TenKN = TenKN;
             unit.MaXa = MaXa;
             unit.SoDT = SoDT;
             DataProvide.Ins.DB.SaveChanges();
             SelectedItem.MaKN = MaKN;
             SelectedItem.TenKN = TenKN;
             SelectedItem.MaXa = MaXa;
             SelectedItem.SoDT = SoDT;

             OnPropertyChangedEventHandler(nameof(CSKNlist));
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
                    if (MaKN == null || MaXa == null || TenKN == null || SoDT == null)
                    {
                        LoadData();
                    }
                    var results = DataProvide.Ins.DB.CoSoKhaoNghiemSPs.Where(x =>
                   (string.IsNullOrEmpty(MaKN) || x.MaKN.Contains(MaKN)) &&
                   (string.IsNullOrEmpty(TenKN) || x.TenKN.Contains(TenKN)) &&
                   (string.IsNullOrEmpty(MaXa) || x.MaXa.Contains(MaXa)) &&
                   (string.IsNullOrEmpty(SoDT) || x.SoDT.Contains(SoDT)))
                    .ToList();
                    if (results != null)
                    {


                        CSKNlist.Clear();
                        foreach (var item in results)
                        {
                            CSKNlist.Add(item);
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
                    var unit = DataProvide.Ins.DB.CoSoKhaoNghiemSPs.SingleOrDefault(x => x.MaKN == MaKN);
                    if (unit != null)
                    {
                        DataProvide.Ins.DB.CoSoKhaoNghiemSPs.Remove(unit);
                        DataProvide.Ins.DB.SaveChanges();
                        CSKNlist.Remove(SelectedItem);
                        ClearInputFields();
                        MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                });
        }

        private void LoadData()
        {
            CSKNlist = new ObservableCollection<CoSoKhaoNghiemSP>(
                DataProvide.Ins.DB.CoSoKhaoNghiemSPs);
        }

        private void ClearInputFields()
        {
            MaKN = null;
            TenKN = null;
            MaXa = null;
            SoDT = null;
            SelectedItem = null;
        }
    }
}

