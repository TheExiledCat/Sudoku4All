using System;
using System.Collections.Generic;
using YAXLib.Attributes;

namespace Sudoku.Shared;

public class SudokuRow : List<SudokuCell>
{
    [YAXAttributeForClass]
    public int RowNumber { get; set; }

    public SudokuRow(int rownumber)
    {
        RowNumber = rownumber;
    }
}
