using KnihovnaHer.Maui.ViewModels;

namespace KnihovnaHer.Maui.Views;

[QueryProperty(nameof(HraIdString),"HraId")]
public partial class PridatHruPage : ContentPage
{
    public string HraIdString
    {
        set
        {
            if (uint.TryParse(value, out var parsedId))
            {
                HraId = parsedId;
            }
        }
    }
    public uint? HraId { get; set; }

	public PridatHruPage()
	{
		InitializeComponent();
		BindingContext = App.Services.GetService<VytvoritHruViewModel>();
	}


	protected override async void OnAppearing()
	{
		base.OnAppearing();

		if (BindingContext is VytvoritHruViewModel viewModel)
		{ 
            viewModel.HraId = HraId;
            await viewModel.NacistDataAsync(); 
		
		if(viewModel.HraId.HasValue)
			{
				var hraDto = await viewModel.GetHraByIdAsync(viewModel.HraId.Value);
               
				viewModel.NaplnitFormular(hraDto);
            }

            var saveButton = this.FindByName<Button>("SaveButton");
            if (saveButton != null)
            {
                saveButton.Text = viewModel.HraId.HasValue ? "Upravit hru" : "Pøidat hru";
            }



        }
	}

	private async void OnPridatHruClicked(object sender, EventArgs e)
	{
        if (BindingContext is VytvoritHruViewModel viewModel)
        {
            bool result = await viewModel.PridatEditovatHruAsync();

            if (result)
                await Shell.Current.GoToAsync("..");
                
        }

        
	}



}