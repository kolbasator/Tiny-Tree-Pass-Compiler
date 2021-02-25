using System;

namespace Compiler
{
    // keep class names such way that it displays the meaning
    public class AbstractSyntaxTree
    {
        // this is base class, it should contain a reference to left and right children
        public string Status { get; protected set; }
        public string Value { get; protected set; }
        public AbstractSyntaxTree LeftChild { get; protected set; }
        public AbstractSyntaxTree RightChild { get; protected set; }

        public void InOrderTraversal(AbstractSyntaxTree binaryTree)
        {
            if (binaryTree == null) return;
            InOrderTraversal(binaryTree.LeftChild);
            Console.Write(binaryTree.Value + " ");
            InOrderTraversal(binaryTree.RightChild);
        }
    }
}