using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BillAccounter
{

    public class MyValidationRule : ValidationRule
    {
        public double MaxNumb { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            double i = 0;
            try
            {
                i = Convert.ToDouble(value);
            }
            catch (Exception ex)
            {
                return new ValidationResult(false, "Введите число!!");
            }
            if (i > MaxNumb)
                return new ValidationResult(false, "Сумма слишком большая!");
            else
                return new ValidationResult(true, String.Empty);
        }
    }
}
