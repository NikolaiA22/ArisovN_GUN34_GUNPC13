using UnityEngine;

[CreateAssetMenu(fileName = "CellPaletteSettings", menuName = "Game/Cell Palette Settings")]
public class CellPaletteSettings : ScriptableObject
{
    [Header("Selection Materials")]
    [Tooltip("�������� ��� ������ ��������� ������")]
    public Material selectedMaterial;

    [Header("Movement Options")]
    [Tooltip("�������� ��� ������, �� ������� ����� �����")]
    public Material moveableMaterial;

    [Tooltip("�������� ��� ������, �� ������� ����� ���������")]
    public Material attackableMaterial;

    [Tooltip("�������� ��� ������, �� ������� ����� � ����� � ���������")]
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