using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnihovnaHer.Dto;
using KnihovnaHer.Maui.Services;

namespace KnihovnaHer.Maui.ViewModels
{
    public partial class LoginViewModel(IApiService apiService, ITokenStorageService tokenStorageService) : BaseViewModel
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



        public async Task<bool> TryLoginAsync()
        {
            ErrorMessage = "";

            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Zadejte e-mail i heslo.";
                Debug.WriteLine("E-mail nebo heslo nebylo vyplněno.");
                return false;
            }

            try
            {
                Debug.WriteLine($"Odesílám přihlašovací požadavek pro: {Email}");

                var response = await apiService.LoginAsync(new AuthDto { Email = Email, Password = Password });

                if (response == null)
                {
                    Debug.WriteLine("Odpověď z API byla null.");
                    ErrorMessage = "Přihlášení selhalo (žádná odpověď).";
                    return false;
                }

                if (!string.IsNullOrEmpty(response.Token))
                {
                    Debug.WriteLine("Přihlášení úspěšné. Ukládám token...");
                    await tokenStorageService.SaveTokenAsync(response.Token);

                    // Můžeš si zde uložit i informace o uživateli, pokud chceš
                    Debug.WriteLine($"Uživatel: {response.User.Email}, IsAdmin: {response.User.IsAdmin}");

                    return true;
                }

                Debug.WriteLine("Token byl prázdný.");
                ErrorMessage = "Neplatné přihlašovací údaje.";
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Chyba při pokusu o přihlášení: {ex}");
                ErrorMessage = "Chyba během přihlášení.";
                return false;
            }
        }









    }
}
