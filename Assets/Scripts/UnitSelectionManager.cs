using UnityEngine;

public class UnitSelectionManager : MonoBehaviour
{
    public static UnitSelectionManager Instance { get; private set; }

    public Unit SelectedUnit { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void SelectUnit(Unit unit)
    {
        if (SelectedUnit != null)
        {
            SelectedUnit.Deselect();
        }

        SelectedUnit = unit;
    }

    public void MoveSelectedUnit(Cell targetCell)
    {
        if (SelectedUnit == null || targetCell == null) return;

        SelectedUnit.MoveToCell(targetCell);
        SelectedUnit = null;
    }
}
