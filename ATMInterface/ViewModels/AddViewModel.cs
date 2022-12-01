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
    class AddViewModel : INotifyPropertyChanged
    {
        private string _userInput;

        private Action _goToMain;

        private RelayCommand<object> _cancelCommand;
        private RelayCommand<object> _addCommand;

        private bool CanExecuteCancel(Object obj)
        {
            return true; // validation here
        }

        private bool CanExecuteAdd(Object obj)
        {
            return Validation.HasCurrencyFormat(UserInput);
        }

        private void ExecuteAdd()
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

        public AddViewModel(Action goToMain) => _goToMain = goToMain;

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

        public RelayCommand<object> AddCommand
        {
            get
            {
                return _addCommand ??= new RelayCommand<object>(_ => ExecuteAdd(), CanExecuteAdd);
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
