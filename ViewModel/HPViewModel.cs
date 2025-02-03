using KTPMUDMVVM.Model;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;

namespace KTPMUDMVVM.ViewModel
{
    public class HPViewModel : BaseViewModel
    {
        private int _CSCB;
        private int _CSCN;
        private int _CSSX;

        public int CSCB
        {
            get => _CSCB;
            set 
            { 
                _CSCB = value;
                OnPropertyChangedEventHandler();
            }
        }
        public int CSSX
        {
            get => _CSSX;
            set
            {
                _CSSX = value;
                OnPropertyChangedEventHandler();
            }
        }
        public int CSCN
        {
            get => _CSCN;
            set
            {
                _CSCN = value;
                OnPropertyChangedEventHandler();
            }
        }
        private int _Dichbenh { get; set; }
        public int DichBenh
        {
            get => _Dichbenh;
            set
            {
                _Dichbenh = value;
                OnPropertyChangedEventHandler();
            }
        }

        private ObservableCollection<string> _TVlist;
        public ObservableCollection<string> TVlist
        {
            get => _TVlist;
            set
            {
                _TVlist = value;
                OnPropertyChangedEventHandler();
            }
        }

        public ChartValues<int> CSCNValues { get; set; }
        public ChartValues<int> CSSXValues { get; set; }
        public ChartValues<int> CSCBValues { get; set; }
        public ChartValues<int> CSGMValues {  get; set; }
        public ChartValues<int> CSKNValues { get; set; }
        public ChartValues<int> DLBTVal { get; set; }
        public ObservableCollection<string> AxisLabels { get; set; }
        public ChartValues<int> DiseaseCountsValues { get; set; }  // Để lưu số lượng cơ sở chăn nuôi theo bệnh
        public ObservableCollection<string> DiseaseNames { get; set; } // Để lưu tên các bệnh

        public void LoadData()
        {
            // Cập nhật dữ liệu cho các biểu đồ dòng (cơ sở chăn nuôi, cơ sở chế biến, ...)
            CSCN = DataProvide.Ins.DB.CoSoChanNuois.Count();
            CSSX = DataProvide.Ins.DB.CoSoSanXuatSPs.Count();
            CSCB = DataProvide.Ins.DB.CoSoCheBiens.Count();
            DichBenh = DataProvide.Ins.DB.Dichbenhs.Count();

            // Lưu dữ liệu cho các biểu đồ dòng
            CSCNValues = new ChartValues<int> { CSCN };
            CSSXValues = new ChartValues<int> { CSSX };
            CSCBValues = new ChartValues<int> { CSCB };
            CSGMValues = new ChartValues<int> { DataProvide.Ins.DB.CoSoGietMoes.Count() };
            CSKNValues = new ChartValues<int> { DataProvide.Ins.DB.CoSoKhaoNghiemSPs.Count() };
            DLBTVal = new ChartValues<int> { DataProvide.Ins.DB.DaiLyBanThuocs.Count() };

            // Lấy kết quả từ hàm đếm bệnh
            var diseaseCount = GetDiseaseCount();

            // Lưu kết quả cho biểu đồ cột dọc
            DiseaseCountsValues = new ChartValues<int>(diseaseCount.Values);  // Giá trị đếm số lượng cơ sở bị bệnh
            DiseaseNames = new ObservableCollection<string>(diseaseCount.Keys);  // Tên các bệnh

            var cơSởDanhSách = new List<string> { "CSCN", "CSSX", "CSCB", "CSGM", "CSKN", "DLBT" };
            AxisLabels = new ObservableCollection<string>(cơSởDanhSách);

            // Cập nhật danh sách các tỉnh
            TVlist = new ObservableCollection<string>(DataProvide.Ins.DB.Tinhs.Select(x => x.TenTinh).ToList());

            OnPropertyChangedEventHandler();
        }
        public Dictionary<string, int> GetDiseaseCount()
        {
            var diseaseCountDict = new Dictionary<string, int>();

            // Lấy danh sách bệnh và đếm số lượng cơ sở chăn nuôi bị ảnh hưởng
            var diseases = DataProvide.Ins.DB.Dichbenhs.ToList();

            foreach (var disease in diseases)
            {
                // Đếm số lượng cơ sở chăn nuôi liên quan đến bệnh này
                int count = disease.CoSoChanNuois.Count();

                // Thêm vào dictionary
                diseaseCountDict[disease.TenDB] = count;
            }

            return diseaseCountDict;
        }


        public HPViewModel()
        {
            LoadData();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
