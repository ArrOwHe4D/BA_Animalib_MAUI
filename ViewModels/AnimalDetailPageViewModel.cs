using Animalib.DataModels;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Animalib.ViewModels
{
    [QueryProperty("Animal", "Animal")]
    public partial class AnimalDetailPageViewModel : ObservableObject
    {
        public AnimalDetailPageViewModel() 
        {

        }

        [ObservableProperty]
        public Animal animal;
    }
}
