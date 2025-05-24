using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace KnihovnaHer.Maui.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
         
        // Událost pro PropertyChanged (notifikace UI)
        public event PropertyChangedEventHandler? PropertyChanged;

        // Metoda pro notifikaci UI o změně vlastnosti
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Pomocná metoda pro nastavení hodnoty a notifikaci změny
        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}

