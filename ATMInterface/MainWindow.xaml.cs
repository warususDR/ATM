using ATM;
using ATMInterface.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
		private eMonobank mb;

        private void Killing_App(object sender, CancelEventArgs e)
        {
            string msg = "Are you sure you want to exit???";
            MessageBoxResult result = MessageBox.Show(msg,"ATM", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
            
        }


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
            Content = new MainView(GoToAuth, GoToAdd, GoToWithdraw, GoToCheckBalance, currentATM);
        }
        public void GoToAdd()
        {
            Content = new AddView(GoToMain, currentATM);
        }

        public void GoToWithdraw()
        {
            Content = new WithdrawView(GoToMain, currentATM);
        }

        public void GoToCheckBalance()
        {
            Content = new CheckBalanceView(GoToMain, currentATM);
        }
        #endregion

        #region InitATM
        public void InitATM()
        {
			commutator = new eCommutator();
			mb = new eMonobank(commutator);
			currentATM = new eATM(commutator, mb);
			ps = new ePaymentSystem(commutator);
		}
		#endregion
	}
}
