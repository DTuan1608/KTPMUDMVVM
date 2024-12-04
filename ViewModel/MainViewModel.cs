using KTPMUDMVVM.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace KTPMUDMVVM.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public bool isLoaded = false;
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand CoSoChanNuoiCommand { get; set; }

        private UserControl _currentView;
        public UserControl CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChangedEventHandler(nameof(CurrentView));
            }
        }

        private ICommand _changeViewCommand;
        public ICommand ChangeViewCommand { get; }

        public MainViewModel() {

            ChangeViewCommand = new RelayCommand<string>(
                (p) => true, // CanExecute luôn trả về true
                (p) => ChangeView(p) // Logic thay đổi trang
            );
            CurrentView = new HomePage();

            LoadedWindowCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                isLoaded = true;
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();
            });
        }

        private void ChangeView(string viewName)
        {
            switch (viewName)
            {
                case "HomePage":
                    CurrentView = new HomePage();
                    break;
                case "CoSoChanNuoi":
                    CurrentView = new CoSoChanNuoi();
                    break;
                case "CoSoCheBien":
                    CurrentView = new CoSoCheBien();
                    break;
                case "TamGiuTieuHuy":
                    CurrentView = new TamGiuTieuHuy();
                    break;
                case "XuLyChatThai":
                    CurrentView = new XuLyChatThai();
                    break;
                case "CoSoKhaoNghiem":
                    CurrentView = new CoSoKhaoNghiem();
                    break;
                case "DaiLyBanThuoc":
                    CurrentView = new DaiLyBanThuoc();
                    break;
                case "CoSoGietMo":
                    CurrentView = new CoSoGietMo();
                    break;
                case "KhuVucTieuHuy":
                    CurrentView = new KhuVucTieuHuy();
                    break;
                case "ToChucVaVung":
                    CurrentView = new ToChucVaVung();
                    break;
                case "QuanLyDongVat":
                    CurrentView = new QuanLyDongVat();
                    break;
                case "QuanLyDich":
                    CurrentView = new QuanLyDich();
                    break;
                default:
                    break;
            }
        }
    }
}
