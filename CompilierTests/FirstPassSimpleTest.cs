using NUnit.Framework;
using System;
using System.Collections.Generic;
using FluentAssertions;
using Compilier;

namespace CompilierTests
{ 
    public class FirstPassSimpleTest
    {
         
        [Test]
        public void FirstPassTest()
        {
            var compiler = new Compiler();
            var prog = "[ x y z ] ( 2*3*x + 5*y - 3*z ) / (1 + 3 + 2*2)";
            var t1 = new AstOperator("/", new AstOperator("-", new AstOperator("+", new AstOperator("*", new AstOperator("*", new AstOperand("imm", 2), new AstOperand("imm", 3)), new AstOperand("arg", 0)), new AstOperator("*", new AstOperand("imm", 5), new AstOperand("arg", 1))), new AstOperator("*", new AstOperand("imm", 3), new AstOperand("arg", 2))), new AstOperator("+", new AstOperator("+", new AstOperand("imm", 1), new AstOperand("imm", 3)), new AstOperator("*", new AstOperand("imm", 2), new AstOperand("imm", 2))));
            var p1 = compiler.FirstPass(prog);
            Simulator.PolishNotation = "";
            Simulator.NodesToPolishNotation(t1);
            var pNt1 = Simulator.PolishNotation;
            Simulator.PolishNotation = "";
            Simulator.NodesToPolishNotation(p1);
            var pNp1 = Simulator.PolishNotation;
            pNp1.Should().Be(pNt1);
        }
        
    }
}