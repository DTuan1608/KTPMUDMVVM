using KTPMUDMVVM.Model;
using System.Collections.ObjectModel;
using System.Linq;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;
using System.Data.Entity.Infrastructure;
using System;

namespace KTPMUDMVVM.ViewModel
{
    public class QLDBViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private string _maXa;
        public string MaXa
        {
            get => _maXa;
            set
            {
                if (_maXa != value)
                {
                    _maXa = value;
                    OnPropertyChanged(nameof(MaXa));
                }
            }
        }

        private string _maHuyen;
        public string MaHuyen
        {
            get => _maHuyen;
            set
            {
                if (_maHuyen != value)
                {
                    _maHuyen = value;
                    OnPropertyChanged(nameof(MaHuyen));
                    UpdateXaList();
                }
            }
        }

        private string _maTinh;
        public string MaTinh
        {
            get => _maTinh;
            set
            {
                if (_maTinh != value)
                {
                    _maTinh = value;
                    OnPropertyChanged(nameof(MaTinh));
                    UpdateHuyenList();
                }
            }
        }

        private string _maDB;
        public string MaDB
        {
            get => _maDB;
            set
            {
                _maDB = value;
                OnPropertyChanged(nameof(MaDB));
            }
        }

        private string _tenDB;
        public string TenDB
        {
            get => _tenDB;
            set
            {
                _tenDB = value;
                OnPropertyChanged(nameof(TenDB));
            }
        }

        private string _mtDB;
        public string MTDB
        {
            get => _mtDB;
            set
            {
                _mtDB = value;
                OnPropertyChanged(nameof(MTDB));
            }
        }

