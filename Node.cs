using System;
using System.Collections.Generic;
using System.Text;

namespace AVLTree
{
    public class Node
    {
        public int data;
        public Node left;
        public Node right;

        public Node() { }
        public Node(int data)
        {
            this.data = data;
        }
    }
}
