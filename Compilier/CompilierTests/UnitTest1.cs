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
        public Compilier compiler = new Compilier();

        [Test]
        public void FirstPassTest()
        {
            string prog = "[ x y z ] ( 2*3*x + 5*y - 3*z ) / (1 + 3 + 2*2)";
            Console.WriteLine("Testing: " + prog);
            Ast t1 = new BinOp("/", new BinOp("-", new BinOp("+", new BinOp("*", new BinOp("*", new UnOp("imm", 2), new UnOp("imm", 3)), new UnOp("arg", 0)), new BinOp("*", new UnOp("imm", 5), new UnOp("arg", 1))), new BinOp("*", new UnOp("imm", 3), new UnOp("arg", 2))), new BinOp("+", new BinOp("+", new UnOp("imm", 1), new UnOp("imm", 3)), new BinOp("*", new UnOp("imm", 2), new UnOp("imm", 2))));
            Ast p1 = compiler.Pass1(prog);
            Simulator.PolishNotation = "";
            Simulator.NodesToPolishNotation(t1);
            string pNt1 = Simulator.PolishNotation;
            Simulator.PolishNotation = "";
            Simulator.NodesToPolishNotation(p1);
            string pNp1 = Simulator.PolishNotation;
            if (pNt1 != pNp1) Assert.Fail("t1 != p1, wrong solution for pass1, aborted!"); else Console.WriteLine("Pass1 was ok!"); 
        }
        [Test]
        public void SecondPassTest()
        {
            Ast t2 = new BinOp("/", new BinOp("-", new BinOp("+", new BinOp("*", new UnOp("imm", 6), new UnOp("arg", 0)), new BinOp("*", new UnOp("imm", 5), new UnOp("arg", 1))), new BinOp("*", new UnOp("imm", 3), new UnOp("arg", 2))), new UnOp("imm", 8));
            Ast p2 = compiler.SecondPass(compiler.Pass1("[ x y z ] ( 2*3*x + 5*y - 3*z ) / (1 + 3 + 2*2)"));

            Simulator.PolishNotation = "";
            Simulator.NodesToPolishNotation(t2);
            string pNt2 = Simulator.PolishNotation;

            Simulator.PolishNotation = "";
            Simulator.NodesToPolishNotation(p2);
            string pNp2 = Simulator.PolishNotation;
            if (pNt2 != pNp2) Assert.Fail("t2 != p2, wrong solution for pass2, aborted!"); else Console.WriteLine("Pass2 was ok!");
        }
        [Test]
        public void ThirdPassTest()
        { 
            List<string> p3 = compiler.Pass3(compiler.Pass1("[ x y z ] ( 2*3*x + 5*y - 3*z ) / (1 + 3 + 2*2)"));
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