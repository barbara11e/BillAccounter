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
        public int MaxNumb { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Int16 i = 0;
            try
            {
                i = Convert.ToInt16(value);
            }
            catch (Exception ex)
            {
                return new ValidationResult(false, "Введите число!!");
            }
            if (i > MaxNumb || i <= 0)
                return new ValidationResult(false, "Сумма слишком большая!");
            else
                return new ValidationResult(true, String.Empty);
        }
    }
}
