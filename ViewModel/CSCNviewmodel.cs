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
            get => _Dongvat;
            set
            {
                _Dongvat = value;
                OnPropertyChangedEventHandler();
            }
        }

        private int _SoluongDV;
        public int SoluongDV
        {
            get => _SoluongDV;
            set
            {
                _SoluongDV = value;
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
                        Ten = SelectedItem.Ten;
                        Dongvat = SelectedItem.DongVat?.MaDV ?? "Không có thông tin"; // Default if null
                        SoluongDV = SelectedItem.SoLuongDV ?? 0;
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
            Dongvat = string.Empty;
            SoluongDV = 0;
            LoadData();

            AddCommand = new RelayCommand<object>(
                (p) =>
                {
                    return !string.IsNullOrEmpty(MaCN) &&
                           !DataProvide.Ins.DB.CoSoChanNuois.Any(x => x.MaCN == MaCN);
                },
                (p) =>
                {
                    // Check for empty fields
                    if (string.IsNullOrEmpty(MaXa) || string.IsNullOrEmpty(MaCN) || string.IsNullOrEmpty(Ten) || string.IsNullOrEmpty(SoDT) || string.IsNullOrEmpty(Dongvat))
                    {
                        MessageBox.Show("Chưa điền đủ thông tin cần thiết!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    // Check if MaXa exists
                    var existingMaXa = DataProvide.Ins.DB.CoSoChanNuois.Any(x => x.MaXa == MaXa);
                    if (!existingMaXa)
                    {
                        MessageBox.Show("Mã xã không tồn tại trong hệ thống!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        MaXa = null; // Reset MaXa
                        return;
                    }

                    // Add new CoSoChanNuoi
                    var unit = new CoSoChanNuoi
                    {
                        MaCN = MaCN,
                        Ten = Ten,
                        MaXa = MaXa,
                        SoDT = SoDT,
                        SoLuongDV = SoluongDV,
                        DongVat = new DongVat { MaDV = Dongvat } // Assuming DongVat is an entity with MaDV
                    };

                    // Add to DB
                    DataProvide.Ins.DB.CoSoChanNuois.Add(unit);
                    DataProvide.Ins.DB.SaveChanges();

                    // Add to ViewModel's list
                    CSCNlist.Add(unit);

                    // Clear fields
                    ClearInputFields();
                });

            EditCommand = new RelayCommand<object>(
                (p) =>
                {
                    return SelectedItem != null && !string.IsNullOrEmpty(MaCN);
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
                        unit.DongVat = new DongVat { MaDV = Dongvat }; // Update DongVat
                        unit.SoLuongDV = SoluongDV; // Update SoLuongDV

                        DataProvide.Ins.DB.SaveChanges();

                        // Update SelectedItem
                        SelectedItem.MaCN = MaCN;
                        SelectedItem.Ten = Ten;
                        SelectedItem.MaXa = MaXa;
                        SelectedItem.SoDT = SoDT;
                        SelectedItem.DongVat = new DongVat { MaDV = Dongvat }; // Update DongVat in SelectedItem
                        SelectedItem.SoLuongDV = SoluongDV;

                        OnPropertyChangedEventHandler(nameof(CSCNlist));
                        MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy cơ sở chăn nuôi này!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                });

            SearchCommand = new RelayCommand<object>(
                (p) => true,
                (p) =>
                {
                    var results = DataProvide.Ins.DB.CoSoChanNuois.Where(x =>
                        (string.IsNullOrEmpty(MaCN) || x.MaCN.Contains(MaCN)) &&
                        (string.IsNullOrEmpty(Ten) || x.Ten.Contains(Ten)) &&
                        (string.IsNullOrEmpty(MaXa) || x.MaXa.Contains(MaXa)) &&
                        (string.IsNullOrEmpty(SoDT) || x.SoDT.Contains(SoDT)) &&
                        (string.IsNullOrEmpty(Dongvat) || x.DongVat.MaDV.Contains(Dongvat)))
                        .ToList();

                    CSCNlist.Clear();
                    foreach (var item in results)
                    {
                        CSCNlist.Add(item);
                    }

                    if (!results.Any())
                    {
                        MessageBox.Show("Không tìm thấy cơ sở chăn nuôi!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadData();
                    }
                });

            DeleteCommand = new RelayCommand<object>(
                (p) => SelectedItem != null,
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
            CSCNlist = new ObservableCollection<CoSoChanNuoi>(DataProvide.Ins.DB.CoSoChanNuois);
            OnPropertyChangedEventHandler();
        }

        private void ClearInputFields()
        {
            MaCN = null;
            Ten = null;
            MaXa = null;
            SoDT = null;
            Dongvat = string.Empty;
            SoluongDV = 0;
            SelectedItem = null;
        }
    }
}
