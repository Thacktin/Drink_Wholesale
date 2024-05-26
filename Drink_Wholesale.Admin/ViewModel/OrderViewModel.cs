using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ELTE.TodoList.Desktop.ViewModel;

namespace Drink_Wholesale.Admin.ViewModel
{
    public class OrderViewModel : ViewModelBase

    {



    //public virtual List<CartItem> Products { get; set; } = null!;


    private int _id;
    private String _name;
    private String _address;
    private String _phoneNumber;
    private String _email;
    private bool _isFulfilled;

    public int Id
    {
        get
        {
            return _id;
        }
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

    public String Address
    {
        get => _address;
        set
        {
            _address = value;
            OnPropertyChanged();
        }
    }

    public String PhoneNumber
    {
        get => _phoneNumber;
        set
        {
            _phoneNumber = value;
            OnPropertyChanged();
        }
    }

    public String Email
    {
        get => _email;
        set
        {
            _email = value;
            OnPropertyChanged();
        }
    }

    public bool IsFulfilled
    {
        get => _isFulfilled;
        set
        {
            _isFulfilled = value;
            OnPropertyChanged();
        }
    }
    }
}
