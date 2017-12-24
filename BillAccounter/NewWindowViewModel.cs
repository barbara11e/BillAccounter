using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BillAccounter
{
    class NewWindowViewModel : INotifyPropertyChanged
    {
        public ICommand CloseNewWindowCommand { get; private set; }
        public List<string> Currency { get; set; }
        private DateTime _thisDate = DateTime.Today;
        public String ThisDate
        {
            get { return _thisDate.ToString("d"); }
        }
        public String TypeName { get; set; }
        public double Amount { get; set; }
        public List<string> Category { get; set; }
        public List<string> BillType { get; set; }
        private string _selectedBillType;
        public string SelectedBillType
        {
            get { return _selectedBillType; }
            set { _selectedBillType = value; }
        }
        private string _selectedCategory;
        public string SelectedCategory
        {
            get { return _selectedCategory; }
            set { _selectedCategory = value; }
        }

        private bool _canExecute;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        public NewWindowViewModel()
        {
            CloseNewWindowCommand = new RouteCommand(CloseNewWindow);
            BillType = new List<string>()
            {
                "Доход",
                "Расход",
                "В планах"
            };
            Category = new List<string>
            {
                "Наличные",
                "Банковская карта",
                "Дополнительный"

            };
            Currency = new List<string>()
            {
                "$", "Руб."
            };
            Bills = new ObservableCollection<BillViewModel>();
            _canExecute = true;
        }

        public ObservableCollection<BillViewModel> Bills { get; set; }
        protected void RaisePropertyChanged(string propertyName)
        {
            var ev = PropertyChanged;
            if (ev != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private ICommand _clickCommand;
        public ICommand Cancel { get; set; }
        public ICommand Save
        {
            get
            {
                return _clickCommand ?? (_clickCommand = new CommandHandler(() => Bill.SetTypesToDataBase("Bill", SelectedCategory, SelectedBillType, Amount, ThisDate), _canExecute));
            }
        }

        private void CloseNewWindow(Object p)
        {
            Routes.OpenNewWindow(false);
        }

        public class CommandHandler : ICommand
        {
            private Action _action;
            private bool _canExecute;
            public CommandHandler(Action action, bool canExecute)
            {
                _action = action;
                _canExecute = canExecute;
            }
            public bool CanExecute(object parameter)
            {
                return _canExecute;
            }
            public event EventHandler CanExecuteChanged;
            public void Execute(object parameter)
            {
                _action();
            }
        }
    }
}
