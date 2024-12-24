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
    /// Interaction logic for CoSoCheBien.xaml
    /// </summary>
    public partial class CoSoCheBien : UserControl
    {
        private readonly MainViewModel _viewModel;

        public CoSoCheBien(MainViewModel viewModel)
        {
            
            InitializeComponent();
        }

        public void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (_viewModel.SelectedItem != null)
            {
                // Điều hướng sang trang chi tiết
                _viewModel.ChangeViewCommand.Execute("Show");
            }
        }
    }
}
