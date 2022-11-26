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
    class WithdrawViewModel : INotifyPropertyChanged
    {
        private Action _goToMain;

        private RelayCommand<object> _cancelCommand;

        private bool CanExecuteCancel(Object obj)
        {
            return true; // validation here
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

        // onPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
