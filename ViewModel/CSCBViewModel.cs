using KTPMUDMVVM.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
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
                        Dongvat = SelectedItem.DongVat?.MaDV ?? "Không có thông tin"; // Kiểm tra nếu DongVat là null thì gán "Không có thông tin"
                        SoluongDV = SelectedItem.SoLuongDV ?? 0;
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
                    return !string.IsNullOrEmpty(MaCB) && !string.IsNullOrEmpty(Dongvat) && SoluongDV > 0;
                },
                (p) =>
                {
                    if (MaXa == null || MaCB == null || Ten == null || SoDT == null || Dongvat == null || SoluongDV <= 0)
                    {
                        MessageBox.Show("Chưa điền đủ thông tin cần thiết!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
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
                        SoDT = SoDT,
                        DongVat = new DongVat { MaDV = Dongvat }, // Add Dongvat
                        SoLuongDV = SoluongDV // Add SoluongDV
                    };

                    DataProvide.Ins.DB.CoSoCheBiens.Add(unit);
                    DataProvide.Ins.DB.SaveChanges();
                    CSCBlist.Add(unit);
                    ClearInputFields();
                    OnPropertyChangedEventHandler();
                });

            EditCommand = new RelayCommand<object>(
    (p) =>
    {
        // Ensure that the SelectedItem exists and the MaCB is valid
        return SelectedItem != null && !string.IsNullOrEmpty(MaCB) && DataProvide.Ins.DB.CoSoCheBiens.Any(x => x.MaCB == MaCB);
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
            // Update properties directly in the unit
            unit.MaCB = MaCB;
            unit.Ten = Ten;
            unit.MaXa = MaXa;
            unit.SoDT = SoDT;

            // Handle the DongVat property: We should look for the DongVat in the DB or create a new one
            if (Dongvat != null)
            {
                var dongVatObject = DataProvide.Ins.DB.DongVats.SingleOrDefault(x => x.MaDV == Dongvat);
                if (dongVatObject != null)
                {
                    unit.DongVat = dongVatObject; // Update with the existing DongVat object
                }
                else
                {
                    unit.DongVat = new DongVat { MaDV = Dongvat }; // Create a new DongVat object if not found
                }
            }

            // Update SoluongDV (quantity)
            unit.SoLuongDV = SoluongDV;

            // Save changes to the database
            DataProvide.Ins.DB.SaveChanges();

            // Update the selected item as well
            SelectedItem.MaCB = MaCB;
            SelectedItem.Ten = Ten;
            SelectedItem.MaXa = MaXa;
            SelectedItem.SoDT = SoDT;
            SelectedItem.DongVat = unit.DongVat;
            SelectedItem.SoLuongDV = SoluongDV;

            // Notify the UI about the change
            OnPropertyChangedEventHandler(nameof(CSCBlist));

            MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        else
        {
            MessageBox.Show("Không tìm thấy cơ sở chế biến này!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    });


            SearchCommand = new RelayCommand<object>(
                (p) => true,
                (p) =>
                {
                    if (MaCB == null || MaXa == null || Ten == null || SoDT == null || Dongvat == null || SoluongDV <= 0)
                    {
                        LoadData();
                    }

                    var results = DataProvide.Ins.DB.CoSoCheBiens.Where(x =>
                        (string.IsNullOrEmpty(MaCB) || x.MaCB.Contains(MaCB)) &&
                        (string.IsNullOrEmpty(Ten) || x.Ten.Contains(Ten)) &&
                        (string.IsNullOrEmpty(MaXa) || x.MaXa.Contains(MaXa)) &&
                        (string.IsNullOrEmpty(SoDT) || x.SoDT.Contains(SoDT)) &&
                        (string.IsNullOrEmpty(Dongvat) || x.DongVat.MaDV.Contains(Dongvat)))
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
                        MessageBox.Show("Không tìm thấy Cơ sở chế biến!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadData();
                    }
                });

            DeleteCommand = new RelayCommand<object>(
                (p) => SelectedItem != null,
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
            CSCBlist = new ObservableCollection<CoSoCheBien>(DataProvide.Ins.DB.CoSoCheBiens);
        }

        private void ClearInputFields()
        {
            MaCB = null;
            Ten = null;
            MaXa = null;
            SoDT = null;
            Dongvat = null;
            SoluongDV = 0;
            SelectedItem = null;
        }
    }
}
