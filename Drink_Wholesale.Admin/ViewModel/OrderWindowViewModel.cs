using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Drink_Wholesale.Admin.Model;
using Drink_Wholesale.Desktop.Model;
using ELTE.TodoList.Desktop.ViewModel;

namespace Drink_Wholesale.Admin.ViewModel
{
    public class OrderWindowViewModel : ViewModelBase
    {
        #region Fields
        private readonly DrinkWholesaleAPIService _service;
        private readonly IMapper _mapper;

        private IEnumerable<OrderViewModel> _orders;
        private IEnumerable<CartItemViewModel> _cart;
        private OrderViewModel _selectedOrder;
        #endregion

        #region Public Getter/Setters

        public IEnumerable<OrderViewModel> Orders
        {
            get => _orders;
            set
            {
                _orders = value;
                OnPropertyChanged();
            }
        }

        public OrderViewModel SelectedOrder
        {
            get => _selectedOrder;
            set
            {
                _selectedOrder = value;
                OnPropertyChanged();
            }
        }

        public IEnumerable<CartItemViewModel> Cart
        {
            get => _cart;
            set
            {
                _cart = value;
                OnPropertyChanged();
            }
        }

        #endregion
        #region Commands

        public DelegateCommand SelectOrderCommand { get; private set; }
        public DelegateCommand RefreshOrdersCommand { get; private set; }


        #endregion

        public OrderWindowViewModel(DrinkWholesaleAPIService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;

            RefreshOrdersCommand = new DelegateCommand(async _ => await LoadOrdersAsync());
            SelectOrderCommand = new DelegateCommand(async _ => await LoadProductsAsync(SelectedOrder));
        }

        public async Task LoadOrdersAsync()
        {
            try
            {
                Orders = new ObservableCollection<OrderViewModel>((await _service.LoadOrdersAsync()).Select(order =>
                {
                    var orderVm = _mapper.Map<OrderViewModel>(order);
                    return orderVm;
                }));
            }
            catch (Exception ex)

                when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occurred! ({ex.Message})");
            }
        }

        private async Task LoadProductsAsync(OrderViewModel order)
        {
            if (order is null || order.Id == 0)
            {
                Cart = null;
                return;
            }

            try
            {
                Cart = new ObservableCollection<CartItemViewModel>((await _service.LoadCartAsync(order.Id)).Select(product =>
                {
                    var cartVm = _mapper.Map<CartItemViewModel>(product);
                    return cartVm;
                }));
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occurred! ({ex.Message})");
            }
        }
    }
}
