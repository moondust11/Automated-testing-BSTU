using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit_3_lab
{
    [TestFixture]
    public class TestCases
    {
        [TestCase]
        public void TriangleIsPossible()
        {
            int firstSide = 3;
            int secondSide = 6;
            int thirdSide = 8;

            var result = Math.TriangleIsPossible(firstSide, secondSide, thirdSide);

            Assert.IsTrue(result);
        }

        [TestCase]
        public void TriangleIsNotPossible()
        {
            int firstSide = 2;
            int secondSide = 6;
            int thirdSide = 8;

            var result = Math.TriangleIsPossible(firstSide, secondSide, thirdSide);

            Assert.IsFalse(result);
        }

        [TestCase]
        public void SomeInputParamsAreNegative()
        {
            int firstSide = -6;
            int secondSide = 1;
            int thirdSide = 2;

            var result = Math.TriangleIsPossible(firstSide, secondSide, thirdSide);

            Assert.IsFalse(result);
        }

        [TestCase]
        public void AllInputParamsAreNegative()
        {
            int firstSide = -6;
            int secondSide = -6;
            int thirdSide = -8;

            var result = Math.TriangleIsPossible(firstSide, secondSide, thirdSide);

            Assert.IsFalse(result);
        }

        [TestCase]
        public void SomeInputParamsAreZero()
        {
            int firstSide = 5;
            int secondSide = 1;
            int thirdSide = 0;

            var result = Math.TriangleIsPossible(firstSide, secondSide, thirdSide);

            Assert.IsFalse(result);
        }

        [TestCase]
        public void AllInputParamsAreZero()
        {
            int firstSide = 0;
            int secondSide = 0;
            int thirdSide = 0;

            var result = Math.TriangleIsPossible(firstSide, secondSide, thirdSide);

            Assert.IsFalse(result);
        }

        [TestCase]
        public void ReturnableParamIsBool()
        {
            int firstSide = 8;
            int secondSide = 5;
            int thirdSide = 1;

            var result = Math.TriangleIsPossible(firstSide, secondSide, thirdSide);

            Assert.IsInstanceOf(typeof(bool), result);
        }

        [TestCase]
        public void EquilateralTriangle()
        {
            int firstSide = 3;
            int secondSide = 3;
            int thirdSide = 3;

            var result = Math.TriangleIsPossible(firstSide, secondSide, thirdSide);

            Assert.IsTrue(result);
        }

        [TestCase]
        public void IsoscelesTriangleIsPossible()
        {
            int firstSide = 3;
            int secondSide = 3;
            int thirdSide = 5;

            var result = Math.TriangleIsPossible(firstSide, secondSide, thirdSide);

            Assert.IsTrue(result);
        }

        [TestCase]
        public void IsoscelesTriangleIsNotPossible()
        {
            int firstSide = 3;
            int secondSide = 3;
            int thirdSide = 6;

            var result = Math.TriangleIsPossible(firstSide, secondSide, thirdSide);

            Assert.IsFalse(result);
        }

    }
}
