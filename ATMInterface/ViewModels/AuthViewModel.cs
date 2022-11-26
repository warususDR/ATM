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
    class AuthViewModel : INotifyPropertyChanged
    {
        private Action _goToMain;

        private RelayCommand<object> _authorizeCommand;
        private RelayCommand<object> _cancelCommand;

        private bool CanExecuteAuthorize(Object obj)
        {
            return true; // validation here
        }

        public AuthViewModel(Action goToMain) => _goToMain = goToMain;

        public void GoToMain()
        {
            _goToMain.Invoke();
        }

        public RelayCommand<object> AuthorizeCommand
        {
            get
            {
                return _authorizeCommand ??= new RelayCommand<object>(_ => GoToMain(), CanExecuteAuthorize);
            }
        }

        public RelayCommand<object> CancelCommand
        {
            get
            {
                return _cancelCommand ??= new RelayCommand<object>(_ => Environment.Exit(0), CanExecuteCancel);
            }
        }

        private bool CanExecuteCancel(Object obj)
        {
            return true; // validation here
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
