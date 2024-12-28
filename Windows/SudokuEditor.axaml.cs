using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Platform.Storage;
using Avalonia.Threading;
using Sudoku.Tools;
using Sdk = Sudoku.Shared;

namespace Sudoku.Windows;

public partial class SudokuEditor : Window
{
    Sdk.Sudoku m_CurrentSudoku;
    IStorageFile? m_CurrentFile;
    bool m_SudokuChanged = true;
    public SudokuEditor(Sdk.Sudoku? sudoku = null, IStorageFile? file = null)
    {
        InitializeComponent();
        if (sudoku == null)
        {
            sudoku = new Sdk.Sudoku();
        }
        m_CurrentFile = file;
        m_CurrentSudoku = sudoku;
        Resized += (s, e) =>
        {
            Toolbar.OpenPaneLength = Width / 4;
        };
    }
    public void SaveAsOrOverwriteHandler(object sender, RoutedEventArgs args)
    {
        if (m_CurrentFile == null)
        {
            SaveAs();
        }
        else if (m_SudokuChanged)
        {
            Overwrite();
        }
    }

    public void SaveAsHandler(object sender, RoutedEventArgs args)
    {
        SaveAs();
    }
    public void ToggleToolBarHandler(object sender, RoutedEventArgs args)
    {
        Toolbar.IsPaneOpen = !Toolbar.IsPaneOpen;
    }
    public void HomeMenuHandler(object sender, RoutedEventArgs args)
    {
        //check if unsaved progress

        //go to home
        MainWindow window = new MainWindow();
        window.Show();
        Close();
    }
    async Task Overwrite()
    {

    }
    async Task SaveAs()
    {
        string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Sudoku");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        IStorageFolder startlocation = await StorageProvider.TryGetFolderFromPathAsync(path);
        File.WriteAllText("log.txt", startlocation.Path.ToString());

        IStorageFile? file = await StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions()
        {
            Title = "Choose name for file",
            DefaultExtension = ".sdku",
            FileTypeChoices = [new FilePickerFileType("Sudoku file") { Patterns = ["*.sdku"] }],
            ShowOverwritePrompt = true,
            SuggestedStartLocation = startlocation

        });
        if (file == null)
        {
            return;
        }
        m_CurrentFile = file;
        SudokuHelpers.SaveSudoku(m_CurrentSudoku, m_CurrentFile);
        m_SudokuChanged = false;
        UpdateTitle();
    }
    void UpdateTitle()
    {
        Title = $"Editor {(m_CurrentFile != null ? m_CurrentFile.Name : "")} {DateTime.Now.ToString("yyyy-mm-dd")}";
    }
}