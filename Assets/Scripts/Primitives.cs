using System;
using UnityEngine;

[Flags]
[Serializable]
public enum NeighbourType
{
    None = 0,
    Left = 1 << 0,
    Right = 1 << 1,
    Top = 1 << 2,
    Bottom = 1 << 3
}

[Serializable]
public enum Team
{
    None = 0,
    Player1 = 1,
    Player2 = 2,
}
