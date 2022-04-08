using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TollFeeCalculatorNET_original.data;
using TollFeeCalculatorNET_original.models;

namespace TollFeeCalculatorNET_original
{
    static class Program
    {
        static void Main(string[] args)
        {
            TestFees();
            //TestDates2022();
        }

        static void TestFees()
        {
            DateTime start = new DateTime(2012, 10, 25, 7, 26, 50);
            DateTime middle = new DateTime(2012, 10, 25, 13, 30, 50);
            DateTime middleLeft = new DateTime(2012, 10, 25, 16, 30, 50);
            DateTime end = new DateTime(2012, 10, 25, 17, 30, 50);

            DateTime[] parkingPeriod = { start, middleLeft, middle, end };
            Car car = new Car();
            TollCalculator calculator = new TollCalculator();
            int feeResult = calculator.GetTollFee(car, parkingPeriod);
            Console.WriteLine(feeResult);
        }

        static void TestDates2022()
        {
            Holidays holidays = new Holidays();
            List<DateTime> holidaysList = holidays.GetYear(2022);
            holidaysList.Sort();
            DateTime date = new DateTime(2012, 12, 25, 7, 26, 50);
            List<DateTime> filteredHolidays = new List<DateTime>();
            foreach (DateTime dateTime in holidaysList)
            {
                if (dateTime.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday)
                {
                }
                else
                {
                    filteredHolidays.Add(dateTime);
                }
            }

            bool exist = holidaysList.Any(d => d.Month == date.Month && d.Day == date.Day);
            Console.WriteLine(exist);
        }
    }
}