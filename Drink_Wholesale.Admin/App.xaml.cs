using System.Configuration;
using System.Data;
using System.Reflection;
using System.Windows;
using AutoMapper;
using Drink_Wholesale.Admin.Model;
using Drink_Wholesale.Admin.View;
using Drink_Wholesale.Admin.ViewModel;

namespace Drink_Wholesale.Admin
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private DrinkWholesaleAPIService _service;
        private IMapper _mapper;
        private MainViewModel _mainViewModel;
        private CategoryViewModel _categoryViewModel;

        private MainWindow _mainView;
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

            _mainViewModel = new MainViewModel(_service, _mapper);
            _mainView = new View.MainWindow
            {
                DataContext = _mainViewModel
            };
            _mainView.Show();
        }
    }

}
