using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Drink_Wholesale.DTO;
using ELTE.TodoList.Desktop.ViewModel;

namespace Drink_Wholesale.Admin.ViewModel
{
    public class ProductViewModel : ViewModelBase, IEditableObject
    {
        private int _id;
        private String _description = null!;
        private String _producer = null!;
        private int _artno;
        private int _inventory;
        private decimal _netprice;
        private int _subCatId;
        private Packaging _packaging;

        private Boolean _isDirty = false;
        private ProductViewModel _backup;

        public String this[string columnName]
        {
            get
            {
                String error = String.Empty;
                switch (columnName)
                {
                    case nameof(Inventory):
                        if (Inventory < 1)
                            error = "Item name cannot be empty.";
                        //else if (Name.Length > 30)
                        //    error = "Item name cannot be longer than 30 characters.";
                        break;
                }


                return error;
            }
        }

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

        public String Producer
        {
            get => _producer;
            set
            {
                _producer = value;
                OnPropertyChanged();
            }
        }

        public int ArtNo
        {
            get => _artno;
            set
            {
                _artno = value;
                OnPropertyChanged();
            }
        }
        public int Inventory
        {
            get => _inventory;
            set
            {
                _inventory = value;
                OnPropertyChanged();
            }
        }

        public Packaging Packaging
        {
            get => _packaging;
            set
            {
                _packaging = value;
                OnPropertyChanged();
            }
        }
        public decimal NetPrice
        {
            get => _netprice;
            set
            {
                _netprice = value;
                OnPropertyChanged();
            }
        }
        public decimal GrossPrice =>_netprice * 1.27m;

        public int SubCategoryId
        {
            get => _subCatId;
            set
            {
                _subCatId = value;
                OnPropertyChanged();
            }
        }

        public Boolean IsDirty
        {
            get => _isDirty;
            private set => _isDirty = value;
        }

        public event EventHandler EditEnded;
        public void BeginEdit()
        {
            if (!_isDirty)
            {
                _backup = (ProductViewModel)this.MemberwiseClone();
                _isDirty = true;
            }
        }

        public void CancelEdit()
        {
            if (_isDirty)
            {
                Inventory = _inventory;
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
}
