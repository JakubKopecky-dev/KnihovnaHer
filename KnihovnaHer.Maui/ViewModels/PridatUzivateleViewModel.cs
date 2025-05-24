using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnihovnaHer.Dto;
using KnihovnaHer.Maui.Services;

namespace KnihovnaHer.Maui.ViewModels
{
    public partial class PridatUzivateleViewModel(IApiService apiService) : BaseViewModel
    {

        private readonly IApiService apiService = apiService;

        private string email = "";
        public string Email
        {
            get => email;
            set =>SetProperty(ref email, value);
        }


        private string password = "";
        public string Password
        {
            get => password;
            set =>SetProperty(ref password, value);
        }

        private bool isAdmin;
        public bool IsAdmin
        {
            get => isAdmin;
            set => SetProperty(ref isAdmin, value);
        }

     

        public async Task<bool> PridatUzivateleAsync()
        {
            var uzivatelDto = new UzivatelCreateDto
            {
                Email = Email,
                Password = Password,
                IsAdmin = IsAdmin,
            };
            

            var addeduzivatel = await apiService.AddUzivatelAsync(uzivatelDto);

            if(addeduzivatel is null)
                return false;

            return true;

        }







    }
}
