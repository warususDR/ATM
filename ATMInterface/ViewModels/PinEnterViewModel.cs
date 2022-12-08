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
    class PinEnterViewModel : INotifyPropertyChanged
    {
        private string _userInput;

        private Action _goToMain;
        private Action _goToAuth;

        private RelayCommand<object> _acceptPinCommand;
        private RelayCommand<object> _cancelCommand;

        public eATM CurrentATM { get; set; }

        private bool CanExecuteAcceptPin(Object obj)
        {
            return Validation.HasPinFormat(UserInput);
        }

        private void ExecuteAcceptPin()
        {
            int actionSuccess = CurrentATM.Engine.OnUserInput(eUserAction.PASSWORD_ENTERED, UserInput);
            if (actionSuccess == 1) GoToMain();
            else if (actionSuccess == 0)
            {
                string msg = "Incorrect Pin Entered!";
                MessageBox.Show(msg, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if(actionSuccess == -1)
            {
                string msg = "0 password input attempts left!";
                MessageBox.Show(msg, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                CurrentATM.Engine.SessionIsOver();
                GoToAuth();
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

        public PinEnterViewModel(Action goToMain, Action goToAuth, eATM atm)
        {
            _goToMain = goToMain;
            _goToAuth = goToAuth;
            CurrentATM = atm;
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
                return _cancelCommand ??= new RelayCommand<object>(_ => GoToAuth(), Validation.AlwaysExecute);
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
