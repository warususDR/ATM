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

        private Action _goToMain;

        private RelayCommand<object> _cancelCommand;
        private RelayCommand<object> _withdrawCommand;
        public eATM CurrentATM { get; set; }

        private bool CanExecuteCancel(Object obj)
        {
            return true; // validation here
        }

        private bool CanExecuteWithdraw(Object obj)
        {
            return Validation.HasCurrencyFormat(UserInput);
        }

        private void ExecuteWithdraw()
        {
            int actionSuccess = CurrentATM.Engine.OnUserInput(eUserAction.GET_CASH, UserInput);
            if (actionSuccess == 1)
            {
                GoToMain();
                MessageBox.Show("Successfully withdrew cash!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
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

        public WithdrawViewModel(Action goToMain, eATM atm)
        {
            _goToMain = goToMain;
            CurrentATM = atm;
        }

            public void GoToMain()
        {
            _goToMain.Invoke();
        }

        public RelayCommand<object> CancelCommand
        {
            get
            {
                return _cancelCommand ??= new RelayCommand<object>(_ => GoToMain(), CanExecuteCancel);
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
