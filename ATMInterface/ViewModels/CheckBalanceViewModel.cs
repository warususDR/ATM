using ATM;
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
    internal class CheckBalanceViewModel
    {
        private Action _goToMain;

        private string _balance;

        private RelayCommand<object> _exitCommand;
        public eATM CurrentATM { get; set; }

        private bool CanExecuteExit(Object obj)
        {
            return true;
        }
        public CheckBalanceViewModel(Action goToMain, eATM currentATM)
        {
            _goToMain = goToMain;
            CurrentATM = currentATM;
            Balance = CurrentATM.Engine.OnUserInput(eUserAction.CHECK_BALANCE).Item2;
        }

        public void GoToMain()
        {
            _goToMain.Invoke();
        }

        public RelayCommand<object> ExitCommand
        {
            get
            {
                return _exitCommand ??= new RelayCommand<object>(_ => GoToMain(), CanExecuteExit);
            }
        }

        public string Balance 
        { get => _balance; 
          set
            {
                _balance = value;
                OnPropertyChanged();
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
