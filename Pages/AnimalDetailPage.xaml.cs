using Animalib.ViewModels;

namespace Animalib.Pages
{
    public partial class AnimalDetailPage : ContentPage
    {
        public AnimalDetailPage(AnimalDetailPageViewModel animalDetailPageViewModel)
        {
            InitializeComponent();
            BindingContext = animalDetailPageViewModel;
        }

        protected override void OnNavigatedTo(NavigatedToEventArgs args)
        {
            base.OnNavigatedTo(args);
        }
    }
}
