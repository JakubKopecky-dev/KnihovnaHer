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
    public partial class MainSeznamHerViewModel(IApiService apiService, ITokenStorageService tokenStorageService) : BaseViewModel
    {

        private readonly IApiService apiService = apiService;
        private readonly ITokenStorageService tokenStorageService = tokenStorageService;

        public ObservableCollection<StatusHryViewDto> StatusyHer { get; set; } = [];

        private bool isLoading;

        public bool IsLoading
        {
            get => isLoading;
            set => SetProperty(ref isLoading, value);
        }

        private bool isAdmin;

        public bool IsAdmin
        {
            get => isAdmin;
            set => SetProperty(ref isAdmin, value);
        }

      
       

        public async Task LoadStatusHer()
        {
            IsLoading = true;
            try
            {
               
                var statusyHer = await apiService.GetStatusHryByUserAsync();
                StatusyHer.Clear();

                foreach (var s in statusyHer)
                    StatusyHer.Add(s);
            }
            finally
            {
                IsLoading = false;
            }
        }


        public async Task DeleteStatusHer(StatusHryViewDto statusHryViewDto)
        {

            var deletedStatus = await apiService.DeleteStatusHryAsync(statusHryViewDto.StatusHryId);
            if(deletedStatus is not null)
            {
                StatusyHer.Remove(statusHryViewDto);
            }



        }


        public async Task LoadAdminStatus() => IsAdmin = await tokenStorageService.IsAdminAsync();







    }
}
