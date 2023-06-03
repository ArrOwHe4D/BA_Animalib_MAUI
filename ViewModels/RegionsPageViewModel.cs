using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using System.Text.Json;
using CommunityToolkit.Mvvm.ComponentModel;
using Region = Animalib.DataModels.Region;

namespace Animalib.ViewModels
{
    public partial class RegionsPageViewModel : ObservableObject
    {
        public RegionsPageViewModel() 
        {
            client = new HttpClient();

            serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            regions = new ObservableCollection<Region>();
            getRegionDataAsync();
        }

        HttpClient client;
        JsonSerializerOptions serializerOptions;

        [ObservableProperty]
        public ObservableCollection<Region> regions;

        public async void getRegionDataAsync()
        {
            try
            {
                var Uri = new Uri(string.Format("http://192.168.178.51:57565/api/Regions", string.Empty));
                HttpResponseMessage response = await client.GetAsync(Uri);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResult = await response.Content.ReadAsStringAsync();

                    var objectList = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(jsonResult);

                    foreach (var item in objectList)
                    {
                        Regions.Add(new Region(
                        Convert.ToString(item["name"]),
                        Convert.ToString(item["size"]),
                        int.Parse(Convert.ToString(item["speciesCount"])),
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
