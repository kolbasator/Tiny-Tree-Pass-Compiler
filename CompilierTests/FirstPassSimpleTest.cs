using NUnit.Framework;
using System;
using System.Collections.Generic;
using FluentAssertions;

namespace CompilierTests
{
    using Compilier;
    using System;
    public class FirstPassSimpleTest
    {
        [SetUp]
        public void Setup()
        {
        } 
        [Test]
        public void FirstPassTest()
        {
            Compiler compiler = new Compiler();
            string prog = "[ x y z ] ( 2*3*x + 5*y - 3*z ) / (1 + 3 + 2*2)";
            Console.WriteLine("Testing: " + prog);
            AbstractSyntaxTree t1 = new AstOperator("/", new AstOperator("-", new AstOperator("+", new AstOperator("*", new AstOperator("*", new AstOperand("imm", 2), new AstOperand("imm", 3)), new AstOperand("arg", 0)), new AstOperator("*", new AstOperand("imm", 5), new AstOperand("arg", 1))), new AstOperator("*", new AstOperand("imm", 3), new AstOperand("arg", 2))), new AstOperator("+", new AstOperator("+", new AstOperand("imm", 1), new AstOperand("imm", 3)), new AstOperator("*", new AstOperand("imm", 2), new AstOperand("imm", 2))));
            AbstractSyntaxTree p1 = compiler.FirstPass(prog);
            Simulator.PolishNotation = "";
            Simulator.NodesToPolishNotation(t1);
            string pNt1 = Simulator.PolishNotation;
            Simulator.PolishNotation = "";
            Simulator.NodesToPolishNotation(p1);
            string pNp1 = Simulator.PolishNotation;
            pNp1.Should().Be(pNt1);
        }
        
    }
}