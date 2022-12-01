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
    class TransferViewModel : INotifyPropertyChanged
    {
        private string _userInput;

        private Action _goToMain;
        private Action _goToTransferAmount;

        private RelayCommand<object> _cancelCommand;
        private RelayCommand<object> _acceptReceiverCommand;

        private bool CanExecuteCancel(Object obj)
        {
            return true; // validation here
        }

        private bool CanExecuteAcceptReceiver(Object obj)
        {
            return Validation.HasCreditCardNumberFormat(UserInput);
        }

        private void ExecuteAcceptReceiver()
        {
            if (UserInput == "1111222233334444") GoToTransferAmount();
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

        public TransferViewModel(Action goToMain, Action goToTransferAmount)
        {
            _goToMain = goToMain;
            _goToTransferAmount = goToTransferAmount;
        }
        

        public void GoToMain()
        {
            _goToMain.Invoke();
        }

        public void GoToTransferAmount()
        {
            _goToTransferAmount.Invoke();
        }

        public RelayCommand<object> CancelCommand
        {
            get
            {
                return _cancelCommand ??= new RelayCommand<object>(_ => GoToMain(), CanExecuteCancel);
            }
        }

        public RelayCommand<object> AcceptReceiverCommand
        {
            get
            {
                return _acceptReceiverCommand ??= new RelayCommand<object>(_ => ExecuteAcceptReceiver(), CanExecuteAcceptReceiver);
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
