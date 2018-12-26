using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit_3_lab
{
    public class Math
    {
        public delegate bool DTriangleIsPossible(double firstSide, double secondSide, double thirdSide);
        public static bool TriangleIsPossible(double firstSide, double secondSide, double thirdSide)
        {
            if (firstSide + secondSide > thirdSide
                && firstSide + thirdSide > secondSide
                && secondSide + thirdSide > firstSide)
                return true;
            else
                return false;
        }
    }
}
