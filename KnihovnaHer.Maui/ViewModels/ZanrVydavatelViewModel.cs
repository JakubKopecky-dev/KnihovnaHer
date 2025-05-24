using System.Collections.ObjectModel;
using KnihovnaHer.Dto;
using KnihovnaHer.Maui.Services;

namespace KnihovnaHer.Maui.ViewModels
{
    public partial class ZanrVydavatelViewModel(IApiService apiService) : BaseViewModel
    {
        private readonly IApiService apiService = apiService;
        public ObservableCollection<ZanrDto> Zanry { get; set; } = [];
        public ObservableCollection<VydavatelDto> Vydavatele { get; set; } = [];

        private bool isLoading;
        public bool IsLoading
        {
            get => isLoading;
            set => SetProperty(ref isLoading, value);
        }
        public async Task LoadZanryVydavateleAsync()
        {
            IsLoading = true;

          try { var zanry = await apiService.GetZanryAsync();
            var vydavatele = await apiService.GetVydavateleAsync();

            Zanry.Clear();
            Vydavatele.Clear();

            foreach (var zanr in zanry)
                Zanry.Add(zanr);

                foreach (var vydavatel in vydavatele)
                    Vydavatele.Add(vydavatel);
            }
            finally 
            { 
                IsLoading = false; 
            }

        }


        public async Task DeleteVydavatelAsnyc(VydavatelDto vydavatelDto)
        {
            var deletedVydavatel = await apiService.DeleteVydavatel(vydavatelDto.VydavatelId);
                
                if(deletedVydavatel is not null)
                {
                Vydavatele.Remove(vydavatelDto);
                Console.WriteLine("Vydavatel byl úspěšně smazán");
                }
                else Console.WriteLine("Odstranění vydavatele selhalo");
                
           

        }

        public async Task DeleteZanrAsync(ZanrDto zanrDto)
        {
            var deletedZanr = await apiService.DeleteZanr(zanrDto.ZanrId);
            if (deletedZanr is not null)
            {
                Zanry.Remove(zanrDto);
                Console.WriteLine("Žánr byl úspěšně smazán");
            }
            else Console.WriteLine("Odstranění žánru selhalo");
        }


        public async Task AddZanrAsync(string nazev)
        {
            var novyZanr = new ZanrDto { Nazev = nazev };

            var createdZanr = await apiService.AddZanrAsync(novyZanr);
            if (createdZanr is not null)
            {
                Zanry.Add(createdZanr);
                Console.WriteLine("Žánr byl úspěšně přidán");
            }
            else Console.WriteLine("Přidání žánru selhalo");
        }

        public async Task AddVydavatelAsync(string nazev)
        {
            var novyVydavatel = new VydavatelDto { Nazev = nazev };
            var createdVydavatel = await apiService.AddVydavatelAsync(novyVydavatel);

            if (createdVydavatel is not null)
            {
                Vydavatele.Add(createdVydavatel);
                Console.WriteLine("Vydavatel úspěšně přidán");
            }
            else Console.WriteLine("Přidání vydavatele selhalo");
            

        }




    }
}