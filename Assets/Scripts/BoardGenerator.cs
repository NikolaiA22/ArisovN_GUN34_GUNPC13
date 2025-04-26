using System.Linq;
using UnityEngine;

public class BoardGenerator : MonoBehaviour
{
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private Material darkMaterial;
    [SerializeField] private Material lightMaterial;
    [SerializeField] private int boardSize = 8;
    [SerializeField] private float cellSize = 1f;
    [SerializeField] private GameObject[] unitPrefabs;
    [SerializeField] private Vector2Int[] unitPositions;

    private void Start()
    {
        GenerateBoard();
    }

    private void GenerateBoard()
    {
        for (int x = 0; x < boardSize; x++)
        {
            for (int z = 0; z < boardSize; z++)
            {
                Vector3 position = new Vector3(
                    x * cellSize - (boardSize * cellSize) / 2 + cellSize / 2,
                    0,
                    z * cellSize - (boardSize * cellSize) / 2 + cellSize / 2);

                GameObject cell = Instantiate(cellPrefab, position, Quaternion.identity, transform);
                cell.name = $"Cell_{x}_{z}";

                MeshRenderer renderer = cell.GetComponent<MeshRenderer>();
                renderer.material = (x + z) % 2 == 0 ? lightMaterial : darkMaterial;
            }
        }
        for (int i = 0; i < unitPrefabs.Length; i++)
        {
            if (i >= unitPositions.Length) break;

            Vector2Int pos = unitPositions[i];
            Cell cell = GetCellAtPosition(pos.x, pos.y);
            if (cell != null)
            {
                Instantiate(unitPrefabs[i], cell.transform.position + Vector3.up, Quaternion.identity);
            }
        }
    }

    private Cell GetCellAtPosition(int x, int z)
    {
        return FindObjectsOfType<Cell>()
            .FirstOrDefault(c => Mathf.Approximately(c.transform.position.x, x) &&
                               Mathf.Approximately(c.transform.position.z, z));
    }
}
