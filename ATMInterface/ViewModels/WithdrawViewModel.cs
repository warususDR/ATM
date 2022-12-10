using ATM;
using ATMInterface.Tools;
using ATMInterface.Tools.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ATMInterface.ViewModels
{
    class WithdrawViewModel : INotifyPropertyChanged
    {
        private string _userInput;
        private string _comission;
        private Visibility _comissionVisibility;

        private Action _goToMain;

        private RelayCommand<object> _cancelCommand;
        private RelayCommand<object> _withdrawCommand;
        public eATM CurrentATM { get; set; }

        private bool CanExecuteWithdraw(Object obj)
        {
            bool isValid = Validation.HasCurrencyFormat(UserInput);
            if (isValid)
            {
                var comissionPercentage = ((iBank)CurrentATM.Engine.BankAcquire).GetComission();
                Comission = comissionPercentage.ToString() + "% = " + (Int32.Parse(UserInput) * comissionPercentage / 100).ToString() + "$";
                ComissionVisibility = Visibility.Visible;
            }
            else
            {
                ComissionVisibility = Visibility.Hidden;
            }
            return isValid;
        }

        private void ExecuteWithdraw()
        {
            int actionSuccess = CurrentATM.Engine.OnUserInput(eUserAction.GET_CASH, UserInput);
            UserInput = "";
            if (actionSuccess == 1)
            {
                GoToMain();
                MessageBox.Show("Successfully withdrew cash!", "ATM", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (actionSuccess == 0)
            {
                string msg = "Couldn't withdraw cash!";
                MessageBox.Show(msg, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (actionSuccess == -1)
            {
                string msg = "Error Occured!";
                MessageBox.Show(msg, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public string UserInput
        {
            get { return _userInput; }
            set
            {
                _userInput = value;
                OnPropertyChanged();
            }
        }

        public string Comission
        {
            get { return _comission; }
            set
            {
                _comission = value;
                OnPropertyChanged();
            }
        }

        public Visibility ComissionVisibility
        {
            get { return _comissionVisibility; }
            set
            {
                _comissionVisibility = value;
                OnPropertyChanged();
            }
        }

        public WithdrawViewModel(Action goToMain, eATM atm)
        {
            _goToMain = goToMain;
            CurrentATM = atm;
            UserInput = "";
            Comission = "";
            ComissionVisibility = Visibility.Hidden;
        }

            public void GoToMain()
        {
            _goToMain.Invoke();
        }

        public RelayCommand<object> CancelCommand
        {
            get
            {
                return _cancelCommand ??= new RelayCommand<object>(_ => GoToMain(), Validation.AlwaysExecute);
            }
        }

        public RelayCommand<object> WithdrawCommand
        {
            get
            {
                return _withdrawCommand ??= new RelayCommand<object>(_ => ExecuteWithdraw(), CanExecuteWithdraw);
            }
        }

        // onPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
