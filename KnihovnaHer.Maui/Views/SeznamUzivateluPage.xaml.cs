using System.Diagnostics;
using KnihovnaHer.Dto;
using KnihovnaHer.Maui.ViewModels;

namespace KnihovnaHer.Maui.Views;

public partial class SeznamUzivateluPage : ContentPage
{
	public SeznamUzivateluPage()
	{
		InitializeComponent();
		BindingContext = App.Services.GetService<SeznamUzivateluViewModel>();
	}


    protected override async void OnAppearing()
    {
        base.OnAppearing();


		if (BindingContext is SeznamUzivateluViewModel viewmodel)
			await viewmodel.LoadUzivatel();

    }


	private async void OnVytvoritUzivateleClicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync(nameof(PridatUzivatelePage));

	}



	private async void OnDeleteUzivatelClicked(object sender,EventArgs e)
	{
		
		var button = sender as Button;
		var uzivatelDto = button?.BindingContext as UzivatelDto;

		if(uzivatelDto is not null )
		{

			bool confirmend = await DisplayAlert("Odstranìní uživatele", $"Opravdu chcete odstranit uživatele: {uzivatelDto.Email}", "ANO", "NE");

			if (confirmend)
			{
				if (BindingContext is SeznamUzivateluViewModel viewmodel)
				{ 
					var result = await viewmodel.DeleteUzivatel(uzivatelDto);

					if (!result)
						Debug.WriteLine("Chyba pøi odstranìní uživatele");
				}

            }

		}
	


	}




	private async void OnOpravneniClicked(object sender, EventArgs e)
	{
		var button = sender as Button;
		var uzivatelDto = button?.BindingContext as UzivatelDto;

		if(uzivatelDto is not null)
		{
			if(uzivatelDto.IsAdmin)
			{
				bool confirmed = await DisplayAlert("Odebrání administrátora", $"Chcete uživateli: {uzivatelDto.Email} odebrat administrátora?", "ANO", "NE");

				if(confirmed)
				{
					if (BindingContext is SeznamUzivateluViewModel viewmodel)
						await viewmodel.ZmenaOpravneniUzivatele(false, uzivatelDto);

                }

			}
			else
			{

                bool confirmed = await DisplayAlert("Pøidání administrátora", $"Chcete uživateli: {uzivatelDto.Email} pøidat administrátora?", "ANO", "NE");

				if (confirmed)
				{
					if (BindingContext is SeznamUzivateluViewModel viewmodel)
						await viewmodel.ZmenaOpravneniUzivatele(true, uzivatelDto);

				}


            }

		}

	}









}