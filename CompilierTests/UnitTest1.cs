using NUnit.Framework;
using System;
using System.Collections.Generic;
using FluentAssertions;

namespace CompilierTests
{
    using Compilier;
    using System;
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }
        public Compiler compiler = new Compiler();
        [Test]
        public void TestSimpleProg()
        {
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
        [Test]
        public void SecondPassTest()
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
        [Test]
        public void FirstThirdPassTest()
        {
            List<string> p3 = compiler.ThirdPass(compiler.SecondPass(compiler.FirstPass("[ x y z ] ( 2*3*x + 5*y - 3*z ) / (1 + 3 + 2*2)")));
            int[] args = new int[3] { 4, 0, 0 };
            int res = Simulator.Calculate(p3, args);
            res.Should().Be(3);
        }
        [Test]
        public void SecondThirdPassTest()
        {
            List<string> p3 = compiler.ThirdPass(compiler.SecondPass(compiler.FirstPass("[ x y z ] ( 2*3*x + 5*y - 3*z ) / (1 + 3 + 2*2)")));
            int[] args = new int[3] { 4, 8, 0 };
            int res = Simulator.Calculate(p3, args);
            res.Should().Be(8);
        }
        [Test]
        public void ThirdThirdPassTest()
        {
            List<string> p3 = compiler.ThirdPass(compiler.SecondPass(compiler.FirstPass("[ x y z ] ( 2*3*x + 5*y - 3*z ) / (1 + 3 + 2*2)")));
            int[] args = new int[3] { 4, 8, 16 };
            int res = Simulator.Calculate(p3, args);
            res.Should().Be(2);
        }
    }
}