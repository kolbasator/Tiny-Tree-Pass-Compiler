using NUnit.Framework;
using System;
using System.Collections.Generic;
using FluentAssertions;
using Compilier;

namespace CompilierTests
{ 
    public class FourSimpleProgTest
    { 
        [Test]
        public void SimpleProgFourTest()
        {
            var compiler = new Compiler();
            var p3 = compiler.ThirdPass(compiler.SecondPass(compiler.FirstPass("[ h g u o ] h + g + u * o * ( 2 + 2 )")));
            var args = new int[4] { 11,12,13,14 };
            var res = Simulator.Calculate(p3, args);
            res.Should().Be(751);
        }
    }
}
