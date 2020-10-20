using UnityEngine;

namespace Core.PathFinding
{
    public class GridTest : MonoBehaviour
    {
        private Grid<MonoTest> grid;


        private void Awake()
        {
            grid = new Grid<MonoTest>();
        }

        private void Start()
        {
            int height = 0;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.name = $"cube_{i}_{j}";
                    MonoTest t = cube.AddComponent<MonoTest>();
                    cube.transform.position = new Vector3(i, height, j);
                    grid.Add((i, j), t, height);
                }

                height += 2;
            }


            var tiles = grid.FindAllTilesWithinRange((5, 5), 2, 2);
            foreach (Tile<MonoTest> t in tiles)
            {
                Destroy(t.gameObject);
                Debug.Log(t.name);
            }
        }
    }
}