using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using Avalonia.Threading;
using Sudoku.Windows;
namespace Sudoku;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    public void PlaySudokuHandler(object sender, RoutedEventArgs e)
    {

    }
    public void EditorSudokuHandler(object sender, RoutedEventArgs e)
    {

        RunSelectionWindow();
    }
    public async Task RunSelectionWindow()
    {
        EditorSelectionWindow editorSelectionWindow = new EditorSelectionWindow();
        await Dispatcher.UIThread.InvokeAsync(async () => await editorSelectionWindow.ShowDialog(this));
        SudokuEditor editor = new SudokuEditor(editorSelectionWindow.SelectedSudoku);
        editor.Show();
        this.Close();
    }
    public void OptionsSudokuHandler(object sender, RoutedEventArgs e)
    {

    }
    public void QuitSudokuHandler(object sender, RoutedEventArgs e)
    {
        if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktopApp)
        {
            desktopApp.Shutdown();
        }
    }

}