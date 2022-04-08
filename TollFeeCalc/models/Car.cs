using System;
using System.Reflection;
using TollFeeCalculatorNET_original.interfaces;

namespace TollFeeCalculatorNET_original.models
{
    public class Car : IVehicle
    {
        public bool IsTollFree { get; } = false;

        public String GetVehicleType()
        {
            string className = MethodBase.GetCurrentMethod().DeclaringType.Name;
            return className;
        }
    }
}