using ATMInterface.Tools;
using ATMInterface.Tools.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ATMInterface.ViewModels
{
    class TransferAmountViewModel : INotifyPropertyChanged
    {
        private string _userInput;

        private Action _goToTransfer;
        private Action _goToMain;

        private RelayCommand<object> _cancelCommand;
        private RelayCommand<object> _transferCommand;

        private bool CanExecuteCancel(Object obj)
        {
            return true; // validation here
        }

        private bool CanExecuteTransfer(Object obj)
        {
            return Validation.HasCurrencyFormat(UserInput);
        }

        private void ExecuteTransfer()
        {
            if (UserInput == "100,0") GoToMain();
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

        public TransferAmountViewModel(Action goToTransfer, Action goToMain)
        {
            _goToTransfer = goToTransfer;
            _goToMain = goToMain;
        }

        public void GoToTransfer()
        {
            _goToTransfer.Invoke();
        }

        public void  GoToMain()
        {
            _goToMain.Invoke();
        }
        public RelayCommand<object> CancelCommand
        {
            get
            {
                return _cancelCommand ??= new RelayCommand<object>(_ => GoToTransfer(), CanExecuteCancel);
            }
        }

        public RelayCommand<object> TransferCommand
        {
            get
            {
                return _transferCommand ??= new RelayCommand<object>(_ => ExecuteTransfer(), CanExecuteTransfer);
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
