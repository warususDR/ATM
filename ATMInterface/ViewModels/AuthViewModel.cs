using ATM;
using ATMInterface.Tools;
using ATMInterface.Tools.Utilities;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace ATMInterface.ViewModels
{
    class AuthViewModel : INotifyPropertyChanged
    {
        private string _userInput;

        private Action _goToPinEnter;
		public eATM CurrentATM { get; set; }

        private RelayCommand<object> _authorizeCommand;
        private RelayCommand<object> _exitCommand;

        private bool CanExecuteAuthorize(Object obj)
        {
            return Validation.HasCreditCardNumberFormat(UserInput);
        }

        private void ExecuteAuthorize()
        {
            CurrentATM.Engine.OnNewSession();
            int actionSuccess = CurrentATM.Engine.OnUserInput(eUserAction.CREDIT_CARD_INSERTED, UserInput);
            UserInput = "";
            if (actionSuccess == 1)
            {
                GoToPinEnter();
            }
            else if (actionSuccess == 0)
            {
                string msg = "Couldn't find card with this number!";
                MessageBox.Show(msg, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (actionSuccess == -1)
            {
                string msg = "Error Occured!";
                MessageBox.Show(msg, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExecuteExit()
        {
            string msg = "Are you sure you want to exit???";
            MessageBoxResult result = MessageBox.Show(msg, "ATM", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                Environment.Exit(0);
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

        public AuthViewModel(Action goToPinEnter, eATM currentATM)
        {
            _goToPinEnter = goToPinEnter;
            CurrentATM = currentATM;
            UserInput = "";
        }

        public void GoToPinEnter()
        {
            _goToPinEnter.Invoke();
        }

        public RelayCommand<object> AuthorizeCommand
        {
            get
            {
                return _authorizeCommand ??= new RelayCommand<object>(_ => ExecuteAuthorize(), CanExecuteAuthorize);
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
