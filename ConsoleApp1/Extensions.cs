using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public static class Extensions
    {
        public static Node GetCell(this List<Node> list, int x, int y)
        {
            return list.FirstOrDefault(a => a.X == x && a.Y == y);
        }
        public static Node GetLowestCost(this List<Node> list)
        {
            return list.LastOrDefault(x => (x.GCost + x.HCost) == list.Min(y => y.GCost + y.HCost));
        }
        public static Node Map(this List<Node> list, Node node)
        {
            var cur = list.FirstOrDefault(x => x.X == node.X && x.Y == node.Y);
            return cur;
        }
        
    }
}
