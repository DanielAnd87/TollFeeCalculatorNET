using System;

namespace TollFeeCalculatorNET_original.interfaces
{
    public interface IVehicle
    {
        String GetVehicleType();

        bool IsTollFree
        {
            get;
        }
    }
}