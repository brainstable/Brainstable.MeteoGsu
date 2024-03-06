using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainstable.Filial.Location
{
    public class Plot
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public int DistrictId { get; set; } = -1;
        public District District { get; set; }
        public string GenetiveName { get; set; }
        public string DativeName { get; set; }
        public string AblativeName { get; set; }


        public override string ToString()
        {
            return Name;
        }
    }
}
