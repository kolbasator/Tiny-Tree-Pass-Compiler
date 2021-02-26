using NUnit.Framework;
using System;
using System.Collections.Generic;
using FluentAssertions;
using Compilier;

namespace CompilierTests
{ 
    public class SecondSimpleProgTest
    {
        
        [Test]
        public void SimpleProgSecondTest()
        {
            var compiler = new Compiler();
            var p3 = compiler.ThirdPass(compiler.SecondPass(compiler.FirstPass("[a, b, c, d] ( 3 *a + 2*b + 5*c+ 7*d)")));
            var args = new int[4] { 4, 8,6,18 };
            var res = Simulator.Calculate(p3, args);
            res.Should().Be(184);
        }
    }
}
