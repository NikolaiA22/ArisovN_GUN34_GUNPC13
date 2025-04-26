using UnityEngine;

[CreateAssetMenu(fileName = "CellPaletteSettings", menuName = "Settings/Cell Palette")]
public class CellPaletteSettings : ScriptableObject
{
    public Material selectedMaterial;
    public Material moveMaterial;
    public Material attackMaterial;
    public Material moveAndAttackMaterial;
}
