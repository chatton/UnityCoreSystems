using UnityEngine;

namespace Core.Util
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance != null) return _instance;

                T t = FindObjectOfType<T>();
                if (t != null)
                {
                    _instance = t;
                    return _instance;
                }

                GameObject go = new GameObject();
                _instance = go.AddComponent<T>();

                return _instance;
            }
            private set => _instance = value;
        }
    }
}