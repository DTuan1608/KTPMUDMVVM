    using KTPMUDMVVM.Model;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.ComponentModel;
    using System.Security.Cryptography.X509Certificates;
    using System.Windows.Input;
    using System.Windows;

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

            public QLDBViewModel()
            {
                QLDBList = new ObservableCollection<object>();
                MaTinhList = new ObservableCollection<Tinh>(DataProvide.Ins.DB.Tinhs);
                LoadData(); 
                SearchCommand = new RelayCommand<object>
                (
                    (p) =>
                    {
                        return true;
                    },
                    (p) =>
                    {
                        if (string.IsNullOrEmpty(MaHuyen) && string.IsNullOrEmpty(MaXa) && string.IsNullOrEmpty(MaTinh))
                        {
                            MessageBox.Show("Chưa chọn đủ thông tin cần thiết", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                            LoadData();
                            return;
                        }
                        if (MaXa != null)
                        {
                            QLDBList.Clear();
                            var dataFromTable1 = DataProvide.Ins.DB.CoSoCheBiens
                                .Where(item => item.MaXa == MaXa)
                                .ToList()
                                .Select(item => new
                                {
                                    item.Ten,
                                    TenDB = GetTenDichBenh(item.MaDB)
                                });

                            var dataFromTable2 = DataProvide.Ins.DB.CoSoChanNuois
                                .Where(item => item.MaXa == MaXa)
                                .ToList() // Tải dữ liệu ra bộ nhớ
                                .Select(item => new
                                {
                                    item.Ten,
                                    TenDB = GetTenDichBenh(item.MaDB)
                                });
                            var combinedData = dataFromTable1.Concat(dataFromTable2);

                            foreach (var item in combinedData)
                            {
                                QLDBList.Add(item);
                            }

                            OnPropertyChanged(nameof(QLDBList));
                            MaHuyen = null;
                            MaXa = null;
                            MaTinh = null;
                        }

                    }

                );
            }
            public string GetTenDichBenh(string maDB)
            {
                if (string.IsNullOrEmpty(maDB))
                {
                    return "Không rõ";
                }

                var dichBenh = DataProvide.Ins.DB.Dichbenhs
                                .FirstOrDefault(db => db.MaDB == maDB);

                return dichBenh != null ? dichBenh.TenDB : "Không rõ";
            }
           
            private void LoadData()
            {
                QLDBList.Clear();
            var dataFromTable1 = DataProvide.Ins.DB.CoSoCheBiens
            .Select(item => new { item.Ten, item.MaDB, item.MaXa });

            var dataFromTable2 = DataProvide.Ins.DB.CoSoChanNuois
                .Select(item => new { item.Ten, item.MaDB, item.MaXa });
            var Combine = dataFromTable1.Concat(dataFromTable2);
            foreach (var item in Combine)
                {
                    QLDBList.Add(new { item.Ten, TenDB = GetTenDichBenh(item.MaDB) });
                }

                OnPropertyChanged(nameof(QLDBList));
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

                    var filteredXas = DataProvide.Ins.DB.Xas.Where(x => x.MaHuyen == MaHuyen).ToList();
                    foreach (var xa in filteredXas)
                    {
                        MaXaList.Add(xa);
                    }
                OnPropertyChanged($"{nameof(MaXaList)}");
            }

            public event PropertyChangedEventHandler PropertyChanged;

            protected void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

        }
    }
