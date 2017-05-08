using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Node> map = MapLoader.Load();
            Node start = map.FirstOrDefault(x => x.X == 0 && x.Y == 0);
            Node dest = map.FirstOrDefault(x => x.X == 9 && x.Y == 9);
            Node node = new AStarAlgorithm().FindPath(start, dest, map);
            if (node != null)
            {
                MapLoader.OutPath(map, node);
                do
                {
                    Console.WriteLine(string.Format("{0}-{1}", node.X, node.Y));
                    Console.WriteLine("|");
                    node = node.Parent;
                }
                while (node != null);
            }
           
            Console.ReadKey();
        }
    }
}
