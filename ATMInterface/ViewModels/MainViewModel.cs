using ATMInterface.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ATMInterface.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {
        private Action _goToAuth;
        private Action _goToTransfer;
        private Action _goToAdd;
        private Action _goToWithdraw;
        private Action _goToDetailedInfo;

        private RelayCommand<object> _transferCommand;
        private RelayCommand<object> _addCommand;
        private RelayCommand<object> _withdrawCommand;
        private RelayCommand<object> _detailedInfoCommand;
        private RelayCommand<object> _exitCommand;

        private bool CanExecuteTransfer(Object obj)
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
        private bool CanExecuteDetailedInfo(Object obj)
        {
            return true; // validation here
        }
        private bool CanExecuteExit(Object obj)
        {
            return true; // validation here
        }

        public MainViewModel(Action goToAuth, Action goToTransfer, Action goToAdd, Action goToWithdraw, Action goToDetailedInfo)
        {
            _goToAuth = goToAuth;
            _goToTransfer = goToTransfer;
            _goToAdd = goToAdd;
            _goToWithdraw = goToWithdraw;
            _goToDetailedInfo = goToDetailedInfo;
        }

        public void GoToAuth()
        {
            _goToAuth.Invoke();
        }

        public void GoToTransfer()
        {
            _goToTransfer.Invoke();
        }

        public void GoToAdd()
        {
            _goToAdd.Invoke();
        }

        public void GoToWithdraw()
        {
            _goToWithdraw.Invoke();
        }

        public void GoToDetailedInfo()
        {
            _goToDetailedInfo.Invoke();
        }

        public RelayCommand<object> TransferCommand
        {
            get
            {
                return _transferCommand ??= new RelayCommand<object>(_ => GoToTransfer(), CanExecuteTransfer);
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

        public RelayCommand<object> DetailedInfoCommand
        {
            get
            {
                return _detailedInfoCommand ??= new RelayCommand<object>(_ => GoToDetailedInfo(), CanExecuteDetailedInfo);
            }
        }

        public RelayCommand<object> ExitCommand
        {
            get
            {
                return _exitCommand ??= new RelayCommand<object>(_ => GoToAuth(), CanExecuteExit);
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
