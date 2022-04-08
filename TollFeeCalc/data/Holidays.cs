using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TollFeeCalculatorNET_original.data
{
    public class Holidays
    {
        /**
         * Returns all holidays (red days) for the given year
         */
        public List<DateTime> GetYear(int year)
        {
            DateTime easterDate = GetEasterDate(year: year);
            List<DateTime> holidays = new List<DateTime>()
            {
                easterDate, easterDate.AddDays(-1), easterDate.AddDays(-2), easterDate.AddDays(-3),
                easterDate.AddDays(1), easterDate.AddDays(6)
            };
            HolidayRules pingstDayRules = new HolidayRules(easterDate, DayOfWeek.Sunday, 8);
            HolidayRules ascentionsDayRules =
                new HolidayRules(easterDate, DayOfWeek.Thursday, 6);
            //DateTime pingstDay = FindWeekdayInInterval(pingstDayRules);
            DateTime pingstDay = easterDate.AddDays(50);
            DateTime pingstEve = pingstDay.AddDays(-1);
            DateTime ascentionDay =
                FindWeekdayInInterval(ascentionsDayRules);
            holidays.Add(pingstDay);
            holidays.Add(pingstEve);
            holidays.Add(ascentionDay);
            holidays.Add(ascentionDay.AddDays(-1));
            holidays = AddFixed(holidays);
            holidays = AddSemiFixed(holidays);

            return holidays;
        }

        /**
         * Adds holidays that repeats at the same day every year
         * @param holidays - The list that will add holidays into
         */
        private List<DateTime> AddFixed(List<DateTime> holidays)
        {
            int year = holidays[0].Year;
            holidays.Add(new DateTime(year, 4, 30));
            holidays.Add(new DateTime(year, 5, 1));
            holidays.Add(new DateTime(year, 6, 6));
            holidays.Add(new DateTime(year, 1, 1));
            holidays.Add(new DateTime(year, 1, 5));
            holidays.Add(new DateTime(year, 1, 6));
            holidays.Add(new DateTime(year, 12, 24));
            holidays.Add(new DateTime(year, 12, 25));
            holidays.Add(new DateTime(year, 12, 26));
            holidays.Add(new DateTime(year, 12, 31));
            return holidays;
        }

        /**
         * Calculates easter that is explained here https://www.tondering.dk/claus/cal/easter.php
         */
        private DateTime GetEasterDate(int year)
        {
            int recurringYear = year % 19;
            int epoch = year / 100;
            int c = (epoch - (epoch / 4) - ((8 * epoch + 13) / 25) + (19 * recurringYear) + 15) % 30;
            int d = c - (c / 28) * (1 - (c / 28) * (29 / (c + 1)) * ((21 - recurringYear) / 11));
            int e = d - ((year + (year / 4) + d + 2 - epoch + (epoch / 4)) % 7);
            int month = 3 + ((e + 40) / 44);
            int day = e + 28 - (31 * (month / 4));
            return new DateTime(year, month, day);
        }

        /**
         * Adds holidays that repeats in the same period at the same weekday every year
         * @param holidays - The list that will add holidays into
         */
        private List<DateTime> AddSemiFixed(List<DateTime> holidays)
        {
            int year = holidays[0].Year;
            List<HolidayRules> holidayRules = new List<HolidayRules>();
            holidayRules.Add(new HolidayRules(year, 6, 19, DayOfWeek.Friday));
            holidayRules.Add(new HolidayRules(year, 6, 20, DayOfWeek.Saturday));
            holidayRules.Add(new HolidayRules(year, 10, 30, DayOfWeek.Friday));
            holidayRules.Add(new HolidayRules(year, 10, 31, DayOfWeek.Saturday));
            foreach (HolidayRules rules in holidayRules)
            {
                DateTime holidayDate = FindWeekdayInInterval(rules);
                holidays.Add(holidayDate);
            }

            return holidays;
        }

        /**
         * Will loop until it finds the holiday
         * @param rules - how the holidays reappear each year
         */
        private DateTime FindWeekdayInInterval(HolidayRules rules)
        {
            DateTime current = rules.First;
            if (rules.RequiredHits == 1 && rules.First.DayOfWeek == rules.WeekDay)
            {
               return current;
            }
            int numHits = 0;
            while (numHits != rules.RequiredHits)
            {
                current = current.AddDays(1);
                DayOfWeek currentDayOfWeek = current.DayOfWeek;
                if (currentDayOfWeek == rules.WeekDay)
                {
                    numHits++;
                }
            }

            return current;
        }
    }

    class HolidayRules
    {
        public int RequiredHits { get; }
        public DayOfWeek WeekDay { get; }
        public DateTime First { get; }

        public HolidayRules(int year, int firstMonth, int firstDayOfMonth,
            DayOfWeek weekDay,
            int requiredHits = 1)
        {
            RequiredHits = requiredHits;
            WeekDay = weekDay;
            First = new DateTime(year, firstMonth, firstDayOfMonth);
        }

        public HolidayRules(DateTime first, DayOfWeek weekDay, int requiredHits = 1)
        {
            RequiredHits = requiredHits;
            WeekDay = weekDay;
            First = first;
        }
    }
}