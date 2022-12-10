using ATM;
using ATMInterface.Tools;
using ATMInterface.Tools.Utilities;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace ATMInterface.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {


        private Action _goToAuth;
        private Action _goToAdd;
        private Action _goToWithdraw;
        private Action _goToCheckBalance;

        private RelayCommand<object> _checkBalanceCommand;
        private RelayCommand<object> _printBalanceCommand;
        private RelayCommand<object> _addCommand;
        private RelayCommand<object> _withdrawCommand;
        private RelayCommand<object> _exitCommand;

        public eATM CurrentATM { get; set; }

        private void ExecutePrintBalance()
        {
            PrintBalanceUtility.PrintBalance(CurrentATM.Engine.OnUserInput(eUserAction.PRINT_BALANCE));
        }

        private void ExecuteCheckBalance()
        {
            GoToCheckBalance();
        }

        private void ExecuteExit()
        {
            MessageBoxResult res = MessageBox.Show("Closing your session, are you sure?", "ATM", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (res == MessageBoxResult.OK)
            {
                CurrentATM.Engine.SessionIsOver();
                GoToAuth();
            }
        }

        public MainViewModel(Action goToAuth, Action goToAdd, Action goToWithdraw, Action goToCheckBalance, eATM atm)
        {
            _goToAuth = goToAuth;
            _goToAdd = goToAdd;
            _goToWithdraw = goToWithdraw;
            _goToCheckBalance = goToCheckBalance;
            CurrentATM = atm;
        }

        public void GoToAuth()
        {
            _goToAuth.Invoke();
        }

        public void GoToAdd()
        {
            _goToAdd.Invoke();
        }

        public void GoToWithdraw()
        {
            _goToWithdraw.Invoke();
        }

        public void GoToCheckBalance()
        {
            _goToCheckBalance.Invoke();
        }

        public RelayCommand<object> CheckBalanceCommand
        {
            get
            {
                return _checkBalanceCommand ??= new RelayCommand<object>(_ => ExecuteCheckBalance(), Validation.AlwaysExecute);
            }
        }
        public RelayCommand<object> PrintBalanceCommand
        {
            get
            {
                return _printBalanceCommand ??= new RelayCommand<object>(_ => ExecutePrintBalance(), Validation.AlwaysExecute);
            }
        }

        public RelayCommand<object> AddCommand
        {
            get
            {
                return _addCommand ??= new RelayCommand<object>(_ => GoToAdd(), Validation.AlwaysExecute);
            }
        }

        public RelayCommand<object> WithdrawCommand
        {
            get
            {
                return _withdrawCommand ??= new RelayCommand<object>(_ => GoToWithdraw(), Validation.AlwaysExecute);
            }
        }

        public RelayCommand<object> ExitCommand
        {
            get
            {
                return _exitCommand ??= new RelayCommand<object>(_ => ExecuteExit(), Validation.AlwaysExecute);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
