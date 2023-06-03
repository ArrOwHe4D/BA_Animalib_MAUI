using System.Collections.ObjectModel;
using System.Text.Json;
using Animalib.DataModels;
using Animalib.Pages;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Animalib.ViewModels
{
    public partial class AnimalsPageViewModel : ObservableObject
    {
        public AnimalsPageViewModel() 
        {
            client = new HttpClient();

            serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            animals = new ObservableCollection<Animal>();
            getAnimalDataAsync();          
        }

        HttpClient client;
        JsonSerializerOptions serializerOptions;

        [ObservableProperty]
        public ObservableCollection<Animal> animals;        

        [RelayCommand]
        public async Task GoToAnimalDetailPageAsync(Animal animal) 
        {
            if (animal == null) 
            {
                return;
            }

            await Shell.Current.GoToAsync($"{nameof(AnimalDetailPage)}", true, new Dictionary<string, object> { { "Animal", animal } });
        }

        public async void getAnimalDataAsync()
        {            
            try
            {
                var Uri = new Uri(string.Format("http://192.168.178.51:57565/api/Animals", string.Empty));
                HttpResponseMessage response = await client.GetAsync(Uri);
                
                if (response.IsSuccessStatusCode)
                {
                    string jsonResult = await response.Content.ReadAsStringAsync();
                    
                    var objectList = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(jsonResult);

                    foreach(var item in objectList ) 
                    {
                        Animals.Add(new Animal(
                        Convert.ToString(item["id"]),
                        Convert.ToString(item["name"]),
                        Convert.ToString(item["height"]),
                        Convert.ToString(item["weight"]),
                        Convert.ToString(item["regions"]),
                        Convert.ToString(item["species"]),
                        Convert.ToString(item["description"]),
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
