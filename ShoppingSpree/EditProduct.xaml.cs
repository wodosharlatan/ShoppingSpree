using API_Wrapper;
using Newtonsoft.Json.Linq;

namespace ShoppingSpree;

public partial class EditProduct : ContentPage
{
    public API api;
    public EditProduct(string token, int entryID)
    {
        InitializeComponent();
        CreateAPIinstance();
        GetProductInfo(token,entryID);
        
    }





    async void CreateAPIinstance()
    {
        string res = await LoadENVfile();
        api = new API(res);
    }


    async Task<string> LoadENVfile()
    {
        using var stream = await FileSystem.OpenAppPackageFileAsync("APIKEY.env");
        using var reader = new StreamReader(stream);

        var contents = reader.ReadToEnd();
        return contents;
    }

   




    public async void GetProductInfo(string token,int id)
    {
        while (api == null)
        {
            await Task.Delay(100);
        }

        string res = await api.GetSpecificEntry(token, id);

        JObject entry = JObject.Parse(res);
        var x = entry["ProductName"];
        var y = entry["Unit"];
        var z = entry["Count"];

        debug.Text = x.ToString() + " " + y.ToString() + " " + z.ToString();


    }




}