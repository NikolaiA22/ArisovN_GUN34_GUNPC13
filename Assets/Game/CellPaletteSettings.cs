using UnityEngine;

[CreateAssetMenu(fileName = "CellPaletteSettings", menuName = "Game/Cell Palette Settings")]
public class CellPaletteSettings : ScriptableObject
{
    [Header("Selection Materials")]
    [Tooltip("ћатериал дл€ просто выбранной клетки")]
    public Material selectedMaterial;

    [Header("Movement Options")]
    [Tooltip("ћатериал дл€ клетки, на которую можно пойти")]
    public Material moveableMaterial;

    [Tooltip("ћатериал дл€ клетки, на которую можно атаковать")]
    public Material attackableMaterial;

    [Tooltip("ћатериал дл€ клетки, на которую можно и пойти и атаковать")]
    public Material moveAndAttackMaterial;

    public Material GetMaterialForState(bool isMoveable, bool isAttackable)
    {
        if (isMoveable && isAttackable)
            return moveAndAttackMaterial;
        if (isMoveable)
            return moveableMaterial;
        if (isAttackable)
            return attackableMaterial;

        return selectedMaterial;
    }
}