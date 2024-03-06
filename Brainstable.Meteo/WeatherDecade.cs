using System;

namespace Brainstable.Meteo
{
    public class WeatherDecade : IWeatherParameters
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Decade { get; set; }

        public int NumberInYear => (Month - 1) * 3 + Decade;

        public int CountDay
        {
            get { return GetCountDay(Year, Month, NumberInYear); }
        }

        public DateTime Middle
        {
            get { return GetMiddleDay(Year, Month, NumberInYear); }
        }

        public double? Temperature { get; set; }
        public double? MinTemperature { get; set; }
        public double? MaxTemperature { get; set; }
        public double? Rainfall { get; set; }
        public double? SnowHight { get; set; }
        public double? Humidity { get; set; }
        public double? DewPoint { get; set; }

        

        public string TableName 
        {
            get { return monthRu[Month - 1] + " " + Decade; }
        }

        private string[] monthRu =
        {
            "Янв", "Фев", "Мар", "Апр", "Май", "Июн", "Июл", "Авг", "Сен", "Окт", "Ноя", "Дек"
        };

        public override string ToString()
        {
            return $"{Year}-{Month}-{Decade}";
        }

        public static int GetCountDay(int year, int month, int numberDecadeInYear)
        {
            int countDay = 0;
            if (numberDecadeInYear % 3 != 0)
            {
                countDay = 10;
            }
            else
            {
                if (numberDecadeInYear == 6)
                {
                    if (DateTime.IsLeapYear(year))
                    {
                        countDay = 29;
                    }
                    else
                    {
                        countDay = 28;
                    }
                }

                switch (month)
                {
                    case 1:
                    case 3:
                    case 5:
                    case 7:
                    case 8:
                    case 10:
                    case 12:
                        countDay = 11;
                        break;
                    case 4:
                    case 6:
                    case 9:
                    case 11:
                        countDay = 10;
                        break;
                }
            }

            return countDay;
        }

        public static DateTime GetMiddleDay(int year, int month, int numberInYear)
        {
            DateTime middle = new DateTime();
            if (numberInYear >= 3)
            {
                switch (numberInYear % 3)
                {
                    case 0:
                        middle = new DateTime(year, month, 25);
                        break;
                    case 1:
                        middle = new DateTime(year, month, 5);
                        break;
                    case 2:
                        middle = new DateTime(year, month, 15);
                        break;
                }
            }
            else if (numberInYear == 1)
            {
                middle = new DateTime(year, month, 5);
            }
            else if (numberInYear == 2)
            {
                middle = new DateTime(year, month, 15);
            }

            if (month == 2)
            {
                middle = new DateTime(year, month, 24);
            }
            return middle;
        }



        public static int GetNumberMonth(int numberDecadeInYear)
        {
            if (numberDecadeInYear <= 3)
                return 1;
            if (numberDecadeInYear <= 6)
                return 2;
            if (numberDecadeInYear <= 9)
                return 3;
            if (numberDecadeInYear <= 12)
                return 4;
            if (numberDecadeInYear <= 15)
                return 5;
            if (numberDecadeInYear <= 18)
                return 6;
            if (numberDecadeInYear <= 21)
                return 7;
            if (numberDecadeInYear <= 24)
                return 8;
            if (numberDecadeInYear <= 27)
                return 9;
            if (numberDecadeInYear <= 30)
                return 10;
            if (numberDecadeInYear <= 33)
                return 11;
            if (numberDecadeInYear <= 36)
                return 12;
            return -1;
        }

        public static int GetNumberDecadeInMonth(int numberDecadeInYear)
        {
            int M = GetNumberMonth(numberDecadeInYear);
            return M * (M - 1) - numberDecadeInYear;
        }

        public static int NumberDecadeInYear(DateTime date)
        {
            int month = date.Month;
            int day = date.Day;

            int number = 0;

            number = (month - 1) * 3;

            if (day <= 10)
            {
                number += 1;
            }
            else if (day > 20)
            {
                number += 3;
            }
            else
            {
                number += 2;
            }

            return number;
        }


    }
}