using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace Sudoku.Windows;

public partial class EditorSelectionWindow : Window
{
    public Sudoku.Shared.Sudoku? SelectedSudoku { get; set; }
    public EditorSelectionWindow()
    {
        InitializeComponent();
    }
    public void BrowseSudokuHandler(object sender, RoutedEventArgs e)
    {

    }
    public void CreateSudokuHandler(object sender, RoutedEventArgs e)
    {
        SelectedSudoku = new Shared.Sudoku();
        Close();
    }
}