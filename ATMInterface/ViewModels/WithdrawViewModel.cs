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
    class WithdrawViewModel : INotifyPropertyChanged
    {
        private string _userInput;

        private Action _goToMain;

        private RelayCommand<object> _cancelCommand;
        private RelayCommand<object> _withdrawCommand;

        private bool CanExecuteCancel(Object obj)
        {
            return true; // validation here
        }

        private bool CanExecuteWithdraw(Object obj)
        {
            return Validation.HasCurrencyFormat(UserInput);
        }

        private void ExecuteWithdraw()
        {
            //OnUserInputCall here
            if (UserInput == "100,0") GoToMain(); //debug
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

        public WithdrawViewModel(Action goToMain) => _goToMain = goToMain;

        public void GoToMain()
        {
            _goToMain.Invoke();
        }

        public RelayCommand<object> CancelCommand
        {
            get
            {
                return _cancelCommand ??= new RelayCommand<object>(_ => GoToMain(), CanExecuteCancel);
            }
        }

        public RelayCommand<object> WithdrawCommand
        {
            get
            {
                return _withdrawCommand ??= new RelayCommand<object>(_ => ExecuteWithdraw(), CanExecuteWithdraw);
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
