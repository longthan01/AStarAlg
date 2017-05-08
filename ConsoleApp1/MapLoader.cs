using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Node
    {
        public int X
        {
            get;
            set;
        }
        public int Y
        {
            get;
            set;
        }
        private Node _Parent;
        public Node Parent
        {
            get
            {
                return _Parent;
            }
            set
            {
                _Parent = value;
                int cost = 0;
                if ((this.X + 1 == _Parent.X && this.Y + 1 == _Parent.Y) ||
                    (this.X - 1 == _Parent.X && this.Y - 1 == _Parent.Y) ||
                    (this.X - 1 == _Parent.X && this.Y + 1 == _Parent.Y) ||
                    (this.X + 1 == _Parent.X && this.Y - 1 == _Parent.Y))
                {
                    cost = AlgorithmHelper.COST_DIAGONAL;
                }
                else
                {
                    cost = AlgorithmHelper.COST_ORTHOGONAL;
                }
                this.GCost = cost + _Parent.GCost;
            }
        }
        public int GCost
        {
            get;
            set;
        }
        public int HCost
        {
            get;
            set;
        }
        public string Value
        {
            get;
            set;
        }
        public override bool Equals(object obj)
        {
            Node c = obj as Node;
            return this.X == c.X && this.Y == c.Y;
        }
        public List<Node> GetAdjacent()
        {
            List<Node> res = new List<Node>();
            res.Add(new Node()
            {
                X = this.X,
                Y = this.Y + 1,
                GCost = this.GCost + AlgorithmHelper.COST_ORTHOGONAL
            });
            res.Add(new Node()
            {
                X = this.X,
                Y = this.Y - 1,
                GCost = this.GCost + AlgorithmHelper.COST_ORTHOGONAL
            });
            res.Add(new Node()
            {
                X = this.X + 1,
                Y = this.Y,
                GCost = this.GCost + AlgorithmHelper.COST_ORTHOGONAL
            });
            res.Add(new Node()
            {
                X = this.X - 1,
                Y = this.Y,
                GCost = this.GCost + AlgorithmHelper.COST_ORTHOGONAL
            });
            res.Add(new Node()
            {
                X = this.X + 1,
                Y = this.Y + 1,
                GCost = this.GCost + AlgorithmHelper.COST_DIAGONAL
            });
            res.Add(new Node()
            {
                X = this.X - 1,
                Y = this.Y - 1,
                GCost = this.GCost + AlgorithmHelper.COST_DIAGONAL
            });
            res.Add(new Node()
            {
                X = this.X - 1,
                Y = this.Y + 1,
                GCost = this.GCost + AlgorithmHelper.COST_DIAGONAL
            });
            res.Add(new Node()
            {
                X = this.X + 1,
                Y = this.Y - 1,
                GCost = this.GCost + AlgorithmHelper.COST_DIAGONAL
            });
            return res;
        }
        public bool IsWalkable()
        {
            return this.Value != "0";
        }
        public int CompareCost(Node node)
        {
            return (this.GCost + this.HCost) - (node.GCost + node.HCost);
        }
    }
    public class MapLoader
    {
        public static List<Node> Load()
        {
            StreamReader sr = new StreamReader("test.txt");
            List<Node> res = new List<Node>();
            int i = 0;
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                for (int j = 0; j < 10; j++)
                {
                    res.Add(new Node()
                    {
                        X = j,
                        Y = i,
                        Value = line[j].ToString()
                    });
                }
                i++;
            }
            return res;
        }
        public static void OutPath(List<Node> list, Node destination)
        {
            int i = 0;
            StreamWriter sw = new StreamWriter("out.txt");
            List<Node> path = GetPath(destination);
            for (int j = 0; j < list.Count; j++)
            {
                var nip = path.FirstOrDefault(x => x.Equals(list[j]));
                if (nip != null)
                {
                    sw.Write("#");
                }
                else
                {
                    sw.Write(list[j].Value);
                }
                if (j == (10 * i) + 9)
                {
                    i++;
                    sw.WriteLine();
                }
            }
            sw.Flush();
            sw.Close();
        }
        public static List<Node> GetPath(Node dest)
        {
            List<Node> res = new List<Node>();
            Stack<Node> stk = new Stack<Node>();
            Node e = dest;
            if (e != null)
            {
                do
                {
                    stk.Push(e);
                    e = e.Parent;
                }
                while (e != null);
            }
            while (stk.Count > 0)
            {
                res.Add(stk.Pop());
            }
            return res;
        }
    }
}
