using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Drink_Wholesale.DTO;

namespace Drink_Wholesale.Admin.ViewModel
{
    public class PackagingDisplayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || !(value is Packaging))
                return Binding.DoNothing; // ha nem megfelelő, nem csinálunk semmit

            if (parameter == null || !(parameter is IList<String>))
                return Binding.DoNothing;

            Packaging packagings = (Packaging)value;
            IList<String> names = parameter as IList<String>;

            var texts = new List<string>();
            if ((packagings & Packaging.ShrinkWrap) > 0)
                texts.Add(names[1]);
            if ((packagings & Packaging.Tray) > 0)
                texts.Add(names[2]);
            if ((packagings & Packaging.Crate) > 0)
                texts.Add(names[3]);
            if (texts.Count == 0)
            {
                texts.Add(names[0]);
            }
            return string.Join(", ", texts);

            //if (value is null || !(value is IList<>))
            //{
            //    throw new NotImplementedException();
            //}
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
