using System;

namespace Brainstable.Meteo
{
    public static class ExtensionMethods
    {
        public static int GetDecade(this DateTime dt)
        {
            int day = dt.Day;
            int decade = 2;
            if (day <= 10)
                decade = 1;
            if (day >= 21)
                decade = 3;
            return decade;
        }

        
    }
}
