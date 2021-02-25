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
            Ast p1 = compiler.FirstPass(prog);
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
            Ast t2 = new BinOp("/", new BinOp("-", new BinOp("+", new BinOp("*", new UnOp("imm", 6), new UnOp("arg", 0)), new BinOp("*", new UnOp("imm", 5), new UnOp("arg", 1))), new BinOp("*", new UnOp("imm", 3), new UnOp("arg", 2))), new UnOp("imm", 8));
            Ast p2 = compiler.SecondPass(compiler.FirstPass("[ x y z ] ( 2*3*x + 5*y - 3*z ) / (1 + 3 + 2*2)")); 
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