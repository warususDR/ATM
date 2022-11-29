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
        private string _cardNumber;
        private string _cardBalance;
        private string _cardCreditBalance;
        private string _cardOwner;
        private string _cardBank;
        private string _cardExpiryDate;

        private Action _goToMain;

        private RelayCommand<object> _exitCommand;

        private bool CanExecuteExit(Object obj)
        {
            return true; // validation here
        }

        public string CardNumber
        {
            get { return _cardNumber; }
            set
            {
                _cardNumber = value;
                OnPropertyChanged();
            }
        }

        public string CardBalance
        {
            get { return _cardBalance; }
            set
            {
                _cardBalance = value;
                OnPropertyChanged();
            }
        }

        public string CardCreditBalance
        {
            get { return _cardCreditBalance; }
            set
            {
                _cardCreditBalance = value;
                OnPropertyChanged();
            }
        }

        public string CardOwner
        {
            get { return _cardOwner; }
            set
            {
                _cardOwner = value;
                OnPropertyChanged();
            }
        }

        public string CardBank
        {
            get { return _cardBank; }
            set
            {
                _cardBank = value;
                OnPropertyChanged();
            }
        }

        public string CardExpiryDate
        {
            get { return _cardExpiryDate; }
            set
            {
                _cardExpiryDate = value;
                OnPropertyChanged();
            }
        }

        public DetailedInfoViewModel(Action goToMain)
        {
            _goToMain = goToMain;

            //debug

            CardNumber = "00000000";
            CardBalance = "1000$";
            CardCreditBalance = "100$";
            CardOwner = "Volodymyr Bublik";
            CardBank = "C PLUS PLUS Bank";
            CardExpiryDate = "05.12.2022";

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

        // onPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
