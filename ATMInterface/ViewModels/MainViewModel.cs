using ATM;
using ATMInterface.Tools;
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

        private bool CanExecuteCheckBalance(Object obj)
        {
            return true; // validation here
        }
        private bool CanExecutePrintBalance(Object obj)
        {
            return true; // validation here
        }

        private bool CanExecuteAdd(Object obj)
        {
            return true; // validation here
        }

        private bool CanExecuteWithdraw(Object obj)
        {
            return true; // validation here
        }
        private bool CanExecuteExit(Object obj)
        {
            return true; // validation here
        }

        private void ExecutePrintBalance()
        {

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
                //Properly close session
                GoToAuth();
            }
        }

        public MainViewModel(Action goToAuth, Action goToAdd, Action goToWithdraw, Action goToCheckBalance)
        {
            _goToAuth = goToAuth;
            _goToAdd = goToAdd;
            _goToWithdraw = goToWithdraw;
            _goToCheckBalance = goToCheckBalance;
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
                return _checkBalanceCommand ??= new RelayCommand<object>(_ => ExecuteCheckBalance(), CanExecuteCheckBalance);
            }
        }
        public RelayCommand<object> PrintBalanceCommand
        {
            get
            {
                return _printBalanceCommand ??= new RelayCommand<object>(_ => ExecutePrintBalance(), CanExecutePrintBalance);
            }
        }

        public RelayCommand<object> AddCommand
        {
            get
            {
                return _addCommand ??= new RelayCommand<object>(_ => GoToAdd(), CanExecuteAdd);
            }
        }

        public RelayCommand<object> WithdrawCommand
        {
            get
            {
                return _withdrawCommand ??= new RelayCommand<object>(_ => GoToWithdraw(), CanExecuteWithdraw);
            }
        }

        public RelayCommand<object> ExitCommand
        {
            get
            {
                return _exitCommand ??= new RelayCommand<object>(_ => ExecuteExit(), CanExecuteExit);
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
