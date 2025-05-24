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
    public partial class SeznamUzivateluViewModel(IApiService apiService) : BaseViewModel    
    {
        private readonly IApiService apiService = apiService;



        public ObservableCollection<UzivatelDto> Uzivatele { get; set; } = [];

        private bool isLoading;
        public bool IsLoading
        {
            get => isLoading;
            set => SetProperty(ref isLoading, value);
        }

    


        public async Task LoadUzivatel()
        {
            IsLoading = true;
            try 
            {
                var uzivatele =  await apiService.GetAllUzivatelAsync();
                Uzivatele.Clear();

                foreach (var u in uzivatele)
                    Uzivatele.Add(u);
            }
            finally
            {
                IsLoading = false;
            }

        }


        public async Task<bool> DeleteUzivatel(UzivatelDto uzivatel)
        {
           var deletedUser = await apiService.DeleteUzivatelAsync(uzivatel.UzivatelId);
            if (deletedUser is not null)
            { 
                Uzivatele.Remove(uzivatel); 
                return true;
            }
            return false;
        }




        public async Task ZmenaOpravneniUzivatele(bool isAdmin, UzivatelDto uzivatelDto)
        {

         UzivatelEditDto uzivatel =   new() { IsAdmin = isAdmin };

           await apiService.UpdateUzivatel(uzivatelDto.UzivatelId, uzivatel);

            await LoadUzivatel();
                   
              

        }








    }
}
