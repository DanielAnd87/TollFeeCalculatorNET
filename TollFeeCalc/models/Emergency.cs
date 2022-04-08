using System.Reflection;
using TollFeeCalculatorNET_original.interfaces;

namespace TollFeeCalculatorNET_original.models
{
    public class Emergency : IVehicle
    {
        public bool IsTollFree { get; } = true;

        public string GetVehicleType()
        {
            string className = MethodBase.GetCurrentMethod().DeclaringType.Name;
            return className;
        }


        
    }
}