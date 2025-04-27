using System.Collections.Generic;
using UnityEngine;

public class CellManager : MonoBehaviour
{
    public static CellManager Instance { get; private set; }
    public List<Cell> AllCells { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        InitializeCells();
    }

    private void InitializeCells()
    {
        AllCells = new List<Cell>(FindObjectsOfType<Cell>());
        Debug.Log($"Initialized {AllCells.Count} cells");
    }

    public List<Cell> GetNeighbours(Cell centerCell, int radius)
    {
        if (centerCell == null || AllCells == null)
        {
            Debug.LogError("Invalid parameters!");
            return new List<Cell>();
        }

        List<Cell> neighbours = new List<Cell>();

        foreach (var cell in AllCells)
        {
            if (cell == null || cell == centerCell) continue;

            float distance = Vector3.Distance(centerCell.transform.position, cell.transform.position);
            if (distance <= radius * 1.5f)
            {
                neighbours.Add(cell);
            }
        }

        return neighbours;
    }
}