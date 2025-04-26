using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class CellManager : MonoBehaviour
{
    public event System.Action<Cell> OnCellClicked;

    [SerializeField] private CellPaletteSettings paletteSettings;

    private List<Cell> cells = new List<Cell>();
    private List<Unit> units = new List<Unit>();

    private void Start()
    {
        FindAllCells();
        FindAllUnits();
        SetupCellNeighbours();
        SubscribeToCellEvents();
        SetupUnits();
    }

    private void FindAllCells()
    {
        cells.AddRange(FindObjectsOfType<Cell>());
    }

    private void FindAllUnits()
    {
        units.AddRange(FindObjectsOfType<Unit>());
    }

    private void SetupCellNeighbours()
    {
        foreach (Cell cell in cells)
        {
            Vector3 cellPos = cell.transform.position;
            float cellSize = 1f; // или получить из префаба

            cell.Neighbours = new Dictionary<NeighbourType, Cell>();

            // Проверяем всех возможных соседей
            CheckNeighbour(cell, NeighbourType.Left, new Vector3(-cellSize, 0, 0));
            CheckNeighbour(cell, NeighbourType.Right, new Vector3(cellSize, 0, 0));
            CheckNeighbour(cell, NeighbourType.Top, new Vector3(0, 0, cellSize));
            CheckNeighbour(cell, NeighbourType.Bottom, new Vector3(0, 0, -cellSize));
            CheckNeighbour(cell, NeighbourType.TopLeft, new Vector3(-cellSize, 0, cellSize));
            CheckNeighbour(cell, NeighbourType.TopRight, new Vector3(cellSize, 0, cellSize));
            CheckNeighbour(cell, NeighbourType.BottomLeft, new Vector3(-cellSize, 0, -cellSize));
            CheckNeighbour(cell, NeighbourType.BottomRight, new Vector3(cellSize, 0, -cellSize));
        }
    }

    private void CheckNeighbour(Cell cell, NeighbourType type, Vector3 offset)
    {
        Vector3 neighbourPos = cell.transform.position + offset;
        Cell neighbour = cells.FirstOrDefault(c => c.transform.position == neighbourPos);

        if (neighbour != null)
        {
            cell.Neighbours[type] = neighbour;
        }
    }

    private void SetupUnits()
    {
        foreach (Unit unit in units)
        {
            Vector3 unitPos = unit.transform.position;
            Cell cell = cells.FirstOrDefault(c =>
                Vector3.Distance(c.transform.position, unitPos) < 0.5f);

            if (cell != null)
            {
                unit.Cell = cell;
                cell.Unit = unit;
                unit.transform.position = cell.transform.position + Vector3.up * 0.5f;
            }
        }
    }

    private void SubscribeToCellEvents()
    {
        foreach (Cell cell in cells)
        {
            cell.OnPointerClickEvent += HandleCellClick;
        }
    }

    private void HandleCellClick(Cell cell)
    {
        OnCellClicked?.Invoke(cell);
    }
}

