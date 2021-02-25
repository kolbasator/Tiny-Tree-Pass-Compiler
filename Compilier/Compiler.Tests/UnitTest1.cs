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
<<<<<<< HEAD:Compilier/CompilierTests/UnitTest1.cs
            pNp1.Should().Be(pNt1);
        }
        [Test]
        public void SecondPassTest()
        {
            Ast t2 = new BinOp("/", new BinOp("-", new BinOp("+", new BinOp("*", new UnOp("imm", 6), new UnOp("arg", 0)), new BinOp("*", new UnOp("imm", 5), new UnOp("arg", 1))), new BinOp("*", new UnOp("imm", 3), new UnOp("arg", 2))), new UnOp("imm", 8));
            Ast p2 = compiler.SecondPass(compiler.FirstPass("[ x y z ] ( 2*3*x + 5*y - 3*z ) / (1 + 3 + 2*2)")); 
=======
            if (pNt1 != pNp1) Assert.Fail("t1 != p1, wrong solution for pass1, aborted!"); else Console.WriteLine("Pass1 was ok!");

            AbstractSyntaxTree t2 = new AstOperator("/", new AstOperator("-", new AstOperator("+", new AstOperator("*", new AstOperand("imm", 6), new AstOperand("arg", 0)), new AstOperator("*", new AstOperand("imm", 5), new AstOperand("arg", 1))), new AstOperator("*", new AstOperand("imm", 3), new AstOperand("arg", 2))), new AstOperand("imm", 8));
            AbstractSyntaxTree p2 = AstCompiler.SecondPass(p1);

>>>>>>> 4e585ea7fc2546d7a16b28337a89ad72cd5c2761:Compilier/Compiler.Tests/UnitTest1.cs
            Simulator.PolishNotation = "";
            Simulator.NodesToPolishNotation(t2);
            string pNt2 = Simulator.PolishNotation; 
            Simulator.PolishNotation = "";
            Simulator.NodesToPolishNotation(p2);
            string pNp2 = Simulator.PolishNotation;
<<<<<<< HEAD:Compilier/CompilierTests/UnitTest1.cs
            pNp2.Should().Be(pNt2);
        }
        [Test]
        public void FirstThirdPassTest()
        { 
            List<string> p3 = compiler.ThirdPass(compiler.SecondPass(compiler.FirstPass("[ x y z ] ( 2*3*x + 5*y - 3*z ) / (1 + 3 + 2*2)")));
=======
            if (pNt2 != pNp2) Assert.Fail("t2 != p2, wrong solution for pass2, aborted!"); else Console.WriteLine("Pass2 was ok!");

            List<string> p3 = AstCompiler.ThirdPass(AstCompiler.FirstPass(prog));
>>>>>>> 4e585ea7fc2546d7a16b28337a89ad72cd5c2761:Compilier/Compiler.Tests/UnitTest1.cs
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