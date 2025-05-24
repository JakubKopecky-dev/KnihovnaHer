using System.Collections.ObjectModel;
using KnihovnaHer.Dto;
using KnihovnaHer.Maui.Services;
using KnihovnaHer.Maui.UiModels;

namespace KnihovnaHer.Maui.ViewModels
{
    public partial class VytvoritHruViewModel (IApiService apiService) : BaseViewModel
    {
        private readonly IApiService apiService = apiService;



        private string nazev="";
        public string Nazev
        {
            get => nazev;
            set => SetProperty(ref nazev, value);
        }

        private int rokVydani;
        public int RokVydani
        {
            get => rokVydani;
            set => SetProperty(ref rokVydani, value);
        }

        private bool jeEditace;
        public bool JeEditace
        {
            get => jeEditace;
            set => SetProperty(ref jeEditace, value);
        }

        private uint? hraId;
        public uint? HraId
        {
            get => hraId;
            set => SetProperty(ref hraId, value);
        }

        private ObservableCollection<VyberZanr> _zanryKVyberu = [];
        public ObservableCollection<VyberZanr> ZanryKVyberu
        {
            get => _zanryKVyberu;
            set => SetProperty(ref _zanryKVyberu, value);
        }

        private ObservableCollection<VydavatelDto> vydavatele = [];
        public ObservableCollection<VydavatelDto> Vydavatele
        {
            get => vydavatele;
            set => SetProperty(ref vydavatele, value);
        }

        private VydavatelDto? vybranyVydavatele;
        public VydavatelDto? VybranyVydavatele
        {
            get => vybranyVydavatele;
            set => SetProperty(ref vybranyVydavatele, value);
        }



        public async Task NacistDataAsync()
        {
            var zanry = await apiService.GetZanryAsync();
            var vydavatele = await apiService.GetVydavateleAsync();

            Vydavatele.Clear();
            ZanryKVyberu.Clear();

            foreach (var z in zanry)
                ZanryKVyberu.Add(new VyberZanr { Nazev = z.Nazev });

            foreach(var v in vydavatele)
                Vydavatele.Add(v);

        }




        public async Task<bool> PridatEditovatHruAsync()
        {
            var vybraneZanry = ZanryKVyberu
                .Where(z => z.JeVybrany)
                .Select(z => z.Nazev)
                .ToList();


            var hraDto = new HraCreateEditDto
            {
                Nazev = Nazev,
                RokVydani = RokVydani,
                Zanry = vybraneZanry,
                VydavatelId = VybranyVydavatele?.VydavatelId
            };


            bool uspesne;

            if (JeEditace && HraId.HasValue)
            {

                var updatedHra = await apiService.UpdateHraAsync(HraId.Value, hraDto);
                uspesne = updatedHra is not null;

                if (uspesne)
                    await Shell.Current.DisplayAlert("Úspěch", "Hra byla upravena", "OK");
                else
                    await Shell.Current.DisplayAlert("Chyba", "Nepodařilo se upravit hru", "OK");
            }
            else
            {
                var addedHra = await apiService.AddHraAsync(hraDto);
                uspesne = addedHra is not null;

                if (uspesne)
                    await Shell.Current.DisplayAlert("Úspěch", "Hra byla přidána", "OK");

                else
                    await Shell.Current.DisplayAlert("Chyba", "Nepodařilo se přidat hru", "OK");

            }

            return uspesne;

        }







        public void NaplnitFormular(HraDto hra)
        {
            HraId = hra.HraId;
            Nazev = hra.Nazev;
            RokVydani = hra.RokVydani;
            VybranyVydavatele = Vydavatele.FirstOrDefault(v => v.VydavatelId == hra.Vydavatel?.VydavatelId);

            foreach (var zanr in ZanryKVyberu)
                zanr.JeVybrany = hra.Zanry.Contains(zanr.Nazev);


        JeEditace = true;
        }

        public async Task<HraDto> GetHraByIdAsync(uint hraId)
        {
            var hra = await apiService.GetHra(hraId);
            return hra!;

        }


    }
}
