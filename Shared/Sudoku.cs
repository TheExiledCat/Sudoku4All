using System;
using CommunityToolkit.Mvvm.ComponentModel;
using YAXLib.Attributes;

namespace Sudoku.Shared;

public partial class Sudoku : ObservableObject
{
    string title,
        version,
        rules;

    [YAXAttributeForClass]
    public string Title
    {
        get => title;
        set
        {
            if (title != value)
            {
                title = value;
                OnPropertyChanged(nameof(Rules));
            }
        }
    }

    [YAXAttributeForClass]
    public string Version
    {
        get => version;
        set
        {
            if (version != value)
            {
                version = value;
                OnPropertyChanged(nameof(Rules));
            }
        }
    }
    public string Rules
    {
        get => rules;
        set
        {
            if (rules != value)
            {
                rules = value;
                OnPropertyChanged(nameof(Rules));
            }
        }
    }
    const int DEFAULT_GRID_SIZE = 9;
    public SudokuGrid Grid { get; set; } = new SudokuGrid(9, 9);

    public Sudoku(string _title = "New Sudoku", string _version = "0.1")
    {
        Title = _title;
        Version = _version;
    }
}
