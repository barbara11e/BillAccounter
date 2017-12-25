using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Contracts;
using BL;

namespace BillAccounter
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public ICommand OpenNewWindowCommand { get; private set; }
        public ICommand CloseNewWindowCommand { get; private set; }
        public ICommand UpdateDataCommand { get; private set; }
        public Double CurrentBill{ get; set; }
        public String TypeName { get; set; }
        public double Amount { get; set; }
        public string Category { get; set; }
        public double CurrentDollar { get; set; }
        public List<string> BillType { get; set; }
        private string _selectedBillType;
        public string SelectedBillType
        {
            get { return _selectedBillType; }
            set { _selectedBillType= value; }
        }
        private ICommand _command;
        public ICommand GenerateFile
        {
            get
            {
                return _command ?? (_command = new CommandHandler(() => MainLogic.GenerateExcel(), _canExecute));
            }
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

         public MainWindowViewModel()
        {
            CloseNewWindowCommand = new RouteCommand(CloseNewWindow);
            OpenNewWindowCommand = new RouteCommand(OpenNewWindow);
            Bills = new ObservableCollection<BillViewModel>();
            MainLogic ml = new MainLogic();
            CurrentBill = ml.GetDataFromDataBase();
            CurrentDollar = 58.9;
        }

        private void CloseNewWindow(Object p)
        {
            Routes.OpenNewWindow(false);
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

        private void OpenNewWindow(Object p)
        {
            Routes.OpenNewWindow(true);
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
