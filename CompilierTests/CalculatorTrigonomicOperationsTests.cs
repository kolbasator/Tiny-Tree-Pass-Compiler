using NUnit.Framework;
using System;
using System.Collections.Generic;
using FluentAssertions;
using Compilier;


namespace CompilierTests
{
    class CalculatorTrigonomicOperationsTests
    {
        [Test]
        public void CalculatorTest1()
        {
            Simulator.Calculate("[ x ] x + cos ( 1 / 2 ) + sin ( 1 / 2 )",new double[1] { 1 }).Should().Be(2.10028204919844);
        } 
        [Test]
        public void CalculatorTest2()
        {
            Simulator.Calculate("[ c ] ctn ( c ) + c", new double[1] { 5 }).Should().Be(4.704187084467255);
        }
        [Test]
        public void CalculatorTest3()
        {
            Simulator.Calculate("[ z ] tan ( z ) + z", new double[1] { 5 }).Should().Be(1.6194849937534141);
        }
        public void CalculatorTest4()
        {
            Simulator.Calculate("[ v ] ex ( v ) * v + 3", new double[1] { 6 }).Should().Be(2423.57276096);
        }
        [Test]
        public void CalculatorTest5()
        {
            Simulator.Calculate("[ c ] log ( 5 ) + c", new double[1] { 10 }).Should().Be(10.47588499532711);
        }
    }
}
