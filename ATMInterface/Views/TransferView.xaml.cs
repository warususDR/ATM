using ATMInterface.ViewModels;
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

namespace ATMInterface.Views
{
    /// <summary>
    /// Логика взаимодействия для TransferView.xaml
    /// </summary>
    public partial class TransferView : UserControl
    {
        TransferViewModel _viewmodel;
        public TransferView(Action goToMain, Action goToTransferAmount)
        {
            InitializeComponent();
            DataContext = _viewmodel = new TransferViewModel(goToMain, goToTransferAmount);
        }
    }
}
