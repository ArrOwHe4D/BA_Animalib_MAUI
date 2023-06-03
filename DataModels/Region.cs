namespace Animalib.DataModels
{
    public class Region
    {
        public Region(string name, string size, int speciescount, StreamImageSource image) 
        {
            this.Name = name;
            this.Size = size;
            this.SpeciesCount = speciescount;
            this.Image = image;
        }

        public string Name { get; set; }

        public string Size { get; set; }

        public int SpeciesCount { get; set; }

        public ImageSource Image { get; set; }
    }
}
