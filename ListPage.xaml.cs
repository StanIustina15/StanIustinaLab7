using StanIustinaLab7.Models;
namespace StanIustinaLab7;


public partial class ListPage : ContentPage
{
	public ListPage()
	{
		InitializeComponent();
	}
    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var slist = (ShopList)BindingContext;
        slist.Date = DateTime.UtcNow;
        await App.Database.SaveShopListAsync(slist);
        await Navigation.PopAsync();
    }
    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var slist = (ShopList)BindingContext;
        await App.Database.DeleteShopListAsync(slist);
        await Navigation.PopAsync();
    }
    async void OnChooseButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ProductPage((ShopList)
       this.BindingContext)
        {
            BindingContext = new Product()
        });

    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var shopl = (ShopList)BindingContext;

        listView.ItemsSource = await App.Database.GetListProductsAsync(shopl.ID);
    }
    async void OnDeleteItemButtonClicked(object sender, EventArgs e)
    {
        var selectedItem = listView.SelectedItem as Product;
        if (selectedItem != null) // Verificăm dacă există un element selectat
        {
            var shopList = (ShopList)this.BindingContext;

            // Găsim legătura dintre produs și lista curentă
            var listProduct = (await App.Database.GetListProductsAsync(shopList.ID))
                .FirstOrDefault(p => p.ID == selectedItem.ID);

            if (listProduct != null)
            {
                await App.Database.DeleteListProductAsync(listProduct);
                listView.ItemsSource = await App.Database.GetListProductsAsync(shopList.ID); // Reîmprospătăm lista
            }
            else
            {
                await DisplayAlert("Error", "Item not found in the list.", "OK");
            }
        }
        else
        {
            await DisplayAlert("Error", "No item selected for deletion.", "OK");
        }
        }
    }

