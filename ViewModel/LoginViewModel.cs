using KTPMUDMVVM.Model;
using Microsoft.Expression.Interactivity.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace KTPMUDMVVM.ViewModel
{
    class LoginViewModel: BaseViewModel
    {
        public bool Islogin { get; set; }
        public ICommand LoginCommand { get; set; }
        private string _Username { get; set; }
        public string Username {
            get => _Username; 
            set 
            {
                _Username = value;
                OnPropertyChangedEventHandler();
            }
        }
        private string _Password { get; set; }
        public string Password { 
            get => _Password;
            set
            {
                _Password = value;
                OnPropertyChangedEventHandler();
            } 
        }

        public LoginViewModel() 
        {
            Islogin = false;
            LoginCommand = new RelayCommand<object>(
                (p) => 
                {
                    return true; 
                },
                (p) => 
                {
                    if (Username == null || Password == null)
                    {
                        MessageBox.Show("Chua dien du thong tin can thiet!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    if (DataProvide.Ins.DB.Nguoi_dung.Any(x => x.MatKhau == Password) && DataProvide.Ins.DB.Nguoi_dung.Any(x => x.TenDN == Username))
                    {
                        MainWindow ph = new MainWindow();
                        ph.Show();
                        
                    }
                    else
                    {
                        Username = null;
                        Password = null;
                        MessageBox.Show("Khong dung thong tin\nVui Long nhap lai", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        
                    }
                    return;
                });
        }
    }
}
