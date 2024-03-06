

namespace Brainstable.Meteo
{
    public class MeteoStation
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public double? Longitude { get; set; }
        public double? Altutude { get; }
        public double? Latitude { get; set; }

        public string FriendName => $"{Id} - {Name}";

        public MeteoStation()
            : this("", "", "", null, null, null)
        {

        }

        public MeteoStation(string id, string name, string country)
            : this(id, name, country, null, null, null)
        {

        }

        public MeteoStation(string id, string name, string country, double? latitude, double? longitude, double? altutude)
        {
            Id = id;
            Name = name;
            Country = country;
            Latitude = latitude;
            Longitude = longitude;
            Altutude = altutude;
        }

        public override string ToString()
        {
            return FriendName;
        }
    }
}
