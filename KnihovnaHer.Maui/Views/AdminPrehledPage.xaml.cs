namespace KnihovnaHer.Maui.Views;

public partial class AdminPrehledPage : ContentPage
{
	public AdminPrehledPage()
	{
		InitializeComponent();
	}



    private async void OnSeznamHerAdminClicked(object sender, EventArgs e)
    {

        await Shell.Current.GoToAsync(nameof(SeznamHerPage));
    }



    private async void OnSeznamUzivateluClicked(Object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(SeznamUzivateluPage));
    }



    // TLAÈÍTKO žánry a vydavatelé
    private async void OnZanrVydavatelClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(ZanrVydavatelPage));
    }







}


