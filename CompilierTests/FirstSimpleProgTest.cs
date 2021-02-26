using NUnit.Framework;
using System;
using System.Collections.Generic;
using FluentAssertions;

namespace CompilierTests
{
    using Compilier;
    using System;
    public class FirstSimpleProgTest
    {
        [SetUp]
        public void Setup()
        {
        }
        [Test]
        public void SimpleProgFirstTest()
        {
            Compiler compiler = new Compiler();
            List<string> p3 = compiler.ThirdPass(compiler.SecondPass(compiler.FirstPass("[ x y z ] ( 2*3*x + 5*y - 3*z ) / (1 + 3 + 2*2)")));
            int[] args = new int[3] { 4, 8, 16 };
            int res = Simulator.Calculate(p3, args);
            res.Should().Be(2);
        }
    }
}
