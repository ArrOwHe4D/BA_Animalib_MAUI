using System.Collections.ObjectModel;
using System.Text.Json;
using Animalib.DataModels;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Animalib.ViewModels
{
    public partial class SpeciesPageViewModel : ObservableObject
    {
        public SpeciesPageViewModel()
        {
            client = new HttpClient();

            serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            species = new ObservableCollection<Species>();
            getSpeciesDataAsync();
        }

        HttpClient client;
        JsonSerializerOptions serializerOptions;

        [ObservableProperty]
        public ObservableCollection<Species> species;

        public async void getSpeciesDataAsync()
        {
            try
            {
                var Uri = new Uri(string.Format("http://192.168.178.51:57565/api/Species", string.Empty));
                HttpResponseMessage response = await client.GetAsync(Uri);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResult = await response.Content.ReadAsStringAsync();

                    var objectList = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(jsonResult);

                    foreach (var item in objectList)
                    {
                        Species.Add(new Species(
                        Convert.ToString(item["name"]),
                        Convert.ToString(item["type"]),
                        int.Parse(Convert.ToString(item["animalCount"])),
                        (StreamImageSource)ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(Convert.ToString(item["image"]))))));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await Toast.Make("Could not retrieve data from the REST Service!", ToastDuration.Long).Show();
            }
        }
    }
}
