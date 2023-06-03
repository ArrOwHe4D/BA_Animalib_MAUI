namespace Animalib.DataModels
{
    public class Species
    {
        public Species(string name, string speciestype, int animalcount, StreamImageSource image) 
        {
            this.Name = name;
            this.SpeciesType = speciestype;
            this.AnimalCount = animalcount;
            this.Image = image;
        }        

        public string Name { get; set; }

        public string SpeciesType { get; set; }

        public int AnimalCount { get; set; }

        public ImageSource Image { get; set; }
    }
}
