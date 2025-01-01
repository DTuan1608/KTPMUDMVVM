using KTPMUDMVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KTPMUDMVVM.Views
{
    /// <summary>
    /// Interaction logic for DaiLyBanThuoc.xaml
    /// </summary>
    public partial class DaiLyBanThuoc : UserControl
    {
        public DaiLyBanThuoc()
        {
            InitializeComponent();
            this.DataContext = new DLBTViewModel();
        }
    }
}
