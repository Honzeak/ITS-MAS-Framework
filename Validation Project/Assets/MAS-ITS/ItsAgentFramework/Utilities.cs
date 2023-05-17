using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItsAgentFramework
{
    internal static class Utilities
    {
        private static int _IdCounter = 0;
        public static int GetIntID()
        {
            return ++_IdCounter;
        }


        private static double ValidateInternal(double value, double? minThreshold = null, double? maxThreshold = null)
        {
            if (minThreshold != null && value < minThreshold)
            {
                Console.WriteLine($"WARNING: Setting value less than {minThreshold} is forbidden, using {minThreshold}.");
                return (int)minThreshold;
            }
            if (maxThreshold != null && value > maxThreshold)
            {
                Console.WriteLine($"WARNING: Setting value more than {maxThreshold} is forbidden, using {maxThreshold}.");
                return (int)maxThreshold;
            }
            return value;
        }

        public static double ValidateRange(double value, double minThreshold, double maxThreshold)
        {
            return ValidateInternal(value, minThreshold, maxThreshold);
        }
        public static double ValidateMin(double value, double minThreshold)
        {
            return ValidateInternal(value, minThreshold: minThreshold);
        }
        public static double ValidateMax(double value, double maxThreshold)
        {
            return ValidateInternal(value, maxThreshold: maxThreshold);
        }
    }
}
