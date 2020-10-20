using UnityEngine;

namespace Core.PathFinding
{
    public abstract class Tile<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] private T gridObject;
        [SerializeField] private int height;

        public T GridObject => gridObject;

        public virtual bool IsEmpty => gridObject == null;
    }
}