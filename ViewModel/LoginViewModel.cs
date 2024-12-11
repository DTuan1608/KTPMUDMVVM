using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KTPMUDMVVM.ViewModel
{
    class LoginViewModel: BaseViewModel
    {
        public bool Islogin {  get; set; }  
        
        public ICommand LoginCommand { get; set; }

        public LoginViewModel() 
        {
            Islogin = false;
            LoginCommand = new RelayCommand<object>((p) => { return true; }, (p) => { Islogin = true; });
        }
    }
}
