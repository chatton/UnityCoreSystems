using UnityEngine;

namespace Core.PathFinding
{
    public abstract class Tile<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] private T gridObject;

        public T GridObject => gridObject;

        public virtual bool IsEmpty => gridObject == null;

        // internal values used to track the state of ongoing pathfinding happening
        // to these tiles

        // Height does not get reset
        public int Height { get; internal set; }
        
        public int Distance { get; internal set; }
        internal bool Visited;
        internal Tile<T> Parent;

        internal void Reset()
        {
            Visited = false;
            Parent = null;
            Distance = 0;
        }
    }
}