﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BillAccounter
{
    class Routes
    {
        public static void OpenNewWindow(bool OpWin)
        {
            Route<NewWindow, NewWindowViewModel>(OpWin);
        }

        protected static void Route<TWindow, TViewModel>(bool OpWin)
            where TWindow : Window, new()
            where TViewModel : class
        {
            if (OpWin == true)
            {
                //REVIEW: Тут может быть ашипка
                //ммм.. Необработанное исключение?
                TWindow w = new TWindow();
                TViewModel vm = Activator.CreateInstance(typeof(TViewModel)) as TViewModel;
                w.Show();
                w.DataContext = vm;
            }
            else Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive).Close();
        }
    }
}
