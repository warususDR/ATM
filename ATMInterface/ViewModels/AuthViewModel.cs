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

        private bool CanExecuteExit(Object obj)
        {
            return true; // validation here
        }

        private void ExecuteAuthorize()
        {
            //CurrentATM.Engine.OnNewSession();
            if(CurrentATM.Engine.OnUserInput(eUserAction.CREDIT_CARD_INSERTED, UserInput) == 1)
                GoToPinEnter();
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
                return _exitCommand ??= new RelayCommand<object>(_ => Environment.Exit(0), CanExecuteExit);
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
