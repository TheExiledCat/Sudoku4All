using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using YAXLib.Attributes;

namespace Sudoku.Shared;

public class SudokuGrid
{
    [YAXAttributeForClass]
    public uint Width { get; set; }

    [YAXAttributeForClass]
    public uint Height { get; set; }
    public List<SudokuRow> Cells { get; set; } = [];

    public SudokuGrid(uint width, uint height)
    {
        Width = width;
        Height = height;

        for (int row = 0; row < height; row++)
        {
            Cells.Add(new SudokuRow(row));

            for (int col = 0; col < width; col++)
            {
                Cells[row].Add(new SudokuCell(new VectorInt(col, row)));
            }
        }
    }
}
