using API_Wrapper;
using Newtonsoft.Json.Linq;

namespace ShoppingSpree;

public partial class Products : ContentPage
{
    private API api;

    public string Count;
    public string Unit;
    public string Product;
    public string UserToken;

	public Products(string token)
	{
		InitializeComponent();
        UserToken = token;
        CreateAPIinstance();

    }

    async void CreateAPIinstance()
    {
        string res = await LoadMauiAsset();
        api = new API(res);
    }

    async Task<string> LoadMauiAsset()
    {
        using var stream = await FileSystem.OpenAppPackageFileAsync("APIKEY.env");
        using var reader = new StreamReader(stream);

        var contents = reader.ReadToEnd();
        return contents;
    }

    private void CountEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        Count = CountEntry.Text;
        test.Text = Count;
    }

    private void UnitEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        Unit = UnitEntry.Text;
        test.Text = Unit;
    }

    private void ProductEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        Product = ProductEntry.Text;
        test.Text= Unit;
    }

    private async void SubmitButton_Clicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(UserToken) && !string.IsNullOrEmpty(Product) && !string.IsNullOrEmpty(Unit))
        {
            if (int.TryParse(Count, out int count))
            {
                string res = await api.CreateNewEntry(UserToken, Product, Unit, count);
                JObject responseJson = JObject.Parse(res);
                if (responseJson.TryGetValue("message", out var message))
                {
                    string messageText = message.ToString();

                    if (messageText == "Entry saved successfully")
                    {
                        test.Text = messageText + " => Redirecting... ";
                        await Task.Delay(750);
                        await Navigation.PopAsync();
                    }
                    else
                    {
                        test.Text = messageText;
                    }
                }
            }
            else
            {
                test.Text = "Count is not a number";
            }
        }
        else
        {
            test.Text = "Uh Oh! something is missing";
        }
    }

}