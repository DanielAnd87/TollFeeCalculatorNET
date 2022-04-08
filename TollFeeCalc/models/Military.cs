using System.Reflection;
using TollFeeCalculatorNET_original.interfaces;

namespace TollFeeCalculatorNET_original.models
{
    public class Military : IVehicle
    {
        
        private readonly bool _isTollFree = true;

        public string GetVehicleType()
        {
            string className = MethodBase.GetCurrentMethod().DeclaringType.Name;
            return className;
        }

        public bool IsTollFree => _isTollFree;
    }
}