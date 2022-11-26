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
    class DetailedInfoViewModel : INotifyPropertyChanged
    {
        private Action _goToMain;

        private RelayCommand<object> _exitCommand;

        private bool CanExecuteExit(Object obj)
        {
            return true; // validation here
        }

        public DetailedInfoViewModel(Action goToMain) => _goToMain = goToMain;

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

        // onPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