        private int _mdnh;
        public int MDNH
        {
            get => _mdnh;
            set
            {
                _mdnh = value;
                OnPropertyChanged(nameof(MDNH));
            }
        }
        private object _selectedItem;
        public object SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                if (_selectedItem != null)
                {
                    var selectedItem = (CoSoInfo)_selectedItem; // Cast to CoSoInfo
                    var dichbenh = GetDichBenh(selectedItem.MaDB);
                    MaDB = dichbenh?.MaDB;
                    TenDB = dichbenh?.TenDB;
                    MTDB = dichbenh?.CachXyLy;
                    MDNH = dichbenh?.MucNguyHiem ?? 0;
                }
                OnPropertyChanged(nameof(SelectedItem));
            }
        }


        public ICommand SearchCommand { get; set; }

        public ObservableCollection<Tinh> MaTinhList { get; set; } = new ObservableCollection<Tinh>();
        public ObservableCollection<Huyen> MaHuyenList { get; set; } = new ObservableCollection<Huyen>();
        public ObservableCollection<Xa> MaXaList { get; set; } = new ObservableCollection<Xa>();

        private ObservableCollection<object> _qldbList;
        public ObservableCollection<object> QLDBList
        {
            get => _qldbList;
            set
            {
                _qldbList = value;
                OnPropertyChanged(nameof(QLDBList));
            }
        }
        public class CoSoInfo
        {
            public string Ten { get; set; }
            public string MaDB { get; set; }
            public string TenDB { get; set; }
        }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public QLDBViewModel()
        {
            MaTinhList = new ObservableCollection<Tinh>(DataProvide.Ins.DB.Tinhs);
            QLDBList = new ObservableCollection<object>();
            LoadData();

            SearchCommand = new RelayCommand<object>((p) => true, ExecuteSearchCommand);
            AddCommand = new RelayCommand<object>((p) => CanExecuteAdd(), ExecuteAddCommand);
            EditCommand = new RelayCommand<object>((p) => CanExecuteEdit(), ExecuteEditCommand);
            DeleteCommand = new RelayCommand<object>((p) => CanExecuteDelete(), ExecuteDeleteCommand);
        }

        private bool CanExecuteAdd()
        {
            return !string.IsNullOrEmpty(TenDB) && MDNH > 0; // Add condition based on your data validation
        }

        private void ExecuteAddCommand(object parameter)
        {
            if (string.IsNullOrEmpty(MaDB) || string.IsNullOrEmpty(TenDB) || string.IsNullOrEmpty(MTDB) || MDNH <= 0)
            {
                MessageBox.Show("Chưa điền đầy đủ thông tin cần thiết!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                var unit = new Dichbenh
                {
                    MaDB = MaDB,
                    TenDB = TenDB,
                    MucNguyHiem = MDNH,
                    CachXyLy = MTDB
                };

                // Check for duplicates
                var existingDisease = DataProvide.Ins.DB.Dichbenhs.FirstOrDefault(d => d.MaDB == MaDB);
                if (existingDisease != null)
                {
                    MessageBox.Show("Mã dịch bệnh đã tồn tại trong hệ thống!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Add and save changes
                DataProvide.Ins.DB.Dichbenhs.Add(unit);
                DataProvide.Ins.DB.SaveChanges();

                MessageBox.Show("Thêm dịch bệnh thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadData();  // Reload data to reflect the changes
            }
            catch (DbUpdateException dbEx)
            {
                MessageBox.Show($"Database error: {dbEx.Message}", "Lỗi Cập Nhật Cơ Sở Dữ Liệu", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private bool CanExecuteEdit()
        {
            return SelectedItem != null; // Ensure an item is selected
        }

        private void ExecuteEditCommand(object parameter)
        {
            // Edit the selected disease logic here
            var selectedItem = (CoSoInfo)SelectedItem;
            var diseaseToEdit = DataProvide.Ins.DB.Dichbenhs.FirstOrDefault(d => d.MaDB == selectedItem.MaDB);

            if (diseaseToEdit != null)
            {
                diseaseToEdit.TenDB = TenDB;
                diseaseToEdit.CachXyLy = MTDB;
                diseaseToEdit.MucNguyHiem = MDNH;

                DataProvide.Ins.DB.SaveChanges(); // Persist changes to the database
                LoadData(); // Reload data to reflect the changes
            }
        }

        private bool CanExecuteDelete()
        {
            return SelectedItem != null; // Ensure an item is selected
        }

        private void ExecuteDeleteCommand(object parameter)
        {
            // Delete the selected disease logic here
            var selectedItem = (CoSoInfo)SelectedItem;
            var diseaseToDelete = DataProvide.Ins.DB.Dichbenhs.FirstOrDefault(d => d.MaDB == selectedItem.MaDB);

            if (diseaseToDelete != null)
            {
                DataProvide.Ins.DB.Dichbenhs.Remove(diseaseToDelete);
                DataProvide.Ins.DB.SaveChanges(); // Remove from database

                LoadData(); // Reload data after deletion
            }
        }


        private void ExecuteSearchCommand(object parameter)
        {
            if (string.IsNullOrEmpty(MaHuyen) && string.IsNullOrEmpty(MaXa) && string.IsNullOrEmpty(MaTinh))
            {
                MessageBox.Show("Chưa chọn đủ thông tin cần thiết", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                LoadData();
                return;
            }

            if (!string.IsNullOrEmpty(MaXa))
            {
                PerformSearch();
            }
        }

        private void ResetFilters()
        {
            MaHuyen = null;
            MaXa = null;
            MaTinh = null;
        }

        public Dichbenh GetDichBenh(string maDB)
        {
            return string.IsNullOrEmpty(maDB)
                ? null
                : DataProvide.Ins.DB.Dichbenhs.FirstOrDefault(db => db.MaDB == maDB);
        }
        public string GetTenDichBenh(string maDB)
        {
            if (string.IsNullOrEmpty(maDB))
            {
                return string.Empty;
            }

            // Look up the Dichbenh record by MaDB
            var dichBenh = DataProvide.Ins.DB.Dichbenhs.FirstOrDefault(db => db.MaDB == maDB);

            return dichBenh?.TenDB ?? "Không tìm thấy bệnh"; // Return the name or a default message if not found
        }


        private void PerformSearch()
        {
            QLDBList.Clear();

            // Retrieve data into memory first
            var dataFromTable1 = DataProvide.Ins.DB.CoSoCheBiens
                .Where(item => item.MaXa == MaXa)
                .ToList() // Materialize the data
                .Select(item => new CoSoInfo
                {
                    Ten = item.Ten,
                    MaDB = item.MaDB,
                    TenDB = GetTenDichBenh(item.MaDB) // Apply the custom logic after materialization
                });

            var dataFromTable2 = DataProvide.Ins.DB.CoSoChanNuois
                .Where(item => item.MaXa == MaXa)
                .ToList() // Materialize the data
                .Select(item => new CoSoInfo
                {
                    Ten = item.Ten,
                    MaDB = item.MaDB,
                    TenDB = GetTenDichBenh(item.MaDB) // Apply the custom logic after materialization
                });

            var combinedData = dataFromTable1.Concat(dataFromTable2);

            foreach (var item in combinedData)
            {
                QLDBList.Add(item);
            }

            ResetFilters();
        }

        private void LoadData()
        {
            QLDBList.Clear();

            // Retrieve data into memory first
            var dataFromTable1 = DataProvide.Ins.DB.CoSoCheBiens
                .ToList() // Materialize the data
                .Select(item => new CoSoInfo
                {
                    Ten = item.Ten,
                    MaDB = item.MaDB,
                    TenDB = GetTenDichBenh(item.MaDB) // Apply the custom logic after materialization
                });

            var dataFromTable2 = DataProvide.Ins.DB.CoSoChanNuois
                .ToList() // Materialize the data
                .Select(item => new CoSoInfo
                {
                    Ten = item.Ten,
                    MaDB = item.MaDB,
                    TenDB = GetTenDichBenh(item.MaDB) // Apply the custom logic after materialization
                });

            var combinedData = dataFromTable1.Concat(dataFromTable2);

            foreach (var item in combinedData)
            {
                QLDBList.Add(item);
            }
        }



        private void UpdateHuyenList()
        {
            MaHuyenList.Clear();

            if (!string.IsNullOrEmpty(MaTinh))
            {
                var filteredHuyens = DataProvide.Ins.DB.Huyens.Where(h => h.MaTinh == MaTinh).ToList();
                foreach (var huyen in filteredHuyens)
                {
                    MaHuyenList.Add(huyen);
                }
            }

            OnPropertyChanged(nameof(MaHuyenList));
        }

        private void UpdateXaList()
        {
            MaXaList.Clear();

            if (!string.IsNullOrEmpty(MaHuyen))
            {
                var filteredXas = DataProvide.Ins.DB.Xas.Where(x => x.MaHuyen == MaHuyen).ToList();
                foreach (var xa in filteredXas)
                {
                    MaXaList.Add(xa);
                }
            }

            OnPropertyChanged(nameof(MaXaList));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}