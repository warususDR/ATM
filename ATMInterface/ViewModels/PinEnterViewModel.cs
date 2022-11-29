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
    class PinEnterViewModel : INotifyPropertyChanged
    {
        private string _userInput;

        private Action _goToMain;
        private Action _goToAuth;

        private RelayCommand<object> _acceptPinCommand;
        private RelayCommand<object> _cancelCommand;

        private bool CanExecuteAcceptPin(Object obj)
        {
            return true; // validation here
        }

        private bool CanExecuteCancel(Object obj)
        {
            return true; // validation here
        }

        private void ExecuteAcceptPin()
        {
            // onUserInput call here
            if(UserInput == "1") GoToMain(); //debug
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

        public PinEnterViewModel(Action goToMain, Action goToAuth)
        {
            _goToMain = goToMain;
            _goToAuth = goToAuth;
        }

        public void GoToMain()
        {
            _goToMain.Invoke();
        }

        public void GoToAuth()
        {
            _goToAuth.Invoke();
        }

        public RelayCommand<object> AcceptPinCommand
        {
            get
            {
                return _acceptPinCommand ??= new RelayCommand<object>(_ => ExecuteAcceptPin(), CanExecuteAcceptPin);
            }
        }

        public RelayCommand<object> CancelCommand
        {
            get
            {
                return _cancelCommand ??= new RelayCommand<object>(_ => GoToAuth(), CanExecuteCancel);
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
