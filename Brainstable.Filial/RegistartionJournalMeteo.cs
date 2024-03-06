using System;
using System.Collections.Generic;
using System.Linq;
using Brainstable.Filial.Location;
using Brainstable.Meteo;

namespace Brainstable.Filial
{
    internal class RegistartionJournalMeteo : RegistrationJournal<MeteoPlot>
    {


        public static RegistartionJournalMeteo RegistartionPlots(List<Plot> plots, List<MeteoStation> stations)
        {
            RegistartionJournalMeteo meteoRegistration = new RegistartionJournalMeteo();
            // Бейский
            meteoRegistration.RegistrationNotes[62] = new MeteoPlot
            {
                Plot = plots.FirstOrDefault(p => p.Id == 62),
                MeteoStation = stations.FirstOrDefault(s => s.Id == "29962"),
                Distance = 0,
                Id = Guid.NewGuid()
            };

            // Боградский
            meteoRegistration.RegistrationNotes[64] = new MeteoPlot
            {
                Plot = plots.FirstOrDefault(p => p.Id == 64),
                MeteoStation = stations.FirstOrDefault(s => s.Id == "29761"),
                Distance = 40,
                Id = Guid.NewGuid()
            };

            //Дзержинский
            meteoRegistration.RegistrationNotes[65] = new MeteoPlot
            {
                Plot = plots.FirstOrDefault(p => p.Id == 65),
                MeteoStation = stations.FirstOrDefault(s => s.Id == "29481"),
                Distance = 0,
                Id = Guid.NewGuid()
            };

            // Казачинский
            meteoRegistration.RegistrationNotes[69] = new MeteoPlot
            {
                Plot = plots.FirstOrDefault(p => p.Id == 69),
                MeteoStation = stations.FirstOrDefault(s => s.Id == "29374"),
                Distance = 28,
                Id = Guid.NewGuid()
            };

            // Канский (закрыт)
            meteoRegistration.RegistrationNotes[70] = new MeteoPlot
            {
                Plot = plots.FirstOrDefault(p => p.Id == 70),
                MeteoStation = stations.FirstOrDefault(s => s.Id == "29581"),
                Distance = 0,
                Id = Guid.NewGuid()
            };

            // Каратузский
            meteoRegistration.RegistrationNotes[71] = new MeteoPlot
            {
                Plot = plots.FirstOrDefault(p => p.Id == 71),
                MeteoStation = stations.FirstOrDefault(s => s.Id == "29874"),
                Distance = 6,
                Id = Guid.NewGuid()
            };

            // Краснотуранский
            meteoRegistration.RegistrationNotes[73] = new MeteoPlot
            {
                Plot = plots.FirstOrDefault(p => p.Id == 73),
                MeteoStation = stations.FirstOrDefault(s => s.Id == "29766"),
                Distance = 26,
                Id = Guid.NewGuid()
            };

            // Минусинский
            meteoRegistration.RegistrationNotes[76] = new MeteoPlot
            {
                Plot = plots.FirstOrDefault(p => p.Id == 76),
                MeteoStation = stations.FirstOrDefault(s => s.Id == "29866"),
                Distance = 29,
                Id = Guid.NewGuid()
            };

            // Назаровский
            meteoRegistration.RegistrationNotes[77] = new MeteoPlot
            {
                Plot = plots.FirstOrDefault(p => p.Id == 77),
                MeteoStation = stations.FirstOrDefault(s => s.Id == "29561"),
                Distance = 41,
                Id = Guid.NewGuid()
            };

            // Новоселовский
            meteoRegistration.RegistrationNotes[78] = new MeteoPlot
            {
                Plot = plots.FirstOrDefault(p => p.Id == 78),
                MeteoStation = stations.FirstOrDefault(s => s.Id == "29664"),
                Distance = 13,
                Id = Guid.NewGuid()
            };

            // Саянский
            meteoRegistration.RegistrationNotes[79] = new MeteoPlot
            {
                Plot = plots.FirstOrDefault(p => p.Id == 79),
                MeteoStation = stations.FirstOrDefault(s => s.Id == "29676"),
                Distance = 0,
                Id = Guid.NewGuid()
            };

            // Сухобузимский
            meteoRegistration.RegistrationNotes[81] = new MeteoPlot
            {
                Plot = plots.FirstOrDefault(p => p.Id == 81),
                MeteoStation = stations.FirstOrDefault(s => s.Id == "29477"),
                Distance = 16,
                Id = Guid.NewGuid()
            };

            // Ужурский
            meteoRegistration.RegistrationNotes[85] = new MeteoPlot
            {
                Plot = plots.FirstOrDefault(p => p.Id == 85),
                MeteoStation = stations.FirstOrDefault(s => s.Id == "29653"),
                Distance = 21,
                Id = Guid.NewGuid()
            };

            // Уярский
            meteoRegistration.RegistrationNotes[87] = new MeteoPlot
            {
                Plot = plots.FirstOrDefault(p => p.Id == 87),
                MeteoStation = stations.FirstOrDefault(s => s.Id == "29576"),
                Distance = 10,
                Id = Guid.NewGuid()
            };

            // Ширинский
            meteoRegistration.RegistrationNotes[88] = new MeteoPlot
            {
                Plot = plots.FirstOrDefault(p => p.Id == 88),
                MeteoStation = stations.FirstOrDefault(s => s.Id == "29756"),
                Distance = 28,
                Id = Guid.NewGuid()
            };

            // Шушенский овощной
            meteoRegistration.RegistrationNotes[89] = new MeteoPlot
            {
                Plot = plots.FirstOrDefault(p => p.Id == 89),
                MeteoStation = stations.FirstOrDefault(s => s.Id == "29869"),
                Distance = 37,
                Id = Guid.NewGuid()
            };

            // Шушенский плодово-ягодный
            meteoRegistration.RegistrationNotes[728] = new MeteoPlot
            {
                Plot = plots.FirstOrDefault(p => p.Id == 728),
                MeteoStation = stations.FirstOrDefault(s => s.Id == "29869"),
                Distance = 39,
                Id = Guid.NewGuid()
            };

            // Пий-Хемский
            meteoRegistration.RegistrationNotes[672] = new MeteoPlot
            {
                Plot = plots.FirstOrDefault(p => p.Id == 672),
                MeteoStation = stations.FirstOrDefault(s => s.Id == "36092"),
                Distance = 0,
                Id = Guid.NewGuid()
            };

            // Восточно-сибирский
            meteoRegistration.RegistrationNotes[803] = new MeteoPlot
            {
                Plot = plots.FirstOrDefault(p => p.Id == 803),
                MeteoStation = stations.FirstOrDefault(s => s.Id == "29467"),
                Distance = 7,
                Id = Guid.NewGuid()
            };

            // Уярский (Емельяново)
            meteoRegistration.RegistrationNotes[10001] = new MeteoPlot
            {
                Plot = plots.FirstOrDefault(p => p.Id == 10001),
                MeteoStation = stations.FirstOrDefault(s => s.Id == "29570"),
                Distance = 7,
                Id = Guid.NewGuid()
            };

            return meteoRegistration;
        }
    
    }
}