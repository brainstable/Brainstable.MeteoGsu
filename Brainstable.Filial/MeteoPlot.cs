using System;
using Brainstable.Filial.Location;
using Brainstable.Meteo;

namespace Brainstable.Filial
{
    internal class MeteoPlot
    {
        public Plot Plot { get; set; }
        public MeteoStation MeteoStation { get; set; }
        public double Distance { get; set; }
        public Guid Id { get; set; }
    }
}
