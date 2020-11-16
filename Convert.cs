using System;

namespace TheHarbor
{
    public static class Convert
    {
        public static double KnotToKph(double boat)
        {
            return Math.Round(boat * 1.852);
        }
    }
}
