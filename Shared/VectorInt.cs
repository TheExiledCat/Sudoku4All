using System;
using YAXLib.Attributes;

namespace Sudoku.Shared;

public struct VectorInt(int x, int y)
{
    [YAXAttributeForClass]
    public int X { get; set; } = x;

    [YAXAttributeForClass]
    public int Y { get; set; } = y;
}
