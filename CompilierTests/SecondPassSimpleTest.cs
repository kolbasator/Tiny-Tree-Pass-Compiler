using NUnit.Framework;
using System;
using System.Collections.Generic;
using FluentAssertions;

namespace CompilierTests
{
    using Compilier;
    using System;
    public class SecondPassTest
    {
        [SetUp]
        public void Setup()
        {
        }
        public Compiler compiler = new Compiler();
        [Test]
        public void SecondPassSimpleTest()
        {
            AbstractSyntaxTree t2 = new AstOperator("/", new AstOperator("-", new AstOperator("+", new AstOperator("*", new AstOperand("imm", 6), new AstOperand("arg", 0)), new AstOperator("*", new AstOperand("imm", 5), new AstOperand("arg", 1))), new AstOperator("*", new AstOperand("imm", 3), new AstOperand("arg", 2))), new AstOperand("imm", 8));
            AbstractSyntaxTree p2 = compiler.SecondPass(compiler.FirstPass("[ x y z ] ( 2*3*x + 5*y - 3*z ) / (1 + 3 + 2*2)"));
            Simulator.PolishNotation = "";
            Simulator.NodesToPolishNotation(t2);
            string pNt2 = Simulator.PolishNotation;
            Simulator.PolishNotation = "";
            Simulator.NodesToPolishNotation(p2);
            string pNp2 = Simulator.PolishNotation;
            pNp2.Should().Be(pNt2);
        }
    }
}