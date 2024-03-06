namespace Brainstable.Meteo
{
    public class WeatherMonth : IWeatherParameters
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public double? Temperature { get; set; }
        public double? MinTemperature { get; set; }
        public double? MaxTemperature { get; set; }
        public double? Rainfall { get; set; }
        public double? SnowHight { get; set; }
        public double? Humidity { get; set; }
        public double? DewPoint { get; set; }

        public string TableName => monthRu[Month - 1];

        private string[] monthRu =
        {
            "Янв", "Фев", "Мар", "Апр", "Май", "Июн", "Июл", "Авг", "Сен", "Окт", "Ноя", "Дек"
        };

        public override string ToString()
        {
            return $"{Year}-{Month}";
        }
    }
}