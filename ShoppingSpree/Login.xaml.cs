using API_Wrapper;
using Newtonsoft.Json.Linq;


namespace ShoppingSpree;

public partial class MainPage : ContentPage
{
    private API api;
    public bool SignUp = true;
    public string Username = "";
    public string Password = "";
    public string Confirmation = "";


    public MainPage()
	{
        CreateAPIinstance();
        InitializeComponent();
        
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


    



    private void SignUpButton_Clicked(object sender, EventArgs e)
	{
        SignUp = true;
        ConfirmPassword.IsVisible = true;
        SubtitleLabel.Text = "Sign up to join the shopping list";
        SubmitButton.Text = "Sign Up";
        SignUpButton.BackgroundColor = Color.Parse("#fcde6c");
        SignInButton.BackgroundColor = Color.Parse("#f6ffff");
    }

    private void SignInButton_Clicked(object sender, EventArgs e)
    {
		SignUp = false;
        ConfirmPassword.IsVisible = false;
        SubtitleLabel.Text = "Sign in to see content of the app";
        SubmitButton.Text = "Sign In";
        SignInButton.BackgroundColor = Color.Parse("#fcde6c");
        SignUpButton.BackgroundColor = Color.Parse("#f6ffff");
    }

    private async void SubmitButton_Clicked(object sender, EventArgs e)
    {
        if (SignUp == true && (!string.IsNullOrWhiteSpace(Confirmation) && !string.IsNullOrWhiteSpace(Password) && !string.IsNullOrWhiteSpace(Username)))
        {
            if (Confirmation.Trim() == Password.Trim())
            {
                try
                {
                    string res = await api.CreateNewUser(Username.Trim(), Password.Trim());

                    JObject responseJson = JObject.Parse(res);

                    if (responseJson.TryGetValue("message", out var message))
                    {
                        string messageText = message.ToString();
                        
                        if (messageText == "User saved successfully")
                        {
                            SubtitleLabel.Text = messageText;

                            string token = await api.Login(Username.Trim(), Password.Trim());

                            await Task.Delay(500);
                            await Navigation.PushAsync(new Home(token));

                        }
                        else
                        {
                            SubtitleLabel.Text = messageText;
                        }
                        
                    }
                    else
                    {
                        SubtitleLabel.Text = "Invalid response format.";
                    }
                }
                catch (Exception ex)
                {
                    SubtitleLabel.Text = "An error occurred: " + ex.Message;
                }
            }
       }
       else
       {

            if (!string.IsNullOrWhiteSpace(Password) && !string.IsNullOrWhiteSpace(Username))
            {
                try
                {
                    string res = await api.Login(Username.Trim(), Password.Trim());

                    SubtitleLabel.Text = res;
                    try
                    {

                        JObject responseJson = JObject.Parse(res);

                        if (responseJson.TryGetValue("message", out var message))
                        {
                            string messageText = message.ToString();
                            SubtitleLabel.Text = messageText;
                        }
                    }
                    catch(Exception ex)
                    {

                        SubtitleLabel.Text = "Logged in !";

                        await Navigation.PushAsync(new Home(res));
                    }
                }
                catch (Exception ex)
                {
                    SubtitleLabel.Text = "An error occurred: " + ex.Message;
                }
            } 
        }
    }

    private void ConfirmEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        Confirmation = ConfirmEntry.Text;
        
    }

    private void PasswordEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        Password = PasswordEntry.Text;
        
    }

    private void NameEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        Username = NameEntry.Text;
       
    }
}

