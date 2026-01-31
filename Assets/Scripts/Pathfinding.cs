//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Extensions;
//using UnityEngine;

//namespace Systems
//{
//    public static class Pathfinding
//    {   //The following resources were used to write this class:
//        //SEE https://www.raywenderlich.com/3016-introduction-to-a-pathfinding
//        //AND https://medium.com/@nicholas.w.swift/easy-a-star-pathfinding-7e6689c7f7b2

//        private class MapSquare : IComparable<MapSquare>
//        {
//            public readonly Vector2Int Position;

//            private int _g;

//            public int F => _g + H;

//            public int G
//            {
//                get { return _g; }
//                set { _g = value; }
//            }

//            public int H { get; set; }

//            public MapSquare Parent;

//            public MapSquare(Vector2Int location, int g, int h)
//            {
//                Position = location;
//                _g = g;
//                H = h;
//            }

//            public int CompareTo(MapSquare other)
//            {
//                if (F > other.F)
//                {
//                    return -1;
//                }

//                if (F == other.F)
//                {
//                    return 0;
//                }

//                return 1;
//            }

//            public bool Equals(MapSquare other)
//            {
//                return Position.x == other.Position.x && Position.y == other.Position.y;
//            }

//            public override int GetHashCode()
//            {
//                return 7 * Position.x + 11 * Position.y;
//            }
//        }

//        public static bool GetPathTo(Vector2Int from, Vector2Int to, out List<Vector2Int> path, bool includeEndpoints = false)
//        {
//            //Get map maximum values
//            Vector2Int mapMaxBounds = MapDataAccess.Service.mapMaxBounds;

//            //Declare openList
//            List<MapSquare> openList = new List<MapSquare>();

//            //Declare closed list
//            List<MapSquare> closedList = new List<MapSquare>();

//            //The first square will contain the "from" location
//            MapSquare firstSquare = new MapSquare(
//                from, 0, from.GetDeltaTo(to)
//            );

//            //Add the original square to the open list
//            openList.Add(firstSquare);

//            bool pathFound = false;

//            do
//            {
//                //Sort open list
//                openList = openList.OrderBy(l => l.F).ToList();

//                //Set current square equal to square with the lowest "F" = G + H
//                MapSquare currentPathSquare = openList.First();

//                //Add current square to the closed list
//                closedList.Add(currentPathSquare);

//                //Remove current square from open list
//                openList.Remove(currentPathSquare);

//                //Create a list of neighbor blocks
//                List<MapSquare> neighborSquares = new List<MapSquare>();

//                //Add open neighboring squares
//                for (int delta = -1; delta < 2; delta += 2)
//                {
//                    Vector2Int dxPosition = new Vector2Int(currentPathSquare.Position.x + delta, currentPathSquare.Position.y);
//                    Vector2Int dyPosition = new Vector2Int(currentPathSquare.Position.x, currentPathSquare.Position.y + delta);

//                    if (dxPosition.x > 0 && dxPosition.x < mapMaxBounds.x && !MapDataAccess.Service.IsCellBlocked(dxPosition))
//                    {
//                        MapSquare mapSquare = new MapSquare(dxPosition, currentPathSquare.G + 1, dxPosition.GetDeltaTo(to));
//                        neighborSquares.Add(mapSquare);
//                    }

//                    if (dyPosition.y > 0 && dyPosition.y < mapMaxBounds.y && !MapDataAccess.Service.IsCellBlocked(dyPosition))
//                    {
//                        MapSquare mapSquare = new MapSquare(dyPosition, currentPathSquare.G + 1, dyPosition.GetDeltaTo(to));
//                        neighborSquares.Add(mapSquare);
//                    }
//                }

//                //If any neighbor squares are the target, don't even worry about adding anything to the open list
//                if (neighborSquares.Any(s => s.Position.Equals(to)))
//                {
//                    MapSquare targetSquare = neighborSquares.Single(s => s.Position.Equals(to));
//                    targetSquare.Parent = currentPathSquare;
//                    openList.Add(targetSquare);
//                    pathFound = true;
//                    break;
//                }

//                //Add/update neighbors in the open list or ignore them if already in the closed list.
//                foreach (MapSquare square in neighborSquares)
//                {
//                    //Ignore any square that is in the closed list
//                    if (closedList.Any(s => s.Equals(square)))
//                    {
//                        continue;
//                    }

//                    //Try to find matching square in open list
//                    MapSquare openMatch = openList.SingleOrDefault(m => m.Equals(square));

//                    //Add square that is not in the open list to the open list
//                    if (openMatch == null)
//                    {
//                        //set parent square to current
//                        square.Parent = currentPathSquare;
//                        openList.Add(square);
//                    }
//                    else
//                    {
//                        //Neighbor is already in the open list. Update it's G score and parent if it is
//                        int nextPathG = currentPathSquare.G + 1;
//                        if (nextPathG < openMatch.G)
//                        {
//                            int matchIndex = openList.IndexOf(openMatch);
//                            openList[matchIndex].G = nextPathG;
//                            openList[matchIndex].Parent = currentPathSquare;
//                        }
//                    }

//                }

//            } while (openList.Count != 0); //If there is nothing in the open list, then the path was not found


//            //Closed list vis
//            foreach (var n in closedList)
//            {
//                var offSet = 1f;
//                Debug.DrawLine(new Vector3(n.Position.x, n.Position.y, 0f), new Vector3(n.Position.x + offSet, n.Position.y + offSet, 0f), Color.red, 1f);
//                Debug.DrawLine(new Vector3(n.Position.x, n.Position.y + offSet, 0f), new Vector3(n.Position.x + offSet, n.Position.y, 0f), Color.red, 1f);
//            }

//            path = new List<Vector2Int>();

//            if (pathFound)
//            {
//                MapSquare lastNode = openList.Last();

//                while (lastNode != null)
//                {
//                    path.Add(lastNode.Position);

//                    //Open list vis
//                    //Debug.DrawLine(new Vector3(lastNode.Position.x, lastNode.Position.y, 0f), new Vector3(lastNode.Position.x + 1f, lastNode.Position.y + 1f, 0f), Color.cyan, 1f);
//                    //Debug.DrawLine(new Vector3(lastNode.Position.x, lastNode.Position.y + 1f, 0f), new Vector3(lastNode.Position.x + 1f, lastNode.Position.y, 0f), Color.cyan, 1f);

//                    lastNode = lastNode.Parent;
//                }

//                if (!includeEndpoints)
//                {
//                    //Remove the "from" node
//                    path.RemoveAt(path.Count - 1);

//                    //Remove the "to" node
//                    path.RemoveAt(0);
//                }

//                //Because path will be "reversed", reverse it
//                path.Reverse();

//                foreach (var pnode in path)
//                {
//                    Debug.DrawLine(new Vector3(pnode.x, pnode.y, 0f), new Vector3(pnode.x + 1f, pnode.y + 1f, 0f), Color.cyan, 1f);
//                    Debug.DrawLine(new Vector3(pnode.x, pnode.y + 1f, 0f), new Vector3(pnode.x + 1f, pnode.y, 0f), Color.cyan, 1f);
//                }

//                ////Remove
//                //path = path.Except(new List<Vector2Int>{path.First()}).ToList();
//            }

//            return pathFound;
//        }
//    }
//}