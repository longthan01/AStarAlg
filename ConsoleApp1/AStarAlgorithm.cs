using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class AlgorithmHelper
    {
        public const int COST_DIAGONAL = 14;
        public const int COST_ORTHOGONAL = 10;

        public static int GetHeuristicMahattanCost(Node current, Node dest)
        {
            return (Math.Abs(current.X - dest.X)) + Math.Abs(current.Y - dest.Y) - ((current.X == dest.X || current.Y == dest.Y) ? 1 : 0);
        }
        public static int GetMovementCost(Node curr, Node next)
        {
            bool diagonal = Math.Abs(curr.X - next.X) == 1 && Math.Abs(curr.Y - next.Y) == 1;
            return diagonal ? curr.GCost + COST_DIAGONAL : curr.GCost + COST_ORTHOGONAL;
        }
        public static int GetTotalCost(Node curr, Node next)
        {
            return GetHeuristicMahattanCost(curr, next) + GetMovementCost(curr, next);
        }
    }
    public class AStarAlgorithm
    {
        public Node FindPath(Node start, Node end, List<Node> map)
        {
            if (!end.IsWalkable())
            {
                return null;
            }
            List<Node> openList = new List<Node>();
            List<Node> closedList = new List<Node>();
            openList.Add(start);
            while (openList.Count > 0)
            {
                Node curr = openList.GetLowestCost();
                
                System.Diagnostics.Debug.WriteLine(string.Format("\nPick {0},{1} out openlist list", curr.X, curr.Y));
                openList.Remove(curr);   // switch from open list to closed list
                System.Diagnostics.Debug.WriteLine(string.Format("Add {0},{1} to closed list", curr.X, curr.Y));
                closedList.Add(curr);
                var adjacents = curr.GetAdjacent();
                foreach (var ad in adjacents)
                {
                    // point must be existed in the map
                    if (map.Contains(ad))
                    {
                        Node item = map.Map(ad);
                        item.HCost = AlgorithmHelper.GetHeuristicMahattanCost(item, end);
                        if (item.Equals(end))
                        {
                            item.Parent = curr;
                            return item;
                        }
                        // check if node is walkable and not in closed list
                        if (closedList.Any(x => x.Equals(item)))
                        {
                            continue;
                        }
                        if (!item.IsWalkable())
                        {
                            System.Diagnostics.Debug.WriteLine(string.Format("Add {0},{1} to closed list\n", item.X, item.Y));
                            closedList.Add(item);
                            continue;
                        }
                        if (openList.Any(x => x.Equals(item)))
                        {
                            var existed = openList.LastOrDefault(x => x.Equals(item));
                            if (existed != null)
                            {
                                if (AlgorithmHelper.GetMovementCost(item, existed) < existed.GCost)
                                {
                                    existed.Parent = item;
                                }
                            }
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine(string.Format("Add {0},{1} to open list", item.X, item.Y));
                            openList.Add(item);
                        }
                        item.Parent = curr;
                    }
                }
            }
            return null;
        }
    }
}
