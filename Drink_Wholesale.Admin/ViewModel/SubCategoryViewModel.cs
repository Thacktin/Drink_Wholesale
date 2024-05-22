using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Xml.Linq;
using ELTE.TodoList.Desktop.ViewModel;

namespace Drink_Wholesale.Admin.ViewModel
{
    public class SubCategoryViewModel : ViewModelBase, IEditableObject
    {
        private int _id;
        private String _name;
        private Boolean _isDirty = false;
        private SubCategoryViewModel _backup;

        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        public String Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }


        public event EventHandler EditEnded;

        public void BeginEdit()
        {
            if (!_isDirty)
            {
                _backup = (SubCategoryViewModel)this.MemberwiseClone();
                _isDirty = true;
            }
        }

        public void CancelEdit()
        {
            if (_isDirty)
            {
                Id = _backup.Id;
                Name = _backup.Name;
                _isDirty = false;
                _backup = null;
            }
        }

        public void EndEdit()
        {
            if (_isDirty)
            {
                EditEnded?.Invoke(this, EventArgs.Empty);
                _isDirty = false;
                _backup = null;
            }
        }
    }

    public class SubCategoryValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            SubCategoryViewModel viewModel = (value as BindingGroup).Items[0] as SubCategoryViewModel;

            if (String.IsNullOrEmpty(viewModel.Name))
            {
                return new ValidationResult(false, "A név nem lehet üres");
            }
            else
            {
                return ValidationResult.ValidResult;
            }
        }
    }
}
