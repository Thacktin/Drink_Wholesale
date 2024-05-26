using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using AutoMapper;
using Drink_Wholesale.Admin.Model;
using Drink_Wholesale.Desktop.Model;
using ELTE.TodoList.Desktop.ViewModel;

namespace Drink_Wholesale.Admin.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        #region Private fields

        

        private String _username;
        private Boolean _isLoading;

        private readonly DrinkWholesaleAPIService _service;
        private readonly IMapper _mapper;

        #endregion


        #region Public properties

        

        public String Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }
        public Boolean IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }



        #endregion

        #region Events

        public EventHandler LoginSucceeded;
        public EventHandler LoginFailed;

#endregion
        #region Commands

        public DelegateCommand LoginCommand { get; private set; }

        #endregion
        public LoginViewModel(DrinkWholesaleAPIService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
            Username = String.Empty;
            LoginCommand = new DelegateCommand(_ => !IsLoading, async param => await LoginAsync(param as PasswordBox));

        }

        private async Task LoginAsync(PasswordBox passwordBox)
        {
            if (passwordBox == null)
                return;

            try
            {
                IsLoading = true;
                bool result = await _service.LoginAsync(Username, passwordBox.Password);
                IsLoading = false;

                if (result)
                    OnLoginSuccess();
                else
                    OnLoginFailed();
            }
            catch (HttpRequestException ex)
            {
                OnMessageApplication($"Server error occurred: ({ex.Message})");
            }
            catch (NetworkException ex)
            {
                OnMessageApplication($"Unexpected error occurred: ({ex.Message})");
            }
        }

        private void OnLoginFailed()
        {

            LoginFailed?.Invoke(this, EventArgs.Empty);
        }

        private void OnLoginSuccess()
        {
            LoginSucceeded?.Invoke(this, EventArgs.Empty);
        }
    }
}
