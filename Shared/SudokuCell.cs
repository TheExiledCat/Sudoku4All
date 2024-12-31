using System;
using System.Collections.Generic;
using YAXLib.Attributes;

namespace Sudoku.Shared;

public class SudokuCell
{
    public VectorInt Position { get; private init; }

    public List<int> PencilMarks { get; set; } = [];

    [YAXAttributeForClass]
    public int Value { get; set; }

    public SudokuCell(VectorInt gridPosition)
    {
        Position = gridPosition;
    }
}
