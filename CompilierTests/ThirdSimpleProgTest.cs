using NUnit.Framework;
using System;
using System.Collections.Generic;
using FluentAssertions;
using Compilier;

namespace CompilierTests
{ 
    public class ThirdSimpleProgTest
    { 
        [Test]
        public void SimpleProgThirdTest()
        {
            var compiler = new Compiler();
            var p3 = compiler.ThirdPass(compiler.SecondPass(compiler.FirstPass("[ h g u ] h + g + u + 100")));
            var args = new double[3] { 200 ,30,1000 };
            var res = Simulator.Simulate(p3, args);
            res.Should().Be(1330);
        }
    }
}
