using System;
using System.Collections.Generic;
using System.Linq;

namespace Brainstable.Meteo
{
    public class TransitionTemperature
    {
        private readonly DateTime? firstDate;
        private readonly DateTime? lastDate;
        public DateTime MinDate { get; set; }
        public DateTime MaxDate { get; set; }
        
        public int Year { get; private set; }

        public DateTime? FirstDate
        {
            get
            {
                if (!firstDate.HasValue)
                    return MinDate;
                return firstDate;
            }
        }

        public DateTime? LastDate
        {
            get
            {
                if (!lastDate.HasValue)
                    return MaxDate;
                return lastDate;
            }
        }

        public int CountDays { get; set; }

        public string FirstDateString => FirstDate.HasValue ? $"{FirstDate.Value.Day.ToString("D2")}.{FirstDate.Value.Month.ToString("D2")}" : "";
        public string LastDateString => LastDate.HasValue ? $"{LastDate.Value.Day.ToString("D2")}.{LastDate.Value.Month.ToString("D2")}" : "";

        public double SumRainfalls { get; set; } = 0;
        public double SumTemperatures { get; set; } = 0;

        public TransitionTemperature(int year, DateTime? first, DateTime? last)
        {
            Year = year;
            if (first.HasValue)
            {
                firstDate = new DateTime(first.Value.Year,
                    first.Value.Month,
                    first.Value.Day);
            }


            if (last.HasValue)
            {
                lastDate = new DateTime(last.Value.Year,
                    last.Value.Month,
                    last.Value.Day);
            }

            DateTime jan = new DateTime(year, 1, 1);
            DateTime dec = new DateTime(year, 12, 31);
            
            /*
            bool isCurrentYear = DateTime.Now.Year == year;

            if (first.HasValue)
            {
                if (!last.HasValue)
                {
                    if (isCurrentYear)
                    {
                        CountDays = (DateTime.Now - first.Value).Days;
                    }
                    else
                    {
                        //CountDays = (dec - first.Value).Days;
                        CountDays = -1;
                    }
                }
                else
                {
                    CountDays = (last.Value - first.Value).Days;
                }
            }
            else
            {
                if (last.HasValue)
                {
                    //CountDays = (last.Value - jan).Days;
                    CountDays = -1;
                }
                else
                {
                    CountDays = -1;
                }
            }
            */

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime CreateAverageDate(List<DateTime> date)
        {
            string dateString = String.Empty;
            List<int> orderDays = new List<int>();
            foreach (var dt in date)
            {
                //var startDayYear = new DateTime(dt.Year, 1, 1);
                //var dayNumber = (int)(dt - startDayYear).TotalDays;

                orderDays.Add(dt.DayOfYear);
            }

            int avr = (int) orderDays.Average();
            int year = DateTime.Now.Year;
            DateTime d = new DateTime(year - 1, 12, 31).AddDays(avr);
            
            return d;
        }


        public static TransitionTemperature CreateTransitionTemperature(int year, List<WeatherDecade> decades, List<WeatherDay> days, double temperatureTransition)
        {
            DateTime?[] date = Algorithm.GetDateRun(temperatureTransition, year, decades);
            DateTime? first = date[0];
            DateTime? last = date[1];



            TransitionTemperature transition = new TransitionTemperature(year, first, last);
            transition.MinDate = days.Min(x => x.DateTime);
            transition.MaxDate = days.Max(x => x.DateTime);
            DateTime firstDate = transition.MinDate, lastDate = transition.MaxDate;


            if (first.HasValue)
            {
                firstDate = first.Value;
            }
            if (last.HasValue)
            {
                lastDate = last.Value;
            }

            transition.CountDays = days
                .Where(d => d.DateTime >= firstDate && d.DateTime <= lastDate && d.Temperature >= temperatureTransition)
                .Count();
            transition.SumTemperatures = days
                .Where(d => d.DateTime >= firstDate && d.DateTime <= lastDate && d.Temperature >= temperatureTransition)
                .Sum(d => d.Temperature).Value;
            transition.SumRainfalls = days
                .Where(d => d.DateTime >= firstDate && d.DateTime <= lastDate)
                .Sum(d => d.Rainfall).Value;
            return transition;
        }

       
    }
}
