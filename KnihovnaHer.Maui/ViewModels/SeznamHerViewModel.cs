using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnihovnaHer.Dto;
using KnihovnaHer.Maui.Services;

namespace KnihovnaHer.Maui.ViewModels
{
    public partial class SeznamHerViewModel(IApiService apiService) : BaseViewModel
    {
        private readonly IApiService apiService = apiService;


        public ObservableCollection<HraDto> Hry { get; set; } = [];

        private bool isLoading;
        public bool IsLoading
        {
            get => isLoading;
            set => SetProperty(ref isLoading, value);
        }


        public async Task LoadHryAsync()
        {
            IsLoading = true;
            try{
            var hry = await apiService.GetHryAsync();

            Hry.Clear();

                foreach (var hra in hry)
                    Hry.Add(hra);
            }
            finally
            {
                IsLoading = false;
            }
            
        }

        public async Task DeleteHraAsync(HraDto hra)
        {
            var deletedHra = await apiService.DeleteHraAsync(hra.HraId);
            if (deletedHra is not null)
            {
                Hry.Remove(hra);
                Console.WriteLine("Hra byla úspěšně smazána");
            }
            else Console.WriteLine("Odstraňování hry selhalo");
        }


        public async Task<bool> PridatStatusHryAsync(HraDto hra)
        {
            try 
            {
                

                var dto = new StatusHryCreateDto
                {
                    HraId = hra.HraId,
                };

                var result = await apiService.AddStatusHryAsync(dto);
                if(result is not null)
                {
                    Debug.WriteLine("Status úspěšně přidán.");
                    return true;
                }
                else
                {
                    Debug.WriteLine("Přidání statusu selhalo.");  
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Výjímka při vytváření statusu hry: {ex}");
                return false;
            }











        }




    }
}
