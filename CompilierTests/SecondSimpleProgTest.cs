using NUnit.Framework;
using System;
using System.Collections.Generic;
using FluentAssertions;

namespace CompilierTests
{
    using Compilier;
    using System;
    public class SecondSimpleProgTest
    {
        [SetUp]
        public void Setup()
        {
        }
        [Test]
        public void SimpleProgSecondTest()
        {
            Compiler compiler = new Compiler();
            List<string> p3 = compiler.ThirdPass(compiler.SecondPass(compiler.FirstPass("[a, b, c, d] ( 3 *a + 2*b + 5*c+ 7*d)")));
            int[] args = new int[4] { 4, 8,6,18 };
            int res = Simulator.Calculate(p3, args);
            res.Should().Be(184);
        }
    }
}
