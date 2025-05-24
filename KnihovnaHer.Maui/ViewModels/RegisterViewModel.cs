using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnihovnaHer.Maui.Services;

namespace KnihovnaHer.Maui.ViewModels
{
    public partial class RegisterViewModel(IApiService apiService, ITokenStorageService tokenStorageService) : BaseViewModel
    {
        private readonly IApiService apiService = apiService;
        private readonly ITokenStorageService tokenStorageService = tokenStorageService;


        private string email = "";
        public string Email
        { 
        get => email;
        set=> SetProperty(ref email, value);
        }

        private string password = "";
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }


        private string confirmPassword = "";
        public string ConfirmPassword
        {
            get => confirmPassword;
            set => SetProperty(ref confirmPassword, value);
        }

        private string errorMessage = "";
        public string ErrorMessage
        {
            get => errorMessage;
            set => SetProperty(ref errorMessage, value);
        }

        private bool hasError;
        public bool HasError
        {
            get => hasError;
            set => SetProperty(ref hasError, value);
        }


        public async Task<bool> RegisterAsync()
        {
            ErrorMessage = "";

            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(ConfirmPassword))
            {
                ErrorMessage = "Všechna pole musí být vyplněna.";
                HasError = true;
                return false;
            }


            if(Password != ConfirmPassword)
            {
                ErrorMessage = "Hesla se neshodují.";
                HasError = true;
                return false;
            }


            var response = await apiService.RegisterAsync(new Dto.AuthDto { Email = Email, Password = Password });

            if(response is not null && !string.IsNullOrEmpty(response.Token))
            {
                await tokenStorageService.SaveTokenAsync(response.Token);
                return true;
            }
            else
            {
                ErrorMessage = "Chyba při registraci. Zkuste to znovu.";
                HasError = true;
                return false;
            }




        }



    }
}
