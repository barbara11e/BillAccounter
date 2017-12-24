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
    
/// <summary>
/// FOR REVIEW:   Добавила, где требовалось enum 
/// </summary>
    enum BillTypeEnum { Доход =1, Расход =2, План=3};
    enum CategoryEnum {Наличные, Кредитка, Дополнительный};
    enum CurrencyEnum { USD, Руб};

    class NewWindowViewModel : INotifyPropertyChanged
    {
        private DateTime _thisDate = DateTime.Today;
        public String ThisDate
        {
            get { return _thisDate.ToString("d"); }
        }

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

        public List<string> BillType
        {
            get { return Enum.GetNames(typeof(BillTypeEnum)).ToList(); }
        }
        public List<string> Category
        {
            get
            { return Enum.GetNames(typeof(CategoryEnum)).ToList(); }
        }
        public List<string> Currency
        {
            get
            { return Enum.GetNames(typeof(CurrencyEnum)).ToList(); }
        }

        public String TypeName { get; set; }
        public double Amount { get; set; }
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

        public ICommand CloseNewWindowCommand { get; private set; }
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
