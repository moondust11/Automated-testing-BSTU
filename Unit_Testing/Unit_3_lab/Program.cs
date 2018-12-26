using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit_3_lab
{
    class Program
    {
        static void Main(string[] args)
        {
            int firstSide, secondSide, thirdSide;
            Console.Write("Enter sides values: ");
            firstSide = int.Parse(Console.ReadKey().KeyChar.ToString());
            Console.Write(", ");
            secondSide = int.Parse(Console.ReadKey().KeyChar.ToString());
            Console.Write(", ");
            thirdSide = int.Parse(Console.ReadKey().KeyChar.ToString());

            if (Math.TriangleIsPossible(firstSide, secondSide, thirdSide))
                Console.WriteLine($"\nTringle is possible for: {firstSide}, {secondSide}, {thirdSide}");
            else
                Console.WriteLine($"\nTringle is NOT possible for: {firstSide}, {secondSide}, {thirdSide}");
        }
    }
}
