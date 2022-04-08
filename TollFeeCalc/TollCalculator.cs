using System;
using System.Globalization;
using TollFeeCalculatorNET_original.interfaces;

namespace TollFeeCalculatorNET_original
{
    public class TollCalculator
    {
        /**
         * Calculate the total toll fee for one day
         *
         * @param vehicle - the vehicle
         * @param dates   - date and time of all passes on one day
         * @return - the total toll fee for that day
         */
        public int GetTollFee(IVehicle vehicle, DateTime[] dates)
        {
            DateTime intervalStart = dates[0];
            if (DayChecker.IsTollFree(intervalStart) || IsTollFreeVehicle(vehicle)) return 0;
            int totalFee = 0;
            foreach (DateTime date in dates)
            {
                int nextFee = GetTollFee(date);
                int tempFee = GetTollFee(intervalStart);
                var minutes = GetMinutesDiff(intervalStart, date);
                if (minutes <= 60)
                {
                    if (totalFee > 0)
                    {
                        totalFee -= tempFee;
                    }

                    if (nextFee > tempFee)
                    {
                        tempFee = nextFee;
                    }

                    totalFee += tempFee;
                }
                else
                {
                    totalFee += nextFee;
                }

                if (totalFee <= 60) continue;
                totalFee = 60;
                break;
            }

            return totalFee;
        }

        /**
         * Gets the difference in seconds
         *
         */
        private static long GetMinutesDiff(DateTime first, DateTime last)
        {
            long startMillies = new DateTimeOffset(first).ToUnixTimeMilliseconds();
            long endMillies = new DateTimeOffset(last).ToUnixTimeMilliseconds();
            long diffInMillies = endMillies - startMillies;
            long minutes = diffInMillies / 1000 / 60;
            return minutes;
        }

        private bool IsTollFreeVehicle(IVehicle vehicle)
        {
            if (vehicle == null) return false;
            return vehicle.IsTollFree;
        }

        private int GetTollFee(DateTime date)
        {
            int hour = date.Hour;
            int minute = date.Minute;
            switch (hour)
            {
                case 6 when minute <= 29:
                    return 9;
                case 6:
                    return 16;
                case 7:
                    return 22;
                case 8 when minute <= 29:
                    return 16;
                case >= 8 and <= 14:
                    return 9;
                case 15 when minute <= 29:
                    return 16;
                case 15:
                case 16:
                    return 22;
                case 17:
                    return 16;
                case 18 when minute <= 29:
                    return 9;
                default:
                    return 0;
            }
        }
    }
}