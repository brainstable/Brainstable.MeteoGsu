namespace Brainstable.Filial.Location
{
    public class District
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public PlotCollection Plots { get; set; } = new PlotCollection();

        public override string ToString()
        {
            return Name;
        }
    }
}