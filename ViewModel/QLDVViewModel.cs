using KTPMUDMVVM.Model;
using Microsoft.Expression.Interactivity.Media;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace KTPMUDMVVM.ViewModel
{
    public class QLDVViewModel : BaseViewModel
    {
        private Xa _tenXa;
        public Xa TenXa
        {
            get => _tenXa;
            set
            {
                if (_tenXa != value)
                {
                    _tenXa = value;
                    OnPropertyChanged(nameof(TenXa));
                    UpdateTenCSList(); // Cập nhật lại danh sách cơ sở khi chọn một xã mới
                }
            }
        }

        private string _loaiCS;
        public string LoaiCS
        {
            get => _loaiCS;
            set
            {
                if (_loaiCS != value)
                {
                    _loaiCS = value;
                    OnPropertyChanged(nameof(LoaiCS));
                }
            }
        }

        private string _tenCS;
        public string TenCS
        {
            get => _tenCS;
            set
            {
                if (_tenCS != value)
                {
                    _tenCS = value;
                    OnPropertyChanged(nameof(TenCS));
                }
            }
        }

        private ObservableCollection<Xa> _tenXalist;
        public ObservableCollection<Xa> TenXalist
        {
            get => _tenXalist;
            set
            {
                if (_tenXalist != value)
                {
                    _tenXalist = value;
                    OnPropertyChanged(nameof(TenXalist));
                }
            }
        }

        private ObservableCollection<string> _loaiCSlist;
        public ObservableCollection<string> LoaiCSlist
        {
            get => _loaiCSlist;
            set
            {
                if (_loaiCSlist != value)
                {
                    _loaiCSlist = value;
                    OnPropertyChanged(nameof(LoaiCSlist));
                }
            }
        }

        private ObservableCollection<string> _tenCSlist;
        public ObservableCollection<string> TenCSlist
        {
            get => _tenCSlist;
            set
            {
                if (_tenCSlist != value)
                {
                    _tenCSlist = value;
                    OnPropertyChanged(nameof(TenCSlist));
                }
            }
        }

        private ObservableCollection<DongVat> _qldvlist;
        public ObservableCollection<DongVat> QLDVlist
        {
            get => _qldvlist;
            set
            {
                if (_qldvlist != value)
                {
                    _qldvlist = value;
                    OnPropertyChanged(nameof(QLDVlist));
                }
            }
        } 



        public ICommand SearchCommand { get; private set; }

        public QLDVViewModel()
        {
            LoadData();
            QLDVlist = new ObservableCollection<DongVat>();
            TenCSlist = new ObservableCollection<string>(DataProvide.Ins.DB.CoSoChanNuois.Select(x => x.Ten).ToList());
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            SearchCommand = new RelayCommand<object>(
                (p) => CanSearch(),
                (p) => PerformSearch()
            );
        }

        private bool CanSearch()
        {
            return !string.IsNullOrEmpty(TenCS) && !string.IsNullOrEmpty(LoaiCS);
        }

        private void PerformSearch()
        {
            try
            {
                // Làm trống danh sách động vật trước khi tìm kiếm
                QLDVlist.Clear();

                if (LoaiCS == "Cơ sở chăn nuôi")
                {
                    var dongVatList = DataProvide.Ins.DB.CoSoChanNuois
                        .Where(x => x.Ten == TenCS)
                        .Select(x => x.MaDV)
                        .ToList();

                    if (!dongVatList.Any())
                    {
                        MessageBox.Show("Không tìm thấy cơ sở chăn nuôi nào khớp với tên đã nhập.", "Thông báo");
                        return;
                    }

                    foreach (var maDV in dongVatList)
                    {
                        var dongVat = FindDongVatByMaDV(maDV);

                        if (dongVat != null)
                        {
                            QLDVlist.Add(dongVat);
                        }
                    }
                    OnPropertyChanged(nameof(QLDVlist));
                }
                else if (LoaiCS == "Cơ sở chế biến")
                {
                    var coSoCheBienList = DataProvide.Ins.DB.CoSoCheBiens
                        .Where(x => x.Ten == TenCS)
                        .ToList();

                    if (!coSoCheBienList.Any())
                    {
                        MessageBox.Show("Không tìm thấy cơ sở chế biến nào khớp với tên đã nhập.", "Thông báo");
                        return;
                    }

                    var dongVatList = coSoCheBienList.Select(x => x.DongVat).ToList();
                    QLDVlist = new ObservableCollection<DongVat>(dongVatList);
                    OnPropertyChanged(nameof(QLDVlist)); // Cập nhật danh sách động vật
                }

                // Reset các giá trị
                LoaiCS = null;
                TenXa = null;
                TenCS = null;
                OnPropertyChanged(nameof(LoaiCS));
                OnPropertyChanged(nameof(TenXa));
                OnPropertyChanged(nameof(TenCS));

                MessageBox.Show("Tìm kiếm thành công!", "Thông báo");

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi");
            }
        }

        private void LoadData()
        {
            TenXalist = new ObservableCollection<Xa>(DataProvide.Ins.DB.Xas.ToList());
            LoaiCSlist = new ObservableCollection<string> { "Cơ sở chăn nuôi", "Cơ sở chế biến" };
            TenCSlist = new ObservableCollection<string>();
        }

        private void UpdateTenCSList()
        {
            if (TenXa == null) return; // Kiểm tra nếu xã chưa được chọn

            if (LoaiCS == "Cơ sở chăn nuôi") // Cơ Sở Chăn Nuôi
            {
                TenCSlist = new ObservableCollection<string>(
                    DataProvide.Ins.DB.CoSoChanNuois
                        .Where(x => x.MaXa == TenXa.MaXa)
                        .Select(x => x.Ten)
                        .ToList()
                );
            }
            else if (LoaiCS == "Cơ sở chế biến") // Cơ Sở Chế Biến
            {
                TenCSlist = new ObservableCollection<string>(
                    DataProvide.Ins.DB.CoSoCheBiens
                        .Where(x => x.MaXa == TenXa.MaXa)
                        .Select(x => x.Ten)
                        .ToList()
                );
            }

            OnPropertyChanged(nameof(TenCSlist));
        }

        public DongVat FindDongVatByMaDV(string maDV)
        {
            try
            {
                if (string.IsNullOrEmpty(maDV))
                {
                    MessageBox.Show("Mã động vật không hợp lệ.", "Thông báo");
                    return null;
                }

                DongVat dongVat = DataProvide.Ins.DB.DongVats.FirstOrDefault(x => x.MaDV == maDV);

                if (dongVat == null)
                {
                    MessageBox.Show($"Không tìm thấy động vật với Mã: {maDV}", "Thông báo");
                }

                return dongVat;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi");
                return null;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
