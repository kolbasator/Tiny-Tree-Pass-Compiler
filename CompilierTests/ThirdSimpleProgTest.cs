using NUnit.Framework;
using System;
using System.Collections.Generic;
using FluentAssertions;

namespace CompilierTests
{
    using Compilier;
    using System;
    public class ThirdSimpleProgTest
    {
        [SetUp]
        public void Setup()
        {
        }
        [Test]
        public void SimpleProgThirdTest()
        {
            Compiler compiler = new Compiler();
            List<string> p3 = compiler.ThirdPass(compiler.SecondPass(compiler.FirstPass("[ h g u ] h + g + u")));
            int[] args = new int[3] { 200 ,30,1000 };
            int res = Simulator.Calculate(p3, args);
            res.Should().Be(1230);
        }
    }
}
