using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ELTE.TodoList.Desktop.ViewModel;

namespace Drink_Wholesale.Admin.ViewModel
{
    public class CategoryViewModel : ViewModelBase
    {
        private int _id;
        private String _name;

        public int Id
        {
            get
            {
                return _id;
            } set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        public String Name
        {
            get=> _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

    }
}
