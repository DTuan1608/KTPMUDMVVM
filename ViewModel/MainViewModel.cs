using KTPMUDMVVM.Views;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace KTPMUDMVVM.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public bool IsLoaded { get; private set; } = false;
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand ChangeViewCommand { get; set; }

        private UserControl _currentView;
        public UserControl CurrentView
        {
            get => _currentView;
            set
            {
                if (_currentView != value)
                {
                    _currentView = value;
                    OnPropertyChangedEventHandler(nameof(CurrentView));
                }
            }
        }

        public MainViewModel()
        {
            // Initialize default view
            CurrentView = new HomePage();

            // Command to handle window loaded event
            LoadedWindowCommand = new RelayCommand<Window>(
                canExecute: (p) => true,
                execute: (p) =>
                {
                    IsLoaded = true;
                    if (p == null) return;

                    // Hide main window and show login window
                    p.Hide();
                    var loginWindow = new LoginWindow();
                    loginWindow.ShowDialog();

                    if (loginWindow.DataContext is LoginViewModel loginVM && loginVM.Islogin)
                    {

                        p.Show();
                    }
                    else
                    {
                     
                        p.Close();
                    }
                });

            ChangeViewCommand = new RelayCommand<string>(
                canExecute: (p) => !string.IsNullOrEmpty(p), 
                execute: (p) => ChangeView(p));
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
                    // Default to current view if viewName does not match
                    break;
            }
        }
    }
}
