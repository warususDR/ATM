using ATMInterface.Views;
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

namespace ATMInterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GoToAuth();
        }

        #region Navigation
        public void GoToAuth()
        {
            Content = new AuthView(GoToMain);
        }
        public void GoToMain()
        {
            Content = new MainView(GoToAuth, GoToTransfer, GoToAdd, GoToWithdraw, GoToDetailedInfo);
        }

        public void GoToDetailedInfo()
        {
            Content = new DetailedInfoView(GoToMain);
        }

        public void GoToAdd()
        {
            Content = new AddView(GoToMain);
        }

        public void GoToWithdraw()
        {
            Content = new WithdrawView(GoToMain);
        }
        public void GoToTransfer()
        {
            Content = new TransferView(GoToMain);
        }
        #endregion
    }
}
