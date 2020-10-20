using System.Collections.Generic;
using Core.Util;
using UnityEngine;

namespace Core.PathFinding
{
    public class Grid<T> : MultiMap<(int, int), Tile<T>> where T : MonoBehaviour
    {
        private readonly Dictionary<Tile<T>, int> _tileHeights = new Dictionary<Tile<T>, int>();
        private readonly Queue<Tile<T>> _tileQueue = new Queue<Tile<T>>();
        private readonly HashSet<Tile<T>> _visitedTiles = new HashSet<Tile<T>>();
        private readonly Dictionary<Tile<T>, int> _depthMap = new Dictionary<Tile<T>, int>();

        public void Add((int, int) coords, Tile<T> t, int height)
        {
            base.Add(coords, t);
            _tileHeights[t] = height;
        }

        public override void Add((int, int) coords, Tile<T> t)
        {
            Add(coords, t, 0);
        }

        private void ClearCache()
        {
            _tileQueue.Clear();
            _visitedTiles.Clear();
            _depthMap.Clear();
        }

        public IEnumerable<Tile<T>> FindAllTilesWithinRange((int, int) startPos, int moveDistance,
            int verticalDistance = 0)
        {
            ClearCache();

            List<Tile<T>> tilesWithinRange = new List<Tile<T>>();

            Tile<T> startingTile = Get(startPos);

            _tileQueue.Enqueue(startingTile);
            tilesWithinRange.Add(startingTile);
            _depthMap[startingTile] = 0;

            while (_tileQueue.Count > 0)
            {
                Tile<T> t = _tileQueue.Dequeue();
                _visitedTiles.Add(t);

                // the tile is too far away, we have reached the edge
                if (_depthMap[t] > moveDistance)
                {
                    continue;
                }


                foreach (Tile<T> neighbour in Neighbours(t))
                {
                    if (neighbour == null)
                    {
                        continue;
                    }

                    if (_visitedTiles.Contains(neighbour))
                    {
                        continue;
                    }

                    int heightDiff = Mathf.Abs(GetTileHeight(t) - GetTileHeight(neighbour));

                    // the node is vertically too high, we can't reach it
                    if (heightDiff > verticalDistance)
                    {
                        continue;
                    }

                    _depthMap[neighbour] = _depthMap[t] + 1;
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

        private int GetTileHeight(Tile<T> tile)
        {
            return _tileHeights[tile];
        }

        private IEnumerable<Tile<T>> Neighbours(Tile<T> startingTile)
        {
            return Neighbours(Get(startingTile));
        }

        private IEnumerable<Tile<T>> Neighbours((int, int) startingPos)
        {
            yield return Get((startingPos.Item1 + 1, startingPos.Item2));
            yield return Get((startingPos.Item1 - 1, startingPos.Item2));
            yield return Get((startingPos.Item1, startingPos.Item2 + 1));
            yield return Get((startingPos.Item1, startingPos.Item2 - 1));
        }
    }
}