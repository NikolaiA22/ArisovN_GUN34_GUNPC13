using UnityEngine;
using System.Collections.Generic;

public class CellHighlighter : MonoBehaviour
{
    public static CellHighlighter Instance { get; private set; }

    [Header("Highlight Materials")]
    [SerializeField] private Material movableMaterial;
    [SerializeField] private Material attackableMaterial;
    [SerializeField] private Material moveAndAttackMaterial;

    private List<Cell> _highlightedCells = new List<Cell>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void HighlightMovementCells(Cell centerCell, int movementRange = 1)
    {
        ResetHighlight(); // Сначала сбрасываем предыдущее выделение

        if (centerCell == null || CellManager.Instance == null) return;

        var neighbours = CellManager.Instance.GetNeighbours(centerCell, movementRange);

        foreach (var cell in neighbours)
        {
            if (cell == null) continue;

            // Определяем тип подсветки в зависимости от условий
            if (cell.Unit == null)
            {
                cell.SetHighlight(movableMaterial);
                _highlightedCells.Add(cell);
            }
            // Здесь можно добавить другие условия для attackable и moveAndAttack
        }
    }

    public void ResetHighlight()
    {
        foreach (var cell in _highlightedCells)
        {
            if (cell != null)
                cell.ResetHighlight();
        }
        _highlightedCells.Clear();
    }
}
