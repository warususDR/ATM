using ATM;
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

		private eCommutator commutator;
		private eATM currentATM;
		private ePaymentSystem ps;
		private ePrivatBank pb;

		public MainWindow()
        {
            InitializeComponent();
            InitATM();
            GoToAuth();
        }

        #region Navigation
        public void GoToAuth()
        {
            Content = new AuthView(GoToPinEnter, currentATM);
        }

        public void GoToPinEnter()
        {
            Content = new PinEnterView(GoToMain, GoToAuth, currentATM);
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
            Content = new AddView(GoToMain, currentATM);
        }

        public void GoToWithdraw()
        {
            Content = new WithdrawView(GoToMain, currentATM);
        }
        public void GoToTransfer()
        {
            Content = new TransferView(GoToMain, GoToTransferAmount);
        }
        public void GoToTransferAmount()
        {
            Content = new TransferAmountView(GoToTransfer, GoToMain);
        }
        #endregion

        #region InitATM
        public void InitATM()
        {
			commutator = new eCommutator();
			pb = new ePrivatBank(commutator);
			currentATM = new eATM(commutator, pb);
			ps = new ePaymentSystem(commutator);
		}
		#endregion
	}
}
