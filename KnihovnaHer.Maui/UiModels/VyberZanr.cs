using KnihovnaHer.Maui.ViewModels; 

namespace KnihovnaHer.Maui.UiModels
{
    public class VyberZanr : BaseViewModel
    {
        private string _nazev = "";
        public string Nazev
        {
            get => _nazev;
            set => SetProperty(ref _nazev, value);
        }

        private bool _jeVybrany;
        public bool JeVybrany
        {
            get => _jeVybrany;
            set => SetProperty(ref _jeVybrany, value);
        }
    }
}
