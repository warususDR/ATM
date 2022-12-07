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
    class AddViewModel : INotifyPropertyChanged
    {
        private string _userInput;

        private Action _goToMain;

        private RelayCommand<object> _cancelCommand;
        private RelayCommand<object> _addCommand;
        public eATM CurrentATM { get; set; }

        private bool CanExecuteAdd(Object obj)
        {
            return Validation.HasCurrencyFormat(UserInput);
        }

        private void ExecuteAdd()
        {
            int actionSuccess = CurrentATM.Engine.OnUserInput(eUserAction.PUT_CASH, UserInput);
            if (actionSuccess == 1)
            {
                MessageBox.Show("Successfully added cash!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                GoToMain();
            }
            else if (actionSuccess == 0)
            {
                string msg = "Couldn't add cash!";
                MessageBox.Show(msg, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (actionSuccess == -1)
            {
                string msg = "Error Occured!";
                MessageBox.Show(msg, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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

        public AddViewModel(Action goToMain, eATM atm)
        {
            _goToMain = goToMain;
            CurrentATM = atm;
        }

        public void GoToMain()
        {
            _goToMain.Invoke();
        }

        public RelayCommand<object> CancelCommand
        {
            get
            {
                return _cancelCommand ??= new RelayCommand<object>(_ => GoToMain(), Validation.AlwaysExecute);
            }
        }

        public RelayCommand<object> AddCommand
        {
            get
            {
                return _addCommand ??= new RelayCommand<object>(_ => ExecuteAdd(), CanExecuteAdd);
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
