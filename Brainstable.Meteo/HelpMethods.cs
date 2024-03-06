using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainstable.Meteo
{
    public static class HelpMethods
    {
        private static string[] decadeRu =
        {
            "Янв 1", "Янв 2", "Янв 3", "Фев 1", "Фев 2", "Фев 3", "Мар 1", "Мар 2", "Мар 3", "Апр 1", "Апр 2", "Апр 3",
            "Май 1", "Май 2", "Май 3", "Июн 1", "Июн 2", "Июн 3", "Июл 1", "Июл 2", "Июл 3", "Авг 1", "Авг 2", "Авг 3",
            "Сен 1", "Сен 2", "Сен 3", "Окт 1", "Окт 2", "Окт 3", "Ноя 1", "Ноя 2", "Ноя 3", "Дек 1", "Дек 2", "Дек 3"
        };

        public static string[] DecadeForNamesColumns => decadeRu;

        private static string[] monthRu =
        {
            "Янв", "Фев", "Мар", "Апр", "Май", "Июн", "Июл", "Авг", "Сен", "Окт", "Ноя", "Дек"
        };

        public static string[] MonthForNamesColumns => monthRu;



    }
}
