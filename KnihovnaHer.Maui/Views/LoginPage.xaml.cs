using KnihovnaHer.Maui.ViewModels;

namespace KnihovnaHer.Maui.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
		BindingContext = App.Services.GetService<LoginViewModel>();
	}

	private async void OnLoginClicked(object sender, EventArgs e)
	{
		if (BindingContext is LoginViewModel viewModel)
		{
            var success = await viewModel.TryLoginAsync();

            if (success)
            {
                // Uložení AppShell jako hlavní stránky aplikace

                await Shell.Current.GoToAsync($"//{nameof(MainPage)}"); // pøesmìrování pøes Shell
            }
            else
            {
                await DisplayAlert("Chyba", "Pøihlášení selhalo", "OK");
            }

        }
    }


	private async void OnRegisterClicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync(nameof(RegisterPage));
	}

}