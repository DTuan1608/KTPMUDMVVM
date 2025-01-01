using KTPMUDMVVM.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Windows;
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
                        Ten = SelectedItem.Ten;
                    }
                }
            }
        }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public ICommand DetailCommand { get; set; }
        
        public CSCBViewmodel()
        {
            LoadData();
            
        AddCommand = new RelayCommand<object>(
                (p) =>
                {
                    if (DataProvide.Ins.DB.CoSoCheBiens.Any(x => x.MaCB == MaCB) == true)
                    {
                        return false;
                    }
                    return !string.IsNullOrEmpty(MaCB) && DataProvide.Ins.DB.CoSoCheBiens.Any(x => x.MaCB == SelectedItem.MaCB);
                },
                (p) =>
                {
                    if (MaXa == null || MaCB == null || Ten == null || SoDT == null)
                    {
                        MessageBox.Show("Chua dien du thong tin can thiet!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    var existingMaXa = DataProvide.Ins.DB.CoSoCheBiens.Any(x => x.MaXa == MaXa);
                    if (!existingMaXa)
                    {
                        MessageBox.Show("Mã xã không tồn tại trong hệ thống!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        MaXa = null;
                        return;
                    }

                    var unit = new CoSoCheBien
                    {
                        MaCB = MaCB,
                        Ten = Ten,
                        MaXa = MaXa,
                        SoDT = SoDT
                    };

                    DataProvide.Ins.DB.CoSoCheBiens.Add(unit);
                    DataProvide.Ins.DB.SaveChanges();
                    CSCBlist.Add(unit);
                    ClearInputFields();
                });

            EditCommand = new RelayCommand<object>(
     (p) =>
     {

         return SelectedItem != null &&
                !string.IsNullOrEmpty(MaCB) &&
                DataProvide.Ins.DB.CoSoCheBiens.Any(x => x.MaCB == MaCB);
     },
     (p) =>
     {

         var existingMaXa = DataProvide.Ins.DB.CoSoCheBiens.Any(x => x.MaXa == MaXa);
         if (!existingMaXa)
         {
             MessageBox.Show("Mã xã không tồn tại trong hệ thống!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
             MaXa = null;
             return;
         }

         var unit = DataProvide.Ins.DB.CoSoCheBiens.SingleOrDefault(x => x.MaCB == MaCB);
         if (unit != null)
         {

             unit.MaCB = MaCB;
             unit.Ten = Ten;
             unit.MaXa = MaXa;
             unit.SoDT = SoDT;


             DataProvide.Ins.DB.SaveChanges();


             SelectedItem.MaCB = MaCB;
             SelectedItem.Ten = Ten;
             SelectedItem.MaXa = MaXa;
             SelectedItem.SoDT = SoDT;


             OnPropertyChangedEventHandler(nameof(CSCBlist));
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
                    if (MaCB == null || MaXa == null || Ten == null || SoDT == null )
                    {
                        LoadData();
                    }
                    var results = DataProvide.Ins.DB.CoSoCheBiens.Where(x =>
                   (string.IsNullOrEmpty(MaCB) || x.MaCB.Contains(MaCB)) &&
                   (string.IsNullOrEmpty(Ten) || x.Ten.Contains(Ten)) &&
                   (string.IsNullOrEmpty(MaXa) || x.MaXa.Contains(MaXa)) &&
                   (string.IsNullOrEmpty(SoDT) || x.SoDT.Contains(SoDT)))
                    .ToList();
                    if (results != null) 
                    { 
               

                        CSCBlist.Clear();
                        foreach (var item in results)
                        {
                            CSCBlist.Add(item);
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
                    var unit = DataProvide.Ins.DB.CoSoCheBiens.SingleOrDefault(x => x.MaCB == MaCB);
                    if (unit != null)
                    {
                        DataProvide.Ins.DB.CoSoCheBiens.Remove(unit);
                        DataProvide.Ins.DB.SaveChanges();
                        CSCBlist.Remove(SelectedItem);
                        ClearInputFields();
                        MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                });
        }

        private void LoadData()
        {
            CSCBlist = new ObservableCollection<CoSoCheBien>(
                DataProvide.Ins.DB.CoSoCheBiens);
        }

        private void ClearInputFields()
        {
            MaCB = null;
            Ten = null;
            MaXa = null;
            SoDT = null;
            SelectedItem = null;
        }
    }
}
