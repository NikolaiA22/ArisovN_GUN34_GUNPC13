using System.Collections.Generic;
using UnityEngine;

public class CellManager : MonoBehaviour
{
    public static CellManager Instance { get; private set; }

    public delegate void CellClickHandler(Cell clickedCell);
    public event CellClickHandler OnCellClicked;

    private Dictionary<Cell, NeighbourType> _neighbourCache = new Dictionary<Cell, NeighbourType>();
    private List<Cell> _allCells = new List<Cell>();
    private List<Unit> _allUnits = new List<Unit>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        InitializeAllCells();
        LinkUnitsToCells();
    }

    private void InitializeAllCells()
    {
        _allCells = new List<Cell>(FindObjectsOfType<Cell>());

        foreach (var cell in _allCells)
        {
            // Находим соседей для каждой клетки
            NeighbourType neighbours = FindNeighbours(cell);
            _neighbourCache[cell] = neighbours;

            // Подписываемся на клики
            cell.OnPointerClickEvent += HandleCellClick;
        }
    }

    private NeighbourType FindNeighbours(Cell targetCell)
    {
        NeighbourType result = NeighbourType.None;
        float cellSize = 1f; // Зависит от размера ваших клеток
        float checkDistance = cellSize * 1.1f; // Небольшой запас

        foreach (var otherCell in _allCells)
        {
            if (otherCell == targetCell) continue;

            Vector3 direction = otherCell.transform.position - targetCell.transform.position;
            float distance = direction.magnitude;

            if (distance <= checkDistance)
            {
                direction.Normalize();

                if (Vector3.Dot(direction, Vector3.right) > 0.9f)
                    result |= NeighbourType.Right;
                else if (Vector3.Dot(direction, Vector3.left) > 0.9f)
                    result |= NeighbourType.Left;
                else if (Vector3.Dot(direction, Vector3.forward) > 0.9f)
                    result |= NeighbourType.Top;
                else if (Vector3.Dot(direction, Vector3.back) > 0.9f)
                    result |= NeighbourType.Bottom;
            }
        }

        return result;
    }

    private void LinkUnitsToCells()
    {
        _allUnits = new List<Unit>(FindObjectsOfType<Unit>());

        foreach (var unit in _allUnits)
        {
            Cell closestCell = FindClosestCell(unit.transform.position);
            if (closestCell != null)
            {
                unit.Cell = closestCell;
                closestCell.Unit = unit;
            }
        }
    }

    private Cell FindClosestCell(Vector3 position)
    {
        Cell closestCell = null;
        float minDistance = float.MaxValue;

        foreach (var cell in _allCells)
        {
            float distance = Vector3.Distance(position, cell.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestCell = cell;
            }
        }

        return closestCell;
    }

    private void HandleCellClick(Cell clickedCell)
    {
        OnCellClicked?.Invoke(clickedCell);
    }

    public NeighbourType GetNeighbours(Cell cell)
    {
        return _neighbourCache.TryGetValue(cell, out var neighbours)
            ? neighbours
            : NeighbourType.None;
    }

    private void OnDestroy()
    {
        foreach (var cell in _allCells)
        {
            if (cell != null)
                cell.OnPointerClickEvent -= HandleCellClick;
        }
    }
}
