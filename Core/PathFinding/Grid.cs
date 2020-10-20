using System.Collections.Generic;
using Core.Util;
using UnityEngine;

namespace Core.PathFinding
{
    public class Grid<T> : MultiMap<(int, int), Tile<T>> where T : MonoBehaviour
    {
        private readonly Queue<Tile<T>> _tileQueue = new Queue<Tile<T>>();

        public void Add((int, int) coords, Tile<T> t, int height)
        {
            base.Add(coords, t);
            t.Height = height;
        }

        public override void Add((int, int) coords, Tile<T> t)
        {
            Add(coords, t, 0);
        }

        private void ResetTiles()
        {
            foreach ((int, int) coords in this)
            {
                Tile<T> t = Get(coords);
                t.Reset();
            }

            _tileQueue.Clear();
        }

        public IEnumerable<Tile<T>> FindAllTilesWithinRange((int, int) startPos, int moveDistance,
            int verticalDistance = 0)
        {
            ResetTiles();

            List<Tile<T>> tilesWithinRange = new List<Tile<T>>();

            Tile<T> startingTile = Get(startPos);

            _tileQueue.Enqueue(startingTile);
            tilesWithinRange.Add(startingTile);

            while (_tileQueue.Count > 0)
            {
                Tile<T> t = _tileQueue.Dequeue();
                t.Visited = true;

                // the tile is too far away, we have reached the edge
                if (t.Distance > moveDistance)
                {
                    continue;
                }

                foreach (Tile<T> neighbour in Neighbours(t))
                {
                    if (neighbour == null)
                    {
                        continue;
                    }


                    if (neighbour.Visited)
                    {
                        continue;
                    }

                    int heightDiff = Mathf.Abs(t.Height - neighbour.Height);

                    // the node is vertically too high, we can't reach it
                    if (heightDiff > verticalDistance)
                    {
                        continue;
                    }


                    neighbour.Parent = t;
                    neighbour.Distance = t.Distance + 1;

                    _tileQueue.Enqueue(neighbour);
                    tilesWithinRange.Add(neighbour);
                }
            }


            return tilesWithinRange;
        }

        public IEnumerable<Tile<T>> FindAllTilesWithinRange(Tile<T> startingTile, int moveDistance,
            int verticalDistance)
        {
            return FindAllTilesWithinRange(Get(startingTile), moveDistance, verticalDistance);
        }


        public IEnumerable<Tile<T>> BuildPathFromTile(Tile<T> endTile)
        {
            List<Tile<T>> path = new List<Tile<T>>();

            if (endTile == null || endTile.Parent == null)
            {
                // Path finding hasn't been done, there is no path
                return path;
            }

            Tile<T> currTile = endTile;
            while (currTile != null)
            {
                path.Add(currTile);
                currTile = currTile.Parent;
            }

            return path;
        }

        private IEnumerable<Tile<T>> Neighbours(Tile<T> startingTile)
        {
            return Neighbours(Get(startingTile));
        }

        private IEnumerable<Tile<T>> Neighbours((int, int) startingPos)
        {
            (int, int) rightTileCoords = (startingPos.Item1 + 1, startingPos.Item2);
            if (ContainsKey(rightTileCoords))
            {
                yield return Get(rightTileCoords);
            }

            (int, int) leftTileCoords = (startingPos.Item1 - 1, startingPos.Item2);
            if (ContainsKey(leftTileCoords))
            {
                yield return Get(leftTileCoords);
            }

            (int, int) upTileCoords = (startingPos.Item1, startingPos.Item2 + 1);
            if (ContainsKey(upTileCoords))
            {
                yield return Get(upTileCoords);
            }

            (int, int) downTileCoords = (startingPos.Item1, startingPos.Item2 - 1);
            if (ContainsKey(downTileCoords))
            {
                yield return Get(downTileCoords);
            }
        }
    }
}