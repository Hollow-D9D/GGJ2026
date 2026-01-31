//using System;
//using System.Collections.Generic;
//using UnityEngine;

//namespace Extensions
//{
//    public static class Vector2IntPathfindingExtensions
//    {
//        public static int GetDeltaTo(this Vector2Int from, Vector2Int to)
//        {
//            //Manhattan Dist
//            return Math.Abs(from.x - to.x) + Math.Abs(from.y - to.y);
//        }

//        public static bool FindPathTo(this Vector2Int origin, Vector2Int destination, out List<Vector2Int> path)
//        {
//            return Systems.Pathfinding.GetPathTo(origin, destination, out path);
//        }

//        public static bool IsNeighborOf(this Vector2Int origin, Vector2Int cell)
//        {
//            return (Math.Abs(cell.x - origin.x) == 1 && cell.y - origin.y == 0 || Math.Abs(cell.y - origin.y) == 1 && cell.x - origin.x == 0);
//        }
//    }
//}