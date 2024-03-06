using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Brainstable.Filial
{
    class InitMap
    {
        private Uri uri;
        private Dictionary<int, string> dictSetViewMaps;

        public Uri Uri => uri;

        public InitMap()
        {
            dictSetViewMaps = new Dictionary<int, string>();
            dictSetViewMaps[0] = "map.setView([55.08, 95.229999999999819], 6)";
            dictSetViewMaps[803] = "map.setView([56.33, 90.51], 12)";
            dictSetViewMaps[65] = "map.setView([56.8, 95.23], 12)";
            dictSetViewMaps[70] = "map.setView([56.11, 95.7], 11)";
            dictSetViewMaps[69] = "map.setView([57.65, 93.1], 11)";
            dictSetViewMaps[71] = "map.setView([53.62, 92.80], 12)";
            dictSetViewMaps[73] = "map.setView([54.26, 92.15], 11)";
            dictSetViewMaps[76] = "map.setView([53.71, 91.95], 11)";
            dictSetViewMaps[77] = "map.setView([55.9, 90.2], 10)";
            dictSetViewMaps[78] = "map.setView([55.08, 90.91], 12)";
            dictSetViewMaps[79] = "map.setView([55.27, 94.95], 12)";
            dictSetViewMaps[81] = "map.setView([56.51, 93.15], 11)";
            dictSetViewMaps[85] = "map.setView([55.24, 89.810000000001892], 11)";
            dictSetViewMaps[88] = "map.setView([54.5, 89.930000000001073], 11)";
            dictSetViewMaps[87] = "map.setView([55.81, 94.39], 12)";
            dictSetViewMaps[672] = "map.setView([52.14, 93.91], 11)";
            dictSetViewMaps[89] = "map.setView([53.27, 92.15], 11)";
            dictSetViewMaps[728] = "map.setView([53.17, 92.15], 11)";
            dictSetViewMaps[10001] = "map.setView([56.03, 92.75], 11)";

        }

        public Uri GetUriByPlotId(int plotId)
        {
            string applicationDirectory = Path.GetDirectoryName(Application.ExecutablePath);
            string myFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), $"geo\\{plotId}.html");
            uri = new Uri("file:///" + myFile);
            return uri;
        }

        public Uri GetUriByPlotId()
        {
            string applicationDirectory = Path.GetDirectoryName(Application.ExecutablePath);
            string myFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), $"geo\\index.html");
            uri = new Uri("file:///" + myFile);
            return uri;
        }
    }
}
