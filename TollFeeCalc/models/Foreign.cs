using System.Reflection;
using TollFeeCalculatorNET_original.interfaces;

namespace TollFeeCalculatorNET_original.models
{
    public class Foreign : IVehicle
    {
        public bool IsTollFree { get; } = false;

        public string GetVehicleType()
        {
            string className = MethodBase.GetCurrentMethod().DeclaringType.Name;
            return className;
        }
    }
}