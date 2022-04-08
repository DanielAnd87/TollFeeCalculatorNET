using Microsoft.VisualStudio.TestTools.UnitTesting;
using TollFeeCalculatorNET_original;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TollFeeCalculatorNET_original.interfaces;
using TollFeeCalculatorNET_original.models;

namespace TollFeeCalculatorNET_original.Tests
{
    [TestClass()]
    public class TollCalculatorTests
    {
        [TestMethod]  
        public void VehicleTypeCostTest()
        {
            DateTime[] parkingPeriod = new DateTime[1];
            parkingPeriod[0] = new DateTime(2012, 10, 25, 7, 26, 50);
            TollCalculator calculator = new TollCalculator();
            Assert.AreEqual(22, calculator.GetTollFee(new Car(), parkingPeriod), "diplomat");
            Assert.AreEqual(22, calculator.GetTollFee(new Foreign(), parkingPeriod), "emergency");
        }
        
        [TestMethod]  
        public void VehicleTypeFreeTest()
        {
            DateTime[] parkingPeriod = new DateTime[1];
            parkingPeriod[0] = new DateTime(2012, 10, 25, 7, 26, 50);
            TollCalculator calculator = new TollCalculator();
            Assert.AreEqual(0, calculator.GetTollFee(new Diplomat(), parkingPeriod), "diplomat");
            Assert.AreEqual(0, calculator.GetTollFee(new Emergency(), parkingPeriod), "emergency");
            Assert.AreEqual(0, calculator.GetTollFee(new Military(), parkingPeriod), "military");
            Assert.AreEqual(0, calculator.GetTollFee(new Motorbike(), parkingPeriod), "motorbike");
            Assert.AreEqual(0, calculator.GetTollFee(new Tractor(), parkingPeriod), "tractor");
        }
        
        [DataTestMethod]  
        [DataRow(2012, 10, 25,new int[]{ 7}, new int[]{ 26 }, 22)]  
        [DataRow(2012, 10, 25,new int[]{ 17}, new int[]{ 2}, 16)]  
        public void SinglePassTest(int year, int month, int day, int[] startTimes , int[] startMinutes , int expectedResult)
        {
            DateTime[] parkingPeriod = new DateTime[startTimes.Length];
            for (int i = 0; i < startTimes.Length; i++)
            {
                DateTime currDt = new DateTime(year, month, day, startTimes[i], startMinutes[i], 50);
                parkingPeriod[i] = currDt;
            }
            Car car = new Car();
            TollCalculator calculator = new TollCalculator();
            int feeResult = calculator.GetTollFee(car, parkingPeriod);
            Console.WriteLine(feeResult);
            Assert.AreEqual(expectedResult, feeResult, "max daily fee");
        }
        
        [DataTestMethod]  
        [DataRow(2012, 10, 25,new int[]{ 7,13,16,17 }, new int[]{ 26,30,30,30 }, 60)]  
        [DataRow(2012, 10, 25,new int[]{ 7,13,16 }, new int[]{ 26,30,30}, 53)]  
        public void MultiblePassTest(int year, int month, int day, int[] startTimes , int[] startMinutes , int expectedResult)
        {
            DateTime[] parkingPeriod = new DateTime[startTimes.Length];
            for (int i = 0; i < startTimes.Length; i++)
            {
                DateTime currDt = new DateTime(year, month, day, startTimes[i], startMinutes[i], 50);
                parkingPeriod[i] = currDt;
            }
            Car car = new Car();
            TollCalculator calculator = new TollCalculator();
            int feeResult = calculator.GetTollFee(car, parkingPeriod);
            Console.WriteLine(feeResult);
            Assert.AreEqual(expectedResult, feeResult, "max daily fee");
        }
    }
}