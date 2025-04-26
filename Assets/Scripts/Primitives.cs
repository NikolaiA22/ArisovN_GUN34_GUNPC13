using System;

[Flags]
public enum NeighbourType
{
    None = 0,
    Left = 1,
    Right = 2,
    Top = 4,
    Bottom = 8,
    TopLeft = 16,
    TopRight = 32,
    BottomLeft = 64,
    BottomRight = 128
}

public enum Team
{
    Player1,
    Player2
}
