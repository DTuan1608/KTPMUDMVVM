using KTPMUDMVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace KTPMUDMVVM.ViewModel
{
    public class CSSXSPViewModel : BaseViewModel
    {
            private ObservableCollection<CoSoSanXuatSP> _CSSXlist;
            public ObservableCollection<CoSoSanXuatSP> CSSXlist
            {
                get => _CSSXlist;
                set
                {
                    _CSSXlist = value;
                    OnPropertyChangedEventHandler();
                }
            }

            private string _MaSX;
            public string MaSX
            {
                get => _MaSX;
                set
                {
                    _MaSX = value;
                    OnPropertyChangedEventHandler();
                }
            }

            private string _TenSX;
            public string TenSX
            {
                get => _TenSX;
                set
                {
                    _TenSX = value;
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

            private CoSoSanXuatSP _SelectedItem;
            public CoSoSanXuatSP SelectedItem
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
                            MaSX = SelectedItem.MaSX;
                            SoDT = SelectedItem.SoDT;
                            MaXa = SelectedItem.MaXa;
                            TenSX = SelectedItem.TenSX;
                        }
                    }
                }
            }

            public ICommand AddCommand { get; set; }
            public ICommand EditCommand { get; set; }
            public ICommand SearchCommand { get; set; }
            public ICommand DeleteCommand { get; set; }

            public ICommand DetailCommand { get; set; }

            public CSSXSPViewModel()
            {
                LoadData();

                AddCommand = new RelayCommand<object>(
                        (p) =>
                        {
                            //if (DataProvide.Ins.DB.CoSoGietMoes.Any(x => x.MaGM == MaGM) == true)
                            //{
                            //    return false;
                            //}
                            return !string.IsNullOrEmpty(MaSX) && DataProvide.Ins.DB.CoSoSanXuatSPs.Any(x => x.MaSX == SelectedItem.MaSX);
                        },
                        (p) =>
                        {
                            if (MaXa == null || MaSX == null || TenSX == null || SoDT == null)
                            {
                                MessageBox.Show("Chua dien du thong tin can thiet!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }
                            var existingMaXa = DataProvide.Ins.DB.CoSoSanXuatSPs.Any(x => x.MaXa == MaXa);
                            if (!existingMaXa)
                            {
                                MessageBox.Show("Mã xã không tồn tại trong hệ thống!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                                MaXa = null;
                                return;
                            }

                            var unit = new CoSoSanXuatSP
                            {
                                MaSX = MaSX,
                                TenSX = TenSX,
                                MaXa = MaXa,
                                SoDT = SoDT
                            };

                            DataProvide.Ins.DB.CoSoSanXuatSPs.Add(unit);
                            DataProvide.Ins.DB.SaveChanges();
                            CSSXlist.Add(unit);
                            OnPropertyChangedEventHandler();
                            ClearInputFields();
                        });

                EditCommand = new RelayCommand<object>(
         (p) =>
         {

             return SelectedItem != null &&
                    !string.IsNullOrEmpty(MaSX) &&
                    DataProvide.Ins.DB.CoSoSanXuatSPs.Any(x => x.MaSX == MaSX);
         },
         (p) =>
         {

             var existingMaXa = DataProvide.Ins.DB.CoSoSanXuatSPs.Any(x => x.MaXa == MaXa);
             if (!existingMaXa)
             {
                 MessageBox.Show("Mã xã không tồn tại trong hệ thống!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                 MaXa = null;
                 return;
             }

             var unit = DataProvide.Ins.DB.CoSoSanXuatSPs.SingleOrDefault(x => x.MaSX == MaSX);
             if (unit != null)
             {

                 unit.MaSX = MaSX;
                 unit.TenSX = TenSX;
                 unit.MaXa = MaXa;
                 unit.SoDT = SoDT;


                 DataProvide.Ins.DB.SaveChanges();


                 SelectedItem.MaSX = MaSX;
                 SelectedItem.TenSX = TenSX;
                 SelectedItem.MaXa = MaXa;
                 SelectedItem.SoDT = SoDT;


                 OnPropertyChangedEventHandler(nameof(CSSXlist));
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
                        if (MaSX == null || MaXa == null || TenSX == null || SoDT == null)
                        {
                            LoadData();
                        }
                        var results = DataProvide.Ins.DB.CoSoSanXuatSPs.Where(x =>
                       (string.IsNullOrEmpty(MaSX) || x.MaSX.Contains(MaSX)) &&
                       (string.IsNullOrEmpty(TenSX) || x.TenSX.Contains(TenSX)) &&
                       (string.IsNullOrEmpty(MaXa) || x.MaXa.Contains(MaXa)) &&
                       (string.IsNullOrEmpty(SoDT) || x.SoDT.Contains(SoDT)))
                        .ToList();
                        if (results != null)
                        {


                            CSSXlist.Clear();
                            foreach (var item in results)
                            {
                                CSSXlist.Add(item);
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
                        var unit = DataProvide.Ins.DB.CoSoSanXuatSPs.SingleOrDefault(x => x.MaSX == MaSX);
                        if (unit != null)
                        {
                            DataProvide.Ins.DB.CoSoSanXuatSPs.Remove(unit);
                            DataProvide.Ins.DB.SaveChanges();
                            CSSXlist.Remove(SelectedItem);
                            ClearInputFields();
                            MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    });
            }

            private void LoadData()
            {
                CSSXlist = new ObservableCollection<CoSoSanXuatSP>(
                    DataProvide.Ins.DB.CoSoSanXuatSPs);
            }

            private void ClearInputFields()
            {
                MaSX = null;
                TenSX = null;
                MaXa = null;
                SoDT = null;
                SelectedItem = null;
            }
       
    }
}
