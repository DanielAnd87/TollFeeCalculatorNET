using System;
using System.Collections.Generic;
using System.Linq;
using TollFeeCalculatorNET_original.data;

namespace TollFeeCalculatorNET_original
{
    public static class DayChecker
    {
        /**
         * Checks if the date is toll free, example hollidays and weekends
         * @param date - date to check
         */
        public static bool IsTollFree(DateTime date)
        {
            if (date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday)
            {
                return true;
            };
            if (date.Month is 7) // Checking if month is July
            {
                return true;
            }
            Holidays holidays = new Holidays();
            List<DateTime> holidaysList = holidays.GetYear(date.Year);
            bool exist = holidaysList.Any(d => d.Month == date.Month && d.Day == date.Day);
            return exist;
        }
    }
}