using NUnit.Framework;
using System;
using System.Collections.Generic;
using Compilier; 
using FluentAssertions;

namespace CompilierTests
{ 
    public class FirstSimpleProgTest
    { 
        [Test]
        public void SimpleProgFirstTest()
        {
            var compiler = new Compiler();
            var p3 = compiler.ThirdPass(compiler.SecondPass(compiler.FirstPass("[ x y z ] ( 2*3*x + 5*y - 3*z ) / (1 + 3 + 2*2)")));
            var args = new int[3] { 4, 8, 16 };
            var res = Simulator.Calculate(p3, args);
            res.Should().Be(2);
        }
    }
}
