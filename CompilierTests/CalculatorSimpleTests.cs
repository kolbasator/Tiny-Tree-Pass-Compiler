using NUnit.Framework;
using System;
using System.Collections.Generic;
using FluentAssertions;
using Compilier;


namespace CompilierTests
{
    class CalculatorSimpleTests
    {
        [Test]
        public void FirstSimpleCalculatorTest()
        {
            double res = Simulator.Calculate("[ x ] x + 10", new double[1] { 1 });
            res.Should().Be(11);
        }
        [Test]
        public void SecondSimpleCalculatorTest()
        {
            double res = Simulator.Calculate("[ x ] x * 10", new double[1] { 15 });
            res.Should().Be(150);
        }
        [Test]
        public void ThirdSimpleCalculatorTest()
        {
            double res = Simulator.Calculate("[ x y z ] ( 2*3*x + 5*y - 3*z ) / (1 + 3 + 2*2)", new double[3] { 4 , 8, 16 });
            res.Should().Be(2);
        }
        [Test]
        public void FourthSimpleCalculatorTest()
        {
            double res = Simulator.Calculate("[ x  y ] x * y", new double[2] { 100,100});
            res.Should().Be(10000);
        }
         
    }
}
