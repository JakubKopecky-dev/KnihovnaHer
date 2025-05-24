using KnihovnaHer.Maui.ViewModels;

namespace KnihovnaHer.Maui.Views;

public partial class PridatUzivatelePage : ContentPage
{
	public PridatUzivatelePage()
	{
        InitializeComponent();
        BindingContext = App.Services.GetService<PridatUzivateleViewModel>();
    }



    public async void OnUlozitClicked(object sender, EventArgs e)
    {
        if (BindingContext is PridatUzivateleViewModel viewModel)
        {
            var seccues = await viewModel.PridatUzivateleAsync();

            if (seccues)
                await Shell.Current.GoToAsync("..");

            else
                await Shell.Current.DisplayAlert("Chyba", "Nepodaøilo se vytvoøit uživatele", "OK");





        }

    }













}