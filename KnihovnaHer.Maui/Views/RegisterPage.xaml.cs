using System.Diagnostics;
using KnihovnaHer.Maui.ViewModels;

namespace KnihovnaHer.Maui.Views;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
		BindingContext = App.Services.GetService<RegisterViewModel>();
	}

	private async void OnRegisterClicked (object sender, EventArgs e)
	{
		if(BindingContext is RegisterViewModel viewModel)
		{
			var success = await viewModel.RegisterAsync();

			if (!success)
				Debug.WriteLine("Registrace v register page je false");

			if (success)
				await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
		}
	}

	private async void OnLoginClicked (object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync(nameof(LoginPage));
	}


}