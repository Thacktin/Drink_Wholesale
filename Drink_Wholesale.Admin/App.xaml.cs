using System.Configuration;
using System.Data;
using System.Reflection;
using System.Windows;
using AutoMapper;
using Drink_Wholesale.Admin.Model;
using Drink_Wholesale.Admin.View;
using Drink_Wholesale.Admin.ViewModel;
using ELTE.TodoList.Desktop.ViewModel;

namespace Drink_Wholesale.Admin
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private DrinkWholesaleAPIService _service = null!;
        private IMapper _mapper = null!;
        private MainViewModel _mainViewModel = null!;

        private OrderWindowViewModel _orderViewModel = null!;
        private MainWindow _mainView = null!;

        private ProductEditorWindow _productEditorWindow = null!;
        private OrdersWindow _ordersWindow = null!;


        private LoginWindow _loginWindow = null!;
        private LoginViewModel _loginViewModel = null!;
        public App()
        {
            Startup += App_Startup;
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            _service = new DrinkWholesaleAPIService(ConfigurationManager.AppSettings["baseAddress"]);
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(Assembly.GetExecutingAssembly());
            });
            _mapper = mapperConfig.CreateMapper();

            _loginViewModel = new(_service, _mapper);
            _loginViewModel.LoginSucceeded += _loginViewModel_LoginSucceeded; 
            _loginViewModel.LoginFailed+= _loginViewModel_LoginFailed; 
            _loginWindow = new()
            {
                DataContext = _loginViewModel
            };

           

            _orderViewModel = new OrderWindowViewModel(_service, _mapper);
            _orderViewModel.MessageApplication += MessageApplication;

            _mainViewModel = new MainViewModel(_service, _mapper);
            _mainViewModel.StartingProductEdit += _mainViewModel_StartingProductEdit;
            _mainViewModel.FinishingProductEdit += _mainViewModel_FinishingProductEdit;
            _mainViewModel.RequestingOrdersWindow += _mainViewModel_RequestingOrdersWindow;
            _mainViewModel.MessageApplication += MessageApplication;    
            _mainView = new View.MainWindow
            {
                DataContext = _mainViewModel
            };
            _loginWindow.Show();
        }



        private void _loginViewModel_LoginFailed(object? sender, EventArgs e)
        {
            MessageBox.Show("Hibás felhasználónév/jelszó!", "DrinkWholeSale", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }

        private void _loginViewModel_LoginSucceeded(object? sender, EventArgs e)
        {
            _loginWindow.Hide();
            _mainView.Show();
        }

        private void _mainViewModel_RequestingOrdersWindow(object? sender, EventArgs e)
        {
            _ordersWindow = new OrdersWindow()
            {
                DataContext = _orderViewModel
            };
            _ordersWindow.Show();
        }

        private void _mainViewModel_FinishingProductEdit(object? sender, EventArgs e)
        {
            if (_productEditorWindow.IsActive)
            {
                _productEditorWindow.Close();
            }
        }

        private void _mainViewModel_StartingProductEdit(object? sender, EventArgs e)
        {
            _productEditorWindow = new ProductEditorWindow()
            {
                DataContext = _mainViewModel
            };
            _productEditorWindow.ShowDialog();
        }

        private void MessageApplication(object? sender, ELTE.TodoList.Desktop.ViewModel.MessageEventArgs e)
        {
            MessageBox.Show(e.Message, "DrinkWholeSale", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }
    }

}
