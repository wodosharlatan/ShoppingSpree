using API_Wrapper;
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
            CreateAPIinstance();
        }

        private async void CreateAPIinstance()
        {
            string res = await LoadMauiAsset();
            api = new API(res);

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
            int entryId = entryIDs[listIndex]; // Get the ID of the entry

            Label Count = new Label
            {
                Text = productNames[listIndex].ToString() + ";",
                TextColor = Color.Parse("#000000"),
            };

            Label Unit = new Label
            {
                Text = units[listIndex].ToString() + ";",
                TextColor = Color.Parse("#000000"),
            };

            Label ProductName = new Label
            {
                Text = counts[listIndex].ToString() + ";",
                TextColor = Color.Parse("#000000"),
            };

            Label Time = new Label
            {
                Text = addedBy[listIndex].ToString() + ";",
                TextColor = Color.Parse("#000000"),
            };

            Label Added = new Label
            {
                Text = timeStamps[listIndex].ToString() + ";",
                TextColor = Color.Parse("#000000"),
            };

            CheckBox checkBox = new CheckBox
            {

            };

            checkBox.CheckedChanged += async (sender, e) =>
            {
                if (checkBox.IsChecked)
                {
                    await RemoveEntryById(entryId); // Pass the entry ID to the removal method

                }
            };

            HorizontalStackLayout newStack = new HorizontalStackLayout();
            newStack.Children.Add(Count);
            newStack.Children.Add(Unit);
            newStack.Children.Add(ProductName);
            newStack.Children.Add(Time);
            newStack.Children.Add(Added);
            newStack.Children.Add(checkBox);

            EntryLayout.Children.Add(newStack);
        }


        private async void NewProduct_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Products(userToken));
        }

       
        public async Task UpdateVisualState()
        {
            await LoadData(userToken);

            EntryLayout.Children.Clear(); // Clear previous entries

            for (int i = entryIDs.Count - 1; i >= 0; i--)
            {
                CreateVisualEntry(i);
            }
        }

        public async Task<int> LoadData(string token)
        {
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

        private async void body_Focused(object sender, FocusEventArgs e)
        {
            await UpdateVisualState();
        }


    }
}
