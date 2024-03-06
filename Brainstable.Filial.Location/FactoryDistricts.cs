using System.Collections.Generic;
using System.Linq;

namespace Brainstable.Filial.Location
{
    public static class FactoryDistricts
    {
        public static List<District> CreateDistricts(List<Plot> plots)
        {
            List<District> districts = new List<District>();
            districts = new List<District>();
            districts.Add(new District { Id = 29, Name = "Красноярский край" });
            districts.Add(new District { Id = 69, Name = "Республика Хакасия" });
            districts.Add(new District { Id = 63, Name = "Республика Тыва" });

            foreach (District district in districts)
            {
                foreach (Plot plot in plots.Where(p => p.DistrictId == district.Id))
                {
                    district.Plots.Add(plot);
                }
            }


            return districts;
        }
    }
}