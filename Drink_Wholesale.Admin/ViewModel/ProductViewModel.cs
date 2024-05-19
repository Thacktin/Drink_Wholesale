using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ELTE.TodoList.Desktop.ViewModel;

namespace Drink_Wholesale.Admin.ViewModel
{
    public class ProductViewModel : ViewModelBase
    {
        private int _id;
        private String _description;

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        public String Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }
    }
}
