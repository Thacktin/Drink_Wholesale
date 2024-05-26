using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AutoMapper;
using Drink_Wholesale.Admin.Model;
using Drink_Wholesale.Desktop.Model;
using Drink_Wholesale.DTO;
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
        public DelegateCommand OrderCompleteCommand { get; private set; }
        public DelegateCommand OrderCompleteRevokeCommand { get; private set; }
        public DelegateCommand RefreshOrdersCommand { get; private set; }


        #endregion

        public OrderWindowViewModel(DrinkWholesaleAPIService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;

            RefreshOrdersCommand = new DelegateCommand(async _ => await LoadOrdersAsync());
            SelectOrderCommand = new DelegateCommand(async _ => await LoadProductsAsync(SelectedOrder));
            OrderCompleteCommand = new DelegateCommand(
                _ => SelectedOrder is not null && !SelectedOrder.IsFulfilled, 
                async _ => await CompleteOrderAsync(SelectedOrder));
            OrderCompleteRevokeCommand = new DelegateCommand(
                _ => SelectedOrder is not null && SelectedOrder.IsFulfilled, 
                async _ => await RevokeOrderCompleteAsync(SelectedOrder));
        }

        private async Task RevokeOrderCompleteAsync(OrderViewModel selectedOrder)
        {
            MessageBoxResult result = MessageBox.Show("Vissavonja a teljesítettnek jelölést?", "asd", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                bool res = await _service.RevokeFulfillOrderAsync(_mapper.Map<OrderDto>(selectedOrder));
                if (res == true)
                {
                    await LoadOrdersAsync();
                }
            }
        }

        private async Task CompleteOrderAsync(OrderViewModel selectedOrder)
        {
            MessageBoxResult result = MessageBox.Show("Teljesítettnek jelöli?", "asd", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
               bool res =  await _service.FulfillOrderAsync(_mapper.Map<OrderDto>(selectedOrder));
               if (res == true)
               {
                  await LoadOrdersAsync();
               }
            }
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
