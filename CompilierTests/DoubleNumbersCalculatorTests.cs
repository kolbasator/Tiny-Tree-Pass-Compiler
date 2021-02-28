using NUnit.Framework;
using System;
using System.Collections.Generic;
using FluentAssertions;
using Compilier;


namespace CompilierTests
{
    class DoubleNumbersCalculatorTests
    {
        [Test]
        public void DoubleFirstSimpleCalculatorTest()
        {
            double res = Simulator.Calculate("[ x ] x + 10", new double[1] { 1.56 });
            res.Should().Be(11.56);
        }
        [Test]
        public void DoubleSecondSimpleCalculatorTest()
        {
            double res = Simulator.Calculate("[ x ] (x+5.1)", new double[1] { 15 });
            res.Should().Be(20.1);
        }
        [Test]
        public void DoubleThirdSimpleCalculatorTest()
        {
            double res = Simulator.Calculate("[ x y z ] ( 2*3*x + 5*y - 3*z ) / (1 + 3 + 2*2) + 1.76", new double[3] { 4, 8, 16 });
            res.Should().Be(3.76);
        }
        [Test]
        public void DoubleFourthSimpleCalculatorTest()
        {
            double res = Simulator.Calculate("[ x y l ] x * y + l", new double[3] { 100, 100 , 7.98 });
            res.Should().Be(10007.98);
        }

    }
}