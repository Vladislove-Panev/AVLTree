using System;
using System.Collections.Generic;
using System.Text;

namespace AVLTree
{
    public class AVLTree
    {
        Node root;
        public AVLTree()
        {
        }

        public AVLTree(List<int> list)
        {
            foreach (int i in list)
            {
                Add(i);
            }
        }

        public void Add(int data)
        {
            Node newItem = new Node(data);
            if (root == null)
            {
                root = newItem;
            }
            else
            {
                root = Insert(root, newItem);
            }
        }
        public void Delete(int target)
        {
            root = Delete(root, target);
        }

        private Node Insert(Node current, Node n)
        {
            if (current == null)
            {
                current = n;
                return current;
            }
            else if (n.data < current.data)
            {
                current.left = Insert(current.left, n);
                current = Balance(current);
            }
            else if (n.data >= current.data)
            {
                current.right = Insert(current.right, n);
                current = Balance(current);
            }
            return current;
        }

        private Node Balance(Node current)
        {
            int b_factor = BalanceFactor(current);
            if (b_factor > 1)
            {
                if (BalanceFactor(current.left) > 0)
                {
                    current = RotateLL(current);
                }
                else
                {
                    current = RotateLR(current);
                }
            }
            else if (b_factor < -1)
            {
                if (BalanceFactor(current.right) > 0)
                {
                    current = RotateRL(current);
                }
                else
                {
                    current = RotateRR(current);
                }
            }
            return current;
        }

        private Node Delete(Node current, int target)
        {
            Node parent;
            if (current == null)
                return null;
            else
            {
                if (target < current.data)
                {
                    current.left = Delete(current.left, target);
                    if (BalanceFactor(current) == -2)
                    {
                        if (BalanceFactor(current.right) <= 0)
                        {
                            current = RotateRR(current);
                        }
                        else
                        {
                            current = RotateRL(current);
                        }
                    }
                }
                else if (target > current.data)
                {
                    current.right = Delete(current.right, target);
                    if (BalanceFactor(current) == 2)
                    {
                        if (BalanceFactor(current.left) >= 0)
                        {
                            current = RotateLL(current);
                        }
                        else
                        {
                            current = RotateLR(current);
                        }
                    }
                }
                else
                {
                    if (current.right != null)
                    {
                        parent = current.right;
                        while (parent.left != null)
                        {
                            parent = parent.left;
                        }
                        current.data = parent.data;
                        current.right = Delete(current.right, parent.data);

                        if (BalanceFactor(current) == 2)
                        {
                            if (BalanceFactor(current.left) >= 0)
                            {
                                current = RotateLL(current);
                            }
                            else
                            {
                                current = RotateLR(current);
                            }
                        }
                    }
                    else
                    {
                        return current.left;
                    }
                }
            }
            return current;
        }
        public Node Find(int key)
        {
            Node node = Find(key, root);
            if (node.data == key)
            {
                return node;
            }
            else
            {
                return new Node();
            }
        }
        private Node Find(int target, Node current)
        {
            if (target < current.data)
            {
                if (target == current.data)
                {
                    return current;
                }
                else
                    return Find(target, current.left);
            }
            else
            {
                if (target == current.data)
                {
                    return current;
                }
                else
                    return Find(target, current.right);
            }

        }
        public List<int> ToList()
        {
            List<int> list = new List<int>();
            if (root == null)
            {
                return list;
            }
            ToList(root, list);
            return list;
        }
        private void ToList(Node current, List<int> list)
        {
            if (current != null)
            {
                ToList(current.left, list);
                list.Add(current.data);
                ToList(current.right, list);
            }
        }

        private int Max(int l, int r)
        {
            return l > r ? l : r;
        }
        private int GetHeight(Node current)
        {
            int height = 0;
            if (current != null)
            {
                int l = GetHeight(current.left);
                int r = GetHeight(current.right);
                int m = Max(l, r);
                height = m + 1;
            }
            return height;
        }
        private int BalanceFactor(Node current)
        {
            int l = GetHeight(current.left);
            int r = GetHeight(current.right);
            int bf = l - r;
            return bf;
        }
        private Node RotateRR(Node parent)
        {
            Node pivot = parent.right;
            parent.right = pivot.left;
            pivot.left = parent;
            return pivot;
        }
        private Node RotateLL(Node parent)
        {
            Node pivot = parent.left;
            parent.left = pivot.right;
            pivot.right = parent;
            return pivot;
        }
        private Node RotateLR(Node parent)
        {
            Node pivot = parent.left;
            parent.left = RotateRR(pivot);
            return RotateLL(parent);
        }
        private Node RotateRL(Node parent)
        {
            Node pivot = parent.right;
            parent.right = RotateLL(pivot);
            return RotateRR(parent);
        }
    }
}
