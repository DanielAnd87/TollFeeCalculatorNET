using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TollFeeCalculatorNET_original.data;

namespace TollFeeCalculatorNET_originalTests
{
    [TestClass()]
    public class DayCheckerTests
    {
        [DataTestMethod]  
        [DataRow(2022,6, 6, true)]  
        [DataRow(2022,1, 6, true)]  
        [DataRow(2019,1, 5, true)]  
        [DataRow(2020,12, 26, true)]  
        [DataRow(2024,12, 25, true)]  
        [DataRow(2017,12, 31, true)]  
        public void CheckFixedHolidaysTest(int year, int month, int day, bool expectedResult)
        {
            Holidays holidays = new Holidays();
            List<DateTime> holidaysList = holidays.GetYear(year);
            holidaysList.Sort();
            DateTime date = new DateTime(year, month, day);
            bool exist = holidaysList.Any(d => d.Month == date.Month && d.Day == date.Day);
            Console.WriteLine(exist);
            Assert.AreEqual(expectedResult, exist, "date check");
        }
        
        [DataTestMethod]  
        [DataRow(2021,4, 5, true)]  
        [DataRow(2022,4, 14, true)]  
        [DataRow(2022,4, 15, true)]  
        [DataRow(2022,4, 18, true)]  
        [DataRow(2022,5, 25, true)]
        [DataRow(2021,5, 13, true)]  
        [DataRow(2021,5, 23, true)]  
        [DataRow(2020,5, 31, true)]  
        [DataRow(2021,6, 25, true)]  
        [DataRow(2020,6, 20, true)]  
        [DataRow(2021,6, 26, true)]  
        [DataRow(2022,6, 25, true)]  
        [DataRow(2021,11, 6, true)]  
        [DataRow(2022,5, 26, true)]  
        [DataRow(2022,6, 24, true)]  
        [DataRow(2022,11, 4, true)]  
        [DataRow(2025,11, 1, true)]  
        public void VariableHolidaysTest(int year, int month, int day, bool expectedResult)
        {
            Holidays holidays = new Holidays();
            List<DateTime> holidaysList = holidays.GetYear(year);
            holidaysList.Sort();
            DateTime date = new DateTime(year, month, day);
            bool exist = holidaysList.Any(d => d.Month == date.Month && d.Day == date.Day);
            Console.WriteLine(exist);
            Assert.AreEqual(expectedResult, exist, "date check");
        }

    }
}
