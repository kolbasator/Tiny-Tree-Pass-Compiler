using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Compiler.Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void TestSimpleProg()
        {
            string prog = "[ x y z ] ( 2*3*x + 5*y - 3*z ) / (1 + 3 + 2*2)";
            Console.WriteLine("Testing: " + prog);
            AbstractSyntaxTree t1 = new AstOperator("/", new AstOperator("-", new AstOperator("+", new AstOperator("*", new AstOperator("*", new AstOperand("imm", 2), new AstOperand("imm", 3)), new AstOperand("arg", 0)), new AstOperator("*", new AstOperand("imm", 5), new AstOperand("arg", 1))), new AstOperator("*", new AstOperand("imm", 3), new AstOperand("arg", 2))), new AstOperator("+", new AstOperator("+", new AstOperand("imm", 1), new AstOperand("imm", 3)), new AstOperator("*", new AstOperand("imm", 2), new AstOperand("imm", 2))));
            AbstractSyntaxTree p1 = AstCompiler.FirstPass(prog);

            Simulator.PolishNotation = "";
            Simulator.NodesToPolishNotation(t1);
            string pNt1 = Simulator.PolishNotation;

            Simulator.PolishNotation = "";
            Simulator.NodesToPolishNotation(p1);
            string pNp1 = Simulator.PolishNotation;
            if (pNt1 != pNp1) Assert.Fail("t1 != p1, wrong solution for pass1, aborted!"); else Console.WriteLine("Pass1 was ok!");

            AbstractSyntaxTree t2 = new AstOperator("/", new AstOperator("-", new AstOperator("+", new AstOperator("*", new AstOperand("imm", 6), new AstOperand("arg", 0)), new AstOperator("*", new AstOperand("imm", 5), new AstOperand("arg", 1))), new AstOperator("*", new AstOperand("imm", 3), new AstOperand("arg", 2))), new AstOperand("imm", 8));
            AbstractSyntaxTree p2 = AstCompiler.SecondPass(p1);

            Simulator.PolishNotation = "";
            Simulator.NodesToPolishNotation(t2);
            string pNt2 = Simulator.PolishNotation;

            Simulator.PolishNotation = "";
            Simulator.NodesToPolishNotation(p2);
            string pNp2 = Simulator.PolishNotation;
            if (pNt2 != pNp2) Assert.Fail("t2 != p2, wrong solution for pass2, aborted!"); else Console.WriteLine("Pass2 was ok!");

            List<string> p3 = AstCompiler.ThirdPass(AstCompiler.FirstPass(prog));
            int[] args = new int[3] { 4, 0, 0 };
            int res = Simulator.Simulate(p3, args);
            if (res != 3) Assert.Fail("prog(4,0,0) == 3 and not " + res + " => wrong solution, aborted!"); else Console.WriteLine("prog(4,0,0) == 3 was ok");

            args = new int[3] { 4, 8, 0 };
            res = Simulator.Simulate(p3, args);
            if (res != 8) Assert.Fail("prog(4,8,0) == 8 and not " + res + " => wrong solution, aborted!"); else Console.WriteLine("prog(4,8,0) == 8 was ok");

            args = new int[3] { 4, 8, 16 };
            res = Simulator.Simulate(p3, args);
            if (res != 2) Assert.Fail("prog(4,8,16) == 2 and not " + res + " => wrong solution, aborted!"); else Console.WriteLine("prog(4,8,16) == 2 was ok");
        }
    }
}