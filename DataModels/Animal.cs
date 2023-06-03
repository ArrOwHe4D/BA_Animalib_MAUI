namespace Animalib.DataModels
{
    public class Animal
    {
        public Animal(string id, string name, string height, string weight, string regions, string species, string description, StreamImageSource image) 
        {
            this.Id = id;
            this.Name = name;
            this.Height = height;
            this.Weight = weight;
            this.Regions = regions;
            this.Species = species;                        
            this.Description = description;
            this.Image = image;
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Height { get; set; }

        public string Weight { get; set; }

        public string Regions { get; set; }

        public string Species { get; set; }

        public string Description { get; set; }

        public ImageSource Image { get; set; }
    }
}
