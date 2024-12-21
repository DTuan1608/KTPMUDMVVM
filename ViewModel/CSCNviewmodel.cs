using KTPMUDMVVM.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows;

namespace KTPMUDMVVM.ViewModel
{
    public class CSCNViewmodel : BaseViewModel
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

        private string _TenCN;
        public string TenCN
        {
            get => _TenCN;
            set
            {
                _TenCN = value;
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
                        TenCN = SelectedItem.TenCN;
                    }
                }
            }
        }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public CSCNViewmodel()
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
                    if (MaXa == null || MaCN == null || TenCN == null || SoDT == null)
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
                        TenCN = TenCN,
                        MaXa = MaXa,
                        SoDT = SoDT
                    };

                    DataProvide.Ins.DB.CoSoChanNuois.Add(unit);
                    DataProvide.Ins.DB.SaveChanges();
                    CSCNlist.Add(unit);
                    ClearInputFields();
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
             unit.TenCN = TenCN;
             unit.MaXa = MaXa;
             unit.SoDT = SoDT;


             DataProvide.Ins.DB.SaveChanges();


             SelectedItem.MaCN = MaCN;
             SelectedItem.TenCN = TenCN;
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
                    if (DataProvide.Ins.DB.CoSoChanNuois.SingleOrDefault(x => x.MaCN == MaCN || x.TenCN == TenCN || x.MaXa == MaXa || x.SoDT == SoDT) == null || MaCN != null)
                    {

                        return false;
                    }
                    return true;
                },
                (p) =>
                {
                    if (MaCN == null || MaXa == null || TenCN == null || SoDT == null)
                    {
                        LoadData();
                    }
                    var results = DataProvide.Ins.DB.CoSoChanNuois.Where(x =>
                   (string.IsNullOrEmpty(MaCN) || x.MaCN.Contains(MaCN)) &&
                   (string.IsNullOrEmpty(TenCN) || x.TenCN  .Contains(TenCN)) &&
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
        }

        private void ClearInputFields()
        {
            MaCN = null;
            TenCN = null;
            MaXa = null;
            SoDT = null;
            SelectedItem = null;
        }
    }
}