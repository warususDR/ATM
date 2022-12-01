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
            // authorize OnUserInput call
            if (UserInput == "1111222233334444") GoToPinEnter(); // debug 
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

        public AuthViewModel(Action goToPinEnter) => _goToPinEnter = goToPinEnter;

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
