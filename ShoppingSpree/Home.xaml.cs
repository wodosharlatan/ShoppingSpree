
using API_Wrapper;
using Microsoft.Maui.Controls.Shapes;
using Newtonsoft.Json.Linq;

namespace ShoppingSpree
{
    public partial class Home : ContentPage
    {
        private API api;
        public string userToken;


        private List<int> entryIDs = new List<int>();
        private List<string> productNames = new List<string>();
        private List<string> units = new List<string>();
        private List<int> counts = new List<int>();
        private List<string> addedBy = new List<string>();
        private List<string> timeStamps = new List<string>();


        public Home(string token)
        {
            InitializeComponent();
            userToken = token;
            CreateAPIinstanceAndLoadVisualElements();

        }

        private async void CreateAPIinstanceAndLoadVisualElements()
        {
            string res = await LoadMauiAsset();
            api = new API(res);

            while (true)
            {
                await UpdateVisualState();
                await Task.Delay(5000);
            }

        }

        private async Task<string> LoadMauiAsset()
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync("APIKEY.env");
            using var reader = new StreamReader(stream);

            return await reader.ReadToEndAsync();
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        public void CreateVisualEntry(int listIndex)
        {

            int entryId = entryIDs[listIndex];

            Label ProductName = new Label
            {
                Text = productNames[listIndex].ToString(),
                TextColor = Color.Parse("#000000"),
                FontSize = 16,
                FontAttributes = FontAttributes.Bold
            };

            Label Unit = new Label
            {
                Text = units[listIndex].ToString(),
                TextColor = Color.Parse("#000000"),
                FontSize = 16,
                FontAttributes = FontAttributes.Bold
            };

            Label Count = new Label
            {
                Text = counts[listIndex].ToString(),
                TextColor = Color.Parse("#000000"),
                FontSize = 16,
                FontAttributes = FontAttributes.Bold
            };


            HorizontalStackLayout MainText = new HorizontalStackLayout()
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Start,
                Spacing = 5
            };

            MainText.Children.Add(ProductName);
            MainText.Children.Add(Count);
            MainText.Children.Add(Unit);

            Label Time = new Label
            {
                Text = addedBy[listIndex].ToString(),
                TextColor = Color.Parse("#000000"),
                FontSize = 12
            };

            Label Added = new Label
            {
                Text = timeStamps[listIndex].ToString(),
                TextColor = Color.Parse("#000000"),
                FontSize = 12
            };


            HorizontalStackLayout AdditionalText = new HorizontalStackLayout()
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Start,
                Spacing = 5
            };

            AdditionalText.Children.Add(Added);
            AdditionalText.Children.Add(Time);



            VerticalStackLayout views = new VerticalStackLayout()
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            views.Children.Add(MainText);
            views.Children.Add(AdditionalText);

            CheckBox checkBox = new CheckBox
            {
                Color = Color.Parse("#000000")
            };

            checkBox.CheckedChanged += async (sender, e) =>
            {
                if (checkBox.IsChecked)
                {
                    await RemoveEntryById(entryId);
                }
            };

            HorizontalStackLayout mainHorizont = new HorizontalStackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Padding = 20,
                Spacing = 10,
                BackgroundColor = Color.Parse("#fcde6c")
            };

            mainHorizont.Children.Add(views);
            mainHorizont.Children.Add(checkBox);

            var border = new Frame
            {
                BorderColor = Color.Parse("#fcde6c"),
                BackgroundColor = Color.Parse("#fcde6c"),
                CornerRadius = 25,
                Padding = 2,
                Content = mainHorizont
            };

            var grid = new Grid();
            grid.Children.Add(border);

            var mainView = new StackLayout
            {
                Padding = 5,
                Spacing = 5,
                MinimumWidthRequest = 300,
                MaximumWidthRequest = 500,
            };
            mainView.Children.Add(grid);

            EntryLayout.Children.Add(mainView);
        }



        private async void NewProduct_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Products(userToken));
        }


        public async Task UpdateVisualState()
        {
            try
            {
                while (EntryLayout == null)
                {
                    await Task.Delay(100);
                }

                if (userToken != null)
                {
                    await LoadData(userToken);

                    EntryLayout.Children.Clear();

                    for (int i = entryIDs.Count - 1; i >= 0; i--)
                    {
                        CreateVisualEntry(i);
                    }
                }
            }
            catch (Exception ex) { }
        }



        public async Task<int> LoadData(string token)
        {
            while (api == null)
            {
                await Task.Delay(100);
            }

            string res = await api.GetAllEntries(token);
            JArray entries = JArray.Parse(res);

            entryIDs.Clear(); // Clear previous entry IDs
            productNames.Clear();
            units.Clear();
            counts.Clear();
            addedBy.Clear();
            timeStamps.Clear();

            for (int i = 0; i < entries.Count; i++)
            {
                var entry = entries[i];
                entryIDs.Add((int)entry["Entry_ID"]);
                productNames.Add((string)entry["ProductName"]);
                units.Add((string)entry["Unit"]);
                counts.Add((int)entry["Count"]);
                addedBy.Add((string)entry["AddedBy"]);
                timeStamps.Add((string)entry["TimeStamp"]);
            }

            return entryIDs.Count;
        }

        private async Task RemoveEntryById(int entryIdToRemove)
        {
            await api.DeleteSpecificEntry(userToken, entryIdToRemove);
            int indexToRemove = entryIDs.IndexOf(entryIdToRemove);
            if (indexToRemove >= 0)
            {
                entryIDs.RemoveAt(indexToRemove);
                productNames.RemoveAt(indexToRemove);
                units.RemoveAt(indexToRemove);
                counts.RemoveAt(indexToRemove);
                addedBy.RemoveAt(indexToRemove);
                timeStamps.RemoveAt(indexToRemove);
            }
            await UpdateVisualState();
        }
    }
}
