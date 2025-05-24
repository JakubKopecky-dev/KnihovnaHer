using KnihovnaHer.Data.Models;
using KnihovnaHer.Dto;
using KnihovnaHer.Maui.ViewModels;

namespace KnihovnaHer.Maui.Views;

public partial class ZanrVydavatelPage : ContentPage
{
	public ZanrVydavatelPage()
	{
		InitializeComponent();
		BindingContext = App.Services.GetService<ZanrVydavatelViewModel>();

	}


	protected override async void OnAppearing()
	{
		base.OnAppearing();

		if (BindingContext is ZanrVydavatelViewModel viewModel)
		{ 
			Console.WriteLine("Naèítám zanry a vydavatele");
			await viewModel.LoadZanryVydavateleAsync(); 
		
		
		
		}

	}

	// tlaèítko pøidání žánru
	private async void OnAddZanrClicked(object sender, EventArgs e)
	{
        string nazev = await DisplayPromptAsync("Pøidat žánr", "Zadejte název nového žánru:");
		if(!string.IsNullOrWhiteSpace(nazev))
		{
			if(BindingContext is ZanrVydavatelViewModel viewModel)
				await viewModel.AddZanrAsync(nazev);
		}
	}

    //tlaèítko pøidání vydavatele
    private async void OnAddVydavatelClicked(object sender, EventArgs e)
    {
        string nazev = await DisplayPromptAsync("Pøidat vydavatele", "Zadejte název nového vydavatele:");

        if (!string.IsNullOrWhiteSpace(nazev))
        {
            if (BindingContext is ZanrVydavatelViewModel viewModel)
            {
                await viewModel.AddVydavatelAsync(nazev);
            }
        }
    }


    // tlaèítko odstranìní žánru
    private async void OnDeleteZanrClicked(object sender, EventArgs e)
	{
		var button = sender as Button;
		var zanr = button?.BindingContext as ZanrDto;

		if(zanr is not null)
		{
            bool confirmed = await DisplayAlert("Odstranit žánr", $"Opravdu chcete odstranit žánr: {zanr.Nazev}?", "Ano", "Ne")	;
		
			if(confirmed)
			{
				if(BindingContext is ZanrVydavatelViewModel viewModel)
					await viewModel.DeleteZanrAsync(zanr);
			}
           
        }
    }

	// tlaèítko odstranìní vydavatele
	private async void OnDeleteVydavatelClicked(Object sender, EventArgs e)
	{
		var button = sender as Button;
		var vydavatel = button?.BindingContext as VydavatelDto;

		if(vydavatel is not null)
		{
			bool confirmed = await DisplayAlert("Odstranit vydavatele", $"Opravdu chcete odstranit vydavatele: {vydavatel.Nazev}?", "Ano", "Ne");
			
			if(confirmed)
			{
				if(BindingContext is ZanrVydavatelViewModel viewModel)
					await viewModel.DeleteVydavatelAsnyc(vydavatel);
			}
		}

    }










}