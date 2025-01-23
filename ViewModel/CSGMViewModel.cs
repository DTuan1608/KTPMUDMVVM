using KTPMUDMVVM.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Windows;
using System.Windows.Input;

namespace KTPMUDMVVM.ViewModel
{
    public class CSGMViewModel : BaseViewModel
    {
        private ObservableCollection<CoSoGietMo> _CSGMlist;
        public ObservableCollection<CoSoGietMo> CSGMlist
        {
            get => _CSGMlist;
            set
            {
                _CSGMlist = value;
                OnPropertyChangedEventHandler();
            }
        }

        private string _MaGM;
        public string MaGM
        {
            get => _MaGM;
            set
            {
                _MaGM = value;
                OnPropertyChangedEventHandler();
            }
        }

        private string _TenGM;
        public string TenGM
        {
            get => _TenGM;
            set
            {
                _TenGM = value;
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

        private CoSoGietMo _SelectedItem;
        public CoSoGietMo SelectedItem
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
                        MaGM = SelectedItem.MaGM;
                        SoDT = SelectedItem.SoDT;
                        MaXa = SelectedItem.MaXa;
                        TenGM = SelectedItem.TenGM;
                    }
                }
            }
        }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public ICommand DetailCommand { get; set; }

        public CSGMViewModel()
        {
            LoadData();

            AddCommand = new RelayCommand<object>(
                    (p) =>
                    {
                        //if (DataProvide.Ins.DB.CoSoGietMoes.Any(x => x.MaGM == MaGM) == true)
                        //{
                        //    return false;
                        //}
                        return !string.IsNullOrEmpty(MaGM) && DataProvide.Ins.DB.CoSoGietMoes.Any(x => x.MaGM == SelectedItem.MaGM);
                    },
                    (p) =>
                    {
                        if (MaXa == null || MaGM == null || TenGM == null || SoDT == null)
                        {
                            MessageBox.Show("Chua dien du thong tin can thiet!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        var existingMaXa = DataProvide.Ins.DB.CoSoGietMoes.Any(x => x.MaXa == MaXa);
                        if (!existingMaXa)
                        {
                            MessageBox.Show("Mã xã không tồn tại trong hệ thống!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                            MaXa = null;
                            return;
                        }

                        var unit = new CoSoGietMo
                        {
                            MaGM = MaGM,
                            TenGM = TenGM,
                            MaXa = MaXa,
                            SoDT = SoDT
                        };

                        DataProvide.Ins.DB.CoSoGietMoes.Add(unit);
                        DataProvide.Ins.DB.SaveChanges();
                        CSGMlist.Add(unit);
                        OnPropertyChangedEventHandler();
                        ClearInputFields();
                    });

            EditCommand = new RelayCommand<object>(
     (p) =>
     {

         return SelectedItem != null &&
                !string.IsNullOrEmpty(MaGM) &&
                DataProvide.Ins.DB.CoSoGietMoes.Any(x => x.MaGM == MaGM);
     },
     (p) =>
     {

         var existingMaXa = DataProvide.Ins.DB.CoSoGietMoes.Any(x => x.MaXa == MaXa);
         if (!existingMaXa)
         {
             MessageBox.Show("Mã xã không tồn tại trong hệ thống!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
             MaXa = null;
             return;
         }

         var unit = DataProvide.Ins.DB.CoSoGietMoes.SingleOrDefault(x => x.MaGM == MaGM);
         if (unit != null)
         {

             unit.MaGM = MaGM;
             unit.TenGM = TenGM;
             unit.MaXa = MaXa;
             unit.SoDT = SoDT;


             DataProvide.Ins.DB.SaveChanges();


             SelectedItem.MaGM = MaGM;
             SelectedItem.TenGM = TenGM;
             SelectedItem.MaXa = MaXa;
             SelectedItem.SoDT = SoDT;


             OnPropertyChangedEventHandler(nameof(CSGMlist));
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
                    if (MaGM == null || MaXa == null || TenGM == null || SoDT == null)
                    {
                        LoadData();
                    }
                    var results = DataProvide.Ins.DB.CoSoGietMoes.Where(x =>
                   (string.IsNullOrEmpty(MaGM) || x.MaGM.Contains(MaGM)) &&
                   (string.IsNullOrEmpty(TenGM) || x.TenGM.Contains(TenGM)) &&
                   (string.IsNullOrEmpty(MaXa) || x.MaXa.Contains(MaXa)) &&
                   (string.IsNullOrEmpty(SoDT) || x.SoDT.Contains(SoDT)))
                    .ToList();
                    if (results != null)
                    {


                        CSGMlist.Clear();
                        foreach (var item in results)
                        {
                            CSGMlist.Add(item);
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
                    var unit = DataProvide.Ins.DB.CoSoGietMoes.SingleOrDefault(x => x.MaGM == MaGM);
                    if (unit != null)
                    {
                        DataProvide.Ins.DB.CoSoGietMoes.Remove(unit);
                        DataProvide.Ins.DB.SaveChanges();
                        CSGMlist.Remove(SelectedItem);
                        ClearInputFields();
                        MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                });
        }

        private void LoadData()
        {
            CSGMlist = new ObservableCollection<CoSoGietMo>(
                DataProvide.Ins.DB.CoSoGietMoes);
        }

        private void ClearInputFields()
        {
            MaGM = null;
            TenGM = null;
            MaXa = null;
            SoDT = null;
            SelectedItem = null;
        }
    }
}
